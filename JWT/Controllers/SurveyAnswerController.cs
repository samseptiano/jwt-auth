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
    public class SurveyAnswerController : Controller
    {
        private DBContext db = new DBContext();


        [Route("api/[controller]")]
        // GET api/values
        [HttpGet]
        [Authorize]
        public ActionResult<List<SurveyAnswer>> Get()
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;
            List<SurveyAnswer> evtps = new List<SurveyAnswer> { };

            if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }

            if (spendingTimeWithCompany > 5)
            {
                evtps = db.SurveyAnswer.ToList();
                return Ok(new { evtps });

            }
            else
            {
                evtps = db.SurveyAnswer.ToList();
                return Ok(new { evtps });
            }
        }

        // GET api/values/1
        [Route("api/[controller]/{questionId}")]
        [HttpGet]
        [Authorize]
        public List<SurveyAnswer> Get(int questionId)
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;
            List<SurveyAnswer> evtps = new List<SurveyAnswer> { };

            if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }

            if (spendingTimeWithCompany > 5)
            {
                //evtps = db.SurveyAnswer.Find(id);
                evtps = db.SurveyAnswer.Where(x => x.SurveyQuestionID == questionId && x.FGActiveYN=="Y").ToList<SurveyAnswer>();
                if (evtps == null)
                {
                    //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                }
                return evtps;

            }
            else
            {
                //evtps = db.SurveyAnswer.Find(id);
                evtps = db.SurveyAnswer.Where(x => x.SurveyQuestionID == questionId && x.FGActiveYN == "Y").ToList<SurveyAnswer>();
                if (evtps == null)
                {
                    //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                }
                return evtps;
            }
        }

        // GET api/values/1
        [Route("api/[controller]/{questionId},{answerId}")]
        [HttpGet]
        [Authorize]
        public SurveyAnswer Get(int questionId, int answerId)
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;
            SurveyAnswer evtps = new SurveyAnswer();

            if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }

            if (spendingTimeWithCompany > 5)
            {

                evtps = db.SurveyAnswer.Find(answerId);
                if (evtps == null)
                {
                    //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                }
                return evtps;

            }
            else
            {
                evtps = db.SurveyAnswer.Find(answerId);
                if (evtps == null)
                {
                    //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                }
                return evtps;
            }
        }

        //// POST api/values
        [Route("api/[controller]")]
        [HttpPost]
        [Authorize]
        public void Post([FromBody]SurveyAnswer evts)
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;
            SurveyAnswer evtps = new SurveyAnswer();

            if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }

            if (spendingTimeWithCompany > 5)
            {

                if (!ModelState.IsValid)
                {
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
                }
                else
                {
                    db.SurveyAnswer.Add(evts);
                    db.SaveChanges();
                    //throw new HttpResponseException(HttpStatusCode.Created);
                }
    // return user;
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
                }
                else
                {
                    db.SurveyAnswer.Add(evts);
                    db.SaveChanges();
                    //throw new HttpResponseException(HttpStatusCode.Created);
                }
                //return user;
            }
        }
            
        
        //public async Task<ActionResult<User>> PostUser(int userId, string username, string email, string password, string empNIK)
        //{
        //    User usr = new User();

        //    usr.UserId = userId;
        //    usr.Username = username;
        //    usr.Email = email;
        //    usr.Password = password;
        //    usr.EmpNIK = empNIK;

        //    db.user.Add(usr);
        //    await db.SaveChangesAsync();
        //    return CreatedAtAction("PostUser", new { id = usr.UserId }, usr);
        //}

        //update customer  
        [Route("api/[controller]/{id}")]
        [HttpPut]
        [Authorize]
        public SurveyAnswer Put(int id, [FromBody]SurveyAnswer evts)
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;
            SurveyAnswer evtps = new SurveyAnswer();

            if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }

            if (spendingTimeWithCompany > 5)
            {
                if (!ModelState.IsValid)
                {
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
                }
                
                //tes
                var evt = db.SurveyAnswer.SingleOrDefault(x => x.SurveyAnswerID == id);

                //Might be user sends invalid id.
                if (evt == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }



                evt.SurveyQuestionID = evts.SurveyQuestionID;
                evt.Answer = evts.Answer;
                evt.FGActiveYN = evts.FGActiveYN;
                evt.LastUpdate = DateTime.Today;
                evt.LastUpdateBy = evts.LastUpdateBy;
               
                db.SaveChanges();
                return evt;
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
                }

                var evt = db.SurveyAnswer.SingleOrDefault(x => x.SurveyAnswerID == id);

                // Might be user sends invalid id.
                if (evt == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                evt.SurveyQuestionID = evts.SurveyQuestionID;
                evt.Answer = evts.Answer;
                evt.FGActiveYN = evts.FGActiveYN;
                evt.LastUpdate = DateTime.Today;
                evt.LastUpdateBy = evts.LastUpdateBy;

                db.SaveChanges();
                return evt;
            }
        }

        //delete user by id 
        [Route("api/[controller]/{id}")]
        [HttpDelete]
        [Authorize]
        public void Delete(int id)
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;
            SurveyAnswer evtps = new SurveyAnswer();


            if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }

            if (spendingTimeWithCompany > 5)
            {
                SurveyAnswer evt = db.SurveyAnswer.Find(id);
                if (evt == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                db.SurveyAnswer.Remove(evt);
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound); ;
                }
                //throw new HttpResponseException(HttpStatusCode.OK); ;
            }
            else
            {
                SurveyAnswer evt = db.SurveyAnswer.Find(id);
                if (evt == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                db.SurveyAnswer.Remove(evt);
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound); ;
                }
                //throw new HttpResponseException(HttpStatusCode.OK); ;
            }

        }

    }
}
