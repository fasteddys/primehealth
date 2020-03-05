using BNC.BLL.Logic.Tables;
using BNC.DTOs.Business;
using BNC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static BNC.BLL.StaticData.StaticEnums;
using BNC.DTOs;
using BNC.BLL.StaticData;

namespace BNC.BLL.Logic.Shared_Logic
{
    public class SharedBncBLL
    {
        DateTime creationDate;
        ReceivingRequestBLL receivingRequestBLL;
        BatchingRequestBLL batchingRequestBLL;
        MedicalAuditRequestBLL medicalAuditRequestBLL;
        FinancialAuditRequestBLL financialAuditRequestBLL;
        MIAuditRequestBLL mIAuditRequestBLL;
        ReconciliationAuditRequestBLL reconciliationAuditRequestBLL;
        BatchAuditingRequestBLL batchAuditingRequestBLL;
        DataEntryAdminstrationRequestBLL dataEntryAdminstrationRequestBLL;
        DataEntryBatchAssigningRequestBLL dataEntryBatchAssigningRequestBLL;
        ClosedBatchReAdministrationRequestBLL closedBatchReAdministrationRequestBLL;
        BatchClosingRequestBLL batchClosingRequestBLL;
        UserBLL userBLL;
        InsuranceSystemBLL insuranceSystemBLL;
        FilterationCategoriesBLL filterationCategoriesBLL;
        AuditFlowBatchDetailsBLL auditFlowBatchDetailsBLL;
        AuditCategoryBLL auditCategoryBLL;
        FilterationRequestBLL filterationRequestBLL;
        BatchingFilterationDetailsBLL batchingFilterationDetailsBLL;
        ProvidersBLL providersBLL;
        StatusBLL statusBLL;
        public SharedBncBLL(BNCEntities Context, DateTime CreationDate)
        {
            creationDate = CreationDate;
            batchingRequestBLL = new BatchingRequestBLL(Context, CreationDate);
            batchAuditingRequestBLL = new BatchAuditingRequestBLL(Context, CreationDate);
            medicalAuditRequestBLL = new MedicalAuditRequestBLL(Context, CreationDate);
            financialAuditRequestBLL = new FinancialAuditRequestBLL(Context, CreationDate);
            mIAuditRequestBLL = new MIAuditRequestBLL(Context, CreationDate);
            reconciliationAuditRequestBLL = new ReconciliationAuditRequestBLL(Context, CreationDate);
            dataEntryAdminstrationRequestBLL = new DataEntryAdminstrationRequestBLL(Context, CreationDate);
            dataEntryBatchAssigningRequestBLL = new DataEntryBatchAssigningRequestBLL(Context, CreationDate);
            closedBatchReAdministrationRequestBLL = new ClosedBatchReAdministrationRequestBLL(Context, CreationDate);
            receivingRequestBLL = new ReceivingRequestBLL(Context, CreationDate);
            batchClosingRequestBLL=new BatchClosingRequestBLL(Context, CreationDate);
            userBLL = new UserBLL(Context, CreationDate);
            insuranceSystemBLL = new InsuranceSystemBLL(Context, CreationDate);
            auditFlowBatchDetailsBLL = new AuditFlowBatchDetailsBLL(Context, CreationDate);
            filterationCategoriesBLL = new FilterationCategoriesBLL(Context, CreationDate);
            auditCategoryBLL = new AuditCategoryBLL(Context, CreationDate);
            batchingFilterationDetailsBLL = new BatchingFilterationDetailsBLL(Context, CreationDate);
            filterationRequestBLL = new FilterationRequestBLL(Context, CreationDate);
            providersBLL=new ProvidersBLL(Context, CreationDate);
            statusBLL = new StatusBLL(Context, CreationDate);
        }
        //--------------------------------------------------------------------------------------
        //1 get lifeCycle For Batch by BatchingRequestID or BatchNumber
        #region 
        //apis function
        public LifeCycleBatchtRequestDTO getBatchRequestLifeCycleByBatchId(int BatchingRequestID)
        {
            return getBatchRequestLifeCycle(getBatchRequestById(BatchingRequestID));
        }
        //api function
        public LifeCycleBatchtRequestDTO getBatchRequestLifeCycleByBatchNumber(string BatchNumber, int BatchSystemFK)
        {
            return getBatchRequestLifeCycle(getBatchRequestByBatchNumber(BatchNumber, BatchSystemFK));
        }
        public LifeCycleBatchtRequestDTO getBatchRequestLifeCycle(BatchingRequestDTO batchRequest)
        {

            LifeCycleBatchtRequestDTO newLifeCycleRequestDTO = new LifeCycleBatchtRequestDTO();
            List<CategoryAuditDTO> CategoryAuditList = new List<CategoryAuditDTO>();
            //1)
            if (batchRequest != null)
            {
                int BatchingRequestID = batchRequest.BatchingRequestID;
                newLifeCycleRequestDTO.BatchingRequest = batchRequest;//1)
                //--------------------------------------------------------------------------------------
                //2)
                var batchAuditRequest = getBatchAuditingRequest(BatchingRequestID);
                if (batchAuditRequest != null)
                {
                    newLifeCycleRequestDTO.BatchAuditingRequest = batchAuditRequest;//2)
                    int BatchAuditingRequestID = batchAuditRequest.BatchAuditingRequestID;
                    //--------------------------------------------------------------------------------------
                    var MedicalAuditRequest = getMedicalAuditRequest(BatchAuditingRequestID);//3)
                    var FinancialAuditRequest = getFinancialAuditRequest(BatchAuditingRequestID);//4)
                    var MIAuditRequest = getMIAuditRequest(BatchAuditingRequestID);//5)
                    var ReconciliationAuditRequest = getReconciliationAuditRequest(BatchAuditingRequestID);//6)
                    //--------------------------------------------------------------------------------------
                    if (MedicalAuditRequest != null)
                    {
                        newLifeCycleRequestDTO.MedicalAuditRequest = MedicalAuditRequest;//3)
                        CategoryAuditList.Add(MedicalAuditRequest);

                    }
                    if (FinancialAuditRequest != null)
                    {
                        newLifeCycleRequestDTO.FinancialAuditRequest = FinancialAuditRequest;//4)
                        CategoryAuditList.Add(FinancialAuditRequest);

                    }
                    if (MIAuditRequest != null)
                    {
                        newLifeCycleRequestDTO.MIAuditRequest = MIAuditRequest;//5)
                        CategoryAuditList.Add(MIAuditRequest);
                    }
                    if (ReconciliationAuditRequest != null)
                    {
                        newLifeCycleRequestDTO.ReconciliationAuditRequest = ReconciliationAuditRequest;//6)
                        CategoryAuditList.Add(ReconciliationAuditRequest);
                    }
                    //--------------------------------------------------------------------------------------
                    if (CategoryAuditList.Count > 0)//order categeory Audit based on order number medical,finincial,Misr,Reconsilation
                    {
                        CategoryAuditList = CategoryAuditList.OrderBy(cA => cA.AuditOrder).ToList();
                        newLifeCycleRequestDTO.CategoryAuditList = CategoryAuditList;
                    }

                }
                var dataEntryAdminstrationRequest = getDataEntryAdminstrationRequest(BatchingRequestID);//7)
                if (dataEntryAdminstrationRequest != null)
                {
                    newLifeCycleRequestDTO.DataEntryAdminstrationRequest = dataEntryAdminstrationRequest;//7)
                    //--------------------------------------------------------------------------------------
                    int DataEntryAdminstrationRequestID = dataEntryAdminstrationRequest.DataEntryAdminstrationRequestID;
                    var DataEntryBatchAssigningRequest = getDataEntryBatchAssigningRequest(DataEntryAdminstrationRequestID);//8)
                    if (DataEntryBatchAssigningRequest != null)
                    {
                        newLifeCycleRequestDTO.DataEntryBatchAssigningRequestList = DataEntryBatchAssigningRequest;//8)
                    }
                    var DataEntryBatchClosingRequest = getDataEntryBatchClosingRequest(DataEntryAdminstrationRequestID);//9)
                    if (DataEntryBatchClosingRequest != null)
                    {
                        newLifeCycleRequestDTO.BatchClosingRequest = DataEntryBatchClosingRequest;//9)
                        var DataEntryBatchClosingReAdministrationRequest = getDataEntryBatchClosingReAdministrationRequest(DataEntryBatchClosingRequest.BatchClosingRequestID);
                        if (DataEntryBatchClosingReAdministrationRequest != null)
                        {
                            newLifeCycleRequestDTO.BatchClosingReAdministrationRequest = DataEntryBatchClosingReAdministrationRequest;
                        }
                    }
                }

            }
            return newLifeCycleRequestDTO;
            //-------------------------------------------------------------------------------
        }
        //--------------------------------------------------------------------------------------
        //1 get Batch Request
        public BatchingRequestDTO getBatchRequestByBatchNumber(string BatchNumber, int BatchSystemFK)
        {
            BatchingRequest BatchingRequest = batchingRequestBLL.Find(br => br.BatchNumber == BatchNumber && br.BatchSystemFK == BatchSystemFK).FirstOrDefault();
            if(BatchingRequest!=null)
            {
                return new BatchingRequestDTO()
                {
                    BatchingRequestID = BatchingRequest.BatchingRequestID,
                    NumberOfBatchClaims = BatchingRequest.NumberOfBatchClaims,
                    BatchSystemFK = BatchingRequest.BatchSystemFK,
                    BatchSystemName = insuranceSystemBLL.getInsuranceSystemName(BatchingRequest.BatchSystemFK),
                    BatchNumber = BatchingRequest.BatchNumber,
                    BatchingOfficerFK = BatchingRequest.BatchingOfficerFK,
                    OfficerName = userBLL.getUserName(BatchingRequest.BatchingOfficerFK),
                    BatchingOfficerComment = BatchingRequest.BatchingOfficerComment,
                    FilterationCategoryFK = BatchingRequest.FilterationCategoryFK,
                    FilterationCategoryName = filterationCategoriesBLL.getFilterationCategoryName((int)BatchingRequest.FilterationCategoryFK),
                    TransferredToAuditDate = BatchingRequest.TransferredToAuditDate,//null
                    CreationDate = BatchingRequest.CreationDate,

                };
            }
            return null;
       
        }
        public BatchingRequestDTO getBatchRequestById(int BatchingRequestID)
        {
            BatchingRequest BatchingRequest = batchingRequestBLL.Find(br => br.BatchingRequestID== BatchingRequestID).FirstOrDefault();
            if (BatchingRequest != null)
            {
                return new BatchingRequestDTO()
                {
                    BatchingRequestID = BatchingRequest.BatchingRequestID,
                    NumberOfBatchClaims = BatchingRequest.NumberOfBatchClaims,
                    BatchSystemFK = BatchingRequest.BatchSystemFK,
                    BatchSystemName = insuranceSystemBLL.getInsuranceSystemName(BatchingRequest.BatchSystemFK),
                    BatchNumber = BatchingRequest.BatchNumber,
                    BatchingOfficerFK = BatchingRequest.BatchingOfficerFK,
                    OfficerName = userBLL.getUserName(BatchingRequest.BatchingOfficerFK),
                    BatchingOfficerComment = BatchingRequest.BatchingOfficerComment,
                    FilterationCategoryFK = BatchingRequest.FilterationCategoryFK,
                    FilterationCategoryName = filterationCategoriesBLL.getFilterationCategoryName((int)BatchingRequest.FilterationCategoryFK),
                    TransferredToAuditDate = BatchingRequest.TransferredToAuditDate,//null
                    CreationDate = BatchingRequest.CreationDate,

                };
            }
            return null;

        }
        //2 get Batch Auditing Request
        public BatchAuditingRequestDTO getBatchAuditingRequest(int BatchingRequestID)
        {
            BatchAuditingRequest BatchAuditingRequest= batchAuditingRequestBLL.Find(ba => ba.BatchingRequestFK == BatchingRequestID).FirstOrDefault();
            if(BatchAuditingRequest!=null)
            {
                return new BatchAuditingRequestDTO()
                {
                    BatchAuditingRequestID = BatchAuditingRequest.BatchAuditingRequestID,
                    NumberOfAuditingBatchClaims = BatchAuditingRequest.NumberOfAuditingBatchClaims,
                    TotalNumberOfApprovedClaims= BatchAuditingRequest.TotalNumberOfApprovedClaims,
                    TotalNumberOfRejectedClaims = BatchAuditingRequest.TotalNumberOfRejectedClaims,
                    TransferToDataEntryOn= BatchAuditingRequest.TransferToDataEntryOn,//null
                    CreationDate= BatchAuditingRequest.CreationDate,
                };
            }
            return null;
        }
        //3) get Medical Audit Request
        public CategoryAuditDTO getMedicalAuditRequest(int BatchAuditingRequestID)
        {
            MedicalAuditRequest MedicalAuditRequest = medicalAuditRequestBLL.Find(mr => mr.BatchAuditRequestFK == BatchAuditingRequestID).FirstOrDefault();
            if(MedicalAuditRequest!=null)
            {
                return new CategoryAuditDTO()
                {
                    CategoryAuditRequestID = MedicalAuditRequest.MedicalAuditRequestID,
                    NumberOfApprovedClaims = MedicalAuditRequest.NumberOfApprovedClaims,
                    ApprovedClaimsAmount = MedicalAuditRequest.ApprovedClaimsAmount,
                    NumberOfPendingClaims = MedicalAuditRequest.NumberOfPendingClaims,
                    PendingClaimsAmount = MedicalAuditRequest.PendingClaimsAmount,
                    NumberOfRejectedClaims = MedicalAuditRequest.NumberOfRejectedClaims,
                    RejectedClaimsAmount = MedicalAuditRequest.RejectedClaimsAmount,
                    CategoryAuditOfficerComment = MedicalAuditRequest.MedicalAuditOfficerComment,
                    CategoryAuditOfficerFK = MedicalAuditRequest.MedicalOfficerAssigneeFK,
                    OfficerName = userBLL.getUserName(MedicalAuditRequest.MedicalOfficerAssigneeFK),
                    AssignedTime= MedicalAuditRequest.AssignedTime,
                    CompilationDate= auditFlowBatchDetailsBLL.getAuditCategoryCompilationDate(BatchAuditingRequestID,(int)StaticEnums.AuditCategories.Medical),
                    AuditOrder=auditFlowBatchDetailsBLL.getAuditOrder(BatchAuditingRequestID, (int)StaticEnums.AuditCategories.Medical),
                    CategoryAuditName =auditCategoryBLL.getCategoryAuditName((int)StaticEnums.AuditCategories.Medical),

                };
            }
            return null;
        }
        //4) get Financial Audit Request
        public CategoryAuditDTO getFinancialAuditRequest(int BatchAuditingRequestID)
        {
            FinancialAuditRequest FinancialAuditRequest = financialAuditRequestBLL.Find(mr => mr.BatchAuditRequestFK == BatchAuditingRequestID).FirstOrDefault();
            if(FinancialAuditRequest!=null)
            {
                return new CategoryAuditDTO()
                {
                    CategoryAuditRequestID = FinancialAuditRequest.FinancialAuditRequestID,
                    NumberOfApprovedClaims = FinancialAuditRequest.NumberOfApprovedClaims,
                    ApprovedClaimsAmount = FinancialAuditRequest.ApprovedClaimsAmount,
                    NumberOfPendingClaims = FinancialAuditRequest.NumberOfPendingClaims,
                    PendingClaimsAmount = FinancialAuditRequest.PendingClaimsAmount,
                    NumberOfRejectedClaims = FinancialAuditRequest.NumberOfRejectedClaims,
                    RejectedClaimsAmount = FinancialAuditRequest.RejectedClaimsAmount,
                    CategoryAuditOfficerComment = FinancialAuditRequest.FinancialAuditOfficerComment,
                    CategoryAuditOfficerFK = FinancialAuditRequest.FinancialAuditAssigneeFK,
                    OfficerName = userBLL.getUserName(FinancialAuditRequest.FinancialAuditAssigneeFK),
                    AssignedTime = FinancialAuditRequest.AssignedTime,
                    CompilationDate = auditFlowBatchDetailsBLL.getAuditCategoryCompilationDate(BatchAuditingRequestID, (int)StaticEnums.AuditCategories.Financial),
                    AuditOrder = auditFlowBatchDetailsBLL.getAuditOrder(BatchAuditingRequestID, (int)StaticEnums.AuditCategories.Financial),
                    CategoryAuditName = auditCategoryBLL.getCategoryAuditName((int)StaticEnums.AuditCategories.Financial),

                };

            }
            return null;
  
        }
        //5) get MI Audit Request
        public CategoryAuditDTO getMIAuditRequest(int BatchAuditingRequestID)
        {
            MIAuditRequest MIAuditRequest = mIAuditRequestBLL.Find(mr => mr.BatchAuditRequestFK == BatchAuditingRequestID).FirstOrDefault();
            if(MIAuditRequest!=null)
            {
                return new CategoryAuditDTO()
                {
                    CategoryAuditRequestID = MIAuditRequest.MIAuditRequestID,
                    CategoryAuditOfficerComment = MIAuditRequest.MIOfficerComment,
                    CategoryAuditOfficerFK = MIAuditRequest.MIOfficerAssigneeFK,
                    OfficerName = userBLL.getUserName(MIAuditRequest.MIOfficerAssigneeFK),
                    AssignedTime = MIAuditRequest.AssignedTime,
                    CompilationDate = auditFlowBatchDetailsBLL.getAuditCategoryCompilationDate(BatchAuditingRequestID, (int)StaticEnums.AuditCategories.Misr),
                    AuditOrder = auditFlowBatchDetailsBLL.getAuditOrder(BatchAuditingRequestID, (int)StaticEnums.AuditCategories.Misr),
                    CategoryAuditName = auditCategoryBLL.getCategoryAuditName((int)StaticEnums.AuditCategories.Misr),

                };
            }
            return null;
    
        }
        //6) get Reconciliation Audit Request
        public CategoryAuditDTO getReconciliationAuditRequest(int BatchAuditingRequestID)
        {
            ReconciliationAuditRequest ReconciliationAuditRequest = reconciliationAuditRequestBLL.Find(mr => mr.BatchAuditRequestFK == BatchAuditingRequestID).FirstOrDefault();
            if(ReconciliationAuditRequest!=null)
            {
                return new CategoryAuditDTO()
                {
                    CategoryAuditRequestID = ReconciliationAuditRequest.ReconciliationAuditRequestID,
                    CategoryAuditOfficerComment = ReconciliationAuditRequest.ReconciliationOfficerComment,
                    CategoryAuditOfficerFK = ReconciliationAuditRequest.ReconciliationOfficerFK,
                    OfficerName = userBLL.getUserName(ReconciliationAuditRequest.ReconciliationOfficerFK),
                    AssignedTime = ReconciliationAuditRequest.AssignedTime,
                    CompilationDate = auditFlowBatchDetailsBLL.getAuditCategoryCompilationDate(BatchAuditingRequestID, (int)StaticEnums.AuditCategories.Reconciliation),
                    AuditOrder = auditFlowBatchDetailsBLL.getAuditOrder(BatchAuditingRequestID, (int)StaticEnums.AuditCategories.Reconciliation),
                   CategoryAuditName = auditCategoryBLL.getCategoryAuditName((int)StaticEnums.AuditCategories.Reconciliation),

                };

            }
            return null;
        }
        //7)get DataEntry Adminstration Request
        public DataEntryAdminstrationRequestDTO getDataEntryAdminstrationRequest(int BatchingRequestID)
        {
            DataEntryAdminstrationRequest DataEntryAdminstrationRequest=dataEntryAdminstrationRequestBLL.Find(dear => dear.BatchRequestFK == BatchingRequestID).FirstOrDefault();
            if(DataEntryAdminstrationRequest!=null)
            {
                return new DataEntryAdminstrationRequestDTO()
                {
                    DataEntryAdminstrationRequestID = DataEntryAdminstrationRequest.DataEntryAdminstrationRequestID,
                    DataEntryAdminAssigneeFK = DataEntryAdminstrationRequest.DataEntryAdminAssigneeFK,
                    OfficerName = userBLL.getUserName(DataEntryAdminstrationRequest.DataEntryAdminAssigneeFK),
                    DataEntryAdminComment = DataEntryAdminstrationRequest.DataEntryAdminComment,
                    AssignedTime= DataEntryAdminstrationRequest.AssignedTime,
                    TransferredToClosingTime= DataEntryAdminstrationRequest.TransferredToClosingTime,
                    
                };
            }
            return null;
        
    }
        //8)get DataEntry Batch Assigning Request
        public List<DataEntryBatchAssigningRequestDTO> getDataEntryBatchAssigningRequest(int DataEntryAdministrationRequestFK)
        {
            List<DataEntryBatchAssigningRequestDTO> allDataEntryAssigningRequest = new List<DataEntryBatchAssigningRequestDTO>();
            DataEntryBatchAssigningRequestDTO TempDataEntryBatchAssigningRequestDTO;
            var AllDataEntryAssignBatch = dataEntryBatchAssigningRequestBLL.Find(dear => dear.DataEntryAdministrationRequestFK == DataEntryAdministrationRequestFK).ToList();
            if(AllDataEntryAssignBatch!=null)
            {
                foreach (var DataEntryAssignBatch in AllDataEntryAssignBatch)
                {
                    TempDataEntryBatchAssigningRequestDTO = new DataEntryBatchAssigningRequestDTO()
                    {
                        DataEntryBatchAssigningRequestID = DataEntryAssignBatch.DataEntryBatchAssigningRequestID,
                        DataEntryOfficerFK = DataEntryAssignBatch.DataEntryOfficerFK,
                        DataEntryOfficerName=userBLL.getUserName(DataEntryAssignBatch.DataEntryOfficerFK),
                        DataEntryOfficerReceivingComment = DataEntryAssignBatch.DataEntryOfficerReceivingComment,
                        ConfirmRecievingByOfficerTime = DataEntryAssignBatch.ConfirmRecievingByOfficerTime,
                        DataEntryOfficerFinishedComment = DataEntryAssignBatch.DataEntryOfficerFinishedComment,
                        AssignedClaimsNumber = DataEntryAssignBatch.AssignedClaimsNumber,
                        DataEntryAdminComment = DataEntryAssignBatch.DataEntryAdminComment,
                        DataEntryOfficerFinishedOnSystemTime= DataEntryAssignBatch.DataEntryOfficerFinishedOnSystemTime,
                        AssignedByDataEntryAssigningTime= DataEntryAssignBatch.AssignedByAdminTime

                    };
                    allDataEntryAssigningRequest.Add(TempDataEntryBatchAssigningRequestDTO);
                }
                return allDataEntryAssigningRequest;
            }
            return null;

        }
        //9) get DataEntry Closed Batch ReAdministration Request
        public BatchClosingRequestDTO getDataEntryBatchClosingRequest(int DataEntryAdministrationRequestFK)
        {
            BatchClosingRequest BatchClosingRequest = batchClosingRequestBLL.Find(cbr => cbr.DataEntryAdminstrationRequestFK == DataEntryAdministrationRequestFK).FirstOrDefault();
            if(BatchClosingRequest!=null)
            {
                return new BatchClosingRequestDTO()
                {
                    BatchClosingRequestID = BatchClosingRequest.BatchClosingRequestID,
                    ClosingOfficerAssigneeFK = BatchClosingRequest.ClosingOfficerAssigneeFK,
                    OfficerName = userBLL.getUserName(BatchClosingRequest.ClosingOfficerAssigneeFK),
                    ConfirmReceivingComment = BatchClosingRequest.ConfirmReceivingComment,
                    ConfirmReceivingTime= BatchClosingRequest.ConfirmReceivingTime,
                    FinishedReviewingComment = BatchClosingRequest.FinishedReviewingComment,
                    FinishedReviewingTime= BatchClosingRequest.FinishedReviewingTime,
                    ClosingOfficerAssignedComment = BatchClosingRequest.ClosingOfficerAssignedComment,
                    ClosingOfficerAssignedTime = BatchClosingRequest.ClosingOfficerAssignedTime,
                    TransferredBackToAdminTime= BatchClosingRequest.TransferredBackToAdminTime
                };
            }
            return null;

        }
        //10) get DataEntry BatchClosing ReAdministration Request
        public BatchClosingReAdministrationRequestDTO getDataEntryBatchClosingReAdministrationRequest(int BatchClosingRequestFK)
        {
            ClosedBatchReAdministrationRequest ClosedBatchReAdministrationRequest = closedBatchReAdministrationRequestBLL.Find(cbr => cbr.BatchClosingRequestFK == BatchClosingRequestFK).FirstOrDefault();
            if (ClosedBatchReAdministrationRequest != null)
            {
                return new BatchClosingReAdministrationRequestDTO()
                {
                    BatchClosingReAdministrationRequestID = ClosedBatchReAdministrationRequest.ClosedBatchReAdmissionRequestID,
                    AssignedByAdminFK = ClosedBatchReAdministrationRequest.AssignedByAdminFK,
                    ReAdministrationOfficerName = userBLL.getUserName(ClosedBatchReAdministrationRequest.AssignedByAdminFK),
                    ReAdministrationOfficerAssignedTime = ClosedBatchReAdministrationRequest.ReAdministrationOfficerAssignedTime,
                    FinalClosureTime = ClosedBatchReAdministrationRequest.FinalClosureTime,
                    FinalClosureComment = ClosedBatchReAdministrationRequest.FinalClosureComment,
                    AssignedByAdminComment= ClosedBatchReAdministrationRequest.AssignedByAdminComment,
                    ConfirmedReceivingComment= ClosedBatchReAdministrationRequest.ConfirmedReceivingComment,
                    ArchivingSystemTicketNo = ClosedBatchReAdministrationRequest.ArchivingSystemTicketNo.Value,
                };
            }
            return null;

        }
        //11) get Finish Batch Closing Request
        public void FinishBatchClosingRequest(BatchClosingRequestDTO batchClosingRequestDTO)
        {
            BatchClosingRequest batchClosingRequest = batchClosingRequestBLL.Find(x => x.BatchClosingRequestID == batchClosingRequestDTO.BatchClosingRequestID).FirstOrDefault();
            batchClosingRequest.BatchClosingStatusFK = (int)Status.ClosingPendingTransfer; //Change
            batchClosingRequest.FinishedReviewingTime = creationDate;
            batchClosingRequest.FinishedReviewingComment = batchClosingRequestDTO.FinishedReviewingComment;

            batchClosingRequestBLL.Edit(batchClosingRequest);
            
        }
        public void TransferBatchClosingRequest(BatchClosingRequestDTO batchClosingRequestDTO)
        {
            BatchClosingRequest batchClosingRequest = batchClosingRequestBLL.Find(x => x.BatchClosingRequestID == batchClosingRequestDTO.BatchClosingRequestID).FirstOrDefault();
            batchClosingRequest.BatchClosingStatusFK = (int)Status.ClosingFinished; //Change
            batchClosingRequest.TransferredBackToAdminTime = creationDate;
            batchClosingRequest.TransferredBackToAdminComment = batchClosingRequestDTO.TransferredBackToAdminComment;

            ClosedBatchReAdministrationRequest closedBatchReAdministrationRequest = new ClosedBatchReAdministrationRequest
            {
                BatchClosingRequestFK = batchClosingRequest.BatchClosingRequestID,
                ReceivedFromClosingOn = creationDate,
                ReAdministrationStatusFK = (int)Status.ReAdministrationNewRequest,
                IsActive = true,
                IsDeleted = false,
                CreationDate = creationDate
            };

            batchClosingRequestBLL.Edit(batchClosingRequest);
            closedBatchReAdministrationRequestBLL.Add(closedBatchReAdministrationRequest);
        }
        //--------------------------------------------------------------------------------------
        #endregion
        //------------------------------------------------------

        //2 get lifeCycle For Receving Requests
        #region
        //api function
        public List<RequestsRecevingLifeCycleInfo> getRecevingLifeCycleRequests(int BillingYear, int BilllingMonth,int ProviderFK)
        {
            List<RequestsRecevingLifeCycleInfo> RequestsRecevingLifeCycleInfoList=new List<RequestsRecevingLifeCycleInfo>();
            List<RecievingRequestDTOS> RecievingRequestsList = getRecievingRequest(BillingYear, BilllingMonth, ProviderFK);
            foreach (var RecievingRequest in RecievingRequestsList)
            {
                RequestsRecevingLifeCycleInfoList.Add(getRecevingLifeCycleInfo(RecievingRequest));
            }

            return RequestsRecevingLifeCycleInfoList;
        }
        //------------------------------------------------------------------------------
        public RequestsRecevingLifeCycleInfo getRecevingLifeCycleInfo(RecievingRequestDTOS RecievingRequest)
        {
            RequestsRecevingLifeCycleInfo tempRequestRecevingLifeCycle = new RequestsRecevingLifeCycleInfo();
            tempRequestRecevingLifeCycle.filterationRquestlistInfo = getfilitrationRequestInfoList(RecievingRequest.ReceivingRequestID);
            tempRequestRecevingLifeCycle.RecievingRequest = RecievingRequest;
            return tempRequestRecevingLifeCycle;
        }
        //------------------------------------------------------------------------------
        public List<RecievingRequestDTOS> getRecievingRequest(int BillingYear, int BilllingMonth, int ProviderFK)
        {
            var RecievingRequests = receivingRequestBLL.Find(r => r.BillingYear == BillingYear && r.BilllingMonth == BilllingMonth && r.ProviderFK == ProviderFK).ToList();
            List<RecievingRequestDTOS> RecievingRequestsList = new List<RecievingRequestDTOS>();
            RecievingRequestDTOS tempRecievingRequest;
            if (RecievingRequests!=null)
            {
                foreach (var RecievingRequest in RecievingRequests)
                {
                    tempRecievingRequest = new RecievingRequestDTOS();
                    tempRecievingRequest.ReceivingRequestID = RecievingRequest.ReceivingRequestID;
                    tempRecievingRequest.ReceivingOfficerCalimsCount = RecievingRequest.ReceivingOfficerCalimsCount;
                    tempRecievingRequest.ReceivedClaimsCount = RecievingRequest.ReceivedClaimsCount;
                    tempRecievingRequest.ProviderName =providersBLL.getProviderName(RecievingRequest.ProviderFK);
                    tempRecievingRequest.ReceivingOfficerComment = RecievingRequest.ReceivingOfficerComment;
                    tempRecievingRequest.BillingYear = RecievingRequest.BillingYear;
                    tempRecievingRequest.BilllingMonth = RecievingRequest.BilllingMonth;
                    tempRecievingRequest.CreationDate = RecievingRequest.CreationDate;
                    RecievingRequestsList.Add(tempRecievingRequest);
                }
            }
            return  RecievingRequestsList;
        }
        //------------------------------------------------------------------------------
        //2) get list from Batching Request Info
        //BatchingRequestInfo=>(BatchingRequestDTO,LifeCycleBatchtRequest);
        public List<filitrationRequestInfo> getfilitrationRequestInfoList(int ReceivingRequestID)
        {
            List<filitrationRequestInfo> filitrationRequestInfoList = new List<filitrationRequestInfo>();
            var tempFilterationRequestDetialsList=getFilterationRequests(ReceivingRequestID);
            foreach (var filitrationRequest in tempFilterationRequestDetialsList)
            {
               
                filitrationRequestInfoList.Add(getfilitrationRequestInfo(filitrationRequest));

            }
            return filitrationRequestInfoList;
        }
        //------------------------------------------------------------------------------
        public filitrationRequestInfo getfilitrationRequestInfo(BatchingFilterationDetailDTO filitrationRequest)
        {
            filitrationRequestInfo tempfilitrationRequestInfo = new filitrationRequestInfo();
       
            var BatchingRequestsList = getBatchingRequest(filitrationRequest.BatchingFilterationDetailID);
            tempfilitrationRequestInfo.BatchingRequestInfoList = getBatchingRequestInfoList(BatchingRequestsList);
            tempfilitrationRequestInfo.BatchingFilterationDetails = filitrationRequest;

            return tempfilitrationRequestInfo;
        }
        //------------------------------------------------------------------------------
        //2) get list from Batching Request Info
        //BatchingRequestInfo=>(BatchingRequestDTO,LifeCycleBatchtRequest);
        public List<BatchingRequestInfo>getBatchingRequestInfoList(List<BatchingRequestDTO> BatchingRequestsList)
        {
            List<BatchingRequestInfo> tempBatchingRequestInfoList = new List<BatchingRequestInfo>();
            foreach (var BatchingRequest in BatchingRequestsList)
            {
                tempBatchingRequestInfoList.Add(getBatchingRequestInfo(BatchingRequest));
            }
            return tempBatchingRequestInfoList;
        }
        //------------------------------------------------------------------------------
        //1) get object from Batching Request Info
        //BatchingRequestInfo=>(BatchingRequestDTO,LifeCycleBatchtRequest);
        public BatchingRequestInfo getBatchingRequestInfo(BatchingRequestDTO BatchingRequest)
        {
            BatchingRequestInfo tempBatchingRequestInfo= new BatchingRequestInfo();
            tempBatchingRequestInfo.BatchingRequest = BatchingRequest;
            tempBatchingRequestInfo.LifeCycleBatchtRequest = getBatchRequestLifeCycleByBatchNumber(BatchingRequest.BatchNumber, BatchingRequest.BatchSystemFK);
            return tempBatchingRequestInfo;
        }//1
        //---------------------------------------------------------------------------
        public List<BatchingFilterationDetailDTO> getFilterationRequests(int ReceivingRequestID)
        {
            List<BatchingFilterationDetailDTO> tempBatchingFilterationRequestDetialsList = new List<BatchingFilterationDetailDTO>();
            BatchingFilterationDetailDTO tempBatchingFilterationRequestDetials;
            int? FilterationRequestID = filterationRequestBLL.Find(fr => fr.ReceivingRequestFK == ReceivingRequestID).FirstOrDefault()?.FilterationRequestID;
            var BatchingFilterationDetails = batchingFilterationDetailsBLL.Find(BFD => BFD.FilterationRequestFK == FilterationRequestID).ToList();

            foreach (var BatchingFilitrationRequest in BatchingFilterationDetails)
            {
                tempBatchingFilterationRequestDetials = new BatchingFilterationDetailDTO();
                tempBatchingFilterationRequestDetials.BatchingFilterationDetailID = BatchingFilitrationRequest.BatchingFilterationDetailID;
                tempBatchingFilterationRequestDetials.ReceivingRequestIFK = ReceivingRequestID;
                tempBatchingFilterationRequestDetials.FilterationRequestID = BatchingFilitrationRequest.FilterationRequestFK;
                tempBatchingFilterationRequestDetials.CategoryName =filterationCategoriesBLL.getFilterationCategoryName(BatchingFilitrationRequest.FilterationCategoryFK);
                tempBatchingFilterationRequestDetials.FilterationCategoryID = BatchingFilitrationRequest.FilterationCategoryFK;
                tempBatchingFilterationRequestDetials.TotalClaimsCount = BatchingFilitrationRequest.TotalClaimsCount;
                tempBatchingFilterationRequestDetials.RemainingClaimsCount = BatchingFilitrationRequest.RemainingClaimsCount;
                tempBatchingFilterationRequestDetials.CreationDate = BatchingFilitrationRequest.CreationDate;
                tempBatchingFilterationRequestDetialsList.Add(tempBatchingFilterationRequestDetials);
            }

            return tempBatchingFilterationRequestDetialsList;
        }
        //---------------------------------------------------------------------------
        public List<BatchingRequestDTO> getBatchingRequest(int BatchingFilterationDetailID)
        {
            var BatchingRequests = batchingRequestBLL.Find(br => br.BatchingFilterationDetailsFK == BatchingFilterationDetailID);
            BatchingRequestDTO tempBatchingRequest;
            List<BatchingRequestDTO> BatchingRequestList = new List<BatchingRequestDTO>(); 
            foreach (var BatchingRequest in BatchingRequests)
            {
                tempBatchingRequest = new BatchingRequestDTO();
                tempBatchingRequest.BatchingRequestID = BatchingRequest.BatchingRequestID;
                tempBatchingRequest.BatchingFilterationDetailsFK = BatchingRequest.BatchingFilterationDetailsFK;
                tempBatchingRequest.NumberOfBatchClaims = BatchingRequest.NumberOfBatchClaims;
                tempBatchingRequest.BatchSystemFK = BatchingRequest.BatchSystemFK;
                tempBatchingRequest.BatchSystemName = insuranceSystemBLL.getInsuranceSystemName(BatchingRequest.BatchSystemFK);
                tempBatchingRequest.BatchNumber = BatchingRequest.BatchNumber;
                tempBatchingRequest.BatchingOfficerFK = BatchingRequest.BatchingOfficerFK;
                tempBatchingRequest.OfficerName =userBLL.getUserName(BatchingRequest.BatchingOfficerFK);
                tempBatchingRequest.BatchingOfficerComment = BatchingRequest.BatchingOfficerComment;
                tempBatchingRequest.FilterationCategoryFK = BatchingRequest.FilterationCategoryFK;
                tempBatchingRequest.FilterationCategoryName = filterationCategoriesBLL.getFilterationCategoryName((int)BatchingRequest.FilterationCategoryFK);
                tempBatchingRequest.TransferredToAuditDate = BatchingRequest.TransferredToAuditDate;//null
                tempBatchingRequest.CreationDate = BatchingRequest.CreationDate;
                BatchingRequestList.Add(tempBatchingRequest);
            }
            return BatchingRequestList;
        }

        #endregion

        //----------------------------------------------------------------
        public CheckLockedRequestDTO CheckLockedRequest(EntityRequestDTO EntityRequestDTO)
        {
            CheckLockedRequestDTO Check = new CheckLockedRequestDTO();
            switch (EntityRequestDTO.EntityID)
            {
                case (int)Entity.ReceivingRequest://1
                    {
                        var RequestData = receivingRequestBLL.Find(r => r.ReceivingRequestID == EntityRequestDTO.ReqID).FirstOrDefault();
                        Check.IsLockedRequest = RequestData.IsLocked ? true : false;
                        Check.RequestCanLocked = statusBLL.Find(x => x.StatusID == RequestData.ReceivingStatusFK).FirstOrDefault().CanLocked;
                        break;
                    }
                case (int)Entity.FilterationRequest://2
                    {
                        var RequestData = filterationRequestBLL.Find(fr => fr.FilterationRequestID == EntityRequestDTO.ReqID).FirstOrDefault();
                        Check.IsLockedRequest = RequestData.IsLocked ? true : false;
                        Check.RequestCanLocked = statusBLL.Find(x => x.StatusID == RequestData.FilterationRequestStatusFK).FirstOrDefault().CanLocked;
                        break;
                    }
                case (int)Entity.BatchRequest://3
                    {
                        var RequestData = batchingRequestBLL.Find(fr => fr.BatchingRequestID == EntityRequestDTO.ReqID).FirstOrDefault();
                        Check.IsLockedRequest = RequestData.IsLocked ? true : false;
                        Check.RequestCanLocked = statusBLL.Find(x => x.StatusID == RequestData.StatusFK).FirstOrDefault().CanLocked;
                        break;
                    }
                case (int)Entity.AuditRequest://4
                    {
                        var RequestData = batchAuditingRequestBLL.Find(fr => fr.BatchAuditingRequestID == EntityRequestDTO.ReqID).FirstOrDefault();
                          Check.IsLockedRequest = RequestData.IsLocked ? true : false;
                        Check.RequestCanLocked = statusBLL.Find(x => x.StatusID == RequestData.BatchAuditingStatusFK).FirstOrDefault().CanLocked;
                        break;
                    }
                case (int)Entity.DataEntryAdminstrationRequest://5
                    {
                        var RequestData = dataEntryAdminstrationRequestBLL.Find(fr => fr.DataEntryAdminstrationRequestID == EntityRequestDTO.ReqID).FirstOrDefault();
                        Check.IsLockedRequest = RequestData.IsLocked ? true : false;
                        Check.RequestCanLocked = statusBLL.Find(x => x.StatusID == RequestData.DataEntryAdministrationStatusFK).FirstOrDefault().CanLocked;

                        break;
                    }
                case (int)Entity.DataEntryBatchAssigningRequest://6
                    {
                        var RequestData = dataEntryBatchAssigningRequestBLL.Find(fr => fr.DataEntryBatchAssigningRequestID == EntityRequestDTO.ReqID).FirstOrDefault();
                        Check.IsLockedRequest = RequestData.IsLocked ? true : false;
                        Check.RequestCanLocked = statusBLL.Find(x => x.StatusID == RequestData.DataEntryBatchAssigningStatusFK).FirstOrDefault().CanLocked;
                        break;
                    }
                case (int)Entity.BatchClosingRequest://7
                    {
                        var RequestData = batchClosingRequestBLL.Find(fr => fr.BatchClosingRequestID == EntityRequestDTO.ReqID).FirstOrDefault();
                        Check.IsLockedRequest = RequestData.IsLocked ? true : false;
                        Check.RequestCanLocked = statusBLL.Find(x => x.StatusID == RequestData.BatchClosingStatusFK).FirstOrDefault().CanLocked;
                        break;
                    }
                case (int)Entity.ClosedBatchReAdministrationRequest://8
                    {
                        var RequestData = closedBatchReAdministrationRequestBLL.Find(fr => fr.ClosedBatchReAdmissionRequestID == EntityRequestDTO.ReqID).FirstOrDefault();
                        Check.IsLockedRequest = RequestData.IsLocked ? true : false;
                        Check.RequestCanLocked = statusBLL.Find(x => x.StatusID == RequestData.ReAdministrationStatusFK).FirstOrDefault().CanLocked;
                        break;
                    }
                case (int)Entity.BatchingFilterationDetails://9
                    {
                        var RequestData = batchingFilterationDetailsBLL.Find(fr => fr.BatchingFilterationDetailID == EntityRequestDTO.ReqID).FirstOrDefault();
                        Check.IsLockedRequest = RequestData.IsLocked ? true : false;
                        Check.RequestCanLocked = statusBLL.Find(x => x.StatusID == RequestData.BatchingFilterationDetailStatusFK).FirstOrDefault().CanLocked;
                        break;
                    }
                //case (int)Entity.DataEntryRequest://9
                //    {
                //        var RequestData = dataEntryBatchAssigningRequestBLL.Find(fr => fr.DataEntryBatchAssigningRequestID == EntityRequestDTO.ReqID).FirstOrDefault();
                //        Check.IsLockedRequest = RequestData.IsLocked ? true : false;
                //        Check.RequestCanLocked = statusBLL.Find(x => x.StatusID == RequestData.DataEntryBatchAssigningStatusFK).FirstOrDefault().CanLocked;
                //        break;
                //    }
                      case (int)Entity.DataEntryClosingRequest://9
                    {
                        var RequestData = batchClosingRequestBLL.Find(fr => fr.BatchClosingRequestID == EntityRequestDTO.ReqID).FirstOrDefault();
                        Check.IsLockedRequest = RequestData.IsLocked ? true : false;
                        Check.RequestCanLocked = statusBLL.Find(x => x.StatusID == RequestData.BatchClosingStatusFK).FirstOrDefault().CanLocked;
                        break;
                    }
              
                default:
                    Check.IsLockedRequest = false;
                    Check.RequestCanLocked = false;
                    break;

            }

            return Check;

        }
        //---------------------------------------------------------------
    
}

}
