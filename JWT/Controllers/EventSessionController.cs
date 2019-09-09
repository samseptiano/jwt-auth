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
using JWT.DataProvider;
namespace JWT.Controllers
{
    [Produces("application/json")]
    [ApiController]
    public class EventSessionController : Controller
    {
        private DBContext db = new DBContext();
        private ISurveyAnswerPesertaProvider surveyAnswerPesertaProvider;

        public EventSessionController(ISurveyAnswerPesertaProvider surveyAnswerPesertaProvider)
        {
            this.surveyAnswerPesertaProvider = surveyAnswerPesertaProvider;

        }

        [Route("api/[controller]")]
        // GET api/values
        [HttpGet]
        [Authorize]
        public ActionResult<List<EventSession>> Get()
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;
            List<EventSession> evtps = new List<EventSession> { };

            if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }

            if (spendingTimeWithCompany > 5)
            {
                evtps = db.EventSession.ToList();
                return Ok(new { evtps });

            }
            else
            {
                evtps = db.EventSession.ToList();
                return Ok(new { evtps });
            }
        }

        // GET api/values/1
        [Route("api/[controller]/{id}")]
        [HttpGet]
        [Authorize]
        //public EventSession Get(int id)
        public ActionResult<List<EventSession>> Get(int id)
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;
            //EventSession evtps = new EventSession();
            List<EventSession> evtps = new List<EventSession> { };

            if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }

            if (spendingTimeWithCompany > 5)
            {
                //evtps = db.EventSession.SingleOrDefault(x => x.EventID == id);
                evtps = db.EventSession.Where(x => x.EventID == id).ToList<EventSession>();
                if (evtps == null)
                {
                    //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                }
                return evtps;

            }
            else
            {
                //evtps = db.EventSession.SingleOrDefault(x => x.EventID == id);
                evtps = db.EventSession.Where(x => x.EventID == id).ToList<EventSession>();
                if (evtps == null)
                {
                    //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                }
                return evtps;
            }
        }

        // GET api/values/1
        [Route("api/[controller]/{id}/{empNIK}")]
        [HttpGet]
        [Authorize]
        //public EventSession Get(int id)
        //  public async Task Post([FromBody]List<SurveyAnswerPeserta> evts)
        public  async Task <IEnumerable<EventSessionSurvey>> Get(int id, string empNIK)
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;
            //EventSession evtps = new EventSession();
            List<EventSessionSurvey> evtps = new List<EventSessionSurvey> { };

            if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }

            if (spendingTimeWithCompany > 5)
            {
                //evtps = db.EventSession.SingleOrDefault(x => x.EventID == id);
                //evtps = db.EventSession.Where(x => x.EventID == id).ToList<EventSession>();
                return await this.surveyAnswerPesertaProvider.GetEventSessionSurvey(id, empNIK);
                //if (evtps == null)
                //{
                //    //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                //}
                //return evtps;

            }
            else
            {
                //evtps = db.EventSession.SingleOrDefault(x => x.EventID == id);
                //evtps = db.EventSession.Where(x => x.EventID == id).ToList<EventSession>();
                return await this.surveyAnswerPesertaProvider.GetEventSessionSurvey(id, empNIK);
                //if (evtps == null)
                //{
                //    //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                //}
                //return evtps;
            }
        }

        //// POST api/values
        [Route("api/[controller]")]
        [HttpPost]
        [Authorize]
        public void Post([FromBody]EventSession evts)
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;
            EventSession evtps = new EventSession();

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
                    db.EventSession.Add(evts);
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
                    db.EventSession.Add(evts);
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
        public EventSession Put(int id, [FromBody]EventSession evts)
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;
            EventPeserta evtps = new EventPeserta();

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
                var evt = db.EventSession.SingleOrDefault(x => x.ESID == id);

                //Might be user sends invalid id.
                if (evt == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                evt.EventID = evts.EventID;
                evt.SessionID = evts.SessionID;
                evt.SessionName = evts.SessionName;
                evt.SessionDateStart = evts.SessionDateStart;
                evt.SessionDateEnd = evts.SessionDateEnd;
                evt.SessionPlace = evts.SessionPlace;
                evt.InstructorID = evts.InstructorID;
                evt.SurveyID = evts.SurveyID;
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

                var evt = db.EventSession.SingleOrDefault(x => x.ESID == id);

                // Might be user sends invalid id.
                if (evt == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                evt.EventID = evts.EventID;
                evt.SessionID = evts.SessionID;
                evt.SessionName = evts.SessionName;
                evt.SessionDateStart = evts.SessionDateStart;
                evt.SessionDateEnd = evts.SessionDateEnd;
                evt.SessionPlace = evts.SessionPlace;
                evt.InstructorID = evts.InstructorID;
                evt.SurveyID = evts.SurveyID;
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
            EventSession evtps = new EventSession();


            if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }

            if (spendingTimeWithCompany > 5)
            {
                EventSession evt = db.EventSession.Find(id);
                if (evt == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                db.EventSession.Remove(evt);
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
                EventSession evt = db.EventSession.Find(id);
                if (evt == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                db.EventSession.Remove(evt);
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
