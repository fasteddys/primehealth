using EmaTours.AdminPanel.Helper;
using EmaTours.BLL.Logic.Tables;
using EmaTours.DTOs.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using EmaTours.Entities;
using static EmaTours.BLL.StaticData.StaticEnums;

namespace EmaTours.AdminPanel.Controllers
{
    public class TripController : BaseController
    {

        [HttpPost]

        public ActionResult index(int id, string Password)
        {
            return View();
        }
        public ActionResult ViewAllTripRequests()
        {

            return View(UnitOfWork.ClientTripRequestsBLL.GetAllTripRequest());
        }
        public ActionResult ViewTripRequests(int RequestStatusID)
        {
        
        return View("ViewAllTripRequests", UnitOfWork.ClientTripRequestsBLL.GetTripRequest(RequestStatusID));
            
          
        }

        public ActionResult GetAssignedTripRequest()
        {
            var UserID =Convert.ToInt32( HttpContext.Request.Cookies.Get("UserID").Value);
            return View("ViewAllTripRequests", UnitOfWork.ClientTripRequestsBLL.GetAssignedTripRequest(UserID));


        }

        

        public ActionResult ViewAllTrip()
        {

            return View(UnitOfWork.TripsBLL.GetAllTripsData());
        }
        [HttpGet]
        public ActionResult AddTrip(TripDTO Trip)
        {

            Trip.Languges = new List<LanguageDTO>();
            foreach (var item in UnitOfWork.LanguageBLL.GetAllLanguage())
            {
                Trip.Languges.Add(item);
            }


            Trip.Countries = new  List<OperatingCountryDTO>();
            foreach (var item in UnitOfWork.OperatingCountriesBLL.GetAllOperatingCountries())
            {
                Trip.Countries.Add(item);
            }


            Trip.Locations = new List<OperatingLocationDTO>();
            foreach (var item in UnitOfWork.OperatingLocationsBLL.GetAllOperatingLocations())
            {
                Trip.Locations.Add(item);
            }

            Trip.Currencies = new List<CurrencyDTO>();
            foreach (var item in UnitOfWork.CurrenciesBLL.GetAllCurrencies())
            {
                Trip.Currencies.Add(item);
            }

            return View(Trip);
        }
        [HttpGet]
        public ActionResult EditTrip(int TripID)
        {
            TripDTO Trip = UnitOfWork.TripsBLL.GetTripByID( TripID);
            Trip.Languges = new List<LanguageDTO>();
            Trip.Languges.AddRange(UnitOfWork.LanguageBLL.GetAllLanguage());
            Trip.Countries = new List<OperatingCountryDTO>();
            Trip.Countries.AddRange(UnitOfWork.OperatingCountriesBLL.GetAllOperatingCountries());
            Trip.Locations = new List<OperatingLocationDTO>();
            Trip.Locations.AddRange(UnitOfWork.OperatingLocationsBLL.GetAllOperatingLocations());
            Trip.Photos = new List<PhotoDTO>();
            Trip.Photos.AddRange(UnitOfWork.TripPhotosBLL.GetAllTripPhotos(TripID));
            return View(Trip);

        }
        [HttpPost]
        public ActionResult EditTrip(TripDTO Trip)
        {
            UnitOfWork.TripsBLL.EditTrip(Trip);
            UnitOfWork.Submit();
          
            
            return Redirect("/Trip/EditTrip?TripID="+ Trip.TripID);
        }

        public JsonResult AddAttachJson(int TripID,int languageID)
        {
            int? t = TripID;
            string strMappath = UnitOfWork.ConfigurationsBLL.GetConfigrationByKey("TripsPhotosLocation") +@"\" + t +@"\";
            //string strMappath = "/Attachment/" + t + "/" + d + "/";

            if (!Directory.Exists(strMappath))
            {
                DirectoryInfo di = Directory.CreateDirectory(strMappath);
            }



            for (int i = 0; i < Request.Files.Count; i++)
            {
              PhotoDTO  a = new PhotoDTO();
                var file = Request.Files[i];
                var fileName = Path.GetFileName(file.FileName);
                string[] FileArray = fileName.Split('.');
                string extension = FileArray[1];
                //string fname = t.ToString() + "_" + d.ToString() + "_" + i + "." + extension;
                var path1 = strMappath + fileName;
                file.SaveAs(path1);
                a.TripID =(int) t;
                a.PhotoPath =  @"\"+ t + @"\" + fileName;
                a.PhotoName = fileName;
                a.LangugeID = languageID;
            UnitOfWork.TripPhotosBLL.AddTripPhotos(a);
                UnitOfWork.Submit();
            }
          
          
            return Json("", JsonRequestBehavior.AllowGet);


        }



        [HttpPost]
        public JsonResult AddNewTrip(TripDTO TripDTO)
        {
           Trip NewTrip= UnitOfWork.TripsBLL.AddNewTrip(TripDTO);
            UnitOfWork.Submit();
           


            return Json(NewTrip.TripID, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ManageTripRequest(int TripRequestID)
        {
            return View(UnitOfWork.ClientTripRequestsBLL.ManageTripRequest(TripRequestID));

        }
        //[HttpPost]
        public ActionResult AssignRequest(int TripRequestID)
        {
            ClientTripDTO TripRequest = new ClientTripDTO();
            TripRequest.UserAssigneeID = Convert.ToInt32(HttpContext.Request.Cookies.Get("UserID").Value);
            TripRequest.ClientTripID= TripRequestID;
            UnitOfWork.ClientTripRequestsBLL.UserAssignTripRequest(TripRequest);
            UnitOfWork.Submit();
           // await NotifyAsync("Request Assigned", "Request Assigned");
            return Redirect("/Trip/ManageTripRequest?TripRequestID=" + TripRequest.ClientTripID);

        }


        public async Task<ActionResult> AssignRequestNotificationsAsync(int TripRequestID)
        {
        
            await NotifyAsync("Request Assigned", "Request Assigned");
            return View();

        }


        public ActionResult CloseRequest(int TripRequestID)
        {

            ClientTripDTO ClientTrip = new ClientTripDTO();
            ClientTrip.ClientTripID = TripRequestID;
            ClientTrip.UserAssigneeID = Convert.ToInt32(HttpContext.Request.Cookies.Get("UserID").Value);
            UnitOfWork.ClientTripRequestsBLL.UserCloseTripRequest(ClientTrip);
            UnitOfWork.Submit();

            return Redirect("/Trip/ManageTripRequest?TripRequestID=" + TripRequestID);


        }

        [HttpPost]
        public ActionResult AddRequestDetails(ClientTripDTO ClientTrip)
        {
            UnitOfWork.ClientTripRequestsBLL.UserContactTripRequester(ClientTrip);

            if (UnitOfWork.Submit()&& ClientTrip.ReachedAgreement==true)
            {
                NotificationDTO notification = new NotificationDTO
                {
                    IsActive = true,
                    NotificationBody = "Reach Aggreement For Rquest "+ ClientTrip.ClientTripID,
                    NotificationDirectionFK = (int)NotificationDirection.FromAgentToClient,
                    NotificationHeader = "Reach Aggreement",
                    NotificationMethodFK = (int)NotificationMethod.InApplication,
                    SenderFK = Convert.ToInt32(HttpContext.Request.Cookies.Get("UserID").Value),
                    RequestFK = ClientTrip.ClientTripID,
                    RequestTypeFK = (int)ClientRequestType.PickupRequest,
                    SentTime = DateTime.Now,
                    IsReceived = false,
                    IsSent = true,
                    RecipientFK = ClientTrip.ClientID,
                    VisitID= ClientTrip.ClientVisitID

                };
                UnitOfWork.NotificationsBLL.Addnotification(notification);
                UnitOfWork.Submit();
            }
            return Redirect("/Trip/ManageTripRequest?TripRequestID=" + ClientTrip.ClientTripID);


        }

        public ActionResult _ViewPhotos()
        {
          return  View();
        }

            public async Task<bool> NotifyAsync( string title, string body)
        {
            try
            {
                // Get the server key from FCM console
                var serverKey = string.Format("key={0}", "AIzaSyA7njiGB6Aw_IoSEdYZRmIRL85hPkiCgCI");

                // Get the sender id from FCM console
                


               var senderId = string.Format("id={0}", "928772347403");

                // var senderId = string.Format("id={0}", "Your sender id - use app config");

                var data = new
                {
                    to= "/topics/all",
                    notification = new { title, body }
                };

                // Using Newtonsoft.Json
                var jsonBody = JsonConvert.SerializeObject(data);

                using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://fcm.googleapis.com/fcm/send"))
                {
                    httpRequest.Headers.TryAddWithoutValidation("Authorization", serverKey);
                    httpRequest.Headers.TryAddWithoutValidation("Sender", senderId);
                    httpRequest.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    using (var httpClient = new HttpClient())
                    {
                        var result = await httpClient.SendAsync(httpRequest);

                        if (result.IsSuccessStatusCode)
                        {
                            return true;
                        }
                        else
                        {
                            // Use result.StatusCode to handle failure
                            // Your custom error handler here
                  //          _logger.LogError($"Error sending notification. Status Code: {result.StatusCode}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError($"Exception thrown in Notify Service: {ex}");
            }

            return false;
        }






    }
}