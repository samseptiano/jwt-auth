using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWT.Models;

namespace JWT.DataProvider
{
    //Jika menggunakan Store Procedure harus menggunakan Interface
    public interface IPKTransProvider
    {
        Task<IEnumerable<PK_ViewTransHeader>> GetTransHeaders(string empNIK);
        Task<IEnumerable<PK_ViewTransDetail>> GetTransDetails(string transID);
        Task<IEnumerable<PK_FileEvidence>> GetFileEvidences(string transID, string KPINO);
        Task<IEnumerable<PK_ViewTransGrade>> GetTransGrades(string transID); 
        Task<IEnumerable<PK_Dashboard>> GetDashboards(string APREMPNIK);
        Task PK_UpdateTransDetail(string TRANSID, string KPINO, string GRADESCORE, string EVIDENCE,string KPICATEGORY, string USEREMPNIK);
        Task PK_UpdateTransHeader(string TRANSID,string USEREMPNIK);
        Task PK_UpdateStatus(string TRANSID, string APREMPNIK, string STATUS,string USEREMPNIK);
        Task PK_UpdateFileEvidence(string TRANSID,string KPINO, string FILENAME, string FILESTREAM, string FILEPATH, string FILE_TYPE, string USEREMPNIK);              
    }
}
