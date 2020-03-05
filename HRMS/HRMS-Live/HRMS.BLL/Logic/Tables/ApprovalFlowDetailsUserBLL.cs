using HRMS.DAL.Repositories;
using HRMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.BLL.Logic.Tables
{
    class ApprovalFlowUserDetailsBLL : Repository<ApprovalFlowUserDetail>
    {
        DateTime creationDate;

        public ApprovalFlowUserDetailsBLL(HRMSEntities Context, DateTime CreationDate) : base(Context) 
        {
             creationDate= CreationDate;

        }
    }
}
