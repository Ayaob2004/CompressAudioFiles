using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CompressAudioFiles.Models;
using CompressAudioFiles.Services;
using NAudio.Wave;

namespace CompressAudioFiles
{
    public partial class MainForm : Form
    {
        private string currentAudioPath;
        private bool isAudioLoaded;
        private AudioMetadata currentAudioMetadata;
        private CompressionSettings currentCompressionSettings;
        private CompressionResult lastCompressionResult;
        private string decompressedFilePath;
        private readonly AudioMetadataService audioMetadataService;
        private readonly AudioCompressionService audioCompressionService;
        private readonly AudioDecompressionService audioDecompressionService;

        public MainForm()
        {
            InitializeComponent();
            audioMetadataService = new AudioMetadataService();
            audioCompressionService = new AudioCompressionService();
            currentCompressionSettings = new CompressionSettings();
            audioDecompressionService = new AudioDecompressionService();
            InitializeCompressionAlgorithms();

            this.AllowDrop = true;
            this.DragEnter += MainForm_DragEnter;
            this.DragDrop += MainForm_DragDrop;

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

                string pathForCompression = ConvertToWav(currentAudioPath);

                if (pathForCompression != currentAudioPath)
                {
                    lblConvertedPath.Text = "Converted WAV: " + pathForCompression;
                }
                else
                {
                    lblConvertedPath.Text = "Input is already WAV.";
                }

                lastCompressionResult = audioCompressionService.CompressAudio(
                    pathForCompression,
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
    }
}
