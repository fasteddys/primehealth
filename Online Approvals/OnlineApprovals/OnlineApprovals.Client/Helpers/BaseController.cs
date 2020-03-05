using OnlineApprovals.BLL.UnitOfWork;
using OnlineApprovals.DAL.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineApprovals.Client
{
    public class BaseController : Controller
    {
        public int ProviderID = -1;
        public int ProviderTypeID = -1;
        public string LoginKey = "0";
        protected static ContainerContextFactory Factory;
        protected UnitOfWork UnitOfWork;
        public BaseController()
        {
            ProviderTypeID = System.Web.HttpContext.Current?.Request?.Cookies["ProviderTypeID"]?.Value == null?-1: Convert.ToInt32(System.Web.HttpContext.Current?.Request?.Cookies["ProviderTypeID"]?.Value);
            ProviderID = System.Web.HttpContext.Current?.Request?.Cookies["ProviderID"]?.Value == null ? -1 : Convert.ToInt32(System.Web.HttpContext.Current?.Request?.Cookies["ProviderID"]?.Value);
            LoginKey = System.Web.HttpContext.Current?.Request?.Cookies["BrowserLgGenerated"]?.Value == null ?"0" : System.Web.HttpContext.Current?.Request?.Cookies["BrowserLgGenerated"]?.Value;
            Factory = new ContainerContextFactory();
            UnitOfWork = new UnitOfWork(Factory.Create(),ProviderTypeID,ProviderID,LoginKey);
        }
     
    }
}