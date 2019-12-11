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
    //Provider untuk data suply data dari store procedure PK
    public class UserListProvider : IUserListProvider
    {
        private readonly IConfiguration _config;
        public UserListProvider(IConfiguration config)
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
        public async Task<IEnumerable<UserListPA>> GetUserList(string posId, string tahun)
        {
            using (var sqlConnection = Connection)
            {               
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@APR_EMPNIK", posId);
                dynamicParameters.Add("@TAHUN", tahun);

                return await sqlConnection.QueryAsync<UserListPA>("spGetListEmpPA", dynamicParameters, commandType: CommandType.StoredProcedure);

                //@EventID int,  @EmpNIK VARCHAR(25), @SurveyID  int, @QuestionID  int, @AnswerID int, @AnswerEssay varchar(1000), @LastUpdateBy varchar(50)'
            }
        }


        public async Task<IEnumerable<PA_Org>> GetEmpOrg(String posId, String tahun)
        {
            using (var sqlConnection = Connection)
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@APR_EMPNIK", posId);
                dynamicParameters.Add("@TAHUN", tahun);
                return await sqlConnection.QueryAsync<PA_Org>("spGetListEmpOrg", dynamicParameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<UserList>> GetUserListPJ(string APR_EMPNIK)
        {
            using (var sqlConnection = Connection)
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@APR_EMPNIK", APR_EMPNIK);
                return await sqlConnection.QueryAsync<UserList>("spGetUserListDataPJ", dynamicParameters, commandType: CommandType.StoredProcedure);

                //@EventID int,  @EmpNIK VARCHAR(25), @SurveyID  int, @QuestionID  int, @AnswerID int, @AnswerEssay varchar(1000), @LastUpdateBy varchar(50)'
            }
        }

        public async Task<IEnumerable<UserList>> GetUserListPJOne(string empNIK)
        {
            using (var sqlConnection = Connection)
            {                
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@empNIK", empNIK);
                return await sqlConnection.QueryAsync<UserList>("spGetUserListDataPJOne", dynamicParameters, commandType: CommandType.StoredProcedure);

                //@EventID int,  @EmpNIK VARCHAR(25), @SurveyID  int, @QuestionID  int, @AnswerID int, @AnswerEssay varchar(1000), @LastUpdateBy varchar(50)'
            }
        }

        //public async Task UpdateSurveyAnswerPeserta(SurveyAnswerPeserta surveyAnswerPeserta)
        //{
        //    using (var sqlConnection = Connection)
        //    {
        //        await sqlConnection.OpenAsync();
        //        var dynamicParameters = new DynamicParameters();
        //        dynamicParameters.Add("@EventID", surveyAnswerPeserta.EventID);
        //        dynamicParameters.Add("@EmpNIK", surveyAnswerPeserta.EmpNIK);
        //        dynamicParameters.Add("@SessionID", surveyAnswerPeserta.SessionID);
        //        dynamicParameters.Add("@SurveyID", surveyAnswerPeserta.SurveyID);
        //        dynamicParameters.Add("@QuestionID", surveyAnswerPeserta.QuestionID);
        //        dynamicParameters.Add("@AnswerID", surveyAnswerPeserta.AnswerID);
        //        dynamicParameters.Add("@AnswerEssay", surveyAnswerPeserta.AnswerEssay);
        //        dynamicParameters.Add("@LastUpdateBy", surveyAnswerPeserta.LastUpdateBy);




        //        await sqlConnection.ExecuteAsync("spUpdateSurveyAnswerPeserta", dynamicParameters, commandType: CommandType.StoredProcedure);

        //        //@EventID int,  @EmpNIK VARCHAR(25), @SurveyID  int, @QuestionID  int, @AnswerID int, @AnswerEssay varchar(1000), @LastUpdateBy varchar(50)'
        //    }
        //}

        //public async Task UpdateStatusSurveyPeserta(int eventID, string empNIK, int sessionID )
        //{
        //    using (var sqlConnection = Connection)
        //    {
        //        await sqlConnection.OpenAsync();
        //        var dynamicParameters = new DynamicParameters();
        //        dynamicParameters.Add("@EventID", eventID);
        //        dynamicParameters.Add("@EmpNIK", empNIK);
        //        dynamicParameters.Add("@SessionID", sessionID);



        //        await sqlConnection.ExecuteAsync("spUpdateStatusSurveyPeserta", dynamicParameters, commandType: CommandType.StoredProcedure);

        //        //@EventID int,  @EmpNIK VARCHAR(25), @SurveyID  int, @QuestionID  int, @AnswerID int, @AnswerEssay varchar(1000), @LastUpdateBy varchar(50)'
        //    }
        //}

        //public async Task UpdateStatusAbsenPeserta(int eventID, string empNIK, string LastUpdateBy)
        //{
        //    using (var sqlConnection = Connection)
        //    {
        //        await sqlConnection.OpenAsync();
        //        var dynamicParameters = new DynamicParameters();
        //        dynamicParameters.Add("@EventID", eventID);
        //        dynamicParameters.Add("@EmpNIK", empNIK);
        //        dynamicParameters.Add("@LastUpdateBy", LastUpdateBy);

        //        await sqlConnection.ExecuteAsync("spUpdateStatusAbsenPeserta", dynamicParameters, commandType: CommandType.StoredProcedure);

        //        //@EventID int,  @EmpNIK VARCHAR(25), @SurveyID  int, @QuestionID  int, @AnswerID int, @AnswerEssay varchar(1000), @LastUpdateBy varchar(50)'
        //    }
        //}

        // untuk tampilkan data karyawan PA

        //public async Task<IEnumerable<UserList>> GetUserListPJ(string posId)
        //{
        //    using (var sqlConnection = Connection)
        //    {        
        //        await sqlConnection.OpenAsync();
        //        var dynamicParameters = new DynamicParameters();
        //        dynamicParameters.Add("@positionId", posId);

        //        return await sqlConnection.QueryAsync<UserList>("spGetUserListDataPJ", dynamicParameters, commandType: CommandType.StoredProcedure);

        //        //@EventID int,  @EmpNIK VARCHAR(25), @SurveyID  int, @QuestionID  int, @AnswerID int, @AnswerEssay varchar(1000), @LastUpdateBy varchar(50)'
        //    }
        //}

        //public Task<IEnumerable<SurveyContentView>> GetSurveyContentViews()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
