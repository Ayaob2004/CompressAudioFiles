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
    class AudioDecompressionService
    {
        public string DecompressAudio(string compressedFilePath, string algorithmName)
        {
            if (string.IsNullOrWhiteSpace(compressedFilePath))
                throw new ArgumentException("Compressed file path is empty.");

            if (!File.Exists(compressedFilePath))
                throw new FileNotFoundException("Compressed file not found.", compressedFilePath);

            if (string.IsNullOrWhiteSpace(algorithmName))
                throw new ArgumentException("Algorithm name is required.");

            switch (algorithmName)
            {
                case CompressionAlgorithms.AdaptiveDeltaModulation:
                    return DecompressAdaptiveDeltaModulation(compressedFilePath);

                case CompressionAlgorithms.NonlinearQuantization:
                case CompressionAlgorithms.DPCM:
                case CompressionAlgorithms.PredictiveDifferentialCoding:
                case CompressionAlgorithms.DeltaModulation:
                    throw new NotSupportedException(
                        "This decompression algorithm is listed in the project plan, but it is not implemented in this section yet."
                    );

                default:
                    throw new NotSupportedException("Unknown decompression algorithm.");
            }
        }

        public string DecompressAdaptiveDeltaModulation(string compressedFilePath)
        {
            string outputPath = GenerateDecompressedFilePath(compressedFilePath);

            using (var reader = new BinaryReader(File.OpenRead(compressedFilePath)))
            {
                AdmHeader header = ReadHeader(reader);

                WaveFormat outputFormat = new WaveFormat(
                    header.SampleRate,
                    16,
                    header.Channels
                );

                using (var waveWriter = new WaveFileWriter(outputPath, outputFormat))
                {
                    int predictedSample = 0;
                    int stepSize = header.InitialStep;
                    int previousBit = -1;

                    int currentByte = 0;
                    int bitsRemaining = 0;

                    for (long i = 0; i < header.TotalSamples; i++)
                    {
                        int bit = ReadBit(reader, ref currentByte, ref bitsRemaining);

                        if (bit == 1)
                        {
                            predictedSample += stepSize;
                        }
                        else
                        {
                            predictedSample -= stepSize;
                        }

                        predictedSample = Clamp(
                            predictedSample,
                            short.MinValue,
                            short.MaxValue
                        );

                        if (previousBit == bit)
                        {
                            stepSize = (int)(stepSize * header.IncreaseFactor);
                        }
                        else
                        {
                            stepSize = (int)(stepSize * header.DecreaseFactor);
                        }

                        stepSize = Clamp(stepSize, header.MinStep, header.MaxStep);

                        WritePcm16Sample(waveWriter, (short)predictedSample);

                        previousBit = bit;
                    }
                }
            }

            return outputPath;
        }

        private AdmHeader ReadHeader(BinaryReader reader)
        {
            string magic = reader.ReadString();

            if (magic != "ADM1")
                throw new InvalidDataException("Invalid ADM compressed file.");

            AdmHeader header = new AdmHeader();

            header.SampleRate = reader.ReadInt32();
            header.Channels = reader.ReadInt32();
            header.BitsPerSample = reader.ReadInt32();
            header.InitialStep = reader.ReadInt32();
            header.MinStep = reader.ReadInt32();
            header.MaxStep = reader.ReadInt32();
            header.IncreaseFactor = reader.ReadDouble();
            header.DecreaseFactor = reader.ReadDouble();
            header.TotalSamples = reader.ReadInt64();

            if (header.SampleRate <= 0)
                throw new InvalidDataException("Invalid sample rate in ADM file.");

            if (header.Channels <= 0)
                throw new InvalidDataException("Invalid channels count in ADM file.");

            if (header.TotalSamples <= 0)
                throw new InvalidDataException("Invalid samples count in ADM file.");

            return header;
        }

        private int ReadBit(BinaryReader reader, ref int currentByte, ref int bitsRemaining)
        {
            if (bitsRemaining == 0)
            {
                currentByte = reader.ReadByte();
                bitsRemaining = 8;
            }

            int bit = currentByte & 1;

            currentByte >>= 1;
            bitsRemaining--;

            return bit;
        }

        private void WritePcm16Sample(WaveFileWriter writer, short sample)
        {
            byte[] bytes = BitConverter.GetBytes(sample);
            writer.Write(bytes, 0, bytes.Length);
        }

        private string GenerateDecompressedFilePath(string compressedFilePath)
        {
            string directory = Path.GetDirectoryName(compressedFilePath);
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(compressedFilePath);

            string outputFileName = fileNameWithoutExtension + "_decompressed.wav";

            return Path.Combine(directory, outputFileName);
        }

        private int Clamp(int value, int min, int max)
        {
            if (value < min)
                return min;

            if (value > max)
                return max;

            return value;
        }

        private class AdmHeader
        {
            public int SampleRate { get; set; }
            public int Channels { get; set; }
            public int BitsPerSample { get; set; }
            public int InitialStep { get; set; }
            public int MinStep { get; set; }
            public int MaxStep { get; set; }
            public double IncreaseFactor { get; set; }
            public double DecreaseFactor { get; set; }
            public long TotalSamples { get; set; }
        }
    }
}
