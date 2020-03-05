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
    public class SharedAuditBatchBLL
    {
        DateTime creationDate;
        BatchAuditingRequestBLL batchAuditingRequestBLL;
        AuditFlowBatchDetailsBLL auditFlowBatchDetailBLL;
        AuditFlowDetailsBLL auditFlowDetailsBLL;
        AuditFlowBLL auditFlowBLL;
        BatchingRequestBLL batchingRequestBLL;
        AuditCategoryBLL auditCategoryBLL;
        MIAuditRequestBLL mIAuditRequestBLL;
        ReconciliationAuditRequestBLL reconciliationAuditRequestBLL;
        MedicalAuditRequestBLL medicalAuditRequestBLL;
        FinancialAuditRequestBLL financialAuditRequestBLL;
        SharedSearchBLL sharedSearchBLL;
        InsuranceSystemBLL insuranceSystemBLL;
        //special approval
        SPRequestBLL SPRequestBLL;
        //------------------------------------------------------
        public SharedAuditBatchBLL(BNCEntities Context, DateTime CreationDate)
        {
            creationDate = CreationDate;
            batchAuditingRequestBLL = new BatchAuditingRequestBLL(Context, CreationDate);
            auditFlowBatchDetailBLL = new AuditFlowBatchDetailsBLL(Context, CreationDate);
            auditFlowDetailsBLL = new AuditFlowDetailsBLL(Context, CreationDate);
            auditFlowBLL = new AuditFlowBLL(Context, CreationDate);
            batchingRequestBLL = new BatchingRequestBLL(Context, CreationDate);
            batchingRequestBLL = new BatchingRequestBLL(Context, CreationDate);
            auditCategoryBLL = new AuditCategoryBLL(Context, CreationDate);
            mIAuditRequestBLL = new MIAuditRequestBLL(Context, CreationDate);
            reconciliationAuditRequestBLL = new ReconciliationAuditRequestBLL(Context, CreationDate);
            medicalAuditRequestBLL = new  MedicalAuditRequestBLL(Context, CreationDate);
            financialAuditRequestBLL = new FinancialAuditRequestBLL(Context, CreationDate);
            sharedSearchBLL = new SharedSearchBLL(Context, CreationDate);
            insuranceSystemBLL = new InsuranceSystemBLL(Context, CreationDate);
            SPRequestBLL = new SPRequestBLL(Context, creationDate);
        }

        public void AddBatchAuditFlowDetails(int BatchID)
        {
            BatchingRequest Batch = batchingRequestBLL.Find(x => x.BatchingRequestID == BatchID).FirstOrDefault();
            Batch.StatusFK = (int)Status.PendingAudit;
            batchingRequestBLL.Edit(Batch);
            BatchAuditingRequest BatchAuditingRequest = new BatchAuditingRequest
            {
                //BatchAuditingStatusFK = (int)Status.PendingMedical,
                BatchingRequestFK = Batch.BatchingRequestID,
                CreationDate = creationDate,
                IsActive = true,
                IsDeleted = false,
                FilterationCategoryFK = Batch.FilterationCategoryFK,
                TotalNumberOfApprovedClaims= Batch.NumberOfBatchClaims,
                 TotalNumberOfRejectedClaims=0
            };
            batchAuditingRequestBLL.Add(BatchAuditingRequest);
            int AuditID = auditFlowBLL.Find(x => x.FilterationCategoryFK == BatchAuditingRequest.FilterationCategoryFK).FirstOrDefault().AuditFlowID;
            var ListAuditFlowDetails = auditFlowDetailsBLL.Find(x => x.AuditFlowFK == AuditID).ToList();
            int CountOfSteps = 0;
            foreach (var item in ListAuditFlowDetails)
            {
                AuditFlowBatchDetail AuditFlowBatch = new AuditFlowBatchDetail
                {
                    AuditOrder = item.AuditOrder,
                    AuditCategoryFK = item.AuditCategoriesFK,
                    AuditFlowDetailsFK = item.AuditFlowDetailsID,
                    BatchAuditingRequest = BatchAuditingRequest,
                    BatchFK = Batch.BatchingRequestID,
                    IsActive = true,
                    IsDeleted = false,
                    CreationDate = creationDate,

                };
                if (CountOfSteps==0)
                {
                    auditFlowBLL.EditBatchAuditRequestStatusOnChangeOfFlow(BatchAuditingRequest, AuditFlowBatch);

                }
                auditFlowBatchDetailBLL.Add(AuditFlowBatch);
                CountOfSteps++;

            }
          
            //auditFlowBatchDetailBLL.Find(x=>x.BatchAuditRequestFK== AuditFlowID);


        }
    
        //public bool  IsTheLasetStep(BatchAuditingRequest BatchAuditRequest)
        //{
        //    int? AuditCategoryID = auditFlowBatchDetailBLL.Find
        //   (x =>
        //    x.BatchAuditRequestFK == BatchAuditRequest.BatchAuditingRequestID &&
        //    x.IsActive &&
        //    x.IsDeleted == false &&
        //    x.IsComplete != true).Count();
            
        //    if (AuditCategoryID == 0)
        //    {
        //        return true;
        //    }
        //        return false;

        //}

        public void AddApprovedAndRejectedClaimsToBatchAuditingRequest(CategoryClaimsCountDTO CategoryClaimsCountDTO)
        {

        }
        public MIAuditRequest AddMIAuditRequest(MIAuditDTO MIAuditInputDTO)
        {
            var BatchingAuditRequest = batchAuditingRequestBLL.Find(x => x.BatchingRequestFK == MIAuditInputDTO.BatchRequestFK).FirstOrDefault();
           var mIAuditRequest = mIAuditRequestBLL.Find(x => x.BatchAuditRequestFK == BatchingAuditRequest.BatchAuditingRequestID).FirstOrDefault();
            if (MIAuditInputDTO.MIOfficerComment != "")
            {
                mIAuditRequest.MIOfficerComment = MIAuditInputDTO.MIOfficerComment;
            }
            mIAuditRequestBLL.Edit(mIAuditRequest);
            return mIAuditRequest;
        }
        public MIAuditRequest AssignMIAuditRequest(MIAuditDTO MIAuditInputDTO)
        {
            var BatchingAuditRequest = batchAuditingRequestBLL.Find(x => x.BatchingRequestFK == MIAuditInputDTO.BatchRequestFK).FirstOrDefault();//change

            MIAuditRequest mIAuditRequest = new MIAuditRequest
            {
                //input
                BatchAuditRequestFK = BatchingAuditRequest.BatchAuditingRequestID,
                //from developer
                IsActive = true,
                IsDeleted = false,
                CreationDate = creationDate,
                AssignedTime = creationDate,
                //MIAuditStatusFK =(int) Status.New,
                MIOfficerAssigneeFK = MIAuditInputDTO.MIOfficerFK,
            };
            BatchingAuditRequest.BatchAuditingStatusFK = (int)Status.AssignMi;
            batchAuditingRequestBLL.Edit(BatchingAuditRequest);
            mIAuditRequestBLL.Add(mIAuditRequest);
            return mIAuditRequest;
        }
        public ReconciliationAuditRequest AddReconciliationAuditRequest(ReconciliationDTO ReconciliationAuditInputDTO)
        {
            var BatchingAuditRequest = batchAuditingRequestBLL.Find(x => x.BatchingRequestFK == ReconciliationAuditInputDTO.BatchRequestFK).FirstOrDefault();
            ReconciliationAuditRequest ReconciliationAuditRequest = reconciliationAuditRequestBLL.Find(x => x.BatchAuditRequestFK == BatchingAuditRequest.BatchAuditingRequestID)
                .FirstOrDefault();


            if (ReconciliationAuditInputDTO.ReconciliationOfficerComment != "")
            {
                ReconciliationAuditRequest.ReconciliationOfficerComment = ReconciliationAuditInputDTO.ReconciliationOfficerComment;
            }
            reconciliationAuditRequestBLL.Edit(ReconciliationAuditRequest);
            return ReconciliationAuditRequest;
        }
        public ReconciliationAuditRequest AssignReconciliationAuditRequest(ReconciliationDTO ReconciliationAuditInputDTO)
        {
            var BatchingAuditRequest = batchAuditingRequestBLL.Find(x => x.BatchingRequestFK == ReconciliationAuditInputDTO.BatchRequestFK).FirstOrDefault();
            ReconciliationAuditRequest ReconciliationAuditRequest = new ReconciliationAuditRequest
            {
                BatchAuditRequestFK = BatchingAuditRequest.BatchAuditingRequestID,//change  
                IsActive = true,
                IsDeleted = false,
                CreationDate = creationDate,
                AssignedTime = creationDate,
                ReconciliationOfficerFK = ReconciliationAuditInputDTO.ReconciliationOfficerFK,//change 
            };
            BatchingAuditRequest.BatchAuditingStatusFK = (int)Status.AssignReconilition;
            batchAuditingRequestBLL.Edit(BatchingAuditRequest);
            reconciliationAuditRequestBLL.Add(ReconciliationAuditRequest);
            return ReconciliationAuditRequest;
        }



        public MedicalAuditRequest AddMedicalAuditRequest(MedicalAuditDTO MedicalAuditInputDTO)
        {
            int BatchingAuditRequestID = batchAuditingRequestBLL.Find(x => x.BatchingRequestFK == MedicalAuditInputDTO.BatchRequestFK)
                .FirstOrDefault().BatchAuditingRequestID;
            MedicalAuditRequest medicalAuditRequest= medicalAuditRequestBLL.Find(x => x.BatchAuditRequestFK == BatchingAuditRequestID).FirstOrDefault();

            //input
            medicalAuditRequest.BatchAuditRequestFK = BatchingAuditRequestID;
            medicalAuditRequest.NumberOfApprovedClaims = MedicalAuditInputDTO.NumberOfApprovedClaims;
            medicalAuditRequest.ApprovedClaimsAmount = MedicalAuditInputDTO.ApprovedClaimsAmount;
            medicalAuditRequest.NumberOfPendingClaims = MedicalAuditInputDTO.NumberOfPendingClaims;
            medicalAuditRequest.PendingClaimsAmount = MedicalAuditInputDTO.PendingClaimsAmount;
            medicalAuditRequest.NumberOfRejectedClaims = MedicalAuditInputDTO.NumberOfRejectedClaims;
            medicalAuditRequest.RejectedClaimsAmount = MedicalAuditInputDTO.RejectedClaimsAmount;
            //from developer
            medicalAuditRequest.IsActive = true;
            medicalAuditRequest.IsDeleted = false;
        
            if (MedicalAuditInputDTO.MedicalAuditOfficerComment != "")
            {
                medicalAuditRequest.MedicalAuditOfficerComment = MedicalAuditInputDTO.MedicalAuditOfficerComment;
            }
            medicalAuditRequestBLL.Edit(medicalAuditRequest);
            return medicalAuditRequest;
        }
        public MedicalAuditRequest AssignMedicalAuditRequest(MedicalAuditDTO MedicalAuditInputDTO)
        {
            var BatchingAuditRequest = batchAuditingRequestBLL.Find(x => x.BatchingRequestFK == MedicalAuditInputDTO.BatchRequestFK)
                .FirstOrDefault();
            MedicalAuditRequest medicalAuditRequest = new MedicalAuditRequest
            {
                //input
                BatchAuditRequestFK = BatchingAuditRequest.BatchAuditingRequestID,
                IsActive = true,
                IsDeleted = false,
                CreationDate = creationDate,
                AssignedTime = creationDate,
                MedicalOfficerAssigneeFK = MedicalAuditInputDTO.MedicalAuditOfficerFK,

            };
            
            medicalAuditRequestBLL.Add(medicalAuditRequest);
            BatchingAuditRequest.BatchAuditingStatusFK =(int) Status.AssignMedical;
            batchAuditingRequestBLL.Edit(BatchingAuditRequest);
            return medicalAuditRequest;
        }


        public FinancialAuditRequest AddFinancialAuditRequest(FinancialAuditDTO FinancialAuditInputDTO)
        {
            var BatchAuditRequest = batchAuditingRequestBLL.Find(x => x.BatchingRequestFK == FinancialAuditInputDTO.BatchRequestFK).FirstOrDefault();
            var financialAuditRequest = financialAuditRequestBLL.Find(x => x.BatchAuditRequestFK == BatchAuditRequest.BatchAuditingRequestID).FirstOrDefault();
                //input
              financialAuditRequest.BatchAuditRequestFK = BatchAuditRequest.BatchAuditingRequestID;
              financialAuditRequest.NumberOfApprovedClaims = FinancialAuditInputDTO.NumberOfApprovedClaims;
              financialAuditRequest.ApprovedClaimsAmount = FinancialAuditInputDTO.ApprovedClaimsAmount;
              financialAuditRequest.NumberOfPendingClaims = FinancialAuditInputDTO.NumberOfPendingClaims;
              financialAuditRequest.PendingClaimsAmount = FinancialAuditInputDTO.PendingClaimsAmount;
              financialAuditRequest.NumberOfRejectedClaims = FinancialAuditInputDTO.NumberOfRejectedClaims;
              financialAuditRequest.RejectedClaimsAmount = FinancialAuditInputDTO.RejectedClaimsAmount;
           
            if (FinancialAuditInputDTO.FinancialAuditOfficerComment != "")
            {
                financialAuditRequest.FinancialAuditOfficerComment = FinancialAuditInputDTO.FinancialAuditOfficerComment;
            }
            financialAuditRequestBLL.Edit(financialAuditRequest);
            return financialAuditRequest;
        }


        public FinancialAuditRequest AssignFinancialAuditRequest(FinancialAuditDTO FinancialAuditInputDTO)
        {
            var BatchingAuditRequest = batchAuditingRequestBLL.Find(x => x.BatchingRequestFK == FinancialAuditInputDTO.BatchRequestFK)
                .FirstOrDefault();
            FinancialAuditRequest FinancialAuditRequest = new FinancialAuditRequest
            {
                BatchAuditRequestFK = BatchingAuditRequest.BatchAuditingRequestID,
                IsActive = true,
                IsDeleted = false,
                CreationDate = creationDate,
                AssignedTime = creationDate,
                FinancialAuditAssigneeFK = FinancialAuditInputDTO.FinancialAuditOfficerFK,

            };

            financialAuditRequestBLL.Add(FinancialAuditRequest);
            BatchingAuditRequest.BatchAuditingStatusFK = (int)Status.AssignFindincial;
            batchAuditingRequestBLL.Edit(BatchingAuditRequest);
            return FinancialAuditRequest;
        }

        public BatchAuditingRequest AddBatchAuditingRequest(int BatchingRequestFK)
        {
            BatchAuditingRequest batchAuditingRequest = new BatchAuditingRequest
            {
                //input
                BatchingRequestFK = BatchingRequestFK,
                BatchAuditingStatusFK = (int)Status.PendingAudit,
                //from developer
                IsActive = true,
                IsDeleted = false,
                CreationDate = DateTime.Now,
            };

            batchAuditingRequestBLL.Add(batchAuditingRequest);
            return batchAuditingRequest;
        }
        public BatchAuditingRequestDTO GetBatchAuditingRequest(int BatchAuditingRequestID)
        {
            BatchAuditingRequest batchAuditingRequest = batchAuditingRequestBLL.Find(x => x.BatchAuditingRequestID == BatchAuditingRequestID).FirstOrDefault();

            BatchAuditingRequestDTO batchAuditingRequestDTO = new BatchAuditingRequestDTO
            {
                BatchingRequestFK = batchAuditingRequest.BatchingRequestFK,
                NumberOfAuditingBatchClaims = batchAuditingRequest.NumberOfAuditingBatchClaims.Value
            };

            return batchAuditingRequestDTO;
        }
        public List<BatchingRequestDTO> GetBatchingAuditRequests()
        {
            List<BatchingRequestDTO> BatchingRequests = new List<BatchingRequestDTO>();
            foreach (var item in batchingRequestBLL.GetAll())
            {
                BatchingRequests.Add(new BatchingRequestDTO
                {
                    BatchingOfficerComment = item.BatchingOfficerComment,
                    BatchNumber = item.BatchNumber,
                    BatchSystemFK = -item.BatchSystemFK,
                    BatchingFilterationDetailsFK = item.BatchingFilterationDetailsFK,
                    BatchingOfficerFK = item.BatchingOfficerFK,
                    NumberOfBatchClaims = item.NumberOfBatchClaims,
                    BatchingRequestID = item.BatchingRequestID

                });

            }
            return BatchingRequests;
        }

        public List<BatchingRequestDTO> GetBatchingAuditRequestsByStatus(SearchCriteriaDTO SearchCriteria)
        {
            List<BatchingRequestDTO> BatchingRequests = new List<BatchingRequestDTO>();

            List<dynamic> BatchRequests = sharedSearchBLL.Search(SearchCriteria).OfType<dynamic>().ToList();

            foreach (var item in BatchRequests)
            {
                int BatchingRequestID = item.BatchingRequestFK;
                int TotalNumberOfApprovedClaims = item.TotalNumberOfApprovedClaims??0;
                var BatchRequest= batchingRequestBLL.Find(x => x.BatchingRequestID == BatchingRequestID).FirstOrDefault();
                BatchingRequests.Add(new BatchingRequestDTO
                {

                    BatchingRequestID = BatchRequest.BatchingRequestID,
                      NumberOfBatchClaims = TotalNumberOfApprovedClaims,
                    // BatchSystemFK =item.BatchSystemFK,
                    BatchNumber = BatchRequest.BatchNumber,
                    //BatchingOfficerFK = item.BatchingOfficerFK,
                    //BatchingOfficerComment = item.BatchingOfficerComment,
                    //FilterationCategoryFK = item.FilterationCategoryFK,
                    //TransferredToAuditDate = item.TransferredToAuditDate,//null
                    CreationDate = BatchRequest.CreationDate,
                    BatchSystemName = insuranceSystemBLL.Find(x => x.SystemID == BatchRequest.BatchSystemFK).FirstOrDefault().SystemName

                });

            }
            return BatchingRequests;
        }

        public MedicalAuditDTO GetMedicalAuditRequestData(int BatchID) {
           var BatchRequest= batchingRequestBLL.Find(x => x.BatchingRequestID == BatchID).FirstOrDefault();
            var BatchAudit = batchAuditingRequestBLL.Find(x => x.BatchingRequestFK == BatchID).FirstOrDefault();
          var medicalAudit= medicalAuditRequestBLL.Find(x=>x.BatchAuditRequestFK== BatchAudit.BatchAuditingRequestID).FirstOrDefault();
            return new MedicalAuditDTO
            {
                ApprovedClaimsAmount = medicalAudit?.ApprovedClaimsAmount,
                AssignedTime = medicalAudit?.AssignedTime,
                MedicalAuditOfficerComment = medicalAudit?.MedicalAuditOfficerComment,
                NumberOfApprovedClaims = medicalAudit?.NumberOfApprovedClaims,
                NumberOfPendingClaims = medicalAudit?.NumberOfPendingClaims,
                PendingClaimsAmount = medicalAudit?.PendingClaimsAmount,
                RejectedClaimsAmount = medicalAudit?.RejectedClaimsAmount,
                NumberOfRejectedClaims = medicalAudit?.NumberOfRejectedClaims,
                TotalNumberOfApprovedClaims =(int)BatchAudit.TotalNumberOfApprovedClaims,
                BatchAuditRequestFK= BatchAudit.BatchAuditingRequestID
            };
                

        }
        public FinancialAuditDTO GetfinancialAuditRequestData(int BatchID)
        {
            var BatchAudit = batchAuditingRequestBLL.Find(x => x.BatchingRequestFK == BatchID).FirstOrDefault();

            var financialAudit = financialAuditRequestBLL.Find(x => x.BatchAuditRequestFK == BatchAudit.BatchAuditingRequestID).FirstOrDefault();
            return new  FinancialAuditDTO
            {
                 ApprovedClaimsAmount = financialAudit?.ApprovedClaimsAmount,
                AssignedTime = financialAudit?.AssignedTime,
                 FinancialAuditOfficerComment = financialAudit?.FinancialAuditOfficerComment,
                NumberOfApprovedClaims = financialAudit?.NumberOfApprovedClaims,
                NumberOfPendingClaims = financialAudit?.NumberOfPendingClaims,
                PendingClaimsAmount = financialAudit?.PendingClaimsAmount,
                RejectedClaimsAmount = financialAudit?.RejectedClaimsAmount,
                NumberOfRejectedClaims = financialAudit?.NumberOfRejectedClaims,
               TotalNumberOfApprovedClaims = BatchAudit.TotalNumberOfApprovedClaims,
                BatchAuditRequestFK = BatchAudit.BatchAuditingRequestID
            };

        }
        public ReconciliationDTO GetReconciliationComment(int BatchID)
        {
            var ReconciliationAudit = batchAuditingRequestBLL.Find(x => x.BatchingRequestFK == BatchID).FirstOrDefault();
            var Comment = reconciliationAuditRequestBLL.Find(x => x.BatchAuditRequestFK == ReconciliationAudit.BatchAuditingRequestID).FirstOrDefault()?.ReconciliationOfficerComment;
            return new ReconciliationDTO
            {
                 ReconciliationOfficerComment = Comment,
                BatchAuditRequestFK = ReconciliationAudit.BatchAuditingRequestID,
                TotalClaimsNumber = ReconciliationAudit.TotalNumberOfApprovedClaims

            };
        }
        public MIAuditDTO GetMIComment(int BatchID)
        {
            var MiAudit= batchAuditingRequestBLL.Find(x => x.BatchingRequestFK == BatchID).FirstOrDefault();
            var Comment = mIAuditRequestBLL.Find(x => x.BatchAuditRequestFK == MiAudit.BatchAuditingRequestID).FirstOrDefault()?.MIOfficerComment;
            return new MIAuditDTO {
                MIOfficerComment = Comment,
                BatchAuditRequestFK = MiAudit.BatchAuditingRequestID,
                TotalClaimsNumber=(int) MiAudit.TotalNumberOfApprovedClaims
            };
            
        }

        //public List<MedicalAuditDTO> GetMedicalAuditRequestData()
        //{
        //}

        //--------------------------------------------------------------------
        //special Approval Requests
        public SPRequest addSpecialApprovalRequest(SPReqestDTO SPReqestDTO)
        {
            SPRequest SPRequest = new SPRequest
            {
                ReqByUserFk = SPReqestDTO.ReqByUserFk,
                ReqFK= SPReqestDTO.ReqFK,
                EntrityFK = SPReqestDTO.EntrityFK,
                SPStatusFK = SPReqestDTO.SPStatusFK,
                CreationDate = DateTime.Now,
                IsActive = true,
                IsDeleted=false,
                IsLoadedByUser=false,

            };
            if(SPReqestDTO.ReqByUserNote!="" && SPReqestDTO.ReqByUserNote!=null)
            {
                SPRequest.ReqByUserNote = SPReqestDTO.ReqByUserNote;
            }
            SPRequestBLL.Add(SPRequest);
            return SPRequest;
        }
        //--------------------------------------------------------------------

    }

}
