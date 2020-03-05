using HRMS.DAL.Repositories;
using HRMS.DTOs.Business;
using HRMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.BLL.Logic.Tables
{
    public class LeaveTypeRestrictionTypeBLL : Repository<LeaveTypeRestrictionTypeDIM>
    {

        public LeaveTypeRestrictionTypeBLL(HRMSEntities Context) : base(Context)
        {
        }
        public List<LeaveTypeRestrictionDTO> GetAllRestriction() 
        {
           List< LeaveTypeRestrictionDTO> LeaveTypeRestrictionDTO = new List<LeaveTypeRestrictionDTO> ();
            foreach (var item in GetAll())
            {
                LeaveTypeRestrictionDTO.Add(new LeaveTypeRestrictionDTO
                {
                    RestrictionName = item.RestrictionName,
                    ArName = item.ArName,
                    RestrictionDescription = item.RestrictionDescription,
                    LeaveTypeRestrictionTypeID = item.LeaveTypeRestrictionTypeID,
                    RestrictionDescriptionAr = item.RestrictionDescriptionAr
                    
                });
            }
            return LeaveTypeRestrictionDTO;

        }

    }
}
