﻿using EmaTours.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmaTours.Entities;

namespace EmaTours.BLL.Logic.Tables
{
    public class EMAUserTypesBLL : Repository<EMAUserTypesDIM>
    {
        DateTime creationDate;

        public EMAUserTypesBLL(EMAToursEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
        }




    }

}
