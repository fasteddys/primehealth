using BNC.DAL.Repositories;
using BNC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNC.BLL.Logic.Tables
{
    class ActionBLL : Repository<ActionDIM>
    {
        DateTime creationDate;

        public ActionBLL(BNCEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
        }

    }
}
