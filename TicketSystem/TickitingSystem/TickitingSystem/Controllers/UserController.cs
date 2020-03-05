
using DAL.CRUD;
using DAL.DataBase;
using Harkos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace TickitingSystem.Controllers
{

    public class UserController : Controller
    {

        public TicketingDBEntities Db = new TicketingDBEntities();


        User_DAL userDAL = new User_DAL();
        Deparments_DAL departmentDAL = new Deparments_DAL();
        SubDeparment_DAL sup = new SubDeparment_DAL();


        // GET: User
        [Authorize]
        public ActionResult Index()
        {
            var activeuser = userDAL.GetAllUsers();
            return View(activeuser);
        }
        [Authorize]
        public ActionResult AddUser()
        {
            var dep = departmentDAL.GetAllDepartments();
            ViewBag.Deps = dep;
            var supdep = sup.GetAllSubDepartments();
            ViewBag.Supdep = supdep;
            return View();
        }
        [Authorize]
        public JsonResult AddUserJson(DAL.DataBase.User user)
        {
            user.User_Status = "ACTIVE";
            userDAL.ADD(user);
            int id = user.User_ID;
            string message = "success";
            ViewBag.message = "success";
            ViewBag.uid = id;
            return Json(id, message, JsonRequestBehavior.AllowGet);
        }

        // GET: User/Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User User = Db.Users.Find(id);
            if (User == null)
            {
                return HttpNotFound();
            }
            return View(User);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            User User = Db.Users.Find(id);
            User.User_ID = id;
            User.User_Status = "DELETED";
            Db.SaveChanges();
            return RedirectToAction("Index");
        }


        // GET: User/Details/
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User User = Db.Users.Find(id);

            if (User == null)
            {
                return HttpNotFound();
            }

            return View(User);
        }


        // GET: User/Edit
        public ActionResult Edit(int? id)
        {

            string[] status = {"active" ,"not active"}; //(from d in Db.Departments select d).ToList();
            ViewBag.status = status;

           // var SubDeptid = (from a in Db.SubDeparments select a).ToList();
          //  ViewBag.SubDepartmentID = SubDeptid;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User User = Db.Users.Find(id);
            if (User == null)
            {
                return HttpNotFound();
            }
            return View(User);
        }

        // POST: User/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User User)
        {
         string status=   User.User_Status;
             User = Db.Users.Find(User.User_ID);
            User.User_Status = status;

            if (ModelState.IsValid)
            {
                Db.Entry(User).State = EntityState.Modified;
                Db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(User);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            var Deptid = (from d in Db.Departments select d).ToList();
            ViewBag.DepartmentID = Deptid;

            var SubDeptid = (from a in Db.SubDeparments select a).ToList();
            ViewBag.SubDepartmentID = SubDeptid;

            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(User User)
        {
            if (ModelState.IsValid)
            {
                Db.Users.Add(User);
                Db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(User);
        }

      public ActionResult Add_Users_Exel()
        { return View(); }


        [Authorize]
        [HttpPost]
        public ActionResult AddExelUser(List<User> listUser)
        {
           foreach(var item in listUser)
            {
                Db.Users.Add(item);
                Db.SaveChanges();

            }
            return View();
        }
        public ActionResult GET_ALL_USER_LDAP()
        {
           return View ( getall.GetADUsers());

        }
        public ActionResult ADD_ALL_USER_LDAP(List<UserLDAP> users)
        {
             
            foreach(var item in users)
            {


                if(userDAL.UserExist(item) == true)
                {
                    continue;


                }
                else
                {
                    if (departmentDAL.EepartmentExist(item.Department) == null)
                    {
                        Department newdepartment = new Department();
                        newdepartment.Dept_Name = item.Department;
                        departmentDAL.ADD(newdepartment);
                        User newuser = new User();
                        newuser.User_Name = item.Name;
                        newuser.E_mail = item.Email;
                        newuser.Dept_ID = newdepartment.Dept_Id;
                        newuser.User_Status = "active";
                        userDAL.ADD(newuser);


                    }
                    else
                    {
                        Department department = new Department();
                        department = departmentDAL.EepartmentExist(item.Department);

                        User user = new User();
                        user.User_Name = item.Name;
                        user.E_mail = item.Email;
                        user.Dept_ID = department.Dept_Id;
                        user.User_Status = "active";

                        userDAL.ADD(user);
                    }
                }


            }

            return View();

        }
    }
}