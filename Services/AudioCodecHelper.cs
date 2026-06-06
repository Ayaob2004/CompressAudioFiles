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
    internal static class AudioCodecHelper
    {
        public static int Clamp(int value, int min, int max)
        {
            if (value < min)
                return min;

            if (value > max)
                return max;

            return value;
        }

        public static short FloatToPcm16(float sample)
        {
            if (sample > 1.0f)
                sample = 1.0f;

            if (sample < -1.0f)
                sample = -1.0f;

            return (short)(sample * 32767f);
        }

        public static AudioSamplesData ReadMonoSamples(string filePath)
        {
            List<short> samples = new List<short>();
            int sampleRate;

            using (var reader = new AudioFileReader(filePath))
            {
                sampleRate = reader.WaveFormat.SampleRate;
                int channels = reader.WaveFormat.Channels;

                float[] buffer = new float[4096];
                int samplesRead;

                while ((samplesRead = reader.Read(buffer, 0, buffer.Length)) > 0)
                {
                    for (int i = 0; i < samplesRead; i += channels)
                    {
                        short sample = FloatToPcm16(buffer[i]);
                        samples.Add(sample);
                    }
                }
            }

            return new AudioSamplesData
            {
                Samples = samples.ToArray(),
                SampleRate = sampleRate
            };
        }

        public static string GenerateCompressedFilePath(string inputPath, string algorithmName, string extension)
        {
            string directory = Path.GetDirectoryName(inputPath);
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(inputPath);

            string algorithmSuffix = algorithmName
                .Replace(" ", "_")
                .Replace("-", "_");

            string outputFileName = fileNameWithoutExtension
                + "_compressed_"
                + algorithmSuffix
                + extension;

            return Path.Combine(directory, outputFileName);
        }

        public static string GenerateOutputPath(string inputPath, string suffix, string extension)
        {
            string directory = Path.GetDirectoryName(inputPath);
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(inputPath);

            string outputFileName = fileNameWithoutExtension + suffix + extension;

            return Path.Combine(directory, outputFileName);
        }

        public static long WriteAdmHeader(
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

        public static AdmHeader ReadAdmHeader(BinaryReader reader)
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

        public static void WritePdcHeader(
            BinaryWriter writer,
            int sampleRate,
            int channels,
            int bitsPerSample,
            int quantizationStep,
            long totalSamples,
            short firstSample,
            short secondSample)
        {
            writer.Write("PDC1");
            writer.Write(sampleRate);
            writer.Write(channels);
            writer.Write(bitsPerSample);
            writer.Write(quantizationStep);
            writer.Write(totalSamples);
            writer.Write(firstSample);
            writer.Write(secondSample);
        }

        public static PdcHeader ReadPdcHeader(BinaryReader reader)
        {
            string magic = reader.ReadString();

            if (magic != "PDC1")
                throw new InvalidDataException("Invalid PDC compressed file.");

            PdcHeader header = new PdcHeader();

            header.SampleRate = reader.ReadInt32();
            header.Channels = reader.ReadInt32();
            header.BitsPerSample = reader.ReadInt32();
            header.QuantizationStep = reader.ReadInt32();
            header.TotalSamples = reader.ReadInt64();
            header.FirstSample = reader.ReadInt16();
            header.SecondSample = reader.ReadInt16();

            if (header.SampleRate <= 0)
                throw new InvalidDataException("Invalid sample rate in PDC file.");

            if (header.Channels <= 0)
                throw new InvalidDataException("Invalid channels count in PDC file.");

            if (header.QuantizationStep <= 0)
                throw new InvalidDataException("Invalid quantization step in PDC file.");

            if (header.TotalSamples < 2)
                throw new InvalidDataException("Invalid samples count in PDC file.");

            return header;
        }
        ///////////////////////////////////////////////////////////rrr
        public static long WriteNqHeader(BinaryWriter writer,int sampleRate,int channels,int bitsPerSample,int muValue,int levels)
        {
            writer.Write("NQ1");        
            writer.Write(sampleRate);
            writer.Write(channels);
            writer.Write(bitsPerSample);
            writer.Write(muValue);
            writer.Write(levels);
            long totalSamplesPosition = writer.BaseStream.Position;
            writer.Write((long)0);
            return totalSamplesPosition;
        }
        public static NqHeader ReadNqHeader(BinaryReader reader)
        {
            string magic = reader.ReadString();
            if (magic != "NQ1")
                throw new InvalidDataException("Invalid NQ compressed file.");

            var header = new NqHeader
            {
                SampleRate = reader.ReadInt32(),
                Channels = reader.ReadInt32(),
                BitsPerSample = reader.ReadInt32(),
                MuValue = reader.ReadInt32(),
                Levels = reader.ReadInt32(),
                TotalSamples = reader.ReadInt64()
            };

            if (header.SampleRate <= 0) throw new InvalidDataException("Invalid sample rate.");
            if (header.Channels <= 0) throw new InvalidDataException("Invalid channels.");
            if (header.TotalSamples <= 0) throw new InvalidDataException("Invalid samples count.");

            return header;
        }
        /////////////////////////////////////////////////////////

        public static void PackBit(ref byte currentByte, ref int bitPosition, int bit, BinaryWriter writer)
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

        public static int ReadBit(BinaryReader reader, ref int currentByte, ref int bitsRemaining)
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

        public static void WritePcm16Sample(WaveFileWriter writer, short sample)
        {
            byte[] bytes = BitConverter.GetBytes(sample);
            writer.Write(bytes, 0, bytes.Length);
        }
    }

    internal class AudioSamplesData
    {
        public short[] Samples { get; set; }
        public int SampleRate { get; set; }
    }

    internal class AdmHeader
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

    internal class PdcHeader
    {
        public int SampleRate { get; set; }
        public int Channels { get; set; }
        public int BitsPerSample { get; set; }
        public int QuantizationStep { get; set; }
        public long TotalSamples { get; set; }
        public short FirstSample { get; set; }
        public short SecondSample { get; set; }
    }


    internal class NqHeader
    {
        public int SampleRate { get; set; }
        public int Channels { get; set; }
        public int BitsPerSample { get; set; }
        public int MuValue { get; set; }  
        public int Levels { get; set; }  
        public long TotalSamples { get; set; }
    }
}