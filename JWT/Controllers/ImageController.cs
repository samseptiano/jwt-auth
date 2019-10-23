using JWT.DataProvider;
using JWT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace JWT.Controllers
{

    [Produces("application/json")]
    [ApiController]
    public class ImageController : Controller
    {
        private IGeneralDataProvider generalDataProvider;

        public ImageController(IGeneralDataProvider generalDataProvider)
        {
            this.generalDataProvider = generalDataProvider;

        }

        [Route("api/[controller]/{id}")]
        // GET api/values
        [HttpGet]
        [Authorize]
        //  public  async Task <IEnumerable<EventSessionSurvey>> Get(int id, string empNIK)
        //public ActionResult<List<FileModel>> Get(int id)
        public async Task<IEnumerable<FileModel>> Get(int id)
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;
            //List<EventSessionSurvey> evtps = new List<EventSessionSurvey> { };
            FileModel fileimage = new FileModel();
            if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }

            if (spendingTimeWithCompany > 5)
            {

                return await this.generalDataProvider.GetFile(id);
                //byte[] imgdata = fileimage.FileData;
                //MemoryStream ms = new MemoryStream(imgdata);

                //HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                //response.Content = new StreamContent(ms);
                //response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
                //return response;
            }
            else
            {
                return await this.generalDataProvider.GetFile(id);
                //fileimage = this.generalDataProvider.GetFile(id);
                //byte[] imgdata = fileimage.FileData;
                //MemoryStream ms = new MemoryStream(imgdata);

                //HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                //response.Content = new StreamContent(ms);
                //response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
                //return response;

            }
        }

        //[Route("api/[controller]/{id}")]
        //// GET api/values
        //[HttpGet]
        //[Authorize]
        //public HttpResponseMessage Get( int id)
        //{
        //    var currentUser = HttpContext.User;
        //    int spendingTimeWithCompany = 0;
        //    //List<EventSessionSurvey> evtps = new List<EventSessionSurvey> { };
        //    FileModel fileimage = new FileModel();
        //    if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
        //    {
        //        DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
        //        spendingTimeWithCompany = DateTime.Today.Year - date.Year;
        //    }

        //    if (spendingTimeWithCompany > 5)
        //    {

        //        fileimage= this.generalDataProvider.GetFile(id);
        //        byte[] imgdata = fileimage.FileData;
        //        MemoryStream ms = new MemoryStream(imgdata);

        //        HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
        //        response.Content = new StreamContent(ms);
        //        response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
        //        return response;
        //    }
        //    else
        //    {

        //        fileimage = this.generalDataProvider.GetFile(id);
        //        byte[] imgdata = fileimage.FileData;
        //        MemoryStream ms = new MemoryStream(imgdata);

        //        HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
        //        response.Content = new StreamContent(ms);
        //        response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
        //        return response;

        //    }
        //}

        //public ActionResult<FileStreamResult> GetFile(int id)
        //{
        //    var currentUser = HttpContext.User;
        //    int spendingTimeWithCompany = 0;
        //    //List<EventSessionSurvey> evtps = new List<EventSessionSurvey> { };

        //    if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
        //    {
        //        DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
        //        spendingTimeWithCompany = DateTime.Today.Year - date.Year;
        //    }

        //    if (spendingTimeWithCompany > 5)
        //    {

        //        return this.generalDataProvider.GetImageFile(id);

        //    }
        //    else
        //    {

        //        return this.generalDataProvider.GetImageFile(id);

        //    }
        //}
    }
}
