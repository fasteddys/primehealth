using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BNC.DAL.Repositories;
using BNC.Entities;
using BNC.DTOs;
using static BNC.BLL.StaticData.StaticEnums;
using BNC.DTOs.Business;
using BNC.BLL.Logic.Shared_Logic;

namespace BNC.BLL.Logic.Tables
{
    public class BatchingRequestBLL : Repository<BatchingRequest>
    {
        DateTime creationDate;
        BatchingFilterationDetailsBLL batchingFilterationDetailsBLL;
        BatchAuditingRequestBLL batchAuditingRequestBLL;
        FinancialAuditRequestBLL financialAuditRequestBLL;
        MedicalAuditRequestBLL medicalAuditRequestBLL;
        FilterationRequestBLL FilterationRequestBLL;
        FilterationCategoriesBLL FilterationCategoriesBLL;
        MapperBLL MapperBLL;
        InsuranceSystemBLL InsuranceSystemBLL;
        UserBLL UserBLL;
        AuditCategoryBLL auditCategoryBLL;
        public BatchingRequestBLL(BNCEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
            batchingFilterationDetailsBLL = new BatchingFilterationDetailsBLL(Context, CreationDate);
            batchAuditingRequestBLL = new BatchAuditingRequestBLL(Context, CreationDate);
            financialAuditRequestBLL = new FinancialAuditRequestBLL(Context, CreationDate);
            medicalAuditRequestBLL = new MedicalAuditRequestBLL(Context, CreationDate);
            MapperBLL = new MapperBLL(Context, CreationDate);
            FilterationRequestBLL = new FilterationRequestBLL(Context, CreationDate);
            FilterationCategoriesBLL = new FilterationCategoriesBLL(Context, CreationDate);
            InsuranceSystemBLL = new InsuranceSystemBLL(Context, CreationDate);
            UserBLL = new UserBLL(Context, CreationDate);
            auditCategoryBLL = new AuditCategoryBLL(Context, CreationDate);

        }
        public BatchingRequest AddBatchingRequest(BatchingRequestDTO BatchingInputDTO)
        {
            BatchingRequest batchingRequest = new BatchingRequest
            {
                //input
                NumberOfBatchClaims = BatchingInputDTO.NumberOfBatchClaims,
                BatchSystemFK = BatchingInputDTO.BatchSystemFK,
                BatchNumber = BatchingInputDTO.BatchNumber,
                BatchingOfficerFK = BatchingInputDTO.BatchingOfficerFK,
                //from developer
                IsActive = true,
                IsDeleted = false,
                CreationDate = creationDate,
                BatchingFilterationDetailsFK = BatchingInputDTO.BatchingFilterationDetailsFK,
                StatusFK = (int)Status.NewBatch,
                FilterationCategoryFK = batchingFilterationDetailsBLL.Find(x => x.BatchingFilterationDetailID == BatchingInputDTO.BatchingFilterationDetailsFK)
                 .FirstOrDefault().FilterationCategoryFK
            };
            if (BatchingInputDTO.BatchingOfficerComment != "")
            {
                batchingRequest.BatchingOfficerComment = BatchingInputDTO.BatchingOfficerComment;
            }
            this.Add(batchingRequest);
            return batchingRequest;
        }

        public int getNumberOfClaimsAtBatchingRequest(int BatchingRequestID)
        {
            BatchingRequest batchingRequest = this.Find(id => id.BatchingRequestID == BatchingRequestID).FirstOrDefault();
            if (batchingRequest != null)
            {
                return (int) batchingRequest.NumberOfBatchClaims;

            }
            else
            {
                return -1;
            }
        }
        public int? getNumberOfClaimsAtBatchAuditRequest(int BatchingAuditRequestID)
        {
            var batchingRequest = batchAuditingRequestBLL.Find(id => id.BatchingRequestFK == BatchingAuditRequestID).FirstOrDefault();
            if (batchingRequest != null)
            {
                return batchingRequest.TotalNumberOfApprovedClaims;

            }
            else
            {
                return -1;
            }
        }

        public BatchAuditingRequest EditNumberOfBatchingClaimsOfMedicalAudit(MedicalAuditDTO CategoryClaimsCount)
        {
            var BatchAuditRequest = batchAuditingRequestBLL.Find(x => x.BatchingRequestFK == CategoryClaimsCount.BatchRequestFK).FirstOrDefault();
            if (CategoryClaimsCount.NumberOfPendingClaims == 0)
            {
                BatchAuditRequest.TotalNumberOfApprovedClaims = BatchAuditRequest.TotalNumberOfApprovedClaims - CategoryClaimsCount.NumberOfRejectedClaims;
                BatchAuditRequest.TotalNumberOfRejectedClaims = BatchAuditRequest.TotalNumberOfRejectedClaims+ CategoryClaimsCount.NumberOfRejectedClaims;

            }
            else
            {
                BatchAuditRequest.IsLocked = true;
                BatchAuditRequest.HaveSpecialApproval = true;
                var BatchRequest = Find(x => x.BatchingRequestID == CategoryClaimsCount.BatchRequestFK).FirstOrDefault();
                BatchRequest.IsLocked = false;
                Edit(BatchRequest);
            }
            batchAuditingRequestBLL.Edit(BatchAuditRequest);
            return BatchAuditRequest;

        }
        public BatchAuditingRequest EditNumberOfBatchingClaimsOfFinancialAudit(FinancialAuditDTO CategoryClaimsCount)
        {
            var BatchAuditRequest = batchAuditingRequestBLL.Find(x => x.BatchingRequestFK == CategoryClaimsCount.BatchRequestFK).FirstOrDefault();
            if (CategoryClaimsCount.NumberOfPendingClaims == 0)
            {
                BatchAuditRequest.TotalNumberOfApprovedClaims = BatchAuditRequest.TotalNumberOfApprovedClaims - CategoryClaimsCount.NumberOfRejectedClaims;
                BatchAuditRequest.TotalNumberOfRejectedClaims = BatchAuditRequest.TotalNumberOfRejectedClaims+CategoryClaimsCount.NumberOfRejectedClaims;

            }
            else
            {
                BatchAuditRequest.IsLocked = true;
                BatchAuditRequest.HaveSpecialApproval = true;
                var BatchRequest = Find(x => x.BatchingRequestID == CategoryClaimsCount.BatchRequestFK).FirstOrDefault();
                BatchRequest.IsLocked = true;
                
                Edit(BatchRequest);
            }
            batchAuditingRequestBLL.Edit(BatchAuditRequest);
            return BatchAuditRequest;
        }
        public void UpdateBatchAuditStatusInFindincialAudit(BatchAuditingRequest BatchAuditRequest)
        {
            BatchAuditRequest.BatchAuditingStatusFK = (int)Status.PendingTransferFromFindincialAudit;
            batchAuditingRequestBLL.Edit(BatchAuditRequest);
        }

        public void UpdateBatchAuditStatusInMidecalAudit(BatchAuditingRequest BatchAuditRequest)
        {
            BatchAuditRequest.BatchAuditingStatusFK = (int)Status.PendingTransferFromMidecalAudit;
            batchAuditingRequestBLL.Edit(BatchAuditRequest);
        }

        public void UpdateBatchAuditStatusInReconilitionAudit(BatchAuditingRequest BatchAuditRequest)
        {
            BatchAuditRequest.BatchAuditingStatusFK = (int)Status.PendingTransferFromReconilitionAudit;
            batchAuditingRequestBLL.Edit(BatchAuditRequest);
        }

        public void UpdateBatchAuditStatusInMIAudit(BatchAuditingRequest BatchAuditRequest)
        {
            BatchAuditRequest.BatchAuditingStatusFK = (int)Status.PendingTransferFromMIAudit;
            batchAuditingRequestBLL.Edit(BatchAuditRequest);
        }
        public void ApprovePendingRequestsInMedicalAuditByProviders(FinancialAuditDTO CategoryClaimsCount)
        {
            var medicalAuditRequest = medicalAuditRequestBLL.Find(x => x.BatchAuditRequestFK == CategoryClaimsCount.BatchAuditRequestFK).FirstOrDefault();
            medicalAuditRequest.NumberOfApprovedClaimsBySP = CategoryClaimsCount.NumberOfApprovedClaims;
            medicalAuditRequest.NumberOfRejectedClaimsBySP = CategoryClaimsCount.NumberOfRejectedClaims;
            medicalAuditRequest.TotalNumberOfApprovedClaims = medicalAuditRequest.NumberOfApprovedClaimsBySP
                                                              +
                                                              medicalAuditRequest.NumberOfApprovedClaims;
            medicalAuditRequest.TotalNumberOfRejectedClaims = medicalAuditRequest.NumberOfRejectedClaimsBySP
                                                          +
                                                          medicalAuditRequest.NumberOfRejectedClaims;

            var batchAuditingRequest = batchAuditingRequestBLL.Find(x => x.BatchAuditingRequestID == CategoryClaimsCount.BatchAuditRequestFK).FirstOrDefault();
            batchAuditingRequest.TotalNumberOfApprovedClaims = batchAuditingRequest.TotalNumberOfApprovedClaims + medicalAuditRequest.TotalNumberOfApprovedClaims;
            batchAuditingRequest.TotalNumberOfRejectedClaims = batchAuditingRequest.TotalNumberOfRejectedClaims + medicalAuditRequest.TotalNumberOfRejectedClaims;
            batchAuditingRequest.IsLocked = false;
            batchAuditingRequestBLL.Edit(batchAuditingRequest);
            medicalAuditRequestBLL.Edit(medicalAuditRequest);


        }
        public void ApprovePendingRequestsInFinancialAuditByProviders(FinancialAuditDTO CategoryClaimsCount)
        {
          var FinancialAuditRequest=  financialAuditRequestBLL.Find(x => x.BatchAuditRequestFK == CategoryClaimsCount.BatchAuditRequestFK).FirstOrDefault();
            FinancialAuditRequest.NumberOfApprovedClaimsBySP = CategoryClaimsCount.NumberOfApprovedClaims;
            FinancialAuditRequest.NumberOfRejectedClaimsBySP = CategoryClaimsCount.NumberOfRejectedClaims;

            FinancialAuditRequest.TotalNumberOfApprovedClaims = FinancialAuditRequest.NumberOfApprovedClaimsBySP
                                                     +
                                                     FinancialAuditRequest.NumberOfApprovedClaims;
            FinancialAuditRequest.TotalNumberOfRejectedClaims = FinancialAuditRequest.NumberOfRejectedClaimsBySP
                                                          +
                                                          FinancialAuditRequest.NumberOfRejectedClaims;

            var batchAuditingRequest = batchAuditingRequestBLL.Find(x => x.BatchAuditingRequestID == CategoryClaimsCount.BatchAuditRequestFK).FirstOrDefault();
            batchAuditingRequest.TotalNumberOfApprovedClaims = batchAuditingRequest.TotalNumberOfApprovedClaims + FinancialAuditRequest.TotalNumberOfApprovedClaims;
            batchAuditingRequest.TotalNumberOfRejectedClaims = batchAuditingRequest.TotalNumberOfRejectedClaims + FinancialAuditRequest.TotalNumberOfRejectedClaims;
            batchAuditingRequest.IsLocked = false;
            batchAuditingRequestBLL.Edit(batchAuditingRequest);
            financialAuditRequestBLL.Edit(FinancialAuditRequest);

        }
        //-------------------------------------------------------------------------------------------------------
        public List<BatchingFilterationDetailDTO>getAllPendingBatchingRequestsAtFilitrationBatching()
        {
            List<BatchingFilterationDetailDTO> tempBatchingFilterationDetailList = new List<BatchingFilterationDetailDTO>(); 
            var batchingFilterationDetailsList= batchingFilterationDetailsBLL.Find(bfd => bfd.BatchingFilterationDetailStatusFK == (int)Status.PendingBatching).ToList();
            foreach (var batchingFilterationDetailsRequest in batchingFilterationDetailsList)
            {
                var tempBatchingFilterationRequest = MapperBLL.convertBatchingFilterationRequestToDTO(batchingFilterationDetailsRequest);
                tempBatchingFilterationRequest.CategoryName = FilterationCategoriesBLL.getFilterationCategoryName(tempBatchingFilterationRequest.FilterationCategoryID);
                tempBatchingFilterationRequest.ReceivingRequestIFK = FilterationRequestBLL.getReceivingRequestID(tempBatchingFilterationRequest.FilterationRequestID);
                tempBatchingFilterationDetailList.Add(tempBatchingFilterationRequest);
            }
            return tempBatchingFilterationDetailList;
        }//Pending Page 
        public int getAllPendingBatchingRequestsAtFilitrationBatchingCount()
        {
            return batchingFilterationDetailsBLL.Count(bfd => bfd.BatchingFilterationDetailStatusFK == (int)Status.PendingBatching);
        
        }//count Pending  
        //-------------------------------------------------------------------------------------------------------
        public List<BatchingFilterationDetailDTO> getAllUnderPatchingProcessRequestsAtFilitrationBatching()
        {
            List<BatchingFilterationDetailDTO> tempBatchingFilterationDetailList = new List<BatchingFilterationDetailDTO>();
            var batchingFilterationDetailsList = batchingFilterationDetailsBLL.Find(bfd => bfd.BatchingFilterationDetailStatusFK == (int)Status.UnderBatchingProcess).ToList();
            foreach (var batchingFilterationDetailsRequest in batchingFilterationDetailsList)
            {
                var tempBatchingFilterationRequest = MapperBLL.convertBatchingFilterationRequestToDTO(batchingFilterationDetailsRequest);
                tempBatchingFilterationRequest.CategoryName = FilterationCategoriesBLL.getFilterationCategoryName(tempBatchingFilterationRequest.FilterationCategoryID);
                tempBatchingFilterationRequest.ReceivingRequestIFK = FilterationRequestBLL.getReceivingRequestID(tempBatchingFilterationRequest.FilterationRequestID);
                tempBatchingFilterationDetailList.Add(tempBatchingFilterationRequest);
            }
            return tempBatchingFilterationDetailList;
        } //Under Patching Process Page 
        public int getAllUnderPatchingProcessRequestsAtFilitrationBatchingCount()
        {
            return batchingFilterationDetailsBLL.Count(bfd => bfd.BatchingFilterationDetailStatusFK == (int)Status.UnderBatchingProcess);

        }//count atching Process   
        //-------------------------------------------------------------------------------------------------------
        public List<BatchingFilterationDetailDTO> getAllFinishedBatchingRequestsAtFilitrationBatching()
        {
            List<BatchingFilterationDetailDTO> tempBatchingFilterationDetailList = new List<BatchingFilterationDetailDTO>();
            var batchingFilterationDetailsList = batchingFilterationDetailsBLL.Find(bfd => bfd.BatchingFilterationDetailStatusFK == (int)Status.FinishedBatching).ToList();
            foreach (var batchingFilterationDetailsRequest in batchingFilterationDetailsList)
            {
                var tempBatchingFilterationRequest = MapperBLL.convertBatchingFilterationRequestToDTO(batchingFilterationDetailsRequest);
                tempBatchingFilterationRequest.CategoryName = FilterationCategoriesBLL.getFilterationCategoryName(tempBatchingFilterationRequest.FilterationCategoryID);
                tempBatchingFilterationRequest.ReceivingRequestIFK = FilterationRequestBLL.getReceivingRequestID(tempBatchingFilterationRequest.FilterationRequestID);
                tempBatchingFilterationDetailList.Add(tempBatchingFilterationRequest);
            }
            return tempBatchingFilterationDetailList;
        }//FinishedBatching Page
        public int getAllFinishedBatchingRequestsAtFilitrationBatchingCount()
        {
            return batchingFilterationDetailsBLL.Count(bfd => bfd.BatchingFilterationDetailStatusFK == (int)Status.FinishedBatching);

        }//count FinishedBatching  
        //-------------------------------------------------------------------------------------------------------
        public List<BatchingFilterationDetailDTO> getAllFilitrationBatchingRequests()//All Filitration Batching Requests Page
        {
            List<BatchingFilterationDetailDTO> tempBatchingFilterationDetailList = new List<BatchingFilterationDetailDTO>();
            foreach (var PendingBatchingRequest in GetBatchingFilterationDetails())
            {
                tempBatchingFilterationDetailList.Add(PendingBatchingRequest);
            }
            
            return tempBatchingFilterationDetailList;
        }
        public int getAllFilitrationBatchingRequestsCount()
        {
            return getAllFinishedBatchingRequestsAtFilitrationBatchingCount() + getAllUnderPatchingProcessRequestsAtFilitrationBatchingCount() + getAllPendingBatchingRequestsAtFilitrationBatchingCount();

        }//count Filitration Batching Requests  
        //-------------------------------------------------------------------------------------------------------
        public List<BatchingRequestDTO>getMyBatching(int userFk)
        {
            var myBatchingRequests = Find(br => br.BatchingOfficerFK == userFk);
            List<BatchingRequestDTO> tempBatchingRequestList = new List<BatchingRequestDTO>();
            foreach (var myBatchingRequest in myBatchingRequests)
            {
                var tempBatchingRequest = MapperBLL.convertBatchingRequestToDTO(myBatchingRequest);
                tempBatchingRequest.BatchSystemName = InsuranceSystemBLL.getInsuranceSystemName(tempBatchingRequest.BatchSystemFK);
                tempBatchingRequest.OfficerName = UserBLL.getUserName(tempBatchingRequest.BatchingOfficerFK);
                tempBatchingRequest.FilterationCategoryName = FilterationCategoriesBLL.getFilterationCategoryName((int)tempBatchingRequest.FilterationCategoryFK);
                tempBatchingRequestList.Add(tempBatchingRequest);
            }
            return tempBatchingRequestList;
        }//MyBatching Page
        public int getMyBatchingCount(int userFk)
        {
            return Find(br => br.BatchingOfficerFK == userFk).ToList().Count; ;

        }//MyBatching count
        //-------------------------------------------------------------------------------------------------------
        public List<BatchingRequestDTO> getBatchingCreated()
        {
            var myBatchingRequests = Find(br => br.StatusFK ==(int)Status.PendingAudit);//PendingAudit=8
            List<BatchingRequestDTO> tempBatchingRequestList = new List<BatchingRequestDTO>();
            foreach (var myBatchingRequest in myBatchingRequests)
            {
                var tempBatchingRequest = MapperBLL.convertBatchingRequestToDTO(myBatchingRequest);
                tempBatchingRequest.BatchSystemName = InsuranceSystemBLL.getInsuranceSystemName(tempBatchingRequest.BatchSystemFK);
                tempBatchingRequest.OfficerName = UserBLL.getUserName(tempBatchingRequest.BatchingOfficerFK);
                tempBatchingRequest.FilterationCategoryName = FilterationCategoriesBLL.getFilterationCategoryName((int)tempBatchingRequest.FilterationCategoryFK);
                tempBatchingRequestList.Add(tempBatchingRequest);
            }
            return tempBatchingRequestList;
        }//Created page
        public int getBatchingCreatedCount()
        {
            return Find(br => br.StatusFK == (int)Status.PendingAudit).ToList().Count; ;

        }//Created Count
         //-------------------------------------------------------------------------------------------------------
        public BatchingRequestDTO getBatchData(int BatchID)
        {
            var BatchingRequest = Find(br => br.BatchingRequestID == BatchID).FirstOrDefault();          
            return new BatchingRequestDTO {
                BatchNumber = BatchingRequest.BatchNumber,
                BatchingOfficerComment = BatchingRequest.BatchingOfficerComment,
                BatchSystemFK = BatchingRequest.BatchSystemFK,
                BatchingOfficerFK = BatchingRequest.BatchingOfficerFK,
                BatchingRequestID = BatchingRequest.BatchingRequestID,
                CreationDate = BatchingRequest.CreationDate,
                FilterationCategoryFK = BatchingRequest.FilterationCategoryFK,
                NumberOfBatchClaims = BatchingRequest.NumberOfBatchClaims,
                BatchingFilterationDetailsFK = BatchingRequest.BatchingFilterationDetailsFK,
                StatusFK= BatchingRequest.StatusFK
            };
        }
        public List<BatchingFilterationDetailDTO> GetBatchingFilterationDetails()
        {
            List<BatchingFilterationDetailDTO> BatchingFilterationDetails = new List<BatchingFilterationDetailDTO>();
            foreach (var item in batchingFilterationDetailsBLL.GetAll())
            {
                BatchingFilterationDetails.Add(new BatchingFilterationDetailDTO
                {
                    BatchingFilterationDetailID = item.BatchingFilterationDetailID,
                    FilterationCategoryID = item.FilterationCategoryFK,
                    FilterationRequestID = item.FilterationRequestFK,
                    RemainingClaimsCount = item.RemainingClaimsCount,
                    TotalClaimsCount = item.TotalClaimsCount,
                     CategoryName=auditCategoryBLL.Find(x=>x.AuditCategoriesID== item.FilterationCategoryFK).FirstOrDefault().AuditCategoriesName
                });

            }
            return BatchingFilterationDetails;
        }



    }

}
