using CallCenterNotesApp.DLL;
using CallCenterNotesApp.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CallCenterNotesApp.BLL
{
    public class DashboardSearchAndPaging
    {

        public List<CallCenterNotesApp.DLL.EmailApprovalRequest> SearchRequestsByType (string Type ,string SearchFilter, string SearchKeyWord)
        {
            PhNetworkEntities Db = new PhNetworkEntities();
            List<CallCenterNotesApp.DLL.EmailApprovalRequest> FilteredRequests;
            if (Type == null && SearchKeyWord ==null)
            {
                FilteredRequests = (from t in Db.EmailApprovalRequests select t).ToList();
                return FilteredRequests;
            }
            //else if (Type != null && SearchKeyWord==null)
            //{
            //    FilteredRequests = (from t in Db.CallCenterEmailApprovals where t.ClientType==Type  select t).ToList();
            //    return FilteredRequests;
            //}
            else 
            {
                if (SearchFilter== "ID")
                {
                    int SearchId = int.Parse(SearchKeyWord);
                    FilteredRequests = (from t in Db.EmailApprovalRequests where t.ID== SearchId select t).ToList();
                    return FilteredRequests;
                }
                if (SearchFilter == "PatientName")
                {
                    FilteredRequests = (from t in Db.EmailApprovalRequests where t.PatientName.Contains(SearchKeyWord) select t).ToList();
                    return FilteredRequests;
                }
                if (SearchFilter == "UserMedicalID")
                {
                    FilteredRequests = (from t in Db.EmailApprovalRequests where t.Medical_ID.ToString() == SearchKeyWord select t).ToList();
                    return FilteredRequests;
                }
                if (SearchFilter == "CompanyName")
                {
                    FilteredRequests = (from t in Db.EmailApprovalRequests where t.CompanyName.Contains(SearchKeyWord) select t).ToList();
                    return FilteredRequests;
                }
                if (SearchFilter == "CallcenterOpenNote")
                {
                    FilteredRequests = (from t in Db.EmailApprovalRequests where t.CreatedByNotes.Contains(SearchKeyWord) select t).ToList();
                    return FilteredRequests;
                }
                if (SearchFilter == "CallcenterTicketCreator")
                {
                    FilteredRequests = (from t in Db.EmailApprovalRequests where t.CreatedBy.Contains(SearchKeyWord) select t).ToList();
                    return FilteredRequests;
                }
                if (SearchFilter == "CallcenterlTicketCraetionTime")
                {
                    FilteredRequests = (from t in Db.EmailApprovalRequests where t.CreationDate.ToString().Contains(SearchKeyWord) select t).ToList();
                    return FilteredRequests;
                }
                if (SearchFilter == "CallcenterAssignDoctor")
                {
                    FilteredRequests = (from t in Db.EmailApprovalRequests where t.DoctorAssignee.Contains(SearchKeyWord) select t).ToList();
                    return FilteredRequests;
                }
                if (SearchFilter == "AuditApprovedName")
                {
                    FilteredRequests = (from t in Db.EmailApprovalRequests where t.AuditAssignee.Contains(SearchKeyWord) select t).ToList();
                    return FilteredRequests;
                }
                //if (SearchFilter == "ReqStatues")
                //{
                //    FilteredRequests = (from t in Db.EmailApprovalRequests where t.RequstStatusID.Contains(SearchKeyWord) select t).ToList();
                //    return FilteredRequests;
                //}
                if (SearchFilter == "ApprovalToCompanyName")
                {
                    FilteredRequests = (from t in Db.EmailApprovalRequests where t.CompanyName.Contains(SearchKeyWord) select t).ToList();
                    return FilteredRequests;
                }

                else
                {
                    return null;
                }

            }

            
        }

       public IEnumerable<dynamic> PageItems(int numberOfObjectsPerPage,int pageNumber,IEnumerable<ModelViewTable> Table)
        {
            //CallcentereMailRequest mdb = new CallcentereMailRequest();

            return Table.Skip(numberOfObjectsPerPage * pageNumber).Take(numberOfObjectsPerPage).ToList();


        }

        public int PageNumbers(int pagelimit,int requestcount)
        {
            return (int)Math.Ceiling((double)requestcount / pagelimit); ;
              

        }





    }
}