using EmaTours.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmaTours.Entities;

namespace EmaTours.BLL.Logic.Tables
{
    public class EMAUsersBLL : Repository<EMAUser>
    {
        DateTime creationDate;

        public EMAUsersBLL(EMAToursEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
        }

        public void AddUser()
        {

        }
        public void UpdateUser()
        {

        }

        public void DeActivateUser()
        {

        }


    }

}
