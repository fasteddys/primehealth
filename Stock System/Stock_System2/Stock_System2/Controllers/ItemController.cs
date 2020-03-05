using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stock_System2.Models;
using PagedList.Mvc;
using PagedList;


namespace Stock_System2.Controllers
{

    public class ItemController : Controller
    {
        StockDB DB = new StockDB();
        Mailing mail = new Mailing();
        public string message;
        public ActionResult Add_item()
        {

            if (Request.Cookies["usertype"].Value == "Engineer")
            {
                ViewBag.Str2 = "Withdraw Item For I.T Department";

            }
            else if (Request.Cookies["usertype"].Value == "TPA")
            {
                ViewBag.Str2 = "Withdraw Item For TPA Department";
            }
            else if (Request.Cookies["usertype"].Value == "Issuance")
            {
                ViewBag.Str2 = "Withdraw Item For Issuance Department";
            }
            else if (Request.Cookies["usertype"].Value == "Archiving")
            {
                ViewBag.Str2 = "Withdraw Item For Archiving Department";
            }
            else
            {
                ViewBag.Str2 = "you can Withdraw item for different depatrments";
            }

            return View();
        }

        [HttpPost]
        public JsonResult Add_item(string ItemName, int ItemCount, int ItemWarning, string type)
        {

            Item IT = new Item();
            Activity act = new Activity();
            User us=new User();
            int usid = Convert.ToInt32(Request.Cookies["userid"].Value.ToString());
            string UserName = (from p in DB.User
                                  where p.user_id==usid
                                  select p.user_name).Single();
           string Department = (from p in DB.User
                                  where p.user_id==usid
                                  select p.department).Single();
            IT.item_name = ItemName;
            IT.item_count = ItemCount;
            IT.item_warning_count = ItemWarning;
            IT.Date_Insert = DateTime.Now;
            if (Request.Cookies["usertype"].Value == "Engineer")
            {
                int item_id_it = (from p in DB.Category
                                  where p.Department_name == "IT"
                                  select p.Category_id).Single();
                IT.category_id = item_id_it;
            }
            else if (Request.Cookies["usertype"].Value == "TPA")
            {
                int item_id_it = (from p in DB.Category
                                  where p.Department_name == "TPA"
                                  select p.Category_id).Single();
                IT.category_id = item_id_it;
            }
            else if (Request.Cookies["usertype"].Value == "Archiving")
            {
                int item_id_it = (from p in DB.Category
                                  where p.Department_name == "Archiving"
                                  select p.Category_id).Single();
                IT.category_id = item_id_it;
            }
            else if (Request.Cookies["usertype"].Value == "Issuance")
            {
                int item_id_it = (from p in DB.Category
                                  where p.Department_name == "Issuance"
                                  select p.Category_id).Single();
                IT.category_id = item_id_it;
            }
            else
            {
                if (type == "IT")
                {
                    int item_id_it = (from p in DB.Category
                                      where p.Department_name == "IT"
                                      select p.Category_id).Single();
                    IT.category_id = item_id_it;
                }
                else if (type == "TPA")
                {
                    int item_id_it = (from p in DB.Category
                                      where p.Department_name == "TPA"
                                      select p.Category_id).Single();
                    IT.category_id = item_id_it;
                }
                else if (type == "Archiving")
                {
                    int item_id_it = (from p in DB.Category
                                      where p.Department_name == "Archiving"
                                      select p.Category_id).Single();
                    IT.category_id = item_id_it;
                }
                else
                {
                    int item_id_it = (from p in DB.Category
                                      where p.Department_name == "Issuance"
                                      select p.Category_id).Single();
                    IT.category_id = item_id_it;
                }
            }
            DB.Item.Add(IT);
            DB.SaveChanges();
            if (IT != null)
            {

                message = "success";
                act.item_request_id = IT.item_id;
                act.Request_Type = "Add";
                act.UserName = UserName;
                act.ItemName = IT.item_name;
                act.Number_of_item = ItemCount;
                act.Date_Time_Request = DateTime.Now;
                act.user_id = Convert.ToInt16(Request.Cookies["userid"].Value);
                ViewBag.itemnum = IT.item_id;
                DB.Activity.Add(act);
                DB.SaveChanges();
               mail.SendMail_NewRequestCreated(UserName, Department, IT.item_name, ItemCount);
            }
            else
            {
                message = "faild";
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Withdraw_Item(int Item_Id)
        {
            if (Request.Cookies["usertype"].Value == "Engineer")
            {
                ViewBag.Str = "Withdraw Item For I.T Department";

            }
            else if (Request.Cookies["usertype"].Value == "TPA")
            {
                ViewBag.Str = "Withdraw Item For TPA Department";
            }
            else if (Request.Cookies["usertype"].Value == "Issuance")
            {
                ViewBag.Str = "Withdraw Item For Issuance Department";
            }
            else if (Request.Cookies["usertype"].Value == "Archiving")
            {
                ViewBag.Str = "Withdraw Item For Archiving Department";
            }
            else
            {
                ViewBag.Str = "you can Withdraw item for different depatrments";
            }
            var withdraw_item = (from p in DB.Item
                                 where p.item_id == Item_Id
                                 select p).Single();
            Session["item_id"] = withdraw_item.item_id;
            return View(withdraw_item);
        }
        [HttpPost]
        public JsonResult Withdraw_Item2(int WithdrawCount)
        {
            Item it = new Item();
            Activity act = new Activity();
            User us = new User();
            int usid = Convert.ToInt32(Request.Cookies["userid"].Value.ToString());
            string UserName = (from p in DB.User
                               where p.user_id == usid
                               select p.user_name).Single();
            string Department = (from p in DB.User
                               where p.user_id == usid
                               select p.department).Single();
            int item_id = Convert.ToInt32(Session["item_id"]);
            var withdraw_item = (from p in DB.Item
                                 where p.item_id == item_id
                                 select p).Single();
            it.item_count = withdraw_item.item_count - WithdrawCount;
            if (it.item_count < 0)
            {
                message = "minus is faild";
                //add notifiction
            }
            else if(WithdrawCount>withdraw_item.item_count)
            {
                message = "Faild Count";
            }
            else
            {
                withdraw_item.item_count = it.item_count;
                DB.SaveChanges();
                if (it != null)
                {
                    act.item_request_id = withdraw_item.item_id;
                    act.Number_of_item = WithdrawCount;
                    act.Request_Type = "Withdraw";
                    act.user_id = Convert.ToInt32(Request.Cookies["userid"].Value);
                    act.Date_Time_Request = DateTime.Now;
                    act.ItemName = withdraw_item.item_name;
                    act.UserName = UserName;
                    DB.Activity.Add(act);
                    DB.SaveChanges();
                    message = "success";
                    mail.SendMail_WithdrawRequestCreated(UserName, Department, withdraw_item.item_name, WithdrawCount);
                    if(withdraw_item.item_warning_count>=withdraw_item.item_count)
                   {
                      mail.SendMail_DangerRequestCreated(Department, withdraw_item.item_name, withdraw_item.item_count);
                    }
                }

            }
            return Json(message, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Search(string search,string type, int Pagesize = 6, int page = 1)
        {
            ViewBag.search = search;
            Session["type"] = type;
            int search_number;
            bool check = int.TryParse(search, out search_number);
            if (search == null)
            {
                if (Request.Cookies["usertype"].Value == "Engineer")
                {
                    var item_data_it = (from I in DB.Item
                                        join C in DB.Category on I.category_id equals C.Category_id
                                        where C.Category_name == "IT"
                                        orderby I.item_id descending
                                        select I).ToList();
                    PagedList<Item> Paging = new PagedList<Item>(item_data_it, page, Pagesize);
                    return View(Paging);
                }
                else if (Request.Cookies["usertype"].Value == "TPA")
                {
                    var item_data_tpa = (from I in DB.Item
                                         join C in DB.Category on I.category_id equals C.Category_id
                                         where C.Category_name == "TPA"
                                         orderby I.item_id descending
                                         select I).ToList();
                    PagedList<Item> Paging = new PagedList<Item>(item_data_tpa, page, Pagesize);
                    return View(Paging);
                }
                else if (Request.Cookies["usertype"].Value == "Issuance")
                {
                    var item_data_iss = (from I in DB.Item
                                         join C in DB.Category on I.category_id equals C.Category_id
                                         where C.Category_name == "Issuance"
                                         orderby I.item_id descending
                                         select I).ToList();
                    PagedList<Item> Paging = new PagedList<Item>(item_data_iss, page, Pagesize);
                    return View(Paging);
                }
                else if (Request.Cookies["usertype"].Value == "Archiving")
                {
                    var item_data_arch = (from I in DB.Item
                                         join C in DB.Category on I.category_id equals C.Category_id
                                         where C.Category_name == "Archiving"
                                          orderby I.item_id descending
                                         select I).ToList();
                    PagedList<Item> Paging = new PagedList<Item>(item_data_arch, page, Pagesize);
                    return View(Paging);
                }
                else
                {
                    if (type == null)
                    {
                        var item_data = (from I in DB.Item
                                         join C in DB.Category on I.category_id equals C.Category_id
                                         where C.Category_name == "IT"
                                         orderby I.item_id descending
                                         select I).ToList();
                        PagedList<Item> Paging = new PagedList<Item>(item_data, page, Pagesize);
                        return View(Paging);
                    }
                    else
                    {
                        var item_data = (from I in DB.Item
                                         join C in DB.Category on I.category_id equals C.Category_id
                                         where C.Category_name==type
                                         orderby I.item_id descending
                                         select I).ToList();
                        PagedList<Item> Paging = new PagedList<Item>(item_data, page, Pagesize);
                        return View(Paging);
                    }
                   
                }
            }
            else if(check==true)
            {
                if (Request.Cookies["usertype"].Value == "Engineer")
                {
                    var item_data_it = (from I in DB.Item
                                        join C in DB.Category on I.category_id equals C.Category_id
                                        where C.Category_name == "IT" &&(I.category_id==search_number||I.item_count==search_number||I.item_id==search_number||I.item_warning_count==search_number)
                                        orderby I.item_id descending
                                        select I).ToList();
                    PagedList<Item> Paging = new PagedList<Item>(item_data_it, page, Pagesize);
                    return View(Paging);
                }
                else if (Request.Cookies["usertype"].Value == "TPA")
                {
                    var item_data_tpa = (from I in DB.Item
                                         join C in DB.Category on I.category_id equals C.Category_id
                                         where C.Category_name == "TPA" && (I.category_id == search_number || I.item_count == search_number || I.item_id == search_number || I.item_warning_count == search_number)
                                         orderby I.item_id descending
                                         select I).ToList();
                    PagedList<Item> Paging = new PagedList<Item>(item_data_tpa, page, Pagesize);
                    return View(Paging);
                }
                else if (Request.Cookies["usertype"].Value == "Issuance")
                {
                    var item_data_iss = (from I in DB.Item
                                         join C in DB.Category on I.category_id equals C.Category_id
                                         where C.Category_name == "Issuance" && (I.category_id == search_number || I.item_count == search_number || I.item_id == search_number || I.item_warning_count == search_number)
                                         orderby I.item_id descending
                                         select I).ToList();
                    PagedList<Item> Paging = new PagedList<Item>(item_data_iss, page, Pagesize);
                    return View(Paging);
                }
                else if (Request.Cookies["usertype"].Value == "Archiving")
                {
                    var item_data_arch = (from I in DB.Item
                                         join C in DB.Category on I.category_id equals C.Category_id
                                         where C.Category_name == "Archiving" && (I.category_id == search_number || I.item_count == search_number || I.item_id == search_number || I.item_warning_count == search_number)
                                          orderby I.item_id descending
                                          select I).ToList();
                    PagedList<Item> Paging = new PagedList<Item>(item_data_arch, page, Pagesize);
                    return View(Paging);
                }
                else
                {
                    if (type == null)
                    {
                        var item_data = (from I in DB.Item
                                         join C in DB.Category on I.category_id equals C.Category_id
                                         where (I.category_id == search_number || I.item_count == search_number || I.item_id == search_number || I.item_warning_count == search_number) && C.Category_name == "IT"
                                         orderby I.item_id descending
                                         select I).ToList();
                        PagedList<Item> Paging = new PagedList<Item>(item_data, page, Pagesize);
                        return View(Paging);
                    }
                    else
                    {
                        var item_data = (from I in DB.Item
                                         join C in DB.Category on I.category_id equals C.Category_id
                                         where (I.category_id == search_number || I.item_count == search_number || I.item_id == search_number || I.item_warning_count == search_number) && C.Category_name == type
                                         orderby I.item_id descending
                                         select I).ToList();
                        PagedList<Item> Paging = new PagedList<Item>(item_data, page, Pagesize);
                        return View(Paging);
                    }
                }
            }
            else
            {
                if (Request.Cookies["usertype"].Value == "Engineer")
                {
                    var item_data_it = (from I in DB.Item
                                        join C in DB.Category on I.category_id equals C.Category_id
                                        where C.Category_name == "IT" && (I.item_name.Contains(search))
                                        orderby I.item_id descending
                                        select I).ToList();
                    PagedList<Item> Paging = new PagedList<Item>(item_data_it, page, Pagesize);
                    return View(Paging);
                }
                else if (Request.Cookies["usertype"].Value == "TPA")
                {
                    var item_data_tpa = (from I in DB.Item
                                         join C in DB.Category on I.category_id equals C.Category_id
                                         where C.Category_name == "TPA"
                                         orderby I.item_id descending
                                         select I).ToList();
                    PagedList<Item> Paging = new PagedList<Item>(item_data_tpa, page, Pagesize);
                    return View(Paging);
                }
                else if (Request.Cookies["usertype"].Value == "Issuance")
                {
                    var item_data_iss = (from I in DB.Item
                                         join C in DB.Category on I.category_id equals C.Category_id
                                         where C.Category_name == "Issuance" && (I.item_name.Contains(search))
                                         orderby I.item_id descending
                                         select I).ToList();
                    PagedList<Item> Paging = new PagedList<Item>(item_data_iss, page, Pagesize);
                    return View(Paging);
                }
                else if (Request.Cookies["usertype"].Value == "Archiving")
                {
                    var item_data_arch = (from I in DB.Item
                                         join C in DB.Category on I.category_id equals C.Category_id
                                         where C.Category_name == "Archiving" && (I.item_name.Contains(search))
                                          orderby I.item_id descending
                                          select I).ToList();
                    PagedList<Item> Paging = new PagedList<Item>(item_data_arch, page, Pagesize);
                    return View(Paging);
                }
                else
                {
                    if (type == null)
                    {
                        var item_data = (from I in DB.Item
                                         join C in DB.Category on I.category_id equals C.Category_id
                                         where (I.item_name.Contains(search)) && C.Category_name == "IT"
                                         orderby I.item_id descending
                                         select I).ToList();
                        PagedList<Item> Paging = new PagedList<Item>(item_data, page, Pagesize);
                        return View(Paging);
                    }
                    else
                    {
                        var item_data = (from I in DB.Item
                                         join C in DB.Category on I.category_id equals C.Category_id
                                         where (I.item_name.Contains(search)) && C.Category_name == type
                                         orderby I.item_id descending
                                         select I).ToList();

                        PagedList<Item> Paging = new PagedList<Item>(item_data, page, Pagesize);
                        return View(Paging);
                    }
                   
                }
            }
            

        }
        public ActionResult Edit(int Item_Id)
        {
            var Item_Edit = (from p in DB.Item
                             where p.item_id == Item_Id
                             select p).Single();
            Session["itemid2"] = Item_Id;
            return View(Item_Edit);
        }
        [HttpPost]
        public JsonResult Edit(string ItemName, int ItemCount, int WarningCount,string EditComment)
        {
            string message;
            Activity act = new Activity();
            User us = new User();
            int usid = Convert.ToInt32(Request.Cookies["userid"].Value.ToString());
            string UserName = (from p in DB.User
                               where p.user_id == usid
                               select p.user_name).Single();
            int item_id = Convert.ToInt32(Session["itemid2"]);
            Item it = DB.Item.Where(p => p.item_id == item_id).Single();
            it.item_name = ItemName;
            it.item_count = ItemCount;
            it.item_warning_count = WarningCount;
            DB.SaveChanges();
            if (it != null)
            {
                act.item_request_id = item_id;
                act.Number_of_item = ItemCount;
                act.Request_Type = "Edit";
                act.user_id = Convert.ToInt32(Request.Cookies["userid"].Value);
                act.Date_Time_Request = DateTime.Now;
                act.UserName = UserName;
                act.ItemName = it.item_name;
                act.Edit_Comment = EditComment;
                DB.Activity.Add(act);
                DB.SaveChanges();
                message = "EditSuccessfully";

            }
            else
            {
                message = "faild";
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }
      
        public ActionResult Details(int Item_Id)
        {
           try
            {
                var Item_Details_Edit = (from p in DB.item_details
                                         where p.item_id == Item_Id
                                         select p).Single();
                Session["itemid3"] = Item_Id;
                return View(Item_Details_Edit);
            }
           catch(System.InvalidOperationException)
            {
                Session["itemid3"] = Item_Id;
                return View();
            }
           
        }
        [HttpPost]
        public JsonResult Details(string Ram,string Model,string Man,string Status,string Processor)
        {
            string message;
            int item_id = Convert.ToInt32(Session["itemid3"]);
            item_details its = new item_details();
            try { 
             its = DB.item_details.Where(p => p.item_id == item_id).Single(); 
           
                its.Ram = Ram;
                its.Manufacture = Man;
                its.Processor = Processor;
                its.Model = Model;
                its.status = Status;
                DB.SaveChanges();
                message = "success";
            
            }
            catch(System.InvalidOperationException)
            {
                its.Ram = Ram;
                its.Manufacture = Man;
                its.Processor = Processor;
                its.Model = Model;
                its.status = Status;
                its.item_id = item_id;
                DB.item_details.Add(its);
                DB.SaveChanges();
                message = "success2";
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Detail_It()
        {
            int max = (from P in DB.Item
                       select P.item_id).Max();
                Session["itemid4"] = max;
                return View();
          
        }
        [HttpPost]
        public JsonResult Detail_It(string Ram, string Model, string Man, string Status, string Processor)
        {
            string message;
            int item_id = Convert.ToInt32(Session["itemid4"]);
            item_details its = new item_details();
                its.Ram = Ram;
                its.Manufacture = Man;
                its.Processor = Processor;
                its.Model = Model;
                its.status = Status;
                its.item_id = item_id;
                DB.item_details.Add(its);
                DB.SaveChanges();
                message = "success2";
           
            return Json(message, JsonRequestBehavior.AllowGet);
        }
    }
}
