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
    public class OnlineApprovalsDrugsListDIMBLL : Repository<OnlineApprovals_DrugsListDIM>
    {
        public readonly DTOMapper Mapper;
        public readonly StoredFunctions StoredFunction;
        public readonly FileIO FileIO;
        int ProviderID;
        int ProviderTypeID;
        string LoginKey;
        public OnlineApprovalsDrugsListDIMBLL(PhNetworkEntities Context,int ProviderTypeID,int ProviderID,string LoginKey) : base(Context)
        {
            FileIO = new FileIO(context);

            Mapper = new DTOMapper();
            StoredFunction = new StoredFunctions();
            this.ProviderID = ProviderID;
            this.ProviderTypeID = ProviderTypeID;
            this.LoginKey = LoginKey;
        }
        public List<DrugsListDIMDTO> GetDrugsListDTO()
        {
            return Mapper.MapDrugListToDrugListDTO(StoredFunction.Select_OnlineApprovalsDrugList().ToList());
        }


    public OnlineApprovals_DrugsListDIM GetDruglistByID(int id)
    {
        OnlineApprovals_DrugsListDIM druglist = new OnlineApprovals_DrugsListDIM();

        druglist = GetAll().Where(x => x.DrugID == id).FirstOrDefault();

        return druglist;
    }



}
}



