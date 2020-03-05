using AccessControlUsers.DLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace AccessControlAttendance.BLL
{
   public class AttendanceBLL
    {
        public List<user> GetUsers()
        {
            try
            {
                List<user> LeatestRecords = new List<user>();

                using (AttendanceEntities Access = new AttendanceEntities())
                {
                     LeatestRecords = Access.users.ToList();
                }

         
            return LeatestRecords; }
            catch (Exception e) {
                return null;
            }
            
        }

        public void InsertUsersInHRMS(List<user> LeatestRecords)
        {
            List<AccessControlUser> AccessControlUserHRMS = new List<AccessControlUser>();
            foreach (var item in LeatestRecords)
            {
                AccessControlUserHRMS.Add(new AccessControlUser
                {
                    AccessControlUserID = Convert.ToInt32(item.user_id),
                    AccessControlUserName = item.name,
                    GroupName = item.user_group_name,
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                });

            }
            using (HRMSEntities HRMS = new HRMSEntities())
            {
                foreach (var item in AccessControlUserHRMS)
                {
                    if (HRMS.AccessControlUsers.Where(x => x.AccessControlUserID == item.AccessControlUserID).Count() < 1)
                    {
                        HRMS.AccessControlUsers.Add(item);
                        HRMS.SaveChanges();

                    }
                }
            }


        }

       
    }
}
