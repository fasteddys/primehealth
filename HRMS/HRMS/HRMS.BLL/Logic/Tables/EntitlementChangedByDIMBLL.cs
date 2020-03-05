using HRMS.DAL.Repositories;
using HRMS.DTOs.Business;
using HRMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.BLL.Logic.Tables
{
    public class EntitlementChangedByDIMBLL : Repository<EntitlementChangedByDIM>
    {
        DateTime creationDate;

        public EntitlementChangedByDIMBLL(HRMSEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
        }

        public List<EntitlementChangedByDTO> GetAllEntitlementChangedBy()
        {
            List<EntitlementChangedByDTO> entitlementChangedByDIM = new List<EntitlementChangedByDTO>();

            foreach(var item in GetAll().Where(x => x.IsActive == true).ToList())
            {
                entitlementChangedByDIM.Add(new EntitlementChangedByDTO {
                    EntitlementChangedByID = item.EntitlementChangedByDIMID,
                    EntitlementChangedByName = item.EntitlementChangedByName
                });
            }
            return entitlementChangedByDIM;
        }
    }
}
