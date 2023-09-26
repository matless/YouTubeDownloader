﻿namespace YouTubeDownloader
{
    partial class FormYouTubeDownloader
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            movieURL = new Label();
            textBoxDownload = new TextBox();
            downloadButton = new Button();
            cancelButton = new Button();
            SuspendLayout();
            // 
            // movieURL
            // 
            movieURL.AutoSize = true;
            movieURL.Location = new Point(26, 25);
            movieURL.Name = "movieURL";
            movieURL.Size = new Size(64, 15);
            movieURL.TabIndex = 0;
            movieURL.Text = "Movie URL";
            // 
            // textBoxDownload
            // 
            textBoxDownload.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxDownload.Location = new Point(26, 43);
            textBoxDownload.Name = "textBoxDownload";
            textBoxDownload.Size = new Size(647, 23);
            textBoxDownload.TabIndex = 1;
            // 
            // downloadButton
            // 
            downloadButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            downloadButton.Location = new Point(690, 25);
            downloadButton.Name = "downloadButton";
            downloadButton.Size = new Size(75, 23);
            downloadButton.TabIndex = 2;
            downloadButton.Text = "Download";
            downloadButton.UseVisualStyleBackColor = true;
            downloadButton.Click += downloadButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cancelButton.Enabled = false;
            cancelButton.Location = new Point(690, 54);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.TabIndex = 3;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // FormYouTubeDownloader
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(805, 94);
            Controls.Add(cancelButton);
            Controls.Add(downloadButton);
            Controls.Add(textBoxDownload);
            Controls.Add(movieURL);
            Name = "FormYouTubeDownloader";
            Text = "YouTube Downloader";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label movieURL;
        private TextBox textBoxDownload;
        private Button downloadButton;
        private Button cancelButton;
    }
}