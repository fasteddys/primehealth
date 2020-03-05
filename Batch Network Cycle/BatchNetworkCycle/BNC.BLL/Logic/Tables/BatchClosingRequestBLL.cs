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
using BNC.BLL.StaticData;
using BNC.BLL.Logic.Shared_Logic;

namespace BNC.BLL.Logic.Tables
{
    public class BatchClosingRequestBLL : Repository<BatchClosingRequest>
    {
        DateTime creationDate;

        public BatchClosingRequestBLL(BNCEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
        }

        public void AddBatchClosingRequest(int DataEntryAdminstrationRequestID)
        {
            BatchClosingRequest batchClosingRequest = new BatchClosingRequest {
                DataEntryAdminstrationRequestFK = DataEntryAdminstrationRequestID,
                BatchClosingStatusFK = (int)Status.NewClosingRequest,
                IsActive = true,               
                IsDeleted = false,
                CreationDate = creationDate
            };
            Add(batchClosingRequest);
        }

        public List<BatchClosingRequestDTO> GetAllBatchClosingRequest()
        {
            List<BatchClosingRequest> batchClosingRequest = Find(x => x.BatchClosingStatusFK == (int)Status.NewClosingRequest).ToList();

            List<BatchClosingRequestDTO> batchClosingRequestDTOs = new List<BatchClosingRequestDTO>();
            foreach (var items in batchClosingRequest)
            {
                batchClosingRequestDTOs.Add(new BatchClosingRequestDTO
                {
                    BatchClosingRequestID = items.BatchClosingRequestID,
                    DataEntryAdminstrationRequestFK = items.DataEntryAdminstrationRequestFK,
                    BatchClosingStatusName = ((StaticEnums.Status)Enum.Parse(typeof(StaticEnums.Status), items.BatchClosingStatusFK.ToString())).ToString(),
                    BatchClosingStatusFK = items.BatchClosingStatusFK,

                });
            }

            return batchClosingRequestDTOs;
        }
        
        public void AssignBatchClosingRequest(BatchClosingRequestDTO batchClosingRequestDTO)
        {
            BatchClosingRequest batchClosingRequest = Find(x => x.BatchClosingRequestID == batchClosingRequestDTO.BatchClosingRequestID).FirstOrDefault();
            batchClosingRequest.BatchClosingStatusFK = (int)Status.AssignToClosing; //Change
            batchClosingRequest.ClosingOfficerAssigneeFK = batchClosingRequestDTO.ClosingOfficerAssigneeFK;
            batchClosingRequest.ClosingOfficerAssignedTime = creationDate;
            batchClosingRequest.ClosingOfficerAssignedComment = batchClosingRequestDTO.ClosingOfficerAssignedComment;

            Edit(batchClosingRequest);
        }
        public void ConfirmBatchClosingRequest(BatchClosingRequestDTO batchClosingRequestDTO)
        {
            BatchClosingRequest batchClosingRequest = Find(x => x.BatchClosingRequestID == batchClosingRequestDTO.BatchClosingRequestID).FirstOrDefault();
            batchClosingRequest.BatchClosingStatusFK = (int)Status.ClosingConfirmReceiving; //Change
            batchClosingRequest.ConfirmReceivingTime = creationDate;
            batchClosingRequest.ConfirmReceivingComment = batchClosingRequestDTO.ConfirmReceivingComment;

            Edit(batchClosingRequest);
        }
        public void FinishBatchClosingRequest(BatchClosingRequestDTO batchClosingRequestDTO)
        {
            BatchClosingRequest batchClosingRequest = Find(x => x.BatchClosingRequestID == batchClosingRequestDTO.BatchClosingRequestID).FirstOrDefault();
            batchClosingRequest.BatchClosingStatusFK = (int)Status.ClosingPendingTransfer; //Change
            batchClosingRequest.FinishedReviewingTime = creationDate;
            batchClosingRequest.FinishedReviewingComment = batchClosingRequestDTO.FinishedReviewingComment;

            Edit(batchClosingRequest);
        }
        public BatchClosingRequestDTO GetRequestData(int ID)
        {
            BatchClosingRequest batchClosingRequest = Find(x => x.BatchClosingRequestID == ID).FirstOrDefault();

            BatchClosingRequestDTO batchClosingRequestDTO = new BatchClosingRequestDTO {
                ClosingOfficerAssignedTime = batchClosingRequest.ClosingOfficerAssignedTime,
                ConfirmReceivingTime = batchClosingRequest.ConfirmReceivingTime,
                FinishedReviewingTime = batchClosingRequest.FinishedReviewingTime,
                TransferredBackToAdminTime = batchClosingRequest.TransferredBackToAdminTime,
                BatchClosingRequestID= batchClosingRequest.BatchClosingRequestID,
                ClosingOfficerAssignedComment= batchClosingRequest.ClosingOfficerAssignedComment
            };
            return batchClosingRequestDTO;
        }        
    }
}
