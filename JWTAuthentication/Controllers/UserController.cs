using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using JWTAuthentication.Context;
using JWTAuthentication.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuthentication.Controllers
{
    [Produces("application/json")]
    [ApiController]
    public class UserController : Controller
    {

        private DBContext db = new DBContext();


        [Route("api/[controller]")]
        // GET api/values
        [HttpGet]
        [Authorize]
        public ActionResult<List<TransUserEvents>> Get()
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;
            List<TransUserEvents> newses = new List<TransUserEvents> { };

            if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }

            if (spendingTimeWithCompany > 5)
            {
                newses = db.transUserEvents.ToList();
                return Ok(new { newses });

            }
            else
            {
                newses = db.transUserEvents.ToList();
                return Ok(new { newses });
            }
        }


        // GET api/user_events/1
        [Route("api/[controller]/{id}")]
        [HttpGet]
        [Authorize]
        public ActionResult<List<NewsTable>> Get(int id)
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;
            TransUserEvents usersEvents = new TransUserEvents();

            if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }

            if (spendingTimeWithCompany > 5)
            {


                var newses = (from s in db.newsTable
                            join c in db.transUserEvents on s.id.ToString() equals c.id_news
                            where c.id_user == id.ToString()
                            select new { NewsTable = s, TransUserEvents = c == null ? "(No products)" : c.date_join, c.status, c.id_user }).ToList();


                if (newses == null)
                {
                    //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                }
                return Ok(new { newses });

            }
            else
            {



                var newses = (from s in db.newsTable
                            join c in db.transUserEvents on s.id.ToString() equals c.id_news
                            where c.id_user == id.ToString()
                              select new { NewsTable = s, TransUserEvents = c == null ? "(No products)" : c.date_join,c.status,c.id_user }).ToList();

                if (newses == null)
                {
                    //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                }
                return Ok(new { newses });
            }
        }

        //// POST api/user_events
        [Route("api/[controller]")]
        [HttpPost]
        [Authorize]
        public void Post([FromBody]TransUserEvents user_events)
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;
            User users = new User();

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
                    db.transUserEvents.Add(user_events);
                    db.SaveChanges();
                    throw new HttpResponseException(HttpStatusCode.Created);
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
                    db.transUserEvents.Add(user_events);
                    db.SaveChanges();
                    throw new HttpResponseException(HttpStatusCode.Created);
                }
                //return user;
            }
        }
    }
}