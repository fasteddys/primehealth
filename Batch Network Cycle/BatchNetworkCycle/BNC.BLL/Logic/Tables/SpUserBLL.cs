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
    public class SpUserBLL : Repository<SPUser>
    {
        DateTime creationDate;
        UserBLL UserBLL;

        public SpUserBLL(BNCEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
            UserBLL = new UserBLL(context, creationDate);
        }

        public int GetSpUserId(int userFk)
        {
            SPUser spUser = this.Find(u => u.UserFK == userFk).FirstOrDefault();
            if(spUser != null)
            {
                return spUser.SPUserID;
            }
            return -1;
        }
        public User GetUser(int UserFK)
        {
            return UserBLL.Find(u => u.UserID == UserFK).FirstOrDefault();
        }
        public SPUser GetSpUser(int spUserFK)
        {
            return Find(spu => spu.SPUserID == spUserFK).FirstOrDefault();
        }

        public User GetSpUserData(int spUserFK)
        {
            SPUser spUser = GetSpUser(spUserFK);
            User userData;

            if (spUser != null)
            {
                userData = GetUser(spUser.UserFK);
                return userData;

            }

            else
            {
                return null;
            }
        }
    }
}
