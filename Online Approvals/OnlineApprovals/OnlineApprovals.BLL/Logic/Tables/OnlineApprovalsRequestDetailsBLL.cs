using OnlineApprovals.BLL.CommonFunctions;
using OnlineApprovals.BLL.DbObjects;
using OnlineApprovals.DAL.Repositories;
using OnlineApprovals.DTOs;
using OnlineApprovals.Entities;
using OnlineApprovals.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OnlineApprovals.BLL.Logic
{
    public class OnlineApprovalsRequestDetailsBLL : Repository<OnlineApprovals_RequestDetails>
    {
        public readonly DTOMapper Mapper;
        public readonly StoredFunctions StoredFunction;
        public readonly FileIO FileIO;
        int ProviderID;
        int ProviderTypeID;
        string LoginKey;
        public OnlineApprovalsRequestDetailsBLL(PhNetworkEntities Context,int ProviderTypeID, int ProviderID,string LoginKey) : base(Context)
        {
            FileIO = new FileIO(context);

            Mapper = new DTOMapper();
            StoredFunction = new StoredFunctions();
            this.ProviderID = ProviderID;
            this.ProviderTypeID = ProviderTypeID;
            this.LoginKey = LoginKey;
        }
        public List< RequestDetailsDTO> GetRequestDetails(int RequestID)
        {
          return  Mapper.MapListStoredProsedureToRequest(StoredFunction.Select_OnlineApprovalsRequestDetailss(RequestID).ToList());
        }

        public bool InsertNewRequestDetails(int RequestID, RequestDetailsDTO RequestDetailsDTOObj, IEnumerable<HttpPostedFileBase> files,out string ValidationMessage)
        {
            try
            {
                OnlineApprovals_RequestDetails RequestDetailsObj = new OnlineApprovals_RequestDetails();

                if (RequestDetailsDTOObj != null)
                {
                    RequestDetailsDTOObj.RequestID = RequestID;
                    RequestDetailsDTOObj.IsDeleted = false;
                    RequestDetailsDTOObj.Delivered = false;
                    RequestDetailsDTOObj.Seen = false;

                    RequestDetailsObj = Mapper.MapRequestDetailsDTO(RequestDetailsDTOObj);

                    Add(RequestDetailsObj);

                    bool Result = FileIO.FileUpload(files, RequestDetailsObj.RequestDetailsID, RequestID, 1,out string FileValidationMessage);
                    if (Result == true)
                    {
                        ValidationMessage = "Request Added Successfully";
                        return true;
                    }
                    else
                    {
                        ValidationMessage = "Adding Request Failed ,Please try again later";
                        return false;
                    }
                }
                else
                {
                    ValidationMessage = "Request Added Successfully";
                    return true;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateException(ex, "OnlineSP_objectBLL.cs", "InsertNewRequest");
                ValidationMessage = "Failed To Add Request ,Please try again";
                return false;
            }
        }





    }
}
