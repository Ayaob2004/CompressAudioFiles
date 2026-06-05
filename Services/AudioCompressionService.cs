using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using NAudio.Wave;
using CompressAudioFiles.Models;


namespace CompressAudioFiles.Services
{
    class AudioCompressionService
    {
        public CompressionResult CompressAudio(string inputPath, CompressionSettings settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            if (string.IsNullOrWhiteSpace(settings.AlgorithmName))
                throw new ArgumentException("Algorithm name is required.");

            switch (settings.AlgorithmName)
            {
                case CompressionAlgorithms.AdaptiveDeltaModulation:
                    return CompressUsingAdaptiveDeltaModulation(inputPath, settings);
                case CompressionAlgorithms.DeltaModulation:
                    return CompressUsingDeltaModulation(inputPath, settings);
                case CompressionAlgorithms.NonlinearQuantization:
                case CompressionAlgorithms.DPCM:
                case CompressionAlgorithms.PredictiveDifferentialCoding:
                
                    throw new NotSupportedException(
                        "This algorithm is listed in the project plan, but it is not implemented in this section yet."
                    );

                default:
                    throw new NotSupportedException("Unknown compression algorithm.");
            }
        }

        public CompressionResult CompressUsingAdaptiveDeltaModulation(string inputPath, CompressionSettings settings)
        {
            if (string.IsNullOrWhiteSpace(inputPath))
                throw new ArgumentException("Input audio path is empty.");

            if (!File.Exists(inputPath))
                throw new FileNotFoundException("Input audio file not found.", inputPath);

            Stopwatch stopwatch = Stopwatch.StartNew();

            string outputPath = GenerateCompressedFilePath(
                inputPath,
                CompressionAlgorithms.AdaptiveDeltaModulation
            );

            long totalSamplesWritten = 0;

            const int initialStep = 512;
            const int minStep = 16;
            const int maxStep = 8192;

            const double increaseFactor = 1.25;
            const double decreaseFactor = 0.75;

            using (var reader = new AudioFileReader(inputPath))
            using (var writer = new BinaryWriter(File.Create(outputPath)))
            {
                long totalSamplesPosition = WriteHeader(
                    writer,
                    reader.WaveFormat.SampleRate,
                    reader.WaveFormat.Channels,
                    reader.WaveFormat.BitsPerSample,
                    initialStep,
                    minStep,
                    maxStep,
                    increaseFactor,
                    decreaseFactor
                );

                int predictedSample = 0;
                int stepSize = initialStep;
                int previousBit = -1;

                byte currentByte = 0;
                int bitPosition = 0;

                float[] buffer = new float[4096];
                int samplesRead;

                while ((samplesRead = reader.Read(buffer, 0, buffer.Length)) > 0)
                {
                    for (int i = 0; i < samplesRead; i++)
                    {
                        short currentSample = FloatToPcm16(buffer[i]);

                        int bit;

                        if (currentSample >= predictedSample)
                        {
                            bit = 1;
                            predictedSample += stepSize;
                        }
                        else
                        {
                            bit = 0;
                            predictedSample -= stepSize;
                        }

                        predictedSample = Clamp(
                            predictedSample,
                            short.MinValue,
                            short.MaxValue
                        );

                        if (previousBit == bit)
                        {
                            stepSize = (int)(stepSize * increaseFactor);
                        }
                        else
                        {
                            stepSize = (int)(stepSize * decreaseFactor);
                        }

                        stepSize = Clamp(stepSize, minStep, maxStep);

                        PackBit(ref currentByte, ref bitPosition, bit, writer);

                        previousBit = bit;
                        totalSamplesWritten++;
                    }
                }

                if (bitPosition > 0)
                {
                    writer.Write(currentByte);
                }

                writer.BaseStream.Seek(totalSamplesPosition, SeekOrigin.Begin);
                writer.Write(totalSamplesWritten);
            }

            stopwatch.Stop();

            long originalSize = new FileInfo(inputPath).Length;
            long compressedSize = new FileInfo(outputPath).Length;

            return new CompressionResult
            {
                CompressedFilePath = outputPath,
                OriginalFileSize = originalSize,
                CompressedFileSize = compressedSize,
                CompressionRatio = compressedSize == 0
                    ? 0
                    : (double)originalSize / compressedSize,
                CompressionTime = stopwatch.Elapsed,
                AlgorithmName = CompressionAlgorithms.AdaptiveDeltaModulation,
                UsedSettings = settings
            };
        }

        private long WriteHeader(
            BinaryWriter writer,
            int sampleRate,
            int channels,
            int bitsPerSample,
            int initialStep,
            int minStep,
            int maxStep,
            double increaseFactor,
            double decreaseFactor)
        {
            writer.Write("ADM1");
            writer.Write(sampleRate);
            writer.Write(channels);
            writer.Write(bitsPerSample);
            writer.Write(initialStep);
            writer.Write(minStep);
            writer.Write(maxStep);
            writer.Write(increaseFactor);
            writer.Write(decreaseFactor);

            long totalSamplesPosition = writer.BaseStream.Position;

            writer.Write((long)0);

            return totalSamplesPosition;
        }

        private string GenerateCompressedFilePath(string inputPath, string algorithmName)
        {
            string directory = Path.GetDirectoryName(inputPath);
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(inputPath);

            string algorithmSuffix = algorithmName
                .Replace(" ", "_")
                .Replace("-", "_");

            string outputFileName = fileNameWithoutExtension
                + "_compressed_"
                + algorithmSuffix
                + ".adm";

            return Path.Combine(directory, outputFileName);
        }

        private short FloatToPcm16(float sample)
        {
            if (sample > 1.0f)
                sample = 1.0f;

            if (sample < -1.0f)
                sample = -1.0f;

            return (short)(sample * short.MaxValue);
        }

        private void PackBit(ref byte currentByte, ref int bitPosition, int bit, BinaryWriter writer)
        {
            if (bit == 1)
            {
                currentByte |= (byte)(1 << bitPosition);
            }

            bitPosition++;

            if (bitPosition == 8)
            {
                writer.Write(currentByte);
                currentByte = 0;
                bitPosition = 0;
            }
        }

        private int Clamp(int value, int min, int max)
        {
            if (value < min)
                return min;

            if (value > max)
                return max;

            return value;
        }

        /////////////////////////////////////////////////////FARAH RAM/////////////////////////////////////////////////////////

        public CompressionResult CompressUsingDeltaModulation(string inputPath, CompressionSettings settings)
        {
            Stopwatch sw = Stopwatch.StartNew();

            short[] samples = ReadSamples(inputPath);

            if (samples == null || samples.Length == 0)
            {
                return new CompressionResult
                {
                    StatusMessage = "File is empty",
                    AlgorithmName = "Delta Modulation"
                };
            }

            List<bool> bits = new List<bool>();

            short predicted = samples[0];
            int step = settings.DeltaStep;

            for (int i = 1; i < samples.Length; i++)
            {
                if (samples[i] >= predicted)
                {
                    bits.Add(true);
                    predicted += (short)step;
                }
                else
                {
                    bits.Add(false);
                    predicted -= (short)step;
                }
            }

            sw.Stop();

            string outputPath = Path.ChangeExtension(inputPath, ".dm");

            using (BinaryWriter writer = new BinaryWriter(File.Open(outputPath, FileMode.Create)))
            {
                writer.Write(samples[0]);          // First sample
                writer.Write(step);               // Delta step
                writer.Write(bits.Count);         // Number of bits

                foreach (bool bit in bits)
                {
                    writer.Write(bit);            // 1 bit (bool)
                }
            }


            long originalSize = new FileInfo(inputPath).Length;
            long compressedSize = new FileInfo(outputPath).Length;

            double ratio = (double)compressedSize / originalSize * 100;

            return new CompressionResult
            {
                CompressedFilePath = outputPath,
                OriginalFileSize = originalSize,
                CompressedFileSize = compressedSize,
                CompressionRatio = ratio,
                CompressionTime = sw.Elapsed,
                AlgorithmName = "Delta Modulation",
                UsedSettings = settings,
                TotalSamples = samples.Length,
                TotalBits = bits.Count,
                StatusMessage = "Compression completed successfully"
            };
        }
        



        private short[] ReadSamples(string filePath)
            {
                List<short> samples = new List<short>();

                using (var reader = new AudioFileReader(filePath))
                {
                    float[] buffer = new float[1024];
                    int samplesRead;

                    int channels = reader.WaveFormat.Channels;

                    while ((samplesRead = reader.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        for (int i = 0; i < samplesRead; i += channels)
                        {
                            float sampleF = buffer[i]; 

                            short sample = (short)(sampleF * 32767f);

                            samples.Add(sample);
                        }
                    }
                }

                return samples.ToArray();
            }



        public string DecompressAndSaveWav(string dmFilePath)
        {
            List<short> samples = new List<short>();

            using (BinaryReader reader = new BinaryReader(File.Open(dmFilePath, FileMode.Open)))
            {
                short predicted = reader.ReadInt16();
                int step = reader.ReadInt32();
                int bitCount = reader.ReadInt32();

                samples.Add(predicted);

                for (int i = 0; i < bitCount; i++)
                {
                    bool bit = reader.ReadBoolean();

                    if (bit)
                        predicted += (short)step;
                    else
                        predicted -= (short)step;

                    samples.Add(predicted);
                }
            }

            string directory = Path.GetDirectoryName(dmFilePath);
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(dmFilePath);

            string outputWavPath = Path.Combine(
                directory,
                fileNameWithoutExt + "_new.wav"
            );

            WaveFormat format = new WaveFormat(44100, 16, 1);

            using (WaveFileWriter writer = new WaveFileWriter(outputWavPath, format))
            {
                foreach (short sample in samples)
                {
                    writer.WriteSample(sample / 32768f);
                }
            }

            return outputWavPath;
        }





    }


}
