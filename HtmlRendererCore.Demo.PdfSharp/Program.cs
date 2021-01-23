using System;
using System.IO;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace HtmlRendererCore.Demo.PdfSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var html = @"
            <html>
                <body>
                    <p>Test html document.</p>
                </body>
            </html>
            ";

            Console.WriteLine(html);

            Console.WriteLine("Converting to PDF...");

            var pdf = ConvertHtmlToPdf(html);

            Console.WriteLine(pdf);
        }

        private static string ConvertHtmlToPdf(string html)
        {
            string res = null;

            using (var stream = new MemoryStream())
            {
                var pdf = PdfGenerator.GeneratePdf(html, PdfSharpCore.PageSize.A4);

                pdf.Save(stream);

                res = Convert.ToBase64String(stream.ToArray());
            }

            return res;
        }
    }
}
