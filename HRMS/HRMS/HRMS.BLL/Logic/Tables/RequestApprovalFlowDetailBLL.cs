using HRMS.DAL.Repositories;
using HRMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.BLL.Logic.Tables
{
    public class ApprovalFlowRequestDetailsBLL : Repository<ApprovalFlowRequestDetail>
    {
        DateTime creationDate;
        public ApprovalFlowRequestDetailsBLL(HRMSEntities Context,DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;

        }


    }
}
