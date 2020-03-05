using CallCenterNotesApp.DLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static CallCenterNotesApp.Enums.Enums;
using CallCenterNotesApp.CustomExceptions;

using CallCenterNotesApp.ModelView;
using System;

using System.Data;
using System.IO;

using System.Net.NetworkInformation;


using System.Data.SqlClient;
using ClosedXML.Excel;
using System.Configuration;
using System.Transactions;
namespace CallCenterNotesApp.BLL
{
    public class OnlineApprovalHelper
    {
        PhNetworkEntities db = new PhNetworkEntities();


        public OnlineApprovals_RequestDetails AddReply(int RequestID, string Note, CallCenterAppUser User)
        {

            
            //OnlineApprovals_Requests ApprovalsRequest = new OnlineApprovals_Requests();
            //ApprovalsRequest = db.OnlineApprovals_Requests.Where(x => x.RequestID == RequestID).FirstOrDefault();
            //ApprovalsRequest.CallCenterAssignee = User.UserName;

            OnlineApprovals_Requests ApprovalsRequest = new OnlineApprovals_Requests();
            ApprovalsRequest = db.OnlineApprovals_Requests.Where(x => x.RequestID == RequestID).FirstOrDefault();
            ApprovalsRequest.CallCenterAssignee = User.UserName;
            ApprovalsRequest.RequestStatusID = (int)OnlineApprovalStatus.AssignedByCallCenter;


            OnlineApprovals_RequestDetails ApprovalsDetails = new OnlineApprovals_RequestDetails
            {
                Notes = Note,
                RequestID = RequestID,
                DetailsDate = DateTime.Now,
                IsDeleted = false,
                RequestDetailsTypeID = (int)OnlineApprovalSDetailsType.ReplyFromCallCenter,
                Seen = false,
                UserID = User.UserID,
                UserTypeID = (int)UserType.CallCeneter,
                Delivered = false
            };
            db.OnlineApprovals_RequestDetails.Add(ApprovalsDetails);

            db.SaveChanges();

            return ApprovalsDetails;


        }
        public OnlineApprovals_RequestDetails Assign(int RequestID, CallCenterAppUser User)
        {
            OnlineApprovals_Requests ApprovalsRequest = new OnlineApprovals_Requests();
            ApprovalsRequest = db.OnlineApprovals_Requests.Where(x => x.RequestID == RequestID).FirstOrDefault();
            ApprovalsRequest.CallCenterAssignee = User.UserName;
            ApprovalsRequest.RequestStatusID =(int ) OnlineApprovalStatus.AssignedByCallCenter;
            ApprovalsRequest = db.OnlineApprovals_Requests.Where(x => x.RequestID == RequestID).FirstOrDefault();
            OnlineApprovals_RequestDetails ApprovalsDetails = new OnlineApprovals_RequestDetails
            {
                Notes = "Assign Request",
                RequestID = RequestID,
                DetailsDate = DateTime.Now,
                IsDeleted = false,
                RequestDetailsTypeID = (int)OnlineApprovalSDetailsType.Assign,
                Seen = false,
                UserID = User.UserID,
                UserTypeID = (int)UserType.CallCeneter,
                Delivered = false
            };
            db.OnlineApprovals_RequestDetails.Add(ApprovalsDetails);
            db.SaveChanges();
            return ApprovalsDetails;


        }
        public OnlineApprovals_RequestDetails Reject(int RequestID, string Note, CallCenterAppUser User)
        {
            

            OnlineApprovals_Requests ApprovalsRequest = new OnlineApprovals_Requests();
            ApprovalsRequest = db.OnlineApprovals_Requests.Where(x => x.RequestID == RequestID).FirstOrDefault();
            ApprovalsRequest.CallCenterAssignee = User.UserName;
            ApprovalsRequest.RequestStatusID = (int)OnlineApprovalStatus.Rejected;
            ApprovalsRequest.CloseTime = DateTime.Now;
            OnlineApprovals_RequestDetails ApprovalsDetails = new OnlineApprovals_RequestDetails
            {
                Notes = Note,
                RequestID = RequestID,
                DetailsDate = DateTime.Now,
                IsDeleted = false,
                RequestDetailsTypeID = (int)OnlineApprovalSDetailsType.Reject,
                Seen = false,
                UserID = User.UserID,
                UserTypeID = (int)UserType.CallCeneter,
                Delivered = false
            };
            db.OnlineApprovals_RequestDetails.Add(ApprovalsDetails);

            db.SaveChanges();
            return ApprovalsDetails;


        }
        public OnlineApprovals_RequestDetails Approve(int RequestID, string Note, CallCenterAppUser User,string IVR)
        {


            //OnlineApprovals_Requests ApprovalsRequest = new OnlineApprovals_Requests();
            //ApprovalsRequest = db.OnlineApprovals_Requests.Where(x => x.RequestID == RequestID).FirstOrDefault();
            //ApprovalsRequest.CallCenterAssignee = User.UserName;

            OnlineApprovals_Requests ApprovalsRequest = new OnlineApprovals_Requests();
            ApprovalsRequest = db.OnlineApprovals_Requests.Where(x => x.RequestID == RequestID).FirstOrDefault();
            ApprovalsRequest.CallCenterAssignee = User.UserName;
            ApprovalsRequest.RequestStatusID = (int)OnlineApprovalStatus.Approved;
            ApprovalsRequest.CloseTime = DateTime.Now;
            ApprovalsRequest.IVRNumber = Convert.ToInt32( IVR);

            OnlineApprovals_RequestDetails ApprovalsDetails = new OnlineApprovals_RequestDetails
            {
                Notes = Note,
                RequestID = RequestID,
                DetailsDate = DateTime.Now,
                IsDeleted = false,
                RequestDetailsTypeID = (int)OnlineApprovalSDetailsType.Approve,
                Seen = false,
                UserID = User.UserID,
                UserTypeID = (int)UserType.CallCeneter,
                Delivered = false
            };
            db.OnlineApprovals_RequestDetails.Add(ApprovalsDetails);

            db.SaveChanges();


            return ApprovalsDetails;

        }


        public OnlineApprovals_RequestDetails Termenate(int RequestID, string Note, CallCenterAppUser User,string Reason)
        {


            //OnlineApprovals_Requests ApprovalsRequest = new OnlineApprovals_Requests();
            //ApprovalsRequest = db.OnlineApprovals_Requests.Where(x => x.RequestID == RequestID).FirstOrDefault();
            //ApprovalsRequest.CallCenterAssignee = User.UserName;

            OnlineApprovals_Requests ApprovalsRequest = new OnlineApprovals_Requests();
            ApprovalsRequest = db.OnlineApprovals_Requests.Where(x => x.RequestID == RequestID).FirstOrDefault();


            OnlineApprovals_LogsDetails Log = new OnlineApprovals_LogsDetails()
            {
                Comment = Reason,
                LogTime = DateTime.Now,
                LogTypeID = (int)OnineApprovalLog.Onholdog,
                NewValue = "Is terminated",
                OldValue = ApprovalsRequest.RequestStatusID.ToString(),
                RequestID = ApprovalsRequest.RequestID,
                UserName = ApprovalsRequest.CallCenterAssignee

            };
            db.OnlineApprovals_LogsDetails.Add(Log);



            ApprovalsRequest.CallCenterAssignee = User.UserName;
            ApprovalsRequest.RequestStatusID = (int)OnlineApprovalStatus.Terminated;
            ApprovalsRequest.CloseTime = DateTime.Now;
            ApprovalsRequest.IsDeleted = true;
            OnlineApprovals_RequestDetails ApprovalsDetails = new OnlineApprovals_RequestDetails
            {
                Notes = Note,
                RequestID = RequestID,
                DetailsDate = DateTime.Now,
                IsDeleted = false,
                RequestDetailsTypeID = (int)OnlineApprovalSDetailsType.Approve,
                Seen = false,
                UserID =  User.UserID,
                UserTypeID = (int)UserType.CallCeneter,
                Delivered = false
            };
            db.OnlineApprovals_RequestDetails.Add(ApprovalsDetails);

            db.SaveChanges();

            return ApprovalsDetails;

        }

        public OnlineApprovals_RequestDetails Onhold(int RequestID, string Note, CallCenterAppUser User,string Reason)
        {


            //OnlineApprovals_Requests ApprovalsRequest = new OnlineApprovals_Requests();
            //ApprovalsRequest = db.OnlineApprovals_Requests.Where(x => x.RequestID == RequestID).FirstOrDefault();
            //ApprovalsRequest.CallCenterAssignee = User.UserName;
            OnlineApprovals_Requests ApprovalsRequest = new OnlineApprovals_Requests();
            ApprovalsRequest = db.OnlineApprovals_Requests.Where(x => x.RequestID == RequestID).FirstOrDefault();
            OnlineApprovals_LogsDetails Log = new OnlineApprovals_LogsDetails()
            {
                Comment = Reason,
                LogTime = DateTime.Now,
                LogTypeID = (int)OnineApprovalLog.TerminatLog,
                NewValue = "Is OnHold",
                OldValue = ApprovalsRequest.RequestStatusID.ToString(),
                 RequestID= ApprovalsRequest.RequestID,
                  UserName= ApprovalsRequest.CallCenterAssignee,
                  
            };
            db.OnlineApprovals_LogsDetails.Add(Log);


            ApprovalsRequest.CallCenterAssignee = User.UserName;
            ApprovalsRequest.RequestStatusID = (int)OnlineApprovalStatus.Onhold;
         

            OnlineApprovals_RequestDetails ApprovalsDetails = new OnlineApprovals_RequestDetails
            {
                Notes = Note,
                RequestID = RequestID,
                DetailsDate = DateTime.Now,
                IsDeleted = false,
                RequestDetailsTypeID = (int)OnlineApprovalSDetailsType.Onhold,
                Seen = false,
                UserID = User.UserID,
                UserTypeID = (int)UserType.CallCeneter,
                Delivered = false
            };
            db.OnlineApprovals_RequestDetails.Add(ApprovalsDetails);

            db.SaveChanges();
            return ApprovalsDetails;



        }

        public OnlineApprovals_RequestDetails EndOnhold(int RequestID, CallCenterAppUser User)
        {


            //OnlineApprovals_Requests ApprovalsRequest = new OnlineApprovals_Requests();
            //ApprovalsRequest = db.OnlineApprovals_Requests.Where(x => x.RequestID == RequestID).FirstOrDefault();
            //ApprovalsRequest.CallCenterAssignee = User.UserName;

            OnlineApprovals_Requests ApprovalsRequest = new OnlineApprovals_Requests();
            ApprovalsRequest = db.OnlineApprovals_Requests.Where(x => x.RequestID == RequestID).FirstOrDefault();
            ApprovalsRequest.CallCenterAssignee = User.UserName;
            ApprovalsRequest.RequestStatusID = (int)OnlineApprovalStatus.PendingCallCenterAction;

            OnlineApprovals_RequestDetails ApprovalsDetails = new OnlineApprovals_RequestDetails
            {
                RequestID = RequestID,
                DetailsDate = DateTime.Now,
                IsDeleted = false,
                RequestDetailsTypeID = (int)OnlineApprovalSDetailsType.EndOnhold,
                Seen = false,
                UserID = User.UserID,
                UserTypeID = (int)UserType.CallCeneter,
                Delivered = false
            };
            db.OnlineApprovals_RequestDetails.Add(ApprovalsDetails);

            db.SaveChanges();
            return ApprovalsDetails;



        }

        public bool OnlineApprovalsRequestFileUpload( int RequestID,int RequestDetails,
         int UserID, System.Web.UI.WebControls.FileUpload FileUpload1)
        {
            var Request = db.OnlineApprovals_Requests.Where(x=>x.RequestID== RequestID).FirstOrDefault();
            string FilePath; 
            FilePath = db.OnlineApprovals_Configrations.Where(x => x.ConfigrationKey == "AttachmentsPath").FirstOrDefault().
                Configration + @"\" + RequestID + @"\" + RequestDetails ;
                

            
            try
            {
                foreach (HttpPostedFile postedFile in FileUpload1.PostedFiles)
                {

                    string fileName = Path.GetFileName(postedFile.FileName);

                    if (fileName != "")
                    {
                        if (!Directory.Exists(FilePath))
                        {
                            DirectoryInfo di = Directory.CreateDirectory(FilePath);
                        }


                        postedFile.SaveAs(FilePath + "/" + fileName);

                        using (PhNetworkEntities Db = new PhNetworkEntities())
                        {
                            OnlineApprovals_RequestAttachment EmailRequestAttachDetails = new OnlineApprovals_RequestAttachment();
                            EmailRequestAttachDetails.AttachmentName = fileName;
                            EmailRequestAttachDetails.AttachmentPath = FilePath + @"\" + fileName;
                            EmailRequestAttachDetails.AttachmentTypeID = 1;
                            EmailRequestAttachDetails.IsDeleted =false;
                            EmailRequestAttachDetails.RequestDetailsID = RequestDetails;
                            EmailRequestAttachDetails.RequestID = RequestID;
                            Db.OnlineApprovals_RequestAttachment.Add(EmailRequestAttachDetails);
                            Db.SaveChanges();

                        }

                    }

                }
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }


        //public bool OnlineApprovalsTakeBackUp( int RequestID, int RequestDetails,System.Web.UI.WebControls.FileUpload FileUpload1)
        //{if (db.OnlineApprovals_Configrations.Where(x => x.ConfigrationKey == "TakeBackup").FirstOrDefault().Configration == "1")
        //    {
        //        var Request = db.OnlineApprovals_Requests.Where(x => x.RequestID == RequestID).FirstOrDefault();
        //        string FilePath;
        //        FilePath = db.OnlineApprovals_Configrations.Where
        //            (x => x.ConfigrationKey == "AttachmentsBackupPath").FirstOrDefault().Configration + "/" + "OnlineApprovals/"
        //            + RequestID + "/" + RequestDetails + "/";
        //        try
        //        {
        //            foreach (HttpPostedFile postedFile in FileUpload1.PostedFiles)
        //            {

        //                string fileName = Path.GetFileName(postedFile.FileName);

        //                if (fileName != "")
        //                {
        //                    if (!Directory.Exists(FilePath))
        //                    {
        //                        DirectoryInfo di = Directory.CreateDirectory(FilePath);
        //                    }


        //                    postedFile.SaveAs(FilePath + "/" + fileName);
        //                }

        //            }
        //            return true;

        //        }
        //        catch (Exception ex)
        //        {
        //            return false;
        //        }
        //    }
        //    return false;
        //}











    }
}