﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRMS.BLL.UnitOfWork;
namespace HRMS.API.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {             
            return View();
        }
    }
}