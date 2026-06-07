using System;
using System.Collections.Generic;
using System.IO;
using NAudio.Wave;
using CompressAudioFiles.Models;

namespace CompressAudioFiles.Services
{
    class AudioDecompressionService
    {
        
        //التابع العام
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

                case CompressionAlgorithms.DeltaModulation:
                    return DecompressDeltaModulation(compressedFilePath);
                case CompressionAlgorithms.PredictiveDifferentialCoding:
                    return DecompressPredictiveDifferentialCoding(compressedFilePath);
                case CompressionAlgorithms.NonlinearQuantization:
                    return DecompressNonlinearQuantization(compressedFilePath);
                case CompressionAlgorithms.DPCM:
                    return DecompressDPCM(compressedFilePath);

                default:
                    throw new NotSupportedException("Unknown decompression algorithm.");
            }

        }
        public string DecompressNonlinearQuantization(string compressedFilePath)
        {
            string outputPath = AudioCodecHelper.GenerateOutputPath(
                compressedFilePath,
                "_decompressed",
                ".wav"
            );

            using (var reader = new BinaryReader(File.OpenRead(compressedFilePath)))
            {
               
                NqHeader header = AudioCodecHelper.ReadNqHeader(reader);
                WaveFormat outputFormat = new WaveFormat(
                    header.SampleRate,
                    16,
                    header.Channels
                );

                using (var waveWriter = new WaveFileWriter(outputPath, outputFormat))
                {
                    for (long i = 0; i < header.TotalSamples; i++)
                    {
                        byte quantized = reader.ReadByte();
                        double y = (quantized / (double)(header.Levels - 1)) * 2.0 - 1.0;
                        double x = Math.Sign(y)
                                 * (Math.Pow(1.0 + header.MuValue, Math.Abs(y)) - 1.0)
                                 / header.MuValue;
                        short sample = (short)(
                            AudioCodecHelper.Clamp(
                                (int)(x * 32767),
                                short.MinValue,
                                short.MaxValue
                            )
                        );
                        AudioCodecHelper.WritePcm16Sample(waveWriter, sample);
                    }
                }
            }

            return outputPath;
        }
        public string DecompressDPCM(string compressedFilePath)
        {
            string outputPath = AudioCodecHelper.GenerateOutputPath(
                compressedFilePath,
                "_decompressed_DPCM",
                ".wav"
            );

            using (BinaryReader reader = new BinaryReader(File.OpenRead(compressedFilePath)))
            {
                DpcmHeader header = AudioCodecHelper.ReadDpcmHeader(reader);

                WaveFormat format = new WaveFormat(
                    header.SampleRate,
                    16,
                    header.Channels
                );

                using (WaveFileWriter writer = new WaveFileWriter(outputPath, format))
                {
                    int previousSample = header.FirstSample;

                    AudioCodecHelper.WritePcm16Sample(
                        writer,
                        header.FirstSample
                    );

                    for (long i = 1; i < header.TotalSamples; i++)
                    {
                        sbyte quantizedError = reader.ReadSByte();

                        int reconstructed =
                            previousSample +
                            (quantizedError * header.QuantizationStep);

                        reconstructed = AudioCodecHelper.Clamp(
                            reconstructed,
                            short.MinValue,
                            short.MaxValue
                        );

                        AudioCodecHelper.WritePcm16Sample(
                            writer,
                            (short)reconstructed
                        );

                        previousSample = reconstructed;
                    }
                }
            }

            return outputPath;
        }
        public string DecompressPredictiveDifferentialCoding(string compressedFilePath)
        {
            string outputPath = AudioCodecHelper.GenerateOutputPath(
                compressedFilePath,
                "_decompressed_PDC",
                ".wav"
            );
            using (BinaryReader reader = new BinaryReader(File.OpenRead(compressedFilePath)))
            {
                PdcHeader header = AudioCodecHelper.ReadPdcHeader(reader);
                WaveFormat format = new WaveFormat(
                    header.SampleRate,
                    16,
                    header.Channels
                );

                using (WaveFileWriter writer = new WaveFileWriter(outputPath, format))
                {
                    int previous2 = header.FirstSample;
                    int previous1 = header.SecondSample;

                    AudioCodecHelper.WritePcm16Sample(writer, header.FirstSample);
                    AudioCodecHelper.WritePcm16Sample(writer, header.SecondSample);

                    for (long i = 2; i < header.TotalSamples; i++)
                    {
                        sbyte quantizedError = reader.ReadSByte();

                        int predicted = (2 * previous1) - previous2;
                        predicted = AudioCodecHelper.Clamp(predicted, short.MinValue, short.MaxValue);

                        int reconstructed = predicted + (quantizedError * header.QuantizationStep);
                        reconstructed = AudioCodecHelper.Clamp(reconstructed, short.MinValue, short.MaxValue);

                        AudioCodecHelper.WritePcm16Sample(writer, (short)reconstructed);

                        previous2 = previous1;
                        previous1 = reconstructed;
                    }
                }
            }

            return outputPath;
        }
        public string DecompressDeltaModulation(string compressedFilePath)
        {
            List<short> samples = new List<short>();

            using (BinaryReader reader = new BinaryReader(File.Open(compressedFilePath, FileMode.Open)))
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

            string outputWavPath = AudioCodecHelper.GenerateOutputPath(
                compressedFilePath,
                "_Decom_Delta",
                ".wav"
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
        public string DecompressAdaptiveDeltaModulation(string compressedFilePath)
        {
            string outputPath = AudioCodecHelper.GenerateOutputPath(
                compressedFilePath,
                "_decompressed",
                ".wav"
            );
            using (var reader = new BinaryReader(File.OpenRead(compressedFilePath)))
            {
                AdmHeader header = AudioCodecHelper.ReadAdmHeader(reader);
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
                        int bit = AudioCodecHelper.ReadBit(reader, ref currentByte, ref bitsRemaining);
                        if (bit == 1)
                        {
                            predictedSample += stepSize;
                        }
                        else
                        {
                            predictedSample -= stepSize;
                        }

                        predictedSample = AudioCodecHelper.Clamp(
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

                        stepSize = AudioCodecHelper.Clamp(stepSize, header.MinStep, header.MaxStep);

                        AudioCodecHelper.WritePcm16Sample(waveWriter, (short)predictedSample);

                        previousBit = bit;
                    }
                }
            }

            return outputPath;
        }
    }
}