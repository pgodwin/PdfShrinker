using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatajugV2.ReportCardImporter.Converters
{
    interface IConverter
    {
        /// <summary>
        /// Convert the specified file to the IConveter type...
        /// </summary>
        /// <param name="sourceFilename"></param>
        /// <param name="destinationFilename"></param>
        void Convert(string sourceFilename, string destinationFilename);
    }
}
