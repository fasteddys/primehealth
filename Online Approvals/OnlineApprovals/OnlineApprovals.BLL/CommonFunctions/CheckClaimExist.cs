using OnlineApprovals.DAL.Repositories;
using OnlineApprovals.DTOs;
using OnlineApprovals.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineApprovals.BLL.CommonFunctions
{
    public class CheckClaimExist : Repository<OnlineApprovals_Requests>
    {
        public CheckClaimExist(PhNetworkEntities Context) : base(Context)
        {
        }

        public bool ClaimExist(long? ClaimNumber)
        {
            if (ClaimNumber==null)
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
