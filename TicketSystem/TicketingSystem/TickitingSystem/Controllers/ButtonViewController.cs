using DAL.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TickitingSystem.Controllers
{
    public class ButtonViewController : Controller
    {
        Permission_DAL permission = new Permission_DAL();

        // GET: ButtonView
        public PartialViewResult CloseButton()
        {
            if (permission.CheckIfUserHasPermission("ButtonView-CloseButton"))
            {
                return PartialView();
            }
            else { return null; }
        }

        public PartialViewResult ReplyButton()
        {
            if (permission.CheckIfUserHasPermission("ButtonView-ReplyButton"))
            {
                return PartialView();
            }
            else { return null; }
        }
        public PartialViewResult AssignButton()
        {
            if (permission.CheckIfUserHasPermission("ButtonView-AssignButton"))
            {
                return PartialView();
            }
            else { return null; }
        }
        public PartialViewResult OpenRequestItfusionButton()
        {
            if (permission.CheckIfUserHasPermission("ButtonView-OpenRequestItfusionButton"))
            {
                return PartialView();
            }
            else { return null; }
        }
        public PartialViewResult OpenRequestDataQuestButton()
        {
            if (permission.CheckIfUserHasPermission("ButtonView-OpenRequestDataQuestButton"))
            {
                return PartialView();
            }
            else { return null; }
        }
        public PartialViewResult ReopenButton()
        {
            if (permission.CheckIfUserHasPermission("ButtonView-ReopenButton"))
            {
                return PartialView();
            }
            else { return null; }
        }
    }
}