using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BNC.DAL.Repositories;
using BNC.Entities;
using BNC.DTOs;
using static BNC.BLL.StaticData.StaticEnums;

namespace BNC.BLL.Logic.Tables
{
    public class FinancialAuditRequestBLL : Repository<FinancialAuditRequest>
    {
        DateTime creationDate;

        public FinancialAuditRequestBLL(BNCEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
        }

    }
}
