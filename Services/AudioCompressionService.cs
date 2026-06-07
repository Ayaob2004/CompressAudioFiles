using System;
using System.Diagnostics;
using System.IO;
using NAudio.Wave;
using CompressAudioFiles.Models;


namespace CompressAudioFiles.Services
{
    class AudioCompressionService
    {
        public event EventHandler<OperationProgressEventArgs> ProgressChanged;

        private void ReportProgress(
            string algorithmName,
            string operationName,
            long processedSamples,
            long totalSamples,
            long originalSize,
            long currentOutputSize,
            Stopwatch stopwatch)
        {
            if (processedSamples <= 0 || totalSamples <= 0)
                return;

            double progressPercentage = processedSamples * 100.0 / totalSamples;

            double processedOriginalSize =
                originalSize * (processedSamples / (double)totalSamples);

            double compressionRatio = 0;

            if (currentOutputSize > 0 && processedOriginalSize > 0)
            {
                compressionRatio = processedOriginalSize / currentOutputSize;
            }

            double processingSpeed = stopwatch.Elapsed.TotalSeconds <= 0
                ? 0
                : processedSamples / stopwatch.Elapsed.TotalSeconds;

            ProgressChanged?.Invoke(this, new OperationProgressEventArgs
            {
                AlgorithmName = algorithmName,
                OperationName = operationName,

                ProcessedSamples = processedSamples,
                TotalSamples = totalSamples,

                ProgressPercentage = progressPercentage,

                OriginalSizeBytes = originalSize,
                CurrentOutputSizeBytes = currentOutputSize,

                CompressionRatio = compressionRatio,
                ProcessingSpeed = processingSpeed,

                ElapsedTime = stopwatch.Elapsed
            });
        }
        // التابع العام
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
                case CompressionAlgorithms.PredictiveDifferentialCoding:
                    return CompressUsingPredictiveDifferentialCoding(inputPath, settings);
                case CompressionAlgorithms.NonlinearQuantization:
                    return CompressUsingNonlinearQuantization(inputPath, settings);
                case CompressionAlgorithms.DPCM:
                    return CompressUsingDPCM(inputPath, settings);

                default:
                    throw new NotSupportedException("Unknown compression algorithm.");
            }
        }

        public CompressionResult CompressUsingNonlinearQuantization(string inputPath, CompressionSettings settings)
        {
            if (string.IsNullOrWhiteSpace(inputPath))
                throw new ArgumentException("Input audio path is empty.");

            if (!File.Exists(inputPath))
                throw new FileNotFoundException("Input audio file not found.", inputPath);

            Stopwatch sw = Stopwatch.StartNew();

            long originalSize = new FileInfo(inputPath).Length;

            const int muValue = 255;
            int levels = settings.QuantizationLevels;

            if (levels < 2)
                levels = 256;

            string outputPath = AudioCodecHelper.GenerateCompressedFilePath(
                inputPath,
                CompressionAlgorithms.NonlinearQuantization,
                ".nq"
            );

            long totalSamplesWritten = 0;

            using (var reader = new AudioFileReader(inputPath))
            using (var writer = new BinaryWriter(File.Create(outputPath)))
            {
                long totalSamplesPosition = AudioCodecHelper.WriteNqHeader(
                    writer,
                    reader.WaveFormat.SampleRate,
                    reader.WaveFormat.Channels,
                    reader.WaveFormat.BitsPerSample,
                    muValue,
                    levels
                );

                float[] buffer = new float[4096];
                int samplesRead;

                long totalSamples =
                    reader.Length / (reader.WaveFormat.BitsPerSample / 8);

                TimeSpan reportInterval = TimeSpan.FromMilliseconds(100);
                TimeSpan lastReportTime = TimeSpan.Zero;

                while ((samplesRead = reader.Read(buffer, 0, buffer.Length)) > 0)
                {
                    for (int i = 0; i < samplesRead; i++)
                    {
                        double x = AudioCodecHelper.Clamp(
                            (int)(buffer[i] * 32767),
                            short.MinValue,
                            short.MaxValue
                        ) / 32767.0;

                        double y = Math.Sign(x)
                                 * Math.Log(1.0 + muValue * Math.Abs(x))
                                 / Math.Log(1.0 + muValue);

                        int quantized = (int)((y + 1.0) / 2.0 * (levels - 1));
                        quantized = AudioCodecHelper.Clamp(quantized, 0, levels - 1);

                        writer.Write((byte)quantized);

                        totalSamplesWritten++;

                        if (sw.Elapsed - lastReportTime >= reportInterval)
                        {
                            lastReportTime = sw.Elapsed;

                            ReportProgress(
                                CompressionAlgorithms.NonlinearQuantization,
                                "Compression",
                                totalSamplesWritten,
                                totalSamples,
                                originalSize,
                                writer.BaseStream.Position,
                                sw
                            );
                        }
                    }
                }

                ReportProgress(
                    CompressionAlgorithms.NonlinearQuantization,
                    "Compression",
                    totalSamplesWritten,
                    totalSamples,
                    originalSize,
                    writer.BaseStream.Position,
                    sw
                );

                writer.BaseStream.Seek(totalSamplesPosition, SeekOrigin.Begin);
                writer.Write(totalSamplesWritten);
            }

            sw.Stop();

            long compressedSize = new FileInfo(outputPath).Length;

            return new CompressionResult
            {
                CompressedFilePath = outputPath,
                OriginalFileSize = originalSize,
                CompressedFileSize = compressedSize,
                CompressionRatio = compressedSize == 0
                    ? 0
                    : (double)originalSize / compressedSize,
                CompressionTime = sw.Elapsed,
                AlgorithmName = CompressionAlgorithms.NonlinearQuantization,
                UsedSettings = settings,
                TotalSamples = (int)totalSamplesWritten,
                StatusMessage = "Nonlinear Quantization compression completed successfully."
            };
        }
        public CompressionResult CompressUsingDPCM(string inputPath, CompressionSettings settings)
        {
            if (string.IsNullOrWhiteSpace(inputPath))
                throw new ArgumentException("Input audio path is empty.");

            if (!File.Exists(inputPath))
                throw new FileNotFoundException("Input audio file not found.", inputPath);

            Stopwatch sw = Stopwatch.StartNew();

            long originalSize = new FileInfo(inputPath).Length;

            AudioSamplesData audioData = AudioCodecHelper.ReadMonoSamples(inputPath);
            short[] samples = audioData.Samples;

            if (samples == null || samples.Length < 2)
            {
                return new CompressionResult
                {
                    CompressedFilePath = null,
                    OriginalFileSize = originalSize,
                    CompressedFileSize = 0,
                    CompressionRatio = 0,
                    CompressionTime = sw.Elapsed,
                    AlgorithmName = CompressionAlgorithms.DPCM,
                    UsedSettings = settings,
                    StatusMessage = "Audio file does not contain enough samples."
                };
            }

            int quantizationStep = settings.PredictiveQuantizationStep;

            if (quantizationStep <= 0)
                quantizationStep = 256;

            string outputPath = Path.ChangeExtension(inputPath, ".dpcm");

            using (BinaryWriter writer = new BinaryWriter(File.Open(outputPath, FileMode.Create)))
            {
                AudioCodecHelper.WriteDpcmHeader(
                    writer,
                    audioData.SampleRate,
                    1,
                    16,
                    quantizationStep,
                    samples.Length,
                    samples[0]
                );

                int previousSample = samples[0];

                TimeSpan reportInterval = TimeSpan.FromMilliseconds(100);
                TimeSpan lastReportTime = TimeSpan.Zero;

                for (int i = 1; i < samples.Length; i++)
                {
                    int predicted = previousSample;

                    int error = samples[i] - predicted;

                    int levels = settings.QuantizationLevels;

                    if (levels < 2)
                        levels = 256;

                    int halfLevels = levels / 2;

                    int minQuantizedValue = -halfLevels;
                    int maxQuantizedValue = halfLevels - 1;

                    int quantizedErrorInt =
                        (int)Math.Round(error / (double)quantizationStep);

                    quantizedErrorInt = AudioCodecHelper.Clamp(
                        quantizedErrorInt,
                        minQuantizedValue,
                        maxQuantizedValue
                    );

                    sbyte quantizedError = (sbyte)quantizedErrorInt;

                    writer.Write(quantizedError);

                    int reconstructed =
                        predicted + (quantizedError * quantizationStep);

                    reconstructed = AudioCodecHelper.Clamp(
                        reconstructed,
                        short.MinValue,
                        short.MaxValue
                    );

                    previousSample = reconstructed;

                    if (sw.Elapsed - lastReportTime >= reportInterval || i == samples.Length - 1)
                    {
                        lastReportTime = sw.Elapsed;

                        ReportProgress(
                            CompressionAlgorithms.DPCM,
                            "Compression",
                            i,
                            samples.Length,
                            originalSize,
                            writer.BaseStream.Position,
                            sw
                        );
                    }
                }
            }

            sw.Stop();

            long compressedSize = new FileInfo(outputPath).Length;

            return new CompressionResult
            {
                CompressedFilePath = outputPath,
                OriginalFileSize = originalSize,
                CompressedFileSize = compressedSize,
                CompressionRatio = compressedSize == 0
                    ? 0
                    : (double)originalSize / compressedSize,
                CompressionTime = sw.Elapsed,
                AlgorithmName = CompressionAlgorithms.DPCM,
                UsedSettings = settings,
                TotalSamples = samples.Length,
                TotalBits = (samples.Length - 1) * 8,
                StatusMessage = "DPCM compression completed successfully."
            };
        }
        public CompressionResult CompressUsingPredictiveDifferentialCoding(string inputPath, CompressionSettings settings)
        {
            if (string.IsNullOrWhiteSpace(inputPath))
                throw new ArgumentException("Input audio path is empty.");

            if (!File.Exists(inputPath))
                throw new FileNotFoundException("Input audio file not found.", inputPath);

            Stopwatch sw = Stopwatch.StartNew();

            long originalSize = new FileInfo(inputPath).Length;

            AudioSamplesData audioData = AudioCodecHelper.ReadMonoSamples(inputPath);
            short[] samples = audioData.Samples;

            if (samples == null || samples.Length < 2)
            {
                return new CompressionResult
                {
                    CompressedFilePath = null,
                    OriginalFileSize = originalSize,
                    CompressedFileSize = 0,
                    CompressionRatio = 0,
                    CompressionTime = sw.Elapsed,
                    AlgorithmName = CompressionAlgorithms.PredictiveDifferentialCoding,
                    UsedSettings = settings,
                    StatusMessage = "Audio file does not contain enough samples."
                };
            }

            int quantizationStep = settings.PredictiveQuantizationStep;

            if (quantizationStep <= 0)
                quantizationStep = 256;

            string outputPath = Path.ChangeExtension(inputPath, ".pdc");

            using (BinaryWriter writer = new BinaryWriter(File.Open(outputPath, FileMode.Create)))
            {
                AudioCodecHelper.WritePdcHeader(
                    writer,
                    audioData.SampleRate,
                    1,
                    16,
                    quantizationStep,
                    samples.Length,
                    samples[0],
                    samples[1]
                );

                int previous2 = samples[0];
                int previous1 = samples[1];

                TimeSpan reportInterval = TimeSpan.FromMilliseconds(100);
                TimeSpan lastReportTime = TimeSpan.Zero;

                for (int i = 2; i < samples.Length; i++)
                {
                    int predicted = (2 * previous1) - previous2;
                    predicted = AudioCodecHelper.Clamp(predicted, short.MinValue, short.MaxValue);

                    int error = samples[i] - predicted;

                    int levels = settings.QuantizationLevels;

                    if (levels < 2)
                        levels = 256;

                    int halfLevels = levels / 2;

                    int minQuantizedValue = -halfLevels;
                    int maxQuantizedValue = halfLevels - 1;

                    int quantizedErrorInt = (int)Math.Round(error / (double)quantizationStep);

                    quantizedErrorInt = AudioCodecHelper.Clamp(
                        quantizedErrorInt,
                        minQuantizedValue,
                        maxQuantizedValue
                    );

                    sbyte quantizedError = (sbyte)quantizedErrorInt;

                    writer.Write(quantizedError);

                    int reconstructed = predicted + (quantizedError * quantizationStep);
                    reconstructed = AudioCodecHelper.Clamp(reconstructed, short.MinValue, short.MaxValue);

                    previous2 = previous1;
                    previous1 = reconstructed;


                    if (sw.Elapsed - lastReportTime >= reportInterval || i == samples.Length - 1)
                    {
                        lastReportTime = sw.Elapsed;

                        ReportProgress(
                            CompressionAlgorithms.PredictiveDifferentialCoding,
                            "Compression",
                            i,
                            samples.Length,
                            originalSize,
                            writer.BaseStream.Position,
                            sw
                        );
                    }
                }
            }

            sw.Stop();

            long compressedSize = new FileInfo(outputPath).Length;

            return new CompressionResult
            {
                CompressedFilePath = outputPath,
                OriginalFileSize = originalSize,
                CompressedFileSize = compressedSize,
                CompressionRatio = compressedSize == 0 ? 0 : (double)originalSize / compressedSize,
                CompressionTime = sw.Elapsed,
                AlgorithmName = CompressionAlgorithms.PredictiveDifferentialCoding,
                UsedSettings = settings,
                TotalSamples = samples.Length,
                TotalBits = (samples.Length - 2) * 8,
                StatusMessage = "Predictive Differential Coding compression completed successfully."
            };
        }
        public CompressionResult CompressUsingDeltaModulation(string inputPath, CompressionSettings settings)
        {
            Stopwatch sw = Stopwatch.StartNew();

            long originalSize = new FileInfo(inputPath).Length;

            short[] samples = AudioCodecHelper.ReadMonoSamples(inputPath).Samples;

            if (samples == null || samples.Length == 0)
            {
                return new CompressionResult
                {
                    StatusMessage = "File is empty",
                    AlgorithmName = CompressionAlgorithms.DeltaModulation
                };
            }

            string outputPath = Path.ChangeExtension(inputPath, ".dm");

            int step = settings.DeltaStep;

            if (step <= 0)
                step = 1000;

            int bitsCount = 0;

            using (BinaryWriter writer = new BinaryWriter(File.Open(outputPath, FileMode.Create)))
            {
                writer.Write(samples[0]);      // First sample
                writer.Write(step);            // Delta step

                long bitsCountPosition = writer.BaseStream.Position;
                writer.Write(0);               // Placeholder for number of bits

                short predicted = samples[0];

                TimeSpan reportInterval = TimeSpan.FromMilliseconds(100);
                TimeSpan lastReportTime = TimeSpan.Zero;

                for (int i = 1; i < samples.Length; i++)
                {
                    bool bit;

                    if (samples[i] >= predicted)
                    {
                        bit = true;
                        predicted += (short)step;
                    }
                    else
                    {
                        bit = false;
                        predicted -= (short)step;
                    }

                    writer.Write(bit);
                    bitsCount++;

                    if (sw.Elapsed - lastReportTime >= reportInterval || i == samples.Length - 1)
                    {
                        lastReportTime = sw.Elapsed;

                        ReportProgress(
                            CompressionAlgorithms.DeltaModulation,
                            "Compression",
                            i,
                            samples.Length,
                            originalSize,
                            writer.BaseStream.Position,
                            sw
                        );
                    }
                }

                writer.BaseStream.Seek(bitsCountPosition, SeekOrigin.Begin);
                writer.Write(bitsCount);
            }

            sw.Stop();

            long compressedSize = new FileInfo(outputPath).Length;

            return new CompressionResult
            {
                CompressedFilePath = outputPath,
                OriginalFileSize = originalSize,
                CompressedFileSize = compressedSize,
                CompressionRatio = compressedSize == 0
                    ? 0
                    : (double)originalSize / compressedSize,
                CompressionTime = sw.Elapsed,
                AlgorithmName = CompressionAlgorithms.DeltaModulation,
                UsedSettings = settings,
                TotalSamples = samples.Length,
                TotalBits = bitsCount,
                StatusMessage = "Compression completed successfully"
            };
        }
        public CompressionResult CompressUsingAdaptiveDeltaModulation(string inputPath, CompressionSettings settings)
        {
            if (string.IsNullOrWhiteSpace(inputPath))
                throw new ArgumentException("Input audio path is empty.");

            if (!File.Exists(inputPath))
                throw new FileNotFoundException("Input audio file not found.", inputPath);

            Stopwatch stopwatch = Stopwatch.StartNew();

            long originalSize = new FileInfo(inputPath).Length;

            string outputPath = AudioCodecHelper.GenerateCompressedFilePath(
                inputPath,
                CompressionAlgorithms.AdaptiveDeltaModulation,
                ".adm"
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
                long totalSamplesPosition = AudioCodecHelper.WriteAdmHeader(
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

                long totalSamples = reader.Length / (reader.WaveFormat.BitsPerSample / 8);

                TimeSpan reportInterval = TimeSpan.FromMilliseconds(100);
                TimeSpan lastReportTime = TimeSpan.Zero;


                while ((samplesRead = reader.Read(buffer, 0, buffer.Length)) > 0)
                {
                    for (int i = 0; i < samplesRead; i++)
                    {
                        short currentSample = AudioCodecHelper.FloatToPcm16(buffer[i]);

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

                        predictedSample = AudioCodecHelper.Clamp(
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

                        stepSize = AudioCodecHelper.Clamp(stepSize, minStep, maxStep);

                        AudioCodecHelper.PackBit(ref currentByte, ref bitPosition, bit, writer);

                        previousBit = bit;
                        totalSamplesWritten++;

                        if (stopwatch.Elapsed - lastReportTime >= reportInterval)
                        {
                            lastReportTime = stopwatch.Elapsed;

                            ReportProgress(
                                CompressionAlgorithms.AdaptiveDeltaModulation,
                                "Compression",
                                totalSamplesWritten,
                                totalSamples,
                                originalSize,
                                writer.BaseStream.Position,
                                stopwatch
                            );
                        }
                    }
                }

                if (bitPosition > 0)
                {
                    writer.Write(currentByte);
                }

                ReportProgress(
                    CompressionAlgorithms.AdaptiveDeltaModulation,
                    "Compression",
                    totalSamplesWritten,
                    totalSamples,
                    originalSize,
                    writer.BaseStream.Position,
                    stopwatch
                );

                writer.BaseStream.Seek(totalSamplesPosition, SeekOrigin.Begin);
                writer.Write(totalSamplesWritten);
            }

            stopwatch.Stop();

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
    }
}