using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Publisher;

namespace DatajugV2.ReportCardImporter.Converters
{
    public class PublisherToPdf : IConverter
    {
        
        private Microsoft.Office.Interop.Publisher.Application _appPub;

        public PublisherToPdf()
        {
            _appPub = new Microsoft.Office.Interop.Publisher.Application();
        }

        public void Convert(string sourceFilename, string destinationFilename)
        {
            Trace.TraceInformation("Convert Pub file {0} to pdf.", sourceFilename);
            var publisherFile = _appPub.Open(sourceFilename, ReadOnly: true);
            publisherFile.ExportAsFixedFormat(PbFixedFormatType.pbFixedFormatTypePDF, destinationFilename);
            publisherFile.Close();

            Trace.TraceInformation("Convert Pub file {0} complete.", sourceFilename);
        }


        public void Dispose()
        {
            if (_appPub != null)
            {
                _appPub.Quit();
                _appPub = null;
                GC.Collect();
            }
        }
    }
}
