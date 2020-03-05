using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using CallCenterNotesApp.DLL;
using static CallCenterNotesApp.Enums.Enums;
using CallCenterNotesApp.ModelView;

namespace CallCenterNotesApp.BLL
{
    public class CallcentereMailRequest
    {
        public string AddCallCenterMailRequest(string MailSubject, string usernameStr,string MedicalIDStr, string ClientName, string ProviderName,string CallcenterOpenNoteStr, string AddCCMailRequestNameStr,string TicketTypeID, string TicketCategoryID, string TicketPriorityID)
        {

            try
            {
                using (PhNetworkEntities db=new PhNetworkEntities())
                {
                    EmailApprovalRequest req = new EmailApprovalRequest();
                    System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
                    req.PatientName = usernameStr;
                    req.Medical_ID = Convert.ToInt32(MedicalIDStr);
                    req.CompanyName = ClientName;
                    req.ProviderName = ProviderName;
                    req.MailSubject = MailSubject;
                    req.CreatedByNotes = CallcenterOpenNoteStr;
                    req.CreatedBy = AddCCMailRequestNameStr;
                    req.CreationDate = DateTime.Now;
                    //  req.CreatorAttachedPath = CcEmailAttachedStr;
                    req.RequstStatusID = (int)RequestStatus.PendingDoctorsAssign;
                    req.TicketTypeID = Convert.ToInt32(TicketTypeID);
                    req.ApprovalCategoryID = Convert.ToInt32(TicketCategoryID);
                    req.PriorityID = Convert.ToInt32(TicketPriorityID);
                    req.IsInquiryTicket = false;
                    req.IsAutoGenereated = false;
                    db.EmailApprovalRequests.Add(req);
                    db.SaveChanges();

                    return req.ID.ToString();


                }
            }
            catch (Exception ex)
            {
                return null;
            }


        }
        public List<EmailApprovalRequest> GetAllReqByCallcenterUser(string user, string status)
        {
            try
            {
                using (PhNetworkEntities db=new PhNetworkEntities())
                {
                    List<EmailApprovalRequest> data = (from p in db.EmailApprovalRequests
                                                       where p.RequstStatusID.ToString() == status && p.CreatedBy == user
                                                       orderby p.ID descending
                                                       select p).ToList();
                    return data;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<EmailApprovalRequest> GetAllReqByCallcenterUserWhenAssign(string status)
        {
            try
            {
                using (PhNetworkEntities db=new PhNetworkEntities())
                {
                    List<EmailApprovalRequest> data = (from p in db.EmailApprovalRequests
                                                       where p.RequstStatusID.ToString() == status
                                                       orderby p.ID descending
                                                       select p).ToList();
                    return data;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<EmailApprovalRequest> GetAllReqBYCallcenterManager(string status)
        {
            try
            {
                using (PhNetworkEntities db=new PhNetworkEntities())
                {
                    List<EmailApprovalRequest> data = (from p in db.EmailApprovalRequests
                                                       where p.RequstStatusID.ToString() == status
                                                       orderby p.ID descending
                                                       select p).ToList();
                    return data;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<EmailApprovalRequest> GetAllReqByCallcenterDoctor(string user, string status)
        {
            try
            {
                using (PhNetworkEntities db=new PhNetworkEntities())
                {
                    List<EmailApprovalRequest> data = (from p in db.EmailApprovalRequests
                                                       where (p.RequstStatusID.ToString() == status && p.DoctorAssignee == user) || p.RequstStatusID.ToString() == "UnAssignedEmailRequest"
                                                       orderby p.ID descending
                                                       select p).ToList();
                    return data;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public EmailApprovalRequest GetEmailApprovalRequestsDetailByid(int CCReqid)
        {
            try
            {
                using (PhNetworkEntities db=new PhNetworkEntities())
                {
                    var data = (from p in db.EmailApprovalRequests
                                where p.ID == CCReqid
                                select p).FirstOrDefault();
                    return data;

                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<ModelViewTable> GetAllSearchBID(string word)
        {
            try
            {
                using (PhNetworkEntities db = new PhNetworkEntities())
                {
                    int medicalID = Convert.ToInt32(word);
                    List<ModelViewTable> data = (from p in SearchAllRequests("N/A", null, 1, null)
                                                 where p.MedicalID == medicalID || p.ID == medicalID
                                                 orderby p.ID descending
                                                 select p).ToList()
                                                 ;
                    return data;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<ModelViewTable> GetAllSearch(string word)
        {
            try
            {
                using (PhNetworkEntities db=new PhNetworkEntities())
                {
                    int medicalID = Convert.ToInt32(word);
                    List<ModelViewTable> data = (from p in SearchAllRequests("N/A", null,1,null)
                                                       where p.MedicalID== medicalID || p.ID == medicalID
                                                       orderby p.ID descending
                                                       select p).ToList();
                    return data;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<ModelViewTable> SearchAllRequests(string Type, string SubType, int? asc,int? status )
        {
           
                using (PhNetworkEntities Db = new PhNetworkEntities())
                {
                    List<dynamic> FinalResult = new List<dynamic>();


                  //IQueryable<ModelViewTable> FinalResult = new IQueryable<ModelViewTable>();
                   
                    if (Type == "N/A" && asc == 1)
                    {

                    FinalResult =Db.SP_SelectAll_By_Status(status).OrderBy(x=>x.ID).Cast<dynamic>().ToList();


                }
                    if (Type == "N/A" && asc == 2)
                    {
                    FinalResult =Db.SP_SelectAll_By_Status(status).OrderByDescending(x => x.ID).Cast<dynamic>().ToList();
                }
                    if (Type == "Priority" && asc == 1 && SubType == "")
                    {
                    FinalResult =
                         Db.SP_Select_By_Priority_Asc(SubType, status).Cast<dynamic>().ToList();
                        
                        //data = data.OrderBy(x => x.PriorityID).OrderBy(x => x.ID);
                    }
                    if (Type == "Priority" && asc == 2 && SubType == "")
                    {
                        FinalResult = 
                        Db.SP_Select_By_Priority_Desc(SubType, status).Cast<dynamic>().ToList();


                    //data = data.OrderByDescending(x => x.PriorityID).OrderByDescending(x => x.ID);


                }
                    if (Type == "Priority" && SubType != "" && asc == 1)
                    {

                        FinalResult =
                        Db.SP_Select_By_Priority_Asc(SubType, status).Cast<dynamic>().ToList();


                    // data = data.Where(x => x.PriorityID == Convert.ToInt32(SubType)).OrderBy(x => x.ID);


                }
                    if (Type == "Priority" && SubType != "" && asc == 2)
                    {
                        FinalResult = 

                        Db.SP_Select_By_Priority_Desc(SubType, status).Cast<dynamic>().ToList();

                    // data = data.Where(x => x.PriorityID == Convert.ToInt32(SubType)).OrderByDescending(x => x.ID);


                }
                    if (Type == "ApprovalCategoty" && asc == 1 && SubType == "")
                    {
                        FinalResult =
                        Db.SP_Select_By_Category_Asc(SubType, status).Cast<dynamic>().ToList();


                    //data = data.OrderBy(x => x.ApprovalCategoryID).OrderBy(x => x.ID);

                }
                    if (Type == "ApprovalCategoty" && asc == 2 && SubType == "")
                    {
                        FinalResult = 

                       Db.SP_Select_By_Category_Desc(SubType, status).Cast<dynamic>().ToList();

                    // data = data.OrderByDescending(x => x.ApprovalCategoryID).OrderByDescending(x => x.ID);

                }
                    if (Type == "ApprovalCategoty" && SubType != "" && asc == 1)
                    {
                        FinalResult =
                        Db.SP_Select_By_Category_Asc(SubType, status).Cast<dynamic>().ToList();

                }
                    if (Type == "ApprovalCategoty" && SubType != "" && asc == 2)
                    {

                        FinalResult = 
                         Db.SP_Select_By_Category_Desc(SubType, status).Cast<dynamic>().ToList();

                }
                    if (Type == "Time" && asc == 1)
                    {

                        FinalResult = 
                        Db.SP_Select_By_Time_Asc(status).Cast<dynamic>().ToList();


                }
                    if (Type == "Time" && asc == 2)
                    {
                        FinalResult = 
                       Db.SP_Select_By_Time_desc(status).Cast<dynamic>().ToList();

                }
                    if (Type == "TicketType" && asc == 1 && SubType == "")
                    {

                        FinalResult = 
                         Db.SP_Select_By_Type_Asc(SubType, status).Cast<dynamic>().ToList();


                }
                    if (Type == "TicketType" && asc == 2 && SubType == "")
                    {

                        FinalResult = 
                     Db.SP_Select_By_Type_Desc(SubType, status).Cast<dynamic>().ToList();

                }
                    if (Type == "TicketType" && SubType != "")
                    {
                        FinalResult = 
                         Db.SP_Select_By_Type_Desc(SubType, status).Cast<dynamic>().ToList();


                }
                    if (Type == "TicketType" && SubType != "" && asc == 1)
                    {
                        FinalResult = 
                       Db.SP_Select_By_Type_Asc(SubType, status).Cast<dynamic>().ToList();

                }
                    if (Type == "TicketType" && SubType != "" && asc == 2)
                    {
                        FinalResult = 
                        Db.SP_Select_By_Type_Asc(SubType, status).Cast<dynamic>().ToList();

                    //data = data.Where(x => x.TicketTypeID == Convert.ToInt32(SubType))
                    //    .OrderByDescending(x => x.ID);

                }




                var data =
        (from cust in FinalResult


         select new ModelViewTable
         {
             ID = cust.ID,
             PatientName = cust.PatientName,
             MedicalID = cust.Medical_ID,
             CompanyName = cust.CompanyName,
             DoctorAssignee = cust.DoctorAssignee,
             CreatedBy = cust.CreatedBy,
             CreationDate = cust.CreationDate,
             
             RequstStatusID = cust.RequstStatusID,

             AuditAssignee = cust.AuditAssignee,
             //AuditNotes =cust.AuditNotes,

             AuditActionTime = cust.AuditActionTime,
             OperatorAssignee = cust.OperatorAssignee,
             ProviderName = cust.ProviderName,
             PriorityID = cust.PriorityID,
             ApprovalCategoryID = cust.ApprovalCategoryID,
             TicketTypeID = cust.TicketTypeID,
             ClosedTime = cust.ClosedTime,
             MailSubject = cust.MailSubject.Length > 40 ? cust.MailSubject.Substring(40).ToString() + "..." : cust.MailSubject,

             RequestTypeName= cust.Name,
             ColorHex = cust.ColorHex,

             RequestStatusName =cust.StatusName,
             Priority = cust.Priority,
             CategoryName = cust.CategoryName,
         });





























                return data.ToList();
                }

            
          
        }


        public List<ModelViewSearchTable> SearchAllRequestsModified(string Type, string SubType, int? asc, int? status)
        {

            using (PhNetworkEntities Db = new PhNetworkEntities())
            {
                List<dynamic> FinalResult = new List<dynamic>();
                //IQueryable<ModelViewTable> FinalResult = new IQueryable<ModelViewTable>();

                if (Type == "N/A" && asc == 1)
                {

                    FinalResult = Db.SP_SelectAll_By_Status(status).OrderBy(x => x.ID).Cast<dynamic>().ToList();


                }
                if (Type == "N/A" && asc == 2)
                {
                    FinalResult = Db.SP_SelectAll_By_Status(status).OrderByDescending(x => x.ID).Cast<dynamic>().ToList();
                }
                if (Type == "Priority" && asc == 1 && SubType == "")
                {
                    FinalResult =
                         Db.SP_Select_By_Priority_Asc(SubType, status).Cast<dynamic>().ToList();

                    //data = data.OrderBy(x => x.PriorityID).OrderBy(x => x.ID);
                }
                if (Type == "Priority" && asc == 2 && SubType == "")
                {
                    FinalResult =
                    Db.SP_Select_By_Priority_Desc(SubType, status).Cast<dynamic>().ToList();


                    //data = data.OrderByDescending(x => x.PriorityID).OrderByDescending(x => x.ID);


                }
                if (Type == "Priority" && SubType != "" && asc == 1)
                {

                    FinalResult =
                    Db.SP_Select_By_Priority_Asc(SubType, status).Cast<dynamic>().ToList();


                    // data = data.Where(x => x.PriorityID == Convert.ToInt32(SubType)).OrderBy(x => x.ID);


                }
                if (Type == "Priority" && SubType != "" && asc == 2)
                {
                    FinalResult =

                    Db.SP_Select_By_Priority_Desc(SubType, status).Cast<dynamic>().ToList();

                    // data = data.Where(x => x.PriorityID == Convert.ToInt32(SubType)).OrderByDescending(x => x.ID);


                }
                if (Type == "ApprovalCategoty" && asc == 1 && SubType == "")
                {
                    FinalResult =
                    Db.SP_Select_By_Category_Asc(SubType, status).Cast<dynamic>().ToList();


                    //data = data.OrderBy(x => x.ApprovalCategoryID).OrderBy(x => x.ID);

                }
                if (Type == "ApprovalCategoty" && asc == 2 && SubType == "")
                {
                    FinalResult =

                   Db.SP_Select_By_Category_Desc(SubType, status).Cast<dynamic>().ToList();

                    // data = data.OrderByDescending(x => x.ApprovalCategoryID).OrderByDescending(x => x.ID);

                }
                if (Type == "ApprovalCategoty" && SubType != "" && asc == 1)
                {
                    FinalResult =
                    Db.SP_Select_By_Category_Asc(SubType, status).Cast<dynamic>().ToList();

                }
                if (Type == "ApprovalCategoty" && SubType != "" && asc == 2)
                {

                    FinalResult =
                     Db.SP_Select_By_Category_Desc(SubType, status).Cast<dynamic>().ToList();

                }
                if (Type == "Time" && asc == 1)
                {

                    FinalResult =
                    Db.SP_Select_By_Time_Asc(status).Cast<dynamic>().ToList();


                }
                if (Type == "Time" && asc == 2)
                {
                    FinalResult =
                   Db.SP_Select_By_Time_desc(status).Cast<dynamic>().ToList();

                }
                if (Type == "TicketType" && asc == 1 && SubType == "")
                {

                    FinalResult =
                     Db.SP_Select_By_Type_Asc(SubType, status).Cast<dynamic>().ToList();


                }
                if (Type == "TicketType" && asc == 2 && SubType == "")
                {

                    FinalResult =
                 Db.SP_Select_By_Type_Desc(SubType, status).Cast<dynamic>().ToList();

                }
                if (Type == "TicketType" && SubType != "")
                {
                    FinalResult =
                     Db.SP_Select_By_Type_Desc(SubType, status).Cast<dynamic>().ToList();


                }
                if (Type == "TicketType" && SubType != "" && asc == 1)
                {
                    FinalResult =
                   Db.SP_Select_By_Type_Asc(SubType, status).Cast<dynamic>().ToList();

                }
                if (Type == "TicketType" && SubType != "" && asc == 2)
                {
                    FinalResult =
                    Db.SP_Select_By_Type_Asc(SubType, status).Cast<dynamic>().ToList();

                    //data = data.Where(x => x.TicketTypeID == Convert.ToInt32(SubType))
                    //    .OrderByDescending(x => x.ID);

                }

                var data =
        (from cust in FinalResult


         select new ModelViewSearchTable
         {
             ID = cust.ID,
           
             MedicalID = cust.Medical_ID,
            
             DoctorAssignee = cust.DoctorAssignee,
             CreatedBy = cust.CreatedBy,
           

             RequstStatusID = cust.RequstStatusID,

             AuditAssignee = cust.AuditAssignee,
          

            
             OperatorAssignee = cust.OperatorAssignee,
            
             PriorityID = cust.PriorityID,
             ApprovalCategoryID = cust.ApprovalCategoryID,
             TicketTypeID = cust.TicketTypeID,
          
             MailSubject = cust.MailSubject.Length > 40 ? cust.MailSubject.Substring(40).ToString() + "..." : cust.MailSubject,

             RequestTypeName = cust.Name,
          

             RequestStatusName = cust.StatusName,
             Priority = cust.Priority,
             CategoryName = cust.CategoryName,
         });

                return data.ToList();
            }



        }

        public RequestDetailsViewModel SelectRequestDetails(int ID )
        {
            //using (PhNetworkEntities Db = new PhNetworkEntities())
            //{
            //  var Request = Db.SP_Select_Request(ID).First().;
            //    Request


            //}


                return null;


        }


            //public List<ModelViewTable> SearchAllRequestsCount(string Type, string SubType, int? asc, int? status)
            //{

            //    using (PhNetworkEntities Db = new PhNetworkEntities())
            //    {
            //        List<dynamic> FinalResult = new List<dynamic>();













            //        //IQueryable<ModelViewTable> FinalResult = new IQueryable<ModelViewTable>();

            //        if (Type == "N/A" && asc == 1)
            //        {

            //            FinalResult = Db.SP_SelectAll_By_Status(status).OrderBy(x => x.ID).Cast<dynamic>().ToList();


            //        }
            //        if (Type == "N/A" && asc == 2)
            //        {
            //            FinalResult = Db.SP_SelectAll_By_Status(status).OrderByDescending(x => x.ID).Cast<dynamic>().ToList();
            //        }
            //        if (Type == "Priority" && asc == 1 && SubType == "")
            //        {
            //            FinalResult =
            //                 Db.SP_Select_By_Priority_Asc(SubType, status).Cast<dynamic>().ToList();

            //            //data = data.OrderBy(x => x.PriorityID).OrderBy(x => x.ID);
            //        }
            //        if (Type == "Priority" && asc == 2 && SubType == "")
            //        {
            //            FinalResult =
            //            Db.SP_Select_By_Priority_Desc(SubType, status).Cast<dynamic>().ToList();


            //            //data = data.OrderByDescending(x => x.PriorityID).OrderByDescending(x => x.ID);


            //        }
            //        if (Type == "Priority" && SubType != "" && asc == 1)
            //        {

            //            FinalResult =
            //            Db.SP_Select_By_Priority_Asc(SubType, status).Cast<dynamic>().ToList();


            //            // data = data.Where(x => x.PriorityID == Convert.ToInt32(SubType)).OrderBy(x => x.ID);


            //        }
            //        if (Type == "Priority" && SubType != "" && asc == 2)
            //        {
            //            FinalResult =

            //            Db.SP_Select_By_Priority_Desc(SubType, status).Cast<dynamic>().ToList();

            //            // data = data.Where(x => x.PriorityID == Convert.ToInt32(SubType)).OrderByDescending(x => x.ID);


            //        }
            //        if (Type == "ApprovalCategoty" && asc == 1 && SubType == "")
            //        {
            //            FinalResult =
            //            Db.SP_Select_By_Category_Asc(SubType, status).Cast<dynamic>().ToList();


            //            //data = data.OrderBy(x => x.ApprovalCategoryID).OrderBy(x => x.ID);

            //        }
            //        if (Type == "ApprovalCategoty" && asc == 2 && SubType == "")
            //        {
            //            FinalResult =

            //           Db.SP_Select_By_Category_Desc(SubType, status).Cast<dynamic>().ToList();

            //            // data = data.OrderByDescending(x => x.ApprovalCategoryID).OrderByDescending(x => x.ID);

            //        }
            //        if (Type == "ApprovalCategoty" && SubType != "" && asc == 1)
            //        {
            //            FinalResult =
            //            Db.SP_Select_By_Category_Asc(SubType, status).Cast<dynamic>().ToList();

            //        }
            //        if (Type == "ApprovalCategoty" && SubType != "" && asc == 2)
            //        {

            //            FinalResult =
            //             Db.SP_Select_By_Category_Desc(SubType, status).Cast<dynamic>().ToList();

            //        }
            //        if (Type == "Time" && asc == 1)
            //        {

            //            FinalResult =
            //            Db.SP_Select_By_Time_Asc(status).Cast<dynamic>().ToList();


            //        }
            //        if (Type == "Time" && asc == 2)
            //        {
            //            FinalResult =
            //           Db.SP_Select_By_Time_desc(status).Cast<dynamic>().ToList();

            //        }
            //        if (Type == "TicketType" && asc == 1 && SubType == "")
            //        {

            //            FinalResult =
            //             Db.SP_Select_By_Type_Asc(SubType, status).Cast<dynamic>().ToList();


            //        }
            //        if (Type == "TicketType" && asc == 2 && SubType == "")
            //        {

            //            FinalResult =
            //         Db.SP_Select_By_Type_Desc(SubType, status).Cast<dynamic>().ToList();

            //        }
            //        if (Type == "TicketType" && SubType != "")
            //        {
            //            FinalResult =
            //             Db.SP_Select_By_Type_Desc(SubType, status).Cast<dynamic>().ToList();


            //        }
            //        if (Type == "TicketType" && SubType != "" && asc == 1)
            //        {
            //            FinalResult =
            //           Db.SP_Select_By_Type_Asc(SubType, status).Cast<dynamic>().ToList();

            //        }
            //        if (Type == "TicketType" && SubType != "" && asc == 2)
            //        {
            //            FinalResult =
            //            Db.SP_Select_By_Type_Asc(SubType, status).Cast<dynamic>().ToList();

            //            //data = data.Where(x => x.TicketTypeID == Convert.ToInt32(SubType))
            //            //    .OrderByDescending(x => x.ID);

            //        }




            //        var data =
            //(from cust in FinalResult


            // select new ModelViewTable
            // {
            //     ID = cust.ID,
            //     PatientName = cust.PatientName,
            //     MedicalID =  cust.MedicalID,
            //     CompanyName = cust.CompanyName,
            //     DoctorAssignee = cust.DoctorAssignee,
            //     CreatedBy = cust.CreatedBy,
            //     CreationDate = cust.CreationDate,

            //     RequstStatusID = cust.RequstStatusID,

            //     AuditAssignee = cust.AuditAssignee,
            //     //AuditNotes = "", //cust.AuditNotes,

            //     AuditActionTime = cust.AuditActionTime,
            //     OperatorAssignee = cust.OperatorAssignee,
            //     ProviderName = cust.ProviderName,
            //     PriorityID = cust.PriorityID,
            //     ApprovalCategoryID = cust.ApprovalCategoryID,
            //     TicketTypeID = cust.TicketTypeID,
            //     ClosedTime = cust.ClosedTime,
            //     MailSubject = cust.MailSubject.Length > 40 ? cust.MailSubject.Substring(40).ToString() + "..." : cust.MailSubject,


            //     ColorHex = cust.ColorHex,

            //     RequestStatusName =cust.StatusName,
            //     Priority = cust.Priority,
            //     CategoryName =cust.Categories_name,
            // });





























            //        return data.ToList();
            //    }



            //}
        }
}