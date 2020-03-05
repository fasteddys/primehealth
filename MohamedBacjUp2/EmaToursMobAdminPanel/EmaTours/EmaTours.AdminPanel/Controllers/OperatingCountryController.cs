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
    public class OperatingCountryController : BaseController
    {

        [HttpPost]

        public ActionResult index(int id, string Password)
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddOperatingCountry(OperatingCountryDTO OperatingCountry)
        {
            OperatingCountry.Languges = new List<LanguageDTO>();
            foreach (var item in UnitOfWork.LanguageBLL.GetAllLanguage())
            {
                OperatingCountry.Languges.Add(item);
            }
            return View(OperatingCountry);
        }
        [HttpPost]
        public ActionResult AddNewOperatingCountry(OperatingCountryDTO OperatingCountry)
        {
            UnitOfWork.OperatingCountriesBLL.GetAllOperatingCountries(OperatingCountry);
            UnitOfWork.Submit();
            OperatingCountryDTO newOperatingCountry = new OperatingCountryDTO();
            newOperatingCountry.Languges = new List<LanguageDTO>();
            OperatingCountry.Languges = new List<LanguageDTO>();
            foreach (var item in UnitOfWork.LanguageBLL.GetAllLanguage())
            {
                OperatingCountry.Languges.Add(item);
            }
            return View("AddOperatingCountry", OperatingCountry);
        }

        public ActionResult ViewAllOperatingCountry()
        {

            return View(UnitOfWork.OperatingCountriesBLL.GetAllOperatingCountries());
        }

        [HttpGet]
        public ActionResult EditOperatingCountry(int OperatingCountryID)
        {
            OperatingCountryDTO OperatingCountry = UnitOfWork.OperatingCountriesBLL.GetOperatingCountryByID(OperatingCountryID);

            OperatingCountry.Languges = new List<LanguageDTO>();
            foreach (var item in UnitOfWork.LanguageBLL.GetAllLanguage())
            {
                OperatingCountry.Languges.Add(item);
            }

            return View(OperatingCountry);
        }
        [HttpPost]
        public ActionResult EditOperatingCountry(OperatingCountryDTO OperatingCountry)
        {
            UnitOfWork.OperatingCountriesBLL.EditOperatingCountry(OperatingCountry);
            UnitOfWork.Submit();


            return Redirect("/OperatingCountry/EditOperatingCountry?OperatingCountryID=" + OperatingCountry.OperatingCountryID);
        }



    }
}