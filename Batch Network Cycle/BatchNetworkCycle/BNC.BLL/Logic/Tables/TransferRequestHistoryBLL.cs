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
    public class TransferRequestHistoryBLL : Repository<TransferRequestHistory>
    {
        DateTime creationDate;

        public TransferRequestHistoryBLL(BNCEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
        }
    
    }
}
