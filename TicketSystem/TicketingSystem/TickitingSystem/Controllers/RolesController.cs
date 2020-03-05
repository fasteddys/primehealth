using DAL.CRUD;
using DAL.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TickitingSystem.Models;

namespace TickitingSystem.Controllers
{
    public class RolesController : Controller
    {
        // GET: Roles
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreatRole()
        {


            RoleManagment rolemanagment = new RoleManagment();
            Permission_DAL permissionDAL = new Permission_DAL();

            rolemanagment.permission= permissionDAL.GetAllPermissions();

            return View(rolemanagment);
        }
        [HttpPost]
        public ActionResult CreatRole(RoleManagment rolemanagment)
        {
            RoleToRoleToPermission_DAL permissionDAL = new RoleToRoleToPermission_DAL();
            Role_DAL roleDAL = new Role_DAL();
            roleDAL.ADDRole(rolemanagment.role);
            foreach (var item in rolemanagment.permission)
            {
                RoleToPermission roleperm = new RoleToPermission();
                roleperm.PermissionFK = item.ID;
                roleperm.RoleFK = rolemanagment.role.ID;
                permissionDAL.ADDRoleToPermission(roleperm);


            }

             return RedirectToAction("CreatRole", "Roles");
        }

     



    }
}