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
    public class OnlineApprovalsDrugsDetailBLL : Repository<OnlineApprovals_DemandedDrugsDetails>
    {
        public readonly DTOMapper Mapper;
        public readonly StoredFunctions StoredFunction;
        public readonly FileIO FileIO;
        int ProviderID;
        int ProviderTypeID;
        string LoginKey;
        public OnlineApprovalsDrugsDetailBLL(PhNetworkEntities Context,int ProviderTypeID, int ProviderID,string LoginKey) : base(Context)
        {
            FileIO = new FileIO(context);

            Mapper = new DTOMapper();
            StoredFunction = new StoredFunctions();
            this.ProviderID = ProviderID;
            this.ProviderTypeID = ProviderTypeID;
            this.LoginKey = LoginKey;
        }
        public List<DrugsDetailDTO> GetDrugDetailsList(int RequestID)
        {
            return Mapper.MapDrugDemanadDrugToDrugsDetailDTO(StoredFunction.Select_OnlineApprovalsDemandDrugs_Result(RequestID).ToList());
        }


        public bool InsertListOfDrugsDetail(List<OnlineApprovals_DemandedDrugsDetails> ListOfDrougList,int RequestID)
        {
            try
            {

                if (ListOfDrougList != null)
                {
                  foreach(var item in ListOfDrougList)
                    {
                        item.RequestID = RequestID;
                        //item.DemandedQuantity = ;
                        //item.DrugID = 138;
                        //item.UnitPrice = 10;
                        //item.UnitQuantity = 2;
                        //item.TotalPrice = 100;
                        //item.IsDrugList = false;
                        //item.DrugName = "sad";
                        Add(item);

                    }
                    return true;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateException(ex, "OnlineSP_objectBLL.cs", "InsertNewRequest");
                return false;
            }
        }




    }
}
