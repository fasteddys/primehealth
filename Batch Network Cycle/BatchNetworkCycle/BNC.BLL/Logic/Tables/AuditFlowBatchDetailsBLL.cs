using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BNC.DAL.Repositories;
using BNC.Entities;
using BNC.DTOs;

namespace BNC.BLL.Logic.Tables
{
    public class AuditFlowBatchDetailsBLL : Repository<AuditFlowBatchDetail>
    {
        DateTime creationDate;

        public AuditFlowBatchDetailsBLL(BNCEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
        }
        //15)AuditCategoryFK=medical or finicial ....
        public Nullable<DateTime> getAuditCategoryCompilationDate(int BatchAuditingRequestID, int AuditCategoryFK)
        {
            return Find(aFBD => aFBD.BatchAuditRequestFK == BatchAuditingRequestID && aFBD.AuditCategoryFK == AuditCategoryFK).FirstOrDefault().CompilationDate;
        }
        //16)
        public int getAuditOrder(int BatchAuditingRequestID, int AuditCategoryFK)
        {
            return Find(aFBD => aFBD.BatchAuditRequestFK == BatchAuditingRequestID && aFBD.AuditCategoryFK == AuditCategoryFK).FirstOrDefault().AuditOrder;
        }
    }
}
