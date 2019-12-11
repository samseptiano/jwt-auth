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
    //Provider untuk data user login
    public class VersionProvider : IVersionProvider
    {
        private readonly IConfiguration _config;
        public VersionProvider(IConfiguration config)
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

        public async Task<IEnumerable<Models.Version>> GetVersion()
        {
            using (var sqlConnection = Connection)
            {


                await sqlConnection.OpenAsync();
                return await sqlConnection.QueryAsync<Models.Version>("sp_pa_GetVersion", commandType: CommandType.StoredProcedure);

                //@EventID int,  @EmpNIK VARCHAR(25), @SurveyID  int, @QuestionID  int, @AnswerID int, @AnswerEssay varchar(1000), @LastUpdateBy varchar(50)'
            }
        }

    }
}
