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
    public class SurveyAnswerPesertaProvider : ISurveyAnswerPesertaProvider
    {
        private readonly IConfiguration _config;
        public SurveyAnswerPesertaProvider(IConfiguration config)
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

        public async Task UpdateSurveyAnswerPeserta(SurveyAnswerPeserta surveyAnswerPeserta)
        {
            using (var sqlConnection = Connection)
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@EventID", surveyAnswerPeserta.EventID);
                dynamicParameters.Add("@EmpNIK", surveyAnswerPeserta.EmpNIK);
                dynamicParameters.Add("@SessionID", surveyAnswerPeserta.SessionID);
                dynamicParameters.Add("@SurveyID", surveyAnswerPeserta.SurveyID);
                dynamicParameters.Add("@QuestionID", surveyAnswerPeserta.QuestionID);
                dynamicParameters.Add("@AnswerID", surveyAnswerPeserta.AnswerID);
                dynamicParameters.Add("@AnswerEssay", surveyAnswerPeserta.AnswerEssay);
                dynamicParameters.Add("@LastUpdateBy", surveyAnswerPeserta.LastUpdateBy);
                await sqlConnection.ExecuteAsync("spUpdateSurveyAnswerPeserta", dynamicParameters, commandType: CommandType.StoredProcedure);

                //@EventID int,  @EmpNIK VARCHAR(25), @SurveyID  int, @QuestionID  int, @AnswerID int, @AnswerEssay varchar(1000), @LastUpdateBy varchar(50)'
            }
        }

        public async Task UpdateStatusSurveyPeserta(int eventID, string empNIK, int sessionID )
        {
            using (var sqlConnection = Connection)
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@EventID", eventID);
                dynamicParameters.Add("@EmpNIK", empNIK);
                dynamicParameters.Add("@SessionID", sessionID);                
                await sqlConnection.ExecuteAsync("spUpdateStatusSurveyPeserta", dynamicParameters, commandType: CommandType.StoredProcedure);

                //@EventID int,  @EmpNIK VARCHAR(25), @SurveyID  int, @QuestionID  int, @AnswerID int, @AnswerEssay varchar(1000), @LastUpdateBy varchar(50)'
            }
        }

        public async Task UpdateStatusAbsenPeserta(int eventID, string empNIK, string LastUpdateBy)
        {
            using (var sqlConnection = Connection)
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@EventID", eventID);
                dynamicParameters.Add("@EmpNIK", empNIK);
                dynamicParameters.Add("@LastUpdateBy", LastUpdateBy);
                await sqlConnection.ExecuteAsync("spUpdateStatusAbsenPeserta", dynamicParameters, commandType: CommandType.StoredProcedure);

                //@EventID int,  @EmpNIK VARCHAR(25), @SurveyID  int, @QuestionID  int, @AnswerID int, @AnswerEssay varchar(1000), @LastUpdateBy varchar(50)'
            }
        }

        public async Task<IEnumerable<EventSessionSurvey>> GetEventSessionSurvey(int eventID, string empNIK)
        {
            using (var sqlConnection = Connection)
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@EventID", eventID);
                dynamicParameters.Add("@EmpNIK", empNIK);

                return await sqlConnection.QueryAsync<EventSessionSurvey>("spGetEventSessionSurvey", dynamicParameters, commandType: CommandType.StoredProcedure);

                //@EventID int,  @EmpNIK VARCHAR(25), @SurveyID  int, @QuestionID  int, @AnswerID int, @AnswerEssay varchar(1000), @LastUpdateBy varchar(50)'
            }
        }


        //public Task<IEnumerable<SurveyContentView>> GetSurveyContentViews()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
