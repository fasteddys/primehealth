using HRMS.DAL.Repositories;
using HRMS.DTOs.Business;
using HRMS.Entities;
using HRMS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static HRMS.BLL.StaticData.StaticEnums;

namespace HRMS.BLL.Logic.Tables
{
    public class UserTimeAttendanceBLL
    {
        public readonly DeviceBLL DeviceBLL;
        public readonly UserBLL userBLL;
        public readonly TimeAttendanceBLL timeAttendanceBLL;
        DateTime creationDate;


        public UserTimeAttendanceBLL(HRMSEntities Context, DateTime CreationDate)
        {
            DeviceBLL = new DeviceBLL(Context, CreationDate);
            userBLL = new UserBLL(Context, CreationDate);
            timeAttendanceBLL = new TimeAttendanceBLL(Context, CreationDate);
            creationDate = CreationDate;

        }

        private List<DailyAttendanceDTO> GetDayesBetweenInterval(DateTime StartDate, DateTime EndDate)
        {
            List<DailyAttendanceDTO> dates = new List<DailyAttendanceDTO>();
            //DateTime StartDate = DateTime.ParseExact(Start, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //DateTime EndDate = DateTime.ParseExact(End, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            for (var dt = StartDate.Date; dt <= EndDate.Date; dt = dt.AddDays(1))
            {
                DailyAttendanceDTO date = new DailyAttendanceDTO();
                date.Date = dt.Date.ToString("d/M/yyyy");
                date.FingerPrintIn = "---";
                date.FingerPrintOut = "---";
                dates.Add(date);
            }

            return dates;
        }

        public List<DailyAttendanceDTO> GetAttendance(TimeAttendanceParametersDTO TimeAttendanceParametersDTO)
        {
            if (TimeAttendanceParametersDTO.EndDate == null || TimeAttendanceParametersDTO.EndDate == "")
                TimeAttendanceParametersDTO.EndDate = DateTime.Now.Date.ToString("dd/MM/yyyy");

            DateTime StartDate = DateTime.ParseExact(TimeAttendanceParametersDTO.StartDate, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime EndDate = DateTime.ParseExact(TimeAttendanceParametersDTO.EndDate, @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //if (EndDate.CompareTo(DateTime.Now) > 0)
            //    EndDate = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), @"dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            var Dates = GetDayesBetweenInterval(StartDate, EndDate);

            var UserAccessControl = userBLL.Find(x => x.UserID == TimeAttendanceParametersDTO.UserID)?.FirstOrDefault();
            if (UserAccessControl.AccessControlUserFK == null)
                return Dates;

            TimeSpan UpdateStartDateTime = new TimeSpan(0, 1, 0);
            StartDate = StartDate.Date + UpdateStartDateTime;

            TimeSpan UpdateEndDateTime = new TimeSpan(23, 59, 59);
            EndDate = EndDate.Date + UpdateEndDateTime;

            var data = timeAttendanceBLL.Find(x => x.UserAccessControlID == UserAccessControl.AccessControlUserFK && (x.ActionDate.CompareTo(StartDate) >= 0 && x.ActionDate.CompareTo(EndDate) <= 0)).OrderBy(x => x.ActionDate).ToList();
            //var data = timeAttendanceBLL. Find(x => x.UserAccessControlID == UserAccessControlID && (x.ActionDate.Day <= EndDate.Day && x.ActionDate.Month == EndDate.Month && x.ActionDate.Year == EndDate.Year) && (x.ActionDate.Day >= StartDate.Day && x.ActionDate.Month == StartDate.Month && x.ActionDate.Year == StartDate.Year)).OrderBy(x => x.ActionDate).ToList();

            if (data.Count == 0)
                return Dates;

            List<DailyAttendanceDTO> ListAttendance = new List<DailyAttendanceDTO>();
            try
            {
                for (int i = 0; i < data.Count; i++)
                {


                    var y = DeviceBLL.GetDeviceType((int)data[i].DeviceFK);
                    if (ListAttendance.Where(x => x.Date.ToString() == data[i].ActionDate.Day.ToString() + "/" + data[i].ActionDate.Month.ToString() + "/" + data[i].ActionDate.Year.ToString()

                           ).ToList().Count < 1// && DeviceBLL.GetDeviceType(data[i].DeviceFK).DeviceTypeFK == (int)DeviceType.IN
                           )
                    {
                        DailyAttendanceDTO DayAttendance = new DailyAttendanceDTO();
                        DayAttendance.Date = data[i].ActionDate.Date.Day + "/" + data[i].ActionDate.Date.Month + "/" + data[i].ActionDate.Date.Year;
                        DayAttendance.FingerPrintIn = data[i].ActionDate.ToShortTimeString().ToString();

                        ListAttendance.Add(DayAttendance);
                        for (int j = data.Count - 1; j >= 0; j--)
                        {

                            try
                            {

                                if (DayAttendance.Date == data[j].ActionDate.Day.ToString() + "/" + data[j].ActionDate.Month.ToString() + "/" + data[j].ActionDate.Year.ToString()

                                    //  && DeviceBLL.GetDeviceType(data[j].DeviceFK).DeviceTypeFK == (int)DeviceType.OUT
                                    )
                                {
                                    if (DayAttendance.FingerPrintIn != data[j].ActionDate.ToShortTimeString().ToString())
                                    {
                                        DayAttendance.FingerPrintOut = data[j].ActionDate.ToShortTimeString().ToString();
                                    }
                                    //ListAttendance.Add(new DailyAttendanceDTO { Day = data[j].ActionDate.Date, });
                                    break;
                                }
                            }
                            catch (Exception ex)
                            {
                                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-BLL");
                                continue;

                            }

                        }


                    }



                }
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-BLL");
            }

            //for (var it = 0; it <= Dates.Count; it++ )
            //{
            //    for(var j = 0; j <= ListAttendance.Count; j++)
            //    {
            //        if (!ListAttendance[j].Date.Equals(Dates[it]))
            //            ListAttendance.
            //    }
            //}

            for (var i = 0; i < Dates.Count; i++)
            {
                for (var j = 0; j < ListAttendance.Count; j++)
                {
                    if (Dates[i].Date.Equals(ListAttendance[j].Date))
                        Dates[i] = ListAttendance[j];
                }

            }
            return Dates;
        }



    }


}
