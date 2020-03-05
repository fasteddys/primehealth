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
    public class AuditCategoryBLL : Repository<AuditCategoryDIM>
    {
        DateTime creationDate;

        public AuditCategoryBLL(BNCEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
        }
        //--------------------------------------------------------------------------------------
        //17)medical or finicial ....
        public string getCategoryAuditName(int AuditCategoryFK)
        {
            return Find(ac => ac.AuditCategoriesID == AuditCategoryFK).FirstOrDefault().AuditCategoriesName;
        }
        //--------------------------------------------------------------------------------------
    }
}
