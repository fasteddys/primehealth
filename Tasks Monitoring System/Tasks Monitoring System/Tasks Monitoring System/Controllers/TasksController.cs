using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TasksMonitoringSystem.SharedClass;

namespace TasksMonitoringSystem.Controllers
{
    [MyAuthorizeAttribute]
    public class TasksController : Controller
    {
        private TaskMSEntities db = new TaskMSEntities();
        // GET: Tasks
        public ActionResult Index(int CID = 0)
        {
            ViewBag.CompanyFK = new SelectList(shared.GetAllCompanies(), "CompanyID", "CompanyName", CID);
            if (CID == 0)
                return View(db.Tasks.Include(t => t.CompanyDIM).Include(t => t.PriorityTypeDIM).Include(t => t.User).ToList());
            else
                return View(db.Tasks.Where(t => t.CompanyFK == CID).Include(t => t.CompanyDIM).Include(t => t.PriorityTypeDIM).Include(t => t.User).ToList());

        }
        // GET: Tasks/Create
        public ActionResult Create()
        {
            ViewBag.CompanyFK = new SelectList(db.CompanyDIMs, "CompanyID", "CompanyName");
            ViewBag.PriorityFK = new SelectList(db.PriorityTypeDIMs, "PriorityTypeID", "PriorityTypeName");
            ViewBag.AddedByFK = new SelectList(db.Users, "UserID", "UserName");
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TaskID,TaskName,Description,CompanyFK,PriorityFK,AddedByFK,IsActive,IsDeleted,DeletionDate,CreationDate,ModificationDate")] Task task)
        {


            if (ModelState.IsValid)
            {
                task.CreationDate = DateTime.Now;
                task.IsActive = true;
                task.IsDeleted = false;
                task.AddedByFK = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDTO>(Request.Cookies["UserData"].Value).UserID;
                db.Tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CompanyFK = new SelectList(db.CompanyDIMs, "CompanyID", "CompanyName", task.CompanyFK);
            ViewBag.PriorityFK = new SelectList(db.PriorityTypeDIMs, "PriorityTypeID", "PriorityTypeName", task.PriorityFK);
            ViewBag.AddedByFK = new SelectList(db.Users, "UserID", "UserName", task.AddedByFK);
            return View(task);
        }

        // GET: Tasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyFK = new SelectList(db.CompanyDIMs, "CompanyID", "CompanyName", task.CompanyFK);
            ViewBag.PriorityFK = new SelectList(db.PriorityTypeDIMs, "PriorityTypeID", "PriorityTypeName", task.PriorityFK);
            ViewBag.AddedByFK = new SelectList(db.Users, "UserID", "UserName", task.AddedByFK);
            return View(task);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TaskID,TaskName,Description,CompanyFK,PriorityFK,IsActive,IsDeleted,DeletionDate,CreationDate,ModificationDate")] Task task)
        {
            if (ModelState.IsValid)
            {
                task.ModificationDate = DateTime.Now;
                task.AddedByFK = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDTO>(Request.Cookies["UserData"].Value).UserID;
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyFK = new SelectList(db.CompanyDIMs, "CompanyID", "CompanyName", task.CompanyFK);
            ViewBag.PriorityFK = new SelectList(db.PriorityTypeDIMs, "PriorityTypeID", "PriorityTypeName", task.PriorityFK);
            ViewBag.AddedByFK = new SelectList(db.Users, "UserID", "UserName", task.AddedByFK);
            return View(task);
        }

        // GET: Tasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            else
            {
                task.IsDeleted = true;
                task.IsActive = false;
                task.DeletionDate = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        //------------------------------------------------------

    }
}
