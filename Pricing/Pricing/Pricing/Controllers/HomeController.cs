using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pricing.Models;
using System.IO;
using PagedList;

namespace Pricing.Controllers
{
    
    public class HomeController : Controller
    {
        PricingDBEntities1 DB = new PricingDBEntities1();
        // GET: Home
        [Authorize]
        public ActionResult Create()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.AppendCacheExtension("no-store, must-revalidate");
            Response.AddHeader("Pragma", "no-cache");
            Response.AppendHeader("Expires", "0");// Proxies.
            if (Request.Cookies["UserNameCookie"] ==null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult Create(string ProviderName, string ContractType, int? Policy, float? ContractVersion, string Notes , HttpPostedFileBase file)
        {
            string ActiveUser = Request.Cookies["UserNameCookie"].Value;
            ContractsDetail Contract = new ContractsDetail();
            Attachment attach = new Attachment();
            Contract.Creator = ActiveUser;
            Contract.CreationTime = DateTime.Now;
            Contract.ProviderName = ProviderName;
            Contract.ContractType = ContractType;
            Contract.Policy = Policy;
            Contract.ContractVersion = ContractVersion;
            Contract.ContractNotes = Notes;
            DB.ContractsDetails.Add(Contract);
            DB.SaveChanges();
            string FileName = Path.GetFileName(file.FileName);
            string physicalPath = Server.MapPath("~/Attachment/" + FileName);
            file.SaveAs(physicalPath);
            //new
            string backupBath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"\\10.1.1.15\Backups\Developers Backup\8-Pricing backup\Attachment\", file.FileName);

            file.SaveAs(backupBath);


            //end
            attach.ID = Contract.ID;
            attach.AtatchPath = physicalPath;
            attach.FileName = FileName;
            attach.UploadDate = DateTime.Now.Date;
            DB.Attachments.Add(attach);
            DB.SaveChanges();
            ViewBag.Success = "Contract Inserted Successfully";
            return View();
        }
        public ActionResult AddAttach(int ID , HttpPostedFileBase file)
        {
            var ContractDetails = (from C in DB.ContractsDetails
                                   where C.ID == ID
                                   select C).SingleOrDefault();
            Attachment attach = new Attachment();
            string FileName = Path.GetFileName(file.FileName);
            string physicalPath = Server.MapPath("~/Attachment/" + FileName);
            file.SaveAs(physicalPath);
            attach.ID =ID;
            attach.AtatchPath = physicalPath;
            attach.FileName = FileName;
            attach.UploadDate = DateTime.Now.Date;
            DB.Attachments.Add(attach);
            DB.SaveChanges();
            ViewBag.success = "Attachment Added Successfully";
            return RedirectToAction("ShowDetailsOfContract", "Home" , new { ContractDetails });
        }
        [Authorize]
        public ActionResult ViewContracts(int Pagesize = 10, int page = 1)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.AppendCacheExtension("no-store, must-revalidate");
            Response.AddHeader("Pragma", "no-cache");
            Response.AppendHeader("Expires", "0");// Proxies.
            if (Request.Cookies["UserNameCookie"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                var Contracts = (from C in DB.ContractsDetails
                                 select C).ToList();
                PagedList<ContractsDetail> Paging = new PagedList<ContractsDetail>(Contracts, page, Pagesize);
                return View(Paging);
            }
        }
        [Authorize]
        public ActionResult Edit(int ID)
        {
            var ContractToEdit = (from C in DB.ContractsDetails
                                  where C.ID == ID
                                  select C).SingleOrDefault();
            return View(ContractToEdit);
        }
        [HttpPost]
        public ActionResult Edit (int ID, string ProviderName, string ContractType, string Policy, string ContractVersion, string Notes, HttpPostedFileBase file)
        {
            ContractsDetail ContractToEdit = DB.ContractsDetails.Find(ID);
            ContractToEdit.ProviderName = ProviderName;
            ContractToEdit.ContractType = ContractType;
            ContractToEdit.Policy = int.Parse(Policy) ;
            ContractToEdit.ContractVersion = double.Parse(ContractVersion);
            DB.SaveChanges();
            return RedirectToAction("ViewContracts", "Home");
        }
        [Authorize]
        public ActionResult Delete (int ID)
        {
            ContractsDetail ContractToDelete = DB.ContractsDetails.Find(ID);
            DB.ContractsDetails.Remove(ContractToDelete);
            DB.SaveChanges();
            return RedirectToAction("ViewContracts", "Home");
        }
        public ActionResult ShowDetailsOfContract(int? ID, string From, string To, int page = 1, int Pagesize = 10, string Status = "")
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.AppendCacheExtension("no-store, must-revalidate");
            Response.AddHeader("Pragma", "no-cache");
            Response.AppendHeader("Expires", "0");// Proxies.
            if (Request.Cookies["UserNameCookie"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                if (ID == null)
                {
                    return RedirectToAction("ViewContracts", "Home");
                }
                else
                {
                    List<Attachment> attachments = new List<Attachment>();
                    DateTime IntervalFrom = new DateTime();
                    DateTime IntervalTo = new DateTime();
                    if (From != null)
                    {
                        IntervalFrom = DateTime.Parse(From).Date;
                        IntervalTo = DateTime.Parse(To).Date;
                        attachments = (from a in DB.Attachments
                                       where a.ID == ID && a.UploadDate >= IntervalFrom && a.UploadDate <= IntervalTo
                                       select a).ToList();
                        Status = "Display";
                    }
                    else
                    {
                        attachments = (from a in DB.Attachments
                                       where a.ID == ID
                                       select a).ToList();
                    }
                    var ContractDetails = (from C in DB.ContractsDetails
                                           where C.ID == ID
                                           select C).SingleOrDefault();

                    PagedList<Attachment> Paging = new PagedList<Attachment>(attachments, page, Pagesize);
                    if (attachments.Count() == 0)
                    {
                        ViewBag.Empty = "No Attachments To show";
                    }
                    ViewBag.IntervalFrom = IntervalFrom.ToString();
                    ViewBag.IntervalTo = IntervalTo.ToString();
                    ViewBag.Status = Status;
                    ViewBag.ContractDetails = ContractDetails;
                    return View(Paging);
                }
            }
        }
        [HttpPost]
        public ActionResult Search(string SearchText , int Pagesize = 10, int page = 1)
        {
            
            
                var SearchResult = (from s in DB.ContractsDetails
                                    where s.ProviderName.Contains(SearchText)
                                    select s).ToList();
                PagedList<ContractsDetail> Paging = new PagedList<ContractsDetail>(SearchResult, page, Pagesize);

                return View(Paging);
            
        }
        [Authorize]
        //public FileResult Download(int ID)
        //{
        //    var attaches = (from C in DB.Attachments
        //                    where C.AttachID == ID
        //                    select C).SingleOrDefault();
        //    string AtatchName = attaches.UploadDate.Value.Date.ToString("dd/MM/yyyy");
        //    byte[] fileBytes = System.IO.File.ReadAllBytes(attaches.AtatchPath);
        //    string fileName = AtatchName + ".pdf";
        //    return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        //}

        public FileResult Download(int ID)
        {
            var attaches = (from C in DB.Attachments
                            where C.AttachID == ID
                            select C).First();
            //string AtatchName = attaches.UploadDate.Value.Date.ToString("dd/MM/yyyy");
            byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath("~/Attachment/" + attaches.FileName));
            string fileName = attaches.FileName + ".pdf";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);

            
        }
        
    }   
}