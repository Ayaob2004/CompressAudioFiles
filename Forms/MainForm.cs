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

        public MainForm()
        {
            InitializeComponent();
            audioMetadataService = new AudioMetadataService();
            audioCompressionService = new AudioCompressionService();
            currentCompressionSettings = new CompressionSettings();
            InitializeCompressionAlgorithms();

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


    }
}
