using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NAudio.Wave;

namespace CompressAudioFiles.Services
{
    class AudioDecompressionService
    {
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

            string directory = Path.GetDirectoryName(compressedFilePath);
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(compressedFilePath);

            string outputWavPath = Path.Combine(
                directory,
                fileNameWithoutExt + "_Decom_Delta.wav"
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
