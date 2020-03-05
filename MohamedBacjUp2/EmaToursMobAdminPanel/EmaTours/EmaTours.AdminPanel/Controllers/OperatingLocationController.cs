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
    public class OperatingLocationController : BaseController
    {

        [HttpPost]

        public ActionResult index(int id, string Password)
        {
            return View();
        }

        public ActionResult ViewAllOperatingLocation()

        {

            return View(UnitOfWork.OperatingLocationsBLL.GetAllOperatingLocations());
        }
    

        [HttpGet]
        public ActionResult AddOperatingLocation(OperatingLocationDTO OperatingLocation)
        {
            OperatingLocation.Languges = new List<LanguageDTO>();
            foreach (var item in UnitOfWork.LanguageBLL.GetAllLanguage())
            {
                OperatingLocation.Languges.Add(item);
            }
            OperatingLocation.Countries = new List<OperatingCountryDTO>();

            foreach (var item in UnitOfWork.OperatingCountriesBLL.GetAllOperatingCountries())
            {
                OperatingLocation.Countries.Add(item);
            }

            return View(OperatingLocation);
        }
        [HttpPost]
        public ActionResult AddNewOperatingLocation(OperatingLocationDTO OperatingLocation)
        {
            UnitOfWork.OperatingLocationsBLL.AddOperatingLocation(OperatingLocation);
            UnitOfWork.Submit();
            OperatingLocationDTO newOperatingLocation = new OperatingLocationDTO();
            newOperatingLocation.Languges = new List<LanguageDTO>();
            foreach (var item in UnitOfWork.LanguageBLL.GetAllLanguage())
            {
                newOperatingLocation.Languges.Add(item);
            }
            newOperatingLocation.Countries = new List<OperatingCountryDTO>();

            foreach (var item in UnitOfWork.OperatingCountriesBLL.GetAllOperatingCountries())
            {
                newOperatingLocation.Countries.Add(item);
            }


            return View("AddOperatingLocation", newOperatingLocation);
        }




        [HttpGet]
        public ActionResult EditOperatingLocation(int OperatingLocationID)
        {
            OperatingLocationDTO OperatingLocation = UnitOfWork.OperatingLocationsBLL.GetOperatingLocation(OperatingLocationID);

            OperatingLocation.Languges = new List<LanguageDTO>();
            foreach (var item in UnitOfWork.LanguageBLL.GetAllLanguage())
            {
                OperatingLocation.Languges.Add(item);
            }

            OperatingLocation.Countries = new List<OperatingCountryDTO>();
            foreach (var item in UnitOfWork.OperatingCountriesBLL.GetAll())
            {
                OperatingLocation.Countries.Add(new OperatingCountryDTO {
                      OperatingCountryName= item.ConutryName,
                       OperatingCountryID= item.CountryID
                });
            }

            return View(OperatingLocation);
        }
        [HttpPost]
        public ActionResult EditOperatingLocation(OperatingLocationDTO OperatingLocation)
        {
            UnitOfWork.OperatingLocationsBLL.UpdateOperatingLocation(OperatingLocation);
            UnitOfWork.Submit();


            return Redirect("/OperatingLocation/EditOperatingLocation?OperatingLocationID=" + OperatingLocation.OperatingLocationID);
        }



        //public ActionResult ViewAllOperatingLocation(int LangugeID)
        //{

        //    return View(UnitOfWork.OperatingCountriesBLL.GetAllOperatingCountries(LangugeID));
        //}



    }
}