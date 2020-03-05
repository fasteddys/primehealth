using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using CallCenterNotesApp.DLL;

namespace CallCenterNotesApp.BLL
{
    public class ClaimsApprovalClass
    {
        PhNetworkEntities db = new PhNetworkEntities();
        public void GetApprovalRequestDetailsByid(int id, ref string RequestedUsernamestr, ref string CreationTimestr, ref string MedicalIDstr, ref string Reqtypestr, ref string RequestTitlestr, ref string RequestSubjectstr, ref string UserImagepathstr, ref string CallCenterCommentstr, ref string CallCenterImagepathstr, ref string StatusStr, ref string CallCenterImageRealPath, ref string CallCenterCommentPersonstr, ref string CallCenterCommentDatestr, ref string UserImage2pathstr, ref string UserImage3pathstr, ref string UserImage4pathstr, ref string UserImage5pathstr)
        {

            var data = (from p in db.ClaimsApprovals where p.id == id select p).FirstOrDefault();

            RequestedUsernamestr = data.userName;
            CreationTimestr = data.CreationTime.ToString();
            MedicalIDstr = data.medicalid;
            Reqtypestr = data.Reqtype;
            RequestTitlestr = data.ReqTitle;
            RequestSubjectstr = data.ReqSubject;
            UserImagepathstr = data.imageName;
            UserImage2pathstr = data.ImgName2;
            UserImage3pathstr = data.ImgName3;
            UserImage4pathstr = data.ImgName4;
            UserImage5pathstr = data.ImgName5;
            StatusStr = data.ReqStatus;
            CallCenterCommentstr = data.CallCenterComment;
            CallCenterImagepathstr = data.RepliedImageStrg;
            CallCenterImageRealPath = data.RepliedImageStrPath;
            CallCenterCommentPersonstr = data.CallCenterName;
            CallCenterCommentDatestr = data.ApproveOrRejectTime.ToString();
        }
        public void GetFeedbackRequestDetailsByid(int id, ref string Usernamestr, ref string MedicalIDstr, ref string FBReqTitlestr, ref string FBSubjectstr, ref string PrimeHealthComment, ref string StatusStr, ref string RepliedPersonstr, ref string RepliedDatestr, ref string PrimeHealthCommentHeaderstr, ref string CreationTimeStr)
        {

            var data = from p in db.Feedbacks where p.id == id select p;

            Usernamestr = data.First().UserName;
            MedicalIDstr = data.First().MedicalID;
            FBReqTitlestr = data.First().Title;
            FBSubjectstr = data.First().Subject;
            CreationTimeStr = data.First().CreationTime.ToString();
            PrimeHealthComment = data.First().RepliedMessages;
            PrimeHealthCommentHeaderstr = data.First().RepliedTitle;
            StatusStr = data.First().Seen;
            RepliedPersonstr = data.First().RepliedPerson;
            RepliedDatestr = data.First().RepliedTime.ToString();
        }

        public void Update_To_Seen(int id, string Header, string User)
        {
            Feedback acc = db.Feedbacks.Single(p => p.id == id);
            acc.RepliedTitle = Header;
            acc.RepliedMessages = "No Replied";
            acc.RepliedPerson = User;
            acc.Seen = "Yes";
            acc.RepliedTime = DateTime.Now;
            db.SaveChanges();

        }
        //public void UpdateRequestWithCallCenterApprovedResponse(int id, string UserNamestr, string CallCenterCommentstr, string base64Stringstr, string CallCenterImageRealPathstr)
        //{
        //    ClaimsApproval acc = db.ClaimsApprovals.Single(p => p.id == id);

        //    acc.CallCenterName = UserNamestr;
        //    acc.ApproveOrRejectTime = DateTime.Now;
        //    acc.CallCenterComment = CallCenterCommentstr;
        //    acc.RepliedImageStrg = base64Stringstr;
        //    acc.RepliedImageStrPath = CallCenterImageRealPathstr;
        //    acc.ReqStatus = "Approved";
        //    System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
        //    db.SaveChanges();
        //}
        public void UpdateRequestWithCallCenterResponse(int id, string UserNamestr, string CallCenterCommentstr, string base64Stringstr, string CallCenterImageRealPathstr,string ApproveOrReject)
        {
            ClaimsApproval acc = db.ClaimsApprovals.Single(p => p.id == id);

            acc.CallCenterName = UserNamestr;
            acc.ApproveOrRejectTime = DateTime.Now;
            acc.CallCenterComment = CallCenterCommentstr;
            acc.RepliedImageStrg = base64Stringstr;
            acc.RepliedImageStrPath = CallCenterImageRealPathstr;
            acc.ReqStatus = ApproveOrReject;
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            db.SaveChanges();
        }
        public void UpdateFeedback(int id, string Header , string Comment, string User)
        {
            Feedback acc = db.Feedbacks.Single(p => p.id == id);
            acc.RepliedTitle = Header;
            acc.RepliedMessages = Comment;
            acc.RepliedPerson = User;
            acc.Seen = "Yes";
            acc.RepliedTime = DateTime.Now;
            db.SaveChanges();
        }
        public List<ClaimsApproval> GetAllApprovals(string type , string status)
        {
            List<ClaimsApproval> data = (from p in db.ClaimsApprovals
                                         where p.Reqtype==type && p.ReqStatus==status
                                         orderby p.userName ascending
                                         select p).ToList();
            return data;
        }
        public List<Feedback> GetAllFeedback(string status)
        {
            if (status == "Yes")
            {
                List<Feedback> data = (from p in db.Feedbacks
                                       where p.Seen == status
                                       orderby p.id ascending
                                       select p).ToList();
                return data;
            }
            else if (status == "No")
            {
                List<Feedback> data = (from p in db.Feedbacks
                                       where p.Seen == null
                                       orderby p.id descending
                                       select p).ToList();
                return data;
            }
            else 
            {
                List<Feedback> data = (from p in db.Feedbacks
                                       where p.Seen == "not found"
                                       orderby p.id descending
                                       select p).ToList();
                return data;
            }

        }
        public void GetUserDataByMedicalID(string Medicalid, ref string UserNamestr, ref string Emailstr, ref string Typestr)
        {

            var data = (from p in db.Loginers where p.Medical_id == Medicalid select p).FirstOrDefault();
            if (data != null)
            {
                UserNamestr = data.Name;
                Emailstr = data.Email;
                Typestr = data.gender;
            }
            
        }
        public List<Loginer> GetAllUers(string MedicalIDstr)
        {
                List<Loginer> data = (from p in db.Loginers
                                      where p.Medical_id == MedicalIDstr
                                       orderby p.id ascending
                                       select p).ToList();
                return data;
        }

    }
}