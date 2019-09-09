using JWT.Models;

namespace JWT.DataProvider
{
    public interface IMigraDocService
    {
        string CreateMigraDocPdf(PdfData pdfData);
    }
}
