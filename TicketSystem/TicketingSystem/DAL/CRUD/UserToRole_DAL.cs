using DAL.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.CRUD
{
 public   class UserToRole_DAL
    {
        TicketingDB_updateEntities Db = new TicketingDB_updateEntities();
        //Add new UserToRole
        public void ADDUserToRole(UserToRole UserToRole)
        {
            Db.UserToRoles.Add(UserToRole);
            Db.SaveChanges();
        }
        //Edit a UserToRole
        public void EditUserToRole(UserToRole UserToRole)
        {
            UserToRole = Db.UserToRoles.Find(UserToRole.ID);
            // Db.UserToRoles.Attach(UserToRole);
            Db.Entry(UserToRole).State = System.Data.Entity.EntityState.Modified;
            Db.SaveChanges();

        }
        //Select All UserToRoles
        public List<UserToRole> GetAllUserToRoles()
        {


            return Db.UserToRoles.ToList();

        }
        //Select one UserToRole By ID
        public UserToRole GetByUserToRoleID(int? ID)
        {


            return Db.UserToRoles.Where(x => x.ID == ID).SingleOrDefault();

        }

        public List< UserToRole> GetRoleByUserID(int? ID)
        {


            return Db.UserToRoles.Where(x => x.UserFK == ID).ToList();

        }
        public void DeleteRoleByUserID(int ID)
        {
            UserToRole UserToRole = Db.UserToRoles.Find(ID);
            if (UserToRole != null)
            {
                Db.Entry(UserToRole).State = System.Data.Entity.EntityState.Deleted;
                Db.SaveChanges();
            }

        }


        //Select using customized exprission from UserToRole
        public List<UserToRole> GetBycustomizedExpUserToRole(Expression<Func<UserToRole, bool>> expression)
        {
            return Db.UserToRoles.Where(expression).ToList();
        }


    }
}
