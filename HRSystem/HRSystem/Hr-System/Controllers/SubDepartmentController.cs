using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hr_System.Models;
using System.Data;

namespace Hr_System.Controllers
{
    public class SubDepartmentController : Controller
    {
        private HRSystemEntities db = new HRSystemEntities();

        // GET: /SubDepartment/
        public ActionResult Index()
        {
            var subdeps = db.SubDeps.Include(s => s.DepartmentTB);
            return View(subdeps.ToList());
        }

        // GET: /SubDepartment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubDep subdep = db.SubDeps.Find(id);
            if (subdep == null)
            {
                return HttpNotFound();
            }
            return View(subdep);
        }

        // GET: /SubDepartment/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentID = new SelectList(db.DepartmentTBs, "ID", "DeptName");
            ViewBag.DepTeamLeader = new SelectList(db.accountTBs.Where(p => p.EmpType == "TeamLeader"), "EmpName", "EmpName");
            return View();
        }

        // POST: /SubDepartment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DepartmentID,SubDepartmentName,DepTeamLeader")] SubDep subdep)
        {
            var DepID = subdep.DepartmentID;
            var DepName = db.DepartmentTBs.Where(p => p.ID == DepID).Select(p => p.DeptName).SingleOrDefault();
            var departmentManager=db.DepartmentTBs.Where(p=>p.DeptName==DepName).Select(p=>p.DeptManager).SingleOrDefault();
            if (ModelState.IsValid)
            {
                subdep.DepManager = departmentManager;
                subdep.DepName = DepName;
                db.SubDeps.Add(subdep);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentID = new SelectList(db.DepartmentTBs, "ID", "DeptName", subdep.DepartmentID);
            ViewBag.DepTeamLeader = new SelectList(db.accountTBs.Where(p => p.EmpType == "TeamLeader"), "EmpName", "EmpName");
            return View(subdep);
        }

        // GET: /SubDepartment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubDep subdep = db.SubDeps.Find(id);
            if (subdep == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentID = new SelectList(db.DepartmentTBs, "ID", "DeptName", subdep.DepartmentID);
            return View(subdep);
        }

        // POST: /SubDepartment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DepartmentID,SubDepartmentName,DepTeamLeader")] SubDep subdep)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subdep).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentID = new SelectList(db.DepartmentTBs, "ID", "DeptName", subdep.DepartmentID);
            return View(subdep);
        }

        // GET: /SubDepartment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubDep subdep = db.SubDeps.Find(id);
            if (subdep == null)
            {
                return HttpNotFound();
            }
            return View(subdep);
        }

        // POST: /SubDepartment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubDep subdep = db.SubDeps.Find(id);
            db.SubDeps.Remove(subdep);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult GetTeamLeaderByDepartment(string depName)
        {
            var TeamLeaders = from p in db.accountTBs where p.DepartmentName == depName && p.EmpType == "TeamLeader" select p;
            return Json(TeamLeaders, JsonRequestBehavior.AllowGet);
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
