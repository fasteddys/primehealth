using OnlineApprovals.BLL.CommonFunctions;
using OnlineApprovals.BLL.DbObjects;
using OnlineApprovals.DAL.Repositories;
using OnlineApprovals.DTOs;
using OnlineApprovals.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineApprovals.BLL.Logic
{
    public class OnlineApprovalsRequestAttachmentBLL : Repository<OnlineApprovals_RequestAttachment>
    {
        public readonly DTOMapper Mapper;
        public readonly StoredFunctions StoredFunction;
        int ProviderID;
        int ProviderTypeID;
        string LoginKey;
        public OnlineApprovalsRequestAttachmentBLL(PhNetworkEntities Context,int ProviderTypeID,int ProviderID,string LoginKey) : base(Context)
        {
            Mapper = new DTOMapper();
            StoredFunction = new StoredFunctions();
            this.ProviderID = ProviderID;
            this.ProviderTypeID = ProviderTypeID;
            this.LoginKey = LoginKey;
        }
        public List<AttachmentDTO> GetRequestAttachment(int RequestID)
        {
            return
                
                Mapper.MapListStoredProsedureToRequestAttachment(StoredFunction.Select_OnlineApprovalsAttachment(RequestID).ToList());
        }






    }
}
