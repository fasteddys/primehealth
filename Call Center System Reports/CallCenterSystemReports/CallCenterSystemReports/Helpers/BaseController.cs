using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CallCenterSystemReports.BLL.UnitOfWork;
using CallCenterSystemReports.DAL.Factory;

namespace CallCenterSystemReports.Controllers
{
    public class BaseController : Controller
    {
        protected static ContainerContextFactory Factory = new ContainerContextFactory();
        protected UnitOfWork UnitOfWork = new UnitOfWork(1, DateTime.Now, true, 1, Factory.Create());
    }
}