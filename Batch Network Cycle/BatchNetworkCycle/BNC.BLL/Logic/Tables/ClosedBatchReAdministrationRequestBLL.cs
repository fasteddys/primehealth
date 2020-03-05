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
using BNC.BLL.StaticData;

namespace BNC.BLL.Logic.Tables
{
    public class ClosedBatchReAdministrationRequestBLL : Repository<ClosedBatchReAdministrationRequest>
    {
        BatchClosingRequestBLL batchClosingRequestBLL;
        DataEntryAdminstrationRequestBLL dataEntryAdminstrationRequestBLL;
        DateTime creationDate;

        public ClosedBatchReAdministrationRequestBLL(BNCEntities Context, DateTime CreationDate) : base(Context)
        {
            batchClosingRequestBLL = new BatchClosingRequestBLL(Context, CreationDate);
            dataEntryAdminstrationRequestBLL = new DataEntryAdminstrationRequestBLL(Context, CreationDate);
            creationDate = CreationDate;
        }
        public int GetTotalClaimsCountInBatchClosingReAdministrationRequest(int ID)
        {
            int DataEntryRequestID = batchClosingRequestBLL.Find(x => x.BatchClosingRequestID == ID).FirstOrDefault().DataEntryAdminstrationRequestFK;
            int TotalClaimsNumber = dataEntryAdminstrationRequestBLL.Find(x => x.DataEntryAdminstrationRequestID == DataEntryRequestID).FirstOrDefault().OriginalApprovedClaimsNumber.Value;
            return TotalClaimsNumber;
        }
        public List<BatchClosingReAdministrationRequestDTO> GetAllBatchClosingReAdministrationRequest()
        {
            List<ClosedBatchReAdministrationRequest> batchClosingReAdministrationRequest = Find(x => x.ReAdministrationStatusFK == (int)Status.ReAdministrationNewRequest).ToList();

            List<BatchClosingReAdministrationRequestDTO> batchClosingReAdministrationRequestDTOs = new List<BatchClosingReAdministrationRequestDTO>();
            foreach (var items in batchClosingReAdministrationRequest)
            {
                batchClosingReAdministrationRequestDTOs.Add(new BatchClosingReAdministrationRequestDTO
                {
                    BatchClosingReAdministrationRequestID = items.ClosedBatchReAdmissionRequestID,
                    ReAdministrationStatusFK = items.ReAdministrationStatusFK,
                    ReAdministrationStatusName = ((StaticEnums.Status)Enum.Parse(typeof(StaticEnums.Status), items.ReAdministrationStatusFK.ToString())).ToString(),
                    TotalClaimsCount = GetTotalClaimsCountInBatchClosingReAdministrationRequest(items.BatchClosingRequestFK.Value)
                });
            }

            return batchClosingReAdministrationRequestDTOs;
        }
        public void AssignBatchClosingReAdministrationRequest(BatchClosingReAdministrationRequestDTO batchClosingReAdministrationRequestDTO)
        {
            ClosedBatchReAdministrationRequest closedBatchReAdministrationRequest = Find(x => x.ClosedBatchReAdmissionRequestID == batchClosingReAdministrationRequestDTO.BatchClosingReAdministrationRequestID).FirstOrDefault();
            closedBatchReAdministrationRequest.ReAdministrationStatusFK = (int)Status.ReAdministrationAssign;
            closedBatchReAdministrationRequest.AssignedByAdminFK = batchClosingReAdministrationRequestDTO.AssignedByAdminFK;
            closedBatchReAdministrationRequest.AssignedByAdminComment = batchClosingReAdministrationRequestDTO.AssignedByAdminComment;
            closedBatchReAdministrationRequest.ReAdministrationOfficerAssignedTime = creationDate;

            Edit(closedBatchReAdministrationRequest);
        }
        public void ConfirmBatchClosingReAdministrationRequest(BatchClosingReAdministrationRequestDTO batchClosingReAdministrationRequestDTO)
        {
            ClosedBatchReAdministrationRequest closedBatchReAdministrationRequest = Find(x => x.ClosedBatchReAdmissionRequestID == batchClosingReAdministrationRequestDTO.BatchClosingReAdministrationRequestID).FirstOrDefault();
            closedBatchReAdministrationRequest.ReAdministrationStatusFK = (int)Status.ReAdministrationConfirmReceving;
            closedBatchReAdministrationRequest.ConfirmedReceivingComment = batchClosingReAdministrationRequestDTO.ConfirmedReceivingComment;
            closedBatchReAdministrationRequest.ConfirmedReceivingOn = creationDate;
            
            Edit(closedBatchReAdministrationRequest);
        }
        public void FinishBatchClosingReAdministrationRequest(BatchClosingReAdministrationRequestDTO batchClosingReAdministrationRequestDTO)
        {
            Random rnd = new Random();
            int rundNumber = rnd.Next(9000, 10000);

            ClosedBatchReAdministrationRequest closedBatchReAdministrationRequest = Find(x => x.ClosedBatchReAdmissionRequestID == batchClosingReAdministrationRequestDTO.BatchClosingReAdministrationRequestID).FirstOrDefault();
            closedBatchReAdministrationRequest.ReAdministrationStatusFK = (int)Status.ReAdministrationFinished;
            closedBatchReAdministrationRequest.ArchivingSystemTicketNo = rundNumber;
            closedBatchReAdministrationRequest.FinalClosureComment = batchClosingReAdministrationRequestDTO.FinalClosureComment;
            closedBatchReAdministrationRequest.FinalClosureTime = creationDate;

            batchClosingReAdministrationRequestDTO.ArchivingSystemTicketNo = rundNumber;
            Edit(closedBatchReAdministrationRequest);
        }       
        public BatchClosingReAdministrationRequestDTO GetRequestData(int ID)
        {
            ClosedBatchReAdministrationRequest closedBatchReAdministrationRequest = Find(x => x.ClosedBatchReAdmissionRequestID == ID).FirstOrDefault();

            BatchClosingReAdministrationRequestDTO batchClosingReAdministrationRequestDTO = new BatchClosingReAdministrationRequestDTO {
                ReAdministrationOfficerAssignedTime = closedBatchReAdministrationRequest.ReAdministrationOfficerAssignedTime,
                ConfirmedReceivingOn = closedBatchReAdministrationRequest.ConfirmedReceivingOn,
                FinalClosureTime = closedBatchReAdministrationRequest.FinalClosureTime,
                 BatchClosingReAdministrationRequestID= closedBatchReAdministrationRequest.ClosedBatchReAdmissionRequestID
            };
            return batchClosingReAdministrationRequestDTO;
        }
    }
}

