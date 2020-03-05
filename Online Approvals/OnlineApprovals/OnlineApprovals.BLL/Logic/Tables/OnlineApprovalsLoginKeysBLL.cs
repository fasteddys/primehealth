using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineApprovals.Utilities;
using OnlineApprovals.Entities;
using OnlineApprovals.DAL.Repositories;

namespace OnlineApprovals.BLL.Logic
{
   public class OnlineApprovalsLoginKeysBLL : Repository<OnlineApprovals_LoginKeys>
    {
        int ProviderID;
        int ProviderTypeID;
        string LoginKey;
        public OnlineApprovalsLoginKeysBLL(PhNetworkEntities Context,int ProviderTypeID,int ProviderID,string LoginKey) : base(Context)
        {
            this.ProviderID = ProviderID;
            this.ProviderTypeID = ProviderTypeID;
            this.LoginKey = LoginKey;
        }

        public string GenerateLoginKey (int ProviderID,int ProviderTypeID)
        {
            string GeneratedKey;
            try
            {
               // var check = DeactivePreviousKeys(ProviderID, ProviderTypeID);
               
                    GeneratedKey = GUIDUtilities.GenerateTimeBasedGuid().ToString();
                    OnlineApprovals_LoginKeys LoginKey = new OnlineApprovals_LoginKeys();

                    LoginKey.ProviderID = ProviderID;
                    LoginKey.ProviderTypeID = ProviderTypeID;
                    LoginKey.ProviderKey = GeneratedKey;
                    LoginKey.GenerationTime = DateTime.Now;
                    LoginKey.IsActive = true;
                    Add(LoginKey);
                    context.SaveChanges();
                    
                return GeneratedKey;
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateException(ex, "OnlineApprovals_LoginKeysBLL", "GenerateLoginKey");
                return string.Empty;
           }
        
        }

        public bool DeactivePreviousKeys(int ProviderID, int ProviderTypeID)
        {
            try
            {
                var Keys = GetAll().Where(p => p.ProviderID == ProviderID && p.ProviderTypeID == ProviderTypeID && p.IsActive==true).ToList();
                if (Keys.Any())
                {
                    foreach (var item in Keys)
                    {
                        item.IsActive = false;
                        Edit(item);
                    }
                    int check = context.SaveChanges();
                    if (check >= 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
        
               
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateException(ex, "OnlineApprovals_LoginKeysBLL", "DeactivePreviousKeys");
                throw;
            }
        }
    }
}
