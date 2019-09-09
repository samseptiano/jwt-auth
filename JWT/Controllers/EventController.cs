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
    public class EventController : Controller
    {
        private DBContext db = new DBContext();


        [Route("api/[controller]")]
        // GET api/values
        [HttpGet]
        [Authorize]
        public ActionResult<List<Event>> Get()
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;
            List<Event> events = new List<Event> { };

            if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }

            if (spendingTimeWithCompany > 5)
            {
                events = db.Event.ToList();
                return Ok(new { events });

            }
            else
            {
                events = db.Event.ToList();
                return Ok(new { events });
            }
        }

        // GET api/values/1
        [Route("api/[controller]/{empNIK}/{id}")]
        [HttpGet]
        [Authorize]
        public ActionResult<List<EventSurvey>> Get(string empNIK, int id)
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;
            Event events = new Event();

            if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }

            if (spendingTimeWithCompany > 5)
            {
                //events = db.Event.Find(id);
                //var evt = (from p in db.EventPeserta
                //           join e in db.Event on p.EventID equals e.EventID
                //           join s in db.SurveyPeserta on new { p.EventID, p.EmpNIK, SsnID = 0 } equals new { s.EventID, s.EmpNIK, SsnID = s.SessionID } into sp
                //           from m in sp.DefaultIfEmpty()
                //           where p.EmpNIK == empNIK && p.EventID==id
                //           select new EventSurvey { EventID = e.EventID, EventCode = e.EventCode, EventName = e.EventName, EventType = e.EventType, ExternalEventCode = e.ExternalEventCode, EventDesc = e.EventDesc, EventImage = e.EventImage, FGHasPasscodeYN = e.FGHasPasscodeYN, Passcode = e.Passcode, FGHasSurveyYN = e.FGHasSurveyYN, SurveyID = e.SurveyID, FGSurveyDoneYN = p.FGSurveyDoneYN, FGAllSurveyDoneYN = (m.FGSurveyDoneYN==null) ? "Y": m.FGSurveyDoneYN }).ToList<EventSurvey>();  // (m.MobileNo == null) ? "N/A" : m.MobileNo

                var evt = (from p in db.EventPeserta
                           join e in db.Event on p.EventID equals e.EventID
                           join q in db.EventPesertaAbsen on new { p.EventID, p.EmpNIK, AbsenDate = DateTime.Today.ToString("dd/MM/yyyy") } equals new { q.EventID, q.EmpNIK, AbsenDate = q.EventDate.ToString("dd/MM/yyyy") } into sq
                           join s in db.SurveyPeserta on new { p.EventID, p.EmpNIK, SsnID = 0 } equals new { s.EventID, s.EmpNIK, SsnID = s.SessionID } into sp
                           from m in sp.DefaultIfEmpty()
                           from r in sq.DefaultIfEmpty()
                           where p.EmpNIK == empNIK && p.EventID == id
                           select new EventSurvey { EventID = e.EventID, EventCode = e.EventCode, EventName = e.EventName, EventType = e.EventType, ExternalEventCode = e.ExternalEventCode, EventDesc = (e.EventDesc == null) ? "" : e.EventDesc, EventImage = (e.EventImage == null) ? "" : e.EventImage, FGHasPasscodeYN = e.FGHasPasscodeYN, Passcode = ( e.Passcode == null ) ? "": e.Passcode, FGHasSurveyYN = e.FGHasSurveyYN, SurveyID = e.SurveyID, FGSurveyDoneYN = p.FGSurveyDoneYN, FGAllSurveyDoneYN = (m.FGSurveyDoneYN == null) ? "Y" : m.FGSurveyDoneYN, FGAbsenYN =( r.StatusHadirYN == null) ? "Y" : r.StatusHadirYN }).ToList<EventSurvey>();


                if (evt == null)
                {
                    //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                }
                return evt;

            }
            else
            {
                //events = db.Event.Find(id);
                //var evt = (from p in db.EventPeserta
                //           join e in db.Event on p.EventID equals e.EventID
                //           join s in db.SurveyPeserta on new { p.EventID, p.EmpNIK, SsnID = 0 } equals new { s.EventID, s.EmpNIK, SsnID = s.SessionID }
                //           where p.EmpNIK == empNIK && p.EventID == id
                //           select new EventSurvey { EventID = e.EventID, EventCode = e.EventCode, EventName = e.EventName, EventType = e.EventType, ExternalEventCode = e.ExternalEventCode, EventDesc = e.EventDesc, EventImage = e.EventImage, FGHasPasscodeYN = e.FGHasPasscodeYN, Passcode = e.Passcode, FGHasSurveyYN = e.FGHasSurveyYN, SurveyID = e.SurveyID, FGSurveyDoneYN = p.FGSurveyDoneYN, FGAllSurveyDoneYN = s.FGSurveyDoneYN }).ToList<EventSurvey>();

                var evt = (from p in db.EventPeserta
                           join e in db.Event on p.EventID equals e.EventID
                           join q in db.EventPesertaAbsen on new { p.EventID, p.EmpNIK, AbsenDate = DateTime.Today.ToString("dd/MM/yyyy") } equals new { q.EventID, q.EmpNIK, AbsenDate = q.EventDate.ToString("dd/MM/yyyy") } into sq
                           join s in db.SurveyPeserta on new { p.EventID, p.EmpNIK, SsnID = 0 } equals new { s.EventID, s.EmpNIK, SsnID = s.SessionID } into sp
                           from m in sp.DefaultIfEmpty()
                           from r in sq.DefaultIfEmpty()
                           where p.EmpNIK == empNIK && p.EventID == id
                           select new EventSurvey { EventID = e.EventID, EventCode = e.EventCode, EventName = e.EventName, EventType = e.EventType, ExternalEventCode = e.ExternalEventCode, EventDesc = (e.EventDesc == null) ? "" : e.EventDesc, EventImage = (e.EventImage == null) ? "" : e.EventImage, FGHasPasscodeYN = e.FGHasPasscodeYN, Passcode = (e.Passcode == null) ? "" : e.Passcode, FGHasSurveyYN = e.FGHasSurveyYN, SurveyID = e.SurveyID, FGSurveyDoneYN = p.FGSurveyDoneYN, FGAllSurveyDoneYN = (m.FGSurveyDoneYN == null) ? "Y" : m.FGSurveyDoneYN, FGAbsenYN = (r.StatusHadirYN == null) ? "Y" : r.StatusHadirYN }).ToList<EventSurvey>();

                if (evt == null)
                {
                    //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                }
                return evt;
            }
        }

        [Route("api/[controller]/{empNIK}")]
        [HttpGet]
        [Authorize]
        public ActionResult<List<EventSurvey>> Get(string empNIK)
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;
            //List<Event> evtps = new List<Event> { };
            List<EventSurvey> evtps = new List<EventSurvey> { };

            if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }

            if (spendingTimeWithCompany > 5)
            {
                //evtps = db.EventPesertaAbsen.Find(id);
                //evtps = db.EventPesertaAbsen.SingleOrDefault(x => x.EventID == id);


                //var evt = (from p in db.EventPeserta
                //           join e in db.Event on p.EventID equals e.EventID
                //           where p.EmpNIK == empNIK
                //           select  e).ToList<Event>();
                var evt = (from p in db.EventPeserta
                           join e in db.Event on p.EventID equals e.EventID
                           join s in db.SurveyPeserta on new {p.EventID,p.EmpNIK, SsnID=0 } equals new {s.EventID, s.EmpNIK, SsnID = s.SessionID} into sp
                           from m in sp.DefaultIfEmpty()
                           where p.EmpNIK == empNIK
                           select new EventSurvey { EventID= e.EventID, EventCode=e.EventCode, EventName=e.EventName, EventType=e.EventType, ExternalEventCode=e.ExternalEventCode, EventDesc= e.EventDesc, EventImage=e.EventImage, FGHasPasscodeYN=e.FGHasPasscodeYN, Passcode= e.Passcode, FGHasSurveyYN= e.FGHasSurveyYN, SurveyID=e.SurveyID, FGSurveyDoneYN= p.FGSurveyDoneYN, FGAllSurveyDoneYN= (m.FGSurveyDoneYN == null) ? "Y" : m.FGSurveyDoneYN }).ToList<EventSurvey>();
                           //select new Event { EventID = e.EventID, EventCode=e.EventCode, EventName = e.EventName, EventType=e.EventType, ExternalEventCode=e.ExternalEventCode, EventDesc=e.EventDesc, EventImage=e.EventImage, FGHasPasscodeYN=e.FGHasPasscodeYN }).ToList<Event>();
                           //evtps = db.EventPesertaAbsen.Where(x => x.EmpNIK == empNIK).ToList<EventPesertaAbsen>();

     
                      

                if (evt == null)
                {
                    //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                }
                return evt;

            }
            else
            {

                //var evt = (from p in db.EventPeserta
                //           join e in db.Event on p.EventID equals e.EventID
                //           where p.EmpNIK == empNIK
                //           select e).ToList<Event>();
                //var evt = (from p in db.EventPeserta
                //           join e in db.Event on p.EventID equals e.EventID
                //           join s in db.SurveyPeserta on new { p.EventID, p.EmpNIK, SsnID = 0 } equals new { s.EventID, s.EmpNIK, SsnID = s.SessionID }
                //           where p.EmpNIK == empNIK
                //           select new EventSurvey { EventID = e.EventID, EventCode = e.EventCode, EventName = e.EventName, EventType = e.EventType, ExternalEventCode = e.ExternalEventCode, EventDesc = e.EventDesc, EventImage = e.EventImage, FGHasPasscodeYN = e.FGHasPasscodeYN, Passcode = e.Passcode, FGHasSurveyYN = e.FGHasSurveyYN, SurveyID = e.SurveyID, FGSurveyDoneYN = p.FGSurveyDoneYN, FGAllSurveyDoneYN = s.FGSurveyDoneYN }).ToList<EventSurvey>();

                var evt = (from p in db.EventPeserta
                           join e in db.Event on p.EventID equals e.EventID
                           join s in db.SurveyPeserta on new { p.EventID, p.EmpNIK, SsnID = 0 } equals new { s.EventID, s.EmpNIK, SsnID = s.SessionID } into sp
                           from m in sp.DefaultIfEmpty()
                           where p.EmpNIK == empNIK
                           select new EventSurvey { EventID = e.EventID, EventCode = e.EventCode, EventName = e.EventName, EventType = e.EventType, ExternalEventCode = e.ExternalEventCode, EventDesc = e.EventDesc, EventImage = e.EventImage, FGHasPasscodeYN = e.FGHasPasscodeYN, Passcode = e.Passcode, FGHasSurveyYN = e.FGHasSurveyYN, SurveyID = e.SurveyID, FGSurveyDoneYN = p.FGSurveyDoneYN, FGAllSurveyDoneYN = (m.FGSurveyDoneYN == null) ? "Y" : m.FGSurveyDoneYN }).ToList<EventSurvey>();
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
        public void Post([FromBody]Event evts)
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;
            Event events = new Event();

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
                    db.Event.Add(evts);
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
                    db.Event.Add(evts);
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
        public Event Put(int id, [FromBody]Event evts)
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;
            Event events = new Event();

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
                var evt = db.Event.SingleOrDefault(x => x.EventID == id);

                //Might be user sends invalid id.
                if (evt == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                evt.EventName = evts.EventName;
                evt.EventCode = evts.EventCode;
                evt.EventType = evts.EventType;
                evt.ExternalEventCode = evts.ExternalEventCode;
                evt.EventDesc = evts.EventDesc;
                evt.EventImage = events.EventImage;
                evt.FGHasPasscodeYN = evts.FGHasPasscodeYN;
                evt.FGHasSurveyYN = evts.FGHasSurveyYN;
                evt.SurveyID = evts.SurveyID;
                evt.FGActiveYN = evts.FGActiveYN;
                evt.LastUpdate = DateTime.Today;
                evt.LastUpdateBy = evts.LastUpdateBy;
                //evt.LastUpdateBy = currentUser.Claims.FirstOrDefault(c => c.Type == "NIK").Value;

                db.SaveChanges();
                return events;
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
                }

                var evt = db.Event.SingleOrDefault(x => x.EventID == id);

                // Might be user sends invalid id.
                if (evt == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                evt.EventName = evts.EventName;
                evt.EventCode = evts.EventCode;
                evt.EventType = evts.EventType;
                evt.ExternalEventCode = evts.ExternalEventCode;
                evt.EventDesc = evts.EventDesc;
                evt.EventImage = events.EventImage;
                evt.FGHasPasscodeYN = evts.FGHasPasscodeYN;
                evt.FGHasSurveyYN = evts.FGHasSurveyYN;
                evt.SurveyID = evts.SurveyID;
                evt.FGActiveYN = evts.FGActiveYN;
                evt.LastUpdate = DateTime.Today;
                evt.LastUpdateBy = evts.LastUpdateBy;
                //cek ini
                //evt.LastUpdateBy = currentUser.Claims.FirstOrDefault(c => c.Type == "NIK").Value;
                


                db.SaveChanges();
                return events;
            }
        }

        //delete user by id 
        [Route("api/[controller]/{id}")]
        [HttpDelete]
        [Authorize]
        public void Delete(int id)
        {
            var currentEvent = HttpContext.User;
            int spendingTimeWithCompany = 0;
    Event events = new Event();


            if (currentEvent.HasClaim(c => c.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentEvent.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }

            if (spendingTimeWithCompany > 5)
            {
                Event evt = db.Event.Find(id);
                if (evt == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                db.Event.Remove(evt);
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
                Event evt = db.Event.Find(id);
                if (evt == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                db.Event.Remove(evt);
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
