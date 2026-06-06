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
       
        private string _currentAudioPath;
        private string _currentAudioName;
        private bool _isAudioLoaded;
        private bool _isAudioPlaying;
        private bool _isAudioPaused;
        private TimeSpan _currentAudioPosition;
        private TimeSpan _currentAudioDuration;
        private WaveOutEvent _outputDevice;   
        private AudioFileReader _audioReader;  
        private System.Timers.Timer _positionTimer;

       
        public string CurrentAudioPath => _currentAudioPath;
        public string CurrentAudioName => _currentAudioName;
        public bool IsAudioLoaded => _isAudioLoaded;
        public bool IsAudioPlaying => _isAudioPlaying;
        public bool IsAudioPaused => _isAudioPaused;
        public TimeSpan CurrentAudioDuration => _currentAudioDuration;

        
        public event Action<TimeSpan> OnPositionChanged;
        public AudioPlayerService()
        {
            _isAudioLoaded = false;
            _isAudioPlaying = false;
            _isAudioPaused = false;
            _positionTimer = new System.Timers.Timer(500);
            _positionTimer.Elapsed += (s, e) => UpdatePositionTick();
        }

        public bool LoadAudioForPreview(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                    return false;
                StopAndDispose();

                _currentAudioPath = filePath;
                _currentAudioName = Path.GetFileName(filePath);
                _audioReader = new AudioFileReader(filePath);
                _outputDevice = new WaveOutEvent();
                _outputDevice.Init(_audioReader);
                _currentAudioDuration = _audioReader.TotalTime;
                _outputDevice.PlaybackStopped += OnPlaybackStopped;

                _isAudioLoaded = true;
                _isAudioPlaying = false;
                _isAudioPaused = false;

                return true;
            }
            catch
            {
                _isAudioLoaded = false;
                return false;
            }
        }
        public void PlayAudio()
        {
            if (!_isAudioLoaded || _outputDevice == null)
                return;
            if (_isAudioPlaying)
                return;
            _outputDevice.Play();
            _isAudioPlaying = true;
            _isAudioPaused = false;
            _positionTimer.Start();
        }
        public void PauseAudio()
        {
            if (!_isAudioPlaying || _outputDevice == null)
                return;
            _outputDevice.Pause();
            _positionTimer.Stop();
            _isAudioPlaying = false;
            _isAudioPaused = true;
        }
        public void StopAudio()
        {
            if (_outputDevice != null)
                _outputDevice.Stop();
            if (_audioReader != null)
                _audioReader.Position = 0;

            _positionTimer.Stop();
            _isAudioPlaying = false;
            _isAudioPaused = false;
            _currentAudioPosition = TimeSpan.Zero;
            OnPositionChanged?.Invoke(TimeSpan.Zero);
        }
        public TimeSpan GetAudioCurrentPosition()
        {
            return _audioReader?.CurrentTime ?? TimeSpan.Zero;
        }

        public void SetAudioPosition(TimeSpan position)
        {
            if (_audioReader == null) return;
            if (position < TimeSpan.Zero)
                position = TimeSpan.Zero;
            if (position > _currentAudioDuration)
                position = _currentAudioDuration;
            _audioReader.CurrentTime = position;
        }
        private void UpdatePositionTick()
        {
            if (!_isAudioPlaying) return;
            _currentAudioPosition = GetAudioCurrentPosition();
            OnPositionChanged?.Invoke(_currentAudioPosition);
        }
        private void OnPlaybackStopped(object sender, StoppedEventArgs e)
        {
            _positionTimer.Stop();
            _isAudioPlaying = false;
            _isAudioPaused = false;

            if (_audioReader != null)
                _audioReader.CurrentTime = TimeSpan.Zero;

            OnPositionChanged?.Invoke(TimeSpan.Zero);
        }

       
        private void StopAndDispose()
        {
            _positionTimer?.Stop();

            if (_outputDevice != null)
            {
                _outputDevice.Stop();
                _outputDevice.Dispose();
                _outputDevice = null;
            }

            if (_audioReader != null)
            {
                _audioReader.Dispose();
                _audioReader = null;
            }

            _isAudioPlaying = false;
            _isAudioPaused = false;
        }

       
        public void Dispose()
        {
            StopAndDispose();
            _positionTimer?.Dispose();
        }
    }
}
