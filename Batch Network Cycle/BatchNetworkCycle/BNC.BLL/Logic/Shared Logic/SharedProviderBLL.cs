using BNC.BLL.Logic.Tables;
using BNC.BLL.StaticData;
using BNC.DTOs;
using BNC.DTOs.Business;
using BNC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BNC.BLL.StaticData.StaticEnums;

namespace BNC.BLL.Logic.Shared_Logic
{
    public class SharedProviderBLL
    {
        ReceivingRequestBLL receivingRequestBLL;
        FilterationRequestBLL filterationRequestBLL;
        FilterationCategoriesBLL filterationCategoriesBLL;
        BatchingRequestBLL batchingRequestBLL;
        BatchAuditingRequestBLL batchAuditingRequestBLL;
        BatchingFilterationDetailsBLL batchingFilterationDetailsBLL;
        DataEntryAdminstrationRequestBLL dataEntryAdminstrationRequestBLL;
        DataEntryBatchAssigningRequestBLL dataEntryBatchAssigningRequestBLL;
        BatchClosingRequestBLL batchClosingRequestBLL;
        ClosedBatchReAdministrationRequestBLL closedBatchReAdministrationRequestBLL;
        SharedSearchBLL sharedSearchBLL;
        LockLoggesBLL lockLoggesBLL;
        UserBLL userBLL;
        SystemEntitiesBLL systemEntitiesBLL;
        InsuranceSystemBLL insuranceSystemBLL;
        StatusBLL statusBLL;
        DateTime creationDate;
        public SharedProviderBLL(BNCEntities Context, DateTime CreationDate)
        {
            receivingRequestBLL = new ReceivingRequestBLL(Context, CreationDate);
            filterationRequestBLL = new FilterationRequestBLL(Context, CreationDate);
            filterationCategoriesBLL = new FilterationCategoriesBLL(Context, CreationDate);
            batchingRequestBLL = new BatchingRequestBLL(Context, CreationDate);
            batchAuditingRequestBLL = new BatchAuditingRequestBLL(Context, CreationDate);
            batchingFilterationDetailsBLL = new BatchingFilterationDetailsBLL(Context, CreationDate);
            dataEntryAdminstrationRequestBLL = new DataEntryAdminstrationRequestBLL(Context, CreationDate);
            dataEntryBatchAssigningRequestBLL = new DataEntryBatchAssigningRequestBLL(Context, CreationDate);
            batchClosingRequestBLL = new BatchClosingRequestBLL(Context, CreationDate);
            closedBatchReAdministrationRequestBLL = new ClosedBatchReAdministrationRequestBLL(Context, CreationDate);
            sharedSearchBLL = new SharedSearchBLL(Context, CreationDate);
            lockLoggesBLL = new LockLoggesBLL(Context, CreationDate);
            insuranceSystemBLL = new InsuranceSystemBLL(Context, CreationDate);
            statusBLL = new StatusBLL(Context, CreationDate);
            userBLL = new UserBLL(Context, CreationDate);
            systemEntitiesBLL = new SystemEntitiesBLL(Context, CreationDate);
            creationDate = CreationDate;
        }
        private string GetActionPage(int TableID, int RecordID)
        {
            if (TableID == (int)Entity.ReceivingRequest)
                return "<a class='btn btn-primary btn-lg' role='button' href=/Receiving/AddRecievingRequest?ReceivingRequestID=" + RecordID + ">Open</a>";
            else if (TableID == (int)Entity.BatchingFilterationDetails)
                return "";
            else if (TableID == (int)Entity.BatchRequest)
                return "<a class='btn btn-primary btn-lg' role='button' href=/Batching/ViewBatchingRequest?BatchID=" + RecordID + ">Open</a>";
            else if (TableID == (int)Entity.AuditRequest)
                return "";
            else if (TableID == (int)Entity.DataEntryAdminstrationRequest)
                return "<a class='btn btn-primary btn-lg' role='button' href=/DataEntry/AddDataEntryRequest?ID=" + RecordID + ">Open</a>";
           // else if (TableID == (int)Entity.DataEntryBatchAssigningRequest)
           //   return "<a class='btn btn-primary btn-lg' role='button' href=/DataEntry/DataEntryActions?ID=" + RecordID + ">Open</a>";
            else if (TableID == (int)Entity.BatchClosingRequest)
                return "<a class='btn btn-primary btn-lg' role='button' href=/DataEntry/DataEntryActions?ID=" + RecordID + ">Open</a>";
            else if (TableID == (int)Entity.ClosedBatchReAdministrationRequest)
                return "<a class='btn btn-primary btn-lg' role='button' href=/ReAdministartion/ClosingReAdministrationTeamRequestActions?ID=" + RecordID + ">Open</a>";
            return "";
        }
        public List<LockRequestDTO> GetLockRequests()
        {
            List<LockRequestDTO> lockRequests = new List<LockRequestDTO>();
            List<LockLogge> lockLogges = lockLoggesBLL.Find(x => x.ActionFK == (int)Actions.Locked && x.IsActive == true).ToList();

            foreach (var items in lockLogges)
            {
                lockRequests.Add(new LockRequestDTO
                {
                    RequestID = items.LockLoggesID,
                    UserName = userBLL.getUserName(items.UserFK),
                    EntityName = systemEntitiesBLL.GetEntityName(items.SystemEntityFK),
                    Action = GetActionPage(items.SystemEntityFK, items.EntityRecordID)
                });
            }

            return lockRequests;
        }
        private void AddLockLogge(SearchCriteriaDTO searchCriteriaDTO)
        {
            LockLogge OldLockLogge = lockLoggesBLL.Find(x => x.SystemEntityFK == searchCriteriaDTO.TableID && x.EntityRecordID == searchCriteriaDTO.RecordID && x.IsActive == true).ToList().LastOrDefault();

            if(OldLockLogge != null)
            {
                OldLockLogge.IsActive = false;
                OldLockLogge.ModificationDate = creationDate;

                lockLoggesBLL.Edit(OldLockLogge);
            }

            LockLogge lockLogge = new LockLogge
            {
                ActionFK = searchCriteriaDTO.IsLocked ? (int)Actions.Locked : (int)Actions.UnLocked,
                UserFK = searchCriteriaDTO.UserID,
                SystemEntityFK = searchCriteriaDTO.TableID,
                EntityRecordID = searchCriteriaDTO.RecordID,
                IsActive = true,
                IsDeleted = false,
                CreationDate = creationDate
            };

            lockLoggesBLL.Add(lockLogge);
        }
        
        public void LockUnLockRequest(SearchCriteriaDTO searchCriteriaDTO)
        {
            if (searchCriteriaDTO.TableID == (int)Entity.ReceivingRequest)
            {
                var Request = receivingRequestBLL.Find(x => x.ReceivingRequestID == searchCriteriaDTO.RecordID).FirstOrDefault();
                Request.IsLocked = searchCriteriaDTO.IsLocked;
                AddLockLogge(searchCriteriaDTO);

                receivingRequestBLL.Edit(Request);
            }
            else if (searchCriteriaDTO.TableID == (int)Entity.FilterationRequest)
            {
                var Request = filterationRequestBLL.Find(x => x.FilterationRequestID == searchCriteriaDTO.RecordID).FirstOrDefault();
                Request.IsLocked = searchCriteriaDTO.IsLocked;
                AddLockLogge(searchCriteriaDTO);

                filterationRequestBLL.Edit(Request);
            }
            else if (searchCriteriaDTO.TableID == (int)Entity.BatchingFilterationDetails)
            {
                var Request = batchingFilterationDetailsBLL.Find(x => x.BatchingFilterationDetailID == searchCriteriaDTO.RecordID).FirstOrDefault();
                Request.IsLocked = searchCriteriaDTO.IsLocked;
                AddLockLogge(searchCriteriaDTO);

                batchingFilterationDetailsBLL.Edit(Request);
            }
            else if (searchCriteriaDTO.TableID == (int)Entity.BatchRequest)
            {
                var Request = batchingRequestBLL.Find(x => x.BatchingRequestID == searchCriteriaDTO.RecordID).FirstOrDefault();
                Request.IsLocked = searchCriteriaDTO.IsLocked;
                AddLockLogge(searchCriteriaDTO);

                batchingRequestBLL.Edit(Request);
            }
            else if (searchCriteriaDTO.TableID == (int)Entity.AuditRequest)
            {
                var Request = batchAuditingRequestBLL.Find(x => x.BatchAuditingRequestID == searchCriteriaDTO.RecordID).FirstOrDefault();
                Request.IsLocked = searchCriteriaDTO.IsLocked;
                AddLockLogge(searchCriteriaDTO);

                batchAuditingRequestBLL.Edit(Request);
            }
            else if (searchCriteriaDTO.TableID == (int)Entity.DataEntryAdminstrationRequest)
            {
                var Request = dataEntryAdminstrationRequestBLL.Find(x => x.DataEntryAdminstrationRequestID == searchCriteriaDTO.RecordID).FirstOrDefault();
                Request.IsLocked = searchCriteriaDTO.IsLocked;
                AddLockLogge(searchCriteriaDTO);

                dataEntryAdminstrationRequestBLL.Edit(Request);
            }
            else if (searchCriteriaDTO.TableID == (int)Entity.DataEntryBatchAssigningRequest)
            {
                var Request = dataEntryBatchAssigningRequestBLL.Find(x => x.DataEntryBatchAssigningRequestID == searchCriteriaDTO.RecordID).FirstOrDefault();
                Request.IsLocked = searchCriteriaDTO.IsLocked;
                AddLockLogge(searchCriteriaDTO);

                dataEntryBatchAssigningRequestBLL.Edit(Request);
            }
            else if (searchCriteriaDTO.TableID == (int)Entity.BatchClosingRequest)
            {
                var Request = batchClosingRequestBLL.Find(x => x.BatchClosingRequestID == searchCriteriaDTO.RecordID).FirstOrDefault();
                Request.IsLocked = searchCriteriaDTO.IsLocked;
                AddLockLogge(searchCriteriaDTO);

                batchClosingRequestBLL.Edit(Request);
            }
            else if (searchCriteriaDTO.TableID == (int)Entity.ClosedBatchReAdministrationRequest)
            {
                var Request = closedBatchReAdministrationRequestBLL.Find(x => x.ClosedBatchReAdmissionRequestID == searchCriteriaDTO.RecordID).FirstOrDefault();
                Request.IsLocked = searchCriteriaDTO.IsLocked;
                AddLockLogge(searchCriteriaDTO);

                closedBatchReAdministrationRequestBLL.Edit(Request);
            }
            //else if (searchCriteriaDTO.TableID == (int)Entity.DataEntryRequest)
            //{
            //    var Request = dataEntryBatchAssigningRequestBLL.Find(x => x.DataEntryBatchAssigningRequestID == searchCriteriaDTO.RecordID).FirstOrDefault();
            //    Request.IsLocked = searchCriteriaDTO.IsLocked;
            //    AddLockLogge(searchCriteriaDTO);

            //    dataEntryBatchAssigningRequestBLL.Edit(Request);
            //}
            else if (searchCriteriaDTO.TableID == (int)Entity.DataEntryClosingRequest)
            {
                var Request = batchClosingRequestBLL.Find(x => x.BatchClosingRequestID == searchCriteriaDTO.RecordID).FirstOrDefault();
                Request.IsLocked = searchCriteriaDTO.IsLocked;
                AddLockLogge(searchCriteriaDTO);

                batchClosingRequestBLL.Edit(Request);
            }
          
        }
        public List<ShowReceivingRequestDTO> GetRecievingRequestsByStatus(SearchCriteriaDTO SearchCriteria)
        {
            List<ShowReceivingRequestDTO> ReceivingRequests = new List<ShowReceivingRequestDTO>();
            foreach (var item in sharedSearchBLL.Search(SearchCriteria))
            {
                ReceivingRequests.Add(new ShowReceivingRequestDTO
                {
                    RequestID = item.ReceivingRequestID,
                    ClaimsCount = item.ReceivingOfficerCalimsCount,
                    BilllingMonth = item.BilllingMonth,
                    BillingYear = item.BillingYear,
                   //ReceivingDate = item.ReceivingDate,                   
                    Action = "<a class='btn btn-primary btn-xs' role='button' href=/Receiving/AddRecievingRequest?ReceivingRequestID=" + item.ReceivingRequestID + ">Open</a>"
                });

            }
            return ReceivingRequests;
        }
        public List<ShowFilterationRequestDTO> GetFilterationRequestsByStatus(SearchCriteriaDTO SearchCriteria)
        {
            List<ShowFilterationRequestDTO> showFilterationRequestDTOs = new List<ShowFilterationRequestDTO>();

            foreach(var items in sharedSearchBLL.Search(SearchCriteria))
            {
                showFilterationRequestDTOs.Add(new ShowFilterationRequestDTO
                {
                    RequestID = items.BatchingFilterationDetailID,
                    ClaimsCount = items.TotalClaimsCount,
                    Category = filterationCategoriesBLL.getFilterationCategoryName(items.FilterationCategoryFK),
                    Action = ""
                });
            }

            return showFilterationRequestDTOs;

        }
        public List<ShowBatchRequestDTO> GetBatchRequestsByStatus(SearchCriteriaDTO SearchCriteria)
        
{
            List<ShowBatchRequestDTO> BatchRequests = new List<ShowBatchRequestDTO>();
            foreach(var item in sharedSearchBLL.Search(SearchCriteria))
            {
                BatchRequests.Add(new ShowBatchRequestDTO {
                    RequestID = item.BatchingRequestID,
                    ClaimsCount = item.NumberOfBatchClaims,
                    System = insuranceSystemBLL.getInsuranceSystemName(item.BatchSystemFK),
                    BatchNumber = item.BatchNumber,
                    //CreationDate = item.CreationDate,
                    Action = "<a class='btn btn-primary btn-xs' role='button' href=/Batching/ViewBatchingRequest?BatchID=" + item.BatchingRequestID + ">Open</a>"
                });
            }
            return BatchRequests;
        }
        public List<ShowBatchAuditRequestDTO> GetBatchAuditRequestByStatus(SearchCriteriaDTO SearchCriteria)
        {
            List<ShowBatchAuditRequestDTO> showBatchAuditRequestDTOs = new List<ShowBatchAuditRequestDTO>();

            foreach(var items in sharedSearchBLL.Search(SearchCriteria))
            {
                showBatchAuditRequestDTOs.Add(new ShowBatchAuditRequestDTO {
                    RequestID = items.BatchAuditingRequestID,
                    BatchingRequestID = items.BatchingRequestFK,
                    StatusName = statusBLL.GetStatusName(items.BatchAuditingStatusFK),
                    NumberOfApprovedClaims = items.TotalNumberOfApprovedClaims,
                    NumberOfRejectedClaims = items.TotalNumberOfRejectedClaims,
                    Actions = "<a class='btn btn-primary btn-xs' role='button' href=/Audit/DirectBatchToAuditBage?BatchID=" + items.BatchingRequestFK + ">Open</a>"
                });
            }
   
            return showBatchAuditRequestDTOs;
        }
        public List<ShowAuditRequestDTO> GetMedicalAuditRequestByStatus(SearchCriteriaDTO SearchCriteria)
        {
            List<ShowAuditRequestDTO> showMedicalAuditRequestDTOs = new List<ShowAuditRequestDTO>();

            foreach(var items in sharedSearchBLL.Search(SearchCriteria))
            {
                showMedicalAuditRequestDTOs.Add(new ShowAuditRequestDTO
                {
                    RequestID = items.MedicalAuditRequestID,
                    BatchAuditRequestID = items.BatchAuditRequestFK,
                    OfficerName = userBLL.getUserName(items.MedicalOfficerAssigneeFK),
                    NumberOfApprovedClaims = items.NumberOfApprovedClaims,
                    NumberOfPendingClaims = items.NumberOfPendingClaims,
                    NumberOfRejectedClaims = items.NumberOfRejectedClaims,
                    Action = ""
                });
            }
            return showMedicalAuditRequestDTOs;
        }
        public List<ShowAuditRequestDTO> GetFinancialAuditRequestByStatus(SearchCriteriaDTO SearchCriteria)
        {
            List<ShowAuditRequestDTO> showFinancialAuditRequestDTOs = new List<ShowAuditRequestDTO>();

            foreach (var items in sharedSearchBLL.Search(SearchCriteria))

            {
                showFinancialAuditRequestDTOs.Add(new ShowAuditRequestDTO
                {
                    RequestID = items.FinancialAuditRequestID,
                    BatchAuditRequestID = items.BatchAuditRequestFK,
                    OfficerName = userBLL.getUserName(items.FinancialAuditAssigneeFK),
                    NumberOfApprovedClaims = items.NumberOfApprovedClaims,
                    NumberOfPendingClaims = items.NumberOfPendingClaims,
                    NumberOfRejectedClaims = items.NumberOfRejectedClaims,
                    Action = ""
                });
            }
            return showFinancialAuditRequestDTOs;
        }
        public List<ShowAuditRequestDTO> GetMIAuditRequestByStatus(SearchCriteriaDTO SearchCriteria)
        {
            List<ShowAuditRequestDTO> showMIAuditRequestDTOs = new List<ShowAuditRequestDTO>();

            foreach (var items in sharedSearchBLL.Search(SearchCriteria))

            {
                showMIAuditRequestDTOs.Add(new ShowAuditRequestDTO
                {
                    RequestID = items.MIAuditRequestID,
                    BatchAuditRequestID = items.BatchAuditRequestFK,
                    OfficerName = userBLL.getUserName(items.MIOfficerAssigneeFK),
                    NumberOfApprovedClaims = items.NumberOfApprovedClaims,
                    NumberOfPendingClaims = items.NumberOfPendingClaims,
                    NumberOfRejectedClaims = items.NumberOfRejectedClaims,
                    Action = ""
                });
            }
            return showMIAuditRequestDTOs;
        }
        public List<ShowAuditRequestDTO> GetReconciliationAuditRequestByStatus(SearchCriteriaDTO SearchCriteria)
        {
            List<ShowAuditRequestDTO> showReconciliationAuditRequestDTOs = new List<ShowAuditRequestDTO>();

            foreach (var items in sharedSearchBLL.Search(SearchCriteria))

            {
                showReconciliationAuditRequestDTOs.Add(new ShowAuditRequestDTO
                {
                    RequestID = items.ReconciliationAuditRequestID,
                    BatchAuditRequestID = items.BatchAuditRequestFK,
                    OfficerName = userBLL.getUserName(items.ReconciliationOfficerFK),
                    NumberOfApprovedClaims = items.NumberOfApprovedClaims,
                    NumberOfPendingClaims = items.NumberOfPendingClaims,
                    NumberOfRejectedClaims = items.NumberOfRejectedClaims,
                    Action = ""
                });
            }
            return showReconciliationAuditRequestDTOs;
        }
        public List<ShowDataEntryAdminstrationRequestDTO> GetDataEntryAdminstrationRequestByStatus(SearchCriteriaDTO SearchCriteria)
        {
            List<ShowDataEntryAdminstrationRequestDTO> showDataEntryAdminstrationRequestDTOs = new List<ShowDataEntryAdminstrationRequestDTO>();

            foreach(var items in sharedSearchBLL.Search(SearchCriteria))
            {
                showDataEntryAdminstrationRequestDTOs.Add(new ShowDataEntryAdminstrationRequestDTO
                {
                    RequestID = items.DataEntryAdminstrationRequestID,
                    BatchRequestID = items.BatchRequestFK,
                    DataEntryAdminAssignee = userBLL.getUserName(items.DataEntryAdminAssigneeFK),
                    ApprovedClaimsNumber = items.OriginalApprovedClaimsNumber,
                    Action = "<a class='btn btn-primary btn-xs' role='button' href=/DataEntry/AddDataEntryRequest?ID=" + items.DataEntryAdminstrationRequestID + ">Open</a>"
                });
            }
            return showDataEntryAdminstrationRequestDTOs;
        }
        public List<ShowDataEntryBatchAssigningRequestDTO> GetDataEntryBatchAssigningRequestByStatus(SearchCriteriaDTO SearchCriteria)
        {
            List<ShowDataEntryBatchAssigningRequestDTO> showDataEntryBatchAssigningRequestDTOs = new List<ShowDataEntryBatchAssigningRequestDTO>();

            foreach(var items in sharedSearchBLL.Search(SearchCriteria))
            {
                showDataEntryBatchAssigningRequestDTOs.Add(new ShowDataEntryBatchAssigningRequestDTO {
                    RequestID = items.DataEntryBatchAssigningRequestID,
                    AdministrationRequestID = items.DataEntryAdministrationRequestFK,
                    OfficerName = userBLL.getUserName(items.DataEntryOfficerFK),
                    AssignedClaimsNumber = items.AssignedClaimsNumber,
                    Action = "<a class='btn btn-primary btn-xs' role='button' href=/DataEntry/DataEntryActions?ID=" + items.DataEntryBatchAssigningRequestID + ">Open</a>"
                });
            }
            return showDataEntryBatchAssigningRequestDTOs;
        }
        public List<ShowBatchClosingRequestDTO> GetBatchClosingRequestByStatus(SearchCriteriaDTO SearchCriteria)
        {
            List<ShowBatchClosingRequestDTO> showBatchClosingRequestDTOs = new List<ShowBatchClosingRequestDTO>();

            foreach(var items in sharedSearchBLL.Search(SearchCriteria))
            {
                showBatchClosingRequestDTOs.Add(new ShowBatchClosingRequestDTO
                {
                    RequestID = items.BatchClosingRequestID,
                    AdminstrationRequestID = items.DataEntryAdminstrationRequestFK,
                    ClosingOfficerAssignee = userBLL.getUserName(items.ClosingOfficerAssigneeFK),
                    Action = "<a class='btn btn-primary btn-xs' role='button' href=/Closing/ClosingTeamRequestActions?ID=" + items.BatchClosingRequestID + ">Open</a>"
                });
            }
            return showBatchClosingRequestDTOs;
        }
        public List<ShowClosedBatchReAdministrationRequestDTO> GetClosedBatchReAdministrationRequestByStatus(SearchCriteriaDTO SearchCriteria)
        {
            List<ShowClosedBatchReAdministrationRequestDTO> showClosedBatchReAdministrationRequestDTOs = new List<ShowClosedBatchReAdministrationRequestDTO>();

            foreach(var items in sharedSearchBLL.Search(SearchCriteria))
            {
                showClosedBatchReAdministrationRequestDTOs.Add(new ShowClosedBatchReAdministrationRequestDTO
                {
                    RequestID = items.ClosedBatchReAdmissionRequestID,
                    BatchClosingRequestID = items.BatchClosingRequestFK,
                    OfficerAssignee = userBLL.getUserName(items.AssignedByAdminFK),
                    Action = "<a class='btn btn-primary btn-xs' role='button' href=/ReAdministartion/ClosingReAdministrationTeamRequestActions?ID=" + items.ClosedBatchReAdmissionRequestID + ">Open</a>"
                });
            }
            return showClosedBatchReAdministrationRequestDTOs;
        }
    }
}
