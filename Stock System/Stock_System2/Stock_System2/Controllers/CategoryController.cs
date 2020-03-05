using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stock_System2.Models;

namespace Stock_System2.Controllers
{
    public class CategoryController : Controller
    {
        StockDB DB = new StockDB();
        Category cs = new Category();
        Activity act = new Activity();
        public string message;
        public ActionResult AddCategory()
        {
           
            return View();
        }
        [HttpPost]
        public JsonResult AddCategory(string CategoryName,string DepartmentType)
        {
            cs.Category_name = CategoryName;
            cs.Department_name = DepartmentType;
            cs.Department_email = "IT-Support@prime-health.org";
            DB.Category.Add(cs);
            DB.SaveChanges();
            if(cs!=null)
            {
                message = "success";
                return Json(message,JsonRequestBehavior.AllowGet);
            }
            else
            {
                message = "faild";
                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult List_Of_Category()
        {

            return View(DB.Category.ToList());
        }
        public ActionResult Edit(int Category_Id)
        {
            var Category_Edit = (from p in DB.Category
                              where p.Category_id == Category_Id
                              select p).Single();
            Session["Categoryid"] = Category_Id;
            return View(Category_Edit);
        }
        [HttpPost]
        public JsonResult Edit(string CategoryName, string DepartmentName, string Email)
        {
            string message;
            int category_id = Convert.ToInt32(Session["Categoryid"]);
            Category cs = DB.Category.Where(p => p.Category_id == category_id).Single();
            cs.Category_name = CategoryName;
            cs.Department_name = DepartmentName;
            cs.Department_email = Email;
            DB.SaveChanges();
            if (cs != null)
            {
                message = "EditSuccessfully";
            }
            else
            {
                message = "faild";
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Delete(int Category_Id)
        {
            Session["categoryid2"] = Category_Id;
            return View();
        }
        [HttpPost]
        public JsonResult Delete()
        {
            string message;
            int categoryid = Convert.ToInt32(Session["categoryid2"]);
            Category cs = DB.Category.Find(categoryid);
            DB.Category.Remove(cs);
            DB.SaveChanges();
            if (cs != null)
            {

                message = "success";
            }
            else
            {
                message = "faild";
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }



    }
}
