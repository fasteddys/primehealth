using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BNC.DAL.Repositories;
using BNC.Entities;

namespace BNC.BLL.Logic.Tables
{
    public class LogsDetailControlUserBLL /*: Repository<LogsDetail>*/
    {
        DateTime creationDate;

        public LogsDetailControlUserBLL(BNCEntities Context, DateTime CreationDate) /*: base(Context)*/
        {
            creationDate = CreationDate;
        }
    


    }
}
