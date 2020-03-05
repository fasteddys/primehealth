using OnlineApprovals.BLL.CommonFunctions;
using OnlineApprovals.BLL.DbObjects;
using OnlineApprovals.Entities;
using OnlineApprovals.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using OnlineApprovals.DTOs;
using OnlineApprovals.DAL.Repositories;
using static OnlineApprovals.BLL.StaticData.StaticEnums;

namespace OnlineApprovals.BLL.Logic
{

    public class OnlineApprovalsProviderBLL : Repository<PharmacyTB>
    {
        public readonly DTOMapper Mapper;
        public readonly StoredFunctions StoredFunction;
        public readonly OnlineApprovalsLoginKeysBLL onlineApprovalsLoginKeysBLL;
        int ProviderID;
        int ProviderTypeID;
        string LoginKey;
        public OnlineApprovalsProviderBLL(PhNetworkEntities Context,int ProviderTypeID,int ProviderID, string LoginKey) : base(Context)
        {
            
            Mapper = new DTOMapper();
            StoredFunction = new StoredFunctions();
            onlineApprovalsLoginKeysBLL = new OnlineApprovalsLoginKeysBLL(Context, ProviderTypeID, ProviderID, LoginKey);
            this.ProviderID = ProviderID;
            this.ProviderTypeID = ProviderTypeID;
            this.LoginKey = LoginKey;
        }
        
        public ProviderDTO GetProviderByID()
        {
            return Mapper.MapPharmcyToSPProviderByID(StoredFunction.Select_OnlineApprovalsProviderByID(ProviderID));
        }
        public bool LoginAsPharmacyProvider(LoginDTO loginDTO, out int ProviderTypeID, out int ProviderID,out string LoginKey)
        {
            try
            {
                var UserName = loginDTO.UserName;
                var Password = loginDTO.Password;
                var LoginObj =
                    StoredFunction.SP_Provider_Login(UserName, Password).FirstOrDefault();
                if (LoginObj != null)
                {
                    ProviderTypeID = (int)ProviderType.Pharmacy;
                    ProviderID = LoginObj.ID;
                    LoginKey=onlineApprovalsLoginKeysBLL.GenerateLoginKey(LoginObj.ID, (int)ProviderType.Pharmacy);
                    return true;
                }
                else
                {
                    ProviderTypeID = 0;
                    ProviderID = 0;
                    LoginKey = string.Empty;
                    return false;
                }
            }
            catch (Exception ex)
            {
                ProviderTypeID = 0;
                ProviderID = 0;
                LoginKey = string.Empty;
                return false;
            }
        }


        public bool ChangePassword(ChangePasswordDTO ChangeObj)
        {
            var Provider = GetAll().Where(p => p.ID == ChangeObj.ProviderID).FirstOrDefault();

            if (Provider.Password == ChangeObj.OldPassword)
            {
                Provider.Password = ChangeObj.NewPassword;
                Provider.IsDefaultPassowrd = false;
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

