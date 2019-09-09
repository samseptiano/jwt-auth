using Dapper;
using JWT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.DataProvider
{
    public class GeneralDataProvider : IGeneralDataProvider
    {
        private readonly IConfiguration _config;
        public GeneralDataProvider(IConfiguration config)
        {
            _config = config;
        }
        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            }
        }
        //public FileStreamResult GetImageFile(int ID)
        //{
        //    using (var sqlConnection = Connection)
        //    {

        //        //await sqlConnection.OpenAsync();
        //        sqlConnection.Open();
        //        var dynamicParameters = new DynamicParameters();
        //        dynamicParameters.Add("@ID", ID);

        //        //FileModel result = await sqlConnection.QueryAsync<FileModel>("spGetImageFile", dynamicParameters, commandType: CommandType.StoredProcedure);
        //        FileModel result = sqlConnection.Query<FileModel>("spGetImageFile", dynamicParameters, commandType: CommandType.StoredProcedure).First();
        //        Stream stream = new MemoryStream(result.FileData);
        //        return new FileStreamResult(stream, "image/png");
        //    }
        //}
        public async Task<IEnumerable<FileModel>> GetFile(int ID)
        {
            using (var sqlConnection = Connection)
            {

                await sqlConnection.OpenAsync();
                //sqlConnection.Open();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@ID", ID);

                //FileModel result = await sqlConnection.QueryAsync<FileModel>("spGetImageFile", dynamicParameters, commandType: CommandType.StoredProcedure);
                //FileModel result = sqlConnection.Query<FileModel>("spGetImageFile", dynamicParameters, commandType: CommandType.StoredProcedure).First();
                return await sqlConnection.QueryAsync<FileModel>("spGetImageFile", dynamicParameters, commandType: CommandType.StoredProcedure);
                //Stream stream = new MemoryStream(result.FileData);
                //return new FileStreamResult(stream, "image/png");
            }
        }
    }
}
