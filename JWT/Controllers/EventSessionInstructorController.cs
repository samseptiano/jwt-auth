﻿using JWT.Context;
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
    public class EventSessionInstructorController : Controller
    {
        private DBContext db = new DBContext();


        [Route("api/[controller]")]
        // GET api/values
        [HttpGet]
        [Authorize]
        public ActionResult<List<EventSessionInstructor>> Get()
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;
            List<EventSessionInstructor> evtps = new List<EventSessionInstructor> { };

            if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }

            if (spendingTimeWithCompany > 5)
            {
                evtps = db.EventSessionInstructor.ToList();
                return Ok(new { evtps });

            }
            else
            {
                evtps = db.EventSessionInstructor.ToList();
                return Ok(new { evtps });
            }
        }

        // GET api/values/1
        [Route("api/[controller]/{id}")]
        [HttpGet]
        [Authorize]
        public EventSessionInstructor Get(int id)
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;
            EventSessionInstructor evtps = new EventSessionInstructor();

            if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }

            if (spendingTimeWithCompany > 5)
            {
                evtps = db.EventSessionInstructor.Find(id);
                if (evtps == null)
                {
                    //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                }
                return evtps;

            }
            else
            {
                evtps = db.EventSessionInstructor.Find(id);
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
        public void Post([FromBody]EventSessionInstructor evts)
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;
            EventSessionInstructor evtps = new EventSessionInstructor();

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
                    db.EventSessionInstructor.Add(evts);
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
                    db.EventSessionInstructor.Add(evts);
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
        public EventSessionInstructor Put(int id, [FromBody]EventSessionInstructor evts)
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;
            EventSessionInstructor evtps = new EventSessionInstructor();

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
                var evt = db.EventSessionInstructor.SingleOrDefault(x => x.SIID == id);

                //Might be user sends invalid id.
                if (evt == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }


                evt.EventID = evts.EventID;
                evt.SessionID = evts.SessionID;
                evt.InstructorID = evts.InstructorID;
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

                var evt = db.EventSessionInstructor.SingleOrDefault(x => x.EventID == id);

                // Might be user sends invalid id.
                if (evt == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                evt.EventID = evts.EventID;
                evt.SessionID = evts.SessionID;
                evt.InstructorID = evts.InstructorID;
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
            EventSessionInstructor evtps = new EventSessionInstructor();


            if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }

            if (spendingTimeWithCompany > 5)
            {
                EventSessionInstructor evt = db.EventSessionInstructor.Find(id);
                if (evt == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                db.EventSessionInstructor.Remove(evt);
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
                EventSessionInstructor evt = db.EventSessionInstructor.Find(id);
                if (evt == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                db.EventSessionInstructor.Remove(evt);
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