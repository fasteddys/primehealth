using DAL.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.CRUD
{
   public class RoleToRoleToPermission_DAL
    {
        TicketingDB_updateEntities Db = new TicketingDB_updateEntities();
        //Add new RoleToPermission
        public void ADDRoleToPermission(RoleToPermission RoleToPermission)
        {
            Db.RoleToPermissions.Add(RoleToPermission);
            Db.SaveChanges();
        }
        //Edit a RoleToPermission
        public void EditRoleToPermission(RoleToPermission RoleToPermission)
        {
            RoleToPermission = Db.RoleToPermissions.Find(RoleToPermission.ID);
            // Db.RoleToPermissions.Attach(RoleToPermission);
            Db.Entry(RoleToPermission).State = System.Data.Entity.EntityState.Modified;
            Db.SaveChanges();

        }
        //Select All RoleToPermissions
        public List<RoleToPermission> GetAllRoleToPermissions()
        {


            return Db.RoleToPermissions.ToList();

        }
        //Select one RoleToPermission By ID
        public RoleToPermission GetByRoleToPermissionID(int? ID)
        {


            return Db.RoleToPermissions.Where(x => x.ID == ID).SingleOrDefault();

        }
        //Select using customized exprission from RoleToPermission
        public List<RoleToPermission> GetBycustomizedExpRoleToPermission(Expression<Func<RoleToPermission, bool>> expression)
        {
            return Db.RoleToPermissions.Where(expression).ToList();
        }


    }
}
