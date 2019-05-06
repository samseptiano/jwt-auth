using JWTAuthentication.Context;
using JWTAuthentication.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace JWTAuthentication.Controllers
{
    [Produces("application/json")]
    [ApiController]
    public class ValuesController : Controller
    {

        private DBContext db = new DBContext();


        [Route("api/[controller]")]
        // GET api/values
        [HttpGet]
        [Authorize]
        public ActionResult<List<User>> Get()
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;
            List<User> users= new List<User> { };

            if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }

            if (spendingTimeWithCompany > 5)
            {
               users = db.user.ToList();
                return Ok(new { users });

            }
            else
            {
                users = db.user.ToList();
                return Ok(new { users });
            }
        }

        // GET api/values/1
        [Route("api/[controller]/{id}")]
        [HttpGet]
        [Authorize]
        public User Get(int id)
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
                users = db.user.Find(id);
                if (users == null)
                {
                    //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                }
                return  users;

            }
            else
            {
                users = db.user.Find(id);
                if (users == null)
                {
                    //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                }
                return users;
            }
        }

        //// POST api/values
        [Route("api/[controller]")]
        [HttpPost]
        [Authorize]
        public void Post([FromBody]User user)
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
                    db.user.Add(user);
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
                    db.user.Add(user);
                    db.SaveChanges();
                    throw new HttpResponseException(HttpStatusCode.Created);
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
        public User Put(int id,[FromBody]User user)
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

                var usr = db.user.SingleOrDefault(x => x.UserId == id);

                // Might be user sends invalid id.
                if (usr == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                usr.Username = user.Username;
                usr.Email = user.Email;
                usr.Password = user.Password;
                usr.EmpNIK = user.EmpNIK;

                db.SaveChanges();
                return user;
            }
            else {
                if (!ModelState.IsValid)
                {
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
                }

                var usr = db.user.SingleOrDefault(x => x.UserId == id);

                // Might be user sends invalid id.
                if (usr == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                usr.Username = user.Username;
                usr.Email = user.Email;
                usr.Password = user.Password;
                usr.EmpNIK = user.EmpNIK;

                db.SaveChanges();
                return user;
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
            User users = new User();

            if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }

            if (spendingTimeWithCompany > 5)
            {
                User user = db.user.Find(id);
                if (user == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                db.user.Remove(user);
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound); ;
                }
                throw new HttpResponseException(HttpStatusCode.OK); ;
            }
            else {
                User user = db.user.Find(id);
                if (user == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                db.user.Remove(user);
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound); ;
                }
                throw new HttpResponseException(HttpStatusCode.OK); ;
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
