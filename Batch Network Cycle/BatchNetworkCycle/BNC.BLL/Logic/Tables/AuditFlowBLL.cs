using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BNC.DAL.Repositories;
using BNC.Entities;
using BNC.DTOs;
using BNC.DTOs.Business;
using static BNC.BLL.StaticData.StaticEnums;

namespace BNC.BLL.Logic.Tables
{
    public class AuditFlowBLL : Repository<AuditFlow>
    {
        DateTime creationDate;
        AuditFlowBatchDetailsBLL auditFlowBatchDetailBLL;
        AuditCategoryBLL auditCategoryBLL;
        BatchAuditingRequestBLL batchAuditingRequestBLL;
        BatchingRequestBLL batchingRequestBLL;
        MedicalAuditRequestBLL MedicalAuditRequestBLL;

        public AuditFlowBLL(BNCEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
            batchingRequestBLL = new BatchingRequestBLL(Context, CreationDate);
            batchAuditingRequestBLL = new BatchAuditingRequestBLL(Context, CreationDate);
            auditCategoryBLL = new AuditCategoryBLL(Context, CreationDate);
            auditFlowBatchDetailBLL = new AuditFlowBatchDetailsBLL(Context, CreationDate);
            MedicalAuditRequestBLL = new MedicalAuditRequestBLL(Context, CreationDate);
        }

        public AuditCategoryDIM NextCategory(BatchAuditingRequest BatchAuditRequest)
        {
            int AuditCategoryID = auditFlowBatchDetailBLL.Find
           (x =>
            x.BatchAuditRequestFK == BatchAuditRequest.BatchAuditingRequestID &&
            x.IsActive &&
            x.IsDeleted == false &&
            x.IsComplete != true).FirstOrDefault().AuditCategoryFK;
            return auditCategoryBLL.Find(x => x.AuditCategoriesID == AuditCategoryID).FirstOrDefault();
        }

        public RedirctBatchInAuditingStageDTO GetNextAuditStep(int BatchID)
        {
            var BatchAuditRequest = batchAuditingRequestBLL.Find(x => x.BatchingRequestFK == BatchID).FirstOrDefault();
            var RedirectionObj = NextCategory(BatchAuditRequest);
            //EditBatchAuditRequestStatusOnChangeOfFlow(BatchAuditRequest, RedirectionObj);
            return new RedirctBatchInAuditingStageDTO
            {
                AuditCategoriesControllerName = RedirectionObj.AuditCategoriesControllerName,
                AuditCategoriesActionName = RedirectionObj.AuditCategoriesActionName

            };
        }
        public AuditFlowBatchDetail CompletedNextAudtingStep(int BatchAuditRequestID)
        {
            var BatchAuditRequest = batchAuditingRequestBLL.Find(x => x.BatchingRequestFK == BatchAuditRequestID).FirstOrDefault().BatchAuditingRequestID;
            var AuditStepCompleted = auditFlowBatchDetailBLL.Find
            (x =>
            x.BatchAuditRequestFK == BatchAuditRequest &&
            x.IsActive &&
            x.IsDeleted == false &&
            x.IsComplete != true).FirstOrDefault();
            AuditStepCompleted.IsActive = false;
            AuditStepCompleted.IsComplete = true;
            AuditStepCompleted.CompilationDate = creationDate;
            auditFlowBatchDetailBLL.Edit(AuditStepCompleted);
            return AuditStepCompleted;
        }
        public AuditCategoryDIM NextInCompletedCategory(BatchAuditingRequest BatchAuditRequest, AuditFlowBatchDetail AuditFlowBatchDetail)
        {
            if (BatchAuditRequest.BatchAuditingRequestID != 0)
            {
                int? AuditCategoryID = auditFlowBatchDetailBLL.Find
               (x =>
                x.BatchAuditRequestFK == BatchAuditRequest.BatchAuditingRequestID &&
                x.IsActive &&
                x.IsDeleted == false &&
                x.IsComplete != true &&
                 x.BatchAuditFlowDetailsID != AuditFlowBatchDetail.BatchAuditFlowDetailsID)?.FirstOrDefault()?.AuditCategoryFK;
                return auditCategoryBLL.Find(x => x.AuditCategoriesID == AuditCategoryID).FirstOrDefault();

            }
            else
            {
                
             return auditCategoryBLL.Find(x => x.AuditCategoriesID == AuditFlowBatchDetail.AuditCategoryFK).FirstOrDefault();

            }
        }

        public bool IsTheLastAudtingStep(int BatchID)
        {
            var BatchAuditRequest = batchAuditingRequestBLL.Find(x => x.BatchingRequestFK == BatchID)?.FirstOrDefault();
            if (BatchAuditRequest != null)
            {
                if (auditFlowBatchDetailBLL.Find(x => x.BatchAuditRequestFK == BatchAuditRequest.BatchAuditingRequestID && x.IsComplete != true)?.Count() == 1)
                {
                    return true;
                }
            }
            return false; 

        }
        public void EditBatchAuditRequestStatusOnChangeOfFlow(BatchAuditingRequest BatchAuditRequest, AuditFlowBatchDetail AuditFlowBatchDetail)
        {
           // BatchAuditingRequest BatchAuditRequest=batchAuditingRequestBLL.Find(x=>x.BatchingRequestFK== BatchRequestID).FirstOrDefault();
            int? caseSwitch = NextInCompletedCategory(BatchAuditRequest, AuditFlowBatchDetail)?.AuditCategoriesID;
            switch (caseSwitch)
            {
                case 1:
                    BatchAuditRequest.BatchAuditingStatusFK = (int)Status.PendingMedical;
                    break;
                case 2:
                    BatchAuditRequest.BatchAuditingStatusFK = (int)Status.PendingFindincial;
                    break;
                case 3:
                    BatchAuditRequest.BatchAuditingStatusFK = (int)Status.PendingMi;
                    break;
                case 4:
                    BatchAuditRequest.BatchAuditingStatusFK = (int)Status.PendingReconilition;
                    break;

            }
            
                if (IsTheLastAudtingStep(BatchAuditRequest.BatchingRequestFK))
                {
                    BatchAuditRequest.BatchAuditingStatusFK = (int)Status.FinishedAudit;
                    var Batch = batchingRequestBLL.Find(x => x.BatchingRequestID == BatchAuditRequest.BatchingRequestFK).FirstOrDefault();
                    Batch.StatusFK = (int)Status.PendingDataEntryAssign;
                    Batch.NumberOfBatchClaimsAfterAudit = BatchAuditRequest.TotalNumberOfApprovedClaims;
                    batchingRequestBLL.Edit(Batch);
                }
            

            batchAuditingRequestBLL.Edit(BatchAuditRequest);
        }
        public string CheckAuditRequestStatus(int BatchID)
        {
            var BatchAudit = batchAuditingRequestBLL.Find(x => x.BatchingRequestFK == BatchID).FirstOrDefault();
            return Enum.GetName(typeof(Status), BatchAudit.BatchAuditingStatusFK).ToString();
        }

        

    }
}
