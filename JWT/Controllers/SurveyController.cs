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
    public class SurveyController : Controller
    {
        private DBContext db = new DBContext();


        [Route("api/[controller]")]
        // GET api/values
        [HttpGet]
        [Authorize]
        public ActionResult<List<Survey>> Get()
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;
            List<Survey> evtps = new List<Survey> { };

            if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }

            if (spendingTimeWithCompany > 5)
            {
                evtps = db.Survey.ToList();
                return Ok(new { evtps });

            }
            else
            {
                evtps = db.Survey.ToList();
                return Ok(new { evtps });
            }
        }

        // GET api/values/1
        [Route("api/[controller]/{id}")]
        [HttpGet]
        [Authorize]
        public Survey Get(int id)
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;
            Survey evtps = new Survey();

            if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }

            if (spendingTimeWithCompany > 5)
            {
                evtps = db.Survey.Find(id);
                if (evtps == null)
                {
                    //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                }
                return evtps;

            }
            else
            {
                evtps = db.Survey.Find(id);
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
        public void Post([FromBody]Survey evts)
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;
            Survey evtps = new Survey();

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
                    db.Survey.Add(evts);
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
                    db.Survey.Add(evts);
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
        public Survey Put(int id, [FromBody]Survey evts)
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;
            Survey evtps = new Survey();

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
                var evt = db.Survey.SingleOrDefault(x => x.SurveyID == id);
                
                //Might be user sends invalid id.
                if (evt == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }


                evt.SurveyName = evts.SurveyName;
                evt.SurveyDateStart = evts.SurveyDateStart;
                evt.SurveyDateEnd = evts.SurveyDateEnd;
                evt.SurveyBobot = evts.SurveyBobot;
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

                var evt = db.Survey.SingleOrDefault(x => x.SurveyID == id);

                // Might be user sends invalid id.
                if (evt == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                evt.SurveyName = evts.SurveyName;
                evt.SurveyDateStart = evts.SurveyDateStart;
                evt.SurveyDateEnd = evts.SurveyDateEnd;
                evt.SurveyBobot = evts.SurveyBobot;
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
            Survey evtps = new Survey();


            if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }

            if (spendingTimeWithCompany > 5)
            {
                Survey evt = db.Survey.Find(id);
                if (evt == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                db.Survey.Remove(evt);
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
                Survey evt = db.Survey.Find(id);
                if (evt == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                db.Survey.Remove(evt);
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
