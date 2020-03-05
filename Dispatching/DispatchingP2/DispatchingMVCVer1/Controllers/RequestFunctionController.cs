using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DispatchingMVCVer1.Models;
using PagedList;


namespace DispatchingMVCVer1.Controllers
{
    [Authorize]
    public class RequestFunctionController : Controller
    {
        Mailing mail = new Mailing();
        DisPatchingDBEntities DB = new DisPatchingDBEntities();
              
        /* this action takes the status from the button pressed on the layout
         * views the requests saved depending on the status of it and the viewer type 
         * CreatorAdmin and StockAdmin can view al requests 
         * the others " inhouse" "provider" Callcenter" can view only the requests they approved 
         * page and page size is the parameters of the pagging of the table 
         * it returns a partial view "ViewDataPartial"
         */
        public ActionResult ShowSpecificRequest(string Status , int Pagesize=10, int page = 1 )
        {
            string ActiveUser = Request.Cookies["UserNameCookie"].Value;
            string Viewertype = Request.Cookies["UserTypeCookie"].Value;
            ViewBag.Status = Status;         
            if (Viewertype == "CreatorAdmin" || Viewertype == "StockAdmin" || Viewertype== "SuperAdmin")
            {
                var ReqData = (from p in DB.Requests
                               where p.ReqStatues == Status
                               orderby p.ReqID descending
                               select p).ToList();
                ViewBag.Status = Status;
                PagedList<Request> Paging = new PagedList<Request>(ReqData, page, Pagesize);
                return View("ViewDataPartialView", Paging);
            }
            else if(Status== "PendingAccountApproval")
            {
                string ShowForWho = "";

                if (Viewertype == "InHouse") { ShowForWho = "InHouse"; }
                else if (Viewertype == "Provider") { ShowForWho = "Provider"; }
                else if (Viewertype == "CallCenter") { ShowForWho = "CallCenter"; }

                var ReqData = (from p in DB.Requests
                               where p.ReqStatues == Status && p.ApprovalType == ShowForWho
                               orderby p.ReqID descending
                               select p).ToList();
                ViewBag.Status = Status;
                PagedList<Request> Paging = new PagedList<Request>(ReqData, page, Pagesize);
                return View("ViewDataPartialView", Paging);
            }
            else
            {
                var ReqData = (from p in DB.Requests
                               where p.ReqStatues == Status && (p.AgreementPerson == ActiveUser || p.Creator == ActiveUser)
                               orderby p.ReqID descending
                               select p).ToList();
                ViewBag.Status = Status;
                PagedList<Request> Paging = new PagedList<Request>(ReqData, page, Pagesize);
                return View("ViewDataPartialView", Paging);
            }
        }
        /* Dashboard is the homepage for this application 
         * Users can view how many closed , new and pending requests 
         * the many lines of code is only for the 3 types of Account users which view only the requests they worked on so to get them the count of their work only
         * Users can search for request by the claim number or providername but the search is implemented on Action SearchRequest
         */ 
        public ActionResult DashboardResults()
        {
            string Viewertype = Request.Cookies["UserTypeCookie"].Value;
            string ActiveUser = Request.Cookies["UserNameCookie"].Value;
            var NumberOfClosedRequests =0;
            var NumberOfRejectedRequests = 0;
            var NumberOfPendingAccount = 0;
            var NumberOfPendingAssign = 0;
            var NumberOfPendingPrepare = 0;
            var NumberOfPendingApproval = 0;           
            if (Viewertype == "InHouse")
            {
                NumberOfClosedRequests = (from p in DB.Requests where p.ReqStatues == "Closed" && p.AgreementPerson == ActiveUser select p).Count();
                NumberOfRejectedRequests = (from p in DB.Requests where p.ReqStatues == "Rejected" && p.AgreementPerson == ActiveUser select p).Count();
                NumberOfPendingAccount = (from p in DB.Requests where p.ReqStatues == "PendingAccountApproval" && p.ApprovalType == Viewertype select p).Count();
                NumberOfPendingAssign = (from p in DB.Requests where p.ReqStatues == "PendingStockAssign" && p.AgreementPerson == ActiveUser select p).Count();
                NumberOfPendingPrepare = (from p in DB.Requests where p.ReqStatues == "PendingStockPrepared" && p.AgreementPerson == ActiveUser select p).Count();
                NumberOfPendingApproval = (from p in DB.Requests where p.ReqStatues == "PendingCreatorApproval" && p.AgreementPerson == ActiveUser select p).Count();
            }
            else if (Viewertype == "Provider")
            {
                NumberOfClosedRequests = (from p in DB.Requests where p.ReqStatues == "Closed" && p.AgreementPerson == ActiveUser select p).Count();
                NumberOfRejectedRequests = (from p in DB.Requests where p.ReqStatues == "Rejected" && p.AgreementPerson == ActiveUser select p).Count();
                NumberOfPendingAccount = (from p in DB.Requests where p.ReqStatues == "PendingAccountApproval" && p.ApprovalType == Viewertype select p).Count();
                NumberOfPendingAssign = (from p in DB.Requests where p.ReqStatues == "PendingStockAssign" && p.AgreementPerson == ActiveUser select p).Count();
                NumberOfPendingPrepare = (from p in DB.Requests where p.ReqStatues == "PendingStockPrepared" && p.AgreementPerson == ActiveUser select p).Count();
                NumberOfPendingApproval = (from p in DB.Requests where p.ReqStatues == "PendingCreatorApproval" && p.AgreementPerson == ActiveUser select p).Count();
            }
            else if (Viewertype == "CallCenter")
            {
                NumberOfClosedRequests = (from p in DB.Requests where p.ReqStatues == "Closed" && p.AgreementPerson == ActiveUser select p).Count();
                NumberOfRejectedRequests = (from p in DB.Requests where p.ReqStatues == "Rejected" && p.AgreementPerson == ActiveUser select p).Count();
                NumberOfPendingAccount = (from p in DB.Requests where p.ReqStatues == "PendingAccountApproval" && p.ApprovalType == Viewertype select p).Count();
                NumberOfPendingAssign = (from p in DB.Requests where p.ReqStatues == "PendingStockAssign" && p.AgreementPerson == ActiveUser select p).Count();
                NumberOfPendingPrepare = (from p in DB.Requests where p.ReqStatues == "PendingStockPrepared" && p.AgreementPerson == ActiveUser select p).Count();
                NumberOfPendingApproval = (from p in DB.Requests where p.ReqStatues == "PendingCreatorApproval" && p.AgreementPerson == ActiveUser select p).Count();
            }
            else
            {
                NumberOfClosedRequests = (from p in DB.Requests where p.ReqStatues == "Closed" select p).Count();
                NumberOfRejectedRequests = (from p in DB.Requests where p.ReqStatues == "Rejected" select p).Count();
                NumberOfPendingAccount = (from p in DB.Requests where p.ReqStatues == "PendingAccountApproval" select p).Count();
                NumberOfPendingAssign = (from p in DB.Requests where p.ReqStatues == "PendingStockAssign" select p).Count();
                NumberOfPendingPrepare = (from p in DB.Requests where p.ReqStatues == "PendingStockPrepared" select p).Count();
                NumberOfPendingApproval = (from p in DB.Requests where p.ReqStatues == "PendingCreatorApproval" select p).Count();
            }
            var Stock = (from s in DB.StockItems select s).ToList();
            ViewBag.NumberOfClosedRequests = NumberOfClosedRequests;
            ViewBag.NumberOfRejectedRequests = NumberOfRejectedRequests;
            ViewBag.NumberOfPendingAccount = NumberOfPendingAccount;
            ViewBag.NumberOfPendingAssign = NumberOfPendingAssign;
            ViewBag.NumberOfPendingPrepare = NumberOfPendingPrepare;
            ViewBag.NumberOfPendingApproval = NumberOfPendingApproval;
            ViewBag.MedicalPass = Stock[0].AvailableQuantity;
            ViewBag.QNB = Stock[1].AvailableQuantity;
            ViewBag.ClaimForm3 = Stock[2].AvailableQuantity;
            ViewBag.ClaimForm5 = Stock[3].AvailableQuantity;
            ViewBag.HospitalStamp = Stock[4].AvailableQuantity;
            ViewBag.MedicalPassMedGulf = Stock[5].AvailableQuantity;
            ViewBag.ClaimForm3MedGulf = Stock[6].AvailableQuantity;
            ViewBag.ClaimForm5MedGulf = Stock[7].AvailableQuantity;
            ViewBag.HospitalStampMedGulf = Stock[8].AvailableQuantity;
            if (Stock[0].AvailableQuantity<500 && Stock[0].Notification!="true")
            {
                mail.SendMail_StockNotification(Stock[0].ItemName);
                Stock[0].Notification = "true";   
            }
            else if (Stock[1].AvailableQuantity < 500 && Stock[1].Notification != "true")
            {
                mail.SendMail_StockNotification(Stock[1].ItemName);
                Stock[1].Notification = "true";
            }
            else if (Stock[2].AvailableQuantity < 500 && Stock[2].Notification != "true")
            {
                mail.SendMail_StockNotification(Stock[2].ItemName);
                Stock[2].Notification = "true";
            }
            else if (Stock[3].AvailableQuantity < 500 && Stock[3].Notification != "true")
            {
                mail.SendMail_StockNotification(Stock[3].ItemName);
                Stock[3].Notification = "true";
            }
            else if (Stock[4].AvailableQuantity < 500 && Stock[4].Notification != "true")
            {
                mail.SendMail_StockNotification(Stock[4].ItemName);
                Stock[4].Notification = "true";
            }
            else if (Stock[5].AvailableQuantity < 500 && Stock[5].Notification != "true")
            {
                mail.SendMail_StockNotification(Stock[5].ItemName);
                Stock[5].Notification = "true";
            }
            else if (Stock[6].AvailableQuantity < 500 && Stock[6].Notification != "true")
            {
                mail.SendMail_StockNotification(Stock[6].ItemName);
                Stock[6].Notification = "true";
            }
            else if (Stock[7].AvailableQuantity < 500 && Stock[7].Notification != "true")
            {
                mail.SendMail_StockNotification(Stock[7].ItemName);
                Stock[7].Notification = "true";
            }
            else if (Stock[8].AvailableQuantity < 500 && Stock[8].Notification != "true")
            {
                mail.SendMail_StockNotification(Stock[8].ItemName);
                Stock[8].Notification = "true";
            }
            else
            {
                foreach (var item in Stock)
                {
                    if (item.AvailableQuantity>500 && item.Notification=="true")
                    {
                        item.Notification = null;
                    }
                }
            }
            DB.SaveChanges();
            return View();
        }
        /* This Action have 2 main purposes 
         * 1- user can add a new claim request upon the data sent to it from the jquery in the view 
         * 2- it creates a back to stock action that operates on the closed requests only 
         *    in that condition the back to stock button send an id to it to get the data of the request that will be backed
         *    select the request row from the sent id 
         *    send the data of the request to the view with viewbags to view it on the fileds so the user enter the start claim only and submit
         */    
        public ActionResult CreatClaimsRequest(int? ReqID)
        {
            if (ReqID==null)
            {
                return View();
            }
            var request = (from r in DB.Requests
                           where r.ReqID == ReqID
                           select r).SingleOrDefault();
            ViewBag.ProviderType = request.ProviderType;
            ViewBag.ProviderName = request.ProviderName;
            ViewBag.ClaimType = request.ClaimsType;
            ViewBag.NumOfBocklets = request.NumOfBocklets;
            ViewBag.Notes = request.Notes;
            ViewBag.State = "BackToStock";
            return View(request);
        }
        
        public JsonResult CreatClaimsRequestWithJson(Request NewRequest)
        {
            string CreatorEmail = Request.Cookies["UserEmailCookie"].Value;
            string AddedType = Request.Cookies["UserTypeCookie"].Value;
            string ActiveUser = Request.Cookies["UserNameCookie"].Value;
            string QualityEmail = "Quality@Prime-Health.org";
            if (NewRequest.ProviderType == "Doctors" || NewRequest.ProviderType == "Hospitals" || NewRequest.ProviderType == "Special Center" || NewRequest.ProviderType == "Alexandria Office")
            {
                NewRequest.ApprovalType = "Provider";
            }
            if (NewRequest.ProviderType == "In House Doctor" || NewRequest.ProviderType == "QNB")
            {
                NewRequest.ApprovalType = "InHouse";
            }
            if (NewRequest.ProviderType == "Call Center")
            {
                NewRequest.ApprovalType = "CallCenter";
            }

            NewRequest.CreationRequestDate = DateTime.Now;
            NewRequest.Creator = AddedType = Request.Cookies["UserNameCookie"].Value;
            if (NewRequest.StartClaims != null)
            {
                NewRequest.ReqStatues = "Returned";
                NewRequest.EndClaims = NewRequest.StartClaims + (NewRequest.NumOfBocklets * 25) - 1;
            }
            else
            {
                NewRequest.ReqStatues = "PendingAccountApproval";
                
            }
            DB.Requests.Add(NewRequest);
            int val = DB.SaveChanges();
            int ReqID = NewRequest.ReqID;
            if (val > 0)
            {
                if (NewRequest.ReqStatues == "PendingAccountApproval")
                {
                    if (NewRequest.ProviderType == "Doctors" || NewRequest.ProviderType == "Hospitals" || NewRequest.ProviderType == "Special Center" || NewRequest.ProviderType == "Alexandria Office")
                    {
                        string ProvidersEmail = "Providers@Prime-Health.org";
                        mail.SendMail_NewRequestCreated(ActiveUser, CreatorEmail, ProvidersEmail, "Providers", ReqID);

                    }
                    if (NewRequest.ProviderType == "In House Doctor" || NewRequest.ProviderType == "QNB")
                    {
                        string InHouseEmail = "InhouseDept@Prime-Health.org";

                        mail.SendMail_NewRequestCreated(ActiveUser, CreatorEmail, InHouseEmail, "InHouse", ReqID);
                    }
                    if (NewRequest.ProviderType == "Call Center")
                    {
                        string CallCenterEmail = "CallCenter@Prime-Health.org";

                        mail.SendMail_NewRequestCreated(ActiveUser, CreatorEmail, CallCenterEmail, "CallCenter", ReqID);
                    }
                }
                else
                {
                    mail.SendMail_NewBackToStockRequest(ActiveUser, CreatorEmail,QualityEmail,ReqID);
                }
                string message = "success";
                ViewBag.message = "success";
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string message = "Fail";
                ViewBag.message =message ;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            
        }   
        /* This is the main function of the application 
         * it shows the details of the request from the id sent to it att the button details clicked
         * and returns the selected request to the view 
         * most of the work done here is jquery and returned to manage request action
         * so take a look at the view 
         */    
        public ActionResult ShowDetailsOfRequest(int? ReqID)
        {
            string ActiveUser = Request.Cookies["UserNameCookie"].Value;
            string UserRole = Request.Cookies["UserRoleCookie"].Value;
            var request = (from r in DB.Requests
                           where r.ReqID == ReqID
                           select r).SingleOrDefault();
            if (UserRole == "Team Leader")
            {
                var StockAdmins = (from s in DB.Users
                                   where s.UserType == "StockAdmin"
                                   select s).ToList();
                ViewBag.StockAdmins = StockAdmins;
            }
            else
            {
                var StockAdmins = (from s in DB.Users
                                   where s.UserType == "StockAdmin" && s.UserName != ActiveUser
                                   select s).ToList();
                ViewBag.StockAdmins = StockAdmins;
            }
            ViewBag.State = request.ReqStatues;
            return View(request);
        }
        // Edits a request by selecting it and saving the new data sent from jquery in the view
        public ActionResult EditClaimsRequest(int? ReqID)
        {
            if (ReqID == null)
            {
                string Status = "PendingAccountApproval";
                return RedirectToAction("ShowSpecificRequest", "RequestFunction", new { Status = Status });
            }
            var request = (from r in DB.Requests
                           where r.ReqID == ReqID
                           select r).SingleOrDefault();
            return View("EditClaimsRequest", request);
        }

        public JsonResult EditRequest(Request EditRequest)
        {
            string ActiveUser = Request.Cookies["UserNameCookie"].Value;
            string CreatorEmail = Request.Cookies["UserEmailCookie"].Value;
            var result = (from r in DB.Requests
                          where r.ReqID == EditRequest.ReqID
                          select r).SingleOrDefault();
            string message = "";
            result.ProviderType = EditRequest.ProviderType;
            result.ProviderName = EditRequest.ProviderName;
            result.ClaimsType = EditRequest.ClaimsType;
            result.NumOfBocklets = EditRequest.NumOfBocklets;
            result.OriginalProviderID = EditRequest.OriginalProviderID;
            result.OriginalProviderName = EditRequest.OriginalProviderName;
            result.Notes = EditRequest.Notes;
            if (EditRequest.ProviderType == "Doctors" || EditRequest.ProviderType == "Hospitals" || EditRequest.ProviderType == "Special Center" || EditRequest.ProviderType == "Alexandria Office")
            {
                result.ApprovalType = "Provider";
                string ProvidersEmail = "Providers@Prime-Health.org";
                mail.SendMail_NewRequestCreated(ActiveUser, CreatorEmail, ProvidersEmail, "Providers",EditRequest.ReqID);
            }
            if (EditRequest.ProviderType == "In House Doctor" || EditRequest.ProviderType == "QNB")
            {
                result.ApprovalType = "InHouse";
                string InHouseEmail = "InhouseDept@Prime-Health.org";

                mail.SendMail_NewRequestCreated(ActiveUser, CreatorEmail, InHouseEmail, "InHouse", EditRequest.ReqID);
            }
            if (EditRequest.ProviderType == "Call Center")
            {
                result.ApprovalType = "CallCenter";
                string CallCenterEmail = "CallCenter@Prime-Health.org";

                mail.SendMail_NewRequestCreated(ActiveUser, CreatorEmail, CallCenterEmail, "CallCenter", EditRequest.ReqID);
            }
            int val = DB.SaveChanges();
            if (val > 0)
            {
                message = "success";

            }
            else
            {
                message = "Fail";
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult DeleteClaimsRequest(int? ReqID)
        //{
        //    var request = (from r in DB.Requests where r.ReqID == ReqID select r).SingleOrDefault();
        //    DB.Requests.Remove(request);
        //    DB.SaveChanges();
        //   string  message = " deleted successfully success";

        //    return Json(message, JsonRequestBehavior.AllowGet);
        //}

        /* Takes the button name and the request id that the action will be on 
         * Check the button name to see what action it do 
         * Save the data sent with the button name in the proper action the button suposed to do
         * send mails with the action happened to the actors and who concerened with the actions
         */

        public JsonResult ManageRequest(string Button , int ReqID, string StartClaim ,string RejectComment , string TransferTo , string ProviderID , string ProviderName)
        {
            Request req = DB.Requests.Find(ReqID);
            string ActiveUserEmail = Request.Cookies["UserEmailCookie"].Value;
            string ActiveUser= Request.Cookies["UserNameCookie"].Value;
            string QualityEmail = "Quality@Prime-Health.org";
            int? EndClaim = 0;
            int ClaimsOutOfStock = 0;
            int ClaimsInStock = 0;
            int? BoockletNotInStock = 0;
            var Creator = (from e in DB.Users
                           where e.UserName == req.Creator
                           select e).SingleOrDefault();
            string CreatorName = Creator.UserName;
            string CreatorEmail = Creator.Email;
            var AccountApproved = (from e in DB.Users
                                   where e.UserName == req.AgreementPerson
                                   select e).SingleOrDefault();
            string AccountApprovedName;
            string AccountApprovedEmail;
            if (AccountApproved == null)
            {
                AccountApprovedName = "";
                AccountApprovedEmail = "";
            }
            else
            {
                AccountApprovedName = AccountApproved.UserName;
                AccountApprovedEmail = AccountApproved.Email;
            }
            var QualityApproved = (from e in DB.Users
                                   where e.UserName == req.PreparingPerson
                                   select e).SingleOrDefault();
            string QualityApprovedName;
            string QualityApprovedEmail;
            if (QualityApproved==null)
            {
                QualityApprovedName = "";
                QualityApprovedEmail = "";
            }
            else
            {
                QualityApprovedName = QualityApproved.UserName;
                QualityApprovedEmail = QualityApproved.Email;
            }
            string message = "";
            if (Button == "AccountApprove")
            {
                req.OriginalProviderID = Convert.ToInt32(ProviderID);
                req.OriginalProviderName = ProviderName;
                req.ReqStatues = "PendingStockAssign";
                req.AgreementPerson = ActiveUser;
                req.AgreementDate = DateTime.Now;
                message = "Approved";
                mail.SendMail_RequestApprovedByAccount(QualityEmail,CreatorEmail,ActiveUserEmail,ActiveUser,ReqID);
            }
            else if (Button == "Assign")
            {
                req.ReqStatues = "PendingStockPrepared";
                req.AssignPerson = ActiveUser;
                req.AssignDate = DateTime.Now;
                message = "Assigned";
            }
            else if (Button == "Prepare")
            {
                var comparerequest = (from g in DB.Requests
                                            where g.ReqID == ReqID
                                            select g.ClaimsType).Single();
             
                int claims = int.Parse(StartClaim);

                EndClaim = claims + (req.NumOfBocklets * 25) - 1;
                var comparestock = (from g in DB.StockRequests
                                    where g.StartClaimNo <= claims && g.EndClaimNo >= EndClaim
                                    select g.ItemName).Single();
                for (int i = claims; i <= EndClaim; i += 25)
                {
                    var ClaimIsOut = (from r in DB.Requests
                                      where r.StartClaims <= i && r.EndClaims >= i && r.ReqStatues != "returned-Closed"
                                      select r).Count();
                    var ClaimIsIn = (from r in DB.Requests
                                     where r.StartClaims <= i && r.EndClaims >= i && r.ReqStatues == "returned-Closed"
                                     select r).Count();
                    var ClaimInStock = (from r in DB.StockRequests
                                        where r.StartClaimNo <= i && r.EndClaimNo >= i
                                        select r).Count();
                 
                    if (ClaimIsOut > ClaimIsIn)
                    {
                        ClaimsOutOfStock += 1;
                    }
                    else if (ClaimInStock > 0)
                    {
                        ClaimsInStock += 1;
                    }
                }
                if (ClaimsOutOfStock > 0)
                {
                    message = "ClaimIsOut";

                }
                else if (ClaimsInStock != req.NumOfBocklets)
                {
                    BoockletNotInStock = req.NumOfBocklets - ClaimsInStock;
                    message = "ClaimNotInStock";
                }
                else
                {
                    if (comparerequest == comparestock)
                    {
                        req.ReqStatues = "PendingCreatorApproval";
                        req.PreparingPerson = ActiveUser;
                        req.PreparationData = DateTime.Now;
                        req.StartClaims = claims;
                        req.EndClaims = claims + (req.NumOfBocklets * 25) - 1;
                        message = "Prepared";
                    }
                    else
                    {
                        message = "Not Prepared";
                    }
                    //mail.RequestPreparedByStock(CreatorName, CreatorEmail, ActiveUser, ActiveUserEmail, AccountApprovedEmail, ReqID);
                }
            }
            else if (Button == "Close")
            {
                req.ReqStatues = "Closed";
                req.ClosedPerson = ActiveUser;
                req.ClosedDate = DateTime.Now;
                message = "Closed";
                var item = (from i in DB.StockItems
                            where i.ItemName == req.ClaimsType
                            select i).SingleOrDefault();
                item.AvailableQuantity = item.AvailableQuantity - req.NumOfBocklets;
                item.LastWithdraw = DateTime.Now;
                mail.RequestClosed(CreatorName,CreatorEmail,AccountApprovedEmail,QualityApprovedEmail,ReqID);
            }
            else if (Button == "Reject")
            {
                req.ReqjectComment = RejectComment;
                req.AgreementPerson = ActiveUser;
                req.AgreementDate = DateTime.Now;
                req.ReqStatues = "Rejected";
                message = "Rejected";
                //mail.SendMail_RequestRejected(CreatorName,CreatorEmail,ActiveUser,ActiveUserEmail,ReqID);
            }
            else if(Button== "ReturnToStock")
            {
                req.ClosedPerson = ActiveUser;
                req.ClosedDate = DateTime.Now;
                req.ReqStatues = "returned-Closed";
                message = "returned-Closed";
                var item = (from i in DB.StockItems
                            where i.ItemName == req.ClaimsType
                            select i).SingleOrDefault();
                item.AvailableQuantity = item.AvailableQuantity + req.NumOfBocklets;
                item.LastAdd = DateTime.Now;
                mail.SendMail_RequestReturnedClosed(CreatorName,CreatorEmail,ActiveUser,ActiveUserEmail,ReqID);
            }
            else if (Button == "Transfer")
            {
                req.TransferPerson = ActiveUser;
                req.TransferedFrom = req.AssignPerson;
                req.WasAssignedAT = req.AssignDate;
                req.AssignPerson = TransferTo;
                req.AssignDate = DateTime.Now;
                message = "Transfered";
                var TranseferToEmail = (from e in DB.Users
                                        where e.UserName == TransferTo
                                        select e.Email).SingleOrDefault();
                mail.SendMail_RequestTransfered(TransferTo,TranseferToEmail,ActiveUser,ActiveUserEmail,ReqID);
            }
            else
            {
                message = "Error";
            }
            
            DB.SaveChanges();
            return Json(new { message= message , EndClaim=EndClaim,ClaimsOutOfStock=ClaimsOutOfStock , ClaimsInStock= BoockletNotInStock } , JsonRequestBehavior.AllowGet);
        }
        /* When search takes place from the dashbourd it redirect to this action that find the claim number searched for and return it in a partial view 
         * it simply check if the search parameter sent is a number it searches the claim number
         * but if it cant parse so it's a string and it searches the provider name
         */   
        public ActionResult SearchRequest(string SearchChar, int Pagesize = 10, int page = 1)
        {
            int SearchNum;
            bool ClaimNumber = int.TryParse(SearchChar, out SearchNum);
            if (ClaimNumber == true)
            {
                var result = (from r in DB.Requests
                              where (r.StartClaims <= SearchNum && r.EndClaims >= SearchNum) || r.OriginalProviderID==SearchNum ||r.ReqID==SearchNum
                              select r).ToList();
                ViewBag.SearchPar = SearchChar;
                PagedList<Request> Paging = new PagedList<Request>(result, page, Pagesize);
                return View("SearchRequest", Paging);
            }
            else
            {
                var result = (from r in DB.Requests
                              where r.ProviderName.Contains(SearchChar) || r.OriginalProviderName.Contains(SearchChar) ||r.PreparingPerson.Contains(SearchChar)
                              select r).ToList();
                ViewBag.SearchPar = SearchChar;
                PagedList<Request> Paging = new PagedList<Request>(result, page, Pagesize);
                return View("SearchRequest", Paging);
            }           
        } 
        public ActionResult UserReports()
        {
            var Users = (from u in DB.Users
                         where u.Role != "Admin" || u.Role != "SuperAdmin"
                         select u).ToList();
            ViewBag.Users = Users;
            return View();
        }
        [HttpPost]
        public ActionResult UserReports(string UserName , string From , string To , string ProviderName , string ClaimType)
        {
            DateTime IntervalFrom = DateTime.Parse(From).Date.Add(DateTime.Now.TimeOfDay);
            DateTime IntervalTo = DateTime.Parse(To).Date.Add(DateTime.Now.TimeOfDay);
            var Users = (from u in DB.Users
                         where u.Role != "Admin" || u.Role != "SuperAdmin"
                         select u).ToList();
            ViewBag.Users = Users;
            if (ProviderName!=null)
            {
                var Providername = (from p in DB.Requests
                                    where p.ProviderName.Contains(ProviderName)
                                    select p.ProviderName).FirstOrDefault();
                var Providers = (from p in DB.Requests
                                 where p.ProviderName.Contains(Providername) && p.CreationRequestDate >= IntervalFrom && p.CreationRequestDate <= IntervalTo
                                 select p).ToList();
                var providerRequestCount = Providers.Count();
                var ProvidersNumberOfBocklets = (from p in Providers
                                                 select p.NumOfBocklets).Sum();
                ViewBag.ProviderName = Providername;
                ViewBag.ProvidersNumberOfBocklets = ProvidersNumberOfBocklets;
                ViewBag.providerRequestCount = providerRequestCount;
                ViewBag.report = Providers;
                return View(Providers);
            }
            else if (UserName!=null)
            {
                var usertype = (from u in DB.Users
                                where u.UserName == UserName
                                select u.UserType).SingleOrDefault();
                var report = (from r in DB.Requests
                              where (r.AgreementPerson == UserName || r.PreparingPerson == UserName || r.Creator == UserName || r.ClosedPerson == UserName) && r.CreationRequestDate >= IntervalFrom && r.CreationRequestDate <= IntervalTo && r.ReqStatues!= "Rejected" && r.ReqStatues != "returned-Closed"
                              select r).ToList();
                var RequestsCount = report.Count();
                var numberOFbocklets = (from r in report
                                        select r.NumOfBocklets).Sum();
                ViewBag.UserName = UserName;
                ViewBag.usertype = usertype;
                ViewBag.RequestsCount = RequestsCount;
                ViewBag.numberOFbocklets = numberOFbocklets;
                ViewBag.report = report;
                return View(report);
            }
            else
            {
                var Claims = (from c in DB.Requests
                              where c.ClaimsType == ClaimType && c.CreationRequestDate >= IntervalFrom && c.CreationRequestDate <= IntervalTo && c.ReqStatues!= "Rejected" && c.ReqStatues != "returned-Closed"
                              select c).ToList();
                
                var ClaimNum = (from c in Claims
                                select c.NumOfBocklets).Sum();
                ViewBag.ClaimNum = ClaimNum;
                ViewBag.ClaimType = ClaimType;
                ViewBag.ClaimCount = Claims.Count();
                ViewBag.report = Claims;
                return View(Claims);
            }
           
        }
    }
}