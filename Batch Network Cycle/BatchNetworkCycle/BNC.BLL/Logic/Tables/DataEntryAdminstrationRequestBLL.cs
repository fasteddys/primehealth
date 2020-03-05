using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BNC.DAL.Repositories;
using BNC.Entities;
using BNC.DTOs;
using BNC.DTOs.Business;
using BNC.BLL.StaticData;
using static BNC.BLL.StaticData.StaticEnums;

namespace BNC.BLL.Logic.Tables
{
    public class DataEntryAdminstrationRequestBLL : Repository<DataEntryAdminstrationRequest>
    {
        DateTime creationDate;
        public DataEntryAdminstrationRequestBLL(BNCEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
        }
   }
}
