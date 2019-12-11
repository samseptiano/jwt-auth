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

        public Task<IEnumerable<PK_Dashboard>> GetDashboards(string APREMPNIK)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PK_FileEvidences>> GetFileEvidences(string transID, string KPINO)
        {
            throw new NotImplementedException();
        }

        //public async Task<IEnumerable<PK_ViewTransHeader>> GetTransHeaders (string empNIK)
        //{
        //    using (var sqlConnection = Connection)
        //    {
        //        await sqlConnection.OpenAsync();
        //        var dynamicParameters = new DynamicParameters();
        //        dynamicParameters.Add("@EMPNIK", empNIK);
        //        return await sqlConnection.QueryAsync<PK_ViewTransHeader>("sp_pk_ViewTransHeader", dynamicParameters, commandType: CommandType.StoredProcedure);
        //    }
        //}

        public async Task<IEnumerable<PA_ViewTransDetail>> GetTransDetails(string empNIK,String tahun)
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

        public Task<IEnumerable<PK_ViewTransDetail>> GetTransDetails(string transID)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PK_ViewTransGrade>> GetTransGrades(string transID)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PK_ViewTransHeader>> GetTransHeaders(string empNIK)
        {
            throw new NotImplementedException();
        }

        public Task PK_DeleteFileEvidence(string TRANSID, string KPINO, string FILENAME)
        {
            throw new NotImplementedException();
        }

        public Task PK_UpdateFileEvidence(string TRANSID, string KPINO, string FILENAME, string FILESTREAM, string FILEPATH, string FILE_TYPE, string USEREMPNIK)
        {
            throw new NotImplementedException();
        }

        public Task PK_UpdateStatus(string TRANSID, string APREMPNIK, string STATUS, string USEREMPNIK)
        {
            throw new NotImplementedException();
        }

        public Task PK_UpdateTransDetail(string TRANSID, string KPINO, string GRADESCORE, string EVIDENCE, string KPICATEGORY, string USEREMPNIK)
        {
            throw new NotImplementedException();
        }

        public Task PK_UpdateTransHeader(string TRANSID, string USEREMPNIK)
        {
            throw new NotImplementedException();
        }

        //public async Task<IEnumerable<PA_FileEvidence>> GetFileEvidences(string TRANSID,string KPINO)
        //{
        //    using (var sqlConnection = Connection)
        //    {
        //        await sqlConnection.OpenAsync();
        //        var dynamicParameters = new DynamicParameters();                
        //        dynamicParameters.Add("@TRANSID", TRANSID);
        //        dynamicParameters.Add("@KPINO", KPINO);
        //        return await sqlConnection.QueryAsync<PA_FileEvidence>("sp_pk_ViewFileEvidence", dynamicParameters, commandType: CommandType.StoredProcedure);
        //    }
        //}


        //public async Task<IEnumerable<PK_ViewTransGrade>> GetTransGrades(string transID) //string kpiID,
        //{
        //    using (var sqlConnection = Connection)
        //    {
        //        await sqlConnection.OpenAsync();
        //        var dynamicParameters = new DynamicParameters();
        //        //dynamicParameters.Add("@KPIID", "7");
        //        //dynamicParameters.Add("@KPICATEGORY", "Kuantitatif");
        //        //dynamicParameters.Add("@KPIID", kpiID);                
        //        dynamicParameters.Add("@TRANSID", transID);
        //        return await sqlConnection.QueryAsync<PK_ViewTransGrade>("sp_pk_ViewTransGrade", dynamicParameters, commandType: CommandType.StoredProcedure);
        //    }
        //}

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

        //public async Task PK_UpdateTransDetail(string TRANSID, string KPINO, string GRADESCORE, string EVIDENCE, string KPICATEGORY,string USEREMPNIK)
        //{
        //    using (var sqlConnection = Connection)
        //    {
        //        await sqlConnection.OpenAsync();
        //        var dynamicParameters = new DynamicParameters();
        //        dynamicParameters.Add("@TRANSID", TRANSID);                
        //        dynamicParameters.Add("@KPINO", KPINO);
        //        dynamicParameters.Add("@GRADESCORE", GRADESCORE);
        //        dynamicParameters.Add("@EVIDENCE", EVIDENCE);
        //        dynamicParameters.Add("@KPICATEGORY", KPICATEGORY);
        //        dynamicParameters.Add("@USEREMPNIK", USEREMPNIK);
        //        await sqlConnection.ExecuteAsync("sp_pk_UpdateTransDetail", dynamicParameters, commandType: CommandType.StoredProcedure);
        //    }
        //}

        //public async Task PK_UpdateTransHeader(string TRANSID,string USEREMPNIK)
        //{
        //    using (var sqlConnection = Connection)
        //    {
        //        await sqlConnection.OpenAsync();
        //        var dynamicParameters = new DynamicParameters();
        //        dynamicParameters.Add("@TRANSID", TRANSID);
        //        dynamicParameters.Add("@USEREMPNIK", USEREMPNIK);
        //        await sqlConnection.ExecuteAsync("sp_pk_UpdateTransHeader", dynamicParameters, commandType: CommandType.StoredProcedure);
        //    }
        //}

        //public async Task PK_UpdateFileEvidence(string TRANSID, string KPINO,  string FILENAME , string FILESTREAM , string FILEPATH, string FILETYPE, string USEREMPNIK)
        //{
        //    using (var sqlConnection = Connection)
        //    {
        //        await sqlConnection.OpenAsync();
        //        var dynamicParameters = new DynamicParameters();
        //        dynamicParameters.Add("@TRANSID", TRANSID);
        //        dynamicParameters.Add("@KPINO", KPINO);                
        //        dynamicParameters.Add("@FILENAME", FILENAME);
        //        dynamicParameters.Add("@FILESTREAM", FILESTREAM);
        //        dynamicParameters.Add("@FILEPATH", FILEPATH);
        //        dynamicParameters.Add("@FILETYPE", FILETYPE);
        //        dynamicParameters.Add("@USEREMPNIK", USEREMPNIK);
        //        await sqlConnection.ExecuteAsync("sp_pk_UpdateFileEvidence", dynamicParameters, commandType: CommandType.StoredProcedure);
        //    }            
        //}


        //public async Task PK_DeleteFileEvidence(string TRANSID, string KPINO, string FILENAME)
        //{
        //    using (var sqlConnection = Connection)
        //    {
        //        await sqlConnection.OpenAsync();
        //        var dynamicParameters = new DynamicParameters();
        //        dynamicParameters.Add("@TRANSID", TRANSID);
        //        dynamicParameters.Add("@KPINO", KPINO);
        //        dynamicParameters.Add("@FILENAME", FILENAME);
        //        await sqlConnection.ExecuteAsync("sp_pk_DeleteFileEvidence", dynamicParameters, commandType: CommandType.StoredProcedure);
        //    }
        //}



        //public async Task PK_UpdateStatus(string TRANSID , string APREMPNIK, string STATUS,string USEREMPNIK)
        //{
        //    using (var sqlConnection = Connection)
        //    {
        //        await sqlConnection.OpenAsync();
        //        var dynamicParameters = new DynamicParameters();
        //        dynamicParameters.Add("@TRANSID", TRANSID);
        //        dynamicParameters.Add("@APREMPNIK", APREMPNIK);
        //        dynamicParameters.Add("@STATUS", STATUS);
        //        dynamicParameters.Add("@USEREMPNIK", USEREMPNIK);
        //        await sqlConnection.ExecuteAsync("sp_pk_UpdateStatus", dynamicParameters, commandType: CommandType.StoredProcedure);
        //    }
        //}



    }
}
