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

    public class PKTransController : Controller
    {
        private IPKTransProvider PKTransProvider;
        public PKTransController(IPKTransProvider PKTransProvider)
        {
            this.PKTransProvider = PKTransProvider;
        }
         private DBContext db = new DBContext();

        [Route("api/[controller]/{empNIK}")]
        // GET api/values
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<PK_ViewTransHeader>> GetTransHeaders( String empNIK )
        {
            PK_ViewTransHeader TransHeader = new PK_ViewTransHeader();
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;

            if (currentUser.HasClaim(C=> C.Type=="DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }

            if (spendingTimeWithCompany > 5)
            {
                try
                {
                    //sp1
                     var TransHeaderArray  = await this.PKTransProvider.GetTransHeaders(empNIK);
                    TransHeader = TransHeaderArray.ToList()[0];
                    //sp2
                    IEnumerable<PK_ViewTransDetail> IEnumTransDetails = await this.PKTransProvider.GetTransDetails(TransHeader.transID);
                    List<PK_ViewTransDetail> TransDetailList = IEnumTransDetails.Cast<PK_ViewTransDetail>().ToList();                                       
                    TransHeader.PK_ViewTransDetail = TransDetailList;

                    //for (int i = 0; i < TransDetailList.Count; i++)
                    //{
                    //    IEnumerable<PK_FileEvidence> IEnumFileEvidences = await this.PKTransProvider.GetFileEvidences(TransHeader.transID,TransDetailList.ToList()[i].KPINO);
                    //    TransDetailList[i].PK_FileEvidences = IEnumFileEvidences.ToList();
                    //}
                    //sp3
                    for (int i = 0; i < TransDetailList.Count; i++)
                    {
                        List<PK_ViewTransGrade> IEnumTransGradesTemp = new List<PK_ViewTransGrade>();
                        IEnumerable<PK_ViewTransGrade> IEnumTransGrades = await this.PKTransProvider.GetTransGrades(TransHeader.transID);
                        List<PK_ViewTransGrade> TransGradeList = IEnumTransGrades.Cast<PK_ViewTransGrade>().ToList();
                        for (int j = 0; j < TransGradeList.Count; j++)
                        {
                            if ((TransDetailList[i].TRANSID.ToString() == TransGradeList[j].TRANSID.ToString()) && (TransDetailList[i].KPINO.ToString()) == (TransGradeList[j].KPINO.ToString()) && (TransDetailList[i].KPIcategory.ToString()) == (TransGradeList[j].KPIcategory.ToString()))
                            {
                                IEnumTransGradesTemp.Add(TransGradeList[j]);
                            }
                        }
                        TransDetailList[i].PK_ViewTransGrades = IEnumTransGradesTemp;
                    }
                }
                catch
                {

                }
            }
            else
            {
                try
                {
                    //sp1
                    var getTransHeader = await this.PKTransProvider.GetTransHeaders(empNIK);
                    TransHeader = getTransHeader.ToList()[0];
                    //sp2
                    IEnumerable<PK_ViewTransDetail> IEnumTransDetails = await this.PKTransProvider.GetTransDetails(TransHeader.transID);
                    List<PK_ViewTransDetail> TransDetailList = IEnumTransDetails.Cast<PK_ViewTransDetail>().ToList();
                    TransHeader.PK_ViewTransDetail = TransDetailList;

                    //for (int i = 0; i < TransDetailList.Count; i++)
                    //{
                    //    IEnumerable<PK_FileEvidence> IEnumFileEvidences = await this.PKTransProvider.GetFileEvidences(TransHeader.transID,TransDetailList.ToList()[i].KPINO);
                    //    TransDetailList[i].PK_FileEvidences = IEnumFileEvidences.ToList();
                    //}
                    //sp3
                    for (int i = 0; i < TransDetailList.Count; i++)
                    {
                        List<PK_ViewTransGrade> IEnumTransGradesTemp = new List<PK_ViewTransGrade>();
                        IEnumerable<PK_ViewTransGrade> IEnumTransGrades = await this.PKTransProvider.GetTransGrades( TransHeader.transID);
                        List<PK_ViewTransGrade> TransGradeList = IEnumTransGrades.Cast<PK_ViewTransGrade>().ToList();
                        for (int j = 0; j < TransGradeList.Count; j++)
                        {
                            if ( (TransDetailList[i].TRANSID.ToString() == TransGradeList[j].TRANSID.ToString()) && (TransDetailList[i].KPINO.ToString() ) == (TransGradeList[j].KPINO.ToString()) && (TransDetailList[i].KPIcategory.ToString()) == (TransGradeList[j].KPIcategory.ToString()))
                            { 
                                IEnumTransGradesTemp.Add(TransGradeList[j]);                                
                            }
                        }
                        TransDetailList[i].PK_ViewTransGrades = IEnumTransGradesTemp;
                    }
                }
                catch
                {

                }
            }
            return TransHeader;
        }


        // GET api/values/1
        [Route("api/[controller]/evidences/{transID}/{kpiNO}")]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<PK_FileEvidence>>> GetEvidences(string transID,string kpiNO)
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;
            PK_Dashboard Dashboard = new PK_Dashboard();
            List<PK_FileEvidence> evidenceList = new List<PK_FileEvidence>();

            if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }

            if (spendingTimeWithCompany > 5)
            {
                try
                {
                    IEnumerable<PK_FileEvidence> IEnumFileEvidences = await this.PKTransProvider.GetFileEvidences(transID, kpiNO);
                    evidenceList= IEnumFileEvidences.ToList();
                }
                catch
                {

                }

            }
            else
            {
                try
                {
                    IEnumerable<PK_FileEvidence> IEnumFileEvidences = await this.PKTransProvider.GetFileEvidences(transID, kpiNO);
                    evidenceList = IEnumFileEvidences.ToList();
                }
                catch
                {

                }
            }

            return evidenceList;
        }


        // GET api/values/1
        [Route("api/[controller]/dashboard/{APREMPNIK}")]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult <PK_Dashboard>>  GetDashboard (string APREMPNIK )
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;
            PK_Dashboard Dashboard = new PK_Dashboard();

            if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }

            if (spendingTimeWithCompany > 5)
            {
                try
                {
                    //sp1
                    var getDashboard =  await this.PKTransProvider.GetDashboards(APREMPNIK);
                    Dashboard = getDashboard.ToList()[0];
                }
                catch
                {

                }

            }
            else
            {
                try
                {
                    //sp1
                    var getDashboard = await this.PKTransProvider.GetDashboards(APREMPNIK);
                    Dashboard = getDashboard.ToList()[0];
                }
                catch
                {

                }
            }

            return Dashboard;
        }


        [Route("api/[controller]/status")]
        // POST api/values
        [HttpPost]
        [Authorize]

        public async Task Post([FromBody]PK_TransStatus PKTransStatus)
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;

            if (currentUser.HasClaim(C => C.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }

            if (spendingTimeWithCompany > 5)
            {
                try
                {
                    await this.PKTransProvider.PK_UpdateStatus(PKTransStatus.TRANSID, PKTransStatus.APREMPNIK,PKTransStatus.STATUS,PKTransStatus.APREMPNIK);
                }
                catch
                {

                }
            }
            else
            {
                try
                {
                    await this.PKTransProvider.PK_UpdateStatus(PKTransStatus.TRANSID, PKTransStatus.APREMPNIK, PKTransStatus.STATUS,PKTransStatus.APREMPNIK);
                }
                catch
                {

                }
            }
        }

        [Route("api/[controller]")]
        // POST api/values
        [HttpPost]
        [Authorize]
        
        public async Task Post([FromBody]PK_TransHeader PKTrans)
        {
            
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;

            if (currentUser.HasClaim(C => C.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }

            if (spendingTimeWithCompany > 5)
            {
                try
                {                    
                    await this.PKTransProvider.PK_UpdateTransHeader(PKTrans.TRANSID, PKTrans.USEREMPNIK);                    
                    var tasks = Enumerable.Range(0, PKTrans.PK_TransDetails.Count).Select(async i =>
                    {
                        await this.PKTransProvider.PK_UpdateTransDetail(PKTrans.TRANSID, PKTrans.PK_TransDetails[i].KPINO, PKTrans.PK_TransDetails[i].GRADESCORE, PKTrans.PK_TransDetails[i].Evidence, PKTrans.PK_TransDetails[i].KPIcategory, PKTrans.USEREMPNIK);
                        var tasks2 = Enumerable.Range(0, PKTrans.PK_TransDetails[i].PK_FileEvidences.Count).Select(async j =>
                        {
                            await this.PKTransProvider.PK_UpdateFileEvidence(PKTrans.TRANSID, PKTrans.PK_TransDetails[i].KPINO, PKTrans.PK_TransDetails[i].PK_FileEvidences[j].fileName, PKTrans.PK_TransDetails[i].PK_FileEvidences[j].fileStream, PKTrans.PK_TransDetails[i].PK_FileEvidences[j].filePath, PKTrans.PK_TransDetails[i].PK_FileEvidences[j].fileType, PKTrans.USEREMPNIK);
                        }).ToList();
                    }).ToList();
                    tasks.ForEach(x => Console.WriteLine(x.IsCompleted));
                    Console.WriteLine("Done?");
                    Task.WaitAll(tasks.ToArray());


                    //for (int i = 0; i < PKTrans.PK_TransDetails.Count; i++)
                    //{
                    //    await this.PKTransProvider.PK_UpdateTransDetail(PKTrans.TRANSID, PKTrans.PK_TransDetails[i].KPINO, PKTrans.PK_TransDetails[i].GRADESCORE, PKTrans.PK_TransDetails[i].Evidence, PKTrans.PK_TransDetails[i].KPIcategory,PKTrans.USEREMPNIK);

                    //    for (int j = 0; j < PKTrans.PK_TransDetails[i].PK_FileEvidences.Count; j++)
                    //    {

                    //        await this.PKTransProvider.PK_UpdateFileEvidence(PKTrans.TRANSID, PKTrans.PK_TransDetails[i].KPINO, PKTrans.PK_TransDetails[i].PK_FileEvidences[j].fileName, PKTrans.PK_TransDetails[i].PK_FileEvidences[j].fileStream, PKTrans.PK_TransDetails[i].PK_FileEvidences[j].filePath, PKTrans.PK_TransDetails[i].PK_FileEvidences[j].fileType, PKTrans.USEREMPNIK);
                    //    }
                    //}
                    
                }
                catch
                {

                }
            }
            else
            {
                try
                {
                    await this.PKTransProvider.PK_UpdateTransHeader(PKTrans.TRANSID, PKTrans.USEREMPNIK);
                    var tasks = Enumerable.Range(0, PKTrans.PK_TransDetails.Count).Select(async i =>
                    {
                        await this.PKTransProvider.PK_UpdateTransDetail(PKTrans.TRANSID, PKTrans.PK_TransDetails[i].KPINO, PKTrans.PK_TransDetails[i].GRADESCORE, PKTrans.PK_TransDetails[i].Evidence, PKTrans.PK_TransDetails[i].KPIcategory, PKTrans.USEREMPNIK);
                        var tasks2 = Enumerable.Range(0, PKTrans.PK_TransDetails[i].PK_FileEvidences.Count).Select(async j =>
                        {
                            await this.PKTransProvider.PK_UpdateFileEvidence(PKTrans.TRANSID, PKTrans.PK_TransDetails[i].KPINO, PKTrans.PK_TransDetails[i].PK_FileEvidences[j].fileName, PKTrans.PK_TransDetails[i].PK_FileEvidences[j].fileStream, PKTrans.PK_TransDetails[i].PK_FileEvidences[j].filePath, PKTrans.PK_TransDetails[i].PK_FileEvidences[j].fileType, PKTrans.USEREMPNIK);
                        }).ToList();
                    }).ToList();
                    tasks.ForEach(x => Console.WriteLine(x.IsCompleted));
                    Console.WriteLine("Done?");
                    Task.WaitAll(tasks.ToArray());


                    //for (int i = 0; i < PKTrans.PK_TransDetails.Count; i++)
                    //{
                    //    await this.PKTransProvider.PK_UpdateTransDetail(PKTrans.TRANSID, PKTrans.PK_TransDetails[i].KPINO, PKTrans.PK_TransDetails[i].GRADESCORE, PKTrans.PK_TransDetails[i].Evidence, PKTrans.PK_TransDetails[i].KPIcategory, PKTrans.USEREMPNIK);

                    //    for (int j = 0; j < PKTrans.PK_TransDetails[i].PK_FileEvidences.Count; j++)
                    //    {

                    //        await this.PKTransProvider.PK_UpdateFileEvidence(PKTrans.TRANSID, PKTrans.PK_TransDetails[i].KPINO, PKTrans.PK_TransDetails[i].PK_FileEvidences[j].fileName, PKTrans.PK_TransDetails[i].PK_FileEvidences[j].fileStream, PKTrans.PK_TransDetails[i].PK_FileEvidences[j].filePath, PKTrans.PK_TransDetails[i].PK_FileEvidences[j].fileType, PKTrans.USEREMPNIK);
                    //    }

                    //}


                }
                catch
                {

                }
            }
            
        }

        [Route("api/[controller]/posAtt")]
        // POST api/values
        [HttpPost]
        [Authorize]
        public async Task Post([FromBody]PK_FileEvidences PKTransPosAtt)
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;

            if (currentUser.HasClaim(C => C.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }

            if (spendingTimeWithCompany > 5)
            {
                try
                {
                    await this.PKTransProvider.PK_UpdateFileEvidence(PKTransPosAtt.TRANSID, PKTransPosAtt.KPINO, PKTransPosAtt.fileName, PKTransPosAtt.fileStream, PKTransPosAtt.filePath, PKTransPosAtt.fileType, PKTransPosAtt.empNIK);
                }
                catch
                {

                }
            }
            else
            {
                try
                {
                    await this.PKTransProvider.PK_UpdateFileEvidence(PKTransPosAtt.TRANSID, PKTransPosAtt.KPINO, PKTransPosAtt.fileName, PKTransPosAtt.fileStream, PKTransPosAtt.filePath, PKTransPosAtt.fileType, PKTransPosAtt.empNIK);
                }
                catch
                {

                }
            }

        }

        [Route("api/[controller]/delAtt")]
        // POST api/values
        [HttpPost]
        [Authorize]
        public async Task Post([FromBody]PK_FileEvidence PKTransDelAtt)
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;

            if (currentUser.HasClaim(C => C.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }

            if (spendingTimeWithCompany > 5)
            {
                try
                {
                    await this.PKTransProvider.PK_DeleteFileEvidence(PKTransDelAtt.TRANSID, PKTransDelAtt.KPINO, PKTransDelAtt.fileName);
                }
                catch
                {

                }
            }
            else
            {
                try
                {
                    await this.PKTransProvider.PK_DeleteFileEvidence(PKTransDelAtt.TRANSID, PKTransDelAtt.KPINO, PKTransDelAtt.fileName);                    
                }
                catch
                {

                }
            }

        }


    }
}