using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BNC.BLL.UnitOfWork;
using BNC.DAL.Factory;
using BNC.Utilities;

namespace BNC.Client
{
    public class BaseController : Controller
    {
        public int UserID = -1;
        public string UserName = "";
        protected static ContainerContextFactory Factory = new ContainerContextFactory();
        public readonly UnitOfWork UnitOfWork;
 
        public BaseController()
        {
           // UserID = System.Web.HttpContext.Current?.Request?.Cookies["UserID"]?.Value == null ? -1 : Convert.ToInt32(System.Web.HttpContext.Current?.Request?.Cookies["UserID"]?.Value);
            UnitOfWork = new UnitOfWork( 1,DateTime.Now, Factory.Create());

            UserID = System.Web.HttpContext.Current?.Request?.Cookies["ActiveUserIDCookie"]?.Value == null ? -1 : Convert.ToInt32(System.Web.HttpContext.Current?.Request?.Cookies["CypressUserID"]?.Value);
            UserName = System.Web.HttpContext.Current?.Request?.Cookies["UserNameCookie"]?.Value == "" ? "" : System.Web.HttpContext.Current?.Request?.Cookies["CypressUserName"]?.Value;
        }

        
    }
}