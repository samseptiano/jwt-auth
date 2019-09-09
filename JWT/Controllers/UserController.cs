using JWT.Context;
using JWT.DataProvider;
using JWT.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;



namespace JWT.Controllers
{
    [Produces("application/json")]
    [ApiController]

    public class UserController : Controller
    {

        private IUserProvider userProvider;

        public UserController(IUserProvider userProvider)
        {
            this.userProvider = userProvider;
        }

        private DBContext db = new DBContext();
        //const string LDAP_PATH = "LDAP://exldap.example.com:5555";  //INF-ADDC-1.enseval.com
        const string LDAP_PATH = "LDAP://DC=enseval,DC=com";
        const string LDAP_DOMAIN = "ENSEVAL.COM";

        [Route("api/[controller]")]
        // GET api/values
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<UserData>>> GetAsync()
        {

            List<UserData> usr = new List<UserData>();

            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;
            List<User> users = new List<User> { };

            if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }

            if (spendingTimeWithCompany > 5)
            {
                try
                {

                    var userAuthData = await this.userProvider.GetUserAll();
                    usr = userAuthData.ToList();
                }
                catch
                {
                    //usr = null;
                }

            }
            else
            {
                try
                {

                    var userAuthData = await this.userProvider.GetUserAll();
                    usr = userAuthData.ToList();
                }
                catch
                {
                    //usr = null;
                }
            }

            return usr;
        }


        
        [Route("api/[controller]")]
        [HttpPost]
        [Authorize]
        public async Task<UserData> PostAsync([FromBody]User user)
        {

            UserData usr = new UserData();
            String userAuth ="";

            if (!user.UserID.Equals(null) || !user.Password.Equals(null))
            {
                //userAuth = IsAuthenticated(user.UserID, user.Password);
                userAuth = user.UserID;
            }
                    
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;
            //User users = new User();

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
                    try
                    {

                        var userAuthData = await this.userProvider.GetUser(userAuth);
                        usr = userAuthData.ToList()[0];
                        usr.empUsername = user.UserID;
                        usr.password = user.Password;
                    }
                    catch
                    {
                        //usr = null;
                    }

                }

            }
            else
            {

                if (!ModelState.IsValid)
                {
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
                }
                else
                {
                    try
                    {

                        var userAuthData = await this.userProvider.GetUser(userAuth);
                        usr = userAuthData.ToList()[0];
                        usr.empUsername = user.UserID;
                        usr.password = user.Password;
                    }
                    catch {
                        //usr = null;
                    }
                }

            }

            return usr;
        }

        //update customer  
        [Route("api/[controller]/{id}")]
        [HttpPut]
        [Authorize]
        public User Put(int id, [FromBody]User user)
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

                //tes
                var usr = db.User.SingleOrDefault(x => x.UserID == user.UserID);

                //Might be user sends invalid id.
                if (usr == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }


                usr.Username = user.Username;
                usr.EmpEmail = user.EmpEmail;
                usr.Password = user.Password;
                usr.EmpNIK = user.EmpNIK;
                usr.FGActiveYN = user.FGActiveYN;
                usr.LastChangePassword = DateTime.Today;
                usr.LastUpdateBy = user.Username;

                db.SaveChanges();
                return user;
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
                }

                var usr = db.User.SingleOrDefault(x => x.UserID == user.UserID);

                // Might be user sends invalid id.
                if (usr == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                usr.Username = user.Username;
                usr.EmpEmail = user.EmpEmail;
                usr.Password = user.Password;
                usr.EmpNIK = user.EmpNIK;
                usr.FGActiveYN = user.FGActiveYN;
                usr.LastChangePassword = DateTime.Today;
                usr.LastUpdateBy = user.Username;


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
                User user = db.User.Find(id);
                if (user == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                db.User.Remove(user);
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
            else
            {
                User user = db.User.Find(id);
                if (user == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                db.User.Remove(user);
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


        private String IsAuthenticated(String username, String password)
        {
            string surname = "";
            try
            {
                DirectoryEntry directoryEntry = new DirectoryEntry("LDAP://10.102.4.4", username, password);
                DirectorySearcher searcher = new DirectorySearcher(directoryEntry)
                {
                    PageSize = int.MaxValue,
                    Filter = "(&(objectCategory=person)(objectClass=user)(sAMAccountName=" + username + "))"
                };

                searcher.PropertiesToLoad.Add("sAMAccountName");
                searcher.PropertiesToLoad.Add("employeeNumber");

                var result = searcher.FindOne();
                //
                //foreach (var aaaa in result){ 

                if (result == null)
                {
                    return ""; // Or whatever you need to do in this case
                }



                if (result.Properties.Contains("employeeNumber"))
                {
                    //surname += result.Properties["sAMAccountName"][0].ToString();
                    surname += result.Properties["employeeNumber"][0].ToString();
                    //surname = "true";
                }
                // }
            }
            catch
            {
                surname = "false";
            }
            return surname;
        }

        ////prevent memory leak  
        //protected override void Dispose(bool disposing)
        //{
        //    db.Dispose();
        //    base.Dispose(disposing);
        //}


        //[Route("api/[controller]")]
        //[HttpPost]
        //[Authorize]
        //public Task<AuthenticateResult> Post( [FromBody]User user)
        //{
        //    var currentUser = HttpContext.User;
        //    int spendingTimeWithCompany = 0;
        //    User users = new User();

        //    if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
        //    {
        //        DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
        //        spendingTimeWithCompany = DateTime.Today.Year - date.Year;
        //    }

        //    if (spendingTimeWithCompany > 5)
        //    {
        //        //users = db.User.Find(id);
        //        users = db.User.Find(user.UserID);

        //        using (var context = new PrincipalContext(ContextType.Domain, LDAP_DOMAIN, "service_acct_user", "service_acct_pswd"))
        //        {
        //            if (context.ValidateCredentials(LDAP_DOMAIN + "/" + user.UserID, user.Password))
        //            {
        //                using (var de = new DirectoryEntry(LDAP_PATH))
        //                using (var ds = new DirectorySearcher(de))
        //                {
        //                    // other logic to verify user has correct permissions

        //                    // User authenticated and authorized
        //                    var identities = new List<ClaimsIdentity> { new ClaimsIdentity("custom auth type") };
        //                   //var ticket = new AuthenticationTicket(new ClaimsPrincipal(identities), Options.Scheme);
        //                    var ticket = new AuthenticationTicket(new ClaimsPrincipal(identities), CookieAuthenticationDefaults.AuthenticationScheme);
        //                    return Task.FromResult(AuthenticateResult.Success(ticket));

        //                }
        //            } else
        //            {
        //                return Task.FromResult(AuthenticateResult.Fail("Invalid auth key."));
        //            }
        //        }



        //        //if (users == null)
        //        //{
        //        //    //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
        //        //}
        //        //return users;

        //    }
        //    else
        //    {
        //        //users = db.User.Find(id);
        //        users = db.User.Find(user.UserID);
        //        using (var context = new PrincipalContext(ContextType.Domain, LDAP_DOMAIN, "service_acct_user", "service_acct_pswd"))
        //        {
        //            if (context.ValidateCredentials(LDAP_DOMAIN + "/" + user.UserID, user.Password))
        //            {
        //                using (var de = new DirectoryEntry(LDAP_PATH))
        //                using (var ds = new DirectorySearcher(de))
        //                {
        //                    // other logic to verify user has correct permissions

        //                    // User authenticated and authorized
        //                    var identities = new List<ClaimsIdentity> { new ClaimsIdentity("custom auth type") };
        //                    //var ticket = new AuthenticationTicket(new ClaimsPrincipal(identities), Options.Scheme);
        //                    var ticket = new AuthenticationTicket(new ClaimsPrincipal(identities), CookieAuthenticationDefaults.AuthenticationScheme);
        //                    return Task.FromResult(AuthenticateResult.Success(ticket));

        //                }
        //            }
        //            else
        //            {
        //                return Task.FromResult(AuthenticateResult.Fail("Invalid auth key."));
        //            }
        //        }
        //        //if (users == null)
        //        //{
        //        //    //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
        //        //}
        //        //return users;
        //    }
        //}


        //Backup

    }
}
