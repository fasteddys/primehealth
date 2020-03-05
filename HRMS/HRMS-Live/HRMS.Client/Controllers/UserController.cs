using HRMS.DTOs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
namespace HRMS.Client.Controllers
{
    public class UserController : BaseController
    {


        [ClientAuthorization]
        public ActionResult Index()
        {
           
            return View();
        }
        [ClientAuthorization]

        public ActionResult UserProfile(int LID, int UID)
        {
            return View();
        }
        [ClientAuthorization]

        public async Task<JsonResult> AddProfilePicture(int ID)
        {
            try
            {
                string DbPath = ID.ToString() ;
                //var SavePath =System.Web.HttpContext.Current.Server.MapPath(@"~/imageProfilePic" + "/");
                var SavePath = ConfigurationManager.AppSettings["RequestAttachment"];
                //SavePath = SavePath.Replace(@"\", "/");
                if (!Directory.Exists(SavePath))
                {
                    DirectoryInfo di = Directory.CreateDirectory(SavePath);
                }
                //search about current image for this user
                var files = Directory.GetFiles(SavePath, "*.*", SearchOption.AllDirectories);
                string imageFilesOldPath, imageFilesNewPath;
                foreach (string filename in files)
                {
                    if (Regex.IsMatch(filename, @".jpg") && filename.Contains(ID.ToString()) && !filename.Contains("_"))
                    {
                        imageFilesOldPath = filename;
                        string delmater = ".jpg";
                        imageFilesNewPath = Regex.Split(imageFilesOldPath, delmater)[0] + '_' + DateTime.Now.Day + '_' + DateTime.Now.Month + '_' + DateTime.Now.Year + '_' + DateTime.Now.Hour + '_' + DateTime.Now.Minute + '_' + DateTime.Now.Second + ".jpg";
                        System.IO.File.Move(imageFilesOldPath, imageFilesNewPath);
                    }
                }
                for (int i = 0; i < Request.Files.Count; i++)
                {
                   
                    var file = Request.Files[i];//get from js with object formData()
                    string Path2 = SavePath+ID+ ".jpg";

                    file.SaveAs(Path2);
                    DbPath = DbPath + ".jpg";
                }
                var SuccessUpload = new ResultViewModel
                {
                    Data = DbPath,
                    Message = "Profile Picture Updtaed successfully",
                    Success = true
                };

                return Json(SuccessUpload,JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                var FailedUpload = new ResultViewModel
                {
                    Data = null,
                    Message = "Sorry..error while updating profile picture, please try again",
                    Success = false
                };

                return Json(FailedUpload, JsonRequestBehavior.AllowGet);
            }

            //using (HttpClient client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri(apiUrl);
            //    client.DefaultRequestHeaders.Accept.Clear();
            //    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //    HttpResponseMessage response = await client.GetAsync(apiUrl);
            //    if (response.IsSuccessStatusCode)
            //    {
            //        var data = await response.Content.ReadAsStringAsync();
            //        var table = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(data);
            //    }
            //}           
        }

        [ClientAuthorization]
        public ActionResult MyDepartment()
        {
            return View();
        }

        [ClientAuthorization]
        public ActionResult MyOrganization()
        {
            return View();
        }

        public ActionResult MySubordinates()
        {
            return View();
        }

        public ActionResult UnAuthorizedPage()
        {
            return View("UnAuthorizedPage");
        }
      // start View User Information Report
        public ActionResult ViewUserInfo()
        {
            return View();
        }
        // end  View User Information Report

    }
}