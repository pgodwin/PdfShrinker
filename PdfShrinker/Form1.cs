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
using DatajugV2.ReportCardImporter.Converters;
using Ghostscript.NET;
using Ghostscript.NET.Processor;

namespace PdfShrinker
{
    public partial class frmMain : Form
    {
        private GhostscriptVersionInfo _gs_verssion_info;
           

        private WordToPdf _wordConverter;
        private PublisherToPdf _publisherConverter;

        private string _quality;

        public frmMain()
        {
            InitializeComponent();
            this.AllowDrop = true;
            this.DragEnter += frmMain_DragEnter;
            this.DragDrop += frmMain_DragDrop;

            this.comboBoxQuality.SelectedIndex = 0;
            this.comboBoxQuality_SelectedIndexChanged(this, null);
        }

        

        void frmMain_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            Task.Factory.StartNew(() => ProcessFiles(files));
        }

        void frmMain_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                _gs_verssion_info = GhostscriptVersionInfo.GetLastInstalledVersion(GhostscriptLicense.GPL | GhostscriptLicense.AFPL,
                    GhostscriptLicense.GPL);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not find Ghostscript on this machine. Please install Ghostscript and try again.");
                Application.Exit();
            }
        }

        private void ProcessFiles(string[] files)
        {
            var formats = new string[] {".docx", ".doc", ".pub", ".pubx"};
            UIThread(() =>
            {
                this.AllowDrop = false;
                btnBrowse.Enabled = false;
                this.UseWaitCursor = true;
                pbOverall.Maximum = files.Length;
                pbOverall.Value = 0;
                pbOverall.Visible = true;
                pbFile.Visible = true;
                lblStatus.Visible = true;
                lblFileStatus.Visible = true;
            });
            foreach (var file in files)
            {
                var sourceFile = file;
                
                var fileInfo = new FileInfo(sourceFile);

                var newFilename = Path.Combine(fileInfo.DirectoryName,
                    Path.GetFileNameWithoutExtension(sourceFile) + "-compressed.pdf");


                var counter = 0;
                // Make sure we don't override any existing files
                while (File.Exists(newFilename))
                {
                    counter++;
                    newFilename = Path.Combine(fileInfo.DirectoryName,
                        Path.GetFileNameWithoutExtension(sourceFile) + "-compressed" + counter + ".pdf");
                }


                UIThread(() => pbOverall.Value++);

                // Check if we need to convert the file
                if (formats.Contains(fileInfo.Extension))
                {
                    UIThread(() =>
                    {
                        lblStatus.Text = "Converting " + fileInfo.Name + " to PDF.";
                        lblFileStatus.Text = "...";
                        pbOverall.Style = ProgressBarStyle.Continuous;
                    });
                    sourceFile = ConvertToPdf(sourceFile);
                    UIThread(() => pbOverall.Style = ProgressBarStyle.Continuous);
                }

                UIThread(() => lblStatus.Text = "Compressing " + fileInfo.Name);
                CompressDocument(sourceFile, newFilename);
            }

            UIThread(() =>
            {
                this.AllowDrop = true;
                btnBrowse.Enabled = true;
                this.UseWaitCursor = false;
                pbOverall.Visible = false;
                pbFile.Visible = false;
                lblStatus.Visible = false;
                lblFileStatus.Visible = false;

                // MessageBox.Show("Conversion Complete");
            });
            
        }

        private string ConvertToPdf(string sourceFile)
        {
            var fileInfo = new FileInfo(sourceFile);
            var extension = fileInfo.Extension;
            var tempPath = Path.GetTempPath();
            var pdfPath = Path.Combine(tempPath, new Guid().ToString() + ".pdf");

            if (_wordConverter == null)
                _wordConverter = new WordToPdf();

            if (_publisherConverter == null)
                _publisherConverter = new PublisherToPdf();


            if (extension == ".doc" || extension == ".docx")
                _wordConverter.Convert(sourceFile, pdfPath);
            else if (extension == ".pub" || extension == ".pubx")
                _publisherConverter.Convert(sourceFile, pdfPath);

            return pdfPath;
        }

        private void CompressDocument(string path, string outpath)
        {
            

            // Sourced from https://gist.github.com/firstdoit/6390547
            List<string> gsArgs = new List<string>();

            
            gsArgs.Add("-empty");
            gsArgs.Add("-dSAFER");
            gsArgs.Add("-dBATCH");
            gsArgs.Add("-dNOPAUSE");
            gsArgs.Add("-dNOPROMPT");

            gsArgs.Add("-sDEVICE=pdfwrite");
            gsArgs.Add("-dCompatibilityLevel=1.4");
            // dPDFSETTINGS is basically our commpression option
            //  - /screen selects low-resolution output similar to the Acrobat Distiller "Screen Optimized" setting.
            //  - /ebook selects medium-resolution output similar to the Acrobat Distiller "eBook" setting.
            //  - /printer selects output similar to the Acrobat Distiller "Print Optimized" setting.
            //  - /prepress selects output similar to Acrobat Distiller "Prepress Optimized" setting.
            //  - /default selects output intended to be useful across a wide variety of uses, possibly at the expense of a larger output file.
            gsArgs.Add("-dPDFSETTINGS=/" + this._quality);


            gsArgs.Add("-sOutputFile=" + outpath + "");
            gsArgs.Add("-f");
            gsArgs.Add(path);



            using (GhostscriptProcessor processor = new GhostscriptProcessor(_gs_verssion_info, true))
            {

                processor.Processing += new GhostscriptProcessorProcessingEventHandler(processor_Processing);

                processor.Completed += processor_Completed;

                processor.StartProcessing(gsArgs.ToArray(), null);

                
            }

            
        }

        void processor_Completed(object sender, GhostscriptProcessorEventArgs e)
        {
            //UIThread(() => lblStatus.Text = "Compression Complete");
        }

        void processor_Processing(object sender, GhostscriptProcessorProcessingEventArgs e)
        {
            UIThread(() =>
            {
                lblFileStatus.Text = "Compressing " + e.CurrentPage.ToString() + " / " + e.TotalPages.ToString();
                pbFile.Maximum = e.TotalPages;
                pbFile.Value = e.CurrentPage;
            });
        }

        /// <summary>
        /// Executes the Action asynchronously on the UI thread, does not block execution on the calling thread.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="code"></param>
        public void UIThread(Action code)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(code);
            }
            else
            {
                code.Invoke();
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            var selection = new OpenFileDialog();
            selection.Filter = "All Compatible Files|*.pdf;*.doc;*.docx;*.pub;*.pubx";
            selection.Multiselect = true;
            if (selection.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var files = selection.FileNames;
                Task.Factory.StartNew(() => ProcessFiles(files));
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_wordConverter != null)
                _wordConverter.Dispose();
            if (_publisherConverter != null)
                _publisherConverter.Dispose();
        }

        private void comboBoxQuality_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._quality = comboBoxQuality.Text;
        }
    }
}
