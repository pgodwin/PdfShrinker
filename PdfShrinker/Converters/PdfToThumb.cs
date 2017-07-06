using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using DatajugV2.ReportCardImporter.Converters;
using Ghostscript.NET;
using Ghostscript.NET.Rasterizer;

namespace PdfShrinker.Converters
{
    public class PdfToThumb : IConverter, IDisposable
    {
        private GhostscriptVersionInfo _lastInstalledVersion = null;
        private GhostscriptRasterizer _rasterizer = null;

        public PdfToThumb()
        {

            _lastInstalledVersion =
            GhostscriptVersionInfo.GetLastInstalledVersion(
                    GhostscriptLicense.GPL | GhostscriptLicense.AFPL,
                    GhostscriptLicense.GPL);

            _rasterizer = new GhostscriptRasterizer();

        }

        public void Convert(string sourceFilename, string destinationFilename)
        {
            Trace.TraceInformation("Creating Thumb from PDF for {0}.", sourceFilename);
            int desired_x_dpi = 96;
            int desired_y_dpi = 96;


            _rasterizer.Open(sourceFilename, _lastInstalledVersion, false);
            if (_rasterizer.PageCount > 0)
            {
                Image page = _rasterizer.GetPage(desired_x_dpi, desired_y_dpi, 1);
                page.Save(destinationFilename, ImageFormat.Jpeg);
            }
            _rasterizer.Close();
            Trace.TraceInformation("Creating Thumb from PDF for {0} complete.", sourceFilename);
        }

        public void Dispose()
        {
            if (_rasterizer != null)
                _rasterizer.Dispose();
        }
    }
}
