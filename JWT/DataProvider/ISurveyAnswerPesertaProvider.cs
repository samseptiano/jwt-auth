using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWT.Models;


namespace JWT.DataProvider
{
    public interface ISurveyAnswerPesertaProvider
    {
        Task UpdateSurveyAnswerPeserta(SurveyAnswerPeserta surveyAnswerPeserta);
        Task UpdateStatusSurveyPeserta(int eventID, string empNIK, int sessionID);
        Task UpdateStatusAbsenPeserta(int eventID, string empNIK, string LastUpdateBy);
        Task <IEnumerable<EventSessionSurvey>> GetEventSessionSurvey(int eventID, string empNIK);
       
    }
}
