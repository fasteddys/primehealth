using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebService.Models;
using WebService.Classes;
using System.Configuration;
using WebService.Utilities;
using Newtonsoft.Json;
using System.Drawing;

namespace WebService.Controllers
{


    public class DataController : ApiController
    {

        PhMobAppEntities _db = new PhMobAppEntities();

        //    To be Disabled
        //[HttpGet]
        //[ActionName("UpdatePassword")]
        //public string UpdatePassword(string medid, string pass)
        //{
        //    try
        //    {
        //        List<Loginer> n_pass = new List<Loginer>();
        //        n_pass = (from p in _db.Loginers
        //                  where p.Medical_id == medid
        //                  select p).ToList();
        //        int id = n_pass.FirstOrDefault().id;
        //        Loginer _log = _db.Loginers.Find(id);
        //        _log.Password = pass;
        //        _db.SaveChanges();
        //        return "Success";
        //    }
        //    catch (Exception ex)
        //    {
        //        return (ex.Message);
        //    }

        //}
        [HttpGet]
        [ActionName("Update_New_Password")]
        public string Update_New_Password(string medid, string pass)
        {
            try
            {
                List<Loginer> n_pass = new List<Loginer>();
                n_pass = (from p in _db.Loginers
                          where p.Medical_id == medid
                          select p).ToList();
                int id = n_pass.FirstOrDefault().id;
                Loginer _log = _db.Loginers.Find(id);
                _log.Password = pass;
                _db.SaveChanges();
                return "Success";
            }
            catch (Exception ex)
            {
                return (ex.Message);
            }

        }
        [HttpGet]
        [ActionName("UpdateEmail")]
        public string UpdateEmail(string medid, string mail)
        {
            try
            {
                List<Loginer> n_pass = new List<Loginer>();
                n_pass = (from p in _db.Loginers
                          where p.Medical_id == medid
                          select p).ToList();
                int id = n_pass.FirstOrDefault().id;
                Loginer _log = _db.Loginers.Find(id);
                _log.Email = mail;
                _db.SaveChanges();
                return "Success";
            }
            catch (Exception ex)
            {
                return (ex.Message);
            }

        }
        [HttpGet]
        [ActionName("GetAllHospitals")]
        public List<HospitalTB> GetAllHospitals()
        {
            List<HospitalTB> _Hospitals = new List<HospitalTB>();
            _Hospitals = _db.HospitalTBs.ToList();
            return _Hospitals;
        }
        [HttpGet]
        [ActionName("GetHospitalsByTown")]
        public List<HospitalTB> GetHospitalsByTown(int LocID, string _cat)
        {
            List<HospitalTB> _Hospital = new List<HospitalTB>();
            _Hospital = (from p in _db.HospitalTBs
                         where p.LocID == LocID && p.HospNotes == _cat
                         select p).ToList();
            return _Hospital;
        }
        [HttpGet]
        [ActionName("GetHospitalsByTownForQNB")]
        public List<HospitalTB> GetHospitalsByTownForQNB(int LocID, int _cat)
        {
            List<HospitalTB> _Hospital = new List<HospitalTB>();
            _Hospital = (from p in _db.HospitalTBs
                         where p.LocID == LocID && p.QNB == _cat
                         select p).ToList();
            return _Hospital;
        }
        [HttpGet]
        [ActionName("GetHospitalsByTownForDiscount")]
        public List<HospitalTB> GetHospitalsByTownForDiscount(int LocID, int _cat)
        {
            List<HospitalTB> _Hospital = new List<HospitalTB>();
            _Hospital = (from p in _db.HospitalTBs
                         where p.LocID == LocID && p.Discount == _cat
                         select p).ToList();
            return _Hospital;
        }
        [HttpGet]
        [ActionName("GetHospitalsByTownForLarge")]
        public List<HospitalTB> GetHospitalsByTownForLarge(int LocID, int _cat)
        {
            List<HospitalTB> _Hospital = new List<HospitalTB>();
            _Hospital = (from p in _db.HospitalTBs
                         where p.LocID == LocID && p.Large == _cat
                         select p).ToList();
            return _Hospital;
        }
        [HttpGet]
        [ActionName("GetHospitalsByTownForMeduim")]
        public List<HospitalTB> GetHospitalsByTownForMeduim(int LocID, int _cat)
        {
            List<HospitalTB> _Hospital = new List<HospitalTB>();
            _Hospital = (from p in _db.HospitalTBs
                         where p.LocID == LocID && p.Meduim == _cat
                         select p).ToList();
            return _Hospital;
        }
        [HttpGet]
        [ActionName("GetAllLocations")]
        public List<LocationsTB> GetAllLocation()
        {
            List<LocationsTB> Loc = new List<LocationsTB>();

            Loc = (from p in _db.LocationsTBs
                   orderby p.LocID ascending
                   select p).ToList();
            return Loc;
        }
        [HttpGet]
        [ActionName("GetAllDoctors")]
        public List<DoctorTB> GetAllDoctors()
        {
            List<DoctorTB> Doc = new List<DoctorTB>();

            Doc = (from p in _db.DoctorTBs
                   orderby p.LocID ascending
                   select p).ToList();
            return Doc;
        }
        [HttpGet]
        [ActionName("GetDoctorsByTown")]

        public List<DoctorTB> GetDoctorsByLocation(int LocID, string _cat)
        {
            List<DoctorTB> _Doc = new List<DoctorTB>();
            _Doc = (from p in _db.DoctorTBs
                    where p.LocID == LocID && p.DoctorNotes == _cat
                    select p).ToList();
            return _Doc;
        }
        [HttpGet]
        [ActionName("GetAllNewsFeeds")]
        public List<NewsFeed> GetAllNewsFeeds()
        {
            List<NewsFeed> _NF = new List<NewsFeed>();

            _NF = (from p in _db.NewsFeeds
                   orderby p.id ascending
                   select p).ToList();
            return _NF;
        }
        [HttpGet]
        [ActionName("GetDoctorsByTownForQNB")]
        public List<DoctorTB> GetDoctorsByTownForQNB(int LocID, int _cat)
        {
            List<DoctorTB> _Doc = new List<DoctorTB>();
            _Doc = (from p in _db.DoctorTBs
                    where p.LocID == LocID && p.QNB == _cat
                    select p).ToList();
            return _Doc;
        }
        [HttpGet]
        [ActionName("GetDoctorsByTownForQNB_categories")]
        public List<DoctorTB> GetDoctorsByTownForQNB_categories(int LocID, int _cat, int cat)
        {
            List<DoctorTB> _Doc = new List<DoctorTB>();
            _Doc = (from p in _db.DoctorTBs
                    where p.LocID == LocID && p.QNB == _cat && p.Doc_cat == cat
                    select p).ToList();
            return _Doc;
        }
        [HttpGet]
        [ActionName("GetDoctorsByTownForDiscount_categories")]
        public List<DoctorTB> GetDoctorsByTownForDiscount_categories(int LocID, int _cat, int cat)
        {
            List<DoctorTB> _Doc = new List<DoctorTB>();
            _Doc = (from p in _db.DoctorTBs
                    where p.LocID == LocID && p.Discount == _cat && p.Doc_cat == cat
                    select p).ToList();
            return _Doc;
        }
        [HttpGet]
        [ActionName("GetDoctorsByTownForLarge")]
        public List<DoctorTB> GetDoctorsByTownForLarge(int LocID, int _cat)
        {
            List<DoctorTB> _Doc = new List<DoctorTB>();
            _Doc = (from p in _db.DoctorTBs
                    where p.LocID == LocID && p.Large == _cat
                    select p).ToList();
            return _Doc;
        }
        [HttpGet]
        [ActionName("GetDoctorsByTownForLarge_categories")]
        public List<DoctorTB> GetDoctorsByTownForLarge_categories(int LocID, int _cat, int cat)
        {
            List<DoctorTB> _Doc = new List<DoctorTB>();
            _Doc = (from p in _db.DoctorTBs
                    where p.LocID == LocID && p.Large == _cat && p.Doc_cat == cat
                    select p).ToList();
            return _Doc;
        }
        [HttpGet]
        [ActionName("GetDoctorsByTownForMeduim")]
        public List<DoctorTB> GetDoctorsByTownForMeduim(int LocID, int _cat)
        {
            List<DoctorTB> _Doc = new List<DoctorTB>();
            _Doc = (from p in _db.DoctorTBs
                    where p.LocID == LocID && p.Meduim == _cat
                    select p).ToList();
            return _Doc;
        }
        [HttpGet]
        [ActionName("GetDoctorsByTownForMeduim_categories")]
        public List<DoctorTB> GetDoctorsByTownForMeduim(int LocID, int _cat, int cat)
        {
            List<DoctorTB> _Doc = new List<DoctorTB>();
            _Doc = (from p in _db.DoctorTBs
                    where p.LocID == LocID && p.Meduim == _cat && p.Doc_cat == cat
                    select p).ToList();
            return _Doc;
        }
        [HttpGet]
        [ActionName("GetAllPharmacies")]
        public List<PharmacyTB> GetAllPharmacies()
        {
            List<PharmacyTB> Pharm = new List<PharmacyTB>();

            Pharm = (from p in _db.PharmacyTBs
                     orderby p.LocID ascending
                     select p).ToList();
            return Pharm;
        }
        [HttpGet]
        [ActionName("GetAllCategories")]
        public List<Category> GetAllCategories()
        {

            List<Category> cat = new List<Category>();

            cat = (from p in _db.Categories
                   orderby p.Categories_name ascending
                   select p).ToList();
            return cat;
        }
        [HttpGet]
        [ActionName("GetPharmaciesByID")]
        public List<PharmacyTB> GetPharmaciesByID(int LocID)
        {
            List<PharmacyTB> _Doc = new List<PharmacyTB>();
            _Doc = (from p in _db.PharmacyTBs
                    where p.LocID == LocID
                    select p).ToList();
            return _Doc;
        }
        [HttpGet]
        [ActionName("GetPharmaciesByID_For_Discount")]
        public List<PharmacyTB> GetPharmaciesByID_For_Discount(int LocID, int _cat)
        {
            List<PharmacyTB> _Doc = new List<PharmacyTB>();
            _Doc = (from p in _db.PharmacyTBs
                    where p.LocID == LocID && p.Discount == _cat
                    select p).ToList();
            return _Doc;
        }
        [HttpGet]
        [ActionName("GetLabsByID_For_Discount")]
        public List<LaboratoryTB> GetLabsByID_For_Discount(int LocID, int _cat)
        {
            List<LaboratoryTB> _Doc = new List<LaboratoryTB>();
            _Doc = (from p in _db.LaboratoryTBs
                    where p.LocID == LocID && p.Discount == _cat
                    select p).ToList();
            return _Doc;
        }
        [HttpGet]
        [ActionName("GetAllLaboratories")]
        public List<LaboratoryTB> GetAllLaboratories()
        {
            List<LaboratoryTB> Pharm = new List<LaboratoryTB>();

            Pharm = (from p in _db.LaboratoryTBs
                     orderby p.LocID ascending
                     select p).ToList();
            return Pharm;
        }
        [HttpGet]
        [ActionName("GetAllFeeds")]
        public List<Category> GetAllFeeds()
        {
            List<Category> Pharm = new List<Category>();

            Pharm = (from p in _db.Categories
                     orderby p.Categories_name ascending
                     select p).ToList();
            return Pharm;
        }
        [HttpGet]
        [ActionName("GetLaboratoriesByID")]
        public List<LaboratoryTB> GetLaboratoriesByID(int LocID)
        {
            List<LaboratoryTB> _Doc = new List<LaboratoryTB>();
            _Doc = (from p in _db.LaboratoryTBs
                    where p.LocID == LocID
                    select p).ToList();
            return _Doc;
        }
        [HttpGet]
        [ActionName("GetSubLocByID")]
        public List<SubLocatiobTB> GetSubLocByID(int LocID)
        {
            List<SubLocatiobTB> _Doc = new List<SubLocatiobTB>();
            _Doc = (from p in _db.SubLocatiobTBs
                    where p.MainLocID == LocID
                    select p).ToList();
            return _Doc;

        }
        [HttpGet]
        [ActionName("getloginer")]
        public List<Loginer> getloginer(string medid, string pass)
        {
            List<Loginer> _log = new List<Loginer>();
            _log = (from p in _db.Loginers
                    where p.Medical_id == medid && p.Password == pass
                    select p).ToList();
            return _log;
        }
        [HttpGet]
        [ActionName("getfreeloginer")]
        public List<Loginer> getfreeloginer(string mail, string pass)
        {
            List<Loginer> _log = new List<Loginer>();
            _log = (from p in _db.Loginers
                    where p.Email == mail && p.Password == pass
                    select p).ToList();
            return _log;
        }
        [HttpPost]
        [ActionName("Addloginer")]
        public bool Addloginer(Loginer user)
        {
            bool sign = false;

            try
            {
                _db.Loginers.Add(user);
                _db.SaveChanges();

                sign = true;

            }
            catch
            {
                sign = false;

            }
            return sign;
        }
        [HttpGet]
        [ActionName("GetPhoneCallButtonAppearance")]
        public string GetPhoneCallButtonAppearance()
        {
            return "false";
        }
        [HttpGet]
        [ActionName("GetActivationStatus")]
        public string GetActivationStatus(string medical)
        {
            var status = (from p in _db.Loginers
                          where p.Medical_id == medical
                          select p.Activated).FirstOrDefault().ToString();
            return status.ToString();
        }
        //To Be Disabled
        //[HttpGet]
        //[ActionName("GetClaim")]
        //public string GetClaim(string medical, string imgName, string user, string sub, string title, string ReqType)
        //{

        //    try
        //    {
        //        using (PhMobAppEntities context = new PhMobAppEntities())
        //        {
        //            ClaimsApproval _ca = new ClaimsApproval();
        //            _ca.imageName = imgName;
        //            _ca.CreationTime = DateTime.Now;
        //            _ca.medicalid = medical;
        //            _ca.userName = user;
        //            _ca.ReqStatus = "PendingApproval";
        //            _ca.ReqSubject = sub;
        //            _ca.ReqTitle = title;
        //            _ca.CallCenterComment = "";
        //            _ca.CallCenterName = "";
        //            _ca.Reqtype = ReqType;
        //            context.ClaimsApprovals.Add(_ca);
        //            context.SaveChanges();
        //            return "Success";
        //        }

        //    }
        //    catch (DbEntityValidationException ex)
        //    {

        //        var errorMessages = ex.EntityValidationErrors
        //                .SelectMany(x => x.ValidationErrors)
        //                .Select(x => x.ErrorMessage);

        //        var fullErrorMessage = string.Join("; ", errorMessages);

        //        var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

        //        throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
        //    }
        //}
        [HttpPost]
        [AllowAnonymous]
        [ActionName("GetClaims")]
        public string GetClaims(RequestDTO requestDTO)
        {

            try
            {
                HttpContext.Current.Server.ScriptTimeout = 900;
                if (ConfigurationManager.AppSettings["LogObject"] == "1")
                {
                    var objectSeralized = JsonConvert.SerializeObject(requestDTO);
                    Logger.LogInformation(objectSeralized);
                }
                using (PhMobAppEntities context = new PhMobAppEntities())
                {
                    if (requestDTO.ImgName2 == string.Empty || requestDTO.ImgName2 == null)
                    {
                        requestDTO.ImgName2 = null;
                    }
                    if (requestDTO.ImgName3 == string.Empty || requestDTO.ImgName3 == null)
                    {
                        requestDTO.ImgName3 = null;
                    }
                    if (requestDTO.ImgName4 == string.Empty || requestDTO.ImgName4 == null)
                    {
                        requestDTO.ImgName4 = null;
                    }
                    if (requestDTO.ImgName5 == string.Empty || requestDTO.ImgName5 == null)
                    {
                        requestDTO.ImgName5 = null;
                    }
                    ClaimsApproval _ca = new ClaimsApproval();
                    var Image1 = GetBytesBase64(requestDTO.ImgName);
                    _ca.imageName = SaveImage(Image1, GenerateTimeBasedGuid().ToString());

                    if (requestDTO.ImgName2 != null)
                    {
                        var Image2 = GetBytesBase64(requestDTO.ImgName2);
                        if (Image2 != null)
                        {
                            _ca.ImgName2 = SaveImage(Image2, GenerateTimeBasedGuid().ToString());
                        }
                        else
                        {
                            _ca.ImgName2 = null;
                        }
                    }
                    else
                    {
                        _ca.ImgName2 = null;
                    }

                    if (requestDTO.ImgName3 != null)
                    {
                        var Image3 = GetBytesBase64(requestDTO.ImgName3);
                        if (Image3 != null)
                        {
                            _ca.ImgName3 = SaveImage(Image3, GenerateTimeBasedGuid().ToString());
                        }
                        else
                        {
                            _ca.ImgName3 = null;
                        }
                    }
                    else
                    {
                        _ca.ImgName3 = null;
                    }

                    if (requestDTO.ImgName4 != null)
                    {
                        var Image4 = GetBytesBase64(requestDTO.ImgName4);
                        if (Image4 != null)
                        {
                            _ca.ImgName4 = SaveImage(Image4, GenerateTimeBasedGuid().ToString());
                        }
                        else
                        {
                            _ca.ImgName4 = null;
                        }
                    }
                    else
                    {
                        _ca.ImgName4 = null;
                    }

                    if (requestDTO.ImgName5 != null)
                    {
                        var Image5 = GetBytesBase64(requestDTO.ImgName5);
                        if (Image5 != null)
                        {
                            _ca.ImgName5 = SaveImage(Image5, GenerateTimeBasedGuid().ToString());
                        }
                        else
                        {
                            _ca.ImgName5 = null;
                        }
                    }
                    else
                    {
                        _ca.ImgName5 = null;
                    }

                    _ca.CreationTime = DateTime.Now;
                    _ca.medicalid = requestDTO.Medical;
                    _ca.userName = requestDTO.User;
                    _ca.ReqStatus = "PendingApproval";
                    _ca.ReqSubject = requestDTO.Sub;
                    _ca.ReqTitle = requestDTO.Title;
                    _ca.CallCenterComment = "";
                    _ca.CallCenterName = "";
                    _ca.Reqtype = requestDTO.ReqType;
                    _ca.Platform = requestDTO.Platform;
                    context.ClaimsApprovals.Add(_ca);
                    context.SaveChanges();
                    return "Success";
                }

            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateException(ex, "DataController", "GetClaims");
                return "Failure";
                //var errorMessages = ex.EntityValidationErrors
                //        .SelectMany(x => x.ValidationErrors)
                //        .Select(x => x.ErrorMessage);

                //var fullErrorMessage = string.Join("; ", errorMessages);

                //var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                //throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }
        [HttpGet]
        [ActionName("GetImage")]
        public string GetImage(string base64String, string imgName, string reqTitle, string reqSubject, string reqStatus, string Creator, DateTime creationdate, string ReqType)
        {

            try
            {
                using (PhMobAppEntities context = new PhMobAppEntities())
                {
                    ClaimsApproval _ca = new ClaimsApproval();
                    _ca.imageBasestrg = base64String;
                    _ca.imageName = imgName;
                    _ca.Creator = Creator;
                    _ca.CreationTime = creationdate;
                    _ca.ReqStatus = reqStatus;
                    _ca.ReqTitle = reqTitle;
                    _ca.ReqSubject = reqSubject;
                    _ca.Reqtype = ReqType;
                    context.ClaimsApprovals.Add(_ca);
                    context.SaveChanges();
                    return "Success";
                }

            }
            catch (DbEntityValidationException ex)
            {

                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                var fullErrorMessage = string.Join("; ", errorMessages);

                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }
        [HttpGet]
        [ActionName("GetClaimsRequests")]
        public List<ClaimsApproval> GetClaimsRequests(string medical, string reqstatus)
        {
            List<ClaimsApproval> _App = new List<ClaimsApproval>();
            _App = (from p in _db.ClaimsApprovals
                    where p.medicalid == medical && p.ReqStatus == reqstatus
                    orderby p.id descending
                    select p).ToList();
            return _App;
        }
        [HttpGet]
        [ActionName("SearchClaimsRequests")]
        public List<ClaimsApproval> SearchClaimsRequests(string medical, string word)
        {
            List<ClaimsApproval> _App = new List<ClaimsApproval>();
            int num;
            bool isNum = int.TryParse(word, out num);
            if (isNum)
            {
                int RequestID = int.Parse(word);
                _App = (from p in _db.ClaimsApprovals
                        where p.medicalid == medical && p.id == RequestID
                        orderby p.id descending
                        select p).ToList();
                return _App;
            }
            else
            {
                _App = (from p in _db.ClaimsApprovals
                        where p.medicalid == medical && (p.ReqSubject == word || p.ReqTitle == word || p.CallCenterComment == word)
                        orderby p.id descending
                        select p).ToList();
                return _App;
            }

        }
        //To be Disabled
        //[HttpGet]
        //public string getFeedBack(string title, string subject, string medical, string username)
        //{
        //    try
        //    {
        //        using (PhMobAppEntities context = new PhMobAppEntities())
        //        {
        //            Feedback _ca = new Feedback();
        //            _ca.Title = title;
        //            _ca.Subject = subject;
        //            _ca.MedicalID = medical;
        //            _ca.UserName = username;
        //            _ca.CreationTime = DateTime.Now;
        //            context.Feedbacks.Add(_ca);
        //            context.SaveChanges();
        //            return "Success";
        //        }

        //    }
        //    catch (DbEntityValidationException ex)
        //    {

        //        var errorMessages = ex.EntityValidationErrors
        //                .SelectMany(x => x.ValidationErrors)
        //                .Select(x => x.ErrorMessage);

        //        var fullErrorMessage = string.Join("; ", errorMessages);

        //        var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

        //        throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
        //    }
        //}
        [HttpGet]
        public string SendFeedBack(string title, string subject, string medical, string username)
        {
            try
            {
                using (PhMobAppEntities context = new PhMobAppEntities())
                {
                    Feedback _ca = new Feedback();
                    _ca.Title = title;
                    _ca.Subject = subject;
                    _ca.MedicalID = medical;
                    _ca.UserName = username;
                    _ca.CreationTime = DateTime.Now;
                    context.Feedbacks.Add(_ca);
                    context.SaveChanges();
                    return "Success";
                }

            }
            catch (DbEntityValidationException ex)
            {

                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                var fullErrorMessage = string.Join("; ", errorMessages);

                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }
        [HttpGet]
        public List<Feedback> getAllMessages(string medicalid)
        {

            var _fb = (from p in _db.Feedbacks where (p.MedicalID == medicalid && p.Seen == "Yes") select p).ToList();
            return _fb;
        }
        [HttpGet]
        [ActionName("SendMail_FeedBack")]
        public void SendMail_FeedBack(string username, string medicalID, string subject)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("noreply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("noreply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "You have received a new feedback";
                message.Body = "Dear Prime Health Team , you have received Feedback from \r " + username + " \r  , Medical ID : " + medicalID + ", with subject : " + subject + ".";
                message.From = "noreply@Prime-Health.org";
                message.ToRecipients.Add("Info@Prime-Health.org");
                message.Save();
                message.SendAndSaveCopy();
            }

            catch (Exception ex)
            {

            }
        }
        [HttpGet]
        [ActionName("SendMail_FeedBack_For_User")]
        public void SendMail_FeedBack_For_User(string medicalID, string username)
        {
            try
            {
                var usermail = (from p in _db.Loginers
                                where p.Medical_id == medicalID
                                select p.Email).SingleOrDefault().ToString();
                if (usermail != null || usermail != "" || usermail != string.Empty)
                {
                    ExchangeService service = new ExchangeService();
                    service.AutodiscoverUrl("noreply@prime-health.org");
                    service.UseDefaultCredentials = false;
                    service.Credentials = new WebCredentials("noreply", "NoP@ssw0rd", "primehealth.local");
                    EmailMessage message = new EmailMessage(service);
                    message.Subject = "Prime Health Team have received your feedback";
                    message.Body = "Dear " + username + " , \n Thank you for sending us your feedback. \n Prime Health Team will review your feedback as soon as possible.";
                    message.From = "noreply@Prime-Health.org";
                    message.ToRecipients.Add(usermail.ToString());
                    message.Save();
                    message.SendAndSaveCopy();
                }
                else
                {

                }

            }

            catch (Exception ex)
            {

            }
        }
        [HttpGet]
        [ActionName("SendMail_FeedBack_For_Approvals")]
        public void SendMail_FeedBack_For_Approvals(string username, string medicalID, string subject, string type)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("noreply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("noreply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "You have received a new approval request";
                message.Body = "Dear CallCenter Team , you have received approval request from \r " + username + " \r  , Medical ID : " + medicalID + ", with subject : " + subject + ".";
                message.From = "noreply@Prime-Health.org";
                if (type == "Normal")
                {
                    message.ToRecipients.Add("CallCenter@Prime-Health.org");
                }
                else if (type == "Special")
                {
                    message.ToRecipients.Add("SP.CallCenter@Prime-Health.org");
                }
                else
                {
                }
                message.Save();
                message.SendAndSaveCopy();
            }

            catch (Exception ex)
            {

            }
        }
        [HttpGet]
        [ActionName("SendMail_Approval_For_User")]
        public void SendMail_Approval_For_User(string medicalID, string username)
        {
            try
            {
                var usermail = (from p in _db.Loginers
                                where p.Medical_id == medicalID
                                select p.Email).SingleOrDefault().ToString();
                if (usermail != null || usermail != "" || usermail != string.Empty)
                {
                    ExchangeService service = new ExchangeService();
                    service.AutodiscoverUrl("noreply@prime-health.org");
                    service.UseDefaultCredentials = false;
                    service.Credentials = new WebCredentials("noreply", "NoP@ssw0rd", "primehealth.local");
                    EmailMessage message = new EmailMessage(service);
                    message.Subject = "Prime Health Team have received your approval request";
                    message.Body = "Dear " + username + " , \n We have received your approval request. \n Prime Health Team will respond as soon as possible.";
                    message.From = "noreply@Prime-Health.org";
                    message.ToRecipients.Add(usermail.ToString());
                    message.Save();
                    message.SendAndSaveCopy();
                }
                else
                {

                }

            }

            catch (Exception ex)
            {

            }
        }
        //[HttpPost]
        //public string Post()
        //{
        //    try
        //    {
        //        HttpResponseMessage result = null;
        //        string message = "";
        //        var httpRequest = HttpContext.Current.Request;
        //        if (httpRequest.Files.Count > 0)
        //        {
        //            var docfiles = new List<string>();
        //            foreach (string file in httpRequest.Files)
        //            {
        //                var postedFile = httpRequest.Files[file];
        //                var filePath = HttpContext.Current.Server.MapPath("~/TransientStorage/" + postedFile.FileName);
        //                postedFile.SaveAs(filePath);
        //                docfiles.Add(filePath);
        //            }
        //            result = Request.CreateResponse(HttpStatusCode.Created, docfiles);
        //            message = "Success";
        //        }
        //        else
        //        {
        //            result = Request.CreateResponse(HttpStatusCode.BadRequest);
        //            message = "failed";
        //        }
        //        return (message);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        [HttpGet]
        public HttpResponseMessage FileAsAttachment(string path, string filename)
        {
            if (File.Exists(path))
            {

                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                var stream = new FileStream(path, FileMode.Open);
                result.Content = new StreamContent(stream);
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                result.Content.Headers.ContentDisposition.FileName = filename;
                return result;
            }
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }
        public HttpResponseMessage get([FromUri]string filename)
        {
            string path = HttpContext.Current.Server.MapPath("~/TransientStorage/" + filename);
            if (!File.Exists(path))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            try
            {
                MemoryStream responseStream = new MemoryStream();
                Stream fileStream = File.Open(path, FileMode.Open);
                bool fullContent = true;
                if (this.Request.Headers.Range != null)
                {
                    fullContent = false;

                    // Currently we only support a single range.
                    RangeItemHeaderValue range = this.Request.Headers.Range.Ranges.First();


                    // From specified, so seek to the requested position.
                    if (range.From != null)
                    {
                        fileStream.Seek(range.From.Value, SeekOrigin.Begin);

                        // In this case, actually the complete file will be returned.
                        if (range.From == 0 && (range.To == null || range.To >= fileStream.Length))
                        {
                            fileStream.CopyTo(responseStream);
                            fullContent = true;
                        }
                    }
                    if (range.To != null)
                    {
                        // 10-20, return the range.
                        if (range.From != null)
                        {
                            long? rangeLength = range.To - range.From;
                            int length = (int)Math.Min(rangeLength.Value, fileStream.Length - range.From.Value);
                            byte[] buffer = new byte[length];
                            fileStream.Read(buffer, 0, length);
                            responseStream.Write(buffer, 0, length);
                        }
                        // -20, return the bytes from beginning to the specified value.
                        else
                        {
                            int length = (int)Math.Min(range.To.Value, fileStream.Length);
                            byte[] buffer = new byte[length];
                            fileStream.Read(buffer, 0, length);
                            responseStream.Write(buffer, 0, length);
                        }
                    }
                    // No Range.To
                    else
                    {
                        // 10-, return from the specified value to the end of file.
                        if (range.From != null)
                        {
                            if (range.From < fileStream.Length)
                            {
                                int length = (int)(fileStream.Length - range.From.Value);
                                byte[] buffer = new byte[length];
                                fileStream.Read(buffer, 0, length);
                                responseStream.Write(buffer, 0, length);
                            }
                        }
                    }
                }
                // No Range header. Return the complete file.
                else
                {
                    fileStream.CopyTo(responseStream);
                }
                fileStream.Close();
                responseStream.Position = 0;

                HttpResponseMessage response = new HttpResponseMessage();
                response.StatusCode = fullContent ? HttpStatusCode.OK : HttpStatusCode.PartialContent;
                //response.Content = new StreamContent(responseStream);
                response.Content = new ByteArrayContent(responseStream.ToArray());

                response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                return response;
            }
            catch (IOException)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);

            }

        }
        public HttpResponseMessage GetFiles(string id)
        {
            HttpResponseMessage result = null;
            var localFilePath = HttpContext.Current.Server.MapPath("~/TransientStorage/" + id);

            // check if parameter is valid
            if (String.IsNullOrEmpty(id))
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            // check if file exists on the server
            else if (!File.Exists(localFilePath))
            {
                result = Request.CreateResponse(HttpStatusCode.Gone);
            }
            else
            {// serve the file to the client
                result = Request.CreateResponse(HttpStatusCode.OK);
                result.Content = new StreamContent(new FileStream(localFilePath, FileMode.Open, FileAccess.Read));
                result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                result.Content.Headers.ContentDisposition.FileName = id;
            }

            return result;
        }
        [HttpPost]
        public async Task<string> Post()
        {
            try
            {
                HttpResponseMessage result = null;
                string message = "";
                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count > 0)
                {
                    var docfiles = "";
                    //var task =  System.Threading.Tasks.Task.Run(() => { UploadFiles(httpRequest, ref docfiles, out message); });
                    //task.Wait();
                    message = await UploadFiles(httpRequest, docfiles);
                    result = Request.CreateResponse(HttpStatusCode.Created, docfiles);


                }
                else
                {
                    result = Request.CreateResponse(HttpStatusCode.BadRequest);
                    message = "failed";
                }
                return (message);

            }
            catch (Exception ex)
            {
                return ("");
            }
        }
        private async Task<string> UploadFiles(HttpRequest httpRequest, string docfiles)
        {
            string result = "";
            try
            {
                var postedFile = httpRequest.Files[0];
                var filePath = HttpContext.Current.Server.MapPath("~/TransientStorage/" + postedFile.FileName);
                postedFile.SaveAs(filePath);
                try
                {
                    postedFile.SaveAs(@"\\10.1.1.15\Backups\Mobile Application Photos\" + postedFile.FileName);
                }
                catch (Exception ex)
                {

                }
                docfiles = (filePath);
                result = "Success";
            }
            catch (NullReferenceException)
            {

                result = "failed";
            }
            catch (Exception ex)
            {
                result = "System Error";
                Logger.LogInformation(ex.InnerException.ToString());


            }
            return result;
        }
        [HttpGet]
        [ActionName("GetOpticalByID_For_Discount")]
        public List<OpticalTB> GetOpticalByID_For_Discount(int LocID, int _cat)
        {
            List<OpticalTB> _Doc = new List<OpticalTB>();
            _Doc = (from p in _db.OpticalTBs
                    where p.LocID == LocID && p.Discount == _cat
                    select p).ToList();
            return _Doc;
        }
        [HttpGet]
        [ActionName("GetOpticalsByID")]
        public List<OpticalTB> GetOpticalsByID(int LocID)
        {
            List<OpticalTB> _Doc = new List<OpticalTB>();
            _Doc = (from p in _db.OpticalTBs
                    where p.LocID == LocID
                    select p).ToList();
            return _Doc;
        }


        public static byte[] GetBytesBase64(string str)
        {
            try
            {
                if (str == null || str == "")
                {
                    return null;
                }
                byte[] bytes = Convert.FromBase64String(str);
                return bytes;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string SaveImage(byte[] Img, string ImgName)
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory+ConfigurationManager.AppSettings["ImageFolderPath"];                    
                Image image;
                using (MemoryStream ms = new MemoryStream(Img))
                {
                    image = Image.FromStream(ms);
                    string Imagepath = path + (ImgName + ".jpg");
                    string imgPath = Path.Combine(Imagepath);
                    image.Save(imgPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                    if (ConfigurationManager.AppSettings["TakePhotoBackup"] == "1")
                    {
                        string BackupPath = ConfigurationManager.AppSettings["PhotosBackupPath"];
                        string ImageBackupPath = BackupPath + (ImgName + ".jpg");
                        string imgBackupPath = Path.Combine(ImageBackupPath);
                        image.Save(imgBackupPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    return ImgName;
                }
       
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateException(ex, "DataController", "SaveImage");
                return null;
            }
        }

        public Guid GenerateTimeBasedGuid()
        {
            try
            {
                byte[] destinationArray = Guid.NewGuid().ToByteArray();
                DateTime time = new DateTime(0x76c, 1, 1);
                DateTime now = DateTime.Now;
                TimeSpan span = new TimeSpan(now.Ticks - time.Ticks);
                TimeSpan timeOfDay = now.TimeOfDay;
                byte[] bytes = BitConverter.GetBytes(span.Days);
                byte[] array = BitConverter.GetBytes((long)(timeOfDay.TotalMilliseconds / 3.333333));
                Array.Reverse(bytes);
                Array.Reverse(array);
                Array.Copy(bytes, bytes.Length - 2, destinationArray, destinationArray.Length - 6, 2);
                Array.Copy(array, array.Length - 4, destinationArray, destinationArray.Length - 4, 4);
                return new Guid(destinationArray);

            }
            catch (Exception ex)
            {

                ExceptionHandlerConstants.CreateException(ex, "DataController", "GenerateTimeBasedGuid");
                return new Guid();
            }
        }

        public ClaimsApproval GetClaimDetailsByID(int ID)
        {
            try
            {
                using (PhMobAppEntities db = new PhMobAppEntities())
                {
                    var claimApproval = db.ClaimsApprovals.Where(x => x.id == ID).FirstOrDefault();
                    return claimApproval;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateException(ex, "DataController", "GetClaimDetailsByID");
                return null;
            }

        }

    }
}