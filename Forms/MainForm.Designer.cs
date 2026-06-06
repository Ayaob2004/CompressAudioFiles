
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
            this.SuspendLayout();
            // 
            // btnChooseAudio
            // 
            this.btnChooseAudio.Location = new System.Drawing.Point(12, 11);
            this.btnChooseAudio.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnChooseAudio.Name = "btnChooseAudio";
            this.btnChooseAudio.Size = new System.Drawing.Size(98, 40);
            this.btnChooseAudio.TabIndex = 0;
            this.btnChooseAudio.Text = "ChooseAudio";
            this.btnChooseAudio.UseVisualStyleBackColor = true;
            this.btnChooseAudio.Click += new System.EventHandler(this.btnChooseAudio_Click);
            // 
            // lblFilePath
            // 
            this.lblFilePath.AutoSize = true;
            this.lblFilePath.Location = new System.Drawing.Point(27, 53);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(45, 13);
            this.lblFilePath.TabIndex = 1;
            this.lblFilePath.Text = "FilePath";
            this.lblFilePath.Click += new System.EventHandler(this.lblFilePath_Click);
            // 
            // lblFileSize
            // 
            this.lblFileSize.AutoSize = true;
            this.lblFileSize.Location = new System.Drawing.Point(30, 80);
            this.lblFileSize.Name = "lblFileSize";
            this.lblFileSize.Size = new System.Drawing.Size(42, 13);
            this.lblFileSize.TabIndex = 2;
            this.lblFileSize.Text = "FileSize";
            // 
            // lblDuration
            // 
            this.lblDuration.AutoSize = true;
            this.lblDuration.Location = new System.Drawing.Point(30, 115);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(48, 13);
            this.lblDuration.TabIndex = 3;
            this.lblDuration.Text = "Duration";
            // 
            // lblSampleRate
            // 
            this.lblSampleRate.AutoSize = true;
            this.lblSampleRate.Location = new System.Drawing.Point(27, 144);
            this.lblSampleRate.Name = "lblSampleRate";
            this.lblSampleRate.Size = new System.Drawing.Size(64, 13);
            this.lblSampleRate.TabIndex = 4;
            this.lblSampleRate.Text = "SampleRate";
            // 
            // lblChannels
            // 
            this.lblChannels.AutoSize = true;
            this.lblChannels.Location = new System.Drawing.Point(30, 175);
            this.lblChannels.Name = "lblChannels";
            this.lblChannels.Size = new System.Drawing.Size(51, 13);
            this.lblChannels.TabIndex = 5;
            this.lblChannels.Text = "Channels";
            // 
            // lblBitRate
            // 
            this.lblBitRate.AutoSize = true;
            this.lblBitRate.Location = new System.Drawing.Point(36, 212);
            this.lblBitRate.Name = "lblBitRate";
            this.lblBitRate.Size = new System.Drawing.Size(42, 13);
            this.lblBitRate.TabIndex = 6;
            this.lblBitRate.Text = "BitRate";
            // 
            // lblEncodingType
            // 
            this.lblEncodingType.AutoSize = true;
            this.lblEncodingType.Location = new System.Drawing.Point(27, 255);
            this.lblEncodingType.Name = "lblEncodingType";
            this.lblEncodingType.Size = new System.Drawing.Size(74, 13);
            this.lblEncodingType.TabIndex = 7;
            this.lblEncodingType.Text = "EncodingType";
            // 
            // cmbCompressionAlgorithm
            // 
            this.cmbCompressionAlgorithm.FormattingEnabled = true;
            this.cmbCompressionAlgorithm.Location = new System.Drawing.Point(584, 11);
            this.cmbCompressionAlgorithm.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbCompressionAlgorithm.Name = "cmbCompressionAlgorithm";
            this.cmbCompressionAlgorithm.Size = new System.Drawing.Size(91, 21);
            this.cmbCompressionAlgorithm.TabIndex = 8;
            // 
            // btnCompress
            // 
            this.btnCompress.Location = new System.Drawing.Point(691, 2);
            this.btnCompress.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCompress.Name = "btnCompress";
            this.btnCompress.Size = new System.Drawing.Size(70, 36);
            this.btnCompress.TabIndex = 9;
            this.btnCompress.Text = "Compress";
            this.btnCompress.UseVisualStyleBackColor = true;
            this.btnCompress.Click += new System.EventHandler(this.btnCompress_Click);
            // 
            // lblCompressedPath
            // 
            this.lblCompressedPath.AutoSize = true;
            this.lblCompressedPath.Location = new System.Drawing.Point(587, 64);
            this.lblCompressedPath.Name = "lblCompressedPath";
            this.lblCompressedPath.Size = new System.Drawing.Size(88, 13);
            this.lblCompressedPath.TabIndex = 10;
            this.lblCompressedPath.Text = "CompressedPath";
            // 
            // lblOriginalSize
            // 
            this.lblOriginalSize.AutoSize = true;
            this.lblOriginalSize.Location = new System.Drawing.Point(604, 100);
            this.lblOriginalSize.Name = "lblOriginalSize";
            this.lblOriginalSize.Size = new System.Drawing.Size(62, 13);
            this.lblOriginalSize.TabIndex = 10;
            this.lblOriginalSize.Text = "OriginalSize";
            // 
            // lblCompressedSize
            // 
            this.lblCompressedSize.AutoSize = true;
            this.lblCompressedSize.Location = new System.Drawing.Point(604, 144);
            this.lblCompressedSize.Name = "lblCompressedSize";
            this.lblCompressedSize.Size = new System.Drawing.Size(85, 13);
            this.lblCompressedSize.TabIndex = 10;
            this.lblCompressedSize.Text = "CompressedSize";
            // 
            // lblCompressionRatio
            // 
            this.lblCompressionRatio.AutoSize = true;
            this.lblCompressionRatio.Location = new System.Drawing.Point(604, 188);
            this.lblCompressionRatio.Name = "lblCompressionRatio";
            this.lblCompressionRatio.Size = new System.Drawing.Size(93, 13);
            this.lblCompressionRatio.TabIndex = 10;
            this.lblCompressionRatio.Text = "CompressionRatio";
            // 
            // lblCompressionTime
            // 
            this.lblCompressionTime.AutoSize = true;
            this.lblCompressionTime.Location = new System.Drawing.Point(607, 239);
            this.lblCompressionTime.Name = "lblCompressionTime";
            this.lblCompressionTime.Size = new System.Drawing.Size(90, 13);
            this.lblCompressionTime.TabIndex = 10;
            this.lblCompressionTime.Text = "CompressionTime";
            // 
            // lblAlgorithm
            // 
            this.lblAlgorithm.AutoSize = true;
            this.lblAlgorithm.Location = new System.Drawing.Point(511, 14);
            this.lblAlgorithm.Name = "lblAlgorithm";
            this.lblAlgorithm.Size = new System.Drawing.Size(52, 13);
            this.lblAlgorithm.TabIndex = 11;
            this.lblAlgorithm.Text = "Algorithm";
            // 
            // lblDecompressedPath
            // 
            this.lblDecompressedPath.AutoSize = true;
            this.lblDecompressedPath.Location = new System.Drawing.Point(1051, 132);
            this.lblDecompressedPath.Name = "lblDecompressedPath";
            this.lblDecompressedPath.Size = new System.Drawing.Size(132, 17);
            this.lblDecompressedPath.TabIndex = 1;
            this.lblDecompressedPath.Text = "DecompressedPath";
            // 
            // btnDecompress
            // 
            this.btnDecompress.Location = new System.Drawing.Point(927, 121);
            this.btnDecompress.Name = "btnDecompress";
            this.btnDecompress.Size = new System.Drawing.Size(118, 44);
            this.btnDecompress.TabIndex = 12;
            this.btnDecompress.Text = "Decompress";
            this.btnDecompress.UseVisualStyleBackColor = true;
            this.btnDecompress.Click += new System.EventHandler(this.btnDecompress_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(469, 351);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(75, 23);
            this.btnPlay.TabIndex = 12;
            this.btnPlay.Text = "▶";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnstop
            // 
            this.btnstop.Location = new System.Drawing.Point(369, 351);
            this.btnstop.Name = "btnstop";
            this.btnstop.Size = new System.Drawing.Size(75, 23);
            this.btnstop.TabIndex = 15;
            this.btnstop.Text = "■";
            this.btnstop.UseVisualStyleBackColor = true;
            this.btnstop.Click += new System.EventHandler(this.btnstop_Click);
            // 
            // lblCurrentTime
            // 
            this.lblCurrentTime.AutoSize = true;
            this.lblCurrentTime.Location = new System.Drawing.Point(75, 387);
            this.lblCurrentTime.Name = "lblCurrentTime";
            this.lblCurrentTime.Size = new System.Drawing.Size(35, 13);
            this.lblCurrentTime.TabIndex = 16;
            this.lblCurrentTime.Text = "label1";
            // 
            // lblTotalTime
            // 
            this.lblTotalTime.AutoSize = true;
            this.lblTotalTime.Location = new System.Drawing.Point(936, 387);
            this.lblTotalTime.Name = "lblTotalTime";
            this.lblTotalTime.Size = new System.Drawing.Size(35, 13);
            this.lblTotalTime.TabIndex = 17;
            this.lblTotalTime.Text = "label2";
            // 
            // pnlProgressTrack
            // 
            this.pnlProgressTrack.BackColor = System.Drawing.SystemColors.GrayText;
            this.pnlProgressTrack.Location = new System.Drawing.Point(137, 390);
            this.pnlProgressTrack.Name = "pnlProgressTrack";
            this.pnlProgressTrack.Size = new System.Drawing.Size(777, 10);
            this.pnlProgressTrack.TabIndex = 18;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1077, 609);
            this.Controls.Add(this.pnlProgressTrack);
            this.Controls.Add(this.lblTotalTime);
            this.Controls.Add(this.lblCurrentTime);
            this.Controls.Add(this.btnstop);
            this.Controls.Add(this.btnPlay);
            this.ClientSize = new System.Drawing.Size(1437, 829);
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
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
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
    }
}

