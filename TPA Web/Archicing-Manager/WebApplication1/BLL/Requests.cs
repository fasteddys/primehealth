using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WebApplication1.DLL;
namespace WebApplication1.BLL
{
    public class Requests
    {
        TPASystemDataContext db = new TPASystemDataContext();
        TPASystemDataContext _db = new TPASystemDataContext();
       // TPASystemDataContext _db = new TPASystemDataContext();

        #region AddRequest

        public void addrequest(string ClientType , string CName , string month, string year,string providername, string src, string Fuser, string addedType, string assignperson)
        {

                        Provider pro = new Provider();
                        var data = (from p in db.Providers
                                    where (p.PType == ClientType && p.ClientName == CName && p.PMonth == month && p.PYear == year && p.PName == providername && p.Assigned == null)
                                    orderby p.PId ascending
                                   select p).ToList();

            foreach (var i in data)
            {
                requestTB req = new requestTB();
                Provider s = i;
                System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
                req.rBody = providername;
                req.rDate = DateTime.Now.ToString();
                req.rFrom = Fuser;
                req.rStatus = "New";
                req.rAddedByType = addedType;
                req.Assigned = "false";
                req.rStatusColor = "#68c39f";
                req.rApproved = "false";
                req.AssigenPerson = assignperson;
                req.ProviderID = s.PId;
                req.PolicyNum = s.PolicyNumber;
                s.Assigned = "true";
                req.rAttach = s.AttachPath + "\\" + s.PName + s.PolicyNumber +".xlsx";
                req.TottalClaims = s.TottalClaims;
                req.ClientName = s.ClientName;
                req.ClientType = s.PType;
                req.folderpath = ClientType + "/" + CName + "/" + year + "/" + month;
                //db.SubmitChanges();
                db.requestTBs.InsertOnSubmit(req);
                req.ReturnQcCount = 0;
                req.ReturnTPACount = 0;
            }
            db.SubmitChanges();

        }

        public void addrequestToArchPanel(string ClientName, string month, string year, string TottProv, string Creator, string TotExcel, string type, string attch, string NumClaims, string TotComm)
        {
                ArchPanel ArchReq = new ArchPanel();
                System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
                ArchReq.ClientName = ClientName;
                ArchReq.Creator = Creator;
                ArchReq.CreationTime = DateTime.Now.ToString();
                ArchReq.Month = month;
                ArchReq.Year = year;
                ArchReq.TottalProviders = int.Parse(TottProv);
                ArchReq.TottalExcel = int.Parse(TotExcel);
                ArchReq.TottalClaims = int.Parse(NumClaims);
                ArchReq.ServiceType = type;
                ArchReq.AttachFile = attch;
                ArchReq.Status = "NewArch";
                ArchReq.StatusColor = "#68c39f";
                ArchReq.TottalComments =Creator + " Said :-                                       @" + DateTime.Now.ToString() + Environment.NewLine + "         " + TotComm + Environment.NewLine;
                db.ArchPanels.InsertOnSubmit(ArchReq);
                db.SubmitChanges();

        }

       public List<Provider> GetAllReqRepeated(string CName , string month , string year)
       {
           List<Provider> data = (from p in db.Providers
                                   where (p.PName == CName || p.PMonth==month||p.PYear==year )
                                   orderby p.PId ascending
                                   select p).ToList();
           return data;
       }   // "usr" All Pending_missing Reauests page

       public void DeleteProvider(int id)
       {
           Provider bt = _db.Providers.Single(p => p.PId == id);
           _db.Providers.DeleteOnSubmit(bt);
           _db.SubmitChanges();
       }

        public void deletereq(int id)
        {
            requestTB req = new requestTB();
            Provider prov = new Provider();

            req = db.requestTBs.Single(r => r.id == id);
            prov = _db.Providers.Single(s => s.PId == req.ProviderID);
            prov.Assigned = null;

            db.requestTBs.DeleteOnSubmit(req);
            db.SubmitChanges();
            _db.SubmitChanges();

        }

        #endregion

        #region Admin & Archiing
        //get all closed requests for admin
        //get all new requests for admin and archiving

        public List<requestTB> GetAllReqNewNotAssigned()
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.Assigned == "false"
                                    orderby p.id ascending
                                    select p).ToList();
            return data;
        }


        public List<requestTB> GetAllReqSearching()
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rStatus == "Searching"
                                    orderby p.id ascending
                                    select p).ToList();
            return data;
        }
        #endregion

        #region Users Function


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
        public void UpdatePending(int id, string ArchivingComment, string foundcounter, string misspath, string foundpath)
        {
            requestTB acc = db.requestTBs.Single(p => p.id == id);
            //Provider pro = _db.Providers.Single(s => s.PId == acc.ProviderID);
            acc.rStatus = "Quality Control";
            acc.rStatusColor = "#0ea3dc";
            acc.ArchivingComment = ArchivingComment;
            acc.TottalFoundClaims = int.Parse(foundcounter);
            int miss = (int)acc.TottalClaims - int.Parse(foundcounter);
            acc.TottalMissClaims = miss;
            acc.FoundPath = foundpath;
            acc.MissingPath = misspath;
            acc.FinishedArchivingDate = DateTime.Now.ToString();
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            //db.Providers.(pro);
            db.SubmitChanges();
            //_db.SubmitChanges();

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

        //public List<requestTBsDesign> GetAllTpaRequestInExcel(string DateStr)   // by month
        //{
        //    var data = (from p in requestTBsDesign
        //                            //where p.ApprovedDate.Contains(DateStr)
        //                            where p.ApprovedDate == "26/10/2016 02:38:58 م"
        //                select new requestTBsDesign
        //                            {
        //                                id = p.id,
        //                                folderpath = p.folderpath,
        //                                rBody = p.rBody,
        //                                PolicyNum = p.PolicyNum,
        //                                TottalMoney = p.TottalMoney,
        //                                QualityPerson = p.QualityPerson,
        //                                QDate = p.QDate
        //                            }
        //                      ).ToList();
        //    return data;
        //}


        //public List<requestTB> GetAllTpaRequestInExcel(string DateStr)
        //{
        //    try
        //    {

        //        List<requestTB> employeeReport = (from itm in db.requestTBs
        //                                  where itm.ApprovedDate.Contains(DateStr)
        //                            select new requestTB
        //                            {
        //                                id = itm.id,
        //                                folderpath = itm.folderpath,
        //                                rBody = itm.rBody,
        //                                PolicyNum = itm.PolicyNum,
        //                                TottalMoney = itm.TottalMoney,
        //                                QualityPerson = itm.QualityPerson,
        //                                TBAPerson = itm.TBAPerson,
                                        

        //                            }).OrderByDescending(x => x.id).ToList();
        //            return employeeReport;
                
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        public List<requestTB> GetAllSearchByDateEntryReport(string employee, DateTime StartDate, DateTime EndDate)   // by month
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.AssDate >= StartDate && p.AssDate <= EndDate && p.AssigenPerson == employee && p.rStatus != "New" && p.rStatus != "Searching" && p.rStatus != "Pending Missing"
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }
        public List<requestTB> GetAllSearchBy_QC_Tpa_Report(string employee, DateTime StartDate, DateTime EndDate)   // by month
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.AssDate >= StartDate && p.AssDate <= EndDate && p.AssigenPerson == employee
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }


        public int Counter_Search_DataEntry_Req(string employee, DateTime StartDate, DateTime EndDate)
        {
            var data = (from p in db.requestTBs
                        where p.AssDate >= StartDate && p.AssDate <= EndDate && p.AssigenPerson == employee && p.rStatus != "New" && p.rStatus != "Searching" && p.rStatus != "Pending Missing"
                        select p).Count();
            int d = (int)data;
            return d;
        }
        public int CounterSearch_QC_Tpa_Req(string employee, DateTime StartDate, DateTime EndDate)
        {
            var data = (from p in db.requestTBs
                        where p.AssDate >= StartDate && p.AssDate <= EndDate && p.AssigenPerson == employee
                        select p).Count();
            int d = (int)data;
            return d;
        }


        public int Sum_Search_DataEntry_Req(string employee, DateTime StartDate, DateTime EndDate)
        {
            var data = (from p in db.requestTBs
                        where p.AssDate >= StartDate && p.AssDate <= EndDate && p.AssigenPerson == employee && p.rStatus != "New" && p.rStatus != "Searching" && p.rStatus != "Pending Missing"
                        select p.TottalFoundClaims).Sum();
            int counter = (int)data;
            return counter;
        }
        public int Sum_Search_QControl_Req(string employee, DateTime StartDate, DateTime EndDate)
        {
            var data = (from p in db.requestTBs
                        where p.AssDate >= StartDate && p.AssDate <= EndDate && p.AssigenPerson == employee
                        select p.ReturnQcCount).Sum();
            int counter = (int)data;
            return counter;
        }
        public int Sum_Search_TPA_Req(string employee, DateTime StartDate, DateTime EndDate)
        {
            var data = (from p in db.requestTBs
                        where p.AssDate >= StartDate && p.AssDate <= EndDate && p.AssigenPerson == employee
                        select p.ReturnTPACount).Sum();
            int counter = (int)data;
            return counter;
        }









        public int[] QualityClientSearchReq(string ClientName, string Month, string Year)
        {
            string path = ClientName + "/" + Year + "/" + Month;
            int[] results = new int[6];
            var TottalClaims = (from p in db.requestTBs
                                where ( p.folderpath == path) && (p.rStatus == "Quality Control" || p.rStatus == "Pending Confirmation" || p.rStatus == "Closed" )
                                select p.TottalClaims).Sum();
            
            var TottalFoundClaims = (from p in db.requestTBs
                                     where (p.folderpath == path) && (p.rStatus == "Quality Control" || p.rStatus == "Pending Confirmation" || p.rStatus == "Closed")
                                     select p.TottalFoundClaims).Sum();
            
            var TottalMissClaims = (from p in db.requestTBs
                                    where (p.folderpath == path) && (p.rStatus == "Quality Control" || p.rStatus == "Pending Confirmation" || p.rStatus == "Closed")
                                    select p.TottalMissClaims).Sum();
            
            var TottalDublicatedClaims = (from p in db.requestTBs
                                          where (p.folderpath == path) && (p.rStatus == "Quality Control" || p.rStatus == "Pending Confirmation" || p.rStatus == "Closed")
                                          select p.DublicatedClaims).Sum();
            
            var TottalInPatientClaims = (from p in db.requestTBs
                                         where (p.folderpath == path) && (p.rStatus == "Quality Control" || p.rStatus == "Pending Confirmation" || p.rStatus == "Closed")
                                         select p.InPatientClaims).Sum();
            
            var TottalWrongClaims = (from p in db.requestTBs
                                     where (p.folderpath == path) && (p.rStatus == "Quality Control" || p.rStatus == "Pending Confirmation" || p.rStatus == "Closed")
                                     select p.WrongClaims).Sum();
            results[0] = (int)TottalClaims;
            results[1] = (int)TottalFoundClaims;
            results[2] = (int)TottalMissClaims;
            results[3] = (int)TottalDublicatedClaims;
            results[4] = (int)TottalInPatientClaims;
            results[5] = (int)TottalWrongClaims;
            return results;
        }

        public int[] TBAClienSearchReq(string ClientName, string Month, string Year)
        {
            string path = ClientName + "/" + Year + "/" + Month;
            int[] results = new int[6];
            var TottalClaims = (from p in db.requestTBs
                                where (p.folderpath == path) && (p.rStatus == "Closed")
                                select p.TottalClaims).Sum();

            var TottalFoundClaims = (from p in db.requestTBs
                                     where (p.folderpath == path) && (p.rStatus == "Closed")
                                     select p.TottalFoundClaims).Sum();

            var TottalMissClaims = (from p in db.requestTBs
                                    where (p.folderpath == path) && (p.rStatus == "Closed")
                                    select p.TottalMissClaims).Sum();

            var TottalDublicatedClaims = (from p in db.requestTBs
                                          where (p.folderpath == path) && (p.rStatus == "Closed")
                                          select p.DublicatedClaims).Sum();

            var TottalInPatientClaims = (from p in db.requestTBs
                                         where (p.folderpath == path) && (p.rStatus == "Closed")
                                         select p.InPatientClaims).Sum();

            var TottalWrongClaims = (from p in db.requestTBs
                                     where (p.folderpath == path) && (p.rStatus == "Closed")
                                     select p.WrongClaims).Sum();
            results[0] = (int)TottalClaims;
            results[1] = (int)TottalFoundClaims;
            results[2] = (int)TottalMissClaims;
            results[3] = (int)TottalDublicatedClaims;
            results[4] = (int)TottalInPatientClaims;
            results[5] = (int)TottalWrongClaims;
            return results;
        }

        public IEnumerable<requestTB> QualityClientExportrReq(string ClientName, string Month, string Year)
        {
            string path = ClientName + "/" + Year + "/" + Month;
            List<requestTB> data = (from p in db.requestTBs
                                    orderby p.id ascending
                                    select p).ToList();
            return data;
            
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
        public void GetDetailByid(int id, ref string sub, ref string body, ref string attach, ref string AccountM, ref string UserComment, ref string CAsign, ref string AssignedPerson, ref string status, ref string addedtype)
        {
            var data = from p in db.requestTBs
                       where p.id == id

                       select p;
            CAsign = data.First().Assigned;
            AssignedPerson = data.First().AssigenPerson;
            sub = data.First().rSubject;
            body = data.First().rBody;
            attach = data.First().rAttach;
            AccountM = data.First().rFrom;
            status = data.First().rStatus;
            UserComment = data.First().ArchivingComment;
            addedtype = data.First().rAddedByType;

        }

        public List<requestTB> GETDAtaDeatailsSearch(int id)
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.id == id
                                    select p).ToList();
            return data;
        }


        public void DownloadLink(int id, ref string path)
        {
            var data = from p in db.requestTBs
                       where p.id == id
                       select p;
            path = data.First().rAttach;
        }
        #endregion

        #region Searching
        public List<requestTB> GetAllSearch(string word)
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rBody.Contains(word) || p.AssigenPerson.Contains(word)
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }

        public List<requestTB> GetAllSearchByMonth(string type , string client , string Pname , string month , string year)   // by month
        {
            string path = type + "/" + client + "/" + year + "/" + month;
            List<requestTB> data = (from p in db.requestTBs
                                    where  p.rBody.Contains(Pname) && p.folderpath.Contains(path)
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }

        public List<requestTB> GetClientsReportyMonth_QC(string client, string month, string year)   // by month
        {
            string path = client + "/" + year + "/" + month;
            List<requestTB> data = (from p in db.requestTBs
                                    where (p.folderpath == path) && (p.rStatus == "Quality Control" || p.rStatus == "Pending Confirmation" || p.rStatus == "Closed")
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }

        public List<requestTB> GetClientsReportyMonth_TBA(string client, string month, string year)   // by month
        {
            string path = client + "/" + year + "/" + month;
            List<requestTB> data = (from p in db.requestTBs
                                    where (p.folderpath == path) && (p.rStatus == "Closed")
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
        #endregion

        #region System New functions

        public void GetDetailByidnew(int id, ref string CType , ref string CName, ref string Pname, ref string AccountM, ref string attach, ref string AssignedPerson, ref string status, ref string CAsign, ref string addedtype, ref string NClaims, ref string misspath, ref string foundpath, ref string folderpath, ref string AllComments, ref string PolicyNumber)
        {
            var data = from p in db.requestTBs
                       where p.id == id

                       select p;
            CName = data.First().ClientName;
            CType = data.First().ClientType;
            Pname = data.First().rBody;
            AccountM = data.First().rFrom;
            attach = data.First().rAttach;
            AssignedPerson = data.First().AssigenPerson;
            status = data.First().rStatus;
            CAsign = data.First().Assigned;
            addedtype = data.First().rAddedByType;
            NClaims = data.First().TottalClaims.ToString();
            misspath = data.First().MissingPath;
            foundpath = data.First().FoundPath;
            folderpath = data.First().folderpath;
            AllComments = data.First().DisplayComments;
            PolicyNumber = data.First().PolicyNum;
        }

        public void GetDetailByidArchiving(int id, ref string CName, ref string Month, ref string Year, ref string status, ref string Creator, ref string TottalProviders, ref string Stype, ref string Pfile, ref string TExcel, ref string Reciver, ref string Assign, ref string Tclaims , ref string tottcomm)
        {
            var data = from p in db.ArchPanels
                       where p.ArchTickID == id
                       select p;

            CName = data.First().ClientName;
            Month = data.First().Month;
            Year = data.First().Year;
            status = data.First().Status;
            Creator = data.First().Creator;
            TottalProviders = data.First().TottalProviders.ToString();
            Stype = data.First().ServiceType;
            TExcel = data.First().TottalExcel.ToString();
            Pfile = data.First().AttachFile;
            Reciver = data.First().Reciver;
            Assign = data.First().Assigned;
            Tclaims = data.First().TottalClaims.ToString();
            tottcomm = data.First().TottalComments;


        }


        #region pages loading information
        public List<requestTB> GetAllReqNewByArchiving(string user)
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rStatus == "New" && p.AssigenPerson == user
                                    orderby p.id ascending
                                    select p).ToList();
            return data;
        }       //  All new Reauests page by archiving

        public List<requestTB> GetAllReqNewByUser(string user)
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rStatus == "New" && p.rFrom == user
                                    orderby p.id ascending
                                    select p).ToList();
            return data;
        }       //  All new Reauests page by user

        public List<requestTB> GetAllReqNewByAdmin()
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rStatus == "New"
                                    orderby p.id ascending
                                    select p).ToList();
            return data;
        }       //  All new Reauests page by Admin

        public List<requestTB> GetAllReqSearchingByUser(string user)
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rStatus == "Searching" && p.rFrom == user
                                    orderby p.id ascending
                                    select p).ToList();
            return data;
        }   // "usr" All searching Reauests page

        public List<requestTB> GetAllReqSearchingByArchiving(string user)
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rStatus == "Searching" && p.AssigenPerson == user
                                    orderby p.id ascending
                                    select p).ToList();
            return data;
        }   // "data_entry" All searching Reauests page

        public List<requestTB> GetAllReqSearchingByAdmin()
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rStatus == "Searching"
                                    orderby p.id ascending
                                    select p).ToList();
            return data;
        }   // "Admin" All searching Reauests page

        public List<requestTB> GetAllReqP_MissingByUser(string user)
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rStatus == "Pending Missing" && p.rFrom == user
                                    orderby p.id ascending
                                    select p).ToList();
            return data;
        }   // "usr" All Pending_missing Reauests page

        public List<requestTB> GetAllReqP_MissingByArchiving(string user)
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rStatus == "Pending Missing" && p.AssigenPerson == user
                                    orderby p.id ascending
                                    select p).ToList();
            return data;
        }   // "data_entry" All searching Reauests page

        public List<requestTB> GetAllReqP_MissingByAdmin()
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rStatus == "Pending Missing"
                                    orderby p.id ascending
                                    select p).ToList();
            return data;
        }   // "Admin" All searching Reauests page

        public List<requestTB> GetAllReq_PQ_ControlByUser()
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rStatus == "Pending Quality Control"
                                    orderby p.id ascending
                                    select p).ToList();
            return data;
        }   // "usr" All Quality Control Reauests page

        public List<requestTB> GetAllReq_PQ_ControlByArchiving(string user)
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rStatus == "Pending Quality Control" && p.AssigenPerson == user
                                    orderby p.id ascending
                                    select p).ToList();
            return data;
        }   // "data_entry" All Quality Control Reauests page

        public List<requestTB> GetAllReq_PQ_ControlByAdmin()
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rStatus == "Pending Quality Control"
                                    orderby p.id ascending
                                    select p).ToList();
            return data;
        }   // "Admin" All Quality Control Reauests page


        public List<requestTB> GetAllReqQ_ControlByUser()
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rStatus == "Quality Control"
                                    orderby p.id ascending
                                    select p).ToList();
            return data;
        }   // "usr" All Quality Control Reauests page

        public List<requestTB> GetAllReqQ_ControlByArchiving(string user)
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rStatus == "Quality Control" && p.AssigenPerson == user
                                    orderby p.id ascending
                                    select p).ToList();
            return data;
        }   // "data_entry" All Quality Control Reauests page

        public List<requestTB> GetAllReqQ_ControlByAdmin()
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rStatus == "Quality Control"
                                    orderby p.id ascending
                                    select p).ToList();
            return data;
        }   // "Admin" All Quality Control Reauests page
        
        
        
        public List<requestTB> GetAllReqPConfirmBYUser(string user)
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rStatus == "Pending Confirmation" && p.QualityPerson == user
                                    orderby p.id ascending
                                    select p).ToList();
            return data;
        }          // "usr" All Pending Conf Reauests page

        public List<requestTB> GetAllReqPConfirmBYTba()
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rStatus == "Pending Confirmation"
                                    orderby p.id ascending
                                    select p).ToList();
            return data;
        }          // "usr" All Pending Conf Reauests page

        public List<requestTB> GetAllReqPConfirmByArchiving(string user)
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rStatus == "Pending Confirmation" && p.AssigenPerson == user
                                    orderby p.id ascending
                                    select p).ToList();
            return data;
        }    // "data_entry" All Pending Conf Reauests page

        public List<requestTB> GetAllReqPConfirmByAdmin()
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rStatus == "Pending Confirmation"
                                    orderby p.id ascending
                                    select p).ToList();
            return data;
        }    // "Admin" All Pending Conf Reauests page

        ///////////////////////////////////////////////////////////////////

        public List<requestTB> GetAllReqAcceptBYUser(string user)
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rStatus == "Accept" && p.QualityPerson == user
                                    orderby p.id ascending
                                    select p).ToList();
            return data;
        }          // "usr" All  Accept Reauests page

        public List<requestTB> GetAllReqAcceptBYTba()
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rStatus == "Accept"
                                    orderby p.id ascending
                                    select p).ToList();
            return data;
        }          // "usr" All Accept Reauests page

        public List<requestTB> GetAllReqAcceptByArchiving(string user)
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rStatus == "Accept" && p.AssigenPerson == user
                                    orderby p.id ascending
                                    select p).ToList();
            return data;
        }    // "data_entry" All Accept Reauests page

        public List<requestTB> GetAllReqAcceptByAdmin()
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rStatus == "Accept"
                                    orderby p.id ascending
                                    select p).ToList();
            return data;
        }    // "Admin" All Accept Reauests page



        public List<requestTB> GetAllReqClosedBYACM(string user)
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rStatus == "Closed" && p.rFrom == user
                                    orderby p.id ascending
                                    select p).ToList();
            return data;
        }            // "usr" All closed Reauests page

        public List<requestTB> GetAllReqClosedByArchiving(string user)
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rStatus == "Closed" && p.AssigenPerson == user
                                    orderby p.id ascending
                                    select p).ToList();
            return data;
        }            // "data_entry" All closed Reauests page

        public List<requestTB> GetAllReqClosedAll()
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rStatus == "Closed"
                                    orderby p.id ascending
                                    select p).ToList();
            return data;
        }            // "data_entry" All closed Reauests page

        public List<requestTB> GetAllReqQualityClosedAll()
        {
            List<requestTB> data = (from p in db.requestTBs
                                    where p.rStatus == "Quality Closed"
                                    orderby p.id ascending
                                    select p).ToList();
            return data;
        }            // "data_entry" All closed Reauests page


        #endregion

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Archiving panel
        public void updateReviewPerson(int id, string user)
        {
            ArchPanel acc = db.ArchPanels.Single(p => p.ArchTickID == id);
            acc.Assigned = "true";
            acc.Status = "ReviewArch";
            acc.StatusColor = "#fff000";
            acc.Reciver = user;
            acc.ReciveDate = DateTime.Now.ToString();
            db.SubmitChanges();
        }
        public List<ArchPanel> GetAllReqNewArch()
        {
            List<ArchPanel> data = (from p in db.ArchPanels
                                    where p.Status == "NewArch"
                                    orderby p.ArchTickID ascending
                                    select p).ToList();
            return data;
        }
        public void updatePendingTicket(int id, string user, string DEntryComm)
        {
            ArchPanel acc = db.ArchPanels.Single(p => p.ArchTickID == id);
            acc.Status = "PendingArch";
            acc.StatusColor = "#15b224";
            acc.Pending = user;
            acc.PendingDate = DateTime.Now.ToString();
            acc.TottalComments = acc.TottalComments + Environment.NewLine + user + " Said :-                                       @" + DateTime.Now.ToString() + Environment.NewLine +"         " + DEntryComm + Environment.NewLine;
            db.SubmitChanges();
        }
        public List<ArchPanel> GetAllReqReviewArch()
        {
            List<ArchPanel> data = (from p in db.ArchPanels
                                    where p.Status == "ReviewArch"
                                    orderby p.ArchTickID ascending
                                    select p).ToList();
            return data;
        }
        public void updateClosedPerson(int id, string user)
        {
            ArchPanel acc = db.ArchPanels.Single(p => p.ArchTickID == id);
            acc.Status = "ClosedArch";
            acc.StatusColor = "#1678dc";
            acc.ClosedPerson = user;
            acc.ClosedDate = DateTime.Now.ToString();
            db.SubmitChanges();
        }
        public List<ArchPanel> GetAllReqClosedArch()
        {
            List<ArchPanel> data = (from p in db.ArchPanels
                                    where p.Status == "ClosedArch"
                                    orderby p.ArchTickID ascending
                                    select p).ToList();
            return data;
        }
        public void updateBackToNew(int id, string user, string newTottalProvider, string newTottalExcel, string newTottalClaims, string archcomm)
        {
            ArchPanel acc = db.ArchPanels.Single(p => p.ArchTickID == id);
            if (newTottalProvider == "")
            {
                acc.TottalProviders = acc.TottalProviders;
            }
            else
            {
                acc.TottalProviders = int.Parse(newTottalProvider);
            }
            if (newTottalExcel == "")
            {
                acc.TottalExcel = acc.TottalExcel;
            }
            else
            {
                acc.TottalExcel = int.Parse(newTottalExcel);
            }
            if (newTottalClaims == "")
            {
                acc.TottalClaims = acc.TottalClaims;
            }
            else
            {
                acc.TottalClaims = int.Parse(newTottalClaims);
            }
            acc.Status = "ReviewArch";
            acc.StatusColor = "#fff000";
            acc.BackToNewPer = user;
            acc.BackToNewDate = DateTime.Now.ToString();
            acc.TottalComments = acc.TottalComments + Environment.NewLine + user + " Said :-                                       @" + DateTime.Now.ToString() + Environment.NewLine + "         " + archcomm + Environment.NewLine;
            db.SubmitChanges();

        }
        public List<ArchPanel> GetAllReqPendingArch()
        {
            List<ArchPanel> data = (from p in db.ArchPanels
                                    where p.Status == "PendingArch"
                                    orderby p.ArchTickID ascending
                                    select p).ToList();
            return data;
        }
        //////////////////////////////////////////////////Notification////////////////////////////////
        public int GetAllReqNewArchCounter()
        {
            var data = (from p in db.ArchPanels
                        where p.Status == "NewArch"
                        select p).Count();
            int d = (int)data;
            return d;
        }
        public int GetAllReqReviewArchCounter()
        {
            var data = (from p in db.ArchPanels
                        where p.Status == "ReviewArch"
                        select p).Count();
            int d = (int)data;
            return d;
        }
        public int GetAllReqPendingArchCounter()
        {
            var data = (from p in db.ArchPanels
                        where p.Status == "PendingArch"
                        select p).Count();
            int d = (int)data;
            return d;
        }
        public int GetAllReqClosedArchCounter()
        {
            var data = (from p in db.ArchPanels
                        where p.Status == "ClosedArch"
                        select p).Count();
            int d = (int)data;
            return d;
        }
        public int GetAllReqNewCounter()
        {
            var data = (from p in db.requestTBs
                        where p.rStatus == "New"
                        select p).Count();
            int d = (int)data;
            return d;
        }                                                   // Admin
        public int GetAllReqNewCounterByUser(string user)
        {
            var data = (from p in db.requestTBs
                        where p.rStatus == "New" && p.rFrom == user
                        select p).Count();
            int d = (int)data;
            return d;
        }                                 // User
        public int GetAllReqNewCounterByDataEntry(string DataEntry)
        {
            var data = (from p in db.requestTBs
                        where p.rStatus == "New" && p.AssigenPerson == DataEntry
                        select p).Count();
            int d = (int)data;
            return d;
        }                       // DataEntry

        public int GetAllReqSearchingCounter()
        {
            var data = (from p in db.requestTBs
                        where p.rStatus == "Searching"
                        select p).Count();
            int d = (int)data;
            return d;
        }                                           // Admin
        public int GetAllReqSearchingCounterByUser(string user)
        {
            var data = (from p in db.requestTBs
                        where p.rStatus == "Searching" && p.rFrom == user
                        select p).Count();
            int d = (int)data;
            return d;
        }                          // User
        public int GetAllReqSearchingCounterByDataEntry(string DataEntry)
        {
            var data = (from p in db.requestTBs
                        where p.rStatus == "Searching" && p.AssigenPerson == DataEntry
                        select p).Count();
            int d = (int)data;
            return d;
        }               // DataEntry

        public int GetAllReqPMissingCounter()
        {
            var data = (from p in db.requestTBs
                        where p.rStatus == "Pending Missing"
                        select p).Count();
            int d = (int)data;
            return d;
        }                                           // Admin
        public int GetAllReqPMissingCounterByUser(string user)
        {
            var data = (from p in db.requestTBs
                        where p.rStatus == "Pending Missing" && p.rFrom == user
                        select p).Count();
            int d = (int)data;
            return d;
        }                          // User
        public int GetAllReqPMissingCounterByDataEntry(string DataEntry)
        {
            var data = (from p in db.requestTBs
                        where p.rStatus == "Pending Missing" && p.AssigenPerson == DataEntry
                        select p).Count();
            int d = (int)data;
            return d;
        }                // DataEntry

        public int GetAllReqQControlCounter()
        {
            var data = (from p in db.requestTBs
                        where p.rStatus == "Quality Control"
                        select p).Count();
            int d = (int)data;
            return d;
        }                                           // Admin
        public int GetAllReqPQControlCounter()
        {
            var data = (from p in db.requestTBs
                        where p.rStatus == "Pending Quality Control"
                        select p).Count();
            int d = (int)data;
            return d;
        }                                           // Admin

        public int GetAllReqQControlCounterByUser(string user)
        {
            var data = (from p in db.requestTBs
                        where p.rStatus == "Quality Control"
                        select p).Count();
            int d = (int)data;
            return d;
        }                         // User
        public int GetAllReqPQControlCounterByUser(string user)
        {
            var data = (from p in db.requestTBs
                        where p.rStatus == "Pending Quality Control"
                        select p).Count();
            int d = (int)data;
            return d;
        }                         // User

        public int GetAllReqQControlCounterByDataEntry(string DataEntry)
        {
            var data = (from p in db.requestTBs
                        where p.rStatus == "Quality Control" && p.AssigenPerson == DataEntry
                        select p).Count();
            int d = (int)data;
            return d;
        }              // DataEntry
        public int GetAllReqPQControlCounterByDataEntry(string DataEntry)
        {
            var data = (from p in db.requestTBs
                        where p.rStatus == "Pending Quality Control" && p.AssigenPerson == DataEntry
                        select p).Count();
            int d = (int)data;
            return d;
        }              // DataEntry

        public int GetAllReqPConfirmationCounter()
        {
            var data = (from p in db.requestTBs
                        where p.rStatus == "Pending Confirmation"
                        select p).Count();
            int d = (int)data;
            return d;
        }                                   // Admin
        public int GetAllReqPConfirmationCounterByUser(string user)
        {
            var data = (from p in db.requestTBs
                        where p.rStatus == "Pending Confirmation" && p.QualityPerson == user
                        select p).Count();
            int d = (int)data;
            return d;
        }                 // User
        public int GetAllReqPConfirmationCounterByDataEntry(string DataEntry)
        {
            var data = (from p in db.requestTBs
                        where p.rStatus == "Pending Confirmation" && p.AssigenPerson == DataEntry
                        select p).Count();
            int d = (int)data;
            return d;
        }      // DataEntry

        public int GetAllReqAcceptCounter()
        {
            var data = (from p in db.requestTBs
                        where p.rStatus == "Accept"
                        select p).Count();
            int d = (int)data;
            return d;
        }                                   // Admin
        public int GetAllReqAcceptCounterByUser(string user)
        {
            var data = (from p in db.requestTBs
                        where p.rStatus == "Accept" && p.QualityPerson == user
                        select p).Count();
            int d = (int)data;
            return d;
        }                 // User
        public int GetAllReqAcceptCounterByDataEntry(string DataEntry)
        {
            var data = (from p in db.requestTBs
                        where p.rStatus == "Accept" && p.AssigenPerson == DataEntry
                        select p).Count();
            int d = (int)data;
            return d;
        }      // DataEntry

        public int GetAllReqClosedCounter()
        {
            var data = (from p in db.requestTBs
                        where p.rStatus == "Closed"
                        select p).Count();
            int d = (int)data;
            return d;
        }                                       // Admin
        public int GetAllReqQualityClosedCounter()
        {
            var data = (from p in db.requestTBs
                        where p.rStatus == "Quality Closed"
                        select p).Count();
            int d = (int)data;
            return d;
        }                                       // Admin

        public int GetAllReqClosedCounterByUser(string user)
        {
            var data = (from p in db.requestTBs
                        where p.rStatus == "Closed" && p.rFrom == user
                        select p).Count();
            int d = (int)data;
            return d;
        }                     // User
        public int GetAllReqClosedCounterByDataEntry(string DataEntry)
        {
            var data = (from p in db.requestTBs
                        where p.rStatus == "Closed" && p.AssigenPerson == DataEntry
                        select p).Count();
            int d = (int)data;
            return d;
        }          // DataEntry





        #endregion

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        
        
        #region Action_Related_Function

        //////////////////////////////////////////////////////////////////////////////////
        public void updateReciveTicket(int id, string user)
        {
            ArchPanel acc = db.ArchPanels.Single(p => p.ArchTickID == id);
            if(acc.Pending==null && acc.PendingDate==null)
            { 
                acc.Pending = " "; acc.PendingDate = " "; 
            }
            acc.Status = "ClosedArch";
            acc.StatusColor = "#ffc052";
            acc.Reciver = user;
            acc.ReciveDate = DateTime.Now.ToString(); ;
            db.SubmitChanges();
        }


        //////////////////////////////////////////////////////////////////////////////////
        public void updateAssignPerson(int id, string user)
        {
            requestTB acc = db.requestTBs.Single(p => p.id == id);
            acc.Assigned = "true";
            acc.rStatus = "Searching";
            acc.rStatusColor = "#ffc052";
            acc.AssDate = DateTime.Now;
            db.SubmitChanges();
        }
        public void UpdateQualityControlFound(int id, string ArchivingComment, string foundcounter, string Dublicatedcounter, string InPatient, string WrongCounter, string misspath, string foundpath, string Moneycounter, string YourComment, string user, string RejCounter, string RecCounter)
        {
            requestTB acc = db.requestTBs.Single(p => p.id == id);
            //Provider pro = _db.Providers.Single(s => s.PId == acc.ProviderID);
            acc.rStatus = "Pending Quality Control";
            acc.rStatusColor = "#0ea3dc";
            acc.ArchivingComment = ArchivingComment;
            //acc.TottalFoundClaims = acc.TottalClaims;
           
            acc.DublicatedClaims = int.Parse(Dublicatedcounter);
            acc.InPatientClaims = int.Parse(InPatient);
            acc.WrongClaims = int.Parse(WrongCounter);
            acc.RejectedClaims = int.Parse(RejCounter);
            acc.ReceiveClaims = int.Parse(RecCounter);
            acc.TottalMoney = int.Parse(Moneycounter);
            if (YourComment != "")
            {
                acc.DisplayComments = acc.DisplayComments + Environment.NewLine + user + " Said : " + " at      " + DateTime.Now.ToString() + Environment.NewLine + "         " + YourComment + Environment.NewLine;
            }
            int realfound = (int)acc.TottalClaims - (int.Parse(Dublicatedcounter)) - (int.Parse(InPatient)) - (int.Parse(WrongCounter)) - (int.Parse(RejCounter)) - (int.Parse(RecCounter));
            acc.TottalFoundClaims = realfound;            
            acc.TottalMissClaims = 0;
            acc.FoundPath = foundpath;
            //acc.MissingPath = misspath;
            acc.FinishedArchivingDate = DateTime.Now.ToString();
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            //db.Providers.(pro);
            db.SubmitChanges();
            //_db.SubmitChanges();

        }
        public void UpdateQualityControlFoundAndMiss(int id, string ArchivingComment, string foundcounter, string Dublicatedcounter, string InPatient, string WrongCounter, string misspath, string foundpath, string Moneycounter, string YourComment, string user, string RejCounter, string RecCounter)
        {
            requestTB acc = db.requestTBs.Single(p => p.id == id);
            //Provider pro = _db.Providers.Single(s => s.PId == acc.ProviderID);
            acc.rStatus = "Pending Quality Control";
            acc.rStatusColor = "#0ea3dc";
            acc.ArchivingComment = ArchivingComment;

            
            
                acc.DublicatedClaims = int.Parse(Dublicatedcounter);
                acc.InPatientClaims = int.Parse(InPatient);
                acc.WrongClaims = int.Parse(WrongCounter);
                acc.RejectedClaims = int.Parse(RejCounter);
                acc.ReceiveClaims = int.Parse(RecCounter);
                acc.TottalMoney = int.Parse(Moneycounter);
                if (YourComment != "")
                {
                    acc.DisplayComments = acc.DisplayComments + Environment.NewLine + user + " Said : " + " at      " + DateTime.Now.ToString() + Environment.NewLine + "         " + YourComment + Environment.NewLine;
                }

                acc.TottalFoundClaims = int.Parse(foundcounter);
                int miss = (int)acc.TottalClaims - int.Parse(foundcounter) - (int)acc.DublicatedClaims - (int)acc.InPatientClaims - (int)acc.WrongClaims - (int)acc.RejectedClaims - (int)acc.ReceiveClaims;
                acc.TottalMissClaims = miss;
             
            acc.FoundPath = foundpath;
            acc.MissingPath = misspath;
            acc.FinishedArchivingDate = DateTime.Now.ToString();
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            db.SubmitChanges();
            //_db.SubmitChanges();

        }
        public void UpdatePendingMissing(int id)
        {
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            requestTB acc = db.requestTBs.Single(p => p.id == id);
            acc.rStatus = "Pending Missing";
            acc.rStatusColor = "#e15554";
            //acc.rApproved = "true";
            acc.MissDate = DateTime.Now.ToString();
            db.SubmitChanges();
        }
        public void BackToSearching(int id, string user, string YourComment)
        {
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            requestTB acc = db.requestTBs.Single(p => p.id == id);
            acc.rStatus = "Searching";
            acc.rStatusColor = "#ffc052";
            acc.ReturnQcCount += 1;
            acc.ReturnQcPerson = user;
            acc.DisplayComments = acc.DisplayComments + Environment.NewLine + user + " Said : " + " at      " + DateTime.Now.ToString() + Environment.NewLine + "         " + YourComment + Environment.NewLine;
            db.SubmitChanges();
        }

        public void BackToQuality(int id, string user, string YourComment)
        {
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            requestTB acc = db.requestTBs.Single(p => p.id == id);
            acc.rStatus = "Quality Control";
            acc.rStatusColor = "#0ea3dc";
            acc.ReturnTPACount += 1;
            acc.ReturnTPAPerson = user;
            acc.DisplayComments = acc.DisplayComments + Environment.NewLine + user + " Said : " + " at      " + DateTime.Now.ToString() + Environment.NewLine + "         " + YourComment + Environment.NewLine;
            db.SubmitChanges();
        }
        public void UpdatePendingQC(int id , string user)
        {
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            requestTB acc = db.requestTBs.Single(p => p.id == id);
            acc.rStatus = "Quality Control";
            acc.rStatusColor = "#e15554";
            acc.rApproved = "true";
            acc.ReciveQCPerson = user;
            acc.ReciveQCDate = DateTime.Now.ToString();
            db.SubmitChanges();
        }
        public void UpdatePrimeConfirmation(int id, string user)
        {
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            requestTB acc = db.requestTBs.Single(p => p.id == id);
            acc.rStatus = "Pending Confirmation";
            acc.rStatusColor = "#e15554";
            acc.rApproved = "true";
            acc.QualityPerson = user;
            acc.QDate = DateTime.Now.ToString();
            db.SubmitChanges();
        }
        public void UpdateQualityClosing(int id, string user)
        {
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            requestTB acc = db.requestTBs.Single(p => p.id == id);
            acc.rStatus = "Quality Closed";
            acc.rStatusColor = "#e15554";
            acc.rApproved = "true";
            acc.QualityPerson = user;
            acc.QDate = DateTime.Now.ToString();
            db.SubmitChanges();
        }

        public void UpdateAcceptArchiving(int id , string user)
        {
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            requestTB acc = db.requestTBs.Single(p => p.id == id);
            acc.rStatus = "Accept";
            acc.rStatusColor = "#e15554";
            acc.rAccept = "true";
            acc.TBAAccept = user;
            acc.AcceptDate = DateTime.Now.ToString();
            db.SubmitChanges();
        }
        public void UpdateClosedArchiving(int id, string user)
        {
            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            requestTB acc = db.requestTBs.Single(p => p.id == id);
            acc.rStatus = "Closed";
            acc.rStatusColor = "#e15554";
            acc.rApproved = "true";
            acc.TBAPerson = user;
            acc.ApprovedDate = DateTime.Now.ToString();
            db.SubmitChanges();
        }

        #endregion

        #endregion



    }

}