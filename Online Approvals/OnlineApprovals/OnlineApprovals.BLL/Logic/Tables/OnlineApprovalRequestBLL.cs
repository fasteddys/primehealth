using OnlineApprovals.BLL.CommonFunctions;
using OnlineApprovals.DAL.Repositories;
using OnlineApprovals.DTOs;
using OnlineApprovals.Entities;
using OnlineApprovals.Utilities;
using OnlineApprovals.BLL.DbObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using OnlineApprovals.BLL.StaticData;
using static OnlineApprovals.BLL.StaticData.StaticEnums;
using OnlineApprovals.BLL.Logic.Tables;

namespace OnlineApprovals.BLL.Logic
{
    public class OnlineApprovalRequestBLL : Repository<OnlineApprovals_Requests>
    {
        public readonly OnlineApprovalsRequestDetailsBLL RequestDetails;
        public readonly OnlineApprovalsRequestAttachmentBLL RequestAttachments;
        public readonly OnlineApprovalsDrugsDetailBLL RequestDrugs;
        public readonly OnlineApprovalsInvoicesBLL Inovices;

        public readonly DTOMapper Mapper;
        public readonly FileIO FileIO;
        public readonly StoredFunctions StoredFunction;
        OnlineApprovalsProviderBLL provider;
        int ProviderID;
        int ProviderTypeID;
        string LoginKey;
        public OnlineApprovalRequestBLL(PhNetworkEntities Context,int ProviderTypeID,int ProviderID,string LoginKey) : base(Context)
        {

            Mapper = new DTOMapper();
            FileIO = new FileIO(context);
            StoredFunction = new StoredFunctions();
            RequestAttachments = new OnlineApprovalsRequestAttachmentBLL(context,ProviderTypeID,ProviderID,LoginKey);
            RequestDrugs = new OnlineApprovalsDrugsDetailBLL(context, ProviderTypeID, ProviderID, LoginKey);
            RequestDetails = new OnlineApprovalsRequestDetailsBLL(context, ProviderTypeID, ProviderID, LoginKey);
            provider = new OnlineApprovalsProviderBLL(context, ProviderTypeID, ProviderID, LoginKey);
          //  Inovices = new OnlineApprovalsInvoicesBLL(context, ProviderTypeID, ProviderID, LoginKey);


            this.ProviderID = ProviderID;
            this.ProviderTypeID = ProviderTypeID;
            this.LoginKey= LoginKey;

        }

        public void InsertNewRequest(OnlineApprovals_Requests RequestObj, List<OnlineApprovals_DemandedDrugsDetails> DrugList)
        {
            RequestDTO requestDTO = new RequestDTO();
            
            RequestObj.Delivered = false;
            RequestObj.Seen = false;
            RequestObj.RequestStatusID = (int)StaticEnums.RequestStatus.New;
            RequestObj.SeenByUserID = -1;
            RequestObj.IsDeleted = false;
            RequestObj.RequestCreationTime = DateTime.Now;
            RequestObj.ProviderID = ProviderID;
            RequestObj.ProviderTypeID = ProviderTypeID;
            RequestObj.RequestTypeID = (int)RequestType.Medication;
            Add(RequestObj);

            RequestDrugs.InsertListOfDrugsDetail(DrugList, RequestObj.RequestID);
        }

        public OnlineApprovals_Requests GetRequestsByID(int ID)
        {

            return Mapper.MapStoredProsedureToRequest(StoredFunction.Select_OnlineApprovalsRequests_Result(ID).FirstOrDefault());
        }

        public List<RequestDTO> GetRequestsByStatusAndProvider(int StatusID)
        {
            return Mapper.MapListStoredProsedureToRequest(StoredFunction.Select_OnlineApprovalsProvideIDAndStatusID( ProviderID, StatusID));
        }

        public ALLRequestDataDTO GetAllRequestData(int ID)
        {
            ALLRequestDataDTO AllRequestData = new ALLRequestDataDTO();
            AllRequestData.Request = Mapper.MapRequestDTOToRequest(GetRequestsByID(ID));
            AllRequestData.Request.ProviderName = provider.GetProviderByID().PharmName;
            AllRequestData.RequestDetails = new List<RequestDetailsDTO>();
            foreach (var item in RequestDetails.GetRequestDetails(ID))
            {
                AllRequestData.RequestDetails.Add(item);
            }


            AllRequestData.Attachments = new List<AttachmentDTO>();

            AllRequestData.Attachments.AddRange(
                RequestAttachments.GetRequestAttachment(ID));
            AllRequestData.DemandedDrugs = new List<DrugsDetailDTO>();

            AllRequestData.DemandedDrugs.AddRange(

             RequestDrugs.GetDrugDetailsList(ID));

            return AllRequestData;

        }


        public List<RequestDTO> SearchRequests(string MemberName, string ClientName, string ClaimNumber,
            string Diagnose, DateTime CreationDate, DateTime CloseDate, string IvrNumber, int RequestTypeID,
            int RequestStatusID, string DrugName)

        {
            return Mapper.MapSearchStoredProsedureToRequest(StoredFunction.Search_OnlineApprovals(MemberName, ClientName, ClaimNumber, Diagnose,
                CreationDate, CloseDate, IvrNumber, RequestTypeID, RequestStatusID, DrugName).ToList());
        }



        public int? CountOfALLRequestByProvideID()
        {
            return StoredFunction.Select_CountOfALLRequestByProvideID(ProviderID);
        }


        public int? CountOfALLRequestByProvideIDAndStatus( int StatusID)
        {
            return StoredFunction.Select_CountOfALLRequestByProvideIDAndStatusID(ProviderID, StatusID);
        }
        public int? CountOfTotal_Members()
        {
            return StoredFunction.Select_CountTotal_Members(ProviderID);
        }
        public int? CountClaimsPerCurrentMonth()
        {
            return StoredFunction.Select_CountClaimsPerCurrentMonth(ProviderID);
        }
        public List<RequestDTO> GetRequestsByprovider()
        {

            return Mapper.MapStoredProsedureToRequestDTo(StoredFunction.OnlineApprovals_RequestsByprovider(ProviderID));
        }

        public bool ReopenRequest(int RequestID , string Reason)
        {
            try
            {
                var RequestToReopen = GetAll().Where(p => p.RequestID == RequestID).FirstOrDefault();
                RequestToReopen.RequestStatusID = (int)StaticEnums.RequestStatus.Reopend;
                Edit(RequestToReopen);
                OnlineApprovals_RequestDetails RequestDetailsObj = new OnlineApprovals_RequestDetails();
                RequestDetailsObj.RequestID = RequestID;
                RequestDetailsObj.RequestDetailsTypeID = (int)StaticEnums.RequestDetailsType.Reopen;
                RequestDetailsObj.UserID = (int)RequestToReopen.ProviderID;
                RequestDetailsObj.UserTypeID = (int)StaticEnums.UserType.Provider;
                RequestDetailsObj.DetailsDate = DateTime.Now;
                RequestDetailsObj.Notes = "Request reopened By Provider for Reason : "+Reason;
                RequestDetailsObj.IsDeleted = false;
                RequestDetailsObj.Delivered = false;
                RequestDetailsObj.Seen = false;
                RequestDetails.Add(RequestDetailsObj);

                return true;

            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateException(ex, "OnlineApprovalRequestBLL", "ReopenRequest");
                return false;
                throw;
            }
        }

        public bool TerminateRequest(int RequestID, string Reason, out string ValidationMessage)
        {
            try
            {

                var RequestToTerminate = GetAll().Where(p => p.RequestID == RequestID).FirstOrDefault();
                if (RequestToTerminate.RequestStatusID== (int)StaticEnums.RequestStatus.New)
                {
                    OnlineApprovals_RequestDetails RequestDetailsObj = new OnlineApprovals_RequestDetails();
                    RequestDetailsObj.RequestID = RequestID;
                    RequestDetailsObj.RequestDetailsTypeID = (int)StaticEnums.RequestDetailsType.Terminate;
                    RequestDetailsObj.UserID = ProviderID;
                    RequestDetailsObj.UserTypeID = (int)StaticEnums.UserType.Provider;
                    RequestDetailsObj.DetailsDate = DateTime.Now;
                    RequestDetailsObj.Notes = "Request Terminated By Provider for Reason : " + Reason;
                    RequestDetailsObj.IsDeleted = true;
                    RequestDetailsObj.Delivered = false;
                    RequestDetailsObj.Seen = false;
                    RequestToTerminate.RequestStatusID = (int)StaticEnums.RequestStatus.Terminated;
                    RequestToTerminate.IsDeleted = true;
                    RequestToTerminate.CloseTime = DateTime.Now;

                    Edit(RequestToTerminate);
                    RequestDetails.Add(RequestDetailsObj);
                    ValidationMessage = "Request has been Terminated Successfuly";
                    return true;
                }
                else
                {
                    ValidationMessage = "Sorry... You can't Terminate this request as it's has been assigned By callcenter team ,you can Contact our Callcenter for any further actions";
                    return false;
                }
                

            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateException(ex, "OnlineApprovalRequestBLL", "ReopenRequest");
                ValidationMessage = "Failed To Terminate Request...,please Try again";
                return false;
                throw;
            }
        }

        public bool CheckClaimExistence(long? ClaimNumber)
        {
            if (ClaimNumber == null)
            {
                return false;
            }
            else
            {
                var Count = GetAll().Where(p => p.ClaimNumber == ClaimNumber && p.IsDeleted == false).Count();

                if (Count > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }


        }

    }
}
