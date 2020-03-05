using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EmaTours.AdminPanel.Helper;
using EmaTours.DTOs.Business;
using EmaTours.Entities;

namespace EmaTours.AdminPanel.Controllers
{
    public class HotelsController : BaseController
    {
        //private EMAToursEntities db = new EMAToursEntities();

        // GET: Hotels
        public ActionResult Index()
        {
            List<HotelDTO> hotes = new List<HotelDTO>();
            var result = UnitOfWork.HotelsBLL.GetAll();
            foreach (var item in result)
            {
                hotes.Add(UnitOfWork.HotelsBLL.MapTOHotelDTO(item));
            }
            ViewBag.Languages = UnitOfWork.LanguageBLL.GetAllLanguage();
            ViewBag.Countries = UnitOfWork.OperatingCountriesBLL.GetAllOperatingCountries();
            ViewBag.Locations = UnitOfWork.OperatingLocationsBLL.GetAllOperatingLocations();

            return View(hotes);
        }

        // GET: Hotels/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelDTO hotel = UnitOfWork.HotelsBLL.SearchHotels(HoteId:id).SingleOrDefault();
            //HotelDTO hotel = UnitOfWork.HotelsBLL.MapTOHotelDTO(UnitOfWork.HotelsBLL.Find(x => x.HotelID == id).SingleOrDefault());
            ViewBag.Languages = UnitOfWork.LanguageBLL.GetAllLanguage();
            ViewBag.Countries = UnitOfWork.OperatingCountriesBLL.GetAllOperatingCountries();
            ViewBag.Locations = UnitOfWork.OperatingLocationsBLL.GetAllOperatingLocations();
            if (hotel == null)
            {
                return HttpNotFound();
            }
            return View(hotel);
        }

        // GET: Hotels/Create
        public ActionResult Create()
        {
            ViewBag.Languages = UnitOfWork.LanguageBLL.GetAllLanguage();
            ViewBag.Countries = UnitOfWork.OperatingCountriesBLL.GetAllOperatingCountries();
            ViewBag.Locations = UnitOfWork.OperatingLocationsBLL.GetAllOperatingLocations();
            return View();
        }

        // POST: Hotels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HotelID,Name,LanguageFK,OperatingCountryFK,OperatingLocationFK")] Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                UnitOfWork.HotelsBLL.Add(hotel);
                UnitOfWork.Submit();
                return RedirectToAction("Index");
            }

            return View(hotel);
        }

        // GET: Hotels/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelDTO hotel = UnitOfWork.HotelsBLL.MapTOHotelDTO(UnitOfWork.HotelsBLL.Find(x=>x.HotelID==id).SingleOrDefault());
            ViewBag.Languages = UnitOfWork.LanguageBLL.GetAllLanguage();
            ViewBag.Countries = UnitOfWork.OperatingCountriesBLL.GetAllOperatingCountries();
            ViewBag.Locations = UnitOfWork.OperatingLocationsBLL.GetAllOperatingLocations();

            if (hotel == null)
            {
                return HttpNotFound();
            }
            return View(hotel);
        }

        // POST: Hotels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HotelID,Name,LanguageFK,OperatingCountryFK,OperatingLocationFK")] Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                UnitOfWork.HotelsBLL.Edit(hotel) ;
                UnitOfWork.Submit();
                return RedirectToAction("Index");
            }
            return View(hotel);
        }

        // GET: Hotels/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelDTO hotel = UnitOfWork.HotelsBLL.MapTOHotelDTO(UnitOfWork.HotelsBLL.Find(x => x.HotelID == id).SingleOrDefault());
            if (hotel == null)
            {
                return HttpNotFound();
            }
            return View(hotel);
        }

        // POST: Hotels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HotelDTO hotel = UnitOfWork.HotelsBLL.MapTOHotelDTO(UnitOfWork.HotelsBLL.Find(x => x.HotelID == id).SingleOrDefault());
            UnitOfWork.HotelsBLL.Delete(UnitOfWork.HotelsBLL.MapTOHotel(hotel));
            UnitOfWork.Submit();
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
