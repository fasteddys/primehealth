using DAL.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.CRUD
{
  public  class Role_DAL
    {

        TicketingDB_updateEntities Db = new TicketingDB_updateEntities();
        //Add new Role
        public void ADDRole(Role Role)
        {
            Db.Roles.Add(Role);
            Db.SaveChanges();
        }
        //Edit a Role
        public void EditRole(Role Role)
        {
            Role = Db.Roles.Find(Role.ID);
            // Db.Roles.Attach(Role);
            Db.Entry(Role).State = System.Data.Entity.EntityState.Modified;
            Db.SaveChanges();

        }
        //Select All Roles
        public List<Role> GetAllRoles()
        {


            return Db.Roles.ToList();

        }
        //Select one Role By ID
        public Role GetByRoleID(int? ID)
        {

            return Db.Roles.Where(x => x.ID == ID).SingleOrDefault();

        }

     


        //Select using customized exprission from Role
        public List<Role> GetBycustomizedExpRole(Expression<Func<Role, bool>> expression)
        {
            return Db.Roles.Where(expression).ToList();
        }
        public List<int> GetListOfRolesByUserID(int? UserID)
        {
            List<int> roles =    ( //x.Seen_By_User == null&&x.User_ID!= theloginuser.User_ID
                   from post in Db.Roles
                   join meta in Db.UserToRoles on post.ID equals meta.RoleFK
                   where meta.UserFK==UserID
                   select new  { ID = post.ID, Name = post.Name, Description = post.Description }).ToList()

           .Select(x => x.ID).ToList();
            return roles;

        }

    }
     
    
}
