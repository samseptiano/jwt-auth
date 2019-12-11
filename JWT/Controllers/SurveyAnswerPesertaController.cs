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
using JWT.DataProvider;



namespace JWT.Controllers
{
    [Produces("application/json")]
    [ApiController]
    //[Route("api/v1/[controller]")]
    public class SurveyAnswerPesertaController : Controller
    {
        //private DBContext db = new DBContext();
        private  ISurveyAnswerPesertaProvider surveyAnswerPesertaProvider;

        public SurveyAnswerPesertaController(ISurveyAnswerPesertaProvider surveyAnswerPesertaProvider)
        {
            this.surveyAnswerPesertaProvider = surveyAnswerPesertaProvider;

        }

        //// POST api/values
    //    [Route("api/[controller]")]
    //    [HttpPost]
    //    [Authorize]
    //    public async Task Post([FromBody]SurveyAnswerPeserta evts)
    //    {
    //        var currentUser = HttpContext.User;
    //        int spendingTimeWithCompany = 0;
    //        //SurveyAnswerPeserta evtps = new SurveyAnswerPeserta();
           

    //        if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
    //        {
    //            DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
    //            spendingTimeWithCompany = DateTime.Today.Year - date.Year;
    //        }

    //        if (spendingTimeWithCompany > 5)
    //        {

    //            if (!ModelState.IsValid)
    //            {
    //                throw new HttpResponseException(HttpStatusCode.BadRequest);
    //            }
    //            else
    //            {
    //                await this.surveyAnswerPesertaProvider.UpdateSurveyAnswerPeserta(evts);
    //                //throw new HttpResponseException(HttpStatusCode.Created);
    //            }
    //// return user;
    //        }
    //        else
    //        {
    //            if (!ModelState.IsValid)
    //            {
    //                throw new HttpResponseException(HttpStatusCode.BadRequest);
    //            }
    //            else
    //            {
    //                //db.SurveyAnswerPeserta.Add(evts);
    //                //db.SaveChanges();
    //                await this.surveyAnswerPesertaProvider.UpdateSurveyAnswerPeserta(evts);
    //                //throw new HttpResponseException(HttpStatusCode.Created);
    //            }
    //            //return user;
    //        }
    //    }

        //// POST api/values
        [Route("api/[controller]")]
        [HttpPost]
        [Authorize]
        public async Task Post([FromBody]List<SurveyAnswerPeserta> evts)
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;
            int jmlSurvey = 0;
            
            //SurveyAnswerPeserta evtps = new SurveyAnswerPeserta();


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
                    foreach(SurveyAnswerPeserta evtps in evts)
                    {
                        await this.surveyAnswerPesertaProvider.UpdateSurveyAnswerPeserta(evtps);
                        jmlSurvey++;

                        if (jmlSurvey == evts.Count)
                        //tes
                        //if (jmlSurvey == 3)
                        {
                            //update status survey done  UpdateStatusSurveyPeserta

                            await this.surveyAnswerPesertaProvider.UpdateStatusSurveyPeserta(evtps.EventID, evtps.EmpNIK, evtps.SessionID);
                        }


                    }

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
                    //db.SurveyAnswerPeserta.Add(evts);
                    //db.SaveChanges();
                    //await this.surveyAnswerPesertaProvider.UpdateSurveyAnswerPeserta(evts);
                    //throw new HttpResponseException(HttpStatusCode.Created);
                    foreach (SurveyAnswerPeserta evtps in evts)
                    {
                        await this.surveyAnswerPesertaProvider.UpdateSurveyAnswerPeserta(evtps);
                        jmlSurvey++;

                        if (jmlSurvey == evts.Count)
                        //if (jmlSurvey == 3)
                        {
                            //update status survey done  UpdateStatusSurveyPeserta

                            await this.surveyAnswerPesertaProvider.UpdateStatusSurveyPeserta(evtps.EventID, evtps.EmpNIK, evtps.SessionID);
                        }
                    }
                }
                //return user;
            }
        }



        ////delete user by id 
        //[Route("api/[controller]/{id}")]
        //[HttpDelete]
        //[Authorize]
        //public void Delete(int id)
        //{
        //    var currentUser = HttpContext.User;
        //    int spendingTimeWithCompany = 0;
        //    SurveyQuestion evtps = new SurveyQuestion();


        //    if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
        //    {
        //        DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
        //        spendingTimeWithCompany = DateTime.Today.Year - date.Year;
        //    }

        //    if (spendingTimeWithCompany > 5)
        //    {
        //        SurveyQuestion evt = db.SurveyQuestion.Find(id);
        //        if (evt == null)
        //        {
        //            throw new HttpResponseException(HttpStatusCode.NotFound);
        //        }
        //        db.SurveyQuestion.Remove(evt);
        //        try
        //        {
        //            db.SaveChanges();
        //        }
        //        catch (DbUpdateConcurrencyException ex)
        //        {
        //            throw new HttpResponseException(HttpStatusCode.NotFound); ;
        //        }
        //        //throw new HttpResponseException(HttpStatusCode.OK); ;
        //    }
        //    else
        //    {
        //        SurveyQuestion evt = db.SurveyQuestion.Find(id);
        //        if (evt == null)
        //        {
        //            throw new HttpResponseException(HttpStatusCode.NotFound);
        //        }
        //        db.SurveyQuestion.Remove(evt);
        //        try
        //        {
        //            db.SaveChanges();
        //        }
        //        catch (DbUpdateConcurrencyException ex)
        //        {
        //            throw new HttpResponseException(HttpStatusCode.NotFound); ;
        //        }
        //        //throw new HttpResponseException(HttpStatusCode.OK); ;
        //    }

        //}

    }
}
