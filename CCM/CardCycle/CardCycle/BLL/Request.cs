using CardCycle.DAL;
using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;


namespace CardCycle.BLL
{
    public class Request
    {
        DataContextDataContext db = new DataContextDataContext();
        
        public void addrequestToIssuance(string head,string body,string src,string Fuser,string tpe,string clientname,string NumOfCards , string AccSrc )
        {
            requestTB req = new requestTB();
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            req.rSubject = head;
            req.rBody = body;
            req.rdate = DateTime.Now.ToString();
            req.rFrom = Fuser;
            req.ClientName = clientname;
            req.ToIssunge = "true";
            req.ToPrint = "false";
            req.ToQC = "false";
            req.apvIssuance = "Null";
            req.apvPrint = "Null";
            req.apvQControl = "Null";
            req.isApproved = "Null";
            req.States = "Issuing";
            req.Assigned = "false";
            req.color = "#68c39f";
            req.confrimAM = "false";
            req.ConfrimUW = "false";
            req.rAttach = src;
            req.rAccAttach = AccSrc;
            req.rType = tpe;
            req.CardsNum = int.Parse(NumOfCards);
            db.requestTBs.InsertOnSubmit(req);
            db.SubmitChanges();
        }
        public void addrequestToException(string head, string body, string src, string Fuser, string tpe, string clientname, string NumOfCards , string ExcepComm , string AccSrc)
        {
            requestTB req = new requestTB();
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            req.rSubject = head;
            req.rBody = body;
            req.rdate = DateTime.Now.ToString();
            req.rFrom = Fuser;
            req.ClientName = clientname;
            req.ToIssunge = "true";
            req.ToPrint = "false";
            req.ToQC = "false";
            req.apvIssuance = "Null";
            req.apvPrint = "Null";
            req.apvQControl = "Null";
            req.isApproved = "Null";
            req.States = "Exception Pending";
            req.AccExcepApp = "false";
            req.ISSExcepApp = "false";
            req.Exceptions = ExcepComm;
            req.Assigned = "false";
            req.color = "#0090ff";
            req.confrimAM = "false";
            req.ConfrimUW = "false";
            req.rAttach = src;
            req.rAccAttach = AccSrc;
            req.rType = tpe;
            req.CardsNum = int.Parse(NumOfCards);
            db.requestTBs.InsertOnSubmit(req);
            db.SubmitChanges();
        }

        public List<requestTB> GetAllReqNew()
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.ToIssunge == "true" && p.ToPrint == "false" && p.ToQC == "false" && p.States == "Issuing" && p.Assigned=="true" 
                                    orderby p.id descending
                       select p).ToList();
            return data;
        }
        public bool Isissuing(int id)
        {
            var data = (from p in db.requestTBs
                        where p.id == id
                        select p).FirstOrDefault();
            bool bol = data.States.Trim() == "Issuing".Trim();
            bool bol1 = data.Assigned == "true";
            if ( bol==true && bol1==true )
            {
                return true;
            }
            else
            { return false;}
          
        }
        public bool Check_status(int id)
        {
            var data = (from p in db.requestTBs
                        where p.id == id
                        select p).FirstOrDefault();
            bool bol = data.States.Trim() == "Issuing".Trim();
           
            if (bol == true)
            {
                return true;
            }
            else
            { return false; }

        }
        //added
        public List<requestTB> GetAllReqPenTech()
        {
            List<requestTB> data = (from p in db.requestTBs

                                    where p.ToIssunge == "true" && p.ToPrint == "false" && p.ToQC == "false" && p.States == "Pending Technical" && p.Assigned == "true"
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }

        public List<requestTB> GetAllReqExceptions()
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.ToIssunge == "true" && p.ToPrint == "false" && p.ToQC == "false" && p.States == "Exception Pending" && p.Assigned == "false"
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }

        public List<requestTB> GetAllReqNewNotAssigned()
        {
            List<requestTB> data = (from p in db.requestTBs

                                    where p.ToIssunge == "true" && p.ToPrint == "false" && p.ToQC == "false" && p.States == "Issuing" && p.Assigned == "false" 
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }
        public List<requestTB> GetAll()
        {
            List<requestTB> data = (from p in db.requestTBs
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }
        public List<requestTB> GetAllSearch(string word)
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rSubject.Contains(word) || p.rFrom.Contains(word) || p.ClientName.Contains(word) || p.rdate.Contains(word) || p.rAttach.Contains(word) || p.rBody.Contains(word)
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }
        public List<requestTB> GetAllSearchBID(string word)
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.id == int.Parse(word)
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }
        public List<requestTB> GetAllByACM(string user)
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rFrom == user
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }
        public List<requestTB> GetAllByACMSearch(string user,string word)
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rFrom == user &&( p.rSubject.Contains(word) || p.rFrom.Contains(word) )
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }
        public List<requestTB> GetAllByACMSearchBID(string user, string word)
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rFrom == user && (p.id==int.Parse(word))
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }
        public List<requestTB> GetConfirmedUnderwriting(string user)
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.ApvUnderWriting == user
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }
        public void UpdateIssing(int id, string IssunceName, string src, string IssComment, string CardsNum)
        {
       
            requestTB acc = db.requestTBs.Single(p => p.id == id);
            ClientTB cl = db.ClientTBs.Single(p => p.ClientName==acc.ClientName );
            acc.apvIssuance = IssunceName;
            acc.States = "pending confirmation";
            acc.color = "#0ea3dc";
            acc.rAttach = src;
            acc.rBody3 = IssComment;
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            acc.LogIssuance = DateTime.Now.ToString();
            acc.EndAsigntime = DateTime.Now.ToString("HH:mm");
            acc.EndAsignDate = DateTime.Now.ToString("dd/MM/yyyy");
            acc.IssCardsNum = int.Parse(CardsNum);
            if (cl.Type=="TPA")
            {
                acc.ConfrimUW = "true";
                acc.ApvUnderWriting = "Admin";
            }
            //acc.ToPrint = "true";
            db.SubmitChanges();
        }
        ////////////////////////// Cmplain Area ///////////////////////////////////
        public void AddAccountComplain(int id, string user, string ComplainComments)
        {
            requestTB acc = db.requestTBs.Single(p => p.id == id);
            acc.States = "IssuanceComplain";
            acc.color = "#ffc052";
            acc.CompAction = "true";
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            acc.ComplainComm = acc.ComplainComm + Environment.NewLine + user + " Said : " + " at      " + DateTime.Now.ToString() + Environment.NewLine + "         " + ComplainComments + Environment.NewLine;
            db.SubmitChanges();

        }
        public void AddIssuanceComplain(int id, string user, string ComplainComments)
        {
            requestTB acc = db.requestTBs.Single(p => p.id == id);
            acc.States = "AccountComplain";
            acc.color = "#ffc052";
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            acc.ComplainComm = acc.ComplainComm + Environment.NewLine + user + " Said : " + " at      " + DateTime.Now.ToString() + Environment.NewLine + "         " + ComplainComments + Environment.NewLine;
            db.SubmitChanges();

        }
        public void CloseComplain(int id, string user)
        {
            requestTB acc = db.requestTBs.Single(p => p.id == id);
            acc.States = "Closed";
            acc.color = "#e15554";
            acc.CompAction = "ReComp";
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            db.SubmitChanges();

        }

        //////////////////////////////////////////////////////////////////////////

        //added ////////////////////////////////////////////////////////////////////
        public void UpdateRequest(int id, string accountcomments, string pricingcomments)
        {

            requestTB acc = db.requestTBs.Single(p => p.id == id);
            acc.States = "Issuing";
            acc.color = "#ffc052";
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            acc.AComments = accountcomments;
            acc.PComments = pricingcomments;
           // db.requestTBs.InsertOnSubmit(acc);
            db.SubmitChanges();

        }

        public void UpdatePTech(int id, string IssunceName, string src, string IssComment)
        {

            requestTB acc = db.requestTBs.Single(p => p.id == id);
            acc.apvIssuance = IssunceName;
            acc.States = "Pending Technical";
            acc.color = "#0ea3dc";
            acc.rAttach = src;
            acc.rBody3 = IssComment;
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            acc.LogIssuance = DateTime.Now.ToString();
            acc.EndAsigntime = DateTime.Now.ToString("HH:mm");
            acc.EndAsignDate = DateTime.Now.ToString("dd/MM/yyyy");
            //acc.ToPrint = "true";
            db.SubmitChanges();



        }
        public void UpdatePTech2(int id, string IssunceName, string IssComment)
        {

            requestTB acc = db.requestTBs.Single(p => p.id == id);
            acc.apvIssuance = IssunceName;
            acc.States = "Pending Technical";
            acc.color = "#0ea3dc";
            acc.rBody3 = IssComment;
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            acc.LogIssuance = DateTime.Now.ToString();
            acc.EndAsigntime = DateTime.Now.ToString("HH:mm");
            acc.EndAsignDate = DateTime.Now.ToString("dd/MM/yyyy");

            //acc.ToPrint = "true";
            db.SubmitChanges();



        }

        ///////////////////////////////////////////////////////////////////////
        public void UpdateCancelIssuning(int id, string IssunceName, string IssComment, string CardsNum)
        {
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            requestTB acc = db.requestTBs.Single(p => p.id == id);
            acc.apvIssuance = IssunceName;
            acc.States = "Closed";
            acc.color = "#e15554";
            acc.isApproved = DateTime.Now.ToString();
            //acc.rAttach = src;
            acc.rBody3 = IssComment;
            acc.LogIssuance = DateTime.Now.ToString();
            acc.EndAsigntime = DateTime.Now.ToString("HH:mm");
            acc.EndAsignDate = DateTime.Now.ToString("dd/MM/yyyy");
            acc.IssCardsNum = int.Parse(CardsNum);
            //acc.ToPrint = "true";
            db.SubmitChanges();
        }

        public void UpdateCancel2Issuning(int id, string IssunceName,string src, string IssComment, string CardsNum)
        {
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            requestTB acc = db.requestTBs.Single(p => p.id == id);
            acc.apvIssuance = IssunceName;
            acc.States = "Closed";
            acc.color = "#e15554";
            acc.isApproved = DateTime.Now.ToString();
            acc.rAttach = src;
            acc.rBody3 = IssComment;
            acc.LogIssuance = DateTime.Now.ToString();
            acc.EndAsigntime = DateTime.Now.ToString("HH:mm");
            acc.EndAsignDate = DateTime.Now.ToString("dd/MM/yyyy");
            acc.IssCardsNum = int.Parse(CardsNum);
            //acc.ToPrint = "true";
            db.SubmitChanges();
        }

        public void UpdateIssing2(int id, string IssunceName, string IssComment, string CardsNum)
        {

            requestTB acc = db.requestTBs.Single(p => p.id == id);
            ClientTB cl = db.ClientTBs.Single(p => p.ClientName == acc.ClientName);
            acc.apvIssuance = IssunceName;
            acc.States = "pending confirmation";
            acc.color = "#0ea3dc";
            acc.rBody3 = IssComment;
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            acc.LogIssuance = DateTime.Now.ToString();
            acc.EndAsigntime = DateTime.Now.ToString("HH:mm");
            acc.EndAsignDate = DateTime.Now.ToString("dd/MM/yyyy");
            acc.IssCardsNum = int.Parse(CardsNum);
            if (cl.Type == "TPA")
            {
                acc.ConfrimUW = "true";
                acc.ApvUnderWriting = "Underwriting.admin";
            }
            //acc.ToPrint = "true";
            db.SubmitChanges();



        }
        
        /////////////////////////nnnnnnnnnnnnn///////////////////////////////////////
        public void UpdateACM(int id)
        {
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            requestTB acc = db.requestTBs.Single(p => p.id == id);
            acc.confrimAM = "true";
            acc.LogAM = DateTime.Now.ToString();
            db.SubmitChanges();
        }
        public void  GetNumberOfBooklet(int id, ref string NumBk)
        {
            var data = (from p in db.requestTBs
                       where p.id == id
                       select p).FirstOrDefault();

            NumBk = data.NumOfBooklets;
           
        }
        public void AddNumberOfBooklet(int id,string NumBk)
        {
            var data = (from p in db.requestTBs
                        where p.id == id
                        select p).FirstOrDefault();

           data.NumOfBooklets=NumBk;
            db.SubmitChanges();

        }
        public void GetDetailByid(int id, ref string sub, ref string body, ref string src, ref string attach, ref string AccountM, ref string IssComment, ref string AMconfirm, ref string UWconfirm, ref string states, ref string CAsign, ref string AsignP, ref string AsignQC, ref string closeNotes, ref string clientname, ref string accountcomments, ref string pricingcomments, ref string NumberOfCards, ref string IssNumberOfCards, ref string QualityControlCom, ref string ExceptionsCom, ref string AMExceptionApv, ref string UWExceptionApv, ref string Accsrc, ref string rCol, ref string CompComments, ref string CompAct)
        {
            var data = from p in db.requestTBs
                       where p.id == id

                       select p;
            CAsign = data.First().AssignPerson;
            sub = data.First().rSubject;
            body = data.First().rBody;
            src = data.First().rAttach;
            attach = data.First().rAttach;
            AccountM = data.First().rFrom;
            IssComment = data.First().rBody3;
            AMconfirm = data.First().confrimAM;
            UWconfirm = data.First().ConfrimUW;
            AsignP = data.First().AssignPrint;
            AsignQC = data.First().AssignQC;
            states = data.First().rType;
            clientname = data.First().ClientName;
            closeNotes = data.First().closeNotes;
            accountcomments = data.First().AComments;
            pricingcomments = data.First().PComments;
            QualityControlCom = data.First().QComments;
            NumberOfCards = data.First().CardsNum.ToString();
            IssNumberOfCards = data.First().IssCardsNum.ToString();
            ExceptionsCom = data.First().Exceptions;
            UWExceptionApv = data.First().ISSExcepApp;
            AMExceptionApv = data.First().AccExcepApp;
            Accsrc = data.First().rAccAttach;
            rCol = data.First().color;
            CompComments=data.First().ComplainComm;
            CompAct = data.First().CompAction;
        }
        public void GetConfirmByid(int id, ref string AMconfirm, ref string UWconfirm)
        {
            var data = from p in db.requestTBs
                       where p.id == id

                       select p;
            //sub = data.First().rSubject;
            //body = data.First().rBody;
            //src = data.First().rAttach;
            //attach = data.First().rAttach;
            //AccountM = data.First().rFrom;
            //IssComment = data.First().rBody3;
            AMconfirm = data.First().confrimAM;
            UWconfirm = data.First().ConfrimUW;

        }
        public void UpdateConfirmTWO(int id, string Name)
        {

            requestTB acc = db.requestTBs.Single(p => p.id == id);
            //    acc.ApvUnderWriting = Name;
            acc.States = "Printing";
            acc.color = "#ffc052";

            // acc.rAttach = src;
            // acc.rBody3 = IssComment;
            //acc.ToPrint = "true";
            db.SubmitChanges();



        }
        public void UpdateUWM(int id, string user)
        {
            requestTB acc = db.requestTBs.Single(p => p.id == id);
            acc.ConfrimUW = "true";
            acc.ApvUnderWriting = user;
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            acc.LogUW = DateTime.Now.ToString();
            db.SubmitChanges();
        }

                        /////////////////////////////////////////////////////////////
        public void UpdateExcACM(int id , string user)
        {
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            requestTB acc = db.requestTBs.Single(p => p.id == id);
            acc.AccExcepApp = "true";
            acc.AccExcepAppUser = user;
            acc.AccExcepAppDate = DateTime.Now.ToString();
            db.SubmitChanges();
        }
        public void GetConfirmExcByid(int id, ref string AMExceptionConfirm, ref string IssExceptionConfirm)
        {
            var data = from p in db.requestTBs
                       where p.id == id
                       select p;

            AMExceptionConfirm = data.First().AccExcepApp;
            IssExceptionConfirm = data.First().ISSExcepApp;

        }
        public void UpdateExcConfirmTWO(int id, string Name)
        {
            requestTB acc = db.requestTBs.Single(p => p.id == id);
            acc.States = "Issuing";
            acc.color = "#68c39f";
            db.SubmitChanges();
        }
        public void UpdateExcUWM(int id, string user)
        {
            requestTB acc = db.requestTBs.Single(p => p.id == id);
            acc.ISSExcepApp = "true";
            acc.ISSExcepAppUser = user;
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            acc.ISSExcepAppDate = DateTime.Now.ToString();
            db.SubmitChanges();
        }

        /////////////////////////nnnnnnnnnnnnn///////////////////////////////////////

        
        
        public void UpdateIssingNoTAPV(int id, string IssunceName, string src, string IssComment, string CardsNum)
        {

            requestTB acc = db.requestTBs.Single(p => p.id == id);
            acc.apvIssuance = IssunceName;
            acc.States = "Printing";
            acc.color = "#ffc052";
            acc.rAttach = src;
            acc.rBody3 = IssComment;
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            acc.LogIssuance = DateTime.Now.ToString();
            acc.EndAsigntime = DateTime.Now.ToString("HH:mm");
            acc.EndAsignDate = DateTime.Now.ToString("dd/MM/yyyy");
            acc.IssCardsNum = int.Parse(CardsNum);
            //acc.ToPrint = "true";
            db.SubmitChanges();



        }
        public void UpdateIssingClose(int id, string IssunceName, string src, string IssComment, string CardsNum)
        {

            requestTB acc = db.requestTBs.Single(p => p.id == id);
            acc.apvIssuance = IssunceName;
            acc.States = "Closed";
            acc.color = "#e15554";
            acc.rAttach = src;
            acc.rBody3 = IssComment;
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            acc.LogIssuance = DateTime.Now.ToString();
            acc.IssCardsNum = int.Parse(CardsNum);
            //acc.ToPrint = "true";
            db.SubmitChanges();



        }
        public void UpdateIssing2NOTAPV(int id, string IssunceName, string IssComment, string CardsNum)
        {

            requestTB acc = db.requestTBs.Single(p => p.id == id);
            acc.apvIssuance = IssunceName;
            acc.States = "Printing";
            acc.color = "#ffc052";
            acc.rBody3 = IssComment;
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            acc.LogIssuance = DateTime.Now.ToString();
            acc.EndAsigntime = DateTime.Now.ToString("HH:mm");
            acc.EndAsignDate = DateTime.Now.ToString("dd/MM/yyyy");
            acc.IssCardsNum = int.Parse(CardsNum);
            //acc.ToPrint = "true";
            db.SubmitChanges();



        }
        public void UpdateIssing2Close(int id, string IssunceName, string IssComment)
        {

            requestTB acc = db.requestTBs.Single(p => p.id == id);
            acc.apvIssuance = IssunceName;
            acc.States = "Closed";
            acc.color = "#e15554";
            acc.rBody3 = IssComment;
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            acc.LogIssuance = DateTime.Now.ToString();
            //acc.ToPrint = "true";
            db.SubmitChanges();



        }
        public void Updateprinting(int id, string IssunceName)
        {
            requestTB acc = db.requestTBs.Single(p => p.id == id);
            acc.apvPrint =  IssunceName;
            acc.States = "Quality Control";
            acc.color = "#ffc052";
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            acc.LogPrint = DateTime.Now.ToString();
            acc.EndPrintTime = DateTime.Now.ToString("HH:mm");
            acc.EPDate = DateTime.Now.ToString("dd/MM/yyyy");
            //acc.ToPrint = "true";
            db.SubmitChanges();
        }
        public void UpdateQC(int id, string IssunceName, string CardsNum, string QCComments)
        {

            requestTB acc = db.requestTBs.Single(p => p.id == id);
            acc.apvQControl = IssunceName;
            acc.States = "Closing";
            acc.color = "#65bbd6";
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            acc.LogQC = DateTime.Now.ToString();
            acc.EndAsingQTime = DateTime.Now.ToString("HH:mm");
            acc.EQCDate = DateTime.Now.ToString("dd/MM/yyyy");
            acc.IssCardsNum = int.Parse(CardsNum);
            acc.QComments = QCComments;
            //acc.ToPrint = "true";
            db.SubmitChanges();



        }
        public void UpdateClose(int id, string IssunceName,string closeNotes)
        {
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            requestTB acc = db.requestTBs.Single(p => p.id == id);
            acc.isApproved = DateTime.Now.ToString();
            acc.States = "Closed";
            acc.color = "#e15554";
            acc.closeNotes = closeNotes;
          //  acc.ap
            acc.aproveBy = IssunceName;
            //acc.ToPrint = "true";
            db.SubmitChanges();



        }
        public List<requestTB> GetAllReqPending()
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.States == "Printing"
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }
        public List<requestTB> GetAllReqPConfirm()
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.States == "pending confirmation" 
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }
        public List<requestTB> GetAllReqPConfirmPricing()
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.States == "pending confirmation"  && (p.rType == "Renewal" || p.rType =="New Company") && p.ConfrimUW !="true"
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }
        public List<requestTB> GetAllReqPConfirmBYACManger(string user)
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.States == "pending confirmation"  && p.rFrom==user
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }
        public List<requestTB> GetAllReqQualityC()
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.States == "Quality Control"
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }
        public List<requestTB> GetAllReqClosing()
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.States == "Closing"
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }
        public List<requestTB> GetAllReqClosingBYACM(string user )
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.States == "Closing" && p.rFrom ==user
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }
        public int New_req()
        {
            var data = (from p in db.requestTBs
                        where p.States == "Issuing"
                        select p).Count();
            int d = (int)data;
            return d;
        }
        public int total_req()
        {
            var data = (from p in db.requestTBs
                       
                        select p).Count();
            int d = (int)data;
            return d;
        }
        public int pending_req()
        {
            var data = (from p in db.requestTBs
                        where p.States == "Closing" || p.States == "Printing" || p.States == "Quality Control"
                        select p).Count();
            int d = (int)data;
            return d;
        }
        public int close_req()
        {
            var data = (from p in db.requestTBs
                        where p.States == "Closed"
                        select p).Count();
            int d = (int)data;
            return d;
        }
        public List<requestTB> GetAllReqClosed()
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.States == "Closed"
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }

        public List<requestTB> GetAllReqClosedBYACM(string user)
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.States == "Closed" && p.rFrom==user
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }
        public List<requestTB> GetAllReqComplainBYACM(string user)
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.States == "AccountComplain"  

                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }

        /// <summary>
        /// //
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// 

         public bool GetAllReqComplainAC(int id ,  string user)
            {
                var data = (from p in db.requestTBs
                        where p.States == "AccountComplain" && user ==p.rFrom && id == p.id


                        select p).ToList();
            if (data.Count == 1)
                return true;
            return false;
        }


        public List<requestTB> GetAllReqComplainBYISS(string user)
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.States == "IssuanceComplain" 

                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }
        /// <summary>
        /// ///////////////////////
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool GetAllReqComplainiss(int id , string user)
        {
            var data = (from p in db.requestTBs
                                    where p.States == "IssuanceComplain" && user == p.apvIssuance &&  id == p.id

                                   
                                    select p).ToList();
            if (data.Count == 1)
                return true;
            return false;
        }

        public void updateAssign(int id)
        {
            requestTB acc = db.requestTBs.Single(p => p.id == id);
            //acc.isApproved = DateTime.Now.ToString();
            //acc.States = "Closed";
            acc.Assigned = "true";
            acc.color = "#ffc052";
            acc.asignTime = DateTime.Now.ToString("HH:mm");
            acc.AsignDate = DateTime.Now.ToString("dd/MM/yyyy");
            //acc.ToPrint = "true";
            db.SubmitChanges();
        }
        public void transfereTicket(int id)
        {
            requestTB acc = db.requestTBs.Single(p => p.id == id);
            //acc.isApproved = DateTime.Now.ToString();
            //acc.States = "Closed";
            acc.Assigned = "false";
            acc.color = "#ffffff";
            acc.asignTime = DateTime.Now.ToString("HH:mm");
            acc.AsignDate = DateTime.Now.ToString("dd/MM/yyyy");
            //acc.ToPrint = "true";
            db.SubmitChanges();
        }
        public void updateReject(int id, string mm , string source)
        {
            requestTB acc = db.requestTBs.Single(p => p.id == id);
            acc.States = "Rejected";
            acc.color = "#ffd966";          // change color
            acc.rBody3 = mm;
            acc.rAttach=source;
            db.SubmitChanges();
        }
        public void updateRejectSeen(int id)
        {
            requestTB acc = db.requestTBs.Single(p => p.id == id);
            //acc.isApproved = DateTime.Now.ToString();
            //acc.States = "Closed";
            //acc.States = "Rejected";
            acc.color = "#6698ff";          // change color
            //acc.rBody3 = mm;
            //acc.ToPrint = "true";
            db.SubmitChanges();
        }

        public void updateAssignPrint(int id,string user)
        {
            requestTB acc = db.requestTBs.Single(p => p.id == id);
            //acc.isApproved = DateTime.Now.ToString();
            //acc.States = "Closed";
            acc.AssignPrint = user;
            acc.AsignPrintTime = DateTime.Now.ToString("HH:mm");
            acc.APDate = DateTime.Now.ToString("dd/MM/yyyy");
            //acc.ToPrint = "true";
            db.SubmitChanges();
        }
        public void  changeStatus(int id, string Status)
        {
            requestTB acc = db.requestTBs.Single(p => p.id == id);
            acc.States = Status;
            db.SubmitChanges();
        }
        public void changeStatusNumOfCard(int id, string Status, int numCard)
        {
            requestTB acc = db.requestTBs.Single(p => p.id == id);
            acc.States = Status;
            acc.IssCardsNum = numCard;
            db.SubmitChanges();
        }
        public void updateIssuanceStatus(int id, string Status,string numofbooklet,int numCard)
        {
            requestTB acc = db.requestTBs.Single(p => p.id == id);
            acc.States = Status;
            acc.IssCardsNum = numCard;
            acc.NumOfBooklets = numofbooklet;
            db.SubmitChanges();
        }


        public void updateAssignPerson(int id,string user)
        {
            requestTB acc = db.requestTBs.Single(p => p.id == id);
            //acc.isApproved = DateTime.Now.ToString();
            //acc.States = "Closed";
            acc.AssignPerson= user;
            
            //acc.ToPrint = "true";
            db.SubmitChanges();
        }

        public void updateAssignQc(int id, string user)
        {
            requestTB re = db.requestTBs.Single(p => p.id == id);
            re.AssignQC = user;
            re.AsignQTime = DateTime.Now.ToString("HH:mm");
            re.AQCDate = DateTime.Now.ToString("dd/MM/yyyy");
            db.SubmitChanges();
        }
        public void updateAssignUnderWriting(int id, string user)
        {
            requestTB re = db.requestTBs.Single(p => p.id == id);
            re.AssignUnderWriting = user;
            db.SubmitChanges();
        }
        public List<requestTB> AllData()
        {
            List<requestTB> data = (from p in db.requestTBs                                 
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }
        public List<requestTB> GetAllRejectBYACM(string user)
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.States == "Rejected" && p.rFrom == user && p.color == "#ffd966"
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }

        public List<requestTB> GetAllRejectSeenBYACM(string user)
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.States == "Rejected" && p.rFrom == user && p.color == "#6698ff"
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }

        public List<requestTB> GetAllReject()
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.States == "Rejected" && p.color == "#ffd966"
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }
        public List<requestTB> GetAllRejectSeen()
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.States == "Rejected" && p.color == "#6698ff"
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }

        public List<requestTB> GETDAtaDeatailsSearch(int id)
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.id==id
                                    select p).ToList();
            return data;
        }
        // get latest id for tabel
        public int GetMaxID()
        {
            var data = db.requestTBs.Max(p => p.id);
            int id = int.Parse(data.ToString());
            return id;
        }
        // update v.1.3
        public int Tot_NewREQ()
        {
            var data = (from p in db.requestTBs
                        where p.States == "Issuing" && p.color == "#68c39f"
                        select p).Count();
            int d = (int)data;
            return d;
        }
        public int Tot_NewREQ_ac(string user)
        {
            var data = (from p in db.requestTBs
                        where p.States == "Issuing" && p.color == "#68c39f" && p.rFrom==user
                        select p).Count();
            int d = (int)data;
            return d;
        }
        public int Tot_REQ()
        {
            var data = (from p in db.requestTBs
                      
                        select p).Count();
            int d = (int)data;
            return d;
        }
        public int Tot_REQ_ac(string user)
        {
            var data = (from p in db.requestTBs
                        where p.rFrom==user
                        select p).Count();
            int d = (int)data;
            return d;
        }  
        public int Tot_IssREQ()
        {
            var data = (from p in db.requestTBs
                        where p.States == "Issuing" && p.color == "#ffc052"
                        select p).Count();
            int d = (int)data;
            return d;
        }
        public int Tot_PCREQ()
        {
            var data = (from p in db.requestTBs
                        where p.States == "pending confirmation"
                        select p).Count();
            int d = (int)data;
            return d;
        }
        public int Tot_PCREQ_ac(string user)
        {
            var data = (from p in db.requestTBs
                        where p.States == "pending confirmation" && p.rFrom== user
                        select p).Count();
            int d = (int)data;
            return d;
        }
        //added
        public int Tot_PTREQ()
        {
            var data = (from p in db.requestTBs
                        where p.States == "Pending Technical"
                        select p).Count();
            int d = (int)data;
            return d;
        }
        public int Tot_PTREQ_ac(string user)
        {
            var data = (from p in db.requestTBs
                        where p.States == "Pending Technical" && p.rFrom == user
                        select p).Count();
            int d = (int)data;
            return d;
        }
        public int Tot_ExcepREQ()
        {
            var data = (from p in db.requestTBs
                        where p.States == "Exception Pending"
                        select p).Count();
            int d = (int)data;
            return d;
        }

        //////////
        public int Tot_PrintREQ()
        {
            var data = (from p in db.requestTBs
                        where p.States == "Printing"
                        select p).Count();
            int d = (int)data;
            return d;
        }
        public int Tot_QCREQ()
        {
            var data = (from p in db.requestTBs
                        where p.States == "Quality Control"
                        select p).Count();
            int d = (int)data;
            return d;
        }
        public int Tot_PAREQ()
        {
            var data = (from p in db.requestTBs
                        where p.States == "Closing"
                        select p).Count();
            int d = (int)data;
            return d;
        }
        public int Tot_PAREQ_ac(string user)
        {
            var data = (from p in db.requestTBs
                        where p.States == "Closing" && p.rFrom==user
                        select p).Count();
            int d = (int)data;
            return d;
        }
        public int Tot_Con_UW(string user)
        {
            var data = (from p in db.requestTBs
                        where p.ApvUnderWriting == user
                        select p).Count();
            int d = (int)data;
            return d;
        }
        public int Tot_ClosedREQ()
        {
            var data = (from p in db.requestTBs
                        where p.States == "Closed"
                        select p).Count();
            int d = (int)data;
            return d;
        }
        public int Tot_ClosedREQ_ac(string user)
        {
            var data = (from p in db.requestTBs
                        where p.States == "Closed" && p.rFrom==user

                        select p).Count();
            int d = (int)data;
            return d;
        } 
       public int Tot_REGREQ()
        {
            var data = (from p in db.requestTBs
                        where p.States == "Rejected" && p.color == "#ffd966"
                        select p).Count();
            int d = (int)data;
            return d;
        }

       public int Tot_REGREQSeen()
       {
           var data = (from p in db.requestTBs
                       where p.States == "Rejected" && p.color == "#6698ff"
                       select p).Count();
           int d = (int)data;
           return d;
       }

       public int Tot_REGREQ_ac(string user)
       {
           var data = (from p in db.requestTBs
                       where p.States == "Rejected" && p.rFrom == user && p.color == "#ffd966"
                       select p).Count();
           int d = (int)data;
           return d;
       }
       public int Tot_REGREQSeen_ac(string user)
       {
           var data = (from p in db.requestTBs
                       where p.States == "Rejected" && p.rFrom == user && p.color == "#6698ff"
                       select p).Count();
           int d = (int)data;
           return d;
       }
       public int Tot_Complain_ac(string user)
       {
           var data = (from p in db.requestTBs
                       where p.States == "AccountComplain" 
                       select p).Count();
           int d = (int)data;
           return d;
       }

       public int Tot_Complain_Iss(string user)
       {
           var data = (from p in db.requestTBs
                       where p.States == "IssuanceComplain"
                       select p).Count();
           int d = (int)data;
           return d;
       }

       public List<requestTB> GetAllDaiyReport(string word,string user)
       {
           List<requestTB> data = (from p in db.requestTBs
                                   where p.rdate.Contains(word) && (p.apvIssuance==user || p.apvPrint==user || p.apvQControl == user)
                                   orderby p.id descending
                                   select p).ToList();
           return data;
       }
        /// <summary>
        /// /
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        //kerillos report 
        public List<requestTB> GetAllReports(DateTime date1 , DateTime date2 ,string stat ,  string user)
        {
            List<requestTB> data;
          
                if (stat == "ALL")
                {
                    data = (from p in db.requestTBs
                            where Convert.ToDateTime(p.rdate.ToString()) >= date1 && Convert.ToDateTime(p.rdate.ToString()) <= date2 && (p.apvIssuance == user || p.apvPrint == user || p.apvQControl == user)
                            orderby p.id descending
                            select p).ToList();
                    return data;
                }
                else
                {
                    data = (from p in db.requestTBs
                            where Convert.ToDateTime(p.rdate.ToString()) >= date1 && Convert.ToDateTime(p.rdate.ToString()) <= date2 && p.States == stat && (p.apvIssuance == user || p.apvPrint == user || p.apvQControl == user)
                            orderby p.id descending
                            select p).ToList();

                }

            

            return data;
        }

        public int CountAllIssuance(DateTime date1, DateTime date2,string stat , string user)
        {
            int d;
            if (stat =="ALL")
            {
                var data = (from p in db.requestTBs
                            where Convert.ToDateTime(p.rdate.ToString()) >= date1 && Convert.ToDateTime(p.rdate.ToString()) <= date2 && (p.apvIssuance == user || p.apvPrint == user || p.apvQControl == user)
                            orderby p.id descending
                            select p).Count();
                 d = (int)data;
                return d;
            }
           else
            {
                var data = (from p in db.requestTBs
                            where Convert.ToDateTime(p.rdate.ToString()) >= date1 && Convert.ToDateTime(p.rdate.ToString()) <= date2 && p.States == stat && (p.apvIssuance == user || p.apvPrint == user || p.apvQControl == user)
                            orderby p.id descending
                            select p).Count();
                d = (int)data;
                return d;
            }
        }
        ///
        /// Get all names
        /// 
        public List<requestTB> Get_Reports(DateTime date1, DateTime date2, string team)
        {
            List<requestTB> data = new List<requestTB>();




            if (team == "QulityControl")
            {
                var d = (from it in db.requestTBs
                         where Convert.ToDateTime(it.rdate.ToString()) >= date1 && Convert.ToDateTime(it.rdate.ToString()) <= date2
                         //  where it.id >= 33230 && it.apvQControl != "Null"
                         group it by it.apvQControl into newgroup

                         orderby newgroup.Key ascending
                         select newgroup);

              
                bool find = false;
                int numcard = 0;
                 int tickets = 0;
                string name = "";
                requestTB r;
                foreach (var iteam in d)
                {

                    r = new requestTB();
                    foreach (var g in iteam)
                    {
                        if (g.CardsNum != null && g.apvQControl != "Null")
                        {
                            numcard += (int)g.CardsNum;
                            // numbook += Convert.ToInt32(g.NumOfBooklets);
                            name = g.apvQControl;
                            find = true;
                            tickets++;
                        }



                    }
                    if (find == true)
                    {
                        r.CardsNum = numcard;
                        // r.NumOfBooklets = numbook.ToString();
                        r.apvQControl = name;
                        r.LogQC = tickets.ToString();

                        data.Add(r);
                        numcard = 0;
                        tickets = 0;

                    }



                }
                return data;
            }
            else if (team == "Issuance")
            {
                var d = (from it in db.requestTBs
                             //where Convert.ToDateTime(it.rdate).Day >= date1.Day && Convert.ToDateTime(it.rdate).Month >= date1.Month && Convert.ToDateTime(it.rdate).Day <= date2.Day && Convert.ToDateTime(it.rdate).Month <= date2.Month
                         where Convert.ToDateTime(it.rdate.ToString()) >= date1 && Convert.ToDateTime(it.rdate.ToString()) <= date2
                         where it.id >= 33230 && it.apvIssuance != "Null"
                         group it by it.apvIssuance into newgroup

                         orderby newgroup.Key ascending

                         select newgroup);

                int x = d.Count();
                bool find = false;
                int Issnumcard = 0;
                int tickets = 0;
                string name = "";
                requestTB r;
                foreach (var iteam in d)
                {

                    r = new requestTB();
                    foreach (var g in iteam)
                    {
                        if (g.IssCardsNum != null && g.apvIssuance != "Null")
                        {
                            Issnumcard += (int)g.IssCardsNum;
                            // numbook += Convert.ToInt32(g.NumOfBooklets);
                            name = g.apvIssuance;
                            find = true;
                            tickets++;
                        }



                    }
                    if (find == true)
                    {
                        r.IssCardsNum = Issnumcard;
                        // r.NumOfBooklets = numbook.ToString();
                        r.apvIssuance = name;
                        r.LogIssuance = tickets.ToString();

                        data.Add(r);
                        Issnumcard = 0;
                        tickets = 0;
                    }



                }
                return data;
            }
            return data;

        }

        public List<requestTB> Get_cards_Report(DateTime date1, DateTime date2)
        {
            List<requestTB> data = new List<requestTB>();


            var d = (from p in db.requestTBs
                     where Convert.ToDateTime(p.rdate.ToString()) >= date1 && Convert.ToDateTime(p.rdate.ToString()) <= date2
                     //where p.id > 33232
                     orderby p.id descending
                    select p).ToList();
            int cardsnum = 0;

                requestTB req = new requestTB();
         
                 foreach( var i in d)
                {
                    if(i.CardsNum != null )
                    cardsnum += Convert.ToInt16(i.CardsNum);
                }
                req.CardsNum = cardsnum;

                int isscardsnum = 0;

                foreach (var i in d)
                {
                    if (i.IssCardsNum != null)
                        isscardsnum += Convert.ToInt16(i.IssCardsNum);
                }


                req.IssCardsNum = isscardsnum;

                int Bookletsnum = 0;

                foreach (var i in d)
                {
                    if (i.NumOfBooklets != "NULL" && i.NumOfBooklets != "")
                        Bookletsnum += Convert.ToInt16(i.NumOfBooklets);
                }


                req.NumOfBooklets = Bookletsnum.ToString();
                data.Add(req);

                return data;
           



           
        }
        //data = (from p in db.requestTBs
        //        where Convert.ToDateTime(p.rdate.ToString()) >= date1 && Convert.ToDateTime(p.rdate.ToString()) <= date2
        //        orderby p.id descending
        //        select p).ToList();
        //return data;

        /*  List<string> qteam = new List<string>();

          foreach( var iteam in data )
          {
           qteam.Add(iteam.apvQControl.Distinct().ToString());
              iteam.CardsNum.Value.ToString();
          }

      }
      else if (team == "Issuance")
      {
          data = (from p in db.requestTBs
                  where Convert.ToDateTime(p.rdate.ToString()) >= date1 && Convert.ToDateTime(p.rdate.ToString()) <= date2

                  select p).ToList();*/








        /// <summary>
        /// //
        /// </summary>
        /// <param name="word"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public int CountIssuance(string word,string user)
       {
           var data = (from p in db.requestTBs
                       where p.rdate.Contains(word) && (p.apvIssuance == user || p.apvPrint == user || p.apvQControl == user)
                       select p).Count();
           int d = (int)data;
           return d;
       }
       public void GetTimeData(int id, ref string sub, ref string recived,ref string apvISS,ref string apvPrint,ref string APVQC, ref string at, ref string eat, ref string ad, ref string ead, ref string apt, ref string eapt, ref string apd, ref string eapd,ref string aqt,ref string eaqt,ref string aqd,ref string eaqd )
       {
           var data = from p in db.requestTBs
                      where p.id==id
                      select p;
          sub = data.First().rSubject;
          recived = data.First().rdate;
          apvISS = data.First().apvIssuance;
          apvPrint = data.First().apvPrint;
          APVQC = data.First().apvQControl;
          at = data.First().asignTime;
          eat = data.First().EndAsigntime;
          apt = data.First().AsignPrintTime;
          eapt = data.First().EndPrintTime;
          aqt = data.First().AsignQTime;
          eaqt = data.First().EndAsingQTime;
          ad = data.First().AsignDate;
          ead = data.First().EndAsignDate;
          apd = data.First().APDate;
          eapd = data.First().EPDate;
          apd = data.First().APDate;
          eapd = data.First().EPDate;
          aqd = data.First().AQCDate;
          eaqd = data.First().EQCDate;
       }
       public List<requestTB> GetAllRLostcard()
       {
          List<requestTB> lst = (from p in db.requestTBs
          where p.rType=="Lost Card" || p.rSubject.Contains("Lost Card")
          select p).ToList();
          return lst;

       }
       public int total_LostCard()
       {
           var data = (from p in db.requestTBs
                       where p.rType == "Lost Card" || p.rSubject.Contains("Lost Card")
                       select p).Count();
           int d = (int)data;
           return d;
       }

        public void SendEmailEpecialRequest()
        {
            ExchangeService service = new ExchangeService();
            service.AutodiscoverUrl("CCMReply@prime-health.org");
            service.UseDefaultCredentials = false;
            service.Credentials = new WebCredentials("CCMReply", "NoP@ssw0rd", "primehealth.local");
            EmailMessage message = new EmailMessage(service);
            message.Subject = "Special Request";
            message.Body = " Be careful there is a new request has been submitted now ";
            message.From = "CCMReply@Prime-Health.org";

           // message.ToRecipients.Add("Mohamed.AbdElsattar@Prime-Health.org");
            message.ToRecipients.Add("david.maher@prime-health.org");
            message.CcRecipients.Add("Ayman.Shaalan@Prime-Health.org");
            message.ToRecipients.Add("Ahmed.Abdellah@Prime-Health.org");
            message.Save();
            message.SendAndSaveCopy();
        }

        public void SendEmailBackupFail()
        {
            ExchangeService service = new ExchangeService();
            service.AutodiscoverUrl("CCMReply@prime-health.org");
            service.UseDefaultCredentials = false;
            service.Credentials = new WebCredentials("CCMReply", "NoP@ssw0rd", "primehealth.local");
            EmailMessage message = new EmailMessage(service);
            message.Subject = "Backup Failure";
            message.Body = " Be careful there is a problem in folder backup ";
            message.From = "CCMReply@Prime-Health.org";
            message.ToRecipients.Add("Ahmed.Abdellah@Prime-Health.org");
            //message.CcRecipients.Add("Mohamed.AbdElsattar@prime-health.org");
            //message.CcRecipients.Add("Mohamed.Maher@Prime-Health.org");
            //message.BccRecipients.Add("Mohamed.Soliman@Prime-Health.org");
            message.Save();
            message.SendAndSaveCopy();
        }

        public List<requestTB> daily_report(string  Start_Date,string End_Date,string  status)
        {

            // List<string> listofnulls = new List<string>();
            List<requestTB> data = new List<requestTB>();
            foreach (var itemDate in get_days_between_two_dates(Start_Date, End_Date))
            {

                data.AddRange(db.requestTBs
                   .Where((m) => m.isApproved.Contains(itemDate)
                   ||
             m.LogPrint.Contains(itemDate)
                   || m.LogIssuance.Contains(itemDate)
                  || m.LogQC.Contains(itemDate)).ToList()
                .Where(m => status.Contains(m.rType))
                  .Select(x => new requestTB
                  {
                      id = x.id,
                      ClientName = x.ClientName,
                      IssCardsNum = x.IssCardsNum,
                      rType = x.rType,
                      rdate = x.rdate,
                      isApproved = x.isApproved,
                      States = x.States
                  })

                   .OrderBy(x => x.id)
                   .ToList());



                foreach (var item in data)
                {
                    if (item.isApproved == "Null")
                        item.isApproved = "";

                }

            }







            return (data);
        }
        public List<string> get_days_between_two_dates(string start_date_str, string end_date_str)
        {
            DateTime start_date=   DateTime.ParseExact(start_date_str, "M/d/yyyy", CultureInfo.InvariantCulture);
            DateTime end_date = DateTime.ParseExact(end_date_str, "M/d/yyyy", CultureInfo.InvariantCulture);


            List<string> days_list = new List<string>();
            DateTime temp_start;
            DateTime temp_end;

            //--Normalize dates by getting rid of minues since they will get in the way when doing the loop
            temp_start = new DateTime(start_date.Year, start_date.Month, start_date.Day);
            temp_end = new DateTime(end_date.Year, end_date.Month, end_date.Day);

            //--Example Should return
            //--1-12-2014 5:59AM - 1-13-2014 6:01AM return 12 and 13
            for (DateTime date = temp_start; date <= temp_end; date = date.AddDays(1))
            {
                days_list.Add(date.Month+"/"+date.Day+"/"+date.Year);
            }

            return days_list;
        }
    }

}