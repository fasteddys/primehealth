using DAL.DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace TickitingSystem.Controllers
{


    public class DepartmentController : Controller
    {
      public TicketingDB_updateEntities Db = new TicketingDB_updateEntities();

        [Authorize]
        public ActionResult Index()
        {
            return View(Db.Departments.ToList());
        }

        [Authorize]
        // GET: Deparment/Create
        public ActionResult Create()
        {
            return View();
        }
        [Authorize]
        // POST: Deparment/Create
        [HttpPost]
        public ActionResult Create(Department Department)
        {
            if (ModelState.IsValid)
            {
                Db.Departments.Add(Department);
                Db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Department);
        }


        [Authorize]
        // GET: Department/Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department Department = Db.Departments.Find(id);
            if (Department == null)
            {
                return HttpNotFound();
            }
            return View(Department);
        }
        [Authorize]
        // POST: Department/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Department Department = Db.Departments.Find(id);
           
            Db.Departments.Remove(Department);
            Department.Dept_Id = id;
            Db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        // GET: Department/Details/
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department Department = Db.Departments.Find(id);
           
            if (Department == null)
            {
                return HttpNotFound();
            }
          
            return View(Department);
        }

        [Authorize]
        // GET: Department/Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department Department = Db.Departments.Find(id);
            if (Department == null)
            {
                return HttpNotFound();
            }
            return View(Department);
        }
        [Authorize]
        // POST: Department/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Department Department)
        {
            if (ModelState.IsValid)
            {
                Db.Entry(Department).State = EntityState.Modified;
                Db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Department);
        }
    }
}