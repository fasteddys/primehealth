using Hr_System.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Hr_System.Classes
{
    public class Helpers
    {

        HRSystemEntities hr = new HRSystemEntities();

        public List<SubDep> getSubDepsForDuputyManger(string User)
        {
            accountTB user = new accountTB();
            DepartmentTB dep = new DepartmentTB();
            user = (from p in hr.accountTBs where p.EmpName == User select p).SingleOrDefault();
            dep = (from d in hr.DepartmentTBs where d.DeptName == user.DepartmentName select d).SingleOrDefault();
            List < SubDep > result = new List<SubDep>();
            result = (from p in hr.SubDeps
                      where p.DepartmentID==dep.ID
                      select p).ToList();
            return result;
        }

        public string getSubDepTeamLeaderName(string subDep)
        {
            var teamleader = (from p in hr.SubDeps where p.SubDepartmentName == subDep select p.DepBackupTeamLeader).SingleOrDefault();
            return teamleader;
        }
        public string getSubDepSupervisor(string subDep)
        {
            var Supervisor = (from p in hr.SubDeps where p.SubDepartmentName == subDep select p.DepTeamLeader).SingleOrDefault();
            return Supervisor;
        }
        public string getSubDepBackupSupervisor(string subDep)
        {
            var SupervisorBackup = (from p in hr.SubDeps where p.SubDepartmentName == subDep select p.DepBackupSupervisor).SingleOrDefault();
            return SupervisorBackup;
        }
        public string getSubDepSenior(string subDep)
        {
            var DepartmentSenior = (from p in hr.SubDeps where p.SubDepartmentName == subDep select p.DepSenior).SingleOrDefault();
            return DepartmentSenior;
        }
        public string getDepDuputyManager(string Dep)
        {
            var DuputyManager = (from p in hr.DepartmentTBs where p.DeptName == Dep select p.DeptBackupManager).SingleOrDefault();
            return DuputyManager;
        }
        public string getSubDepManagerName(string subDep)
        {
           var manager = (from p in hr.SubDeps where p.SubDepartmentName == subDep select p.DepManager).SingleOrDefault();
            return manager;
        }
        public string getDepManagerName(string Dep)
        {
            var manager = (from p in hr.DepartmentTBs where p.DeptName == Dep select p.DeptManager).SingleOrDefault();
            return manager;

        }
        public string getDepName(string User)
        {
            var data = from p in hr.DepartmentTBs
                       where p.DeptManager == User
                       select p;
            string dm = data.SingleOrDefault().DeptName.ToString();
            return dm;

        }

        public string getSubDepName(string User)
        {
            var data = from p in hr.SubDeps
                       where p.DepTeamLeader == User
                       select p;
            string dm = data.SingleOrDefault().DepTeamLeader.ToString();
            return dm;

        }
        public bool haveSubDep(string user)
        {
            //try
            //{
            var data = from p in hr.accountTBs
                       where p.EmpName == user
                       select p;
            //string subDep = data.First().SubDepartment.ToString();
            if ((data.First().SubDepartmentName).Equals(DBNull.Value))
            {
                return false;
            }
            else
            {
                return true;
            }
            //}
            //catch (Exception)
            //{
            //    return false;
            //}


        }
        public int haveExcuseCredit(string user)
        {
            var data = from p in hr.accountTBs
                       where p.EmpName == user
                       select p;
            int no = int.Parse(data.First().NumberOfRemainingPermissions.ToString());
            return no;
        }
        public decimal GetRemainingExcuseHours(string user)
        {
            var data = from p in hr.accountTBs
                       where p.EmpName == user
                       select p;
            //decimal NumberOfRemainingExcuseHours = decimal.Parse(data.First().NumberOfExcuseHours.ToString());
            decimal? NumberOfRemainingExcuseHours = data.First().NumberOfExcuseHours;
            return NumberOfRemainingExcuseHours.GetValueOrDefault();
        }
        //public string getRemainingExcusesForUser(string user)
        //{
        //    var result = hr.accountTBs.Where(a => a.EmpName.Equals(user)).ToList();
        //    string data = result.First().NumberOfRemainingPermissions.ToString();
        //    return data;
        //}
        public List<Request> getAllMyExcusRequests(string user)
        {
            var results = (from p in hr.Requests
                          where p.UserName == user && p.ReqType == "Excuse"
                          select p).OrderByDescending(p=>p.ID);
            return results.ToList();
        }
        public List<Request> getNewExcusesRequestsToTeamLeader(string user)
        {
            var result = from p in hr.Requests
                         where p.MyTeamLeader == user
                           && p.ReqStatus == "PendingTeamLeaderApproval"
                           && p.ReqType == "Excuse"
                         select p;
            return result.ToList();
        }
        public List<Request> getExcusesRequestsToManager(string user)
        {
            List<Request> result = new List<Request>();
            result = hr.Requests.Where(a => a.MyManager.Equals(user) && a.ReqType.Equals("Excuse")).ToList();
            return result;
        }
        public List<Request> getMyExcuseRequestsByStatusByAllTypes(string user, string status_1,string status_2,string status_3,string status_4,string status_5,string status_6)
        {
            List<Request> result = new List<Request>();
            result = hr.Requests.Where(a => a.UserName.Equals(user) &&( a.ReqStatus.Equals(status_1)|| a.ReqStatus.Equals(status_2)||a.ReqStatus.Equals(status_3)||a.ReqStatus.Equals(status_4)||a.ReqStatus.Equals(status_5)||a.ReqStatus.Equals(status_6)) && a.ReqType.Equals("Excuse")).OrderByDescending(a=>a.ID).ToList();
            return result;
        }
        public List<Request> getMyExcuseRequestsByStatus(string user, string status)
        {
            List<Request> result = new List<Request>();
            result = hr.Requests.Where(a => a.UserName.Equals(user) && (a.ReqStatus.Equals(status)) && a.ReqType.Equals("Excuse")).OrderByDescending(a=>a.ID).ToList();
            return result;
        }
        public List<Request> getTeamLeaderPendinExcusesRequests(string user)
        {
            List<Request> result = new List<Request>();
            result = hr.Requests.Where(a => a.ReqType.Equals("Excuse")).Where(a => a.MyTeamLeader.Equals(user) || a.MyBackupTeamLeader.Equals(user)).Where(a => a.ReqStatus.Equals("PendingTeamLeaderApproval") || a.ReqStatus.Equals("PendingSeniorApproval")).ToList();
            return result;
        }
        public List<Request> getSupervisorExcusesRequests(string user)
        {
            List<Request> result = new List<Request>();
            result = hr.Requests.Where(a => a.ReqType.Equals("Excuse")).Where(a => a.MySupervisor.Equals(user) || a.MyBackupSupervisor.Equals(user)).Where(a => a.ReqStatus.Equals("PendingSupervisorApproval") || a.ReqStatus.Equals("PendingSupervisorBackupApproval")).ToList();
            return result;
        }
        //NEW
        public List<Request> GetBackupSupervisorBackupRequests(string user)
        {
            List<Request> result = new List<Request>();
            result = hr.Requests.Where(a=>a.ReqType.Equals("Excuse")).Where(a=>a.MyBackupSupervisor.Equals(user)).Where(a=>a.ReqStatus.Equals("PendingSupervisorBackupApproval")).ToList();
            return result;
        }
        //END
        public List<Request> getDuputyManagerExcusesRequests(string user)
        {
            var requests = hr.Requests.Where(item => item.ReqType == "Excuse").Where(p => p.MySupervisor == user || p.MyBackupManager == user).Where(p => p.ReqStatus == "PendingSupervisorApproval" || p.ReqStatus == "PendingSupervisorBackupApproval" || p.ReqStatus == "PendingDuputyManagerApproval").ToList();
            List<Request> RequestsForDuputyManager = new List<Request>();
            foreach (var req in requests)
            {
                if ((req.MySupervisor == user) && (req.ReqStatus == "PendingSupervisorApproval" || req.ReqStatus == "PendingSupervisorBackupApproval" || req.ReqStatus == "RevokedByUser" || req.ReqStatus == "RevokedByTeamLeader"))
                {
                    RequestsForDuputyManager.Add(req);
                }
                if ((req.MyBackupManager == user) && (req.ReqStatus == "PendingDuputyManagerApproval" || req.ReqStatus == "PendingDuputyManagerApprovalOnRevoke"))
                {
                    RequestsForDuputyManager.Add(req);
                }
            }
            return RequestsForDuputyManager;
        }
        public List<Request> getTeamLeaderPendinExcusesRequestsForManager(string managername)
        {
            List<Request> result = new List<Request>();
            result = hr.Requests.Where(a => a.MyManager.Equals(managername) && ((a.ReqStatus.Equals("PendingTeamLeaderApproval")||a.ReqStatus.Equals("PendingSeniorApproval")) && a.ReqType.Equals("Excuse"))).ToList();
            return result;
        }
        public List<Request> getSupervisorsPendinExcusesRequestsForManager(string managername)
        {
            List<Request> result = new List<Request>();
            result = hr.Requests.Where(a => a.MyManager.Equals(managername) && ((a.ReqStatus.Equals("PendingSupervisorApproval") || a.ReqStatus.Equals("PendingSupervisorBackupApproval")||a.ReqStatus== "PendingDuputyManagerApproval") && a.ReqType.Equals("Excuse"))).ToList();
            return result;
        }
        public List<Request> getTeamLeaderPendinExcusesRequestsForSupervisor(string managername)
        {
            List<Request> result = new List<Request>();
            result = hr.Requests.Where(a => a.MySupervisor.Equals(managername) && ((a.ReqStatus.Equals("PendingTeamLeaderApproval") || a.ReqStatus.Equals("PendingSeniorApproval")) && a.ReqType.Equals("Excuse"))).ToList();
            return result;
        }
        public List<Request> getManagerPendinExcusesRequests(string user)
        {
            List<Request> result = new List<Request>();
            result = hr.Requests.Where(a => a.MyManager.Equals(user)).Where((a=>a.ReqStatus.Equals("PendingManagerApproval")||a.ReqStatus== "PendingDuputyManagerApproval")).Where(a=>a.ReqType.Equals("Excuse")).ToList();
            return result;
        }
        public List<Request> getRejectedExcusesByTeamLeadersForManager(string managername)
        {
            List<Request> result = new List<Request>();
            result = hr.Requests.Where(a => a.MyManager.Equals(managername) && ((a.ReqStatus.Equals("RejectedByTeamLeader")) && a.ReqType.Equals("Excuse"))).ToList();
            return result;
        }
        public List<Request> getRejectedExcusesBySupervisorsForManager(string managername)
        {
            List<Request> result = new List<Request>();
            result = hr.Requests.Where(a => a.MyManager.Equals(managername) && ((a.ReqStatus.Equals("RejectedBySupervisor")) && a.ReqType.Equals("Excuse"))).ToList();
            return result;
        }
       
        public string getRequeststatus(int ID)
        {
            var result = hr.Requests.Where(a => a.ID.Equals(ID) && a.ReqType.Equals("Excuse")).ToList();
            string data = result.First().ReqStatus.ToString();
            return data;
        }
        public List<accountTB> getUserDataById(int ID)
        {
            List<accountTB> result = new List<accountTB>();
            result = hr.accountTBs.Where(a => a.ID.Equals(ID)).ToList();
            return result;
        }
        public int getIdForUserInRedPoints(string user)
        {
            var data = hr.RedPointsReports.Where(a => a.UserName.Equals(user)).Select(p => p.ID).FirstOrDefault(); ;
            return data;
        }
        public int getIdForUserInGreenPoints(string user)
        {
            var data = hr.GreenPointsReports.Where(a => a.UserName.Equals(user)).Select(p => p.ID).FirstOrDefault(); ;
            return data;
        }
        public List<accountTB> getUserDataForManager(string Dep)
        {
            List<accountTB> result = new List<accountTB>();
            result = hr.accountTBs.Where(a => a.SubDepartmentName.Equals(Dep)).ToList();
            return result;
        }
        public List<accountTB> getUserDataForTeamLeader(string subDep)
        {
            List<accountTB> result = new List<accountTB>();
            result = hr.accountTBs.Where(a => a.SubDepartmentName.Equals(subDep)).ToList();
            return result;
        }
        public void addNotifications(string date, string from, string to, string message, string Dep, string SubDep, string Status)
        {
            Notification noti = new Notification();
            noti.NotificationDate = date;
            noti.FromName = from;
            noti.NotifyTo = to;
            noti.Message = message;
            noti.Department = Dep;
            noti.SubDepartment = SubDep;
            noti.Status = Status;
            hr.Notifications.Add(noti);
            hr.SaveChanges();
        }
        public List<SubDep> getSubDepsForManager(string Manager)
        {
            List<SubDep> result = new List<SubDep>();
            result = hr.SubDeps.Where(a => a.DepManager.Equals(Manager)).ToList();
            return result;
        }

        public List<SubDep> getAllSubDepsForHR()
        {
            List<SubDep> result = new List<SubDep>();
            result = hr.SubDeps.Select(x=>x).ToList();
            return result;
        }
        public List<SubDep> getSubDepsForDuputyOrSupervisors(string User)
        {
            List<SubDep> result = new List<SubDep>();
            result = (from p in hr.SubDeps
                     where p.DepTeamLeader == User || p.DepBackupSupervisor == User
                     select p).ToList();
            return result;
        }
        public List<SubDep> getSubDepsSupervisor(string Supervisor)
        {
            List<SubDep> result = new List<SubDep>();
            result = hr.SubDeps.Where(a => a.DepTeamLeader.Equals(Supervisor)).ToList();
            return result;
        }
        public string getSubDepByID(int ID)
        {
            string result;
            result = hr.SubDeps.Where(a => a.ID.Equals(ID)).First().SubDepartmentName.ToString();
            return result;
        }
        public List<ProjectMember> getProjectMembers(string Project)
        {
            List<ProjectMember> result = new List<ProjectMember>();
            result = hr.ProjectMembers.Where(a => a.ProjectName.Equals(Project)).ToList();
            return result;
        }

        /// <summary>
        /// check if manager delegated or not to view(show) the manager suspended vacation tab for deputy manager only.
        /// </summary>
        /// <param name="Name">Deputy Manager Name</param>
        /// <returns> boolean value</returns>
        public bool CheckifManagerDelegated(string Name)
        {
            //try
            //{
            //     var GetManagerNameIfDelegated = hr.accountTBs.Where(a => a.DelegateTo == Name && a.DelegatedAuthorities == "yes").FirstOrDefault().EmpName;
            //     return true;
            //}
            //catch (Exception)
            //{

            //    return false;
            //}
            var departmentName = (from item in hr.accountTBs where item.EmpName == Name select item.DepartmentName).SingleOrDefault();
            var managerdelegation = (from item in hr.DepartmentTBs where item.DeptName == departmentName select item.DeptBackupManager).SingleOrDefault();

            if (managerdelegation != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


    }
}