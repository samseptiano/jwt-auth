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

    //Controller untuk methode GET dan POST PK

    public class UserListController : Controller
    {

        private IUserListProvider userListProvider;

        public UserListController(IUserListProvider userListProvider)
        {
            this.userListProvider = userListProvider;
        }

        private DBContext db = new DBContext();

        [Route("api/[controller]/PA/{empNIK}/{tahun}")]
        // GET api/values
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<UserListPA>>> GetAsync(String empNIK, String tahun)
        {

            List<UserListPA> usr = new List<UserListPA>();
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;

            if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }

            if (spendingTimeWithCompany > 5)
            {
                try
                {
                    var xxx = await this.userListProvider.GetUserList(empNIK, tahun);
                    usr = xxx.ToList();
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
                    var xxx = await this.userListProvider.GetUserList(empNIK, tahun);
                    usr = xxx.ToList();
                }
                catch
                {
                    //usr = null;
                }
            }

            return usr;
        }


        [Route("api/[controller]/empPhoto/{fileName}/")]
        [HttpGet]
        public async Task<IActionResult> Get(String fileName)
        {
            var image = System.IO.File.OpenRead(".\\Resources\\f153236793cc003c5c987f95b371c800\\" + fileName);
            return File(image, "image/jpeg");
        }


        [Route("api/[controller]/emporg/{empNIK}/{tahun}")]
        // GET api/values
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<PA_Org>>> GetAsyncOrg(String empNIK, String tahun)
        {

            List<PA_Org> usr = new List<PA_Org>();
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;

            if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }

            if (spendingTimeWithCompany > 5)
            {
                try
                {
                    var xxx = await this.userListProvider.GetEmpOrg(empNIK, tahun);
                    usr = xxx.ToList();
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
                    var xxx = await this.userListProvider.GetEmpOrg(empNIK, tahun);
                    usr = xxx.ToList();
                }
                catch
                {
                    //usr = null;
                }
            }

            return usr;
        }



        [Route("api/[controller]/PJ/{empNIK}")]
        // GET api/values
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<UserList>>> GetAsyncPJ(String empNIK)
        {

            List<UserList> usr = new List<UserList>();
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;

            if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }

            if (spendingTimeWithCompany > 5)
            {
                try
                {

                    var xxx = await this.userListProvider.GetUserListPJ(empNIK);
                    usr = xxx.ToList();
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
                    var xxx = await this.userListProvider.GetUserListPJ(empNIK);
                    usr = xxx.ToList();
                }
                catch
                {
                    //usr = null;
                }
            }
            return usr;
        }


        [Route("api/[controller]/PJ/ONE/{empNIK}")]
        // GET api/values
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<UserList>>> GetAsyncPJOne(String empNIK)
        {

            List<UserList> usr = new List<UserList>();
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;

            if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }

            if (spendingTimeWithCompany > 5)
            {
                try
                {

                    var xxx = await this.userListProvider.GetUserListPJOne(empNIK);
                    usr = xxx.ToList();
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

                    var xxx = await this.userListProvider.GetUserListPJOne(empNIK);
                    usr = xxx.ToList();
                }
                catch
                {
                    //usr = null;
                }
            }
            
            return usr;
        }


    }
}
