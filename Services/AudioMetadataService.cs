using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NAudio.Wave;
using CompressAudioFiles.Models;

namespace CompressAudioFiles.Services
{
    class AudioMetadataService
    {
        public AudioMetadata ExtractAudioMetadata(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("Audio file path is empty.");

            if (!File.Exists(filePath))
                throw new FileNotFoundException("Audio file not found.", filePath);

            using (var reader = new AudioFileReader(filePath))
            {
                long fileSize = GetAudioFileSize(filePath);
                TimeSpan duration = reader.TotalTime;
                int sampleRate = reader.WaveFormat.SampleRate;
                int channels = reader.WaveFormat.Channels;
                int bitRate = GetBitRate(filePath, sampleRate, channels, reader.WaveFormat.BitsPerSample);
                string encodingType = GetEncodingType(filePath);

                return new AudioMetadata
                {
                    FileSize = fileSize,
                    Duration = duration,
                    SampleRate = sampleRate,
                    ChannelsCount = channels,
                    BitRate = bitRate,
                    EncodingType = encodingType
                };
            }
        }

        public long GetAudioFileSize(string filePath)
        {
            return new FileInfo(filePath).Length;
        }

        public TimeSpan GetAudioDuration(string filePath)
        {
            using (var reader = new AudioFileReader(filePath))
            {
                return reader.TotalTime;
            }
        }

        public int GetSampleRate(string filePath)
        {
            using (var reader = new AudioFileReader(filePath))
            {
                return reader.WaveFormat.SampleRate;
            }
        }

        public int GetChannelsCount(string filePath)
        {
            using (var reader = new AudioFileReader(filePath))
            {
                return reader.WaveFormat.Channels;
            }
        }

        public int GetBitRate(string filePath)
        {
            using (var reader = new AudioFileReader(filePath))
            {
                return GetBitRate(
                    filePath,
                    reader.WaveFormat.SampleRate,
                    reader.WaveFormat.Channels,
                    reader.WaveFormat.BitsPerSample
                );
            }
        }

        public string GetEncodingType(string filePath)
        {
            string extension = Path.GetExtension(filePath);

            if (string.IsNullOrWhiteSpace(extension))
                return "Unknown";

            return extension.Replace(".", "").ToUpper();
        }

        private int GetBitRate(string filePath, int sampleRate, int channels, int bitsPerSample)
        {
            if (bitsPerSample > 0)
            {
                return sampleRate * channels * bitsPerSample;
            }

            TimeSpan duration = GetAudioDuration(filePath);

            if (duration.TotalSeconds <= 0)
                return 0;

            long fileSizeInBits = GetAudioFileSize(filePath) * 8;
            return (int)(fileSizeInBits / duration.TotalSeconds);
        }
    }
}
