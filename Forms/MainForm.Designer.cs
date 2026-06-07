
namespace CompressAudioFiles
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnChooseAudio = new System.Windows.Forms.Button();
            this.lblFilePath = new System.Windows.Forms.Label();
            this.lblFileSize = new System.Windows.Forms.Label();
            this.lblDuration = new System.Windows.Forms.Label();
            this.lblSampleRate = new System.Windows.Forms.Label();
            this.lblChannels = new System.Windows.Forms.Label();
            this.lblBitRate = new System.Windows.Forms.Label();
            this.lblEncodingType = new System.Windows.Forms.Label();
            this.cmbCompressionAlgorithm = new System.Windows.Forms.ComboBox();
            this.btnCompress = new System.Windows.Forms.Button();
            this.lblCompressedPath = new System.Windows.Forms.Label();
            this.lblOriginalSize = new System.Windows.Forms.Label();
            this.lblCompressedSize = new System.Windows.Forms.Label();
            this.lblCompressionRatio = new System.Windows.Forms.Label();
            this.lblCompressionTime = new System.Windows.Forms.Label();
            this.lblAlgorithm = new System.Windows.Forms.Label();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnstop = new System.Windows.Forms.Button();
            this.lblCurrentTime = new System.Windows.Forms.Label();
            this.lblTotalTime = new System.Windows.Forms.Label();
            this.pnlProgressTrack = new System.Windows.Forms.Panel();
            this.lblDecompressedPath = new System.Windows.Forms.Label();
            this.btnDecompress = new System.Windows.Forms.Button();
            this.lblConvertedPath = new System.Windows.Forms.Label();
            this.progressBarCompression = new System.Windows.Forms.ProgressBar();
            this.chartCompressionRatio = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartProcessingSpeed = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lblChartCompressionRatio = new System.Windows.Forms.Label();
            this.lblChartProcessingSpeed = new System.Windows.Forms.Label();
            this.lblProgress = new System.Windows.Forms.Label();
            this.grpCompressionSettings = new System.Windows.Forms.GroupBox();
            this.lblPredictiveQuantizationStep = new System.Windows.Forms.Label();
            this.lblDeltaStep = new System.Windows.Forms.Label();
            this.lblQuantizationLevels = new System.Windows.Forms.Label();
            this.lblSampleRate2 = new System.Windows.Forms.Label();
            this.nudPredictiveQuantizationStep = new System.Windows.Forms.NumericUpDown();
            this.nudDeltaStep = new System.Windows.Forms.NumericUpDown();
            this.cmbQuantizationLevels = new System.Windows.Forms.ComboBox();
            this.cmbSampleRate = new System.Windows.Forms.ComboBox();
            this.btnStopCompression = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chartCompressionRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartProcessingSpeed)).BeginInit();
            this.grpCompressionSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPredictiveQuantizationStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDeltaStep)).BeginInit();
            this.SuspendLayout();
            // 
            // btnChooseAudio
            // 
            this.btnChooseAudio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(95)))), ((int)(((byte)(165)))));
            this.btnChooseAudio.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.btnChooseAudio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChooseAudio.ForeColor = System.Drawing.Color.White;
            this.btnChooseAudio.Location = new System.Drawing.Point(39, 9);
            this.btnChooseAudio.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnChooseAudio.Name = "btnChooseAudio";
            this.btnChooseAudio.Size = new System.Drawing.Size(131, 49);
            this.btnChooseAudio.TabIndex = 0;
            this.btnChooseAudio.Text = "ChooseAudio";
            this.btnChooseAudio.UseVisualStyleBackColor = false;
            this.btnChooseAudio.Click += new System.EventHandler(this.btnChooseAudio_Click);
            // 
            // lblFilePath
            // 
            this.lblFilePath.AutoSize = true;
            this.lblFilePath.ForeColor = System.Drawing.Color.White;
            this.lblFilePath.Location = new System.Drawing.Point(36, 65);
            this.lblFilePath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(59, 17);
            this.lblFilePath.TabIndex = 1;
            this.lblFilePath.Text = "FilePath";
            this.lblFilePath.Click += new System.EventHandler(this.lblFilePath_Click);
            // 
            // lblFileSize
            // 
            this.lblFileSize.AutoSize = true;
            this.lblFileSize.ForeColor = System.Drawing.Color.White;
            this.lblFileSize.Location = new System.Drawing.Point(40, 97);
            this.lblFileSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFileSize.Name = "lblFileSize";
            this.lblFileSize.Size = new System.Drawing.Size(57, 17);
            this.lblFileSize.TabIndex = 2;
            this.lblFileSize.Text = "FileSize";
            // 
            // lblDuration
            // 
            this.lblDuration.AutoSize = true;
            this.lblDuration.ForeColor = System.Drawing.Color.White;
            this.lblDuration.Location = new System.Drawing.Point(36, 134);
            this.lblDuration.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(62, 17);
            this.lblDuration.TabIndex = 3;
            this.lblDuration.Text = "Duration";
            // 
            // lblSampleRate
            // 
            this.lblSampleRate.AutoSize = true;
            this.lblSampleRate.ForeColor = System.Drawing.Color.White;
            this.lblSampleRate.Location = new System.Drawing.Point(36, 169);
            this.lblSampleRate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSampleRate.Name = "lblSampleRate";
            this.lblSampleRate.Size = new System.Drawing.Size(85, 17);
            this.lblSampleRate.TabIndex = 4;
            this.lblSampleRate.Text = "SampleRate";
            // 
            // lblChannels
            // 
            this.lblChannels.AutoSize = true;
            this.lblChannels.ForeColor = System.Drawing.Color.White;
            this.lblChannels.Location = new System.Drawing.Point(40, 206);
            this.lblChannels.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblChannels.Name = "lblChannels";
            this.lblChannels.Size = new System.Drawing.Size(67, 17);
            this.lblChannels.TabIndex = 5;
            this.lblChannels.Text = "Channels";
            // 
            // lblBitRate
            // 
            this.lblBitRate.AutoSize = true;
            this.lblBitRate.ForeColor = System.Drawing.Color.White;
            this.lblBitRate.Location = new System.Drawing.Point(40, 245);
            this.lblBitRate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBitRate.Name = "lblBitRate";
            this.lblBitRate.Size = new System.Drawing.Size(54, 17);
            this.lblBitRate.TabIndex = 6;
            this.lblBitRate.Text = "BitRate";
            // 
            // lblEncodingType
            // 
            this.lblEncodingType.AutoSize = true;
            this.lblEncodingType.ForeColor = System.Drawing.Color.White;
            this.lblEncodingType.Location = new System.Drawing.Point(36, 294);
            this.lblEncodingType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEncodingType.Name = "lblEncodingType";
            this.lblEncodingType.Size = new System.Drawing.Size(99, 17);
            this.lblEncodingType.TabIndex = 7;
            this.lblEncodingType.Text = "EncodingType";
            // 
            // cmbCompressionAlgorithm
            // 
            this.cmbCompressionAlgorithm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(53)))));
            this.cmbCompressionAlgorithm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCompressionAlgorithm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.cmbCompressionAlgorithm.FormattingEnabled = true;
            this.cmbCompressionAlgorithm.Location = new System.Drawing.Point(590, 21);
            this.cmbCompressionAlgorithm.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.cmbCompressionAlgorithm.Name = "cmbCompressionAlgorithm";
            this.cmbCompressionAlgorithm.Size = new System.Drawing.Size(223, 24);
            this.cmbCompressionAlgorithm.TabIndex = 8;
            // 
            // btnCompress
            // 
            this.btnCompress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(95)))), ((int)(((byte)(165)))));
            this.btnCompress.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(95)))), ((int)(((byte)(165)))));
            this.btnCompress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCompress.ForeColor = System.Drawing.Color.White;
            this.btnCompress.Location = new System.Drawing.Point(821, 14);
            this.btnCompress.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnCompress.Name = "btnCompress";
            this.btnCompress.Size = new System.Drawing.Size(125, 44);
            this.btnCompress.TabIndex = 9;
            this.btnCompress.Text = "Compress";
            this.btnCompress.UseVisualStyleBackColor = false;
            this.btnCompress.Click += new System.EventHandler(this.btnCompress_Click);
            // 
            // lblCompressedPath
            // 
            this.lblCompressedPath.AutoSize = true;
            this.lblCompressedPath.ForeColor = System.Drawing.Color.White;
            this.lblCompressedPath.Location = new System.Drawing.Point(587, 63);
            this.lblCompressedPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCompressedPath.Name = "lblCompressedPath";
            this.lblCompressedPath.Size = new System.Drawing.Size(116, 17);
            this.lblCompressedPath.TabIndex = 10;
            this.lblCompressedPath.Text = "CompressedPath";
            // 
            // lblOriginalSize
            // 
            this.lblOriginalSize.AutoSize = true;
            this.lblOriginalSize.ForeColor = System.Drawing.Color.White;
            this.lblOriginalSize.Location = new System.Drawing.Point(587, 104);
            this.lblOriginalSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOriginalSize.Name = "lblOriginalSize";
            this.lblOriginalSize.Size = new System.Drawing.Size(84, 17);
            this.lblOriginalSize.TabIndex = 10;
            this.lblOriginalSize.Text = "OriginalSize";
            // 
            // lblCompressedSize
            // 
            this.lblCompressedSize.AutoSize = true;
            this.lblCompressedSize.ForeColor = System.Drawing.Color.White;
            this.lblCompressedSize.Location = new System.Drawing.Point(587, 141);
            this.lblCompressedSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCompressedSize.Name = "lblCompressedSize";
            this.lblCompressedSize.Size = new System.Drawing.Size(114, 17);
            this.lblCompressedSize.TabIndex = 10;
            this.lblCompressedSize.Text = "CompressedSize";
            // 
            // lblCompressionRatio
            // 
            this.lblCompressionRatio.AutoSize = true;
            this.lblCompressionRatio.ForeColor = System.Drawing.Color.White;
            this.lblCompressionRatio.Location = new System.Drawing.Point(587, 176);
            this.lblCompressionRatio.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCompressionRatio.Name = "lblCompressionRatio";
            this.lblCompressionRatio.Size = new System.Drawing.Size(123, 17);
            this.lblCompressionRatio.TabIndex = 10;
            this.lblCompressionRatio.Text = "CompressionRatio";
            // 
            // lblCompressionTime
            // 
            this.lblCompressionTime.AutoSize = true;
            this.lblCompressionTime.ForeColor = System.Drawing.Color.White;
            this.lblCompressionTime.Location = new System.Drawing.Point(587, 213);
            this.lblCompressionTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCompressionTime.Name = "lblCompressionTime";
            this.lblCompressionTime.Size = new System.Drawing.Size(121, 17);
            this.lblCompressionTime.TabIndex = 10;
            this.lblCompressionTime.Text = "CompressionTime";
            // 
            // lblAlgorithm
            // 
            this.lblAlgorithm.AutoSize = true;
            this.lblAlgorithm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(66)))));
            this.lblAlgorithm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.lblAlgorithm.Location = new System.Drawing.Point(511, 24);
            this.lblAlgorithm.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAlgorithm.Name = "lblAlgorithm";
            this.lblAlgorithm.Size = new System.Drawing.Size(67, 17);
            this.lblAlgorithm.TabIndex = 11;
            this.lblAlgorithm.Text = "Algorithm";
            // 
            // btnPlay
            // 
            this.btnPlay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(95)))), ((int)(((byte)(165)))));
            this.btnPlay.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(95)))), ((int)(((byte)(165)))));
            this.btnPlay.ForeColor = System.Drawing.Color.White;
            this.btnPlay.Location = new System.Drawing.Point(749, 799);
            this.btnPlay.Margin = new System.Windows.Forms.Padding(4);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(64, 59);
            this.btnPlay.TabIndex = 12;
            this.btnPlay.Text = "▶";
            this.btnPlay.UseVisualStyleBackColor = false;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnstop
            // 
            this.btnstop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(53)))));
            this.btnstop.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnstop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnstop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.btnstop.Location = new System.Drawing.Point(688, 804);
            this.btnstop.Margin = new System.Windows.Forms.Padding(4);
            this.btnstop.Name = "btnstop";
            this.btnstop.Size = new System.Drawing.Size(53, 49);
            this.btnstop.TabIndex = 15;
            this.btnstop.Text = "■";
            this.btnstop.UseVisualStyleBackColor = false;
            this.btnstop.Click += new System.EventHandler(this.btnstop_Click);
            // 
            // lblCurrentTime
            // 
            this.lblCurrentTime.AutoSize = true;
            this.lblCurrentTime.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCurrentTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(138)))), ((int)(((byte)(221)))));
            this.lblCurrentTime.Location = new System.Drawing.Point(454, 859);
            this.lblCurrentTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCurrentTime.Name = "lblCurrentTime";
            this.lblCurrentTime.Size = new System.Drawing.Size(44, 20);
            this.lblCurrentTime.TabIndex = 16;
            this.lblCurrentTime.Text = "00:00";
            // 
            // lblTotalTime
            // 
            this.lblTotalTime.AutoSize = true;
            this.lblTotalTime.Font = new System.Drawing.Font("Segoe UI Light", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.lblTotalTime.Location = new System.Drawing.Point(1050, 857);
            this.lblTotalTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalTime.Name = "lblTotalTime";
            this.lblTotalTime.Size = new System.Drawing.Size(44, 20);
            this.lblTotalTime.TabIndex = 17;
            this.lblTotalTime.Text = "00:00";
            // 
            // pnlProgressTrack
            // 
            this.pnlProgressTrack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(69)))));
            this.pnlProgressTrack.Location = new System.Drawing.Point(509, 865);
            this.pnlProgressTrack.Margin = new System.Windows.Forms.Padding(4);
            this.pnlProgressTrack.Name = "pnlProgressTrack";
            this.pnlProgressTrack.Size = new System.Drawing.Size(533, 7);
            this.pnlProgressTrack.TabIndex = 18;
            // 
            // lblDecompressedPath
            // 
            this.lblDecompressedPath.AutoSize = true;
            this.lblDecompressedPath.ForeColor = System.Drawing.Color.White;
            this.lblDecompressedPath.Location = new System.Drawing.Point(587, 289);
            this.lblDecompressedPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDecompressedPath.Name = "lblDecompressedPath";
            this.lblDecompressedPath.Size = new System.Drawing.Size(132, 17);
            this.lblDecompressedPath.TabIndex = 1;
            this.lblDecompressedPath.Text = "DecompressedPath";
            // 
            // btnDecompress
            // 
            this.btnDecompress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(95)))), ((int)(((byte)(165)))));
            this.btnDecompress.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(95)))), ((int)(((byte)(165)))));
            this.btnDecompress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDecompress.ForeColor = System.Drawing.Color.White;
            this.btnDecompress.Location = new System.Drawing.Point(1096, 14);
            this.btnDecompress.Margin = new System.Windows.Forms.Padding(4);
            this.btnDecompress.Name = "btnDecompress";
            this.btnDecompress.Size = new System.Drawing.Size(126, 44);
            this.btnDecompress.TabIndex = 12;
            this.btnDecompress.Text = "Decompress";
            this.btnDecompress.UseVisualStyleBackColor = false;
            this.btnDecompress.Click += new System.EventHandler(this.btnDecompress_Click);
            // 
            // lblConvertedPath
            // 
            this.lblConvertedPath.AutoSize = true;
            this.lblConvertedPath.ForeColor = System.Drawing.Color.White;
            this.lblConvertedPath.Location = new System.Drawing.Point(590, 252);
            this.lblConvertedPath.Name = "lblConvertedPath";
            this.lblConvertedPath.Size = new System.Drawing.Size(102, 17);
            this.lblConvertedPath.TabIndex = 19;
            this.lblConvertedPath.Text = "ConvertedPath";
            // 
            // progressBarCompression
            // 
            this.progressBarCompression.Location = new System.Drawing.Point(1229, 25);
            this.progressBarCompression.Name = "progressBarCompression";
            this.progressBarCompression.Size = new System.Drawing.Size(125, 23);
            this.progressBarCompression.TabIndex = 20;
            // 
            // chartCompressionRatio
            // 
            chartArea1.Name = "ChartArea1";
            this.chartCompressionRatio.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartCompressionRatio.Legends.Add(legend1);
            this.chartCompressionRatio.Location = new System.Drawing.Point(735, 330);
            this.chartCompressionRatio.Name = "chartCompressionRatio";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartCompressionRatio.Series.Add(series1);
            this.chartCompressionRatio.Size = new System.Drawing.Size(378, 300);
            this.chartCompressionRatio.TabIndex = 21;
            this.chartCompressionRatio.Text = "chart1";
            // 
            // chartProcessingSpeed
            // 
            chartArea2.Name = "ChartArea1";
            this.chartProcessingSpeed.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartProcessingSpeed.Legends.Add(legend2);
            this.chartProcessingSpeed.Location = new System.Drawing.Point(1119, 330);
            this.chartProcessingSpeed.Name = "chartProcessingSpeed";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartProcessingSpeed.Series.Add(series2);
            this.chartProcessingSpeed.Size = new System.Drawing.Size(378, 300);
            this.chartProcessingSpeed.TabIndex = 22;
            this.chartProcessingSpeed.Text = "chart2";
            // 
            // lblChartCompressionRatio
            // 
            this.lblChartCompressionRatio.AutoSize = true;
            this.lblChartCompressionRatio.ForeColor = System.Drawing.Color.White;
            this.lblChartCompressionRatio.Location = new System.Drawing.Point(915, 650);
            this.lblChartCompressionRatio.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblChartCompressionRatio.Name = "lblChartCompressionRatio";
            this.lblChartCompressionRatio.Size = new System.Drawing.Size(13, 17);
            this.lblChartCompressionRatio.TabIndex = 23;
            this.lblChartCompressionRatio.Text = "-";
            // 
            // lblChartProcessingSpeed
            // 
            this.lblChartProcessingSpeed.AutoSize = true;
            this.lblChartProcessingSpeed.ForeColor = System.Drawing.Color.White;
            this.lblChartProcessingSpeed.Location = new System.Drawing.Point(1307, 650);
            this.lblChartProcessingSpeed.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblChartProcessingSpeed.Name = "lblChartProcessingSpeed";
            this.lblChartProcessingSpeed.Size = new System.Drawing.Size(13, 17);
            this.lblChartProcessingSpeed.TabIndex = 24;
            this.lblChartProcessingSpeed.Text = "-";
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.ForeColor = System.Drawing.Color.White;
            this.lblProgress.Location = new System.Drawing.Point(1361, 28);
            this.lblProgress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(28, 17);
            this.lblProgress.TabIndex = 25;
            this.lblProgress.Text = "0%";
            // 
            // grpCompressionSettings
            // 
            this.grpCompressionSettings.Controls.Add(this.lblPredictiveQuantizationStep);
            this.grpCompressionSettings.Controls.Add(this.lblDeltaStep);
            this.grpCompressionSettings.Controls.Add(this.lblQuantizationLevels);
            this.grpCompressionSettings.Controls.Add(this.lblSampleRate2);
            this.grpCompressionSettings.Controls.Add(this.nudPredictiveQuantizationStep);
            this.grpCompressionSettings.Controls.Add(this.nudDeltaStep);
            this.grpCompressionSettings.Controls.Add(this.cmbQuantizationLevels);
            this.grpCompressionSettings.Controls.Add(this.cmbSampleRate);
            this.grpCompressionSettings.ForeColor = System.Drawing.Color.White;
            this.grpCompressionSettings.Location = new System.Drawing.Point(39, 351);
            this.grpCompressionSettings.Name = "grpCompressionSettings";
            this.grpCompressionSettings.Size = new System.Drawing.Size(459, 236);
            this.grpCompressionSettings.TabIndex = 26;
            this.grpCompressionSettings.TabStop = false;
            this.grpCompressionSettings.Text = "Compression Settings";
            // 
            // lblPredictiveQuantizationStep
            // 
            this.lblPredictiveQuantizationStep.AutoSize = true;
            this.lblPredictiveQuantizationStep.Location = new System.Drawing.Point(37, 183);
            this.lblPredictiveQuantizationStep.Name = "lblPredictiveQuantizationStep";
            this.lblPredictiveQuantizationStep.Size = new System.Drawing.Size(187, 17);
            this.lblPredictiveQuantizationStep.TabIndex = 3;
            this.lblPredictiveQuantizationStep.Text = "Predictive Quantization Step";
            // 
            // lblDeltaStep
            // 
            this.lblDeltaStep.AutoSize = true;
            this.lblDeltaStep.Location = new System.Drawing.Point(37, 142);
            this.lblDeltaStep.Name = "lblDeltaStep";
            this.lblDeltaStep.Size = new System.Drawing.Size(74, 17);
            this.lblDeltaStep.TabIndex = 3;
            this.lblDeltaStep.Text = "Delta Step";
            // 
            // lblQuantizationLevels
            // 
            this.lblQuantizationLevels.AutoSize = true;
            this.lblQuantizationLevels.Location = new System.Drawing.Point(36, 93);
            this.lblQuantizationLevels.Name = "lblQuantizationLevels";
            this.lblQuantizationLevels.Size = new System.Drawing.Size(133, 17);
            this.lblQuantizationLevels.TabIndex = 3;
            this.lblQuantizationLevels.Text = "Quantization Levels";
            // 
            // lblSampleRate2
            // 
            this.lblSampleRate2.AutoSize = true;
            this.lblSampleRate2.Location = new System.Drawing.Point(36, 51);
            this.lblSampleRate2.Name = "lblSampleRate2";
            this.lblSampleRate2.Size = new System.Drawing.Size(89, 17);
            this.lblSampleRate2.TabIndex = 3;
            this.lblSampleRate2.Text = "Sample Rate";
            // 
            // nudPredictiveQuantizationStep
            // 
            this.nudPredictiveQuantizationStep.Location = new System.Drawing.Point(275, 178);
            this.nudPredictiveQuantizationStep.Name = "nudPredictiveQuantizationStep";
            this.nudPredictiveQuantizationStep.Size = new System.Drawing.Size(151, 22);
            this.nudPredictiveQuantizationStep.TabIndex = 2;
            // 
            // nudDeltaStep
            // 
            this.nudDeltaStep.Location = new System.Drawing.Point(275, 137);
            this.nudDeltaStep.Name = "nudDeltaStep";
            this.nudDeltaStep.Size = new System.Drawing.Size(151, 22);
            this.nudDeltaStep.TabIndex = 1;
            // 
            // cmbQuantizationLevels
            // 
            this.cmbQuantizationLevels.FormattingEnabled = true;
            this.cmbQuantizationLevels.Location = new System.Drawing.Point(274, 86);
            this.cmbQuantizationLevels.Name = "cmbQuantizationLevels";
            this.cmbQuantizationLevels.Size = new System.Drawing.Size(152, 24);
            this.cmbQuantizationLevels.TabIndex = 0;
            // 
            // cmbSampleRate
            // 
            this.cmbSampleRate.FormattingEnabled = true;
            this.cmbSampleRate.Location = new System.Drawing.Point(274, 44);
            this.cmbSampleRate.Name = "cmbSampleRate";
            this.cmbSampleRate.Size = new System.Drawing.Size(152, 24);
            this.cmbSampleRate.TabIndex = 0;
            // 
            // btnStopCompression
            // 
            this.btnStopCompression.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(95)))), ((int)(((byte)(165)))));
            this.btnStopCompression.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(95)))), ((int)(((byte)(165)))));
            this.btnStopCompression.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStopCompression.ForeColor = System.Drawing.Color.White;
            this.btnStopCompression.Location = new System.Drawing.Point(954, 14);
            this.btnStopCompression.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnStopCompression.Name = "btnStopCompression";
            this.btnStopCompression.Size = new System.Drawing.Size(125, 44);
            this.btnStopCompression.TabIndex = 27;
            this.btnStopCompression.Text = "Stop";
            this.btnStopCompression.UseVisualStyleBackColor = false;
            this.btnStopCompression.Click += new System.EventHandler(this.btnStopCompression_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(46)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1827, 922);
            this.Controls.Add(this.btnStopCompression);
            this.Controls.Add(this.grpCompressionSettings);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.lblChartProcessingSpeed);
            this.Controls.Add(this.lblChartCompressionRatio);
            this.Controls.Add(this.chartProcessingSpeed);
            this.Controls.Add(this.chartCompressionRatio);
            this.Controls.Add(this.progressBarCompression);
            this.Controls.Add(this.lblConvertedPath);
            this.Controls.Add(this.pnlProgressTrack);
            this.Controls.Add(this.lblTotalTime);
            this.Controls.Add(this.lblCurrentTime);
            this.Controls.Add(this.btnstop);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.btnDecompress);
            this.Controls.Add(this.lblAlgorithm);
            this.Controls.Add(this.lblCompressionTime);
            this.Controls.Add(this.lblCompressionRatio);
            this.Controls.Add(this.lblCompressedSize);
            this.Controls.Add(this.lblOriginalSize);
            this.Controls.Add(this.lblCompressedPath);
            this.Controls.Add(this.btnCompress);
            this.Controls.Add(this.cmbCompressionAlgorithm);
            this.Controls.Add(this.lblEncodingType);
            this.Controls.Add(this.lblBitRate);
            this.Controls.Add(this.lblChannels);
            this.Controls.Add(this.lblSampleRate);
            this.Controls.Add(this.lblDuration);
            this.Controls.Add(this.lblFileSize);
            this.Controls.Add(this.lblDecompressedPath);
            this.Controls.Add(this.lblFilePath);
            this.Controls.Add(this.btnChooseAudio);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.Name = "MainForm";
            this.Text = "Audio";
            ((System.ComponentModel.ISupportInitialize)(this.chartCompressionRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartProcessingSpeed)).EndInit();
            this.grpCompressionSettings.ResumeLayout(false);
            this.grpCompressionSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPredictiveQuantizationStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDeltaStep)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnChooseAudio;
        private System.Windows.Forms.Label lblFilePath;
        private System.Windows.Forms.Label lblFileSize;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.Label lblSampleRate;
        private System.Windows.Forms.Label lblChannels;
        private System.Windows.Forms.Label lblBitRate;
        private System.Windows.Forms.Label lblEncodingType;
        private System.Windows.Forms.ComboBox cmbCompressionAlgorithm;
        private System.Windows.Forms.Button btnCompress;
        private System.Windows.Forms.Label lblCompressedPath;
        private System.Windows.Forms.Label lblOriginalSize;
        private System.Windows.Forms.Label lblCompressedSize;
        private System.Windows.Forms.Label lblCompressionRatio;
        private System.Windows.Forms.Label lblCompressionTime;
        private System.Windows.Forms.Label lblAlgorithm;
        private System.Windows.Forms.Label lblDecompressedPath;
        private System.Windows.Forms.Button btnDecompress;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnstop;
        private System.Windows.Forms.Label lblCurrentTime;
        private System.Windows.Forms.Label lblTotalTime;
        private System.Windows.Forms.Panel pnlProgressTrack;
        private System.Windows.Forms.Label lblConvertedPath;
        private System.Windows.Forms.ProgressBar progressBarCompression;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartCompressionRatio;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartProcessingSpeed;
        private System.Windows.Forms.Label lblChartCompressionRatio;
        private System.Windows.Forms.Label lblChartProcessingSpeed;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.GroupBox grpCompressionSettings;
        private System.Windows.Forms.ComboBox cmbSampleRate;
        private System.Windows.Forms.Label lblPredictiveQuantizationStep;
        private System.Windows.Forms.Label lblDeltaStep;
        private System.Windows.Forms.Label lblQuantizationLevels;
        private System.Windows.Forms.Label lblSampleRate2;
        private System.Windows.Forms.NumericUpDown nudPredictiveQuantizationStep;
        private System.Windows.Forms.NumericUpDown nudDeltaStep;
        private System.Windows.Forms.ComboBox cmbQuantizationLevels;
        private System.Windows.Forms.Button btnStopCompression;
    }
}

