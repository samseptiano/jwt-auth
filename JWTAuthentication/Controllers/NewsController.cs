using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWTAuthentication.Context;
using JWTAuthentication.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuthentication.Controllers
{

    [Produces("application/json")]
    [ApiController]
    public class NewsController : Controller
    {
        private DBContext db = new DBContext();


        [Route("api/[controller]")]
        // GET api/values
        [HttpGet]
        [Authorize]
        public ActionResult<List<NewsTable>> Get()
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;
            List<NewsTable> newses = new List<NewsTable> { };

            if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }

            if (spendingTimeWithCompany > 5)
            {
                newses = db.newsTable.ToList();
                return Ok(new { newses });

            }
            else
            {
                newses = db.newsTable.ToList();
                return Ok(new { newses });
            }
        }

        // GET api/values/1
        [Route("api/[controller]/{id}")]
        [HttpGet]
        [Authorize]
        public NewsTable Get(int id)
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;
            NewsTable newses = new NewsTable();

            if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }

            if (spendingTimeWithCompany > 5)
            {
                newses = db.newsTable.Find(id);
                if (newses == null)
                {
                    //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                }
                return newses;

            }
            else
            {
                newses = db.newsTable.Find(id);
                if (newses == null)
                {
                    //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                }
                return newses;
            }
        }

        ////prevent memory leak  
        //protected override void Dispose(bool disposing)
        //{
        //    db.Dispose();
        //    base.Dispose(disposing);
        //}

    }
}
