
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
            this.SuspendLayout();
            // 
            // btnChooseAudio
            // 
            this.btnChooseAudio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(69)))));
            this.btnChooseAudio.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))), ((int)(((byte)(4)))));
            this.btnChooseAudio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChooseAudio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.btnChooseAudio.Location = new System.Drawing.Point(12, 11);
            this.btnChooseAudio.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnChooseAudio.Name = "btnChooseAudio";
            this.btnChooseAudio.Size = new System.Drawing.Size(98, 40);
            this.btnChooseAudio.TabIndex = 0;
            this.btnChooseAudio.Text = "ChooseAudio";
            this.btnChooseAudio.UseVisualStyleBackColor = false;
            this.btnChooseAudio.Click += new System.EventHandler(this.btnChooseAudio_Click);
            // 
            // lblFilePath
            // 
            this.lblFilePath.AutoSize = true;
            this.lblFilePath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
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
            this.lblFileSize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.lblFileSize.Location = new System.Drawing.Point(30, 79);
            this.lblFileSize.Name = "lblFileSize";
            this.lblFileSize.Size = new System.Drawing.Size(42, 13);
            this.lblFileSize.TabIndex = 2;
            this.lblFileSize.Text = "FileSize";
            // 
            // lblDuration
            // 
            this.lblDuration.AutoSize = true;
            this.lblDuration.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.lblDuration.Location = new System.Drawing.Point(27, 109);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(48, 13);
            this.lblDuration.TabIndex = 3;
            this.lblDuration.Text = "Duration";
            // 
            // lblSampleRate
            // 
            this.lblSampleRate.AutoSize = true;
            this.lblSampleRate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.lblSampleRate.Location = new System.Drawing.Point(27, 137);
            this.lblSampleRate.Name = "lblSampleRate";
            this.lblSampleRate.Size = new System.Drawing.Size(64, 13);
            this.lblSampleRate.TabIndex = 4;
            this.lblSampleRate.Text = "SampleRate";
            // 
            // lblChannels
            // 
            this.lblChannels.AutoSize = true;
            this.lblChannels.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.lblChannels.Location = new System.Drawing.Point(30, 167);
            this.lblChannels.Name = "lblChannels";
            this.lblChannels.Size = new System.Drawing.Size(51, 13);
            this.lblChannels.TabIndex = 5;
            this.lblChannels.Text = "Channels";
            // 
            // lblBitRate
            // 
            this.lblBitRate.AutoSize = true;
            this.lblBitRate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.lblBitRate.Location = new System.Drawing.Point(30, 199);
            this.lblBitRate.Name = "lblBitRate";
            this.lblBitRate.Size = new System.Drawing.Size(42, 13);
            this.lblBitRate.TabIndex = 6;
            this.lblBitRate.Text = "BitRate";
            // 
            // lblEncodingType
            // 
            this.lblEncodingType.AutoSize = true;
            this.lblEncodingType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.lblEncodingType.Location = new System.Drawing.Point(27, 239);
            this.lblEncodingType.Name = "lblEncodingType";
            this.lblEncodingType.Size = new System.Drawing.Size(74, 13);
            this.lblEncodingType.TabIndex = 7;
            this.lblEncodingType.Text = "EncodingType";
            // 
            // cmbCompressionAlgorithm
            // 
            this.cmbCompressionAlgorithm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(53)))));
            this.cmbCompressionAlgorithm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCompressionAlgorithm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.cmbCompressionAlgorithm.FormattingEnabled = true;
            this.cmbCompressionAlgorithm.Location = new System.Drawing.Point(584, 11);
            this.cmbCompressionAlgorithm.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbCompressionAlgorithm.Name = "cmbCompressionAlgorithm";
            this.cmbCompressionAlgorithm.Size = new System.Drawing.Size(91, 21);
            this.cmbCompressionAlgorithm.TabIndex = 8;
            // 
            // btnCompress
            // 
            this.btnCompress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(95)))), ((int)(((byte)(165)))));
            this.btnCompress.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(95)))), ((int)(((byte)(165)))));
            this.btnCompress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCompress.ForeColor = System.Drawing.Color.White;
            this.btnCompress.Location = new System.Drawing.Point(691, 2);
            this.btnCompress.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCompress.Name = "btnCompress";
            this.btnCompress.Size = new System.Drawing.Size(76, 36);
            this.btnCompress.TabIndex = 9;
            this.btnCompress.Text = "Compress";
            this.btnCompress.UseVisualStyleBackColor = false;
            this.btnCompress.Click += new System.EventHandler(this.btnCompress_Click);
            // 
            // lblCompressedPath
            // 
            this.lblCompressedPath.AutoSize = true;
            this.lblCompressedPath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.lblCompressedPath.Location = new System.Drawing.Point(587, 53);
            this.lblCompressedPath.Name = "lblCompressedPath";
            this.lblCompressedPath.Size = new System.Drawing.Size(88, 13);
            this.lblCompressedPath.TabIndex = 10;
            this.lblCompressedPath.Text = "CompressedPath";
            // 
            // lblOriginalSize
            // 
            this.lblOriginalSize.AutoSize = true;
            this.lblOriginalSize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.lblOriginalSize.Location = new System.Drawing.Point(587, 93);
            this.lblOriginalSize.Name = "lblOriginalSize";
            this.lblOriginalSize.Size = new System.Drawing.Size(62, 13);
            this.lblOriginalSize.TabIndex = 10;
            this.lblOriginalSize.Text = "OriginalSize";
            // 
            // lblCompressedSize
            // 
            this.lblCompressedSize.AutoSize = true;
            this.lblCompressedSize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.lblCompressedSize.Location = new System.Drawing.Point(590, 132);
            this.lblCompressedSize.Name = "lblCompressedSize";
            this.lblCompressedSize.Size = new System.Drawing.Size(85, 13);
            this.lblCompressedSize.TabIndex = 10;
            this.lblCompressedSize.Text = "CompressedSize";
            // 
            // lblCompressionRatio
            // 
            this.lblCompressionRatio.AutoSize = true;
            this.lblCompressionRatio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.lblCompressionRatio.Location = new System.Drawing.Point(590, 167);
            this.lblCompressionRatio.Name = "lblCompressionRatio";
            this.lblCompressionRatio.Size = new System.Drawing.Size(93, 13);
            this.lblCompressionRatio.TabIndex = 10;
            this.lblCompressionRatio.Text = "CompressionRatio";
            // 
            // lblCompressionTime
            // 
            this.lblCompressionTime.AutoSize = true;
            this.lblCompressionTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.lblCompressionTime.Location = new System.Drawing.Point(593, 199);
            this.lblCompressionTime.Name = "lblCompressionTime";
            this.lblCompressionTime.Size = new System.Drawing.Size(90, 13);
            this.lblCompressionTime.TabIndex = 10;
            this.lblCompressionTime.Text = "CompressionTime";
            // 
            // lblAlgorithm
            // 
            this.lblAlgorithm.AutoSize = true;
            this.lblAlgorithm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(66)))));
            this.lblAlgorithm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.lblAlgorithm.Location = new System.Drawing.Point(511, 14);
            this.lblAlgorithm.Name = "lblAlgorithm";
            this.lblAlgorithm.Size = new System.Drawing.Size(52, 13);
            this.lblAlgorithm.TabIndex = 11;
            this.lblAlgorithm.Text = "Algorithm";
            // 
            // btnPlay
            // 
            this.btnPlay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(95)))), ((int)(((byte)(165)))));
            this.btnPlay.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(95)))), ((int)(((byte)(165)))));
            this.btnPlay.ForeColor = System.Drawing.Color.White;
            this.btnPlay.Location = new System.Drawing.Point(320, 372);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(48, 48);
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
            this.btnstop.Location = new System.Drawing.Point(261, 380);
            this.btnstop.Name = "btnstop";
            this.btnstop.Size = new System.Drawing.Size(40, 40);
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
            this.lblCurrentTime.Location = new System.Drawing.Point(99, 421);
            this.lblCurrentTime.Name = "lblCurrentTime";
            this.lblCurrentTime.Size = new System.Drawing.Size(34, 15);
            this.lblCurrentTime.TabIndex = 16;
            this.lblCurrentTime.Text = "00:00";
            // 
            // lblTotalTime
            // 
            this.lblTotalTime.AutoSize = true;
            this.lblTotalTime.Font = new System.Drawing.Font("Segoe UI Light", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.lblTotalTime.Location = new System.Drawing.Point(546, 419);
            this.lblTotalTime.Name = "lblTotalTime";
            this.lblTotalTime.Size = new System.Drawing.Size(34, 15);
            this.lblTotalTime.TabIndex = 17;
            this.lblTotalTime.Text = "00:00";
            // 
            // pnlProgressTrack
            // 
            this.pnlProgressTrack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(69)))));
            this.pnlProgressTrack.Location = new System.Drawing.Point(140, 426);
            this.pnlProgressTrack.Name = "pnlProgressTrack";
            this.pnlProgressTrack.Size = new System.Drawing.Size(400, 6);
            this.pnlProgressTrack.TabIndex = 18;
            // 
            // lblDecompressedPath
            // 
            this.lblDecompressedPath.AutoSize = true;
            this.lblDecompressedPath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.lblDecompressedPath.Location = new System.Drawing.Point(1016, 53);
            this.lblDecompressedPath.Name = "lblDecompressedPath";
            this.lblDecompressedPath.Size = new System.Drawing.Size(99, 13);
            this.lblDecompressedPath.TabIndex = 1;
            this.lblDecompressedPath.Text = "DecompressedPath";
            // 
            // btnDecompress
            // 
            this.btnDecompress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(95)))), ((int)(((byte)(165)))));
            this.btnDecompress.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(95)))), ((int)(((byte)(165)))));
            this.btnDecompress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDecompress.ForeColor = System.Drawing.Color.White;
            this.btnDecompress.Location = new System.Drawing.Point(1019, 2);
            this.btnDecompress.Name = "btnDecompress";
            this.btnDecompress.Size = new System.Drawing.Size(75, 36);
            this.btnDecompress.TabIndex = 12;
            this.btnDecompress.Text = "Decompress";
            this.btnDecompress.UseVisualStyleBackColor = false;
            this.btnDecompress.Click += new System.EventHandler(this.btnDecompress_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(46)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1370, 749);
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
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainForm";
            this.Text = "Audio";
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

