using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JWT.Controllers
{
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        [HttpGet, Authorize]
        public IEnumerable<Books> Get()
        {
            var currentUser = HttpContext.User;
            int userAge = 0;
            var resultBookList = new Books[]
            {
                new Books { Author= "Ray Bradbury", Title = "Fahrenheit 451", AgeRestriction=false},
                new Books { Author= "Gabriel Garcia", Title = "One Hundred Years", AgeRestriction=false},
                new Books { Author= "George Orwell", Title = "1984", AgeRestriction=false},
                new Books { Author= "Anais Nin", Title = "Delta of Venus", AgeRestriction=true}
            };

            if (currentUser.HasClaim(c => c.Type == ClaimTypes.DateOfBirth))
            {
                DateTime birthDate = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.DateOfBirth).Value);
                userAge = DateTime.Today.Year - birthDate.Year;
            }

            if (userAge < 18)
            {
                resultBookList = resultBookList.Where(b => !b.AgeRestriction).ToArray();
            }
            return resultBookList;
        }
    }

    public class Books
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public bool AgeRestriction { get; set; }
    }
}
