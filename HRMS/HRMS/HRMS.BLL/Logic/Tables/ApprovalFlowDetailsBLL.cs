using HRMS.DAL.Repositories;
using HRMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.BLL.Logic.Tables
{
public    class ApprovalFlowDetailsBLL:Repository<ApprovalFlowDetail>
    {
        DateTime creationDate;

        public ApprovalFlowDetailsBLL(HRMSEntities Context, DateTime CreationDate) : base(Context) { 
            creationDate = CreationDate;


        }


    }
}
