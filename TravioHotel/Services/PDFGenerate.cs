using System.IO;
using iText.Html2pdf;
using iText.Kernel.Pdf;

namespace TravioHotel.Services
{
    public class PDFGenerate : IPDFInteface
    {
        public byte[] GeneratePDF(string HtmlContent)
        {
            using (var stream = new MemoryStream()) {
                var writter = new PdfWriter(stream);
                var pdf = new PdfDocument(writter);
                var convertor = new ConverterProperties().SetBaseUri(".");
                HtmlConverter.ConvertToPdf(HtmlContent, pdf, convertor);
                pdf.Close();
                return stream.ToArray();
            }
        }
    }
}
