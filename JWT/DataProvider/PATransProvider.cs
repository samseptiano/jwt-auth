using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWT.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace JWT.DataProvider
{
    public class PATransProvider:IPATransProvider
    {
        private readonly IConfiguration _config;
        public PATransProvider(IConfiguration config)
        {
            _config = config;
        }

        //public IDbConnection Connection
        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public async Task<IEnumerable<PA_ViewTransHeader>> GetTransHeaders(string empNIK, string tahun)
        {
            using (var sqlConnection = Connection)
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@EMPNIK", empNIK);
                dynamicParameters.Add("@YEAR", tahun);
                return await sqlConnection.QueryAsync<PA_ViewTransHeader>("sp_pa_ViewTransHeader", dynamicParameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<PA_ViewTransDetail>> GetTransDetails(string empNIK,string tahun)
        {
            using (var sqlConnection = Connection)
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@EMPNIK", empNIK);
                dynamicParameters.Add("@YEAR", tahun);
                return await sqlConnection.QueryAsync<PA_ViewTransDetail>("sp_pa_ViewTransDetail", dynamicParameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<PA_FileEvidence>> GetFileEvidences(string TRANSID, string KPINO, string KPITYPE, string SEMESTER)
        {
            using (var sqlConnection = Connection)
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@PAID", TRANSID);
                dynamicParameters.Add("@KPIID", KPINO);
                dynamicParameters.Add("@KPITYPE", KPITYPE);
                dynamicParameters.Add("@SEMESTER", SEMESTER);
                return await sqlConnection.QueryAsync<PA_FileEvidence>("sp_pa_ViewFileEvidence", dynamicParameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<PA_TextEvidence>> GetTextEvidences(string TRANSID, string KPINO, string KPITYPE, string SEMESTER)
        {
            using (var sqlConnection = Connection)
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@PAID", TRANSID);
                dynamicParameters.Add("@KPIID", KPINO);
                dynamicParameters.Add("@KPITYPE", KPITYPE);
                dynamicParameters.Add("@SEMESTER", SEMESTER);
                return await sqlConnection.QueryAsync<PA_TextEvidence>("sp_pa_ViewTextEvidence", dynamicParameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<PA_MDevPlan>> GetMDevPlan()
        {
            using (var sqlConnection = Connection)
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();            
                return await sqlConnection.QueryAsync<PA_MDevPlan>("sp_pa_GetMDevPlan", dynamicParameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<PA_DevPlanHeader>> GetDevPlanHeader(String PAID, String COMPID)
        {
            using (var sqlConnection = Connection)
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@PAID", PAID);
                dynamicParameters.Add("@COMPID", COMPID);
                return await sqlConnection.QueryAsync<PA_DevPlanHeader>("sp_pa_ViewDevPlanH", dynamicParameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task postDevPlanHeader(String PAID, String COMPID, String EMPNIK)
        {
            using (var sqlConnection = Connection)
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@PAID", PAID);
                dynamicParameters.Add("@COMPID", COMPID);
                dynamicParameters.Add("@EMPNIK", EMPNIK);
                await sqlConnection.QueryAsync<PA_DevPlanHeader>("sp_pa_UpdateDevPlanH", dynamicParameters, commandType: CommandType.StoredProcedure);
            }
        }



        public async Task<IEnumerable<PA_DevPlanDetail>> GetDevPlanDetail(String DEVID, String PAID)
        {
            using (var sqlConnection = Connection)
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@DEVID", DEVID);
                dynamicParameters.Add("@PAID", PAID);
                return await sqlConnection.QueryAsync<PA_DevPlanDetail>("sp_pa_ViewDevPlanD", dynamicParameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task deleteDevPlan(String PAID)
        {
            using (var sqlConnection = Connection)
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@PAID", PAID);
                await sqlConnection.QueryAsync<PA_DevPlanDetail>("sp_pa_DeleteDevPlanH", dynamicParameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task postDevPlanDetail(String DEVID, String DEVACTIVITIES, String DEVKPI, String DEVDUEDATE, String DEVMENTOR, String DEVACHIEVEMENT, String DEVSTATUS, String EMPNIK, String DEVPLANMETHODID, String PAID)
        {
            using (var sqlConnection = Connection)
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@DEVPLANACTIVITIES", DEVACTIVITIES);
                dynamicParameters.Add("@DEVID", DEVID);
                dynamicParameters.Add("@DEVPLANKPI", DEVKPI);
                dynamicParameters.Add("@DEVPLANDUEDATE", DEVDUEDATE);
                dynamicParameters.Add("@DEVPLANMENTOR", DEVMENTOR);
                dynamicParameters.Add("@DEVPLANACHIEVEMENT", DEVACHIEVEMENT);
                dynamicParameters.Add("@DEVPLANSTATUS", DEVSTATUS);
                dynamicParameters.Add("@EMPNIK", EMPNIK);
                dynamicParameters.Add("@DEVPLANMETHODID", DEVPLANMETHODID);
                dynamicParameters.Add("@PAID", PAID);
                await sqlConnection.QueryAsync<PA_DevPlanDetail>("sp_pa_UpdateDevPlanD", dynamicParameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task hitungPA(String PAID)
        {
            using (var sqlConnection = Connection)
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@PAID", PAID);
                await sqlConnection.QueryAsync<PA_DevPlanDetail>("sp_pa_HitungPA", dynamicParameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<PA_ViewTransGrade>> GetTransGrades(string kpiID, string kpiType) //string kpiID,
        {
            using (var sqlConnection = Connection)
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                //dynamicParameters.Add("@KPIID", "7");
                //dynamicParameters.Add("@KPICATEGORY", "Kuantitatif");
                //dynamicParameters.Add("@KPIID", kpiID);                
                dynamicParameters.Add("@KPIID", kpiID);
                dynamicParameters.Add("@KPITYPE", kpiType);
                return await sqlConnection.QueryAsync<PA_ViewTransGrade>("sp_pa_ViewKPIGrade", dynamicParameters, commandType: CommandType.StoredProcedure);
            }
        }


        //public async Task<IEnumerable<PK_Dashboard>> GetDashboards(string APREMPNIK)
        //{
        //    using (var sqlConnection = Connection)
        //    {
        //        await sqlConnection.OpenAsync();
        //        var dynamicParameters = new DynamicParameters();
        //        dynamicParameters.Add("@APR_EMPNIK", APREMPNIK);
        //        return await sqlConnection.QueryAsync<PK_Dashboard>("sp_pk_ViewDashboard", dynamicParameters, commandType: CommandType.StoredProcedure);

        //    }
        //}

        public async Task PA_UpdateTransDetail(string PAID, string KPIID, string COMPID, string CP, string EVIDENCES,string ACTUAL, string TARGET, string SEMESTER, string KPITYPE, string EMPNIK)
        {
            using (var sqlConnection = Connection)
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@PAID", PAID);
                dynamicParameters.Add("@KPIID", KPIID);
                dynamicParameters.Add("@COMPID", COMPID);
                dynamicParameters.Add("@CP", CP);
                dynamicParameters.Add("@EVIDENCES", EVIDENCES);
                dynamicParameters.Add("@ACTUAL", ACTUAL);
                dynamicParameters.Add("@TARGET", TARGET);
                dynamicParameters.Add("@SEMESTER", SEMESTER);
                dynamicParameters.Add("@KPITYPE", KPITYPE);
                dynamicParameters.Add("@EMPNIK", EMPNIK);
                await sqlConnection.ExecuteAsync("sp_pa_UpdateTransDetail", dynamicParameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task PA_UpdateTransHeader(string STRENGTH, string PAID, string NIKBAWAHAN, string STATUS)
        {
            using (var sqlConnection = Connection)
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@STRENGTH", STRENGTH);
                dynamicParameters.Add("@PAID", PAID);
                dynamicParameters.Add("@NIKBAWAHAN", NIKBAWAHAN);
                dynamicParameters.Add("@STATUS", STATUS);
                await sqlConnection.ExecuteAsync("sp_pa_UpdateTransHeader", dynamicParameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task PA_UpdateFileEvidence(string PAID, string KPIID, string COMPID, string SEMESTER, string FILESTRING, string FILEEXT, string FILENAME, string EMPNIK)
        {
            using (var sqlConnection = Connection)
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@PAID", PAID);
                dynamicParameters.Add("@KPIID", KPIID);
                dynamicParameters.Add("@COMPID", COMPID);
                dynamicParameters.Add("@SEMESTER", SEMESTER);
                dynamicParameters.Add("@FILESTRING", FILESTRING);
                dynamicParameters.Add("@FILEEXT", FILEEXT);
                dynamicParameters.Add("@FILENAME", FILENAME);
                dynamicParameters.Add("@EMPNIK", EMPNIK);
                await sqlConnection.ExecuteAsync("sp_pa_UpdateFileEvidence", dynamicParameters, commandType: CommandType.StoredProcedure);
            }
        }


        public async Task PA_DeleteFileEvidence(string PAID, string KPIID, string COMPID,string SEMESTER,string FILENAME, string FILESTRING)
        {
            using (var sqlConnection = Connection)
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@PAID", PAID);
                dynamicParameters.Add("@KPIID", KPIID);
                dynamicParameters.Add("@COMPID", COMPID);
                dynamicParameters.Add("@SEMESTER", SEMESTER);
                dynamicParameters.Add("@FILESTRING", FILESTRING);
                dynamicParameters.Add("@FILENAME", FILENAME);
                await sqlConnection.ExecuteAsync("sp_pa_DeleteFileEvidence", dynamicParameters, commandType: CommandType.StoredProcedure);
            }
        }


        public async Task PA_UpdateTransHeaderStatus(string STRENGTH, string PAID, string NIKBAWAHAN, string STATUS)
        {
            using (var sqlConnection = Connection)
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@STRENGTH", STRENGTH);
                dynamicParameters.Add("@PAID", PAID);
                dynamicParameters.Add("@NIKBAWAHAN", NIKBAWAHAN);
                dynamicParameters.Add("@STATUS", STATUS);
                await sqlConnection.ExecuteAsync("sp_pa_UpdatePAStatus", dynamicParameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task PA_UpdateStatus(string STRENGTH, string PAID, string NIKBAWAHAN, string STATUS)
        {
            using (var sqlConnection = Connection)
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@STRENGTH", STRENGTH);
                dynamicParameters.Add("@PAID", PAID);
                dynamicParameters.Add("@NIKBAWAHAN", NIKBAWAHAN);
                dynamicParameters.Add("@STATUS", STATUS);

                await sqlConnection.ExecuteAsync("sp_pa_UpdatePAStatus", dynamicParameters, commandType: CommandType.StoredProcedure);
            }
        }



    }
}
