using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using HRMS.BLL.UnitOfWork;
using HRMS.DAL.Factory;
using HRMS.Utilities;

namespace HRMS.API
{
    public class BaseController : ApiController
    {
        protected static ContainerContextFactory Factory = new ContainerContextFactory();
        public readonly UnitOfWork UnitOfWork;
        //public int UserID= ;//rateb
      //  public int UserID = 596;//minisy
       // public int UserID = 635;//abdelsatar
        //public int UserID = 656;//maher


        public BaseController()
        {
           // UserID = System.Web.HttpContext.Current?.Request?.Cookies["UserID"]?.Value == null ? -1 : Convert.ToInt32(System.Web.HttpContext.Current?.Request?.Cookies["UserID"]?.Value);
            UnitOfWork = new UnitOfWork( 1,DateTime.Now, Factory.Create());
        }

        
    }
}