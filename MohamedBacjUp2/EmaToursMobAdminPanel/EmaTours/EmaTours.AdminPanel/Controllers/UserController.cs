using EmaTours.AdminPanel.Helper;
using EmaTours.BLL.Logic.Tables;
using EmaTours.DTOs.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EmaTours.AdminPanel.Controllers
{
    public class UserController : BaseController
    {

        [HttpPost]

        public ActionResult index(int id, string Password)
        {
            return View();
        }
        public ActionResult ViewAllUsers()
        {
            
            return View(UnitOfWork.EMAUsersBLL.ViewAllUsers());
        }

        [HttpGet]
        public ActionResult AddUser(UserDTO User)
        {
              return View(User);
        }
        [HttpPost]
        public ActionResult AddNewUser(UserDTO User)
        {
            UnitOfWork.EMAUsersBLL.AddUsers(User);
            UnitOfWork.Submit();
            UserDTO NewUser = new UserDTO();
            return View("AddUser", NewUser);
        }
        [HttpGet]
        public ActionResult EditUser(int UserID)
        {
         
            return View(UnitOfWork.EMAUsersBLL.GetUserByID(UserID));

        }
        [HttpPost]
        public ActionResult EditUser(UserDTO user)
        {
            UnitOfWork.EMAUsersBLL.EditUsers(user);
            UnitOfWork.Submit();


            return Redirect("/User/EditUser?UserID=" + user.UserID);
        }





    }
}