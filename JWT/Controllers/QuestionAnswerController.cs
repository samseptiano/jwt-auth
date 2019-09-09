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
    public class QuestionAnswerController : Controller
    {
        private DBContext db = new DBContext();


        [Route("api/[controller]/{surveyID},{questionID}")]
        // GET api/values
        [HttpGet]
        [Authorize]
        public ActionResult<List<QuestionAnswer>> Get(int surveyID, int questionID)
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;
            SurveyQuestion questions = new SurveyQuestion ();
            //List<QuestionAnswer> evtps = new List<QuestionAnswer> { };
            QuestionAnswer questionAnswer = new QuestionAnswer();

            if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }

            if (spendingTimeWithCompany > 5)
            {
                //evtps = db.QuestionAnswer.ToList();

                questions = db.SurveyQuestion.Find(questionID);

                questionAnswer.SurveyID = questions.SurveyID;
                questionAnswer.SurveyQuestionID = questions.SurveyQuestionID;
                questionAnswer.Question = questions.Question;
                questionAnswer.QuestionType = questions.QuestionType;
                questionAnswer.QuestionCategory = questions.QuestionCategory;
                questionAnswer.QuestionSeq = questions.QuestionSeq;
                questionAnswer.FGActiveYN = questions.FGActiveYN;
                questionAnswer.LastUpdate = questions.LastUpdate;
                questionAnswer.LastUpdateBy = questions.LastUpdateBy;
              
                questionAnswer.surveyAnswers = db.SurveyAnswer.Where(x => x.SurveyQuestionID == questions.SurveyQuestionID && x.FGActiveYN == "Y").ToList<SurveyAnswer>();
                
                return Ok(new { questionAnswer });

            }
            else
            {

                //return Ok(new { evtps });

                questions = db.SurveyQuestion.Find(questionID);

                questionAnswer.SurveyID = questions.SurveyID;
                questionAnswer.SurveyQuestionID = questions.SurveyQuestionID;
                questionAnswer.Question = questions.Question;
                questionAnswer.QuestionType = questions.QuestionType;
                questionAnswer.QuestionCategory = questions.QuestionCategory;
                questionAnswer.QuestionSeq = questions.QuestionSeq;
                questionAnswer.FGActiveYN = questions.FGActiveYN;
                questionAnswer.LastUpdate = questions.LastUpdate;
                questionAnswer.LastUpdateBy = questions.LastUpdateBy;

                questionAnswer.surveyAnswers = db.SurveyAnswer.Where(x => x.SurveyQuestionID == questions.SurveyQuestionID && x.FGActiveYN == "Y").ToList<SurveyAnswer>();

                //questionAnswer.surveyAnswers = new List<SurveyAnswer> {  }
                return Ok(new { questionAnswer });
            }
        }

        [Route("api/[controller]/{surveyID}")]
        // GET api/values
        [HttpGet]
        [Authorize]
        public ActionResult<List<QuestionAnswer>> Get(int surveyID)
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;
            List<SurveyQuestion> questions = new List<SurveyQuestion> { };
            QuestionAnswer questionAnswer = new QuestionAnswer();
            List<QuestionAnswer> questionAnswers = new List<QuestionAnswer> { };
     

            if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }

            if (spendingTimeWithCompany > 5)
            {
                //evtps = db.QuestionAnswer.ToList();

                //evtps = db.SurveyQuestion.Where(x =>  x.SurveyID == surveyID).ToList<SurveyQuestion>();

                questions = db.SurveyQuestion.Where(x => x.SurveyID == surveyID).ToList<SurveyQuestion>();

                //SurveyQuestion question = new SurveyQuestion();
                //QuestionAnswer questionAnswer = new QuestionAnswer();

                foreach (SurveyQuestion question in questions)
                {

                    //questionAnswers.Add(db.QuestionAnswer.Find(question.SurveyQuestionID));

                    //questionAnswer.SurveyID = question.SurveyID;
                    //questionAnswer.SurveyQuestionID = question.SurveyQuestionID;
                    //questionAnswer.Question = question.Question;
                    //questionAnswer.QuestionType = question.QuestionType;
                    //questionAnswer.QuestionCategory = question.QuestionCategory;
                    //questionAnswer.QuestionSeq = question.QuestionSeq;
                    //questionAnswer.FGActiveYN = question.FGActiveYN;
                    //questionAnswer.LastUpdate = question.LastUpdate;
                    //questionAnswer.LastUpdateBy = question.LastUpdateBy;

                    //questionAnswer.surveyAnswers = db.SurveyAnswer.Where(x => x.SurveyQuestionID == question.SurveyQuestionID && x.FGActiveYN == "Y").ToList<SurveyAnswer>();

                    //questionAnswers.Add(questionAnswer);

                    questionAnswers.Add(new QuestionAnswer() {
                        SurveyID = question.SurveyID,
                        SurveyQuestionID = question.SurveyQuestionID,
                        Question = question.Question,
                        QuestionType = question.QuestionType,
                        QuestionCategory = question.QuestionCategory,
                        QuestionSeq = question.QuestionSeq,
                        FGActiveYN = question.FGActiveYN,
                        LastUpdate = question.LastUpdate,
                        LastUpdateBy = question.LastUpdateBy,

                        surveyAnswers = db.SurveyAnswer.Where(x => x.SurveyQuestionID == question.SurveyQuestionID && x.FGActiveYN == "Y").ToList<SurveyAnswer>()
                    });


            }

                return Ok(new { questionAnswers });

            }
            else
            {

                //return Ok(new { evtps });

                //questions = db.SurveyQuestion.Find(questionID);

                //questionAnswer.SurveyID = questions.SurveyID;
                //questionAnswer.SurveyQuestionID = questions.SurveyQuestionID;
                //questionAnswer.Question = questions.Question;
                //questionAnswer.QuestionType = questions.QuestionType;
                //questionAnswer.QuestionCategory = questions.QuestionCategory;
                //questionAnswer.QuestionSeq = questions.QuestionSeq;
                //questionAnswer.FGActiveYN = questions.FGActiveYN;
                //questionAnswer.LastUpdate = questions.LastUpdate;
                //questionAnswer.LastUpdateBy = questions.LastUpdateBy;

                //questionAnswer.surveyAnswers = db.SurveyAnswer.Where(x => x.SurveyQuestionID == questions.SurveyQuestionID && x.FGActiveYN == "Y").ToList<SurveyAnswer>();

                //questionAnswer.surveyAnswers = new List<SurveyAnswer> {  }

                questions = db.SurveyQuestion.Where(x => x.SurveyID == surveyID).ToList<SurveyQuestion>();

                //SurveyQuestion question = new SurveyQuestion();
                //QuestionAnswer questionAnswer = new QuestionAnswer();

                foreach (SurveyQuestion question in questions)
                {

                    //questionAnswers.Add(db.QuestionAnswer.Find(question.SurveyQuestionID));

                    //questionAnswer.SurveyID = question.SurveyID;
                    //questionAnswer.SurveyQuestionID = question.SurveyQuestionID;
                    //questionAnswer.Question = question.Question;
                    //questionAnswer.QuestionType = question.QuestionType;
                    //questionAnswer.QuestionCategory = question.QuestionCategory;
                    //questionAnswer.QuestionSeq = question.QuestionSeq;
                    //questionAnswer.FGActiveYN = question.FGActiveYN;
                    //questionAnswer.LastUpdate = question.LastUpdate;
                    //questionAnswer.LastUpdateBy = question.LastUpdateBy;

                    //questionAnswer.surveyAnswers = db.SurveyAnswer.Where(x => x.SurveyQuestionID == question.SurveyQuestionID && x.FGActiveYN == "Y").ToList<SurveyAnswer>();

                    //questionAnswers.Add(questionAnswer);

                    questionAnswers.Add(new QuestionAnswer()
                    {
                        SurveyID = question.SurveyID,
                        SurveyQuestionID = question.SurveyQuestionID,
                        Question = question.Question,
                        QuestionType = question.QuestionType,
                        QuestionCategory = question.QuestionCategory,
                        QuestionSeq = question.QuestionSeq,
                        FGActiveYN = question.FGActiveYN,
                        LastUpdate = question.LastUpdate,
                        LastUpdateBy = question.LastUpdateBy,

                        surveyAnswers = db.SurveyAnswer.Where(x => x.SurveyQuestionID == question.SurveyQuestionID && x.FGActiveYN == "Y").ToList<SurveyAnswer>()
                    });
                }



                return Ok(new { questionAnswers });
            }
        }
        //// GET api/values/1
        //[Route("api/[controller]/{id}")]
        //[HttpGet]
        //[Authorize]
        //public QuestionAnswer Get(int id)
        //{
        //    var currentUser = HttpContext.User;
        //    int spendingTimeWithCompany = 0;
        //    QuestionAnswer evtps = new QuestionAnswer();

        //    if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
        //    {
        //        DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
        //        spendingTimeWithCompany = DateTime.Today.Year - date.Year;
        //    }

        //    if (spendingTimeWithCompany > 5)
        //    {
        //        evtps = db.QuestionAnswer.Find(id);

        //        if (evtps == null)
        //        {
        //            //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
        //        }
        //        return evtps;

        //    }
        //    else
        //    {
        //        evtps = db.QuestionAnswer.Find(id);
        //        if (evtps == null)
        //        {
        //            //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
        //        }
        //        return evtps;
        //    }
        //}

        //    //// POST api/values
        //    [Route("api/[controller]")]
        //    [HttpPost]
        //    [Authorize]
        //    public void Post([FromBody]SurveyQuestion evts)
        //    {
        //        var currentUser = HttpContext.User;
        //        int spendingTimeWithCompany = 0;
        //        SurveyQuestion evtps = new SurveyQuestion();

        //        if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
        //        {
        //            DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
        //            spendingTimeWithCompany = DateTime.Today.Year - date.Year;
        //        }

        //        if (spendingTimeWithCompany > 5)
        //        {

        //            if (!ModelState.IsValid)
        //            {
        //                throw new HttpResponseException(HttpStatusCode.BadRequest);
        //            }
        //            else
        //            {
        //                db.SurveyQuestion.Add(evts);
        //                db.SaveChanges();
        //                //throw new HttpResponseException(HttpStatusCode.Created);
        //            }
        //// return user;
        //        }
        //        else
        //        {
        //            if (!ModelState.IsValid)
        //            {
        //                throw new HttpResponseException(HttpStatusCode.BadRequest);
        //            }
        //            else
        //            {
        //                db.SurveyQuestion.Add(evts);
        //                db.SaveChanges();
        //                //throw new HttpResponseException(HttpStatusCode.Created);
        //            }
        //            //return user;
        //        }
        //    }


        //    //public async Task<ActionResult<User>> PostUser(int userId, string username, string email, string password, string empNIK)
        //    //{
        //    //    User usr = new User();

        //    //    usr.UserId = userId;
        //    //    usr.Username = username;
        //    //    usr.Email = email;
        //    //    usr.Password = password;
        //    //    usr.EmpNIK = empNIK;

        //    //    db.user.Add(usr);
        //    //    await db.SaveChangesAsync();
        //    //    return CreatedAtAction("PostUser", new { id = usr.UserId }, usr);
        //    //}

        //    //update customer  
        //    [Route("api/[controller]/{id}")]
        //    [HttpPut]
        //    [Authorize]
        //    public SurveyQuestion Put(int id, [FromBody]SurveyQuestion evts)
        //    {
        //        var currentUser = HttpContext.User;
        //        int spendingTimeWithCompany = 0;
        //        SurveyQuestion evtps = new SurveyQuestion();

        //        if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
        //        {
        //            DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
        //            spendingTimeWithCompany = DateTime.Today.Year - date.Year;
        //        }

        //        if (spendingTimeWithCompany > 5)
        //        {
        //            if (!ModelState.IsValid)
        //            {
        //                throw new HttpResponseException(HttpStatusCode.BadRequest);
        //            }

        //            //tes
        //            var evt = db.SurveyQuestion.SingleOrDefault(x => x.SurveyQuestionID == id);

        //            //Might be user sends invalid id.
        //            if (evt == null)
        //            {
        //                throw new HttpResponseException(HttpStatusCode.NotFound);
        //            }


        //            evt.SurveyID = evts.SurveyID;
        //            evt.Question = evts.Question;
        //            evt.QuestionType = evts.QuestionType;
        //            evt.QuestionCategory = evts.QuestionCategory;
        //            evt.FGActiveYN = evts.FGActiveYN;
        //            evt.LastUpdate = DateTime.Today;
        //            evt.LastUpdateBy = evts.LastUpdateBy;



        //            db.SaveChanges();
        //            return evt;
        //        }
        //        else
        //        {
        //            if (!ModelState.IsValid)
        //            {
        //                throw new HttpResponseException(HttpStatusCode.BadRequest);
        //            }

        //            var evt = db.SurveyQuestion.SingleOrDefault(x => x.SurveyQuestionID == id);

        //            // Might be user sends invalid id.
        //            if (evt == null)
        //            {
        //                throw new HttpResponseException(HttpStatusCode.NotFound);
        //            }

        //            evt.SurveyID = evts.SurveyID;
        //            evt.Question = evts.Question;
        //            evt.QuestionType = evts.QuestionType;
        //            evt.QuestionCategory = evts.QuestionCategory;
        //            evt.FGActiveYN = evts.FGActiveYN;
        //            evt.LastUpdate = DateTime.Today;
        //            evt.LastUpdateBy = evts.LastUpdateBy;

        //            db.SaveChanges();
        //            return evt;
        //        }
        //    }

        //    //delete user by id 
        //    [Route("api/[controller]/{id}")]
        //    [HttpDelete]
        //    [Authorize]
        //    public void Delete(int id)
        //    {
        //        var currentUser = HttpContext.User;
        //        int spendingTimeWithCompany = 0;
        //        SurveyQuestion evtps = new SurveyQuestion();


        //        if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
        //        {
        //            DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
        //            spendingTimeWithCompany = DateTime.Today.Year - date.Year;
        //        }

        //        if (spendingTimeWithCompany > 5)
        //        {
        //            SurveyQuestion evt = db.SurveyQuestion.Find(id);
        //            if (evt == null)
        //            {
        //                throw new HttpResponseException(HttpStatusCode.NotFound);
        //            }
        //            db.SurveyQuestion.Remove(evt);
        //            try
        //            {
        //                db.SaveChanges();
        //            }
        //            catch (DbUpdateConcurrencyException ex)
        //            {
        //                throw new HttpResponseException(HttpStatusCode.NotFound); ;
        //            }
        //            //throw new HttpResponseException(HttpStatusCode.OK); ;
        //        }
        //        else
        //        {
        //            SurveyQuestion evt = db.SurveyQuestion.Find(id);
        //            if (evt == null)
        //            {
        //                throw new HttpResponseException(HttpStatusCode.NotFound);
        //            }
        //            db.SurveyQuestion.Remove(evt);
        //            try
        //            {
        //                db.SaveChanges();
        //            }
        //            catch (DbUpdateConcurrencyException ex)
        //            {
        //                throw new HttpResponseException(HttpStatusCode.NotFound); ;
        //            }
        //            //throw new HttpResponseException(HttpStatusCode.OK); ;
        //        }

        //    }

    }
}
