using DAL.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TickitingSystem.Controllers
{
    public class sidebarController : Controller
    {
        // GET: sidebar
        //public PartialViewResult Dashboard()
        //     { }
        Permission_DAL permission = new Permission_DAL();

        public PartialViewResult DashboardTab()
        {
            if (permission.CheckIfUserHasPermission("sidebar-DashboardTab"))
            {
                return PartialView();
            }
            else { return null; }
        }
   

        public PartialViewResult UserTab()
        {
            if (permission.CheckIfUserHasPermission("sidebar-UserTab"))
            {
                return PartialView();
            }
            else { return null; }
        }
        public PartialViewResult DepartmentTab()
        {
            if (permission.CheckIfUserHasPermission("sidebar-DepartmentTab"))
            {
                return PartialView();
            }
            else { return null; }
        }
        public PartialViewResult SupDepartmentTab()
        {
            if (permission.CheckIfUserHasPermission("sidebar-SupDepartmentTab"))
            {
                return PartialView();
            }
            else { return null; }
        }
        public PartialViewResult NewTicketTab()
        {
            if (permission.CheckIfUserHasPermission("sidebar-NewTicketTab"))
            {
                return PartialView();
            }
            else { return null; }
        }
        public PartialViewResult myassignTicketItFusionTab()
        {
            if (permission.CheckIfUserHasPermission("sidebar-myassignTicketItFusionTab"))
            {
                return PartialView();
            }
            else { return null; }
        }
        public PartialViewResult myassignTicketDataQuestTab()
        {
            if (permission.CheckIfUserHasPermission("sidebar-myassignTicketDataQuestTab"))
            {
                return PartialView();
            }
            else { return null; }
        }
    }
}