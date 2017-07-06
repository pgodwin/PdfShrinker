using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Word;

namespace DatajugV2.ReportCardImporter.Converters
{
    public class WordToPdf : IConverter, IDisposable
    {
        private Microsoft.Office.Interop.Word.Application _appWord;

        public WordToPdf()
        {
            _appWord = new Microsoft.Office.Interop.Word.Application {Visible = false};
        }

        public void Convert(string sourceFilename, string destinationFilename)
        {
            Trace.TraceInformation("Convert Word file {0} to pdf.", sourceFilename);
            var wordDocument = _appWord.Documents.Open(sourceFilename, ReadOnly: true);
            wordDocument.ExportAsFixedFormat(destinationFilename, WdExportFormat.wdExportFormatPDF);
            wordDocument.Close(false);
            Trace.TraceInformation("Convert Word file {0} complete.", sourceFilename);
        }


        public void Dispose()
        {
            if (_appWord != null)
            {
                _appWord.Quit(false);
                _appWord = null;
                GC.Collect();
            }
        }
    }
}
