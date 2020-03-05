﻿using HRMS.DAL.Repositories;
using HRMS.DTOs.Business;
using HRMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HRMS.BLL.StaticData.StaticEnums;

namespace HRMS.BLL.Logic.Tables
{
    public class RequestAttachmentBLL : Repository<RequestAttachment>
    {
        public readonly DeviceBLL DeviceBLL;
        DateTime creationDate;
        public RequestAttachmentBLL(HRMSEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;

            DeviceBLL = new DeviceBLL(Context, CreationDate);
        }




    }


}
