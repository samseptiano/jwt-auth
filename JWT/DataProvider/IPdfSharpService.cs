using JWT.Models;

namespace JWT.DataProvider
{
    public interface IPdfSharpService
    {
        string CreatePdf(PdfData pdfData);
    }
}
