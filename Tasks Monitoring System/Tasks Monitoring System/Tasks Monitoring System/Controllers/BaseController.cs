using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TasksMonitoringSystem.Controllers
{
    public class BaseController : Controller
    {

        public int UserID = -1;
        public string UserName = "";
        public bool IsAdmin = false;
        public BaseController()
        {
            UserID = System.Web.HttpContext.Current?.Request?.Cookies["CypressUserID"]?.Value == null ? -1 : Convert.ToInt32(System.Web.HttpContext.Current?.Request?.Cookies["CypressUserID"]?.Value);
            UserName = System.Web.HttpContext.Current?.Request?.Cookies["CypressUserName"]?.Value == "" ? "" : System.Web.HttpContext.Current?.Request?.Cookies["CypressUserName"]?.Value;
            IsAdmin = System.Web.HttpContext.Current?.Request?.Cookies["IsAdmin"]?.Value == null ? false : Convert.ToBoolean(System.Web.HttpContext.Current?.Request?.Cookies["IsAdmin"]?.Value);
        }
    }
}