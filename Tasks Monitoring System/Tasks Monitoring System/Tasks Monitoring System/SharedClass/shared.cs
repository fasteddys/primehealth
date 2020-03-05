using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TasksMonitoringSystem.SharedClass
{
    public static class shared
    {
        public static TaskMSEntities db = new TaskMSEntities();
        public static List<CompanyDIM> GetAllCompanies()
        {
            List<CompanyDIM> AllCompanies = new List<CompanyDIM>();
            //AllCompanies.Add(new CompanyDIM
            //{
            //    CompanyID = -1,
            //    CompanyName = "Select Company"
            //});
            AllCompanies.Add(new CompanyDIM
            {
                CompanyID = 0,
                CompanyName = "All"
            });
            AllCompanies.AddRange(db.CompanyDIMs);
            return AllCompanies;
        }
    }
}