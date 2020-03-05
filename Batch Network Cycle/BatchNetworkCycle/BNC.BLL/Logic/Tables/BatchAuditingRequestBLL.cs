using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BNC.DAL.Repositories;
using BNC.Entities;
using BNC.DTOs;
using BNC.DTOs.Business;
using static BNC.BLL.StaticData.StaticEnums;

namespace BNC.BLL.Logic.Tables
{
    public class BatchAuditingRequestBLL : Repository<BatchAuditingRequest>
    {
        DateTime creationDate;
        public BatchAuditingRequestBLL(BNCEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
        }

    }
}
