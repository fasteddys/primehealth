using HRMS.DAL.Repositories;
using HRMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.DTOs.Business;
namespace HRMS.BLL.Logic.Tables
{
    public class LeaveTypeFieldsBLL : Repository<LeaveTypeField>
    {
        DateTime creationDate;

        public LeaveTypeFieldsBLL(HRMSEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;


        }
    }
}
