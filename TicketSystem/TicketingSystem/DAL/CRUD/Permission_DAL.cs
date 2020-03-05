using DAL.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.CRUD
{
  public  class Permission_DAL
    {
    public    static int Userid;


        TicketingDB_updateEntities Db = new TicketingDB_updateEntities();
        //Add new Permission
        public void ADDPermission(Permission Permission)
        {
            Db.Permissions.Add(Permission);
            Db.SaveChanges();
        }
        //Edit a Permission
        public void EditPermission(Permission Permission)
        {
            Permission = Db.Permissions.Find(Permission.ID);
            // Db.Permissions.Attach(Permission);
            Db.Entry(Permission).State = System.Data.Entity.EntityState.Modified;
            Db.SaveChanges();

        }
        //Select All Permissions
        public List<Permission> GetAllPermissions()
        {
            return Db.Permissions.ToList();
        }
        //Select one Permission By ID
        public Permission GetByPermissionID(int? ID)
        {
            return Db.Permissions.Where(x => x.ID == ID).SingleOrDefault();
        }
        //Select using customized exprission from Permission
        public List<Permission> GetBycustomizedExpPermission(Expression<Func<Permission, bool>> expression)
        {
            return Db.Permissions.Where(expression).ToList();
        }
        
        public int Get_Permission(string con_Action)
        {
            int id = 0;
            var user = Db.Permissions.Where(x => x.Name == con_Action).FirstOrDefault();
            if(user!=null)
            {

                 id = Db.Permissions.Where(x => x.Name == con_Action).First().ID ;

            }
            return id;

         
        }


        public bool CheckIfUserHasPermission(string Con_Action)
        {
            User_DAL userDAL = new User_DAL();
          //  User user = userDAL.GetUserById(int.Parse(@Request.Cookies["UserIDCookie"].Value));

            Role_DAL roleDAL = new Role_DAL();
            List<int> roles = roleDAL.GetListOfRolesByUserID(Userid);

            var id = Get_Permission(Con_Action);
            var count = ( //x.Seen_By_User == null&&x.User_ID!= theloginuser.User_ID
                 from post in Db.RoleToPermissions
                 join meta in Db.Permissions on post.PermissionFK equals meta.ID
                 join item in roles on post.RoleFK equals item
                 where post.PermissionFK == id
                 select post).ToList().Count();
            if (count < 1)
            {
                return false;
            }
            else
            {
                return true;
            }



        }

    }
}
