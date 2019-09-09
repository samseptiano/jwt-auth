using JWT.Context;
using JWT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace JWT.Controllers
{
    [Produces("application/json")]
    [ApiController]
    public class SurveyContentController : Controller
    {
        //List<SurveyQuestion> surveyquestion = new List<SurveyQuestion>();
        //List<SurveyAnswer> surveyanswer = new List<SurveyAnswer>();  
        //List<QuestionType> questiontypes = new List<QuestionType>();

        //[Route("api/[controller]/{id}")]
        //// GET api/values
        //[HttpGet]
        //[Authorize]
        ////public ActionResult<List<SurveyContentView>> Get(int id)
        //public ActionResult<SurveyContentView> Get(int id)
        //{

        //    //var surveyContentView = from sq in surveyquestion
        //    //                        join sa in surveyanswer on sq.SurveyQuestionID equals sa.SurveyQuestionID into sa2
        //    //                        from sa in sa2.DefaultIfEmpty()
        //    //                        select new SurveyContentView { surveyquestionVm = sq, surveyanswerVm = sa };
        //    var surveyContentView = from sq in surveyquestion
        //                            join sa in surveyanswer on sq.SurveyQuestionID equals sa.SurveyQuestionID into sa2
        //                            from sa in sa2.DefaultIfEmpty()
        //                            select new SurveyContentView { surveyquestionVm = sq, surveyanswerVm = sa };


        //    //movies = movies.Where(s => s.Title.Contains(searchString));
        //    surveyContentView = surveyContentView.Where(s => s.surveyquestionVm.SurveyID == id);





        //    return View(surveyContentView);
        //}
    }
}
