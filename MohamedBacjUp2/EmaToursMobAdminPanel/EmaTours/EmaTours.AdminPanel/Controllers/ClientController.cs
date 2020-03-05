using EmaTours.AdminPanel.Helper;
using EmaTours.BLL.Logic.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EmaTours.AdminPanel.Controllers
{
    public class ClientController : BaseController
    {

        [HttpPost]

        public ActionResult index(int id, string Password)
        {
            return View();
        }
        public ActionResult ViewAllClients()
        {
            
            return View(UnitOfWork.ClientsBLL.GetAllClients());
        }
        public ActionResult ViewClientsDetails(int ClientID)
        {

            return View(UnitOfWork.ClientsBLL.GetAllClientData(ClientID));
        }
    }
}