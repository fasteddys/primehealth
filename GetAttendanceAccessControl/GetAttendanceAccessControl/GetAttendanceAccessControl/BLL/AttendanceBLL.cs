using GetAttendanceAccessControl.DLL;
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
        public List<GetAttendance_Result> GetLeatestAccess()
        {
            try
            {
                using (HRMSEntities HRMS = new HRMSEntities())
                {
                    List<punchlog> LeatestRecords = new List<punchlog>();

                    AttendanceEntities Access = new AttendanceEntities();
                    var number = Convert.ToInt32(HRMS.Configurations.Where(x => x.ConfigurationKey == "NumberOfAccess").FirstOrDefault().ConfigurationValue);
                    var data = Access.GetAttendance(number).ToList();

                    return data;

                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public void InsertNewAttendanceInHRMS(List<GetAttendance_Result> LeatestRecords)
        {
            List<TimeAttendance> TimeATTendanceHRMS = new List<TimeAttendance>();
            foreach (var item in LeatestRecords)
            {
                TimeATTendanceHRMS.Add(new TimeAttendance
                {
                     TimeAttendanceSerial = (int)item.id,
                    UserAccessControlID = Convert.ToInt32(item.user_id),
                    ActionDate = item.devdt.AddHours(2),
                    ActionTypeName = item.type,
                    PersonName = item.user_name,
                     DeviceFK = Convert.ToInt32(item.devid),
                    IsActive=true,
                    IsDeleted=false


                });

            }
            using (HRMSEntities HRMS = new HRMSEntities())
            {
                foreach (var item in TimeATTendanceHRMS)
                {
                    try
                    {
                        if (HRMS.TimeAttendances.Where(x => x.TimeAttendanceSerial == item.TimeAttendanceSerial).Count() < 1)
                        {
                            HRMS.TimeAttendances.Add(item);
                            HRMS.SaveChanges();

                        }
                    }
                    catch (Exception ex)
                    {
                    }


                }
            }


        }
    }
}
