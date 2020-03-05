using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.DLL;

namespace WebApplication1.BLL
{
    public class Requests
    {
        ArchiveDataContext db = new ArchiveDataContext();

        #region AddRequest
        public void addrequestToArchiving(string head, string body, string src, string Fuser, string addedType, string reqestteditems, out int lastID)
        {
            requestTB req = new requestTB();
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            req.rSubject = head;
            req.rBody = body;
            req.rDate = DateTime.Now;
            req.rFrom = Fuser;
            req.rStatus = "New";
            req.rAddedByType = addedType;
            req.Assigned = "false";
            req.rStatusColor = "#68c39f";
            req.rApproved = "false";
            req.AssigenPerson = "";
            req.userpendingclaimscomment = "";
            req.rAttach = src;
            req.NumberOfReqClaims = int.Parse(reqestteditems);
            req.NumberOfFoundClaims = 0;
            req.Reply = "";
            req.pendingclaims = "false";
            db.requestTBs.InsertOnSubmit(req);
            db.SubmitChanges();
            var lastinsertedId = req.id;
            lastID = lastinsertedId;
        }
        #endregion

        #region Admin & Archiing
        //get all closed requests for admin
        public List<requestTB> GetAllReqClosed()
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rStatus == "Closed"
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }
        //get all new requests for admin and archiving
        public List<requestTB> GetAllReqNew()
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rStatus == "New"
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }

        public List<requestTB> GetAllReqNewNotAssigned()
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.Assigned == "false"
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }


        public List<requestTB> GetAllReqPConfirm()
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rStatus == "Pending Confirmation" && p.rAddedByType == "User"
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }

        public List<requestTB> GetAllReqRembPConfirm()
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rStatus == "Pending Confirmation" && p.rAddedByType == "Remb" || p.rAddedByType == "Data Entry" && p.rStatus == "Pending Confirmation"
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }

        public int GetAllReqRembPConfirmCount()
        {
            int data = (from p in db.requestTBs
                        where p.rStatus == "Pending Confirmation" && p.rAddedByType == "Remb" || p.rStatus == "Pending Confirmation" && p.rAddedByType == "Data Entry"
                        orderby p.id descending
                        select p).Count();
            return data;
        }
        public List<requestTB> GetAllReqSearching()
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rStatus == "Searching"
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }

        public List<requestTB> GetAllPendingClaims()
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rStatus == "Pending Claims"
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }

        public List<requestTB> GetAllPendingClaimsForUser(string user)
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rStatus == "Pending Claims" && p.rFrom==user
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }

        public List<requestTB> GetAllSearchBID(string word)
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.id == int.Parse(word)
                                    orderby p.id ascending
                                    select p).ToList();
            return data;
        }

        public List<requestTB> GetAllSearchBDate(string word)
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rDate.Value.Date == DateTime.Parse(word).Date
                                    orderby p.id ascending
                                    select p).ToList();
            return data;
        }

        public List<requestTB> GetAllSearch(string word)
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rSubject.Contains(word) || p.rFrom.Contains(word) || p.rBody.Contains(word) || p.rAttach.Contains(word) || p.ArchivingComment.Contains(word) ||p.pendingcomments.Contains(word) || p.PendClaimsassignee.Contains(word) || p.AssigenPerson.Contains(word)
                                    orderby p.id ascending
                                    select p).ToList();
            return data;
        }

        #endregion

        #region Users Function
        public List<requestTB> GetAllSearchByID(string word)
        {
            List<requestTB> data = (from p in db.requestTBs
                                              where p.id == int.Parse(word)
                                              orderby p.id ascending
                                              select p).ToList();
            return data;
        }
        public List<requestTB> GetAllSearchBySubject(string word)
        {
            List<requestTB> data = (from p in db.requestTBs
                                              where p.rBody.Contains(word)
                                              orderby p.id ascending
                                              select p).ToList();
            return data;
        }
        public List<requestTB> GetAllSearchBySub(string word)
        {
            List<requestTB> data = (from p in db.requestTBs
                                              where p.rSubject.Contains(word)
                                              orderby p.id ascending
                                              select p).ToList();
            return data;
        }
        public List<requestTB> GetAllSearchByCreator(string word)
        {
            List<requestTB> data = (from p in db.requestTBs
                                              where p.rFrom.Contains(word)
                                              orderby p.id ascending
                                              select p).ToList();
            return data;
        }
        public List<requestTB> GetAllSearchelmentsByCreator(string word)
        {
            List<requestTB> data = (from p in db.requestTBs
                                              where p.rFrom.Contains(word)
                                              orderby p.id ascending
                                              select p).ToList();
            return data;
        }
        public List<requestTB> GetAllSearchByArchivingcomment(string word)
        {
            List<requestTB> data = (from p in db.requestTBs
                                              where p.ArchivingComment.Contains(word)
                                              orderby p.id ascending
                                              select p).ToList();
            return data;
        }
        public List<requestTB> GetAllSearchByAttachment(string word)
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rAttach.Contains(word)
                                    orderby p.id ascending
                                    select p).ToList();
            return data;
        }

        public List<requestTB> GetAllReqSearchingByUser(string user)
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rStatus == "Searching" && p.rFrom == user
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }

        public List<requestTB> GetAllReqSearchingByArchivingforadmin()//update
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rStatus == "Searching"
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }
        public List<requestTB> GetAllReqPendingDataEntryByUser(string user)
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rStatus == "PendingDataEntry" && p.rFrom == user
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }
        public List<requestTB> GetAllReqPendingDataEntry()
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rStatus == "PendingDataEntry"
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }

        public List<requestTB> GetAllReqsubmittedByUser(string user)
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rStatus == "Submitted" && p.rFrom == user
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }

        public List<requestTB> GetAllReqsubmitted()
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rStatus == "Submitted"
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }

        public List<requestTB> GetAllReqRejectedByUser(string user)
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rStatus == "Rejected" && p.rFrom == user
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }
        public List<requestTB> GetAllReqSearchingByArchiving(string user)
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rStatus == "Searching" && p.AssigenPerson == user
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }

        public List<requestTB> GetAllReqUnassigned(string user)
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rStatus == "New" && p.rFrom == user
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }

        public List<requestTB> GetAllReqUnassignedForEnterpriseAdmin()
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rStatus == "New"
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }

        public int GetAllReqUnassignedForEnterpriseAdminCount()
        {
            var data = (from p in db.requestTBs
                        where p.rStatus == "New"
                        select p).Count();
            int count = (int)data;
            return count;
        }

        public List<requestTB> GetAllReqRejected()
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rStatus == "Rejected"
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }
        public List<requestTB> GetAllReqClosedBYACM(string user)
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rStatus == "Closed" && p.rFrom == user
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }
        public List<requestTB> GetAllReqPConfirmBYACManger(string user)
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rStatus == "Pending Confirmation" && p.rFrom == user
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }
        public List<requestTB> GetAllByACC(string user)
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rFrom == user
                                    orderby p.id ascending
                                    select p).ToList();
            return data;
        }
        #endregion

        #region Updates
        public void UpdateClosedArchiving(int id)
        {
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            requestTB acc = db.requestTBs.Single(p => p.id == id);
            acc.rStatus = "Closed";
            acc.rStatusColor = "#e15554";
            acc.rApproved = "true";
            acc.ApprovedDate = DateTime.Now;
            db.SubmitChanges();
        }

        public void UpdatePendingClaims(int id, string usercomment,string pendingclaimsreasonbyuser)
        {
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            requestTB acc = db.requestTBs.Single(p => p.id == id);
            acc.rStatus = "Pending Claims";
            acc.rStatusColor = "#ffc052";
            acc.pendingclaims = "true";
            acc.rBody = usercomment;
            acc.PendingCliamsStart = DateTime.Now;
            acc.userpendingclaimscomment = pendingclaimsreasonbyuser;
            db.SubmitChanges();
        }

        public void EndPendingClaims(int id,string claimscomment)
        {
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            requestTB acc = db.requestTBs.Single(p => p.id == id);
            acc.rStatus = "Searching";
            acc.pendingcomments = claimscomment;
            acc.rStatusColor = "#ffc052";
            acc.PendingClaimsEnd = DateTime.Now;
            db.SubmitChanges();
        }
        public void ClosedByDataEntry(int id)
        {
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            requestTB acc = db.requestTBs.Single(p => p.id == id);
            acc.rStatus = "Closed";
            acc.rStatusColor = "#e15554";
            acc.ApprovedDate = DateTime.Now;
            db.SubmitChanges();
        }

        public void updateAssignPerson(int id, string user)
        {
            requestTB acc = db.requestTBs.Single(p => p.id == id);
            acc.AssigenPerson = user;
            acc.Assigned = "true";
            acc.rStatus = "Searching";
            acc.rStatusColor = "#ffc052";
            acc.AssignedTime = DateTime.Now;
            db.SubmitChanges();
        }

        public void updateAssignPersonforAdmin(int id, string user)
        {
            requestTB acc = db.requestTBs.Single(p => p.id == id);
            if (user=="----")
            {
                
            }
            else if (acc.rStatus == "New")
            {
                acc.AssigenPerson = user;
                acc.Assigned = "true";
                acc.rStatus = "Searching";
                acc.rStatusColor = "#ffc052";
                acc.AssignedTime = DateTime.Now;
            }
            else
            {
                acc.AssigenPerson = user;

            }


            db.SubmitChanges();
        }


        public void updateRejected(int id, string user)
        {
            requestTB acc = db.requestTBs.Single(p => p.id == id);
            acc.AssigenPerson = user;
            acc.Assigned = "true";
            acc.rStatus = "Searching";
            acc.rStatusColor = "#ffc052";
            db.SubmitChanges();
        }
        public void UpdatePending(int id, string ArchivingComment, int found)
        {

            requestTB acc = db.requestTBs.Single(p => p.id == id);
            acc.rStatus = "Pending Confirmation";
            acc.rStatusColor = "#0ea3dc";
            acc.ArchivingComment = ArchivingComment;
            acc.NumberOfFoundClaims = found;
            acc.FinishedArchivingDate = DateTime.Now;
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            db.SubmitChanges();

        }

        public void UpdateRejected(int id, string ArchivingComment, int found)
        {

            requestTB acc = db.requestTBs.Single(p => p.id == id);
            acc.rStatus = "Rejected";
            acc.rStatusColor = "#850606";
            acc.ArchivingComment = ArchivingComment;
            acc.NumberOfFoundClaims = found;
            acc.FinishedArchivingDate = DateTime.Now;
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            db.SubmitChanges();

        }

        public void Upload(int id, string attach)
        {

            requestTB acc = db.requestTBs.Single(p => p.id == id);
            acc.Reply = attach;
            db.SubmitChanges();

        }

        public void FW_Rejected(int id, string header, int claims,string usercomment)
        {
            requestTB acc = db.requestTBs.Single(p => p.id == id);
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            acc.rSubject = "FW:" + header + "";
            acc.rStatus = "New";
            acc.Assigned = "false";
            acc.rStatusColor = "#68c39f";
            acc.rApproved = "false";
            acc.AssigenPerson = "";
            acc.rBody = usercomment;
            acc.NumberOfReqClaims = claims;
            acc.NumberOfFoundClaims = 0;
            acc.rDate = DateTime.Now;
            acc.AssignedTime = null;
            acc.FinishedArchivingDate = null;
            db.SubmitChanges();
        }

        public void BacktoArchiving(int id)
        {
            requestTB acc = db.requestTBs.Single(p => p.id == id);
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            acc.rStatus = "Searching";
            acc.rStatusColor = "#ffc052";
            db.SubmitChanges();
        }

        public void updateRembConfirm(int id)
        {
            requestTB acc = db.requestTBs.Single(p => p.id == id);
            acc.RembConfirm = "true";
            db.SubmitChanges();
        }

        public void updatependingclaimsassignee(int id,string assignee)
        {
            requestTB acc = db.requestTBs.Single(p => p.id == id);
            acc.PendClaimsassignee = assignee;
            acc.pendingassigned = "true";
            db.SubmitChanges();
        }

        public void updatependingDataentryassignee(int id, string assignee)
        {
            requestTB acc = db.requestTBs.Single(p => p.id == id);
            acc.PendClaimsassignee = assignee;
            acc.pendingassigned = "true";
            db.SubmitChanges();
        }

        public void updatetopendingdataentry(int id)
        {
            requestTB acc = db.requestTBs.Single(p => p.id == id);
            acc.rStatus = "PendingDataEntry";
            acc.rStatusColor = "#ffc052";
            db.SubmitChanges();
        }

        public void updatesubmitremb(int id)
        {
            requestTB acc = db.requestTBs.Single(p => p.id == id);
            acc.rStatus = "Submitted";
            acc.rStatusColor = "#ffc052";
            acc.sumbited = "true";
            acc.submittedbyrembtime = DateTime.Now;
            db.SubmitChanges();
        }
        #endregion

        #region
        public int total_req()
        {
            var data = (from p in db.requestTBs
                        select p).Count();
            int d = (int)data;
            return d;
        }

        public int close_req()
        {
            var data = (from p in db.requestTBs
                        where p.rStatus == "Closed"
                        select p).Count();
            int d = (int)data;
            return d;
        }

        public int conf_req()
        {
            var data = (from p in db.requestTBs
                        where p.rStatus == "Pending Confirmation"
                        select p).Count();
            int d = (int)data;
            return d;
        }

        public int GetMaxID()
        {
            var data = db.requestTBs.Max(p => p.id);
            int id = int.Parse(data.ToString());
            return id;
        }
        public int Tot_NewREQ()
        {
            var data = (from p in db.requestTBs
                        where p.rStatus == "Searching" && p.rStatusColor == "#68c39f"
                        select p).Count();
            int d = (int)data;
            return d;
        }
        public IEnumerable<requestTB> GetData()
        {
            List<requestTB> data = (from p in db.requestTBs
                                    orderby p.id ascending
                                    select p).ToList();
            return data;
        }

        public int GetDataCount()
        {
            var data = (from p in db.requestTBs
                        select p).Count();
            return data;
        }

        public int GetDataCountForUsers(string user)
        {
            var data = (from p in db.requestTBs
                        where p.rFrom == user
                        select p).Count();
            return data;
        }

        public int GetDataArchivingCount()
        {
            var data = (from p in db.requestTBs
                        where p.rStatus == "Searching"
                        select p).Count();
            return data;
        }
        public int GetDataArchivingForArchivingforadmin()/////////update
        {
            var data = (from p in db.requestTBs
                        where p.rStatus == "Searching"
                        select p).Count();
            return data;
        }


        public int GetDataArchiving()
        {
            var data = (from p in db.requestTBs
                        where p.rStatus == "Searching"
                        select p).Count();
            return data;
        }


        public int GetDataArchivingForArchiving(string user)
        {
            var data = (from p in db.requestTBs
                        where p.rStatus == "Searching" && p.AssigenPerson == user
                        select p).Count();
            return data;
        }

        public int GetDataClosedForusers(string user)
        {
            var data = (from p in db.requestTBs
                        where p.rStatus == "Closed" && p.rFrom == user
                        select p).Count();
            return data;
        }

        public int GetDataUnassigned(string user)
        {
            var data = (from p in db.requestTBs
                        where p.rStatus == "New" && p.rFrom == user
                        select p).Count();
            return data;
        }

        public string getRequestOwner(int reqID)
        {
            var data = (from p in db.requestTBs
                        where p.id == reqID
                        select p.rFrom).FirstOrDefault();

            return data;
        }

        public string getRequestclaimsassignee(int reqID)
        {
            var data = (from p in db.requestTBs
                        where p.id == reqID
                        select p.PendClaimsassignee).FirstOrDefault();

            return data;
        }

      
        public int GetDataNew()
        {
            var data = (from p in db.requestTBs
                        where p.rStatus == "New"
                        select p).Count();
            return data;
        }

        public int GetDataRejectedusers(string user)
        {
            var data = (from p in db.requestTBs
                        where p.rStatus == "Rejected" && p.rFrom == user
                        select p).Count();
            return data;
        }

        public int GetDataRejected()
        {
            var data = (from p in db.requestTBs
                        where p.rStatus == "Rejected"
                        select p).Count();
            return data;
        }

        public int GetDataSubmitted()
        {
            var data = (from p in db.requestTBs
                        where p.rStatus == "Submitted"
                        select p).Count();
            return data;
        }

        public int GetDataSubmittedforuser(string user)
        {
            var data = (from p in db.requestTBs
                        where p.rStatus == "Submitted" && p.rFrom==user
                        select p).Count();
            return data;
        }

        public int GetDataPendingClaims()
        {
            var data = (from p in db.requestTBs
                        where p.rStatus == "Pending Claims"
                        select p).Count();
            return data;
        }

        public int GetDataPendingClaimsforUser(string user)
        {
            var data = (from p in db.requestTBs
                        where p.rStatus == "Pending Claims" && p.rFrom==user
                        select p).Count();
            return data;
        }
        public int GetDataPendingDataEntry()
        {
            var data = (from p in db.requestTBs
                        where p.rStatus == "PendingDataEntry"
                        select p).Count();
            return data;
        }
        public int GetDataPendingDataEntryForUser(string user)
        {
            var data = (from p in db.requestTBs
                        where p.rStatus == "PendingDataEntry" && p.rFrom == user
                        select p).Count();
            return data;
        }

        public int GetDataClosed()
        {
            var data = (from p in db.requestTBs
                        where p.rStatus == "Closed"
                        select p).Count();
            return data;
        }

        public int GetDataArchivingForUsers(string user)
        {
            var data = (from p in db.requestTBs
                        where p.rStatus == "Searching" && p.rFrom == user
                        select p).Count();
            return data;
        }

        public int GetDataPendingForUsers(string user)
        {
            var data = (from p in db.requestTBs
                        where p.rStatus == "Pending Confirmation" && p.rFrom == user
                        select p).Count();
            return data;
        }

        public int GetDataPending()
        {
            var data = (from p in db.requestTBs
                        where p.rStatus == "Pending Confirmation" && p.rAddedByType != "Remb" && p.rAddedByType != "Data Entry"
                        select p).Count();
            return data;
        }
        public void GetDetailByid(int id, ref string sub, ref string body, ref string attach, ref string reply, ref string AccountM, ref string UserComment, ref string CAsign, ref string AssignedPerson, ref string status, ref string addedtype, ref string rembconfirm, ref int reqitems, ref int founditems,ref DateTime pendingfrom, ref DateTime pendingto , ref string pendingclaimsasigned, ref string pendingclaims , ref string pendingclaimscomments,ref string assignedforclaims,ref string submit , ref DateTime submitdate,ref string pendingusercomment)
        {
            var data = from p in db.requestTBs
                       where p.id == id

                       select p;
            CAsign = data.First().Assigned;
            AssignedPerson = data.First().AssigenPerson;
            sub = data.First().rSubject;
            body = data.First().rBody;
            attach = data.First().rAttach;
            reply = data.First().Reply;
            AccountM = data.First().rFrom;
            status = data.First().rStatus;
            UserComment = data.First().ArchivingComment;
            addedtype = data.First().rAddedByType;
            rembconfirm = data.First().RembConfirm;
            reqitems = (int)data.First().NumberOfReqClaims.GetValueOrDefault();
            founditems = (int)data.First().NumberOfFoundClaims.GetValueOrDefault();
            pendingfrom = (DateTime)data.First().PendingCliamsStart.GetValueOrDefault();
            pendingto = (DateTime)data.First().PendingClaimsEnd.GetValueOrDefault();
            pendingclaimsasigned = data.First().PendClaimsassignee;
            pendingclaims = data.First().pendingclaims;
            pendingclaimscomments = data.First().pendingcomments;
            assignedforclaims = data.First().pendingassigned;
            submit = data.First().sumbited;
            submitdate = (DateTime)data.First().submittedbyrembtime.GetValueOrDefault();
            pendingusercomment =(String)data.FirstOrDefault().userpendingclaimscomment;
           


        }

        public void DownloadLink(int id, ref string path)
        {
            var data = from p in db.requestTBs
                       where p.id == id
                       select p;
            path = data.First().rAttach;
        }
        #endregion



        #region
        public List<requestTB> GetIntervalReport(string startdate,string enddate , string user)
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.AssignedTime.Value.Date >= DateTime.Parse(startdate) && p.AssignedTime.Value.Date <= DateTime.Parse(enddate) && p.AssigenPerson == user
                                    select p).ToList();
            return data;
        }

        public int SumRequestedItems(string user, string StartDate, string EndDate)
        {
            var data = (from p in db.requestTBs
                        where p.AssignedTime.Value.Date >= DateTime.Parse(StartDate) && p.AssignedTime.Value.Date <=DateTime.Parse(EndDate) && p.AssigenPerson == user
                        select p.NumberOfReqClaims).Sum();
            int counter = (int)data;
            return counter;
        }

        public int SumFoundItems(string user, string StartDate, string EndDate)
        {
            var data = (from p in db.requestTBs
                        where p.AssignedTime.Value.Date >= DateTime.Parse(StartDate) && p.AssignedTime.Value.Date <= DateTime.Parse(EndDate) && p.AssigenPerson == user
                        select p.NumberOfFoundClaims).Sum();
            int counter = (int)data;
            return counter;
        }

        public int SumRequestedItemsForAllUsers(string StartDate, string EndDate)
        {
            var data = (from p in db.requestTBs
                        where p.AssignedTime.Value.Date >= DateTime.Parse(StartDate) && p.AssignedTime.Value.Date <= DateTime.Parse(EndDate)
                        select p.NumberOfReqClaims).Sum();
            int counter = (int)data;
            return counter;
        }

        public int SumFoundItemsForAllUsers(string StartDate, string EndDate)
        {
            var data = (from p in db.requestTBs
                        where p.AssignedTime.Value.Date >= DateTime.Parse(StartDate) && p.AssignedTime.Value.Date <= DateTime.Parse(EndDate)
                        select p.NumberOfFoundClaims).Sum();
            int counter = (int)data;
            return counter;
        }
        public int SumRequestedItemsForDailyReport()
        {
            var data = (from p in db.requestTBs
                        where p.rDate.Value.Date >=DateTime.Now.Date 
                        select p.NumberOfReqClaims).Sum();
            int counter = (int)data;
            return counter;
        }

        public int SumFoundItemsForDailyReport()
        {
            var data = (from p in db.requestTBs
                        where p.rDate.Value.Date >= DateTime.Now.Date
                        select p.NumberOfFoundClaims).Sum();
            int counter = (int)data;
            return counter;
        }


        public List<requestTB> GetAllIntervalReport(string startdate, string enddate)
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.AssignedTime.Value.Date >= DateTime.Parse(startdate) && p.AssignedTime.Value.Date <= DateTime.Parse(enddate)
                                    select p).ToList();
            return data;
        }

        public List<requestTB> GetDailyReport()
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rDate.Value.Date == DateTime.Now.Date
                                    select p).ToList();
            return data;
        }
        #endregion

    }

}