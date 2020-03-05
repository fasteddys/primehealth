using OnlineApprovals.BLL.CommonFunctions;
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
    public class PharmacyTBBLL : Repository<PharmacyTB>
    {
        public readonly OnlineApprovalsLoginKeysBLL OnlineApprovals_LoginKeysBLL;
        public PharmacyTBBLL(PhNetworkEntities Context) : base(Context)
        {
            OnlineApprovals_LoginKeysBLL = new OnlineApprovalsLoginKeysBLL(Context);
        }

        public bool LoginAsPharmacyProvider(LoginDTO loginDTO)
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public  bool ChangePassword (ChangePasswordDTO ChangeObj)
        {
            var Provider = GetAll().Where(p => p.ID == ChangeObj.ProviderID).FirstOrDefault();

            if (Provider.Password==ChangeObj.OldPassword)
            {
                Provider.Password = ChangeObj.NewPassword;
                Edit(Provider);
                return true;
            }
            else
            {
                
                return false;
            }
        }
    }
}
