
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
            this.lblDecompressedPath = new System.Windows.Forms.Label();
            this.btnDecompress = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnChooseAudio
            // 
            this.btnChooseAudio.Location = new System.Drawing.Point(68, 121);
            this.btnChooseAudio.Name = "btnChooseAudio";
            this.btnChooseAudio.Size = new System.Drawing.Size(114, 49);
            this.btnChooseAudio.TabIndex = 0;
            this.btnChooseAudio.Text = "ChooseAudio";
            this.btnChooseAudio.UseVisualStyleBackColor = true;
            this.btnChooseAudio.Click += new System.EventHandler(this.btnChooseAudio_Click);
            // 
            // lblFilePath
            // 
            this.lblFilePath.AutoSize = true;
            this.lblFilePath.Location = new System.Drawing.Point(208, 137);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(54, 17);
            this.lblFilePath.TabIndex = 1;
            this.lblFilePath.Text = "FilePath";
            this.lblFilePath.Click += new System.EventHandler(this.lblFilePath_Click);
            // 
            // lblFileSize
            // 
            this.lblFileSize.AutoSize = true;
            this.lblFileSize.Location = new System.Drawing.Point(82, 196);
            this.lblFileSize.Name = "lblFileSize";
            this.lblFileSize.Size = new System.Drawing.Size(49, 17);
            this.lblFileSize.TabIndex = 2;
            this.lblFileSize.Text = "FileSize";
            // 
            // lblDuration
            // 
            this.lblDuration.AutoSize = true;
            this.lblDuration.Location = new System.Drawing.Point(82, 234);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(61, 17);
            this.lblDuration.TabIndex = 3;
            this.lblDuration.Text = "Duration";
            // 
            // lblSampleRate
            // 
            this.lblSampleRate.AutoSize = true;
            this.lblSampleRate.Location = new System.Drawing.Point(85, 277);
            this.lblSampleRate.Name = "lblSampleRate";
            this.lblSampleRate.Size = new System.Drawing.Size(80, 17);
            this.lblSampleRate.TabIndex = 4;
            this.lblSampleRate.Text = "SampleRate";
            // 
            // lblChannels
            // 
            this.lblChannels.AutoSize = true;
            this.lblChannels.Location = new System.Drawing.Point(88, 319);
            this.lblChannels.Name = "lblChannels";
            this.lblChannels.Size = new System.Drawing.Size(63, 17);
            this.lblChannels.TabIndex = 5;
            this.lblChannels.Text = "Channels";
            // 
            // lblBitRate
            // 
            this.lblBitRate.AutoSize = true;
            this.lblBitRate.Location = new System.Drawing.Point(85, 362);
            this.lblBitRate.Name = "lblBitRate";
            this.lblBitRate.Size = new System.Drawing.Size(51, 17);
            this.lblBitRate.TabIndex = 6;
            this.lblBitRate.Text = "BitRate";
            // 
            // lblEncodingType
            // 
            this.lblEncodingType.AutoSize = true;
            this.lblEncodingType.Location = new System.Drawing.Point(85, 407);
            this.lblEncodingType.Name = "lblEncodingType";
            this.lblEncodingType.Size = new System.Drawing.Size(96, 17);
            this.lblEncodingType.TabIndex = 7;
            this.lblEncodingType.Text = "EncodingType";
            // 
            // cmbCompressionAlgorithm
            // 
            this.cmbCompressionAlgorithm.FormattingEnabled = true;
            this.cmbCompressionAlgorithm.Location = new System.Drawing.Point(585, 132);
            this.cmbCompressionAlgorithm.Name = "cmbCompressionAlgorithm";
            this.cmbCompressionAlgorithm.Size = new System.Drawing.Size(106, 24);
            this.cmbCompressionAlgorithm.TabIndex = 8;
            // 
            // btnCompress
            // 
            this.btnCompress.Location = new System.Drawing.Point(706, 121);
            this.btnCompress.Name = "btnCompress";
            this.btnCompress.Size = new System.Drawing.Size(82, 44);
            this.btnCompress.TabIndex = 9;
            this.btnCompress.Text = "Compress";
            this.btnCompress.UseVisualStyleBackColor = true;
            this.btnCompress.Click += new System.EventHandler(this.btnCompress_Click);
            // 
            // lblCompressedPath
            // 
            this.lblCompressedPath.AutoSize = true;
            this.lblCompressedPath.Location = new System.Drawing.Point(585, 191);
            this.lblCompressedPath.Name = "lblCompressedPath";
            this.lblCompressedPath.Size = new System.Drawing.Size(112, 17);
            this.lblCompressedPath.TabIndex = 10;
            this.lblCompressedPath.Text = "CompressedPath";
            // 
            // lblOriginalSize
            // 
            this.lblOriginalSize.AutoSize = true;
            this.lblOriginalSize.Location = new System.Drawing.Point(585, 229);
            this.lblOriginalSize.Name = "lblOriginalSize";
            this.lblOriginalSize.Size = new System.Drawing.Size(75, 17);
            this.lblOriginalSize.TabIndex = 10;
            this.lblOriginalSize.Text = "OriginalSize";
            // 
            // lblCompressedSize
            // 
            this.lblCompressedSize.AutoSize = true;
            this.lblCompressedSize.Location = new System.Drawing.Point(585, 272);
            this.lblCompressedSize.Name = "lblCompressedSize";
            this.lblCompressedSize.Size = new System.Drawing.Size(107, 17);
            this.lblCompressedSize.TabIndex = 10;
            this.lblCompressedSize.Text = "CompressedSize";
            // 
            // lblCompressionRatio
            // 
            this.lblCompressionRatio.AutoSize = true;
            this.lblCompressionRatio.Location = new System.Drawing.Point(585, 314);
            this.lblCompressionRatio.Name = "lblCompressionRatio";
            this.lblCompressionRatio.Size = new System.Drawing.Size(118, 17);
            this.lblCompressionRatio.TabIndex = 10;
            this.lblCompressionRatio.Text = "CompressionRatio";
            // 
            // lblCompressionTime
            // 
            this.lblCompressionTime.AutoSize = true;
            this.lblCompressionTime.Location = new System.Drawing.Point(585, 347);
            this.lblCompressionTime.Name = "lblCompressionTime";
            this.lblCompressionTime.Size = new System.Drawing.Size(116, 17);
            this.lblCompressionTime.TabIndex = 10;
            this.lblCompressionTime.Text = "CompressionTime";
            // 
            // lblAlgorithm
            // 
            this.lblAlgorithm.AutoSize = true;
            this.lblAlgorithm.Location = new System.Drawing.Point(522, 135);
            this.lblAlgorithm.Name = "lblAlgorithm";
            this.lblAlgorithm.Size = new System.Drawing.Size(66, 17);
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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
            this.Name = "MainForm";
            this.Text = "Form1";
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
    }
}

