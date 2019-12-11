using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using JWT.DataProvider;
using static JWT.Controllers.TokenController;

namespace JWT.Controllers
{
    [Route("api/[controller]")]
    public class VersionController : Controller
    {
        private IConfiguration _config;
        private IVersionProvider versionProvider;

        public VersionController(IVersionProvider versionProvider, IConfiguration config)
        {
            this.versionProvider = versionProvider;
            _config = config;
        }



        [AllowAnonymous]
        // GET api/values
        [HttpPost]
        public async Task<IEnumerable<Models.Version>> GetVersionsAsync([FromBody] LoginModel login)
        {

            IActionResult response = Unauthorized();
            var user = Authenticate(login);

            if (user != null)
            {
                return await versionProvider.GetVersion();
            }
            return null;
        }

        private UserModel Authenticate(LoginModel login)
        {
            UserModel user = null;

            if (login.Username == "mario" && login.Password == "secret")
            {
                user = new UserModel { NIK = "10000001", Name = "Mario Rossi", Email = "maria.rossi@domain.com", DateOfJoing = new DateTime(2018, 12, 27) };
            }
            return user;
        }
        public class LoginModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        private class UserModel
        {
            public string NIK { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public DateTime Birthdate { get; set; }
            public DateTime DateOfJoing { get; set; }

        }
    }
}
