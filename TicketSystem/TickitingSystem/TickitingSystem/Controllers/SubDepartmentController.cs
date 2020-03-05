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
    public class SubDepartmentController : Controller
    {

        public TicketingDBEntities Db = new TicketingDBEntities();
        [Authorize]
        // GET: SubDepartmetn
        [Authorize]

        public ActionResult Index()
        {
            return View(Db.SubDeparments.ToList());
        }
        [Authorize]
        // GET: SubDeparment/Create
        public ActionResult Create()
        {
            var DeptId = (from d in Db.Departments select d).ToList();

            ViewBag.DepartmentId = DeptId;
        
            return View();
        }
        [Authorize]
        // POST: SubDeparment/Create
        [HttpPost]
        public ActionResult Create(SubDeparment SubDeparment)
        {
            if (ModelState.IsValid)
            {
                Db.SubDeparments.Add(SubDeparment);
                Db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(SubDeparment);
        }
        [Authorize]
        // GET: SubDeparment/Edit
        public ActionResult Edit(int? id)
        {
            var DeptId = (from d in Db.Departments select d).ToList();

            ViewBag.DepartmentId = DeptId;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubDeparment SubDeparment = Db.SubDeparments.Find(id);
            if (SubDeparment == null)
            {
                return HttpNotFound();
            }
            return View(SubDeparment);
        }
        [Authorize]
        // POST: SubDeparment/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SubDeparment SubDeparment)
        {
            if (ModelState.IsValid)
            {
                Db.Entry(SubDeparment).State = EntityState.Modified;
                Db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(SubDeparment);
        }
        [Authorize]
        // GET: SubDeparment/Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubDeparment SubDeparment = Db.SubDeparments.Find(id);
            if (SubDeparment == null)
            {
                return HttpNotFound();
            }
            return View(SubDeparment);
        }
        [Authorize]
        // POST: SubDeparment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubDeparment SubDeparment = Db.SubDeparments.Find(id);

            Db.SubDeparments.Remove(SubDeparment);
            SubDeparment.Dept_ID = id;
            Db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize]
        // GET: SubDepartment/Details/
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubDeparment SubDeparment = Db.SubDeparments.Find(id);

            if (SubDeparment == null)
            {
                return HttpNotFound();
            }

            return View(SubDeparment);
        }

    }
}
