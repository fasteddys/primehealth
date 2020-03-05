using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
    public class HRViewRequestsFilterDTO
    {
        //public HRViewRequestsFilterDTO(string json)
        //{
        //    JObject jObject = JObject.Parse(json);
        //    JToken jUser = jObject["user"];
        //    name = (string)jUser["name"];
        //    teamname = (string)jUser["teamname"];
        //    email = (string)jUser["email"];
        //    players = jUser["players"].ToArray();
        //}

        public int StatusID { get; set; }
        public int LeaveTypeID { get; set; }
        public int EntitlementChangedByID { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public int Year { get; set; }
        public int? Month { get; set; }
        public List<int> ListLeaveType { get; set; }
        public List<int> ListUsers { get; set; }
        public List<int> ListSubDepartment { get; set; }
        public List<int> ListDepartment { get; set; }
    }
}
