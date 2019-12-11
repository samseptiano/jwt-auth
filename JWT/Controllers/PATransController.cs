using JWT.Context;
using JWT.DataProvider;
using JWT.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JWT.Controllers
{
    [Produces("application/json")]
    [ApiController]

    public class PATransController : Controller
    {
        private IPATransProvider PATransProvider;
        public PATransController(IPATransProvider PATransProvider)
        {
            this.PATransProvider = PATransProvider;
        }
         private DBContext db = new DBContext();

        [Route("api/[controller]/{empNIK}/{tahun}")]
        // GET api/values
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<PA_ViewTransHeader>> GetTransHeaders( String empNIK,String tahun )
        {
            PA_ViewTransHeader TransHeader = new PA_ViewTransHeader();
            List<PA_ViewTransDetail> TransDetail = new List<PA_ViewTransDetail>();

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
                    var TransHeaderArray = await this.PATransProvider.GetTransHeaders(empNIK, tahun);
                    TransHeader = TransHeaderArray.ToList()[0];
                    //sp2
                    var IEnumTransDetails = await this.PATransProvider.GetTransDetails(empNIK, tahun);
                    List<PA_ViewTransDetail> TransDetailList = IEnumTransDetails.ToList();
                    TransHeader.PA_ViewTransDetail = TransDetailList;

                    for (int i = 0; i < TransDetailList.Count; i++)
                    {
                        List<PA_ViewTransGrade> IEnumTransGradesTemp = new List<PA_ViewTransGrade>();
                        IEnumerable<PA_ViewTransGrade> IEnumTransGrades = await this.PATransProvider.GetTransGrades(TransDetailList[i].Id, TransDetailList[i].KPIType);
                        List<PA_ViewTransGrade> TransGradeList = IEnumTransGrades.Cast<PA_ViewTransGrade>().ToList();
                        TransHeader.PA_ViewTransDetail[i].PA_ViewTransGrades = TransGradeList;
                        //get devplan header
                        if (TransHeader.PA_ViewTransDetail[i].KPIType.Equals("KUALITATIF"))
                        {
                            //get devplan Detail
                            var DevPlanH = await this.PATransProvider.GetDevPlanHeader(TransHeader.paId, TransHeader.PA_ViewTransDetail[i].Id);
                            TransHeader.PA_DevPlanH = DevPlanH.ToList();
                            for (int j=0;j < TransHeader.PA_DevPlanH.Count;j++) {
                                var DevPlanD = await this.PATransProvider.GetDevPlanDetail(TransHeader.PA_DevPlanH[j].COMPID, TransHeader.PA_DevPlanH[j].PAID);
                                TransHeader.PA_DevPlanH[j].devPlanDetail = DevPlanD.ToList();
                            }
                        }
                       
                    }

                    var MDevPlan = await this.PATransProvider.GetMDevPlan();
                    TransHeader.PA_MDevPlan = MDevPlan.ToList();

                    

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
                    var TransHeaderArray = await this.PATransProvider.GetTransHeaders(empNIK, tahun);
                    TransHeader = TransHeaderArray.ToList()[0];
                    //sp2
                    var IEnumTransDetails = await this.PATransProvider.GetTransDetails(empNIK, tahun);
                    List<PA_ViewTransDetail> TransDetailList = IEnumTransDetails.ToList();
                    TransHeader.PA_ViewTransDetail = TransDetailList;

                    for (int i = 0; i < TransDetailList.Count; i++)
                    {
                        List<PA_ViewTransGrade> IEnumTransGradesTemp = new List<PA_ViewTransGrade>();
                        var IEnumTransGrades = await this.PATransProvider.GetTransGrades(TransDetailList[i].Id, TransDetailList[i].KPIType);
                        TransHeader.PA_ViewTransDetail[i].PA_ViewTransGrades = IEnumTransGrades.ToList();


                        if (TransHeader.PA_ViewTransDetail[i].KPIType.Equals("KUALITATIF"))
                        {
                            //get devplan Detail
                            var DevPlanH = await this.PATransProvider.GetDevPlanHeader(TransHeader.paId, TransHeader.PA_ViewTransDetail[i].Id);
                            TransHeader.PA_DevPlanH = DevPlanH.ToList();
                            for (int j = 0; j < TransHeader.PA_DevPlanH.Count; j++)
                            {
                                var DevPlanD = await this.PATransProvider.GetDevPlanDetail(TransHeader.PA_DevPlanH[j].COMPID, TransHeader.PA_DevPlanH[j].PAID);
                                TransHeader.PA_DevPlanH[j].devPlanDetail = DevPlanD.ToList();
                            }
                        }

                    }

                    var MDevPlan = await this.PATransProvider.GetMDevPlan();
                    TransHeader.PA_MDevPlan = MDevPlan.ToList();

                }
                catch
                {
                }
            }
            return TransHeader;
        }


        //// GET api/values/1
        [Route("api/[controller]/evidences/{PAID}/{KPIID}/{KPITYPE}/{SEMESTER}")]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<PA_FileEvidence>>> GetEvidences(string PAID, string KPIID, string KPITYPE, string SEMESTER)
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;
            PK_Dashboard Dashboard = new PK_Dashboard();
            List<PA_FileEvidence> evidenceList = new List<PA_FileEvidence>();

            if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }

            if (spendingTimeWithCompany > 5)
            {
                try
                {
                    IEnumerable<PA_FileEvidence> IEnumFileEvidences = await this.PATransProvider.GetFileEvidences(PAID,KPIID,KPITYPE,SEMESTER);
                    evidenceList = IEnumFileEvidences.ToList();
                }
                catch
                {
                }

            }
            else
            {
                try
                {
                    IEnumerable<PA_FileEvidence> IEnumFileEvidences = await this.PATransProvider.GetFileEvidences(PAID, KPIID, KPITYPE,SEMESTER);
                    evidenceList = IEnumFileEvidences.ToList();
                }
                catch
                {
                }
            }

            return evidenceList;
        }






        //======================================================================


        [Route("api/[controller]/evidences/{fileName}/{ext}")]
        [HttpGet]
        public async Task<IActionResult> GetFile(string fileName, string ext)
        {

            var image = System.IO.File.OpenRead(".\\Resources\\9e53f6262b237c8e6e0efd190824be1b\\" + fileName);

            if (ext.Equals(".pdf"))
            {
                FileContentResult result = new FileContentResult(System.IO.File.ReadAllBytes(".\\Resources\\9e53f6262b237c8e6e0efd190824be1b\\" + fileName), "application/pdf")
                {
                    FileDownloadName = fileName
                };

                return result;

            }
            else if (ext.Equals(".doc") || ext.Equals(".docx") || ext.Equals(".xls") || ext.Equals(".xlsx") || ext.Equals(".ppt") || ext.Equals(".pptx"))
            {
                FileContentResult result = new FileContentResult(System.IO.File.ReadAllBytes(".\\Resources\\9e53f6262b237c8e6e0efd190824be1b\\" + fileName), "application/msword")
                {
                    FileDownloadName = fileName
                };

                return result;

            }
            else if (ext.Equals(".jpg") || ext.Equals(".jpeg"))
            {
                return File(image, "image/jpeg");

            }

            return File(image, "application/pdf");

        }

        [Route("api/[controller]/evidences/dl/{fileName}/{ext}")]
        [HttpGet]
        public async Task<IActionResult> GetFileDl(string fileName, string ext)
        {

            var image = System.IO.File.OpenRead(".\\Resources\\9e53f6262b237c8e6e0efd190824be1b\\" + fileName);

            if (ext.Equals(".pdf"))
            {
                FileContentResult result = new FileContentResult(System.IO.File.ReadAllBytes(".\\Resources\\9e53f6262b237c8e6e0efd190824be1b\\" + fileName), "application/pdf")
                {
                    FileDownloadName = fileName
                };

                return result;

            }
            else if (ext.Equals(".doc") || ext.Equals(".docx") || ext.Equals(".xls") || ext.Equals(".xlsx") || ext.Equals(".ppt") || ext.Equals(".pptx"))
            {
                FileContentResult result = new FileContentResult(System.IO.File.ReadAllBytes(".\\Resources\\9e53f6262b237c8e6e0efd190824be1b\\" + fileName), "application/msword")
                {
                    FileDownloadName = fileName
                };

                return result;

            }
            else if (ext.Equals(".jpg") || ext.Equals(".jpeg"))
            {
                FileContentResult result = new FileContentResult(System.IO.File.ReadAllBytes(".\\Resources\\9e53f6262b237c8e6e0efd190824be1b\\" + fileName), "image/jpeg")
                {
                    FileDownloadName = fileName
                };

                return result;

            }

            return File(image, "application/pdf");

        }

        [Route("api/[controller]/evidences/del/{fileName}")]
        [HttpGet]
        public void DeleteFile(string fileName)
        {

            //System.IO.File.SetAttributes("Resources/9e53f6262b237c8e6e0efd190824be1b/" + fileName, FileAttributes.Normal);
            //System.IO.File.Delete("Resources/9e53f6262b237c8e6e0efd190824be1b/" + fileName);

            //File.SetAttributes(file, FileAttributes.Normal);
            System.IO.File.Delete(@"Resources\9e53f6262b237c8e6e0efd190824be1b\" + fileName);

        }


        [Route("api/[controller]/evidences/post")]
        // POST api/values
        [HttpPost]
        public async Task<ActionResult> postFileAsync([FromForm]fileUpload std)
        {          
            // Getting Image
            var image = std.Image;
            // Saving Image on Server
            if (image.Length > 0)
            {
                using (var fileStream = new FileStream(Path.Combine(".\\Resources\\9e53f6262b237c8e6e0efd190824be1b\\", std.fileName+std.extension), FileMode.Create))
                {
                    image.CopyTo(fileStream);
                    await this.PATransProvider.PA_UpdateFileEvidence(std.PAID, std.KPIID, std.COMPID, std.SEMESTER, "", std.extension, std.fileName + std.extension, std.EMPNIK);

                }
            }
            return Ok(new { status = true, message = "File Posted Successfully" });
        }

        //=======================================================================





        //// GET api/values/1
        [Route("api/[controller]/textEvidences/{PAID}/{KPIID}/{KPITYPE}/{SEMESTER}")]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<PA_TextEvidence>>> GetTextEvidences(string PAID, string KPIID, string KPITYPE, string SEMESTER)
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;
            PK_Dashboard Dashboard = new PK_Dashboard();
            List<PA_TextEvidence> evidenceList = new List<PA_TextEvidence>();

            if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }

            if (spendingTimeWithCompany > 5)
            {
                try
                {
                    IEnumerable<PA_TextEvidence> IEnumFileEvidences = await this.PATransProvider.GetTextEvidences(PAID, KPIID, KPITYPE, SEMESTER);
                    evidenceList = IEnumFileEvidences.ToList();
                }
                catch
                {
                }

            }
            else
            {
                try
                {
                    IEnumerable<PA_TextEvidence> IEnumFileEvidences = await this.PATransProvider.GetTextEvidences(PAID, KPIID, KPITYPE, SEMESTER);
                    evidenceList = IEnumFileEvidences.ToList();
                }
                catch
                {
                }
            }

            return evidenceList;
        }



        //// GET api/values/1
        //[Route("api/[controller]/dashboard/{APREMPNIK}")]
        //[HttpGet]
        //[Authorize]
        //public async Task<ActionResult <PK_Dashboard>>  GetDashboard (string APREMPNIK )
        //{
        //    var currentUser = HttpContext.User;
        //    int spendingTimeWithCompany = 0;
        //    PK_Dashboard Dashboard = new PK_Dashboard();

        //    if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
        //    {
        //        DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
        //        spendingTimeWithCompany = DateTime.Today.Year - date.Year;
        //    }

        //    if (spendingTimeWithCompany > 5)
        //    {
        //        try
        //        {
        //            //sp1
        //            var getDashboard =  await this.PKTransProvider.GetDashboards(APREMPNIK);
        //            Dashboard = getDashboard.ToList()[0];
        //        }
        //        catch
        //        {
        //        }

        //    }
        //    else
        //    {
        //        try
        //        {
        //            //sp1
        //            var getDashboard = await this.PKTransProvider.GetDashboards(APREMPNIK);
        //            Dashboard = getDashboard.ToList()[0];
        //        }
        //        catch
        //        {
        //        }
        //    }

        //    return Dashboard;
        //}


        [Route("api/[controller]/statusPA")]
        // POST api/values
        [HttpPost]
        [Authorize]

        public async Task Post([FromBody]PA_TransDetailPostList PKTrans)
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
                    await this.PATransProvider.PA_UpdateStatus(PKTrans.STRENGTH, PKTrans.PAID, PKTrans.NIKBAWAHAN, PKTrans.STATUS);
                    //await this.PATransProvider.deleteDevPlan(PKTrans.PAID);
                    await this.PATransProvider.PA_UpdateTransHeader(PKTrans.STRENGTH, PKTrans.PAID, PKTrans.NIKBAWAHAN, PKTrans.STATUS);
                    var tasks = Enumerable.Range(0, PKTrans.lTransDetail.Count).Select(async i =>
                    {
                        await this.PATransProvider.PA_UpdateTransDetail(PKTrans.lTransDetail[i].PAID, PKTrans.lTransDetail[i].KPIID, PKTrans.lTransDetail[i].COMPID, PKTrans.lTransDetail[i].CP, PKTrans.lTransDetail[i].EVIDENCES, PKTrans.lTransDetail[i].ACTUAL, PKTrans.lTransDetail[i].TARGET, PKTrans.lTransDetail[i].SEMESTER, PKTrans.lTransDetail[i].KPITYPE, PKTrans.lTransDetail[i].EMPNIK);
                    }).ToList();
                    var tasks2 = Enumerable.Range(0, PKTrans.lDevPlanHeader.Count).Select(async i =>
                    {
                        await this.PATransProvider.postDevPlanHeader(PKTrans.lDevPlanHeader[i].PAID, PKTrans.lDevPlanHeader[i].COMPID, PKTrans.lTransDetail[i].EMPNIK);
                        var tasks3 = Enumerable.Range(0, PKTrans.lDevPlanHeader[i].devPlanDetail.Count).Select(async j =>
                        {
                            await this.PATransProvider.postDevPlanDetail(PKTrans.lDevPlanHeader[i].devPlanDetail[j].DEVID, PKTrans.lDevPlanHeader[i].devPlanDetail[j].DEVPLANACTIVITIES,
                                PKTrans.lDevPlanHeader[i].devPlanDetail[j].DEVPLANKPI, PKTrans.lDevPlanHeader[i].devPlanDetail[j].DEVPLANDUEDATE, PKTrans.lDevPlanHeader[i].devPlanDetail[j].DEVPLANMENTOR, PKTrans.lDevPlanHeader[i].devPlanDetail[j].DEVPLANACHIEVEMENT,
                                PKTrans.lDevPlanHeader[i].devPlanDetail[j].DEVPLANSTATUS, PKTrans.lTransDetail[i].EMPNIK, PKTrans.lDevPlanHeader[i].devPlanDetail[j].DEVPLANMETHODID, PKTrans.lDevPlanHeader[i].PAID);
                        }).ToList();
                    }).ToList();
                    await this.PATransProvider.hitungPA(PKTrans.PAID);
                    tasks.ForEach(x => Console.WriteLine(x.IsCompleted));
                    Console.WriteLine("Done?");
                    Task.WaitAll(tasks.ToArray());
                }
                catch
                {

                }
            }
            else
            {
                try
                {
                    await this.PATransProvider.deleteDevPlan(PKTrans.PAID);
                    await this.PATransProvider.PA_UpdateTransHeader(PKTrans.STRENGTH, PKTrans.PAID, PKTrans.NIKBAWAHAN, PKTrans.STATUS);
                    var tasks = Enumerable.Range(0, PKTrans.lTransDetail.Count).Select(async i =>
                    {
                        await this.PATransProvider.PA_UpdateTransDetail(PKTrans.lTransDetail[i].PAID, PKTrans.lTransDetail[i].KPIID, PKTrans.lTransDetail[i].COMPID, PKTrans.lTransDetail[i].CP, PKTrans.lTransDetail[i].EVIDENCES, PKTrans.lTransDetail[i].ACTUAL, PKTrans.lTransDetail[i].TARGET, PKTrans.lTransDetail[i].SEMESTER, PKTrans.lTransDetail[i].KPITYPE, PKTrans.lTransDetail[i].EMPNIK);
                    }).ToList();
                    var tasks2 = Enumerable.Range(0, PKTrans.lDevPlanHeader.Count).Select(async i =>
                    {
                        await this.PATransProvider.postDevPlanHeader(PKTrans.lDevPlanHeader[i].PAID, PKTrans.lDevPlanHeader[i].COMPID, PKTrans.lTransDetail[i].EMPNIK);
                        var tasks3 = Enumerable.Range(0, PKTrans.lDevPlanHeader[i].devPlanDetail.Count).Select(async j =>
                        {
                            await this.PATransProvider.postDevPlanDetail(PKTrans.lDevPlanHeader[i].devPlanDetail[j].DEVID, PKTrans.lDevPlanHeader[i].devPlanDetail[j].DEVPLANACTIVITIES,
                                PKTrans.lDevPlanHeader[i].devPlanDetail[j].DEVPLANKPI, PKTrans.lDevPlanHeader[i].devPlanDetail[j].DEVPLANDUEDATE, PKTrans.lDevPlanHeader[i].devPlanDetail[j].DEVPLANMENTOR, PKTrans.lDevPlanHeader[i].devPlanDetail[j].DEVPLANACHIEVEMENT,
                                PKTrans.lDevPlanHeader[i].devPlanDetail[j].DEVPLANSTATUS, PKTrans.lTransDetail[i].EMPNIK, PKTrans.lDevPlanHeader[i].devPlanDetail[j].DEVPLANMETHODID, PKTrans.lDevPlanHeader[i].PAID);
                        }).ToList();
                    }).ToList();
                    await this.PATransProvider.hitungPA(PKTrans.PAID);
                    await this.PATransProvider.PA_UpdateStatus(PKTrans.STRENGTH, PKTrans.PAID, PKTrans.NIKBAWAHAN, PKTrans.STATUS);
                    tasks.ForEach(x => Console.WriteLine(x.IsCompleted));
                    Console.WriteLine("Done?");
                    Task.WaitAll(tasks.ToArray());
                }
                catch
                {

                }
            }
        }

        [Route("api/[controller]/postKPI")]
        // POST api/values
        [HttpPost]
        [Authorize]

        public async Task PostKPIAnswer([FromBody]PA_TransDetailPostList PKTrans)
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
                    await this.PATransProvider.deleteDevPlan(PKTrans.PAID);
                    await this.PATransProvider.PA_UpdateTransHeader(PKTrans.STRENGTH, PKTrans.PAID, PKTrans.NIKBAWAHAN, PKTrans.STATUS);
                    var tasks = Enumerable.Range(0, PKTrans.lTransDetail.Count).Select(async i =>
                    {
                        await this.PATransProvider.PA_UpdateTransDetail(PKTrans.lTransDetail[i].PAID, PKTrans.lTransDetail[i].KPIID, PKTrans.lTransDetail[i].COMPID, PKTrans.lTransDetail[i].CP, PKTrans.lTransDetail[i].EVIDENCES, PKTrans.lTransDetail[i].ACTUAL, PKTrans.lTransDetail[i].TARGET, PKTrans.lTransDetail[i].SEMESTER, PKTrans.lTransDetail[i].KPITYPE, PKTrans.lTransDetail[i].EMPNIK);
                    }).ToList();
                    var tasks2 = Enumerable.Range(0, PKTrans.lDevPlanHeader.Count).Select(async i =>
                    {
                        await this.PATransProvider.postDevPlanHeader(PKTrans.lDevPlanHeader[i].PAID, PKTrans.lDevPlanHeader[i].COMPID, PKTrans.lTransDetail[i].EMPNIK);
                        var tasks3 = Enumerable.Range(0, PKTrans.lDevPlanHeader[i].devPlanDetail.Count).Select(async j =>
                        {
                            await this.PATransProvider.postDevPlanDetail(PKTrans.lDevPlanHeader[i].devPlanDetail[j].DEVID, PKTrans.lDevPlanHeader[i].devPlanDetail[j].DEVPLANACTIVITIES,
                                PKTrans.lDevPlanHeader[i].devPlanDetail[j].DEVPLANKPI, PKTrans.lDevPlanHeader[i].devPlanDetail[j].DEVPLANDUEDATE, PKTrans.lDevPlanHeader[i].devPlanDetail[j].DEVPLANMENTOR, PKTrans.lDevPlanHeader[i].devPlanDetail[j].DEVPLANACHIEVEMENT,
                                PKTrans.lDevPlanHeader[i].devPlanDetail[j].DEVPLANSTATUS, PKTrans.lTransDetail[i].EMPNIK, PKTrans.lDevPlanHeader[i].devPlanDetail[j].DEVPLANMETHODID, PKTrans.lDevPlanHeader[i].PAID);
                        }).ToList();
                    }).ToList();
                    await this.PATransProvider.hitungPA(PKTrans.PAID);
                    tasks.ForEach(x => Console.WriteLine(x.IsCompleted));
                    Console.WriteLine("Done?");
                    Task.WaitAll(tasks.ToArray());


                    //for (int i = 0; i < PKTrans.lTransDetail.Count; i++)
                    //{
                    //    await this.PATransProvider.PA_UpdateTransDetail(PKTrans.lTransDetail[i].PAID, PKTrans.lTransDetail[i].KPIID, PKTrans.lTransDetail[i].COMPID, PKTrans.lTransDetail[i].CP, PKTrans.lTransDetail[i].EVIDENCES, PKTrans.lTransDetail[i].SEMESTER, PKTrans.lTransDetail[i].KPITYPE, PKTrans.lTransDetail[i].EMPNIK);
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
                    await this.PATransProvider.deleteDevPlan(PKTrans.PAID);
                    await this.PATransProvider.PA_UpdateTransHeader(PKTrans.STRENGTH, PKTrans.PAID, PKTrans.NIKBAWAHAN, PKTrans.STATUS);
                    var tasks = Enumerable.Range(0, PKTrans.lTransDetail.Count).Select(async i =>
                    {
                        await this.PATransProvider.PA_UpdateTransDetail(PKTrans.lTransDetail[i].PAID, PKTrans.lTransDetail[i].KPIID, PKTrans.lTransDetail[i].COMPID, PKTrans.lTransDetail[i].CP, PKTrans.lTransDetail[i].EVIDENCES, PKTrans.lTransDetail[i].ACTUAL, PKTrans.lTransDetail[i].TARGET, PKTrans.lTransDetail[i].SEMESTER, PKTrans.lTransDetail[i].KPITYPE, PKTrans.lTransDetail[i].EMPNIK);
                    }).ToList();
                    var tasks2 = Enumerable.Range(0, PKTrans.lDevPlanHeader.Count).Select(async i =>
                    {
                        await this.PATransProvider.postDevPlanHeader(PKTrans.lDevPlanHeader[i].PAID, PKTrans.lDevPlanHeader[i].COMPID, PKTrans.lTransDetail[i].EMPNIK);
                        var tasks3 = Enumerable.Range(0, PKTrans.lDevPlanHeader[i].devPlanDetail.Count).Select(async j =>
                        {
                            await this.PATransProvider.postDevPlanDetail(PKTrans.lDevPlanHeader[i].devPlanDetail[j].DEVID, PKTrans.lDevPlanHeader[i].devPlanDetail[j].DEVPLANACTIVITIES,
                                PKTrans.lDevPlanHeader[i].devPlanDetail[j].DEVPLANKPI, PKTrans.lDevPlanHeader[i].devPlanDetail[j].DEVPLANDUEDATE, PKTrans.lDevPlanHeader[i].devPlanDetail[j].DEVPLANMENTOR, PKTrans.lDevPlanHeader[i].devPlanDetail[j].DEVPLANACHIEVEMENT,
                                PKTrans.lDevPlanHeader[i].devPlanDetail[j].DEVPLANSTATUS, PKTrans.lTransDetail[i].EMPNIK, PKTrans.lDevPlanHeader[i].devPlanDetail[j].DEVPLANMETHODID, PKTrans.lDevPlanHeader[i].PAID);
                        }).ToList();
                    }).ToList();
                    await this.PATransProvider.hitungPA(PKTrans.PAID);
                    tasks.ForEach(x => Console.WriteLine(x.IsCompleted));
                    Console.WriteLine("Done?");
                    Task.WaitAll(tasks.ToArray());


                    //for (int i = 0; i < PKTrans.lTransDetail.Count; i++)
                    //{
                    //    await this.PATransProvider.PA_UpdateTransDetail(PKTrans.lTransDetail[i].PAID, PKTrans.lTransDetail[i].KPIID, PKTrans.lTransDetail[i].COMPID, PKTrans.lTransDetail[i].CP, PKTrans.lTransDetail[i].EVIDENCES, PKTrans.lTransDetail[i].SEMESTER, PKTrans.lTransDetail[i].KPITYPE, PKTrans.lTransDetail[i].EMPNIK);
                    //}
                }
                catch
                {

                }
            }

        }

        [Route("api/[controller]/postEvidences")]
        // POST api/values
        [HttpPost]
        [Authorize]
        public async Task Post([FromBody]PA_FileEvidencesPost PATransPosAtt)
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
                    await this.PATransProvider.PA_UpdateFileEvidence(PATransPosAtt.PAID, PATransPosAtt.KPIID, PATransPosAtt.COMPID, PATransPosAtt.SEMESTER, PATransPosAtt.FILESTRING, PATransPosAtt.FILEEXT, PATransPosAtt.FILENAME, PATransPosAtt.EMPNIK);
                }
                catch
                {

                }
            }
            else
            {
                try
                {
                    await this.PATransProvider.PA_UpdateFileEvidence(PATransPosAtt.PAID, PATransPosAtt.KPIID, PATransPosAtt.COMPID, PATransPosAtt.SEMESTER, PATransPosAtt.FILESTRING, PATransPosAtt.FILEEXT, PATransPosAtt.FILENAME, PATransPosAtt.EMPNIK);
                }
                catch
                {

                }
            }

        }

        [Route("api/[controller]/deleteEvidences")]
        // POST api/values
        [HttpPost]
        [Authorize]
        public async Task deleteEvidence([FromBody]PA_FileEvidencesPost PATransDelAtt)
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
                    await this.PATransProvider.PA_DeleteFileEvidence(PATransDelAtt.PAID, PATransDelAtt.KPIID, PATransDelAtt.COMPID, PATransDelAtt.SEMESTER, PATransDelAtt.FILENAME, PATransDelAtt.FILESTRING);
                    DeleteFile(PATransDelAtt.FILENAME);
                }
                catch
                {

                }
            }
            else
            {
                try
                {
                    await this.PATransProvider.PA_DeleteFileEvidence(PATransDelAtt.PAID, PATransDelAtt.KPIID, PATransDelAtt.COMPID, PATransDelAtt.SEMESTER, PATransDelAtt.FILENAME, PATransDelAtt.FILESTRING);
                    DeleteFile(PATransDelAtt.FILENAME);
                }
                catch
                {

                }
            }

        }


    }
}