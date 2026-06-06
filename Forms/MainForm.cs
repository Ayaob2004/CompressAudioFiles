using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CompressAudioFiles.Models;
using CompressAudioFiles.Services;

namespace CompressAudioFiles
{
    public partial class MainForm : Form
    {
        private string currentAudioPath;
        private bool isAudioLoaded;
        private AudioMetadata currentAudioMetadata;
        private CompressionSettings currentCompressionSettings;
        private CompressionResult lastCompressionResult;
        private readonly AudioMetadataService audioMetadataService;
        private readonly AudioCompressionService audioCompressionService;
        private AudioPlayerService player;
        private Panel _progressFill;
        private string FormatTime(TimeSpan t)=> $"{(int)t.TotalMinutes:D2}:{t.Seconds:D2}";

        public MainForm()
        {
            InitializeComponent();
            audioMetadataService = new AudioMetadataService();
            audioCompressionService = new AudioCompressionService();
            currentCompressionSettings = new CompressionSettings();
            InitializeCompressionAlgorithms();
           
            this.AllowDrop = true;
            this.DragEnter += MainForm_DragEnter;
            this.DragDrop += MainForm_DragDrop;

            player = new AudioPlayerService();
            player.OnPositionChanged += Player_OnPositionChanged;
           SetupPlayerUI();
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

            cmbCompressionAlgorithm.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCompressionAlgorithm.SelectedItem = CompressionAlgorithms.AdaptiveDeltaModulation;
        }

        private void btnCompress_Click(object sender, EventArgs e)
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

                currentCompressionSettings.AlgorithmName =
                    cmbCompressionAlgorithm.SelectedItem.ToString();

                lastCompressionResult = audioCompressionService.CompressAudio(
                    currentAudioPath,
                    currentCompressionSettings
                );

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

        private void lblFilePath_Click(object sender, EventArgs e)
        {

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

        private void MainForm_Load(object sender, EventArgs e)
        {

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
    }
}
