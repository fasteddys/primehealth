using OnlineApprovals.DAL.Repositories;
using OnlineApprovals.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineApprovals.BLL.Logic
{
   public class OnlineApprovalsConfigrationBLL:Repository<OnlineApprovals_Configrations>
    {
        int ProviderID;
        int ProviderTypeID;
        string LoginKey;
        public OnlineApprovalsConfigrationBLL(PhNetworkEntities Context, int ProviderTypeID,int ProviderID,string LoginKey) : base(Context)
        {
            this.ProviderID = ProviderID;
            this.ProviderTypeID = ProviderTypeID;
            this.LoginKey = LoginKey;
        }

        public OnlineApprovals_Configrations GetConfigByName(string ConfigName)
        {
         return   GetAll().Where(x => x.ConfigrationKey == ConfigName).FirstOrDefault();

        }



        }
}
