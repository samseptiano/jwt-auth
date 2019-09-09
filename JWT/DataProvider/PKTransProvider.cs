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
    public class PKTransProvider:IPKTransProvider
    {
        private readonly IConfiguration _config;
        public PKTransProvider(IConfiguration config)
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

        public async Task<IEnumerable<PK_ViewTransHeader>> GetTransHeaders (string empNIK)
        {
            using (var sqlConnection = Connection)
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@EMPNIK", empNIK);
                return await sqlConnection.QueryAsync<PK_ViewTransHeader>("sp_pk_ViewTransHeader", dynamicParameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<PK_ViewTransDetail>> GetTransDetails(string transID)
        {
            using (var sqlConnection = Connection)
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@TRANSID", transID);
                return await sqlConnection.QueryAsync<PK_ViewTransDetail>("sp_pk_ViewTransDetail", dynamicParameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<PK_FileEvidence>> GetFileEvidences(string TRANSID,string KPINO)
        {
            using (var sqlConnection = Connection)
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();                
                dynamicParameters.Add("@TRANSID", TRANSID);
                dynamicParameters.Add("@KPINO", KPINO);
                return await sqlConnection.QueryAsync<PK_FileEvidence>("sp_pk_ViewFileEvidence", dynamicParameters, commandType: CommandType.StoredProcedure);
            }
        }


        public async Task<IEnumerable<PK_ViewTransGrade>> GetTransGrades(string transID) //string kpiID,
        {
            using (var sqlConnection = Connection)
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                //dynamicParameters.Add("@KPIID", "7");
                //dynamicParameters.Add("@KPICATEGORY", "Kuantitatif");
                //dynamicParameters.Add("@KPIID", kpiID);                
                dynamicParameters.Add("@TRANSID", transID);
                return await sqlConnection.QueryAsync<PK_ViewTransGrade>("sp_pk_ViewTransGrade", dynamicParameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<PK_Dashboard>> GetDashboards(string APREMPNIK)
        {
            using (var sqlConnection = Connection)
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@APR_EMPNIK", APREMPNIK);
                return await sqlConnection.QueryAsync<PK_Dashboard>("sp_pk_ViewDashboard", dynamicParameters, commandType: CommandType.StoredProcedure);

            }
        }

        public async Task PK_UpdateTransDetail(string TRANSID, string KPINO, string GRADESCORE, string EVIDENCE, string KPICATEGORY,string USEREMPNIK)
        {
            using (var sqlConnection = Connection)
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@TRANSID", TRANSID);                
                dynamicParameters.Add("@KPINO", KPINO);
                dynamicParameters.Add("@GRADESCORE", GRADESCORE);
                dynamicParameters.Add("@EVIDENCE", EVIDENCE);
                dynamicParameters.Add("@KPICATEGORY", KPICATEGORY);
                dynamicParameters.Add("@USEREMPNIK", USEREMPNIK);
                await sqlConnection.ExecuteAsync("sp_pk_UpdateTransDetail", dynamicParameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task PK_UpdateTransHeader(string TRANSID,string USEREMPNIK)
        {
            using (var sqlConnection = Connection)
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@TRANSID", TRANSID);
                dynamicParameters.Add("@USEREMPNIK", USEREMPNIK);
                await sqlConnection.ExecuteAsync("sp_pk_UpdateTransHeader", dynamicParameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task PK_UpdateFileEvidence(string TRANSID, string KPINO,  string FILENAME , string FILESTREAM , string FILEPATH, string FILETYPE, string USEREMPNIK)
        {
            using (var sqlConnection = Connection)
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@TRANSID", TRANSID);
                dynamicParameters.Add("@KPINO", KPINO);                
                dynamicParameters.Add("@FILENAME", FILENAME);
                dynamicParameters.Add("@FILESTREAM", FILESTREAM);
                dynamicParameters.Add("@FILEPATH", FILEPATH);
                dynamicParameters.Add("@FILETYPE", FILETYPE);
                dynamicParameters.Add("@USEREMPNIK", USEREMPNIK);
                await sqlConnection.ExecuteAsync("sp_pk_UpdateFileEvidence", dynamicParameters, commandType: CommandType.StoredProcedure);
            }            
        }



        public async Task PK_UpdateStatus(string TRANSID , string APREMPNIK, string STATUS,string USEREMPNIK)
        {
            using (var sqlConnection = Connection)
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@TRANSID", TRANSID);
                dynamicParameters.Add("@APREMPNIK", APREMPNIK);
                dynamicParameters.Add("@STATUS", STATUS);
                dynamicParameters.Add("@USEREMPNIK", USEREMPNIK);
                await sqlConnection.ExecuteAsync("sp_pk_UpdateStatus", dynamicParameters, commandType: CommandType.StoredProcedure);
            }
        }

        

    }
}
