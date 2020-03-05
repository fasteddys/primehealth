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
    public class ReleaseActionBLL : Repository<ReleaseActionDIM>
    {
        DateTime creationDate;

        public ReleaseActionBLL(BNCEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
        }
    
    }
}
