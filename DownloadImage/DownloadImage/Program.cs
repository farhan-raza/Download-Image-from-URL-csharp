using System;
using Aspose.Html;
using Aspose.Html.Dom;
using Aspose.Html.Net;

namespace DownloadImage
{
    class Program
    {
        static void Main(string[] args)
        {
            // You can get a FREE Evaluation License at https://purchase.aspose.com/temporary-license
            Aspose.Html.License license = new License();
            license.SetLicense("License.lic");

            // Set path to read or write data on the disk
            string dataDir = @"D:\Image Web Scraping\";

            // Specify path to read input file from local disk
            //using var doc = new HTMLDocument(dataDir + "Sample.html");

            // Specify the URL to Download Image from URL in C#
            using var doc = new HTMLDocument("YOUR URL GOES HERE");            
            var grabber = new ImageGrabber();
            var saver = new ImageSaver(dataDir);
            saver.Save(grabber.GrabFrom(doc));
        }
    }
}