using BNC.BLL.Logic.Tables;
using BNC.BLL.StaticData;
using BNC.DTOs;
using BNC.DTOs.Business;
using BNC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static BNC.BLL.StaticData.StaticEnums;

namespace BNC.BLL.Logic.Shared_Logic
{
    public class SharedSearchBLL
    {
        ReceivingRequestBLL receivingRequestBLL;
        FilterationRequestBLL filterationRequestBLL;
        BatchingRequestBLL batchingRequestBLL;
        AuditFlowBatchDetailsBLL auditFlowBatchDetailsBLL;
        MedicalAuditRequestBLL medicalAuditRequestBLL;
        FinancialAuditRequestBLL financialAuditRequestBLL;
        MIAuditRequestBLL mIAuditRequestBLL;
        ReconciliationAuditRequestBLL reconciliationAuditRequestBLL;
        DataEntryAdminstrationRequestBLL dataEntryAdminstrationRequestBLL;
        DataEntryBatchAssigningRequestBLL dataEntryBatchAssigningRequestBLL;
        BatchClosingRequestBLL batchClosingRequestBLL;
        ClosedBatchReAdministrationRequestBLL closedBatchReAdministrationRequestBLL;
        FilterationRequestDetailsBLL filterationRequestDetailsBLL;
        LockLoggesBLL lockLoggesBLL; 
        ActionBLL actionBLL;
        BatchAuditingRequestBLL batchAuditingRequestBLL;
        BatchingFilterationDetailsBLL batchingFilterationDetailsBLL;
        DateTime creationDate;
        InsuranceSystemBLL insuranceSystemBLL;
        ProvidersBLL providersBLL;
        //BatchingFilterationDetailsBLL batchingFilterationDetailsBLL;
        MapperBLL mapperBLL;
        FilterationCategoriesBLL filterationCategoriesBLL;
        public SharedSearchBLL(BNCEntities Context, DateTime CreationDate)
        {
            receivingRequestBLL = new ReceivingRequestBLL(Context, CreationDate);
            filterationRequestBLL = new FilterationRequestBLL(Context, CreationDate);
            batchingRequestBLL = new BatchingRequestBLL(Context, CreationDate);
            auditFlowBatchDetailsBLL = new AuditFlowBatchDetailsBLL(Context, CreationDate);
            medicalAuditRequestBLL = new MedicalAuditRequestBLL(Context, CreationDate);
            financialAuditRequestBLL = new FinancialAuditRequestBLL(Context, CreationDate);
            mIAuditRequestBLL = new MIAuditRequestBLL(Context, CreationDate);
            reconciliationAuditRequestBLL = new ReconciliationAuditRequestBLL(Context, CreationDate);
            dataEntryAdminstrationRequestBLL = new DataEntryAdminstrationRequestBLL(Context, CreationDate);
            dataEntryBatchAssigningRequestBLL = new DataEntryBatchAssigningRequestBLL(Context, CreationDate);
            batchClosingRequestBLL = new BatchClosingRequestBLL(Context, CreationDate);
            closedBatchReAdministrationRequestBLL = new ClosedBatchReAdministrationRequestBLL(Context, CreationDate);
            filterationRequestDetailsBLL = new FilterationRequestDetailsBLL(Context, CreationDate);
            lockLoggesBLL = new LockLoggesBLL(Context, CreationDate);
            actionBLL = new ActionBLL(Context, CreationDate);
            providersBLL = new ProvidersBLL(Context, CreationDate);
            batchAuditingRequestBLL = new BatchAuditingRequestBLL(Context, CreationDate);
            insuranceSystemBLL = new InsuranceSystemBLL(Context, CreationDate);
            batchingFilterationDetailsBLL = new BatchingFilterationDetailsBLL(Context, CreationDate);
            mapperBLL = new MapperBLL(Context, CreationDate);
            filterationCategoriesBLL = new FilterationCategoriesBLL(Context, CreationDate);
            creationDate = CreationDate;
        }
        //public void AddLockLogge(SearchCriteriaDTO searchCriteriaDTO)
        //{
        //    LockLogge lockLogge = new LockLogge
        //    {
        //        ActionFK = searchCriteriaDTO.IsLocked ? (int)Actions.Locked : (int)Actions.UnLocked,
        //        UserFK = searchCriteriaDTO.UserID,
        //        SystemEntityFK = searchCriteriaDTO.TableID,
        //        EntityRecordID = searchCriteriaDTO.RecordID,
        //        IsActive = true,
        //        IsDeleted = false,
        //        CreationDate = creationDate
        //    };

        //    lockLoggesBLL.Add(lockLogge);
        //}
        //public void LockUnLockRequest(SearchCriteriaDTO searchCriteriaDTO)
        //{
        //    if (searchCriteriaDTO.TableID == (int)Entites.ReceivingRequest)
        //    {
        //        var Request = receivingRequestBLL.Find(x => x.ReceivingRequestID == searchCriteriaDTO.RecordID).FirstOrDefault();
        //        Request.IsLocked = searchCriteriaDTO.IsLocked;
        //        AddLockLogge(searchCriteriaDTO);

        //        receivingRequestBLL.Edit(Request);
        //    }
        //    else if (searchCriteriaDTO.TableID == (int)Entites.FilterationRequest)
        //    {
        //        var Request = filterationRequestBLL.Find(x => x.FilterationRequestID == searchCriteriaDTO.RecordID).FirstOrDefault();
        //        Request.IsLocked = searchCriteriaDTO.IsLocked;
        //        AddLockLogge(searchCriteriaDTO);

        //        filterationRequestBLL.Edit(Request);
        //    }
        //    else if (searchCriteriaDTO.TableID == (int)Entites.BatchingFilterationDetails)
        //    {
        //        var Request = batchingFilterationDetailsBLL.Find(x => x.BatchingFilterationDetailID == searchCriteriaDTO.RecordID).FirstOrDefault();
        //        Request.IsLocked = searchCriteriaDTO.IsLocked;
        //        AddLockLogge(searchCriteriaDTO);

        //        batchingFilterationDetailsBLL.Edit(Request);
        //    }
        //    else if (searchCriteriaDTO.TableID == (int)Entites.BatchRequest)
        //    {
        //        var Request = batchingRequestBLL.Find(x => x.BatchingRequestID == searchCriteriaDTO.RecordID).FirstOrDefault();
        //        Request.IsLocked = searchCriteriaDTO.IsLocked;
        //        AddLockLogge(searchCriteriaDTO);

        //        batchingRequestBLL.Edit(Request);
        //    }
        //    else if (searchCriteriaDTO.TableID == (int)Entites.AuditRequest)
        //    {
        //        var Request = batchAuditingRequestBLL.Find(x => x.BatchAuditingRequestID == searchCriteriaDTO.RecordID).FirstOrDefault();
        //        Request.IsLocked = searchCriteriaDTO.IsLocked;
        //        AddLockLogge(searchCriteriaDTO);

        //        batchAuditingRequestBLL.Edit(Request);
        //    }
        //    else if (searchCriteriaDTO.TableID == (int)Entites.DataEntryAdminstrationRequest)
        //    {
        //        var Request = dataEntryAdminstrationRequestBLL.Find(x => x.DataEntryAdminstrationRequestID == searchCriteriaDTO.RecordID).FirstOrDefault();
        //        Request.IsLocked = searchCriteriaDTO.IsLocked;
        //        AddLockLogge(searchCriteriaDTO);

        //        dataEntryAdminstrationRequestBLL.Edit(Request);
        //    }
        //    else if (searchCriteriaDTO.TableID == (int)Entites.DataEntryBatchAssigningRequest)
        //    {
        //        var Request = dataEntryBatchAssigningRequestBLL.Find(x => x.DataEntryBatchAssigningRequestID == searchCriteriaDTO.RecordID).FirstOrDefault();
        //        Request.IsLocked = searchCriteriaDTO.IsLocked;
        //        AddLockLogge(searchCriteriaDTO);

        //        dataEntryBatchAssigningRequestBLL.Edit(Request);
        //    }
        //    else if (searchCriteriaDTO.TableID == (int)Entites.BatchClosingRequest)
        //    {
        //        var Request = batchClosingRequestBLL.Find(x => x.BatchClosingRequestID == searchCriteriaDTO.RecordID).FirstOrDefault();
        //        Request.IsLocked = searchCriteriaDTO.IsLocked;
        //        AddLockLogge(searchCriteriaDTO);

        //        batchClosingRequestBLL.Edit(Request);
        //    }
        //    else if (searchCriteriaDTO.TableID == (int)Entites.ClosedBatchReAdministrationRequest)
        //    {
        //        var Request = closedBatchReAdministrationRequestBLL.Find(x => x.ClosedBatchReAdmissionRequestID == searchCriteriaDTO.RecordID).FirstOrDefault();
        //        Request.IsLocked = searchCriteriaDTO.IsLocked;
        //        AddLockLogge(searchCriteriaDTO);

        //        closedBatchReAdministrationRequestBLL.Edit(Request);
        //    }
        //}
        public IEnumerable<dynamic> Search(SearchCriteriaDTO searchCriteriaDTO)
        {
            if (searchCriteriaDTO.StartDate != null && searchCriteriaDTO.EndDate != null)
            {
                if (searchCriteriaDTO.StatusID != 0)
                    return SearchWithDates(searchCriteriaDTO);
                else if(searchCriteriaDTO.StatusID == 0)
                    return SearchWithDatesAndNoStatus(searchCriteriaDTO);
            }
            else if (searchCriteriaDTO.UserID == 0)
            {
                return SearchWithNoUserName(searchCriteriaDTO);
            }
            else if(searchCriteriaDTO.UserID != 0)
            {
                return SearchWithUserName(searchCriteriaDTO);
            }
            return null;
        }
        public IEnumerable<dynamic> SearchWithNoUserName(SearchCriteriaDTO searchCriteriaDTO)
        {
            if (searchCriteriaDTO.TableID == (int)Entity.ReceivingRequest)
                return receivingRequestBLL.CustomSelection(searchCriteriaDTO.FieldsNames, x =>  x.ReceivingStatusFK == searchCriteriaDTO.StatusID).ToList();

            else if (searchCriteriaDTO.TableID == (int)Entity.FilterationRequest)
                return filterationRequestBLL.CustomSelection(searchCriteriaDTO.FieldsNames, x => x.FilterationRequestStatusFK == searchCriteriaDTO.StatusID).ToList();

            else if (searchCriteriaDTO.TableID == (int)Entity.BatchRequest)
                return batchingRequestBLL.CustomSelection(searchCriteriaDTO.FieldsNames, x =>  x.StatusFK == searchCriteriaDTO.StatusID).ToList();

            else if (searchCriteriaDTO.TableID == (int)Entity.AuditRequest)
                return batchAuditingRequestBLL.CustomSelection(searchCriteriaDTO.FieldsNames, x => x.BatchAuditingStatusFK == searchCriteriaDTO.StatusID).ToList();

            else if (searchCriteriaDTO.TableID == (int)Entity.DataEntryAdminstrationRequest)
                return dataEntryAdminstrationRequestBLL.CustomSelection(searchCriteriaDTO.FieldsNames, x =>  x.DataEntryAdministrationStatusFK == searchCriteriaDTO.StatusID).ToList();

            else if (searchCriteriaDTO.TableID == (int)Entity.DataEntryBatchAssigningRequest)
                return dataEntryBatchAssigningRequestBLL.CustomSelection(searchCriteriaDTO.FieldsNames, x => x.DataEntryBatchAssigningStatusFK == searchCriteriaDTO.StatusID).ToList();

            else if (searchCriteriaDTO.TableID == (int)Entity.BatchClosingRequest)
                return batchClosingRequestBLL.CustomSelection(searchCriteriaDTO.FieldsNames, x => x.BatchClosingStatusFK == searchCriteriaDTO.StatusID).ToList();

            else if (searchCriteriaDTO.TableID == (int)Entity.ClosedBatchReAdministrationRequest)
                return closedBatchReAdministrationRequestBLL.CustomSelection(searchCriteriaDTO.FieldsNames, x => x.ReAdministrationStatusFK == searchCriteriaDTO.StatusID).ToList();

            else if (searchCriteriaDTO.TableID == (int)Entity.BatchingFilterationDetails)
                return batchingFilterationDetailsBLL.CustomSelection(searchCriteriaDTO.FieldsNames, x => x.BatchingFilterationDetailStatusFK == searchCriteriaDTO.StatusID).ToList();


            
            return null;
        }
        public IEnumerable<dynamic> SearchWithUserName(SearchCriteriaDTO searchCriteriaDTO)
        {
            if (searchCriteriaDTO.TableID == (int)Entity.ReceivingRequest)
                return receivingRequestBLL.CustomSelection(searchCriteriaDTO.FieldsNames, x => x.ReceivingOfficerFK == searchCriteriaDTO.UserID && x.ReceivingStatusFK == searchCriteriaDTO.StatusID).ToList();

            else if (searchCriteriaDTO.TableID == (int)Entity.FilterationRequest)
                return filterationRequestBLL.CustomSelection(searchCriteriaDTO.FieldsNames, x => x.FilterationRequestStatusFK == searchCriteriaDTO.StatusID).ToList();

            else if (searchCriteriaDTO.TableID == (int)Entity.BatchRequest)
                return batchingRequestBLL.CustomSelection(searchCriteriaDTO.FieldsNames, x => x.BatchingOfficerFK == searchCriteriaDTO.UserID && x.StatusFK == searchCriteriaDTO.StatusID).ToList();
           
            else if (searchCriteriaDTO.TableID == (int)Entity.DataEntryAdminstrationRequest)
                return dataEntryAdminstrationRequestBLL.CustomSelection(searchCriteriaDTO.FieldsNames, x => x.DataEntryAdminAssigneeFK == searchCriteriaDTO.UserID && x.DataEntryAdministrationStatusFK == searchCriteriaDTO.StatusID).ToList();

            else if (searchCriteriaDTO.TableID == (int)Entity.DataEntryBatchAssigningRequest)
                return dataEntryBatchAssigningRequestBLL.CustomSelection(searchCriteriaDTO.FieldsNames, x => x.DataEntryOfficerFK == searchCriteriaDTO.UserID && x.DataEntryBatchAssigningStatusFK == searchCriteriaDTO.StatusID).ToList();

            else if (searchCriteriaDTO.TableID == (int)Entity.BatchClosingRequest)
                return batchClosingRequestBLL.CustomSelection(searchCriteriaDTO.FieldsNames, x => x.ClosingOfficerAssigneeFK == searchCriteriaDTO.UserID && x.BatchClosingStatusFK == searchCriteriaDTO.StatusID).ToList();

            else if (searchCriteriaDTO.TableID == (int)Entity.ClosedBatchReAdministrationRequest)
                return closedBatchReAdministrationRequestBLL.CustomSelection(searchCriteriaDTO.FieldsNames, x => x.AssignedByAdminFK == searchCriteriaDTO.UserID && x.ReAdministrationStatusFK == searchCriteriaDTO.StatusID).ToList();

            return null;
        }
        public IEnumerable<dynamic> SearchWithDates(SearchCriteriaDTO searchCriteriaDTO)
        {
            TimeSpan CreationFromTime = new TimeSpan(0, 1, 0);
            TimeSpan CreationToTime = new TimeSpan(23, 59, 59);

            DateTime StartDate = DateTime.ParseExact(searchCriteriaDTO.StartDate, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date + CreationFromTime;
            DateTime EndDate = DateTime.ParseExact(searchCriteriaDTO.EndDate, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date + CreationToTime;

            if (searchCriteriaDTO.TableID == (int)Entity.ReceivingRequest)
                return receivingRequestBLL.CustomSelection(searchCriteriaDTO.FieldsNames, x => x.ReceivingStatusFK == searchCriteriaDTO.StatusID && x.CreationDate.CompareTo(StartDate) >= 0 && x.CreationDate.CompareTo(EndDate) <= 0).ToList();

            else if (searchCriteriaDTO.TableID == (int)Entity.FilterationRequest)
                return filterationRequestBLL.CustomSelection(searchCriteriaDTO.FieldsNames, x => x.FilterationRequestStatusFK == searchCriteriaDTO.StatusID && x.CreationDate.CompareTo(StartDate) >= 0 && x.CreationDate.CompareTo(EndDate) <= 0).ToList();

            else if (searchCriteriaDTO.TableID == (int)Entity.BatchRequest)
                return batchingRequestBLL.CustomSelection(searchCriteriaDTO.FieldsNames, x => x.StatusFK == searchCriteriaDTO.StatusID && x.CreationDate.CompareTo(StartDate) >= 0 && x.CreationDate.CompareTo(EndDate) <= 0).ToList();

            else if (searchCriteriaDTO.TableID == (int)Entity.AuditRequest)
                return batchAuditingRequestBLL.CustomSelection(searchCriteriaDTO.FieldsNames, x => x.BatchAuditingStatusFK == searchCriteriaDTO.StatusID && x.CreationDate.CompareTo(StartDate) >= 0 && x.CreationDate.CompareTo(EndDate) <= 0).ToList();

            else if (searchCriteriaDTO.TableID == (int)Entity.DataEntryAdminstrationRequest)
                return dataEntryAdminstrationRequestBLL.CustomSelection(searchCriteriaDTO.FieldsNames, x => x.DataEntryAdministrationStatusFK == searchCriteriaDTO.StatusID && x.CreationDate.CompareTo(StartDate) >= 0 && x.CreationDate.CompareTo(EndDate) <= 0).ToList();

            else if (searchCriteriaDTO.TableID == (int)Entity.DataEntryBatchAssigningRequest)
                return dataEntryBatchAssigningRequestBLL.CustomSelection(searchCriteriaDTO.FieldsNames, x => x.DataEntryBatchAssigningStatusFK == searchCriteriaDTO.StatusID && x.CreationDate.CompareTo(StartDate) >= 0 && x.CreationDate.CompareTo(EndDate) <= 0).ToList();

            else if (searchCriteriaDTO.TableID == (int)Entity.BatchClosingRequest)
                return batchClosingRequestBLL.CustomSelection(searchCriteriaDTO.FieldsNames, x => x.BatchClosingStatusFK == searchCriteriaDTO.StatusID && x.CreationDate.CompareTo(StartDate) >= 0 && x.CreationDate.CompareTo(EndDate) <= 0).ToList();

            else if (searchCriteriaDTO.TableID == (int)Entity.ClosedBatchReAdministrationRequest)
                return closedBatchReAdministrationRequestBLL.CustomSelection(searchCriteriaDTO.FieldsNames, x => x.ReAdministrationStatusFK == searchCriteriaDTO.StatusID && x.CreationDate.CompareTo(StartDate) >= 0 && x.CreationDate.CompareTo(EndDate) <= 0).ToList();

            return null;
        }
        public IEnumerable<dynamic> SearchWithDatesAndNoStatus(SearchCriteriaDTO searchCriteriaDTO)
        {
            TimeSpan CreationFromTime = new TimeSpan(0, 1, 0);
            TimeSpan CreationToTime = new TimeSpan(23, 59, 59);

            DateTime StartDate = DateTime.ParseExact(searchCriteriaDTO.StartDate, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date + CreationFromTime;
            DateTime EndDate = DateTime.ParseExact(searchCriteriaDTO.EndDate, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date + CreationToTime;

            if (searchCriteriaDTO.TableID == (int)Entity.ReceivingRequest)
                return receivingRequestBLL.CustomSelection(searchCriteriaDTO.FieldsNames, x => x.CreationDate.CompareTo(StartDate) >= 0 && x.CreationDate.CompareTo(EndDate) <= 0).ToList();

            else if (searchCriteriaDTO.TableID == (int)Entity.FilterationRequest)
                return filterationRequestBLL.CustomSelection(searchCriteriaDTO.FieldsNames, x => x.CreationDate.CompareTo(StartDate) >= 0 && x.CreationDate.CompareTo(EndDate) <= 0).ToList();

            else if (searchCriteriaDTO.TableID == (int)Entity.BatchRequest)
                return batchingRequestBLL.CustomSelection(searchCriteriaDTO.FieldsNames, x => x.CreationDate.CompareTo(StartDate) >= 0 && x.CreationDate.CompareTo(EndDate) <= 0).ToList();

            else if (searchCriteriaDTO.TableID == (int)Entity.AuditRequest)
                return batchAuditingRequestBLL.CustomSelection(searchCriteriaDTO.FieldsNames, x => x.CreationDate.CompareTo(StartDate) >= 0 && x.CreationDate.CompareTo(EndDate) <= 0).ToList();

            else if (searchCriteriaDTO.TableID == (int)Entity.MedicalAuditRequest)
                return medicalAuditRequestBLL.CustomSelection(searchCriteriaDTO.FieldsNames, x => x.CreationDate.CompareTo(StartDate) >= 0 && x.CreationDate.CompareTo(EndDate) <= 0).ToList();

            else if (searchCriteriaDTO.TableID == (int)Entity.FinancialAuditRequest)
                return financialAuditRequestBLL.CustomSelection(searchCriteriaDTO.FieldsNames, x => x.CreationDate.CompareTo(StartDate) >= 0 && x.CreationDate.CompareTo(EndDate) <= 0).ToList();

            else if (searchCriteriaDTO.TableID == (int)Entity.MIAuditRequest)
                return mIAuditRequestBLL.CustomSelection(searchCriteriaDTO.FieldsNames, x => x.CreationDate.CompareTo(StartDate) >= 0 && x.CreationDate.CompareTo(EndDate) <= 0).ToList();

            else if (searchCriteriaDTO.TableID == (int)Entity.ReconciliationAuditRequest)
                return reconciliationAuditRequestBLL.CustomSelection(searchCriteriaDTO.FieldsNames, x => x.CreationDate.CompareTo(StartDate) >= 0 && x.CreationDate.CompareTo(EndDate) <= 0).ToList();

            else if (searchCriteriaDTO.TableID == (int)Entity.DataEntryAdminstrationRequest)
                return dataEntryAdminstrationRequestBLL.CustomSelection(searchCriteriaDTO.FieldsNames, x => x.CreationDate.CompareTo(StartDate) >= 0 && x.CreationDate.CompareTo(EndDate) <= 0).ToList();

            else if (searchCriteriaDTO.TableID == (int)Entity.DataEntryBatchAssigningRequest)
                return dataEntryBatchAssigningRequestBLL.CustomSelection(searchCriteriaDTO.FieldsNames, x => x.CreationDate.CompareTo(StartDate) >= 0 && x.CreationDate.CompareTo(EndDate) <= 0).ToList();

            else if (searchCriteriaDTO.TableID == (int)Entity.BatchClosingRequest)
                return batchClosingRequestBLL.CustomSelection(searchCriteriaDTO.FieldsNames, x => x.CreationDate.CompareTo(StartDate) >= 0 && x.CreationDate.CompareTo(EndDate) <= 0).ToList();

            else if (searchCriteriaDTO.TableID == (int)Entity.ClosedBatchReAdministrationRequest)
                return closedBatchReAdministrationRequestBLL.CustomSelection(searchCriteriaDTO.FieldsNames, x => x.CreationDate.CompareTo(StartDate) >= 0 && x.CreationDate.CompareTo(EndDate) <= 0).ToList();

            return null;
        }
        public List<DataEntryAdminstrationRequestDTO> GetMyDataEntryAdminstrationUnFinishedRequests(SearchCriteriaDTO searchCriteriaDTO)
        {
            List<DataEntryAdminstrationRequestDTO> dataEntryAdminstrationRequestDTOs = new List<DataEntryAdminstrationRequestDTO>();

            foreach (var items in Search(searchCriteriaDTO).ToList().Where(x => x.RemainingUnassignedNumberOfClaims != 0))
            {
                dataEntryAdminstrationRequestDTOs.Add(new DataEntryAdminstrationRequestDTO {
                    DataEntryAdminstrationRequestID = items.DataEntryAdminstrationRequestID,
                    BatchRequestFK = items.BatchRequestFK,
                    RemainingUnassignedNumberOfClaims = items.RemainingUnassignedNumberOfClaims
                });
            }
            return dataEntryAdminstrationRequestDTOs;
        }
        public List<DataEntryAdminstrationRequestDTO> GetMyDataEntryAdminstrationRequests(SearchCriteriaDTO searchCriteriaDTO)
        {
            List<DataEntryAdminstrationRequestDTO> dataEntryAdminstrationRequestDTOs = new List<DataEntryAdminstrationRequestDTO>();

            foreach (var items in Search(searchCriteriaDTO).ToList())
            {
                dataEntryAdminstrationRequestDTOs.Add(new DataEntryAdminstrationRequestDTO
                {
                    DataEntryAdminstrationRequestID = items.DataEntryAdminstrationRequestID,
                    BatchRequestFK = items.BatchRequestFK,
                    RemainingUnassignedNumberOfClaims = items.RemainingUnassignedNumberOfClaims
                });
            }
            return dataEntryAdminstrationRequestDTOs;
        }
        public List<BatchClosingRequestDTO> GetMyBatchClosingRequest(SearchCriteriaDTO searchCriteriaDTO)
        {
            List<BatchClosingRequestDTO> batchClosingRequestDTOs = new List<BatchClosingRequestDTO>();

            foreach (var items in Search(searchCriteriaDTO).ToList())
            {
                int requestID = items.DataEntryAdminstrationRequestFK;
                batchClosingRequestDTOs.Add(new BatchClosingRequestDTO
                {                   
                    BatchClosingRequestID = Convert.ToInt32(items.BatchClosingRequestID),
                    BatchClosingStatusName = ((StaticEnums.Status)Enum.Parse(typeof(StaticEnums.Status), items.BatchClosingStatusFK.ToString())).ToString(),
                    TotalClaimsCount = dataEntryAdminstrationRequestBLL.Find(x => x.DataEntryAdminstrationRequestID == requestID).FirstOrDefault().OriginalApprovedClaimsNumber.Value,
                    ConfirmReceivingTime = items.ConfirmReceivingTime,
                    FinishedReviewingTime = items.FinishedReviewingTime
                });
            }
            return batchClosingRequestDTOs;
        }
        public List<ReceivingRequestDTO> GetFilterationRequestsByStatus(SearchCriteriaDTO searchCriteriaDTO)
        {
            List<ReceivingRequestDTO> ReceivingRequests = new List<ReceivingRequestDTO>();
            List<int> ReceivingRequestsIDs = Search(searchCriteriaDTO).Select(x => x.ReceivingRequestFK).OfType<int>().ToList();
            foreach (var item in ReceivingRequestsIDs)
            {
                var RequestItem =receivingRequestBLL.Find(x => x.ReceivingRequestID == item).FirstOrDefault();
                ReceivingRequests.Add(new ReceivingRequestDTO
                {
                    BillingYear = RequestItem.BillingYear,
                    BilllingMonth = RequestItem.BilllingMonth,
                    ClaimsCount = RequestItem.ReceivingOfficerCalimsCount,
                    ProviderID = RequestItem.ProviderFK,
                    ProviderName = providersBLL.Find(x => x.ProviderID == RequestItem.ProviderFK).FirstOrDefault().ProviderEnglishName,
                    ReceivingDate = RequestItem.ReceivingDate,
                    ReceivingRequestID = RequestItem.ReceivingRequestID
                });

            }
            return ReceivingRequests;
        }
        public List<BatchClosingReAdministrationRequestDTO> GetMyBatchClosingReAdministrationRequests(SearchCriteriaDTO searchCriteriaDTO)
        {
            List<BatchClosingReAdministrationRequestDTO> batchClosingReAdministrationRequestDTOs = new List<BatchClosingReAdministrationRequestDTO>();

            foreach (var items in Search(searchCriteriaDTO).ToList())
            {
                //int requestID = items.DataEntryAdminstrationRequestFK;
                batchClosingReAdministrationRequestDTOs.Add(new BatchClosingReAdministrationRequestDTO
                {
                    BatchClosingReAdministrationRequestID = items.ClosedBatchReAdmissionRequestID,
                    ReAdministrationStatusName = ((StaticEnums.Status)Enum.Parse(typeof(StaticEnums.Status), items.ReAdministrationStatusFK.ToString())).ToString(),
                    TotalClaimsCount = closedBatchReAdministrationRequestBLL.GetTotalClaimsCountInBatchClosingReAdministrationRequest(items.BatchClosingRequestFK),
                    ReceivedFromClosingOn = items.ConfirmedReceivingOn,
                    FinalClosureTime = items.FinalClosureTime
                });
            }
            return batchClosingReAdministrationRequestDTOs;
        }
        public List<DataEntryBatchAssigningRequestDTO> GetMyDataEntryBatchAssigningRequest(SearchCriteriaDTO searchCriteriaDTO)
        {
            List<DataEntryBatchAssigningRequestDTO> dataEntryBatchAssigningRequestDTOs = new List<DataEntryBatchAssigningRequestDTO>();

            foreach (var items in Search(searchCriteriaDTO).ToList())
            {
                dataEntryBatchAssigningRequestDTOs.Add(new DataEntryBatchAssigningRequestDTO
                {
                    DataEntryBatchAssigningRequestID = items.DataEntryBatchAssigningRequestID,
                    DataEntryBatchAssigningStatusName = ((StaticEnums.Status)Enum.Parse(typeof(StaticEnums.Status), items.DataEntryBatchAssigningStatusFK.ToString())).ToString(),
                    DataEntryAdministrationRequestFK = items.DataEntryAdministrationRequestFK,
                    AssignedClaimsNumber = items.AssignedClaimsNumber,
                    ConfirmRecievingByOfficerTime = items.ConfirmRecievingByOfficerTime,
                    DataEntryOfficerFinishedOnSystemTime = items.DataEntryOfficerFinishedOnSystemTime
                });
            }
            return dataEntryBatchAssigningRequestDTOs;
        }
        public void LockRequest(SearchCriteriaDTO searchCriteriaDTO)
        {
            int recordID = 0;
            if (searchCriteriaDTO.TableID == (int)Entity.ReceivingRequest)
            {
                var receivingRequest = receivingRequestBLL.Find(x => x.ReceivingRequestID == searchCriteriaDTO.RecordID).FirstOrDefault();
                recordID = receivingRequest.ReceivingRequestID;
                receivingRequest.IsLocked = searchCriteriaDTO.IsLocked;
                receivingRequest.ModificationDate = creationDate;

                receivingRequestBLL.Edit(receivingRequest);
            }
            else if (searchCriteriaDTO.TableID == (int)Entity.FilterationRequest)
            {
                var filterationRequestDetails = filterationRequestDetailsBLL.Find(x => x.FilterationRequestDetailID == searchCriteriaDTO.RecordID).FirstOrDefault();
                recordID = filterationRequestDetails.FilterationRequestDetailID;
                filterationRequestDetails.IsLocked = searchCriteriaDTO.IsLocked;
                filterationRequestDetails.ModificationDate = creationDate;

                filterationRequestDetailsBLL.Edit(filterationRequestDetails);
            }              
            else if (searchCriteriaDTO.TableID == (int)Entity.MedicalAuditRequest)
            {
                var medicalAuditRequest = medicalAuditRequestBLL.Find(x => x.MedicalAuditRequestID == searchCriteriaDTO.RecordID).FirstOrDefault();
                recordID = medicalAuditRequest.MedicalAuditRequestID;
                medicalAuditRequest.IsLocked = searchCriteriaDTO.IsLocked;
                medicalAuditRequest.ModificationDate = creationDate;

                medicalAuditRequestBLL.Edit(medicalAuditRequest);
            }
            else if (searchCriteriaDTO.TableID == (int)Entity.FinancialAuditRequest)
            {
                var financialAuditRequest = financialAuditRequestBLL.Find(x => x.FinancialAuditRequestID == searchCriteriaDTO.RecordID).FirstOrDefault();
                recordID = financialAuditRequest.FinancialAuditRequestID;
                financialAuditRequest.IsLocked = searchCriteriaDTO.IsLocked;
                financialAuditRequest.ModificationDate = creationDate;

                financialAuditRequestBLL.Edit(financialAuditRequest);
            }
            else if (searchCriteriaDTO.TableID == (int)Entity.MIAuditRequest)
            {
                var mIAuditRequest = mIAuditRequestBLL.Find(x => x.MIAuditRequestID == searchCriteriaDTO.RecordID).FirstOrDefault();
                recordID = mIAuditRequest.MIAuditRequestID;
                mIAuditRequest.IsLocked = searchCriteriaDTO.IsLocked;
                mIAuditRequest.ModificationDate = creationDate;

                mIAuditRequestBLL.Edit(mIAuditRequest);
            }
            else if (searchCriteriaDTO.TableID == (int)Entity.ReconciliationAuditRequest)
            {
                var reconciliationAuditRequest = reconciliationAuditRequestBLL.Find(x => x.ReconciliationAuditRequestID == searchCriteriaDTO.RecordID).FirstOrDefault();
                recordID = reconciliationAuditRequest.ReconciliationAuditRequestID;
                reconciliationAuditRequest.IsLocked = searchCriteriaDTO.IsLocked;
                reconciliationAuditRequest.ModificationDate = creationDate;

                reconciliationAuditRequestBLL.Edit(reconciliationAuditRequest);
            }
            else if (searchCriteriaDTO.TableID == (int)Entity.DataEntryAdminstrationRequest)
            {
                var dataEntryAdminstrationRequest = dataEntryAdminstrationRequestBLL.Find(x => x.DataEntryAdminstrationRequestID == searchCriteriaDTO.RecordID).FirstOrDefault();
                recordID = dataEntryAdminstrationRequest.DataEntryAdminstrationRequestID;
                dataEntryAdminstrationRequest.IsLocked = searchCriteriaDTO.IsLocked;
                dataEntryAdminstrationRequest.ModificationDate = creationDate;

                dataEntryAdminstrationRequestBLL.Edit(dataEntryAdminstrationRequest);
            }
            else if (searchCriteriaDTO.TableID == (int)Entity.DataEntryBatchAssigningRequest)
            {
                var dataEntryBatchAssigningRequest = dataEntryBatchAssigningRequestBLL.Find(x => x.DataEntryBatchAssigningRequestID == searchCriteriaDTO.RecordID).FirstOrDefault();
                recordID = dataEntryBatchAssigningRequest.DataEntryBatchAssigningRequestID;
                dataEntryBatchAssigningRequest.IsLocked = searchCriteriaDTO.IsLocked;
                dataEntryBatchAssigningRequest.ModificationDate = creationDate;

                dataEntryBatchAssigningRequestBLL.Edit(dataEntryBatchAssigningRequest);
            }            
            else if (searchCriteriaDTO.TableID == (int)Entity.BatchClosingRequest)
            {
                var batchClosingRequest = batchClosingRequestBLL.Find(x => x.BatchClosingRequestID == searchCriteriaDTO.RecordID).FirstOrDefault();
                recordID = batchClosingRequest.BatchClosingRequestID;
                batchClosingRequest.IsLocked = searchCriteriaDTO.IsLocked;
                batchClosingRequest.ModificationDate = creationDate;

                batchClosingRequestBLL.Edit(batchClosingRequest);
            }
            else if (searchCriteriaDTO.TableID == (int)Entity.ClosedBatchReAdministrationRequest)
            {
                var closedBatchReAdministrationRequest = closedBatchReAdministrationRequestBLL.Find(x => x.ClosedBatchReAdmissionRequestID == searchCriteriaDTO.RecordID).FirstOrDefault();
                recordID = closedBatchReAdministrationRequest.ClosedBatchReAdmissionRequestID;
                closedBatchReAdministrationRequest.IsLocked = searchCriteriaDTO.IsLocked;
                closedBatchReAdministrationRequest.ModificationDate = creationDate;

                closedBatchReAdministrationRequestBLL.Edit(closedBatchReAdministrationRequest);
            }

            LockLogge lockLogge = new LockLogge {
                EntityRecordID = recordID,
                SystemEntityFK = searchCriteriaDTO.TableID,
                ActionFK = searchCriteriaDTO.IsLocked ? (int)Actions.Locked : (int)Actions.UnLocked,    
                UserFK = searchCriteriaDTO.UserID,
                IsActive = true,
                IsDeleted = false,
                CreationDate = creationDate
            };
            lockLoggesBLL.Add(lockLogge);
        }
        public List<ReceivingRequestDTO> GetRecievingRequestsByStatus(SearchCriteriaDTO SearchCriteria)
        {
            List<ReceivingRequestDTO> ReceivingRequests = new List<ReceivingRequestDTO>();
            foreach (var item in Search(SearchCriteria))
            {
                int ProviderID = item.ProviderFK;
                ReceivingRequests.Add(new ReceivingRequestDTO
                {
                    BillingYear = item.BillingYear,
                    BilllingMonth = item.BilllingMonth,
                    ClaimsCount = item.ReceivingOfficerCalimsCount,
                    ProviderID = item.ProviderFK,
                    ReceivingDate = item.ReceivingDate,
                    ReceivingRequestID = item.ReceivingRequestID,
                    ProviderName = providersBLL.Find(x => x.ProviderID == ProviderID).FirstOrDefault().ProviderEnglishName

                });

            }
            return ReceivingRequests;
        }

        public List<BatchingRequestDTO> GetBatchingRequestsByStatus(SearchCriteriaDTO SearchCriteria)
        {
            List<BatchingRequestDTO> BatchingRequests = new List<BatchingRequestDTO>();
            foreach (var item in Search(SearchCriteria))
            {
                int BatchSystemID = (int)item.BatchSystemFK;
                BatchingRequests.Add(new BatchingRequestDTO
                {
                    BatchingRequestID = item.BatchingRequestID,
                    NumberOfBatchClaims = item.NumberOfBatchClaims,
                   // BatchSystemFK =item.BatchSystemFK,
                    BatchNumber = item.BatchNumber,
                    //BatchingOfficerFK = item.BatchingOfficerFK,
                    //BatchingOfficerComment = item.BatchingOfficerComment,
                    //FilterationCategoryFK = item.FilterationCategoryFK,
                    //TransferredToAuditDate = item.TransferredToAuditDate,//null
                    CreationDate = item.CreationDate,
                    BatchSystemName= insuranceSystemBLL.Find(x => x.SystemID== BatchSystemID).FirstOrDefault().SystemName
                });
                
            }
            return BatchingRequests;
        }

        public List<BatchingFilterationDetailDTO> GetBatchingFilterationDetailsByStatus(SearchCriteriaDTO SearchCriteria)
        {
            List<BatchingFilterationDetailDTO> batchingFilterationDetailsList = new List<BatchingFilterationDetailDTO>();
            foreach (var item in Search(SearchCriteria))
            {
                int CategoryID = item.FilterationCategoryFK;
                batchingFilterationDetailsList.Add(
                new BatchingFilterationDetailDTO
                {
                    BatchingFilterationDetailID = item.BatchingFilterationDetailID,
                    FilterationRequestID = item.FilterationRequestFK,
                    FilterationCategoryID = item.FilterationCategoryFK,
                    TotalClaimsCount = item.TotalClaimsCount,
                    RemainingClaimsCount = item.RemainingClaimsCount,
                    CreationDate = item.CreationDate,
                    CategoryName= filterationCategoriesBLL.Find(x=>x.FilterationCategoriesID== CategoryID).FirstOrDefault().FilterationCategoryName
                });

            }
            return batchingFilterationDetailsList;
        }
    }

}
