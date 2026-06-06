using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompressAudioFiles.Services
{
    class AudioPlayerService
    {
       
        private string currentAudioPath;
        private string currentAudioName;
        private bool isAudioLoaded;
        private bool isAudioPlaying;
        private bool isAudioPaused;
        private TimeSpan currentAudioPosition;
        private TimeSpan currentAudioDuration;
        private AudioFileReader audioReader;
        private WaveOutEvent outputDevice;
        private System.Timers.Timer positionTimer;

       
        public string CurrentAudioPath => currentAudioPath;
        public string CurrentAudioName => currentAudioName;
        public bool IsAudioLoaded => isAudioLoaded;
        public bool IsAudioPlaying => isAudioPlaying;
        public bool IsAudioPaused => isAudioPaused;
        public TimeSpan CurrentAudioDuration => currentAudioDuration;
        public event Action<TimeSpan> OnPositionChanged;
        public AudioPlayerService()
        {
            isAudioLoaded = false;
            isAudioPlaying = false;
            isAudioPaused = false;
            positionTimer = new System.Timers.Timer(500);
            positionTimer.Elapsed += (s, e) => UpdatePositionTick();
        }

        public bool LoadAudioForPreview(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                    return false;
                StopAndDispose();

                currentAudioPath = filePath;
                currentAudioName = Path.GetFileName(filePath);
                audioReader = new AudioFileReader(filePath);
                outputDevice = new WaveOutEvent();
                outputDevice.Init(audioReader);
                currentAudioDuration = audioReader.TotalTime;
                outputDevice.PlaybackStopped += OnPlaybackStopped;

                isAudioLoaded = true;
                isAudioPlaying = false;
                isAudioPaused = false;

                return true;
            }
            catch
            {
                isAudioLoaded = false;
                return false;
            }
        }
        public void PlayAudio()
        {
            if (!isAudioLoaded || outputDevice == null)
                return;
            if (isAudioPlaying)
                return;
            outputDevice.Play();
            isAudioPlaying = true;
            isAudioPaused = false;
            positionTimer.Start();
        }
        public void PauseAudio()
        {
            if (!isAudioPlaying || outputDevice == null)
                return;
            outputDevice.Pause();
            positionTimer.Stop();
            isAudioPlaying = false;
            isAudioPaused = true;
        }
        public void StopAudio()
        {
            if (outputDevice != null)
                outputDevice.Stop();
            if (audioReader != null)
                audioReader.Position = 0;

            positionTimer.Stop();
            isAudioPlaying = false;
            isAudioPaused = false;
            currentAudioPosition = TimeSpan.Zero;
            OnPositionChanged?.Invoke(TimeSpan.Zero);
        }
        public TimeSpan GetAudioCurrentPosition()
        {
            return audioReader?.CurrentTime ?? TimeSpan.Zero;
        }

        public void SetAudioPosition(TimeSpan position)
        {
            if (audioReader == null) return;
            if (position < TimeSpan.Zero)
                position = TimeSpan.Zero;
            if (position > currentAudioDuration)
                position = currentAudioDuration;
            audioReader.CurrentTime = position;
        }
        private void UpdatePositionTick()
        {
            if (!isAudioPlaying) return;
            currentAudioPosition = GetAudioCurrentPosition();
            OnPositionChanged?.Invoke(currentAudioPosition);
        }
        private void OnPlaybackStopped(object sender, StoppedEventArgs e)
        {
            positionTimer.Stop();
            isAudioPlaying = false;
            isAudioPaused = false;

            if (audioReader != null)
                audioReader.CurrentTime = TimeSpan.Zero;

            OnPositionChanged?.Invoke(TimeSpan.Zero);
        }

       
        private void StopAndDispose()
        {
            positionTimer?.Stop();

            if (outputDevice != null)
            {
                outputDevice.Stop();
                outputDevice.Dispose();
                outputDevice = null;
            }

            if (audioReader != null)
            {
                audioReader.Dispose();
                audioReader = null;
            }

            isAudioPlaying = false;
            isAudioPaused = false;
        }

       
        public void Dispose()
        {
            StopAndDispose();
            positionTimer?.Dispose();
        }
    }
}
