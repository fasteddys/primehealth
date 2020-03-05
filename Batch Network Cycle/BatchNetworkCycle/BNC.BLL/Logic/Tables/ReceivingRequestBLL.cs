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
using BNC.BLL.Logic.Shared_Logic;
namespace BNC.BLL.Logic.Tables
{
    public class ReceivingRequestBLL : Repository<ReceivingRequest>
    {
        DateTime creationDate;
        FilterationRequestBLL filterationRequestBLL;
        ProvidersBLL providersBLL;
        LockLoggesBLL lockLoggesBLL;

        public ReceivingRequestBLL(BNCEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
            filterationRequestBLL = new FilterationRequestBLL(Context, CreationDate);
            providersBLL = new ProvidersBLL(Context, CreationDate);
            lockLoggesBLL = new LockLoggesBLL(Context, CreationDate);
        }
        //-----------------------------------------------------------------------------
        public ReceivingRequest AddReceivingRequest(RecievingRequestDTOS addRecievingRequestInputDTOS)
        {
            ReceivingRequest receivingRequest = new ReceivingRequest
            {
                ProviderFK = addRecievingRequestInputDTOS.ProviderFK,
                ReceivingDate = creationDate,
                ExpectedAmount = addRecievingRequestInputDTOS.ExpectedAmount,
                BillingYear = addRecievingRequestInputDTOS.BillingYear,
                BilllingMonth = addRecievingRequestInputDTOS.BilllingMonth,
                CreationDate = creationDate,
                IsActive = true,
                IsDeleted = false,
                ReceivingOfficerCalimsCount = addRecievingRequestInputDTOS.ReceivingOfficerCalimsCount,
                ReceivedClaimsCount = addRecievingRequestInputDTOS.ReceivedClaimsCount,
                ReceivingOfficerFK= addRecievingRequestInputDTOS.ReceivingOfficerFK,
                ReceivingStatusFK=(int)Status.NewReceived,
                AgentName= addRecievingRequestInputDTOS.AgentName,
                CompanyFK = addRecievingRequestInputDTOS.CompanyFK,
                GovernmentFK = addRecievingRequestInputDTOS.GovernmentFK
            };
            if (addRecievingRequestInputDTOS.ReceivingOfficerComment != "")
            {
                receivingRequest.ReceivingOfficerComment = addRecievingRequestInputDTOS.ReceivingOfficerComment;
            } 
            this.Add(receivingRequest);

            return receivingRequest;
        }
        //--------------------------------------------------------------------------------------
        public List<ReceivingRequestDTO> GetReceivingRequests()
        {
            List<ReceivingRequestDTO> ReceivingRequests = new List<ReceivingRequestDTO>();
            foreach (var item in GetAll())
            {
                ReceivingRequests.Add(new  ReceivingRequestDTO
                {
                    BillingYear = item.BillingYear,
                    BilllingMonth = item.BilllingMonth,
                    ClaimsCount = item.ReceivingOfficerCalimsCount,
                    ProviderID = item.ProviderFK,
                    ReceivingDate = item.ReceivingDate,
                    ReceivingRequestID= item.ReceivingRequestID,
                     ProviderName= providersBLL.Find(x=>x.ProviderID== item.ProviderFK).FirstOrDefault().ProviderEnglishName
                });

            }
            return ReceivingRequests;
        }        
        public List<ReceivingRequestDTO> MyReceivingRequests(int ReceivingOfficerID)
        {
            List<ReceivingRequestDTO> ReceivingRequests = new List<ReceivingRequestDTO>();
            foreach (var item in Find(x=>x.ReceivingOfficerFK== ReceivingOfficerID))
            {
                ReceivingRequests.Add(new ReceivingRequestDTO
                {
                    BillingYear = item.BillingYear,
                    BilllingMonth = item.BilllingMonth,
                    ClaimsCount = item.ReceivingOfficerCalimsCount,
                    ProviderID = item.ProviderFK,
                    ReceivingDate = item.ReceivingDate,
                    ReceivingRequestID = item.ReceivingRequestID,
                    ProviderName = providersBLL.Find(x => x.ProviderID == item.ProviderFK).FirstOrDefault().ProviderEnglishName

                });

            }
            return ReceivingRequests;
        }

        public ReceivingRequestDTO GetRecievingRequestData(int ReceivingRequestID)
        {

            var Request = Find(x => x.ReceivingRequestID == ReceivingRequestID).FirstOrDefault();
            var Provider = providersBLL.Find(x => x.ProviderID == Request.ProviderFK).FirstOrDefault();
            ReceivingRequestDTO ReceivingRequest = new ReceivingRequestDTO
            {
                BillingYear = Request.BillingYear,
                BilllingMonth = Request.BilllingMonth,
                ClaimsCount = (int)Request.ReceivingOfficerCalimsCount,
                ProviderID = Request.ProviderFK,
                ReceivingOfficerID = Request.ReceivingOfficerFK,
                ReceivingDate = Request.ReceivingDate,
                ReceivingRequestID = Request.ReceivingRequestID,
                StatusID = Request.ReceivingStatusFK,
                ExpectedAmount = Request.ExpectedAmount,
                ReceivingOfficerComment = Request.ReceivingOfficerComment,
                ReceivedClaimsCount = Request.ReceivedClaimsCount,
                AgentName= Request.AgentName,
                ProviderName= Provider.ProviderEnglishName,
                CompanyFK= Request.CompanyFK,
                IbnSinaProviderPin = Provider.IbnSinaProviderPin,
                MCAProviderPin= Provider.MCAProviderPin,
                GovernmentFK=Provider.GovernmentFK
            };
                
           if(filterationRequestBLL.Find(x=>x.ReceivingRequestFK== ReceivingRequestID).Count()> 0)
            {
                ReceivingRequest.IsTransferdToFiltrationTeam = true;
            }
            return ReceivingRequest;
        }

        public void TransferRecievingRequestToFiltrationRequest(int ReceivingRequestID)
        {
            FilterationRequest newFilterationRequest = new FilterationRequest();
            newFilterationRequest.ReceivingRequestFK = ReceivingRequestID;
            newFilterationRequest.IsActive = true;
            newFilterationRequest.IsDeleted = false;
            newFilterationRequest.CreationDate = creationDate;
            newFilterationRequest.FilterationRequestStatusFK = (int)Status.PendingFiltration;
            filterationRequestBLL.Add(newFilterationRequest);
        }
        public List<ReceivingRequestDTO> GetFilterationRequests(SearchCriteriaDTO SearchCriteria)
        {
            List<ReceivingRequestDTO> ReceivingRequests = new List<ReceivingRequestDTO>();
            List<int> ReceivingRequestsIDs = filterationRequestBLL.Find(x => x.IsActive == true && x.IsDeleted == false).Select(ID => ID.ReceivingRequestFK).ToList();
            foreach (var item in ReceivingRequestsIDs)
            {
                var RequestItem = Find(x => x.ReceivingRequestID == item).FirstOrDefault();
                ReceivingRequests.Add(new ReceivingRequestDTO
                {
                    BillingYear = RequestItem.BillingYear,
                    BilllingMonth = RequestItem.BilllingMonth,
                    ClaimsCount = RequestItem.ReceivingOfficerCalimsCount,
                    ProviderID = RequestItem.ProviderFK,
                    ReceivingDate = RequestItem.ReceivingDate,
                    ReceivingRequestID = RequestItem.ReceivingRequestID
                });

            }
            return ReceivingRequests;
        }

        public ReceivingRequestDTO PrintRecievingRequest(int RecievingRequestID)
        {
            var ReceivingRequest= Find(x => x.ReceivingRequestID == RecievingRequestID).FirstOrDefault();
            var Provider = providersBLL.Find(x => x.ProviderID == ReceivingRequest.ProviderFK).FirstOrDefault();
            ReceivingRequest.ReceivingStatusFK =(int) Status.Printed;
            Edit(ReceivingRequest);
            ReceivingRequestDTO ReceivingRequestDTO = new ReceivingRequestDTO
            {
                BillingYear = ReceivingRequest.BillingYear,
                BilllingMonth = ReceivingRequest.BilllingMonth,
                ClaimsCount = (int)ReceivingRequest.ReceivingOfficerCalimsCount,
                ProviderID = ReceivingRequest.ProviderFK,
                ReceivingOfficerID = ReceivingRequest.ReceivingOfficerFK,
                ReceivingDate = ReceivingRequest.ReceivingDate,
                ReceivingRequestID = ReceivingRequest.ReceivingRequestID,
                StatusID = ReceivingRequest.ReceivingStatusFK,
                ExpectedAmount = ReceivingRequest.ExpectedAmount,
                ReceivingOfficerComment = ReceivingRequest.ReceivingOfficerComment,
                ReceivedClaimsCount = ReceivingRequest.ReceivedClaimsCount,
                AgentName = ReceivingRequest.AgentName,
                ProviderName = Provider.ProviderEnglishName,
                CompanyFK = ReceivingRequest.CompanyFK,
                IbnSinaProviderPin = Provider.IbnSinaProviderPin,
                MCAProviderPin = Provider.MCAProviderPin

            };
            return ReceivingRequestDTO;
        }

        public void LockRequest(SearchCriteriaDTO SearchCriteriaDTO)
        {
            ReceivingRequest receivingRequest = Find(x => x.ReceivingRequestID == SearchCriteriaDTO.RecordID).FirstOrDefault();
            receivingRequest.IsLocked = true;

            LockLogge lockLogge = new LockLogge
            {
                ActionFK = (int)Actions.Locked,
                UserFK = SearchCriteriaDTO.UserID,
                SystemEntityFK = (int)Entity.ReceivingRequest,
                EntityRecordID = SearchCriteriaDTO.RecordID,
                IsActive = true,
                IsDeleted = false,
                CreationDate = creationDate
            };

            Edit(receivingRequest);
            lockLoggesBLL.Add(lockLogge);
        }

        public void UnLockRequest(SearchCriteriaDTO SearchCriteriaDTO)
        {
            ReceivingRequest receivingRequest = Find(x => x.ReceivingRequestID == SearchCriteriaDTO.RecordID).FirstOrDefault();
            receivingRequest.IsLocked = false;

            LockLogge lockLogge = new LockLogge
            {
                ActionFK = (int)Actions.UnLocked,
                UserFK = SearchCriteriaDTO.UserID,
                SystemEntityFK = (int)Entity.ReceivingRequest,
                EntityRecordID = SearchCriteriaDTO.RecordID,
                IsActive = true,
                IsDeleted = false,
                CreationDate = creationDate
            };

            Edit(receivingRequest);
            lockLoggesBLL.Add(lockLogge);
        }
    }
}
