using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.DLL;


namespace WebApplication1.BLL
{
    public class subRequests
    {
        ArchiveDataContext db = new ArchiveDataContext();
       
        public void addrequestToArchiving(string head, string body, string Fuser, string sentbatch,out int lastID,string submittype,string month , string year,string path)
        {
            submissionrequestTB req = new submissionrequestTB();
       
                  
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            req.rSubject = head;
            req.rBody = body;
            req.rDate = DateTime.Now;
            req.rFrom = Fuser;
            req.rStatus = "Submitting";
            req.Assigned = "false";
            req.confirmed = "false";
            req.rStatusColor = "#68c39f";
            req.rApproved = "false";
            req.AssigenPerson = "";
            req.Submittype = submittype;
            req.NumberSentOfBatches = int.Parse(sentbatch);
            req.NumberSentOfRecBatches = 0;
            req.NumberOfRecClaims = 0;
            req.Month = month;
            req.Year = year;
            req.ClosedBy ="";
            req.PendingTPABy="";
            req.PendingTPAByTime=DateTime.Now;
            req.AttachedPath = path;
                db.submissionrequestTBs.InsertOnSubmit(req);
            db.SubmitChanges();
            var lastinsertedId = req.id;
            lastID = lastinsertedId;
        }
        public int GetMaxID()
        {
            var data = db.submissionrequestTBs.Max(p => p.id);
            int id = int.Parse(data.ToString());
            return id;
        }
        public int GetDataCount()
        {
            var data = (from p in db.submissionrequestTBs 
                        select p).Count();
            return data;
        }
        public int GetDataCount_ForArchiving()
        {
            var data = (from p in db.submissionrequestTBs
                        where p.Submittype=="InPatient"|| p.Submittype == "OutPatient" || p.Submittype==null
                        select p).Count();
            return data;
        }
        public int GetDataCount_ForTPA()
        {
            var data = (from p in db.submissionrequestTBs
                        where p.Submittype == "OutPatient"
                        select p).Count();
            return data;
        }



        public int GetDataCountTPA()
        {
            
            var data = (from p in db.submissionrequestTBs
                        where p.Submittype == "OutPatient"
                        select p).Count();
            return data;
        }
        public int GetDataCountforuser(string user)
        {
            var data = (from p in db.submissionrequestTBs
                        where p.rFrom==user
                        select p).Count();
            return data;
        }
        //------------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------------

        public int GetDataCountforNewin()
        {
            var data = (from p in db.submissionrequestTBs
                        where /*p.rStatus == "Submitting" &&*/ p.Submittype == "InPatient" || p.Submittype == "OutPatient" || p.Submittype == null
                        select p).Count();
            return data;
        }


        public int GetDataCountforNew()
        {
            var data = (from p in db.submissionrequestTBs
                        where p.rStatus == "Submitting"
                        select p).Count();
            return data;
        }

    

        public int TPA()
        {
            var data = (from p in db.submissionrequestTBs
                        where p.rStatus == "PendingTPA" && p.Submittype=="OutPatient"
                        select p).Count();
            return data;
        }

        public int GetDataCountforNewout()
        {
            var data = (from p in db.submissionrequestTBs
                        where p.rStatus == "Submitting" && p.Submittype == "OutPatient"
                        select p).Count();
            return data;
        }


        public string Getclosedby(int id)
        {
            var data = (from p in db.userTBs
                        where p.id == id
                        select p.UserName).ToString();
            return data;
        }

        public int GetDataCountforreciving()
        {
            var data = (from p in db.submissionrequestTBs
                        where p.rStatus == "Receiving"
                        select p).Count();
            return data;
        }

        public int GetDataCountforrecivingforarchiving(string user)
        {
            var data = (from p in db.submissionrequestTBs
                        where p.rStatus == "Receiving" && p.AssigenPerson==user
                        where p.Submittype=="InPatient" || p.Submittype==null
                        select p).Count();
            return data;
        }
        public int GetDataCountforrecivingforTPA(string user)
        {
            var data = (from p in db.submissionrequestTBs
                        where p.rStatus == "Receiving" && p.AssigenPerson == user
                        where p.Submittype == "OutPatient"
                        select p).Count();
            return data;
        }

        public int GetDataCountforrecivingforTPAAdmin()
        {
            var data = (from p in db.submissionrequestTBs
                        where p.rStatus == "Receiving" 
                        where p.Submittype == "OutPatient"
                        select p).Count();
            return data;
        }

        public int GetDataCountforpending()
        {
            var data = (from p in db.submissionrequestTBs
                        where p.rStatus == "Submitted"
                        select p).Count();
            return data;
        }
        public int GetDataCountforpending_Archiving()
        {
            var data = (from p in db.submissionrequestTBs
                        where p.rStatus == "Submitted" && (p.Submittype=="InPatient" || p.Submittype==null)
                        select p).Count();
            return data;
        }
        public int GetDataCountforpending_TPA()
        {
            var data = (from p in db.submissionrequestTBs
                        where p.rStatus == "Submitted" && p.Submittype == "OutPatient"
                        select p).Count();
            return data;
        }

        public int GetDataCountforclosedForEnterpriseAdmin()
        {
            var data = (from p in db.submissionrequestTBs
                        where p.rStatus == "Closing"
                        select p).Count();
            return data;
        }
        public int GetDataCountforclosed()
        {
            var data = (from p in db.submissionrequestTBs
                        where p.rStatus == "Closing"
                        where p.Submittype=="InPatient" || p.Submittype==null
                        select p).Count();
            return data;
        }
        public int GetDataCountforclosedForTPA()
        {
            var data = (from p in db.submissionrequestTBs
                        where p.rStatus == "Closing"
                        where p.Submittype == "OutPatient"
                        select p).Count();
            return data;
        }
        public int GetDataCountforRejected()
        {
            var data = (from p in db.submissionrequestTBs
                        where p.rStatus == "Rejected"
                        select p).Count();
            return data;
        }


        public int GetDataCountforclosedtickets()
        {
            var data = (from p in db.submissionrequestTBs
                        where p.rStatus == "Closed"
                        select p).Count();
            return data;
        }

        public int GetDataCountforclosedtickets_in()
        {
            var data = (from p in db.submissionrequestTBs
                        where p.rStatus == "Closed" &&( p.Submittype=="InPatient" || p.Submittype==null)
                        select p).Count();
            return data;
        }

        public int GetDataCountforclosedtickets_out()
        {
            var data = (from p in db.submissionrequestTBs
                        where p.rStatus == "Closed" && p.Submittype == "OutPatient"
                        select p).Count();
            return data;
        }
        public int GetDataCountforclosedticketsforuser(string user)
        {
            var data = (from p in db.submissionrequestTBs
                        where p.rStatus == "Closed" && p.rFrom==user
                        select p).Count();
            return data;
        }


        public int GetDataCountforclosedforuser( string user)
        {
            var data = (from p in db.submissionrequestTBs
                        where p.rStatus == "Closing" && p.rFrom==user
                        select p).Count();
            return data;
        }

        public int GetDataCountforpendingforuser(string user)
        {
            var data = (from p in db.submissionrequestTBs
                        where p.rStatus == "Submitted" && p.rFrom==user
                        select p).Count();
            return data;
        }

        public int GetDataCountforRejectedforuser(string user)
        {
            var data = (from p in db.submissionrequestTBs
                        where p.rStatus == "Rejected" && p.rFrom == user
                        select p).Count();
            return data;
        }

        public int GetDataCountforNewforUser(string user)
        {
            var data = (from p in db.submissionrequestTBs
                        where p.rStatus == "Submitting" && p.rFrom==user
                        select p).Count();
            return data;
        }

        public int GetDataCountforrecforUser(string user)
        {
            var data = (from p in db.submissionrequestTBs
                        where p.rStatus == "Receiving" && p.rFrom == user
                        select p).Count();
            return data;
        }

        public void GetDetailByid(int id, ref string sub, ref string body,ref string path ,ref string AccountM, ref string UserComment, ref string CAsign, ref string AssignedPerson, ref string status, ref int submittedbatches, ref int recievedbatches, ref int recievedclaims,ref string confirmed,ref string Batchestype,ref string mon, ref string year,ref string TPAcomment)
        {
            var data = from p in db.submissionrequestTBs
                       where p.id == id

                       select p;
            CAsign = data.First().Assigned;
            AssignedPerson = data.First().AssigenPerson;
            sub = data.First().rSubject;
            body = data.First().rBody;
            AccountM = data.First().rFrom;
            status = data.First().rStatus;
            path = data.First().AttachedPath;
            UserComment = data.First().ArchivingComment;
            TPAcomment = data.First().TPAcomment;
            submittedbatches = (int)data.First().NumberSentOfBatches.GetValueOrDefault();
            recievedbatches = (int)data.First().NumberSentOfRecBatches.GetValueOrDefault();
            recievedclaims = (int)data.First().NumberOfRecClaims.GetValueOrDefault();
            Batchestype = (string)data.First().Submittype;
            mon = (string)data.First().Month;
            year = (string)data.First().Year;
            confirmed = data.First().confirmed;
          
        }

        public List<submissionrequestTB> GetAllReqNew_out()
        {
            List<submissionrequestTB> data = (from p in db.submissionrequestTBs
                                              where p.rStatus == "Submitting" && p.Submittype == "OutPatient" 
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }
        public List<submissionrequestTB> GetAllReqNew_in()
        {
            List<submissionrequestTB> data = (from p in db.submissionrequestTBs
                                              where p.rStatus == "Submitting" && p.Submittype == "InPatient" || p.Submittype == "OutPatient"
                                              orderby p.id descending
                                              select p).ToList();
            return data;
        }

        public List<submissionrequestTB> GetAllReqNew()
        {
            List<submissionrequestTB> data = (from p in db.submissionrequestTBs
                                              where p.rStatus == "Submitting"
                                              orderby p.id descending
                                              select p).ToList();
            return data;
        }

        public List<submissionrequestTB> GetAllReqNewbyDE(string user)
        {
            List<submissionrequestTB> data = (from p in db.submissionrequestTBs
                                              where p.rStatus == "Submitting" && p.rFrom==user
                                              orderby p.id descending
                                              select p).ToList();
            return data;
        }

        public List<submissionrequestTB> TPASUb()
        {
            List<submissionrequestTB> data = (from p in db.submissionrequestTBs
                                              where p.rStatus == "PendingTPA" &&  
                                              p.Submittype == "OutPatient"
                                              orderby p.id descending
                                              select p).ToList();
            return data;
        }

        public List<submissionrequestTB> GetAllReqConf()
        {
            List<submissionrequestTB> data = (from p in db.submissionrequestTBs
                                              where p.rStatus == "Closing"
                                              orderby p.id descending
                                              select p).ToList();
            return data;
        }

        public List<submissionrequestTB> GetAllReqConf_in()
        {
            List<submissionrequestTB> data = (from p in db.submissionrequestTBs
                                              where p.rStatus == "Closing" &&( p.Submittype=="InPatient" || p.Submittype==null)
                                              orderby p.id descending
                                              select p).ToList();
            return data;
        }
        public List<submissionrequestTB> GetAllReqConf_out()
        {
            List<submissionrequestTB> data = (from p in db.submissionrequestTBs
                                              where p.rStatus == "Closing" && p.Submittype == "OutPatient"
                                              orderby p.id descending
                                              select p).ToList();
            return data;
        }

        public List<submissionrequestTB> GetAllReqConfByDE(string user)
        {
            List<submissionrequestTB> data = (from p in db.submissionrequestTBs
                                              where p.rStatus == "Closing" && p.rFrom==user
                                              orderby p.id descending
                                              select p).ToList();
            return data;
        }

        public List<submissionrequestTB> TPAOut()
        {
            List<submissionrequestTB> data = (from p in db.submissionrequestTBs
                                              where p.rStatus == "OutPatient"
                                              orderby p.id descending
                                              select p).ToList();
            return data;
        }


        public List<submissionrequestTB> GetAllReqPendingByDE(string user)
        {
            List<submissionrequestTB> data = (from p in db.submissionrequestTBs
                                              where p.rStatus == "Submitted" && p.rFrom == user
                                              orderby p.id descending
                                              select p).ToList();
            return data;
        }
        

        public List<submissionrequestTB> GetAllReqPending_ForArchiving()
        {
            List<submissionrequestTB> data = (from p in db.submissionrequestTBs
                                              where p.Submittype=="InPatient" || p.Submittype==null
                                              where p.rStatus == "Submitted" 
                                              orderby p.id descending
                                              select p).ToList();
            return data;
        }
        public List<submissionrequestTB> GetAllReqPending_ForEnterprisAdmin()
        {
            List<submissionrequestTB> data = (from p in db.submissionrequestTBs
                                              where p.rStatus == "Submitted"
                                              orderby p.id descending
                                              select p).ToList();
            return data;
        }
        public List<submissionrequestTB> GetAllReqPending_ForTPA()
        {
            List<submissionrequestTB> data = (from p in db.submissionrequestTBs
                                              where p.Submittype == "OutPatient"
                                              where p.rStatus == "Submitted"
                                              orderby p.id descending
                                              select p).ToList();
            return data;
        }
       

        public List<submissionrequestTB> GetAllReqClosed_in()
        {
            List<submissionrequestTB> data = (from p in db.submissionrequestTBs
                                              where p.rStatus == "Closed" && (p.Submittype=="InPatient" || p.Submittype==null)
                                              orderby p.id descending
                                              select p).ToList();
            return data;
        }

        public List<submissionrequestTB> GetAllReqClosed_out()
        {
            List<submissionrequestTB> data = (from p in db.submissionrequestTBs
                                              where p.rStatus == "Closed" && p.Submittype == "OutPatient"
                                              orderby p.id descending
                                              select p).ToList();
            return data;
        }

        public List<submissionrequestTB> GetAllReqClosed()
        {
            List<submissionrequestTB> data = (from p in db.submissionrequestTBs
                                              where p.rStatus == "Closed"
                                              orderby p.id descending
                                              select p).ToList();
            return data;
        }

        
        
        public List<submissionrequestTB> GetAllReqRejected_in()
        {
            List<submissionrequestTB> data = (from p in db.submissionrequestTBs
                                              where p.rStatus == "Rejected" && (p.Submittype=="InPatient"|| p.Submittype==null) 
                                              orderby p.id descending
                                              select p).ToList();
            return data;
        }

        public List<submissionrequestTB> GetAllReqRejected()
        {
            List<submissionrequestTB> data = (from p in db.submissionrequestTBs
                                              where p.rStatus == "Rejected"
                                              orderby p.id descending
                                              select p).ToList();
            return data;
        }


        public List<submissionrequestTB> GetAllReqRejected_out()
        {
            List<submissionrequestTB> data = (from p in db.submissionrequestTBs
                                              where p.rStatus == "Rejected" && (p.Submittype == "OutPatient" || p.Submittype == null)
                                              orderby p.id descending
                                              select p).ToList();
            return data;
        }

        public List<submissionrequestTB> GetAllReqClosedByDataEntry(string user)
        {
            List<submissionrequestTB> data = (from p in db.submissionrequestTBs
                                              where p.rStatus == "Closed"
                                              && p.rFrom==user
                                              orderby p.id descending
                                              select p).ToList();
            return data;
        }
        public List<submissionrequestTB> GetAllReqRejectedByDataEntry(string user)
        {
            List<submissionrequestTB> data = (from p in db.submissionrequestTBs
                                              where p.rStatus == "Rejected"
                                              && p.rFrom == user
                                              orderby p.id descending
                                              select p).ToList();
            return data;
        }

        public List<submissionrequestTB> GetAllReqRec()
        {
            List<submissionrequestTB> data = (from p in db.submissionrequestTBs
                                              where p.rStatus == "Receiving"
                                              orderby p.id descending
                                              select p).ToList();
            return data;
        }

        public List<submissionrequestTB> GetAllReqRec_out_TPAAdmin()
        {
            List<submissionrequestTB> data = (from p in db.submissionrequestTBs
                                              where p.rStatus == "Receiving" && p.Submittype=="OutPatient"
                                              orderby p.id descending
                                              select p).ToList();
            return data;
        }


        public List<submissionrequestTB> GetAllReqRecforarchiving(string user)
        {
            List<submissionrequestTB> data = (from p in db.submissionrequestTBs
                                              where p.rStatus == "Receiving" && p.AssigenPerson==user
                                              orderby p.id descending
                                              select p).ToList();
            return data;
        }
        public List<submissionrequestTB> Getsubrecforarchiving(string user)
        {
            List<submissionrequestTB> data = (from p in db.submissionrequestTBs
                                              where p.rStatus == "Receiving" && p.AssigenPerson == user
                                              orderby p.id descending
                                              select p).ToList();
            return data;
        }




        public List<submissionrequestTB> GetAllReqRecByDE(string user)
        {
            List<submissionrequestTB> data = (from p in db.submissionrequestTBs
                                              where p.rStatus == "Receiving" && p.rFrom==user
                                              orderby p.id descending
                                              select p).ToList();
            return data;
        }

        public void updateAssignPerson(int id, string user)
        {
            submissionrequestTB acc = db.submissionrequestTBs.Single(p => p.id == id);
            acc.AssigenPerson = user;
            acc.Assigned = "true";
            acc.rStatus = "Receiving";
            acc.rStatusColor = "#ffc052";
            acc.AssignedTime = DateTime.Now;
            db.SubmitChanges();
        }

        public void updateRembConfirm(int id)
        {
            submissionrequestTB acc = db.submissionrequestTBs.Single(p => p.id == id);
            acc.confirmed = "true";
            acc.rStatus = "Closing";
            acc.ConfirmTime = DateTime.Now;
            acc.rStatusColor = "#0ea3dc";
            db.SubmitChanges();
        }
        public void updaterejected(int id,string comment,string TPAcomment)
        {
            submissionrequestTB acc = db.submissionrequestTBs.Single(p => p.id == id);
            acc.confirmed = "false";
            acc.rStatus = "Rejected";
            acc.ArchivingComment = comment;
            acc.TPAcomment = TPAcomment;
            acc.FinishedArchivingDate = DateTime.Now;
            acc.rStatusColor = "#850606";
            db.SubmitChanges();
        }

        public void updateClosed(int id, string user)
        {
           
            submissionrequestTB acc = db.submissionrequestTBs.Single(p => p.id == id);     
                acc.confirmed = "true";
                acc.rStatus = "Closed";
            acc.rStatusColor = "#e15554";
            acc.ApprovedDate = DateTime.Now;
            acc.rApproved = "true";
           
            acc.ClosedBy = user;
            db.SubmitChanges();
               
            }

        public void updateClosedTPA(int id, string user,string comment, string TPA)
        {
            
                submissionrequestTB acc = db.submissionrequestTBs.Single(p => p.id == id);
                acc.confirmed = "true";
                acc.rStatus = "PendingTPA";
                acc.rStatusColor = "#0ea3dc";
                acc.ApprovedDate = DateTime.Now;
                acc.rApproved = "true";
                acc.ArchivingComment = comment;
                acc.ClosedBy = user;
                acc.TPAcomment = TPA;
                db.SubmitChanges();
           

        }

        public void updateClosedTPANew(int id, string user, string comment, string TPA)
        {

            submissionrequestTB acc = db.submissionrequestTBs.Single(p => p.id == id);
            acc.confirmed = "true";
            acc.rStatus = "PendingTPA";
            acc.rStatusColor = "#0ea3dc";
            acc.ApprovedDate = DateTime.Now;
            acc.rApproved = "true";
            acc.ArchivingComment = comment;
            acc.ClosedBy = user;
            acc.PendingTPAByTime = DateTime.Now;
            acc.PendingTPABy = user;
            acc.TPAcomment = TPA;
            db.SubmitChanges();


        }
        

        public void updaterecived(int id,int numberofrecbat,int numberofrecclaims,string arccomment,string TPAcomment)
        {
            submissionrequestTB acc = db.submissionrequestTBs.Single(p => p.id == id);
            acc.rStatus = "Submitted";
            acc.rStatusColor = "#ffc052";
            acc.NumberSentOfRecBatches = numberofrecbat;
            acc.NumberOfRecClaims = numberofrecclaims;
            acc.ArchivingComment = arccomment;
            acc.TPAcomment = TPAcomment;
            acc.FinishedArchivingDate = DateTime.Now;
            db.SubmitChanges();
        }

        public void updateAssignPersonforAdmin(int id, string user)
        {
            submissionrequestTB acc = db.submissionrequestTBs.Single(p => p.id == id);
            if (user == "----")
            {

            }
            else if (acc.rStatus == "Submitting")
            {
                acc.AssigenPerson = user;
                acc.Assigned = "true";
                acc.rStatus = "Receiving";
                acc.rStatusColor = "#ffc052";
                acc.AssignedTime = DateTime.Now;
            }
            else
            {
                acc.AssigenPerson = user;

            }


            db.SubmitChanges();
        }


        public List<submissionrequestTB> GetAllByACC(string user)
        {
            List<submissionrequestTB> data = (from p in db.submissionrequestTBs
                                    where p.rFrom == user
                                    orderby p.id ascending
                                    select p).ToList();
            return data;
        }
        public IEnumerable<submissionrequestTB> GetDataTPA()
        {
            List<submissionrequestTB> data = (from p in db.submissionrequestTBs
                                              where p.Submittype == "OutPatient"
                                    orderby p.id ascending
                                    select p).ToList();
            return data;
        }

        public IEnumerable<submissionrequestTB> EnterpriceAdmin()
        {
            List<submissionrequestTB> data = (from p in db.submissionrequestTBs
                                              
                                              orderby p.id ascending
                                              select p).ToList();
            return data;
        }
        public IEnumerable<submissionrequestTB> GetDataArchiving()
        {
            List<submissionrequestTB> data = (from p in db.submissionrequestTBs
                                              where p.Submittype=="InPatient" || p.Submittype== "OutPatient" || p.Submittype==null
                                              orderby p.id ascending
                                              select p).ToList();
            return data;
        }
        public List<submissionrequestTB> GetAllSearchBID(string word)
        {
            List<submissionrequestTB> data = (from p in db.submissionrequestTBs
                                    where p.id == int.Parse(word)
                                    orderby p.id ascending
                                    select p).ToList();
            return data;
        }

        public List<submissionrequestTB> GetAllSearchBySubject(string word)
        {
            List<submissionrequestTB> data = (from p in db.submissionrequestTBs
                                              where p.rBody.Contains(word)
                                              orderby p.id ascending
                                              select p).ToList();
            return data;
        }

        public List<submissionrequestTB> GetAllSearchBySub(string word)
        {
            List<submissionrequestTB> data = (from p in db.submissionrequestTBs
                                              where p.rSubject.Contains(word)
                                              orderby p.id ascending
                                              select p).ToList();
            return data;
        }

        public List<submissionrequestTB> GetAllSearchByCreator(string word)
        {
            List<submissionrequestTB> data = (from p in db.submissionrequestTBs
                                              where p.rFrom.Contains(word)
                                              orderby p.id ascending
                                              select p).ToList();
            return data;
        }

        public List<submissionrequestTB> GetAllSearchByAssign(string word)
        {
            List<submissionrequestTB> data = (from p in db.submissionrequestTBs
                                              where p.AssigenPerson.Contains(word)
                                              orderby p.id ascending
                                              select p).ToList();
            return data;
        }

        public List<submissionrequestTB> GetAllSearchByArchivingcomment(string word)
        {
            List<submissionrequestTB> data = (from p in db.submissionrequestTBs
                                              where p.ArchivingComment.Contains(word)
                                              orderby p.id ascending
                                              select p).ToList();
            return data;
        }

        public string getRequestOwner(int reqID)
        {
            var data = (from p in db.submissionrequestTBs
                        where p.id == reqID
                        select p.rFrom).FirstOrDefault();

            return data;
        }

       


    }
}