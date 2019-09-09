using JWT.Context;
using JWT.DataProvider;
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
    public class EventPesertaAbsenController : Controller
    {
        private DBContext db = new DBContext();
        private ISurveyAnswerPesertaProvider surveyAnswerPesertaProvider;

        public EventPesertaAbsenController(ISurveyAnswerPesertaProvider surveyAnswerPesertaProvider)
        {
            this.surveyAnswerPesertaProvider = surveyAnswerPesertaProvider;

        }

        [Route("api/[controller]")]
        // GET api/values
        [HttpGet]
        [Authorize]
        public ActionResult<List<EventPesertaAbsen>> Get()
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;
            List<EventPesertaAbsen> evtps = new List<EventPesertaAbsen> { };

            if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }

            if (spendingTimeWithCompany > 5)
            {
                evtps = db.EventPesertaAbsen.ToList();
                return Ok(new { evtps });

            }
            else
            {
                evtps = db.EventPesertaAbsen.ToList();
                return Ok(new { evtps });
            }
        }

        // GET api/values/1
        [Route("api/[controller]/{id}")]
        [HttpGet]
        [Authorize]
        //public EventPesertaAbsen Get(int id, string empNIK)
        public ActionResult<List<EventPesertaAbsen>> Get(int id)
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;
            List<EventPesertaAbsen> evtps = new List<EventPesertaAbsen> { };

            if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }

            if (spendingTimeWithCompany > 5)
            {
                //evtps = db.EventPesertaAbsen.Find(id);
                //evtps = db.EventPesertaAbsen.SingleOrDefault(x => x.EventID == id);
                evtps = db.EventPesertaAbsen.Where(x => x.EventID == id ).ToList<EventPesertaAbsen>();

                if (evtps == null)
                {
                    //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                }
                return evtps;

            }
            else
            {
                evtps = db.EventPesertaAbsen.Where(x => x.EventID == id ).ToList<EventPesertaAbsen>();
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
        //public EventPesertaAbsen Get(int id, string empNIK)
        public ActionResult<List<EventPesertaAbsen>> Get(int id, string empNIK)
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;
            List<EventPesertaAbsen> evtps = new List<EventPesertaAbsen> { };

            if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }

            if (spendingTimeWithCompany > 5)
            {
                //evtps = db.EventPesertaAbsen.Find(id);
                //evtps = db.EventPesertaAbsen.SingleOrDefault(x => x.EventID == id);

                evtps = db.EventPesertaAbsen.Where(x => x.EventID == id &&  x.EmpNIK == empNIK).ToList<EventPesertaAbsen>();

                if (evtps == null)
                {
                    //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                }
                return evtps;

            }
            else
            {
                evtps = db.EventPesertaAbsen.Where(x => x.EventID == id && x.EmpNIK == empNIK).ToList<EventPesertaAbsen>();
                if (evtps == null)
                {
                    //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                }
                return evtps;
            }
        }


        // GET api/values/1
        [Route("api/[controller]/{empNIK}")]
        [HttpGet]
        [Authorize]
        //public EventPesertaAbsen Get(int id, string empNIK)
        public ActionResult<List<AbsenEvent>> Get(string empNIK)
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;
            List<EventPesertaAbsen> evtps = new List<EventPesertaAbsen> { };


            if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }

            if (spendingTimeWithCompany > 5)
            {
                //evtps = db.EventPesertaAbsen.Find(id);
                //evtps = db.EventPesertaAbsen.SingleOrDefault(x => x.EventID == id);


                var evt = (from p in db.EventPesertaAbsen
                           join e in db.Event on p.EventID equals e.EventID
                           where p.EmpNIK == empNIK
                           select new AbsenEvent { EventID = p.EventID, EventName = e.EventName, EmpNIK = p.EmpNIK, EventDate = p.EventDate, EventType=e.EventType }).ToList<AbsenEvent>();
                //evtps = db.EventPesertaAbsen.Where(x => x.EmpNIK == empNIK).ToList<EventPesertaAbsen>();


                if (evt == null)
                {
                    //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                }
                return evt;

            }
            else
            {
                var evt = (from p in db.EventPesertaAbsen
                           join e in db.Event on p.EventID equals e.EventID
                           where p.EmpNIK == empNIK
                           select new AbsenEvent { EventID = p.EventID, EventName = e.EventName, EmpNIK = p.EmpNIK, EventDate = p.EventDate, EventType = e.EventType }).ToList<AbsenEvent>();
                if (evt == null)
                {
                    //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                }
                return evt;
            }
        }

        //// POST api/values
        [Route("api/[controller]")]
        [HttpPost]
        [Authorize]
        public async Task Post([FromBody]EventPesertaAbsen evts)
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;
            //EventPesertaAbsen evtps = new EventPesertaAbsen();

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
                    //db.EventPesertaAbsen.Add(evts);
                    //db.SaveChanges();
                    //throw new HttpResponseException(HttpStatusCode.Created);
                    await this.surveyAnswerPesertaProvider.UpdateStatusAbsenPeserta(evts.EventID, evts.EmpNIK, evts.LastUpdateBy);
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
                    //db.EventPesertaAbsen.Add(evts);
                    //db.SaveChanges();
                    //throw new HttpResponseException(HttpStatusCode.Created);
                    await this.surveyAnswerPesertaProvider.UpdateStatusAbsenPeserta(evts.EventID, evts.EmpNIK, evts.LastUpdateBy);
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
        public EventPesertaAbsen Put(int id, [FromBody]EventPesertaAbsen evts)
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;
            EventPesertaAbsen evtps = new EventPesertaAbsen();

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
                var evt = db.EventPesertaAbsen.SingleOrDefault(x => x.EAID == id);

                //Might be user sends invalid id.
                if (evt == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                //StatusHadirYN
                evt.EventID = evts.EventID;
                evt.EmpNIK = evts.EmpNIK;
                evt.EventDate = evts.EventDate;
                evt.StatusHadirYN = evts.StatusHadirYN;
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

                var evt = db.EventPesertaAbsen.SingleOrDefault(x => x.EventID == id);

                // Might be user sends invalid id.
                if (evt == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                evt.EventID = evts.EventID;
                evt.EmpNIK = evts.EmpNIK;
                evt.EventDate = evts.EventDate;
                evt.StatusHadirYN = evts.StatusHadirYN;
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
            EventPesertaAbsen evtps = new EventPesertaAbsen();


            if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }

            if (spendingTimeWithCompany > 5)
            {
                EventPesertaAbsen evt = db.EventPesertaAbsen.Find(id);
              
                if (evt == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                db.EventPesertaAbsen.Remove(evt);
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
                EventPesertaAbsen evt = db.EventPesertaAbsen.Find(id);
                if (evt == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                db.EventPesertaAbsen.Remove(evt);
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
