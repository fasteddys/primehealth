using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;
using TasksMonitoringSystem.DTOs;
using TasksMonitoringSystem.SharedClass;

namespace TasksMonitoringSystem.Controllers
{
    [MyAuthorizeAttribute]
    public class UsersController : Controller
    {
        private TaskMSEntities db = new TaskMSEntities();
        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,UserName,Password,IsAdminLevel,IsActive,IsDeleted,ModificationDate,CreationDate,DeletionDate")] User user)
        {
            if (ModelState.IsValid)
            {
                user.CreationDate = DateTime.Now;
                user.IsActive =true;
                user.IsDeleted = false;
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,UserName,Password,IsAdminLevel,IsActive,IsDeleted,ModificationDate,CreationDate,DeletionDate")] User user)
        {
            if (ModelState.IsValid)
            {
                user.ModificationDate = DateTime.Now;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);

            if (user == null)
            {
                return HttpNotFound();
            }
            else
            {
                user.IsDeleted = true;
                user.IsActive = false;
                user.DeletionDate = DateTime.Now;
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
        //-----------------------------------------------------
        //my tasks
        public ActionResult MyTasks(int userId, int CID = 0)
        {
            //if(CID == 0)
            Response.Cookies["CID"].Value = new JavaScriptSerializer().Serialize(CID);
            ViewBag.CompanyFK = new SelectList(shared.GetAllCompanies(), "CompanyID", "CompanyName", CID);
             if (CID == 0)
            {
                return View(db.UserDailsTasks.Where(udt => udt.AssignedByFK == userId).OrderBy(udt => udt.StatusFK).ThenByDescending(udt => udt.Task.PriorityFK).ToList());

            }
            else
            {
                return View(db.UserDailsTasks.Where(udt => udt.AssignedByFK == userId&&udt.Task.CompanyFK==CID).OrderBy(udt => udt.StatusFK).ThenByDescending(udt => udt.Task.PriorityFK).ToList());

            }
            //else
            //    return View(db.UserDailsTasks.Where(udt => udt.AssignedByFK == UserId && udt.Task.CompanyFK == CID).OrderBy(udt => udt.StatusFK).ToList());
        }
        //------------------------------------------------------------
        [AllowAnonymous]
        public ActionResult logIn()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public JsonResult UserLogIn(UserLoginInputDTO UserLogin)
        {
            bool IsLogin = false;

            User user = db.Users.Where(u => u.UserName == UserLogin.UserName && u.Password == UserLogin.Password).FirstOrDefault();
        
            if (user != null)
            {
                UserDTO User = new UserDTO
                {
                    UserID = user.UserID,
                    UserName = user.UserName,
                    IsAdminLevel = user.IsAdminLevel,

                };
                IsLogin = true;
                Response.Cookies["UserData"].Value = new JavaScriptSerializer().Serialize(User);
            }

            return Json(IsLogin);
            
        }
        public ActionResult LogOut()
        {
            if (Request.Cookies["UserData"] != null)
            {
                var c = new HttpCookie("UserData");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
            }
            return RedirectToAction("logIn");
        }
        [AllowAnonymous]
        public ActionResult AnAuthorized()
        {
            return View();
        }
        //-------------------------------------------------
        [HttpGet]
        public JsonResult NumberOfMyTasks()
        {
            var todayDataTime = DateTime.Now;
            var todayData = todayDataTime.Date;
            var userId= Newtonsoft.Json.JsonConvert.DeserializeObject<UserDTO>(Request.Cookies["UserData"].Value).UserID;
            //var userDailsTasks = db.UserDailsTasks.Where(udt => udt.DateOfTask == todayData && udt.StatusFK == (int)StaticData.StaticEnums.TaskStatus.Assign &&udt.AssignedByFK== userId).ToList();
            var userDailsTasks = db.UserDailsTasks.Where(udt => udt.StatusFK == (int)StaticData.StaticEnums.TaskStatus.Assign && udt.AssignedByFK == userId).ToList();
            int count = userDailsTasks.Count;
            return Json(count, JsonRequestBehavior.AllowGet);
        }
        //-----------------------------------------------------------------------
    }
}
public class MyAuthorizeAttribute : AuthorizeAttribute
{
    protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
    {
        if (filterContext.HttpContext.Request.Cookies["UserData"]!=null)
        {
            //var routeValues = new RouteValueDictionary(new
            //{
            //    controller = "Users",
            //    action = "logIn",
            //});
            //filterContext.Result = new RedirectToRouteResult(routeValues);
            //base.HandleUnauthorizedRequest(filterContext);

        }
        else
        {
            var routeValues = new RouteValueDictionary(new
            {
                controller = "Users",
                action = "AnAuthorized",
            });
            filterContext.Result = new RedirectToRouteResult(routeValues);
        }
    }

    private bool IsProfileCompleted(string user)
    {
        // You know what to do here => go hit your database to verify if the
        // current user has already completed his profile by checking
        // the corresponding field
        throw new NotImplementedException();
    }
}