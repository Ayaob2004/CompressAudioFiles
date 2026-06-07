using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CompressAudioFiles.Models;
using CompressAudioFiles.Services;
using NAudio.Wave;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading.Tasks;
using System.Threading;

namespace CompressAudioFiles
{
    public partial class MainForm : Form
    {
        private bool isAudioLoaded;
        private string currentAudioPath;
        private string decompressedFilePath;
        private string FormatTime(TimeSpan t)=> $"{(int)t.TotalMinutes:D2}:{t.Seconds:D2}";
        private AudioMetadata currentAudioMetadata;
        private CompressionSettings currentCompressionSettings;
        private CompressionResult lastCompressionResult;
        private readonly AudioMetadataService audioMetadataService;
        private readonly AudioCompressionService audioCompressionService;
        private readonly AudioDecompressionService audioDecompressionService;
        private readonly CompressionMonitoringService monitoringService;
        private readonly AudioPlayerService player;
        private Panel _progressFill;
        private CancellationTokenSource compressionCancellationSource;

        public MainForm()
        {
            InitializeComponent();
            audioMetadataService = new AudioMetadataService();
            currentCompressionSettings = new CompressionSettings();
            audioCompressionService = new AudioCompressionService();
            audioDecompressionService = new AudioDecompressionService();
            monitoringService = new CompressionMonitoringService();

            audioCompressionService.ProgressChanged += monitoringService.HandleProgress;

            monitoringService.MonitoringUpdated += MonitoringService_MonitoringUpdated;
            SetupMonitoringCharts();

            InitializeCompressionAlgorithms();
            InitializeCompressionSettingsControls();
            UpdateCompressionSettingsVisibility();

            cmbCompressionAlgorithm.SelectedIndexChanged += (s, e) =>
            {
                UpdateCompressionSettingsVisibility();
            };

            this.AllowDrop = true;
            this.DragEnter += MainForm_DragEnter;
            this.DragDrop += MainForm_DragDrop;

            player = new AudioPlayerService();
            player.OnPositionChanged += Player_OnPositionChanged;
           SetupPlayerUI();
        }

        private void SetupMonitoringCharts()
        {
            chartCompressionRatio.Series.Clear();
            chartCompressionRatio.ChartAreas.Clear();

            ChartArea ratioArea = new ChartArea("RatioArea");
            chartCompressionRatio.ChartAreas.Add(ratioArea);

            Series ratioSeries = new Series("CompressionRatio");
            ratioSeries.ChartType = SeriesChartType.Line;
            ratioSeries.BorderWidth = 2;
            chartCompressionRatio.Series.Add(ratioSeries);

            chartCompressionRatio.Titles.Clear();
            chartCompressionRatio.Titles.Add("Compression Ratio During Execution");

            chartProcessingSpeed.Series.Clear();
            chartProcessingSpeed.ChartAreas.Clear();

            ChartArea speedArea = new ChartArea("SpeedArea");
            chartProcessingSpeed.ChartAreas.Add(speedArea);

            Series speedSeries = new Series("ProcessingSpeed");
            speedSeries.ChartType = SeriesChartType.Line;
            speedSeries.BorderWidth = 2;
            chartProcessingSpeed.Series.Add(speedSeries);

            chartProcessingSpeed.Titles.Clear();
            chartProcessingSpeed.Titles.Add("Processing Speed During Execution");
        }

        private void MonitoringService_MonitoringUpdated(object sender, OperationProgressEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => MonitoringService_MonitoringUpdated(sender, e)));
                return;
            }

            int progressValue = Math.Min(100, Math.Max(0, (int)e.ProgressPercentage));

            progressBarCompression.Value = progressValue;

            lblProgress.Text = $"{e.ProgressPercentage:F1}%";
            lblChartCompressionRatio.Text = $"Ratio: {e.CompressionRatio:F2}";
            lblChartProcessingSpeed.Text = $"Speed: {e.ProcessingSpeed:F0} samples/sec";

            chartCompressionRatio.Series["CompressionRatio"].Points.AddY(e.CompressionRatio);
            chartProcessingSpeed.Series["ProcessingSpeed"].Points.AddY(e.ProcessingSpeed);
        }

        private void LoadAudioFile(string filePath)
        {
            try
            {
                currentAudioPath = filePath;
                isAudioLoaded = true;

                currentAudioMetadata = audioMetadataService.ExtractAudioMetadata(filePath);

                DisplayAudioMetadata(currentAudioMetadata);

                lblFilePath.Text = currentAudioPath;
                bool loaded = player.LoadAudioForPreview(filePath);
                if (loaded)
                {
                    _progressFill.Width = 0;
                    lblCurrentTime.Text = "00:00";
                    lblTotalTime.Text = FormatTime(player.CurrentAudioDuration);
                    btnPlay.Text = "▶";
                }
            }
            
            catch (Exception ex)
            {
                isAudioLoaded = false;
                currentAudioPath = null;
                currentAudioMetadata = null;

                MessageBox.Show(
                    "Failed to load audio metadata.\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void DisplayAudioMetadata(AudioMetadata metadata)
        {
            lblFileSize.Text = "File Size: " + FormatFileSize(metadata.FileSize);
            lblDuration.Text = "Duration: " + metadata.Duration.ToString(@"hh\:mm\:ss");
            lblSampleRate.Text = "Sample Rate: " + metadata.SampleRate + " Hz";
            lblChannels.Text = "Channels: " + metadata.ChannelsCount;
            lblBitRate.Text = "Bit Rate: " + (metadata.BitRate / 1000) + " kbps";
            lblEncodingType.Text = "Encoding Type: " + metadata.EncodingType;
        }
        private void DisplayCompressionResult(CompressionResult result)
        {
            lblCompressedPath.Text = "Compressed File: " + result.CompressedFilePath;
            lblOriginalSize.Text = "Original Size: " + FormatFileSize(result.OriginalFileSize);
            lblCompressedSize.Text = "Compressed Size: " + FormatFileSize(result.CompressedFileSize);
            lblCompressionRatio.Text = "Compression Ratio: " + result.CompressionRatio.ToString("0.00");
            lblCompressionTime.Text = "Compression Time: " + result.CompressionTime.TotalSeconds.ToString("0.000") + " sec";
        }
        private string FormatFileSize(long bytes)
        {
            if (bytes < 1024)
                return bytes + " B";

            double kb = bytes / 1024.0;

            if (kb < 1024)
                return kb.ToString("0.00") + " KB";

            double mb = kb / 1024.0;
            return mb.ToString("0.00") + " MB";
        }
        private void btnChooseAudio_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Choose Audio File";
                openFileDialog.Filter = "Audio Files|*.wav;*.mp3;*.m4a;*.aiff;*.wma|All Files|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    LoadAudioFile(openFileDialog.FileName);
                }
            }
        }
        private void InitializeCompressionAlgorithms()
        {
            cmbCompressionAlgorithm.Items.Clear();

            cmbCompressionAlgorithm.Items.Add(CompressionAlgorithms.NonlinearQuantization);
            cmbCompressionAlgorithm.Items.Add(CompressionAlgorithms.DPCM);
            cmbCompressionAlgorithm.Items.Add(CompressionAlgorithms.PredictiveDifferentialCoding);
            cmbCompressionAlgorithm.Items.Add(CompressionAlgorithms.DeltaModulation);
            cmbCompressionAlgorithm.Items.Add(CompressionAlgorithms.AdaptiveDeltaModulation);
        }

        private async void btnCompress_Click(object sender, EventArgs e)
        {
            if (!isAudioLoaded || string.IsNullOrWhiteSpace(currentAudioPath))
            {
                MessageBox.Show(
                    "Please choose an audio file first.",
                    "No Audio File",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            if (cmbCompressionAlgorithm.SelectedItem == null)
            {
                MessageBox.Show(
                    "Please choose a compression algorithm.",
                    "Missing Algorithm",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            try
            {
                btnCompress.Enabled = false;
                btnCompress.Text = "Compressing...";
                lblProgress.Text = "0%";
                lblChartCompressionRatio.Text = "Ratio: 0";
                lblChartProcessingSpeed.Text = "Speed: 0 samples/sec";
                progressBarCompression.Value = 0;
                chartCompressionRatio.Series["CompressionRatio"].Points.Clear();
                chartProcessingSpeed.Series["ProcessingSpeed"].Points.Clear();
                monitoringService.Reset();

                if (!TryReadCompressionSettingsFromUI())
                    return;

                string pathForCompression = ConvertToWav(currentAudioPath);
                pathForCompression = ConvertSampleRateIfNeeded(
                    pathForCompression,
                    currentCompressionSettings.SampleRate
                );

                if (pathForCompression != currentAudioPath)
                {
                    lblConvertedPath.Text = "Converted WAV: " + pathForCompression;
                }
                else
                {
                    lblConvertedPath.Text = "Input is already WAV.";
                }

                compressionCancellationSource = new CancellationTokenSource();

                lastCompressionResult = await Task.Run(() =>
                {
                    return audioCompressionService.CompressAudio(pathForCompression, currentCompressionSettings, compressionCancellationSource.Token);

                });

                if (lastCompressionResult.StatusMessage == "Compression cancelled by user.")
                {
                    MessageBox.Show("Compression cancelled safely.");
                    return;
                }

                DisplayCompressionResult(lastCompressionResult);

                MessageBox.Show(
                    "Audio compressed successfully.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (NotSupportedException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Algorithm Not Implemented",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Compression failed.\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                btnCompress.Enabled = true;
                btnCompress.Text = "Compress";
            }
        }

        private void btnDecompress_Click(object sender, EventArgs e)
        {
            if (lastCompressionResult == null ||
                string.IsNullOrWhiteSpace(lastCompressionResult.CompressedFilePath))
            {
                MessageBox.Show(
                    "Please compress an audio file first.",
                    "No Compressed File",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            try
            {
                btnDecompress.Enabled = false;
                btnDecompress.Text = "Decompressing...";

                decompressedFilePath = audioDecompressionService.DecompressAudio(
                    lastCompressionResult.CompressedFilePath,
                    lastCompressionResult.AlgorithmName
                );

                lblDecompressedPath.Text = "Decompressed File: " + decompressedFilePath;

                MessageBox.Show(
                    "Audio decompressed successfully.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Decompression failed.\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                btnDecompress.Enabled = true;
                btnDecompress.Text = "Decompress";
            }
        }

        private void lblFilePath_Click(object sender, EventArgs e)
        {
            // إذا ما بدك تعمل شيء عند الضغط على lblFilePath اتركه فارغ
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (files.Length > 0)
            {
                LoadAudioFile(files[0]);
            }
        }
        //
        string ConvertToWav(string inputFilePath)
        {
            string extension =
                Path.GetExtension(inputFilePath).ToLower();

            if (extension == ".wav")
                return inputFilePath;

            string outputPath =
                Path.Combine(
                    Path.GetDirectoryName(inputFilePath),
                    Path.GetFileNameWithoutExtension(inputFilePath) + "_temp.wav"
                );

            using (AudioFileReader reader =
                   new AudioFileReader(inputFilePath))
            {
                WaveFileWriter.CreateWaveFile16(
                    outputPath,
                    reader);
            }

            return outputPath;
        }


        private void Player_OnPositionChanged(TimeSpan position)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => Player_OnPositionChanged(position)));
                return;
            }

            double total = player.CurrentAudioDuration.TotalSeconds;

            if (total > 0)
            {
                double percent = position.TotalSeconds / total;
                _progressFill.Width = (int)(percent * pnlProgressTrack.Width);
                lblCurrentTime.Text = FormatTime(position);
            }

            if (!player.IsAudioPlaying && !player.IsAudioPaused)
                btnPlay.Text = "▶";
        }
        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (!isAudioLoaded) return;

            if (player.IsAudioPlaying)
            {
                player.PauseAudio();
                btnPlay.Text = "▶";
            }
            else
            {
                player.PlayAudio();
                btnPlay.Text = "⏸";
            }
        }

        private void SetupPlayerUI()
        {
            // لجزء الممتلئ من الشريط
            _progressFill = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(0, pnlProgressTrack.Height),
                BackColor = Color.FromArgb(55, 138, 221)
            };

            pnlProgressTrack.Controls.Add(_progressFill);

            // كليك على الشريط
            pnlProgressTrack.MouseDown += ProgressBar_MouseDown;
            _progressFill.MouseDown += ProgressBar_MouseDown;
        }

        private void ProgressBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (!isAudioLoaded) return;

            int clickX = sender == _progressFill
                ? _progressFill.Left + e.X
                : e.X;

            double percent = Math.Max(0, Math.Min(1,
                (double)clickX / pnlProgressTrack.Width));

            _progressFill.Width = (int)(percent * pnlProgressTrack.Width);

            player.SetAudioPosition(TimeSpan.FromSeconds(
                player.CurrentAudioDuration.TotalSeconds * percent));
        }

        private void btnstop_Click(object sender, EventArgs e)
        {
            player.StopAudio();
            btnPlay.Text = "▶";
            _progressFill.Width = 0;
            lblCurrentTime.Text = "00:00";
        }


        ////////new for 6
        private void InitializeCompressionSettingsControls()
        {
            cmbSampleRate.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSampleRate.Items.Clear();
            cmbSampleRate.Items.AddRange(new object[]
            {
            8000,
            11025,
            16000,
            22050,
            44100,
            48000
            });
            cmbSampleRate.SelectedItem = currentCompressionSettings.SampleRate;

            cmbQuantizationLevels.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbQuantizationLevels.Items.Clear();
            cmbQuantizationLevels.Items.AddRange(new object[]
            {
            2, 4, 8, 16, 32, 64, 128, 256
            });
            cmbQuantizationLevels.SelectedItem = currentCompressionSettings.QuantizationLevels;

            nudDeltaStep.Minimum = 1;
            nudDeltaStep.Maximum = 8192;
            nudDeltaStep.Increment = 10;
            nudDeltaStep.Value = currentCompressionSettings.DeltaStep;

            nudPredictiveQuantizationStep.Minimum = 1;
            nudPredictiveQuantizationStep.Maximum = 8192;
            nudPredictiveQuantizationStep.Increment = 10;
            nudPredictiveQuantizationStep.Value = currentCompressionSettings.PredictiveQuantizationStep;
        }
        private void UpdateCompressionSettingsVisibility()
        {
            if (cmbCompressionAlgorithm.SelectedItem == null)
                return;

            string algorithm = cmbCompressionAlgorithm.SelectedItem.ToString();

            bool isADM = algorithm == CompressionAlgorithms.AdaptiveDeltaModulation;
            bool isPDC = algorithm == CompressionAlgorithms.PredictiveDifferentialCoding;
            bool isDM = algorithm == CompressionAlgorithms.DeltaModulation;
            bool isNQ = algorithm == CompressionAlgorithms.NonlinearQuantization;
            bool isDPCM = algorithm == CompressionAlgorithms.DPCM;

            bool showGeneralSettings = isADM || isPDC || isDM || isNQ || isDPCM;

            lblSampleRate2.Visible = showGeneralSettings;
            cmbSampleRate.Visible = showGeneralSettings;

            lblQuantizationLevels.Visible = showGeneralSettings;
            cmbQuantizationLevels.Visible = showGeneralSettings;

            lblDeltaStep.Visible = isDM;
            nudDeltaStep.Visible = isDM;

            lblPredictiveQuantizationStep.Visible = isPDC || isDPCM;
            nudPredictiveQuantizationStep.Visible = isPDC || isDPCM;

            if (isADM || isDM)
            {
                cmbQuantizationLevels.SelectedItem = 2;
                cmbQuantizationLevels.Enabled = false;
            }
            else
            {
                cmbQuantizationLevels.Enabled = true;
            }
        }
        private bool TryReadCompressionSettingsFromUI()
        {
            if (cmbCompressionAlgorithm.SelectedItem == null)
            {
                MessageBox.Show("Please choose a compression algorithm.");
                return false;
            }

            currentCompressionSettings.AlgorithmName =
                cmbCompressionAlgorithm.SelectedItem.ToString();

            currentCompressionSettings.SampleRate =
                Convert.ToInt32(cmbSampleRate.SelectedItem);

            currentCompressionSettings.QuantizationLevels =
                Convert.ToInt32(cmbQuantizationLevels.SelectedItem);

            currentCompressionSettings.DeltaStep =
                (int)nudDeltaStep.Value;

            currentCompressionSettings.PredictiveQuantizationStep =
                (int)nudPredictiveQuantizationStep.Value;

            return true;
        }
        private string ConvertSampleRateIfNeeded(string wavPath, int targetSampleRate)
        {
            using (var reader = new AudioFileReader(wavPath))
            {
                if (reader.WaveFormat.SampleRate == targetSampleRate)
                    return wavPath;

                string outputPath = Path.Combine(
                    Path.GetDirectoryName(wavPath),
                    Path.GetFileNameWithoutExtension(wavPath) + "_" + targetSampleRate + "Hz.wav"
                );

                var outputFormat = new WaveFormat(
                    targetSampleRate,
                    16,
                    reader.WaveFormat.Channels
                );

                using (var resampler = new MediaFoundationResampler(reader, outputFormat))
                {
                    resampler.ResamplerQuality = 60;
                    WaveFileWriter.CreateWaveFile(outputPath, resampler);
                }

                return outputPath;
            }
        }

        private void btnStopCompression_Click(object sender, EventArgs e)
        {
            compressionCancellationSource.Cancel();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetCompressionWorkspace();
        }

        private void ResetCompressionWorkspace()
        {
            compressionCancellationSource?.Cancel();

            progressBarCompression.Value = 0;
            lblProgress.Text = "0%";

            lblCompressedPath.Text = "CompressedPath";
            lblOriginalSize.Text = "OriginalSize";
            lblCompressedSize.Text = "CompressedSize";
            lblCompressionRatio.Text = "CompressionRatio";
            lblCompressionTime.Text = "CompressionTime";
            lblConvertedPath.Text = "ConvertedPath";
            lblDecompressedPath.Text = "DecompressedPath";

            chartCompressionRatio.Series["CompressionRatio"].Points.Clear();
            chartProcessingSpeed.Series["ProcessingSpeed"].Points.Clear();
            lblChartCompressionRatio.Text = "-";
            lblChartProcessingSpeed.Text = "-";

            monitoringService.Reset();

            currentCompressionSettings = new CompressionSettings();

            cmbSampleRate.SelectedItem = currentCompressionSettings.SampleRate;
            cmbQuantizationLevels.SelectedItem = currentCompressionSettings.QuantizationLevels;
            nudDeltaStep.Value = currentCompressionSettings.DeltaStep;
            nudPredictiveQuantizationStep.Value = currentCompressionSettings.PredictiveQuantizationStep;
            cmbCompressionAlgorithm.SelectedItem = currentCompressionSettings.AlgorithmName;

            decompressedFilePath = null;

            btnCompress.Enabled = true;
            btnStopCompression.Enabled = false;
            btnReset.Enabled = true;
            btnDecompress.Enabled = false;
        }
    }
}