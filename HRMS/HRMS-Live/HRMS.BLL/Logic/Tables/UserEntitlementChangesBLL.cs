using HRMS.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Entities;

namespace HRMS.BLL.Logic.Tables
{
    public class UserEntitlementChangesBLL : Repository<UserEntitlementChange>
    {
        DateTime creationDate;
        public UserEntitlementChangesBLL(HRMSEntities Context,DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;


        }

        public List<UserEntitlementChange> GetUserEntitlementChangesByID(int UserID)
        {
            List<UserEntitlementChange> ListEntitlementChanges = new List<UserEntitlementChange>();
            ListEntitlementChanges =  Find(x => x.UserFK == UserID).ToList();

            return ListEntitlementChanges;
        }


    }
}
