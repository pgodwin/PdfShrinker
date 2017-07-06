﻿namespace PdfShrinker
{
    partial class frmMain
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pbOverall = new System.Windows.Forms.ProgressBar();
            this.lblFileStatus = new System.Windows.Forms.Label();
            this.pbFile = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(73, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(187, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "-- Drop documents to compress here --";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(89, 43);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(144, 23);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "or Open them here";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoEllipsis = true;
            this.lblStatus.Location = new System.Drawing.Point(12, 89);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(302, 13);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Ready.";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblStatus.Visible = false;
            // 
            // pbOverall
            // 
            this.pbOverall.Location = new System.Drawing.Point(15, 105);
            this.pbOverall.Name = "pbOverall";
            this.pbOverall.Size = new System.Drawing.Size(299, 20);
            this.pbOverall.TabIndex = 3;
            this.pbOverall.Visible = false;
            // 
            // lblFileStatus
            // 
            this.lblFileStatus.AutoEllipsis = true;
            this.lblFileStatus.Location = new System.Drawing.Point(12, 128);
            this.lblFileStatus.Name = "lblFileStatus";
            this.lblFileStatus.Size = new System.Drawing.Size(302, 13);
            this.lblFileStatus.TabIndex = 2;
            this.lblFileStatus.Text = "...";
            this.lblFileStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblFileStatus.Visible = false;
            // 
            // pbFile
            // 
            this.pbFile.Location = new System.Drawing.Point(15, 144);
            this.pbFile.Name = "pbFile";
            this.pbFile.Size = new System.Drawing.Size(299, 20);
            this.pbFile.TabIndex = 4;
            this.pbFile.Visible = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 181);
            this.Controls.Add(this.pbFile);
            this.Controls.Add(this.pbOverall);
            this.Controls.Add(this.lblFileStatus);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "Word/Publisher/PDF to Compressed PDF";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ProgressBar pbOverall;
        private System.Windows.Forms.Label lblFileStatus;
        private System.Windows.Forms.ProgressBar pbFile;
    }
}

