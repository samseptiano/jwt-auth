using IronBarCode;
using JWT.Context;
using JWT.DataProvider;
using JWT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace JWT.Controllers
{
    [Produces("application/json")]
    [ApiController]
    //[Route("api/[controller]")]

    public class CertificateController : Controller
    {
        private DBContext db = new DBContext();               
        private IUserProvider userProvider;
        private readonly IPdfSharpService _pdfService;
        private readonly IMigraDocService _migraDocService;
        private ISurveyAnswerPesertaProvider surveyAnswerPesertaProvider;

        public CertificateController(IPdfSharpService pdfService, IMigraDocService migraDocService, ISurveyAnswerPesertaProvider surveyAnswerPesertaProvider, IUserProvider userProvider)
        {
            System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            _pdfService = pdfService;
            _migraDocService = migraDocService;

            this.userProvider = userProvider;
            this.surveyAnswerPesertaProvider = surveyAnswerPesertaProvider;
        }
        [HttpGet]
        [Route("api/[controller]")]
        [Authorize]
        public String Index()
        {
            return "success";
        }

        //[Route("CreatePdf")]
        [Route("api/[controller]/{empNIK}/{id}")]
        [HttpGet]
        [Authorize]
        public async Task<String> CreatePdf(String empNIK, int id)
        {
            FileStream stream;
            String path;
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;
            List<Certificate> evtps = new List<Certificate> { };
            UserData usr;
            String titletraining;

            if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }

            if (spendingTimeWithCompany > 5)
            {
                //sp1
                IEnumerable<EventSessionSurvey> eventSessionSurvey = await this.surveyAnswerPesertaProvider.GetEventSessionSurvey(id, empNIK);
                List<EventSessionSurvey> aaa = eventSessionSurvey.Cast<EventSessionSurvey>().ToList();
                
                //sp2 
                var xxx = await this.userProvider.GetUser(empNIK);
                usr = xxx.ToList()[0];


                String month = romanNumeral(aaa[0].SessionDateStart.ToString().Substring(0, 2));
                String bulan = Bulan(aaa[0].SessionDateStart.ToString().Substring(0, 2));

                evtps = (from e in db.EventPeserta
                             // join u in db.User on e.EmpNIK equals u.EmpNIK
                         join x in db.Event on e.EventID equals x.EventID into sq
                         from r in sq.DefaultIfEmpty()
                         where e.EmpNIK == empNIK && e.EventID == id
                         select new Certificate
                         {
                             eventId = e.EventID.ToString(),
                             TraineeName = usr.empName,
                             TraineeNIK = usr.empNiK,
                             TraineejobTitle = "Peserta",
                             TrainingName = r.EventName,
                             TrainerName = aaa[0].InstructorName,
                             TrainerJobTitle = "Trainer",
                             TrainingDate = aaa[0].SessionDateStart.ToString().Substring(0, 2) + " " + bulan + " " + aaa[0].SessionDateStart.ToString().Substring(6, 4),
                             DocumentCode = r.EventCode,
                             CompanyName = "PT. Enseval Putera Megatrading Tbk.",
                             Location = "Jakarta",
                             Barcode = r.trainerSignature,
                             Topics = r.EventTopic
                         }).ToList();

                //evtps = db.SurveyAnswer.Find(id);
                //evtps = db.User.Where(x => x.EmpNIK == id && x.FGActiveYN == "Y").ToList<User>();
                if (evtps == null)
                {
                    //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                }

                titletraining = evtps[0].TrainingName;

                var data = new PdfData
                {
                    DocumentTitle = "Training Certificate",
                    Trainee = evtps[0].TraineeName,
                    TraineeNIK = evtps[0].TraineeName, //evtps[0].EmpNIK
                    TraineejobTitle = evtps[0].TraineejobTitle,
                    TrainingName = evtps[0].TrainingName,
                    TrainingDate = aaa[0].SessionDateStart.ToString().Substring(0, 2) + " " + bulan + " " + aaa[0].SessionDateStart.ToString().Substring(6, 4),
                    Trainer = aaa[0].InstructorName,
                    TrainerJobTitle = evtps[0].TrainerJobTitle,
                    CompanyName = evtps[0].CompanyName,
                    Location = evtps[0].Location,
                    DocCode = evtps[0].TraineeNIK + "/" + month + "/EPM-TRN/" + DateTime.Now.ToString("yyyy").Substring(2, 2),
                    Topics = evtps[0].Topics.Split('-').Reverse().ToList<string>(),
                    Barcode = evtps[0].Barcode

                };
                GenerateBarcode(data);
                //path = LoadImage(data);
                path = _pdfService.CreatePdf(data);
                Console.WriteLine(path);
            }
            else
            {

                IEnumerable<EventSessionSurvey> eventSessionSurvey = await this.surveyAnswerPesertaProvider.GetEventSessionSurvey(id, empNIK);
                List<EventSessionSurvey> aaa = eventSessionSurvey.Cast<EventSessionSurvey>().ToList();
                var xxx = await this.userProvider.GetUser(empNIK);
                usr = xxx.ToList()[0];

                String month = romanNumeral(aaa[0].SessionDateStart.ToString().Substring(0, 2));
                String bulan = Bulan(aaa[0].SessionDateStart.ToString().Substring(0, 2));

                evtps = (from e in db.EventPeserta
                             // join u in db.User on e.EmpNIK equals u.EmpNIK
                         join x in db.Event on e.EventID equals x.EventID into sq
                         from r in sq.DefaultIfEmpty()
                         where e.EmpNIK == empNIK && e.EventID == id
                         select new Certificate
                         {
                             eventId = e.EventID.ToString(),
                             TraineeName = usr.empName,
                             TraineeNIK = usr.empNiK,
                             TraineejobTitle = "Peserta",
                             TrainingName = r.EventName,
                             TrainerName = aaa[0].InstructorName,
                             TrainerJobTitle = "Trainer",
                             TrainingDate = aaa[0].SessionDateStart.ToString().Substring(8, 2) + " " + bulan + " " + aaa[0].SessionDateStart.ToString().Substring(6, 4),
                             DocumentCode = r.EventCode,
                             CompanyName = "PT. Enseval Putera Megatrading Tbk.",
                             Location = "Jakarta",
                             Barcode = r.trainerSignature,
                             Topics = r.EventTopic
                         }).ToList();

                //evtps = db.SurveyAnswer.Find(id);
                //evtps = db.User.Where(x => x.EmpNIK == id && x.FGActiveYN == "Y").ToList<User>();
                if (evtps == null)
                {
                    //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                }

                titletraining = evtps[0].TrainingName;

                var data = new PdfData
                {
                    DocumentTitle = "Training Certificate",
                    Trainee = evtps[0].TraineeName,
                    TraineeNIK = evtps[0].TraineeName, //evtps[0].EmpNIK
                    TraineejobTitle = evtps[0].TraineejobTitle,
                    TrainingName = evtps[0].TrainingName,
                    TrainingDate = aaa[0].SessionDateStart.ToString().Substring(8, 2) + " " + bulan + " " + aaa[0].SessionDateStart.ToString().Substring(6, 4),
                    Trainer = aaa[0].InstructorName,
                    TrainerJobTitle = evtps[0].TrainerJobTitle,
                    CompanyName = evtps[0].CompanyName,
                    Location = evtps[0].Location,
                    DocCode = evtps[0].TraineeNIK + "/" + month + "/EPM-TRN/" + DateTime.Now.ToString("yyyy").Substring(2, 2),
                    Topics = evtps[0].Topics.Split('-').Reverse().ToList<string>(),
                    Barcode = evtps[0].Barcode

                };
                GenerateBarcode(data);
                //path = LoadImage(data);
                path = _pdfService.CreatePdf(data);
                Console.WriteLine(path);


            }

            //byte[] fileBytes= { };
            //try
            //{
            //     fileBytes= System.IO.File.ReadAllBytes("Resources/Output/Training Certificate.pdf");

            //    string fileName = ("Resources/Output/Training Certificate.pdf");

            //    if (fileName != null || fileName != string.Empty)
            //    {
            //        if ((System.IO.File.Exists(fileName)))
            //        {
            //            System.IO.File.Delete(fileName);
            //        }

            //    }


            //}
            //catch {
            //}


            FileContentResult result = new FileContentResult(System.IO.File.ReadAllBytes("Resources/Output/Training Certificate.pdf"), "application/pdf")
            {
                FileDownloadName = "Training Certificate.pdf"
            };

            //return result;

            //return FileResult(fileBytes, "application/octet-stream", "Certificate.pdf");


            //============================
            //stream = new FileStream(path, FileMode.OpenOrCreate);
            //return File(stream, "application/pdf", "fff.pdf");
            //===================================
            using (MailMessage mm = new MailMessage("hr.service@enseval.com", usr.empEmail))
            {
                mm.Subject = "Certificate";
                mm.Body = "Thank you for attending " + titletraining + " training.  Here is your certificate:";

                Attachment data = new Attachment("Resources/Output/Training Certificate.pdf", MediaTypeNames.Application.Octet);
                mm.IsBodyHtml = false;
                mm.Attachments.Add(data);
                SmtpClient smtp = new SmtpClient("mail.enseval.com", 25);
                smtp.EnableSsl = false;
                NetworkCredential NetworkCred = new NetworkCredential("hr.service@enseval.com", "");
                smtp.UseDefaultCredentials = false;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = NetworkCred;
                smtp.Send(mm);

                //ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Email sent.');", true);
            }

            return "file sent successfully";

        }




        private void GenerateBarcode(PdfData data)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);

            QRCodeData qrCodeData = qrGenerator.CreateQrCode("{'nama peserta':'" + data.Trainee + "','nik':'" + data.TraineeNIK + "','nama training':'" + data.TrainingName + "'}", QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            var bitmapBytes = BitmapToBytes(qrCodeImage); //Convert bitmap into a byte array
            Image image;
            using (MemoryStream ms = new MemoryStream(bitmapBytes))
            {
                image = Image.FromStream(ms);
                try
                {
                    image.Save("Resources/Images/qrr.jpg");
                    //path = _pdfService.CreatePdf(data);
                }
                catch
                {

                }
            }
        }

        private static byte[] BitmapToBytes(Bitmap img)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }




        private string LoadImage(PdfData data)
        {
            string path = "";
            //data:image/gif;base64,
            //this image is a single pixel (black)
            byte[] bytes = Convert.FromBase64String("R0lGODlhAQABAIAAAAAAAAAAACH5BAAAAAAALAAAAAABAAEAAAICTAEAOw==");

            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
                image.Save("sss.jpg");
                path = _pdfService.CreatePdf(data);
            }

            return path;
        }



        //public ActionResult<String> Privacy()
        //{
        //    //return View();


        //}


        private String romanNumeral(string number)
        {
            string res = "";
            switch (number)
            {
                case "01":
                    res = "I";
                    break;
                case "02":
                    res = "II";
                    break;
                case "03":
                    res = "III";
                    break;
                case "04":
                    res = "IV";
                    break;
                case "05":
                    res = "V";
                    break;
                case "06":
                    res = "VI";
                    break;
                case "07":
                    res = "VII";
                    break;
                case "08":
                    res = "VIII";
                    break;
                case "09":
                    res = "IX";
                    break;
                case "10":
                    res = "X";
                    break;
                case "11":
                    res = "XI";
                    break;
                case "12":
                    res = "XII";
                    break;

                default:
                    res = "";
                    break;
            }

            return res;

        }

        private String Bulan(string number)
        {
            string res = "";
            switch (number)
            {
                case "01":
                    res = "Januari";
                    break;
                case "02":
                    res = "Februari";
                    break;
                case "03":
                    res = "Maret";
                    break;
                case "04":
                    res = "April";
                    break;
                case "05":
                    res = "Mei";
                    break;
                case "06":
                    res = "Juni";
                    break;
                case "07":
                    res = "Juli";
                    break;
                case "08":
                    res = "Agustus";
                    break;
                case "09":
                    res = "September";
                    break;
                case "10":
                    res = "Oktober";
                    break;
                case "11":
                    res = "November";
                    break;
                case "12":
                    res = "Desember";
                    break;

                default:
                    res = "";
                    break;
            }

            return res;

        }



        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
