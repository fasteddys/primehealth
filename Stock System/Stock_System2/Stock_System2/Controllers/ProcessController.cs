using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stock_System2.Models;
using PagedList.Mvc;
using PagedList;
using System.Globalization;

namespace Stock_System2.Controllers
{
    public class ProcessController : Controller
    {
        StockDB DB = new StockDB();
        Activity act = new Activity();
        public ActionResult Dashboard()
        {
            int process_it_count_add = (from p in DB.Activity
                                    join U in DB.User on p.user_id equals U.user_id
                                    join I in DB.Item on p.item_request_id equals I.item_id
                                    join C in DB.Category on I.category_id equals C.Category_id
                                    where p.Request_Type == "Add" && (U.user_type == "Engineer" || U.user_type == "Manager") && C.Category_name == "IT"
                                    select p).Count();
            ViewBag.ITADD = process_it_count_add;
            int process_it_count_edit = (from p in DB.Activity
                                        join U in DB.User on p.user_id equals U.user_id
                                        join I in DB.Item on p.item_request_id equals I.item_id
                                        join C in DB.Category on I.category_id equals C.Category_id
                                        where p.Request_Type == "Edit" && (U.user_type == "Engineer" || U.user_type == "Manager") && C.Category_name == "IT"
                                        select p).Count();
            ViewBag.ITEDIT = process_it_count_edit;
            int process_it_count_with = (from p in DB.Activity
                                         join U in DB.User on p.user_id equals U.user_id
                                         join I in DB.Item on p.item_request_id equals I.item_id
                                         join C in DB.Category on I.category_id equals C.Category_id
                                         where p.Request_Type == "Withdraw" && (U.user_type == "Engineer" || U.user_type == "Manager") && C.Category_name == "IT"
                                         select p).Count();
            ViewBag.ITwith = process_it_count_with;
            int process_issuance_count_add = (from p in DB.Activity
                                        join U in DB.User on p.user_id equals U.user_id
                                        join I in DB.Item on p.item_request_id equals I.item_id
                                        join C in DB.Category on I.category_id equals C.Category_id
                                        where p.Request_Type == "Add" && (U.user_type == "Issuance" || U.user_type == "Manager") && C.Category_name == "Issuance"
                                        select p).Count();
            ViewBag.ISSADD = process_issuance_count_add;
            int process_issuance_count_edit = (from p in DB.Activity
                                         join U in DB.User on p.user_id equals U.user_id
                                         join I in DB.Item on p.item_request_id equals I.item_id
                                         join C in DB.Category on I.category_id equals C.Category_id
                                         where p.Request_Type == "Edit" && (U.user_type == "Issuance" || U.user_type == "Manager") && C.Category_name == "Issuance"
                                         select p).Count();
            ViewBag.ISSEDIT = process_issuance_count_edit;
            int process_issuance_count_with = (from p in DB.Activity
                                         join U in DB.User on p.user_id equals U.user_id
                                         join I in DB.Item on p.item_request_id equals I.item_id
                                         join C in DB.Category on I.category_id equals C.Category_id
                                         where p.Request_Type == "Withdraw" && (U.user_type == "Issuance" || U.user_type == "Manager") && C.Category_name == "Issuance"
                                         select p).Count();
            ViewBag.ISSwith = process_issuance_count_with;
            int process_tpa_count_add = (from p in DB.Activity
                                              join U in DB.User on p.user_id equals U.user_id
                                              join I in DB.Item on p.item_request_id equals I.item_id
                                              join C in DB.Category on I.category_id equals C.Category_id
                                              where p.Request_Type == "Add" && (U.user_type == "TPA" || U.user_type == "Manager") && C.Category_name == "TPA"
                                              select p).Count();
            ViewBag.TPAADD = process_tpa_count_add;
            int process_tpa_count_edit = (from p in DB.Activity
                                               join U in DB.User on p.user_id equals U.user_id
                                               join I in DB.Item on p.item_request_id equals I.item_id
                                               join C in DB.Category on I.category_id equals C.Category_id
                                               where p.Request_Type == "Edit" && (U.user_type == "TPA" || U.user_type == "Manager") && C.Category_name == "TPA"
                                               select p).Count();
            ViewBag.TPAEDIT = process_tpa_count_edit;
            int process_tpa_count_with = (from p in DB.Activity
                                               join U in DB.User on p.user_id equals U.user_id
                                               join I in DB.Item on p.item_request_id equals I.item_id
                                               join C in DB.Category on I.category_id equals C.Category_id
                                               where p.Request_Type == "Withdraw" && (U.user_type == "TPA" || U.user_type == "Manager") && C.Category_name == "TPA"
                                               select p).Count();
            ViewBag.TPAwith = process_tpa_count_with;
            int process_arch_count_add = (from p in DB.Activity
                                         join U in DB.User on p.user_id equals U.user_id
                                         join I in DB.Item on p.item_request_id equals I.item_id
                                         join C in DB.Category on I.category_id equals C.Category_id
                                         where p.Request_Type == "Add" && (U.user_type == "Archiving" || U.user_type == "Manager") && C.Category_name == "Archiving"
                                         select p).Count();
            ViewBag.ARCHADD = process_arch_count_add;
            int process_arch_count_edit = (from p in DB.Activity
                                          join U in DB.User on p.user_id equals U.user_id
                                          join I in DB.Item on p.item_request_id equals I.item_id
                                          join C in DB.Category on I.category_id equals C.Category_id
                                          where p.Request_Type == "Edit" && (U.user_type == "Archiving" || U.user_type == "Manager") && C.Category_name == "Archiving"
                                          select p).Count();
            ViewBag.ARCHEDIT = process_arch_count_edit;
            int process_arch_count_with = (from p in DB.Activity
                                          join U in DB.User on p.user_id equals U.user_id
                                          join I in DB.Item on p.item_request_id equals I.item_id
                                          join C in DB.Category on I.category_id equals C.Category_id
                                          where p.Request_Type == "Withdraw" && (U.user_type == "Archiving" || U.user_type == "Manager") && C.Category_name == "Archiving"
                                          select p).Count();
            ViewBag.ARCHwith = process_arch_count_with;
            return View();
        }
        public ActionResult ShowIT(string statue, int Pagesize = 8, int page = 1)
        {
            ViewBag.search = statue;
            var process_it = (from p in DB.Activity
                              join U in DB.User on p.user_id equals U.user_id
                              join I in DB.Item on p.item_request_id equals I.item_id
                              join C in DB.Category on I.category_id equals C.Category_id
                              orderby p.Date_Time_Request descending
                              where p.Request_Type == statue  && (U.user_type=="Engineer" || U.user_type=="Manager") && C.Category_name=="IT"
                              select p).ToList();
            
            if(statue=="Add")
            {
                PagedList<Activity> Paging = new PagedList<Activity>(process_it, page, Pagesize);
                return View(Paging);
               
            }
            else if (statue == "Edit")
            {
                PagedList<Activity> Paging = new PagedList<Activity>(process_it, page, Pagesize);
                return View(Paging);
            }
            else
            {
                PagedList<Activity> Paging = new PagedList<Activity>(process_it, page, Pagesize);
                return View(Paging);
            }
        }
        public ActionResult ShowIssuance(string statue, int Pagesize = 8, int page = 1)
        {
            ViewBag.search = statue;
            var process_issuance = (from p in DB.Activity
                              join U in DB.User on p.user_id equals U.user_id
                              join I in DB.Item on p.item_request_id equals I.item_id
                              join C in DB.Category on I.category_id equals C.Category_id
                              where p.Request_Type == statue && (U.user_type == "Issuance" || U.user_type == "Manager") && C.Category_name == "Issuance"
                              orderby p.Date_Time_Request descending
                              select p).ToList();

            PagedList<Activity> Paging = new PagedList<Activity>(process_issuance, page, Pagesize);
       
            if (statue == "Add")
            {

                return View(Paging);
            }
            else if (statue == "Edit")
            {
                return View(Paging);
            }
            else
            {
                return View(Paging);
            }
        }
        public ActionResult ShowTPA(string statue, int Pagesize = 8, int page = 1)
        {
            ViewBag.search = statue;
            var process_tpa = (from p in DB.Activity
                                    join U in DB.User on p.user_id equals U.user_id
                                    join I in DB.Item on p.item_request_id equals I.item_id
                                    join C in DB.Category on I.category_id equals C.Category_id
                                    where p.Request_Type == statue && (U.user_type == "TPA" || U.user_type == "Manager") && C.Category_name == "TPA"
                                    orderby p.Date_Time_Request descending
                                    select p).ToList();
            PagedList<Activity> Paging = new PagedList<Activity>(process_tpa, page, Pagesize);
            if (statue == "Add")
            {
                return View(Paging);
            }
            else if (statue == "Edit")
            {
                return View(Paging);
            }
            else
            {
                return View(Paging);
            }
        }
        public ActionResult ShowArchiving(string statue , int Pagesize = 8, int page = 1)
        {

            var process_archiving = (from p in DB.Activity
                                    join U in DB.User on p.user_id equals U.user_id
                                    join I in DB.Item on p.item_request_id equals I.item_id
                                    join C in DB.Category on I.category_id equals C.Category_id
                                    where p.Request_Type == statue && (U.user_type == "Archiving" || U.user_type == "Manager") && C.Category_name == "Archiving"
                                     orderby p.Date_Time_Request descending
                                     select p).ToList();

            PagedList<Activity> Paging = new PagedList<Activity>(process_archiving, page, Pagesize);
            if (statue == "Add")
            {
                return View(Paging);
            }
            else if (statue == "Edit")
            {
                return View(Paging);
            }
            else
            {
                return View(Paging);
            }
        }
        public ActionResult Report()
        {
            var UserName = (from P in DB.User
                            select P).ToList();
            ViewBag.Users = UserName;
            return View();
        }

        [HttpPost]
        public ActionResult Report(DateTime Start2,DateTime End2,string type)
        {
            var UserName = (from P in DB.User
                            select P).ToList();
            ViewBag.Users = UserName;
            var Start = Start2;
           
            var End = End2.AddDays(1);
            if (Start2 == null || End2 == null || type == null)
            {
                var item_data_it = (from I in DB.Item
                                    join C in DB.Category on I.category_id equals C.Category_id
                                    join A in DB.Activity on I.item_id equals A.item_request_id
                                    where C.Category_name == "IT" && (A.Date_Time_Request >= Start && A.Date_Time_Request <= End)
                                    orderby I.item_id descending
                                    select A).ToList();
                return View(item_data_it);
            }
            else
            {
                
                    var item_data_it = (from I in DB.Item
                                      
                                        join A in DB.Activity on I.item_id equals A.item_request_id
                                        join U in DB.User on A.user_id equals U.user_id
                                        where U.user_name == type && (A.Date_Time_Request >= Start && A.Date_Time_Request <= End)
                                        orderby I.item_id descending
                                        select A).ToList();
                    int item_data_it_add = (from I in DB.Item
                                           
                                            join A in DB.Activity on I.item_id equals A.item_request_id
                                            join U in DB.User on A.user_id equals U.user_id
                                            where U.user_name == type && A.Request_Type == "Add" && (A.Date_Time_Request >= Start && A.Date_Time_Request <= End)
                                            orderby I.item_id descending
                                            select A).Count();
                    ViewBag.item_data_it_add = item_data_it_add;
                    int item_data_it_edit = (from I in DB.Item
                                            
                                             join A in DB.Activity on I.item_id equals A.item_request_id
                                             join U in DB.User on A.user_id equals U.user_id
                                             where U.user_name == type && A.Request_Type == "Edit" && (A.Date_Time_Request >= Start && A.Date_Time_Request <= End)
                                             orderby I.item_id descending
                                             select A).Count();
                    ViewBag.item_data_it_edit = item_data_it_edit;
                    int item_data_it_with = (from I in DB.Item
                                          
                                             join A in DB.Activity on I.item_id equals A.item_request_id
                                             join U in DB.User on A.user_id equals U.user_id
                                             where U.user_name == type && A.Request_Type == "Withdraw" && (A.Date_Time_Request >= Start && A.Date_Time_Request <= End)
                                             orderby I.item_id descending
                                             select A).Count();
                    ViewBag.item_data_it_with = item_data_it_with;

                    return View(item_data_it);
                
                //else if (type == "TPA")
                //{
                //    var item_data_tpa = (from I in DB.Item
                //                         join C in DB.Category on I.category_id equals C.Category_id
                //                         join A in DB.Activity on I.item_id equals A.item_request_id
                //                         where C.Category_name == "TPA" && (A.Date_Time_Request >= Start && A.Date_Time_Request <= End)
                //                         orderby I.item_id descending
                //                         select A).ToList();
                //    int item_data_tpa_add = (from I in DB.Item
                //                             join C in DB.Category on I.category_id equals C.Category_id
                //                             join A in DB.Activity on I.item_id equals A.item_request_id
                //                             where C.Category_name == "TPA" && A.Request_Type == "Add" && (A.Date_Time_Request >= Start && A.Date_Time_Request <= End)
                //                             orderby I.item_id descending
                //                             select A).Count();
                //    ViewBag.item_data_tpa_add = item_data_tpa_add;
                //    int item_data_tpa_edit = (from I in DB.Item
                //                              join C in DB.Category on I.category_id equals C.Category_id
                //                              join A in DB.Activity on I.item_id equals A.item_request_id
                //                              where C.Category_name == "TPA" && A.Request_Type == "Edit" && (A.Date_Time_Request >= Start && A.Date_Time_Request <= End)
                //                              orderby I.item_id descending
                //                              select A).Count();
                //    ViewBag.item_data_tpa_edit = item_data_tpa_edit;
                //    int item_data_tpa_with = (from I in DB.Item
                //                              join C in DB.Category on I.category_id equals C.Category_id
                //                              join A in DB.Activity on I.item_id equals A.item_request_id
                //                              where C.Category_name == "TPA" && A.Request_Type == "Withdraw" && (A.Date_Time_Request >= Start && A.Date_Time_Request <= End)
                //                              orderby I.item_id descending
                //                              select A).Count();
                //    ViewBag.item_data_tpa_with = item_data_tpa_with;

                //    return View(item_data_tpa);
                //}
                //else if (type == "Issuance")
                //{
                //    var item_data_iss = (from I in DB.Item
                //                         join C in DB.Category on I.category_id equals C.Category_id
                //                         join A in DB.Activity on I.item_id equals A.item_request_id
                //                         where C.Category_name == "Issuance" && (A.Date_Time_Request >= Start && A.Date_Time_Request <= End)
                //                         orderby I.item_id descending
                //                         select A).ToList();
                //    int item_data_iss_add = (from I in DB.Item
                //                             join C in DB.Category on I.category_id equals C.Category_id
                //                             join A in DB.Activity on I.item_id equals A.item_request_id
                //                             where C.Category_name == "Issuance" && A.Request_Type == "Add" && (A.Date_Time_Request >= Start && A.Date_Time_Request <= End)
                //                             orderby I.item_id descending
                //                             select A).Count();
                //    ViewBag.item_data_iss_add = item_data_iss_add;
                //    int item_data_iss_edit = (from I in DB.Item
                //                              join C in DB.Category on I.category_id equals C.Category_id
                //                              join A in DB.Activity on I.item_id equals A.item_request_id
                //                              where C.Category_name == "Issuance" && A.Request_Type == "Edit" && (A.Date_Time_Request >= Start && A.Date_Time_Request <= End)
                //                              orderby I.item_id descending
                //                              select A).Count();
                //    ViewBag.item_data_tpa_edit = item_data_iss_edit;
                //    int item_data_iss_with = (from I in DB.Item
                //                              join C in DB.Category on I.category_id equals C.Category_id
                //                              join A in DB.Activity on I.item_id equals A.item_request_id
                //                              where C.Category_name == "Issuance" && A.Request_Type == "Withdraw" && (A.Date_Time_Request >= Start && A.Date_Time_Request <= End)
                //                              orderby I.item_id descending
                //                              select A).Count();
                //    ViewBag.item_data_iss_with = item_data_iss_with;
                //    return View(item_data_iss);
                //}

                //else
                //{
                //    var item_data_arch = (from I in DB.Item
                //                          join C in DB.Category on I.category_id equals C.Category_id
                //                          join A in DB.Activity on I.item_id equals A.item_request_id
                //                          where C.Category_name == "Archiving" && (A.Date_Time_Request >= Start && A.Date_Time_Request <= End)
                //                          orderby I.item_id descending
                //                          select A).ToList();
                //    int item_data_arch_add = (from I in DB.Item
                //                              join C in DB.Category on I.category_id equals C.Category_id
                //                              join A in DB.Activity on I.item_id equals A.item_request_id
                //                              where C.Category_name == "Archiving" && A.Request_Type == "Add" && (A.Date_Time_Request >= Start && A.Date_Time_Request <= End)
                //                              orderby I.item_id descending
                //                              select A).Count();
                //    ViewBag.item_data_arch_add = item_data_arch_add;
                //    int item_data_arch_edit = (from I in DB.Item
                //                              join C in DB.Category on I.category_id equals C.Category_id
                //                              join A in DB.Activity on I.item_id equals A.item_request_id
                //                              where C.Category_name == "Archiving" && A.Request_Type == "Edit" && (A.Date_Time_Request >= Start && A.Date_Time_Request <= End)
                //                              orderby I.item_id descending
                //                              select A).Count();
                //    ViewBag.item_data_arch_edit = item_data_arch_edit;
                //    int item_data_arch_with = (from I in DB.Item
                //                               join C in DB.Category on I.category_id equals C.Category_id
                //                               join A in DB.Activity on I.item_id equals A.item_request_id
                //                               where C.Category_name == "Archiving" && A.Request_Type == "Withdraw" && (A.Date_Time_Request >= Start && A.Date_Time_Request <= End)
                //                               orderby I.item_id descending
                //                               select A).Count();
                //    ViewBag.item_data_arch_with = item_data_arch_with;
                //    return View(item_data_arch);
                //}
            
            }
           
            
        }

    }
}
