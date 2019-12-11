using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWT.Models;

namespace JWT.DataProvider
{
    //Jika menggunakan Store Procedure harus menggunakan Interface
    public interface IPATransProvider
    {
        Task<IEnumerable<PA_ViewTransHeader>> GetTransHeaders(string empNIK, string tahun);
        Task<IEnumerable<PA_ViewTransDetail>> GetTransDetails(string empNIK,string tahun);
        Task<IEnumerable<PA_FileEvidence>> GetFileEvidences(string paId, string kpiId, string kpiType, string semester);
        Task<IEnumerable<PA_TextEvidence>> GetTextEvidences(string paId, string kpiId, string kpiType, string semester);
        Task<IEnumerable<PA_ViewTransGrade>> GetTransGrades(string kpiID, string kpiType);
        Task<IEnumerable<PA_MDevPlan>> GetMDevPlan();
        Task<IEnumerable<PA_DevPlanHeader>> GetDevPlanHeader(String PAID, String COMPID);
        Task postDevPlanHeader(String PAID, String COMPID, String EMPNIK);
        Task deleteDevPlan(String PAID);
        Task hitungPA(String PAID);
        Task postDevPlanDetail(String DEVID, String DEVACTIVITIES, String DEVKPI, String DEVDUEDATE, String DEVMENTOR, String DEVACHIEVEMENT, String DEVSTATUS, String EMPNIK, String DEVPLANMETHODID, String PAID);

        Task<IEnumerable<PA_DevPlanDetail>> GetDevPlanDetail(String DEVID, String PAID);
        //Task<IEnumerable<PK_Dashboard>> GetDashboards(string APREMPNIK);
        Task PA_UpdateTransDetail(string PAID, string KPIID, string COMPID, string CP, string EVIDENCES, string ACTUAL, string TARGET, string SEMESTER, string KPITYPE, string EMPNIK);
        Task PA_UpdateTransHeader(string STRENGTH, string PAID, string NIKBAWAHAN, string STATUS);
        Task PA_UpdateStatus(string STRENGTH, string PAID, string NIKBAWAHAN, string STATUS);
        Task PA_UpdateFileEvidence(string PAID, string KPIID, string COMPID, string SEMESTER, string FILESTRING, string FILEEXT, string FILENAME, string EMPNIK);
        Task PA_DeleteFileEvidence(string PAID, string KPIID, string COMPID, string SEMESTER, string FILENAME, string FILESTRING);

    }
}
