using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hr_System.Models;
using System.Net;
using System.Data;

namespace Hr_System.Controllers
{
    public class DepartmentController : Controller
    {
        private HRSystemEntities db = new HRSystemEntities();

        // GET: /Department/
        public ActionResult Index()
        {
            return View(db.DepartmentTBs.ToList());
        }

        // GET: /Department/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepartmentTB departmenttb = db.DepartmentTBs.Find(id);
            if (departmenttb == null)
            {
                return HttpNotFound();
            }
            return View(departmenttb);
        }

        // GET: /Department/Create
        public ActionResult Create()
        {
            //ViewBag.DeptManager = new SelectList(db.accountTBs.Where(p => p.EmpType == "Manager"), "EmpName", "EmpName");
            return View();
        }

        // POST: /Department/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DepartmentTB departmenttb)
        {
            if (ModelState.IsValid)
            {
                db.DepartmentTBs.Add(departmenttb);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.DeptManager = new SelectList(db.accountTBs.Where(p => p.EmpType == "Manager"), "EmpName", "EmpName");

            return View(departmenttb);
        }

        // GET: /Department/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepartmentTB departmenttb = db.DepartmentTBs.Find(id);
            if (departmenttb == null)
            {
                return HttpNotFound();
            }
            return View(departmenttb);
        }

        // POST: /Department/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DeptName,DeptManager")] DepartmentTB departmenttb)
        {
            if (ModelState.IsValid)
            {
                db.Entry(departmenttb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(departmenttb);
        }

        // GET: /Department/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepartmentTB departmenttb = db.DepartmentTBs.Find(id);
            if (departmenttb == null)
            {
                return HttpNotFound();
            }
            return View(departmenttb);
        }

        // POST: /Department/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DepartmentTB departmenttb = db.DepartmentTBs.Find(id);
            db.DepartmentTBs.Remove(departmenttb);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}
