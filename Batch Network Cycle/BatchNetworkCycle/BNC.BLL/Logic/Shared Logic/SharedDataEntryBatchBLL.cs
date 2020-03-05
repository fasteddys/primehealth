using BNC.BLL.Logic.Tables;
using BNC.BLL.StaticData;
using BNC.DTOs.Business;
using BNC.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BNC.BLL.StaticData.StaticEnums;

namespace BNC.BLL.Logic.Shared_Logic
{
    public class SharedDataEntryBatchBLL
    {
        DataEntryAdminstrationRequestBLL dataEntryAdminstrationRequestBLL;
        DataEntryBatchAssigningRequestBLL dataEntryBatchAssigningRequestBLL;
        BatchClosingRequestBLL batchClosingRequestBLL;
        DateTime creationDate;
        BatchAuditingRequestBLL batchAuditingRequestBLL;
        ClosedBatchReAdministrationRequestBLL closedBatchReAdministrationRequestBLL;

        public SharedDataEntryBatchBLL(BNCEntities Context, DateTime CreationDate)
        {
            dataEntryAdminstrationRequestBLL = new DataEntryAdminstrationRequestBLL(Context, CreationDate);
            dataEntryBatchAssigningRequestBLL = new DataEntryBatchAssigningRequestBLL(Context, CreationDate);
            batchClosingRequestBLL = new BatchClosingRequestBLL(Context, CreationDate);
            creationDate = CreationDate;
            batchAuditingRequestBLL = new BatchAuditingRequestBLL(Context, CreationDate);
            closedBatchReAdministrationRequestBLL = new ClosedBatchReAdministrationRequestBLL(Context, CreationDate);
        }
        public void AddDataEntryBatchAssigningRequest(DataEntryBatchAssigningRequestDTO dataEntryBatchAssigningRequestDTO)
        {
            DataEntryAdminstrationRequest dataEntryAdminstrationRequest = dataEntryAdminstrationRequestBLL.Find(x => x.DataEntryAdminstrationRequestID == dataEntryBatchAssigningRequestDTO.DataEntryAdministrationRequestFK).FirstOrDefault();
            if (dataEntryAdminstrationRequest.RemainingUnassignedNumberOfClaims == 0)
                throw new Exception("This Batch hasn't UnAssigned Claims");

            dataEntryAdminstrationRequest.RemainingUnassignedNumberOfClaims = dataEntryAdminstrationRequest.RemainingUnassignedNumberOfClaims - dataEntryBatchAssigningRequestDTO.AssignedClaimsNumber;
            dataEntryAdminstrationRequest.ModificationDate = DateTime.Now;      
            
            if (dataEntryAdminstrationRequest.RemainingUnassignedNumberOfClaims < 0)
                throw new Exception("You Want To Assigne Wrong Value");
            if (dataEntryAdminstrationRequest.RemainingUnassignedNumberOfClaims == 0)
                dataEntryAdminstrationRequest.DataEntryAdministrationStatusFK = (int)Status.DataEntryAdminAssign; //Update status to finish

            dataEntryAdminstrationRequestBLL.Edit(dataEntryAdminstrationRequest);

            DataEntryBatchAssigningRequest dataEntryBatchAssigningRequest = new DataEntryBatchAssigningRequest
            {               
                AssignedClaimsNumber = dataEntryBatchAssigningRequestDTO.AssignedClaimsNumber,
                AssignedByAdminTime = creationDate,
                DataEntryBatchAssigningStatusFK = (int)Status.PendingDataEntryConfirm,
                DataEntryAdministrationRequestFK = dataEntryBatchAssigningRequestDTO.DataEntryAdministrationRequestFK,
                DataEntryOfficerFK = dataEntryBatchAssigningRequestDTO.DataEntryOfficerFK,
                DataEntryAdminComment = dataEntryBatchAssigningRequestDTO.DataEntryAdminComment,
                IsActive = true,
                IsDeleted = false,
                CreationDate = creationDate
                                
            };
            dataEntryBatchAssigningRequestBLL.Add(dataEntryBatchAssigningRequest);
        }
        //Finish the DataEntryAdminstration Request when last DataEntry Assigned request is finished
        public void UpdateDataEntryAdminstrationRequestIfFinished(int DataEntryAdminstrationRequestID, int DataEntryBatchAssigningRequestID)
        {
            //int totalClaimsNumber = 0;
            //bool check = false;
            DataEntryAdminstrationRequest dataEntryAdminstrationRequest = dataEntryAdminstrationRequestBLL.Find(x => x.DataEntryAdminstrationRequestID == DataEntryAdminstrationRequestID).FirstOrDefault();
            List<DataEntryBatchAssigningRequest> dataEntryBatchAssigningRequests = dataEntryBatchAssigningRequestBLL.Find(x => x.DataEntryAdministrationRequestFK == DataEntryAdminstrationRequestID && x.DataEntryOfficerFinishedOnSystemTime == null).ToList();

            if (dataEntryBatchAssigningRequests.Count == 1)
            {
                foreach (var items in dataEntryBatchAssigningRequests)
                {
                    //totalClaimsNumber += items.AssignedClaimsNumber;
                    if (items.DataEntryOfficerFinishedOnSystemTime == null && items.DataEntryBatchAssigningRequestID != DataEntryBatchAssigningRequestID)
                        return;
                }

                dataEntryAdminstrationRequest.DataEntryAdministrationStatusFK = (int)Status.PendingTransferToClosingTeam; //Change status
                dataEntryAdminstrationRequest.DateEntryFinishProcessingTime = DateTime.Now;
                //dataEntryAdminstrationRequest.TransferredToClosingTime = DateTime.Now;

                AddBatchClosingRequest(dataEntryAdminstrationRequest.DataEntryAdminstrationRequestID);
                dataEntryAdminstrationRequestBLL.Edit(dataEntryAdminstrationRequest);
            }           
        }
        public void DataEntryOfficerFinished(DataEntryBatchAssigningRequestDTO dataEntryBatchAssigningRequestDTO)
        {
            DataEntryBatchAssigningRequest dataEntryBatchAssigningRequest = dataEntryBatchAssigningRequestBLL.Find(x => x.DataEntryBatchAssigningRequestID == dataEntryBatchAssigningRequestDTO.DataEntryBatchAssigningRequestID).FirstOrDefault();
            dataEntryBatchAssigningRequest.DataEntryOfficerFinishedComment = dataEntryBatchAssigningRequestDTO.DataEntryOfficerFinishedComment;
            dataEntryBatchAssigningRequest.DataEntryOfficerFinishedOnSystemTime = creationDate;
            dataEntryBatchAssigningRequest.DataEntryOfficerFK = dataEntryBatchAssigningRequestDTO.DataEntryOfficerFK;
            dataEntryBatchAssigningRequest.DataEntryBatchAssigningStatusFK = (int)Status.DataEntryAssignFinished; //Change Status
            UpdateDataEntryAdminstrationRequestIfFinished(dataEntryBatchAssigningRequest.DataEntryAdministrationRequestFK, dataEntryBatchAssigningRequest.DataEntryBatchAssigningRequestID);

            dataEntryBatchAssigningRequestBLL.Edit(dataEntryBatchAssigningRequest);
        }
        public void TransferDataEntryAdminstrationRequestToClosingTeam(DataEntryBatchAssigningRequestDTO dataEntryBatchAssigningRequestDTO)
        {
            DataEntryAdminstrationRequest dataEntryAdminstrationRequest = dataEntryAdminstrationRequestBLL.Find(x => x.DataEntryAdminstrationRequestID == dataEntryBatchAssigningRequestDTO.DataEntryBatchAssigningRequestID).FirstOrDefault();
            dataEntryAdminstrationRequest.DataEntryAdministrationStatusFK = (int)Status.DataEntryAdminFinished;
            dataEntryAdminstrationRequest.TransferredToClosingTime = DateTime.Now;
            dataEntryAdminstrationRequest.TransferedToComment = dataEntryBatchAssigningRequestDTO.TransferedToComment;

            AddBatchClosingRequest(dataEntryAdminstrationRequest.DataEntryAdminstrationRequestID);
            dataEntryAdminstrationRequestBLL.Edit(dataEntryAdminstrationRequest);
        }
        public void AddDataEntryAdminstrationRequest(int BatchRequestID)
        {
            var BatchRequest = batchAuditingRequestBLL.Find(x => x.BatchingRequestFK == BatchRequestID).FirstOrDefault();

            dataEntryAdminstrationRequestBLL.Add(new DataEntryAdminstrationRequest
            {
                BatchRequestFK = BatchRequest.BatchingRequestFK,
                DataEntryAdministrationStatusFK = (int)Status.NewAdministrationRequest,//Change
                OriginalApprovedClaimsNumber = BatchRequest.TotalNumberOfApprovedClaims,
                RemainingUnassignedNumberOfClaims = BatchRequest.TotalNumberOfApprovedClaims,
                IsActive = true,
                IsDeleted = false,
                CreationDate = creationDate,
            });
        }
        public DataEntryAdminstrationRequestDTO GetDataEntryAdminstrationRequest(int ID)
        {
            DataEntryAdminstrationRequest dataEntryAdminstrationRequest = dataEntryAdminstrationRequestBLL.Find(x => x.DataEntryAdminstrationRequestID == ID).FirstOrDefault();

            DataEntryAdminstrationRequestDTO dataEntryAdminstrationRequestDTO = new DataEntryAdminstrationRequestDTO
            {
                BatchRequestFK = dataEntryAdminstrationRequest.BatchRequestFK,
                OriginalApprovedClaimsNumber = dataEntryAdminstrationRequest.OriginalApprovedClaimsNumber,
                RemainingUnassignedNumberOfClaims = dataEntryAdminstrationRequest.RemainingUnassignedNumberOfClaims,
                DataEntryAdminComment= dataEntryAdminstrationRequest.DataEntryAdminComment,
                DataEntryAdministrationStatusFK= dataEntryAdminstrationRequest.DataEntryAdministrationStatusFK
            };

            return dataEntryAdminstrationRequestDTO;
        }
        public void AddBatchClosingRequest(int DataEntryAdminstrationRequestID)
        {
            BatchClosingRequest batchClosingRequest = new BatchClosingRequest
            {
                DataEntryAdminstrationRequestFK = DataEntryAdminstrationRequestID,
                BatchClosingStatusFK = (int)Status.NewClosingRequest,
                IsActive = true,
                IsDeleted = false,
                CreationDate = creationDate
            };
            batchClosingRequestBLL.Add(batchClosingRequest);
        }
        public List<BatchClosingRequestDTO> GetAllBatchClosingRequest()
        {
            List<BatchClosingRequest> batchClosingRequest = batchClosingRequestBLL.Find(x => x.BatchClosingStatusFK == (int)Status.NewClosingRequest).ToList();

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
            BatchClosingRequest batchClosingRequest = batchClosingRequestBLL.Find(x => x.BatchClosingRequestID == batchClosingRequestDTO.BatchClosingRequestID).FirstOrDefault();
            batchClosingRequest.BatchClosingStatusFK = (int)Status.AssignToClosing; //Change
            batchClosingRequest.ClosingOfficerAssigneeFK = batchClosingRequestDTO.ClosingOfficerAssigneeFK;
            batchClosingRequest.ClosingOfficerAssignedTime = creationDate;
            batchClosingRequest.ClosingOfficerAssignedComment = batchClosingRequestDTO.ClosingOfficerAssignedComment;

            batchClosingRequestBLL.Edit(batchClosingRequest);
        }
        public void ConfirmBatchClosingRequest(BatchClosingRequestDTO batchClosingRequestDTO)
        {
            BatchClosingRequest batchClosingRequest = batchClosingRequestBLL.Find(x => x.BatchClosingRequestID == batchClosingRequestDTO.BatchClosingRequestID).FirstOrDefault();
            batchClosingRequest.BatchClosingStatusFK = (int)Status.ClosingConfirmReceiving; //Change
            batchClosingRequest.ConfirmReceivingTime = creationDate;
            batchClosingRequest.ConfirmReceivingComment = batchClosingRequestDTO.ConfirmReceivingComment;

            batchClosingRequestBLL.Edit(batchClosingRequest);
        }
        public void FinishBatchClosingRequest(BatchClosingRequestDTO batchClosingRequestDTO)
        {
            BatchClosingRequest batchClosingRequest = batchClosingRequestBLL.Find(x => x.BatchClosingRequestID == batchClosingRequestDTO.BatchClosingRequestID).FirstOrDefault();
            batchClosingRequest.BatchClosingStatusFK = (int)Status.ClosingFinished; //Change
            batchClosingRequest.FinishedReviewingTime = creationDate;
            batchClosingRequest.FinishedReviewingComment = batchClosingRequestDTO.FinishedReviewingComment;
            batchClosingRequest.TransferredBackToAdminTime = creationDate;

            batchClosingRequestBLL.Edit(batchClosingRequest);
        }
        public List<BatchClosingReAdministrationRequestDTO> GetAllBatchClosingReAdministrationRequest()
        {
            List<ClosedBatchReAdministrationRequest> batchClosingReAdministrationRequest = closedBatchReAdministrationRequestBLL.Find(x => x.ReAdministrationStatusFK == (int)Status.ReAdministrationNewRequest).ToList();

            List<BatchClosingReAdministrationRequestDTO> batchClosingReAdministrationRequestDTOs = new List<BatchClosingReAdministrationRequestDTO>();
            foreach (var items in batchClosingReAdministrationRequest)
            {
                batchClosingReAdministrationRequestDTOs.Add(new BatchClosingReAdministrationRequestDTO
                {
                    BatchClosingReAdministrationRequestID = items.ClosedBatchReAdmissionRequestID,
                    ReAdministrationStatusFK = items.ReAdministrationStatusFK,
                    ReAdministrationStatusName = ((StaticEnums.Status)Enum.Parse(typeof(StaticEnums.Status), items.ReAdministrationStatusFK.ToString())).ToString()
                });
            }

            return batchClosingReAdministrationRequestDTOs;
        }
        public void AssignBatchClosingReAdministrationRequest(BatchClosingReAdministrationRequestDTO batchClosingReAdministrationRequestDTO)
        {
            ClosedBatchReAdministrationRequest closedBatchReAdministrationRequest = closedBatchReAdministrationRequestBLL.Find(x => x.ClosedBatchReAdmissionRequestID == batchClosingReAdministrationRequestDTO.BatchClosingReAdministrationRequestID).FirstOrDefault();
            closedBatchReAdministrationRequest.ReAdministrationStatusFK = (int)Status.ReAdministrationAssign;
            closedBatchReAdministrationRequest.AssignedByAdminFK = batchClosingReAdministrationRequestDTO.AssignedByAdminFK;
            closedBatchReAdministrationRequest.AssignedByAdminComment = batchClosingReAdministrationRequestDTO.AssignedByAdminComment;

            closedBatchReAdministrationRequestBLL.Edit(closedBatchReAdministrationRequest);
        }
        public void ConfirmBatchClosingReAdministrationRequest(BatchClosingReAdministrationRequestDTO batchClosingReAdministrationRequestDTO)
        {
            ClosedBatchReAdministrationRequest closedBatchReAdministrationRequest = closedBatchReAdministrationRequestBLL.Find(x => x.ClosedBatchReAdmissionRequestID == batchClosingReAdministrationRequestDTO.BatchClosingReAdministrationRequestID).FirstOrDefault();
            closedBatchReAdministrationRequest.ReAdministrationStatusFK = (int)Status.ReAdministrationConfirmReceving;
            closedBatchReAdministrationRequest.ConfirmedReceivingComment = batchClosingReAdministrationRequestDTO.ConfirmedReceivingComment;
            closedBatchReAdministrationRequest.ConfirmedReceivingOn = creationDate;

            closedBatchReAdministrationRequestBLL.Edit(closedBatchReAdministrationRequest);
        }
        public void FinishBatchClosingReAdministrationRequest(BatchClosingReAdministrationRequestDTO batchClosingReAdministrationRequestDTO)
        {
            ClosedBatchReAdministrationRequest closedBatchReAdministrationRequest = closedBatchReAdministrationRequestBLL.Find(x => x.ClosedBatchReAdmissionRequestID == batchClosingReAdministrationRequestDTO.BatchClosingReAdministrationRequestID).FirstOrDefault();
            closedBatchReAdministrationRequest.ReAdministrationStatusFK = (int)Status.ReAdministrationFinished;
            closedBatchReAdministrationRequest.ArchivingSystemTicketNo = batchClosingReAdministrationRequestDTO.ArchivingSystemTicketNo;
            closedBatchReAdministrationRequest.FinalClosureComment = batchClosingReAdministrationRequestDTO.FinalClosureComment;
            closedBatchReAdministrationRequest.FinalClosureTime = creationDate;

            closedBatchReAdministrationRequestBLL.Edit(closedBatchReAdministrationRequest);
        }
        public List<DataEntryBatchAssigningRequestDTO> GetDataEntryBatchAssigningRequest(int UserID)
        {
            List<DataEntryBatchAssigningRequestDTO> dataEntryBatchAssigningRequestDTO = new List<DataEntryBatchAssigningRequestDTO>();
            List<DataEntryBatchAssigningRequest> dataEntryBatchAssigningRequest = dataEntryBatchAssigningRequestBLL. Find(x => x.DataEntryOfficerFK == UserID).ToList();

            foreach (var items in dataEntryBatchAssigningRequest)
            {
                dataEntryBatchAssigningRequestDTO.Add(new DataEntryBatchAssigningRequestDTO
                {
                    DataEntryBatchAssigningRequestID = items.DataEntryBatchAssigningRequestID,
                    DataEntryAdministrationRequestFK = items.DataEntryAdministrationRequestFK,
                    DataEntryBatchAssigningStatusName = ((StaticEnums.Status)Enum.Parse(typeof(StaticEnums.Status), items.DataEntryBatchAssigningStatusFK.ToString())).ToString(),
                    AssignedClaimsNumber = items.AssignedClaimsNumber,
                    ConfirmRecievingByOfficerTime = items.ConfirmRecievingByOfficerTime,
                    DataEntryOfficerFinishedOnSystemTime = items.DataEntryOfficerFinishedOnSystemTime,
                    DataEntryOfficerReceivingComment = items.DataEntryOfficerReceivingComment,
                    DataEntryOfficerFinishedComment = items.DataEntryOfficerFinishedComment
                });
            }
            return dataEntryBatchAssigningRequestDTO;
        }
        public void ConfirmRecievingByOfficer(DataEntryBatchAssigningRequestDTO dataEntryBatchAssigningRequestDTO)
        {
            DataEntryBatchAssigningRequest dataEntryBatchAssigningRequest = dataEntryBatchAssigningRequestBLL.Find(x => x.DataEntryBatchAssigningRequestID == dataEntryBatchAssigningRequestDTO.DataEntryBatchAssigningRequestID).FirstOrDefault();
            dataEntryBatchAssigningRequest.DataEntryOfficerReceivingComment = dataEntryBatchAssigningRequestDTO.DataEntryOfficerReceivingComment;
            dataEntryBatchAssigningRequest.ConfirmRecievingByOfficerTime = creationDate;
            dataEntryBatchAssigningRequest.DataEntryOfficerFK = dataEntryBatchAssigningRequestDTO.DataEntryOfficerFK;
            dataEntryBatchAssigningRequest.DataEntryBatchAssigningStatusFK = (int)Status.PendingDataEntryFinish;
            dataEntryBatchAssigningRequestBLL.Edit(dataEntryBatchAssigningRequest);
        }
        public DataEntryBatchAssigningRequestDTO GetDataEntryBatchAssigningRequestByID(int RequestID)
        {
            DataEntryBatchAssigningRequest dataEntryBatchAssigningRequest = dataEntryBatchAssigningRequestBLL.Find(x => x.DataEntryBatchAssigningRequestID == RequestID).FirstOrDefault();

            DataEntryBatchAssigningRequestDTO dataEntryBatchAssigningRequestDTO = new DataEntryBatchAssigningRequestDTO
            {
                AssignedClaimsNumber = dataEntryBatchAssigningRequest.AssignedClaimsNumber
            };
            return dataEntryBatchAssigningRequestDTO;
        }
        public List<DataEntryAdminstrationRequestDTO> GetNewDataEntryAdminstrationRequests()
        {
            var dataEntryAdminstrationRequest = dataEntryAdminstrationRequestBLL.Find(x => x.DataEntryAdministrationStatusFK == (int)Status.NewAdministrationRequest);// as DbQuery<DataEntryAdminstrationRequest>;
            //SqlCommand sqlCommand = new SqlCommand(dataEntryAdminstrationRequest.ToString());

            //var sqlDependency = new SqlDependency(sqlCommand);
            //sqlDependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

            //var clientName = Request["clientName"];
            //Task.Factory.StartNew(
            //    () =>
            //    {
            //        var clients = Hub.GetClients<RealTimeJTableDemoHub>();
            //        clients.RecordUpdated(clientName, student);
            //    });

            List<DataEntryAdminstrationRequestDTO> dataEntryAdminstrationRequestsDTO = new List<DataEntryAdminstrationRequestDTO>();
            foreach (var items in dataEntryAdminstrationRequest)
            {
                dataEntryAdminstrationRequestsDTO.Add(new DataEntryAdminstrationRequestDTO
                {
                    DataEntryAdminstrationRequestID = items.DataEntryAdminstrationRequestID,
                    BatchRequestFK = items.BatchRequestFK,
                    DataEntryAdministrationStatusName = ((StaticEnums.Status)Enum.Parse(typeof(StaticEnums.Status), items.DataEntryAdministrationStatusFK.ToString())).ToString(),
                    OriginalApprovedClaimsNumber = items.OriginalApprovedClaimsNumber,
                    RemainingUnassignedNumberOfClaims = items.RemainingUnassignedNumberOfClaims,
                    IsLocked = items.IsLocked                  
                });
            }
            //SqlDependency sqlDependency = new SqlDependency(dataEntryAdminstrationRequest);
            return dataEntryAdminstrationRequestsDTO;
        }
        public DataEntryAdminstrationRequest AddDataEntryAdminstrationRequest(DataEntryAdminstrationRequestDTO dataEntryAdminstrationRequestDTO)
        {
            BatchAuditingRequest batchAuditingRequest = batchAuditingRequestBLL.Find(x => x.BatchAuditingRequestID == dataEntryAdminstrationRequestDTO.BatchAuditingRequestID).FirstOrDefault();
            batchAuditingRequest.TransferToDataEntryOn = DateTime.Now;
            batchAuditingRequest.ModificationDate = DateTime.Now;
            batchAuditingRequest.IsActive = false;
            batchAuditingRequestBLL.Edit(batchAuditingRequest);

            DataEntryAdminstrationRequest dataEntryAdminstrationRequest = new DataEntryAdminstrationRequest
            {
                BatchRequestFK = dataEntryAdminstrationRequestDTO.BatchRequestFK,
                DataEntryAdminAssigneeFK = dataEntryAdminstrationRequestDTO.DataEntryAdminAssigneeFK,
                //AssignedTime = DateTime.Now,
                DataEntryAdminComment = dataEntryAdminstrationRequestDTO.DataEntryAdminComment,
                DataEntryAdministrationStatusFK = (int)Status.NewAdministrationRequest,
                OriginalApprovedClaimsNumber = dataEntryAdminstrationRequestDTO.OriginalApprovedClaimsNumber,
                //RemainingUnassignedNumberOfClaims = dataEntryAdminstrationRequestDTO.RemainingUnassignedNumberOfClaims,
                IsActive = true,
                IsDeleted = false,
                CreationDate = creationDate
            };
            dataEntryAdminstrationRequestBLL.Add(dataEntryAdminstrationRequest);

            return dataEntryAdminstrationRequest;
        }
        //public void AssigneDataEntryAdminstrationRequest(int ID)
        //{
        //    DataEntryAdminstrationRequest DataEntryAdminstrationRequest = Find(x => x.DataEntryAdminstrationRequestID == ID).FirstOrDefault();

        //    DataEntryAdminstrationRequestDTO dataEntryAdminstrationRequestDTO = new DataEntryAdminstrationRequestDTO
        //    {
        //        BatchRequestFK = DataEntryAdminstrationRequest.BatchRequestFK,
        //        NUMBER
        //    }

        //    dataEntryAdminstrationRequest.DataEntryAdminAssigneeFK = dataEntryAdminstrationRequestDTO.DataEntryAdminAssigneeFK;
        //    dataEntryAdminstrationRequest.DataEntryAdminComment = dataEntryAdminstrationRequestDTO.DataEntryAdminComment;
        //    dataEntryAdminstrationRequest.AssignedTime = DateTime.Now;
        //    dataEntryAdminstrationRequest.ModificationDate = DateTime.Now;

        //    Edit(dataEntryAdminstrationRequest);
        //}
        public void AssigneDataEntryAdminstrationRequest(DataEntryAdminstrationRequestDTO dataEntryAdminstrationRequestDTO)
        {
            DataEntryAdminstrationRequest dataEntryAdminstrationRequest = dataEntryAdminstrationRequestBLL.Find(x => x.DataEntryAdminstrationRequestID == dataEntryAdminstrationRequestDTO.DataEntryAdminstrationRequestID).FirstOrDefault();

            if (dataEntryAdminstrationRequest.DataEntryAdminAssigneeFK != null)
                throw new Exception("This Request Is Assigned Before");

            dataEntryAdminstrationRequest.DataEntryAdminAssigneeFK = dataEntryAdminstrationRequestDTO.DataEntryAdminAssigneeFK;
            dataEntryAdminstrationRequest.DataEntryAdministrationStatusFK = (int)Status.DataEntryAdminAssign;
            dataEntryAdminstrationRequest.DataEntryAdminComment = dataEntryAdminstrationRequestDTO.DataEntryAdminComment;
            dataEntryAdminstrationRequest.AssignedTime = DateTime.Now;
            dataEntryAdminstrationRequest.ModificationDate = DateTime.Now;

            dataEntryAdminstrationRequestBLL.Edit(dataEntryAdminstrationRequest);
        }
        //private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        //{
        //    BNC.Client.Hubs.AppHub.Show();
        //}
    }
}
