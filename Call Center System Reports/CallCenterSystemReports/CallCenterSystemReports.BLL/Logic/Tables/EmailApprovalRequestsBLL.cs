using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CallCenterSystemReports.DAL.Repositories;
using CallCenterSystemReports.Entities;
using static CallCenterSystemReports.BLL.StaticData.StaticEnums;
using CallCenterSystemReports.DTOs;

namespace CallCenterSystemReports.BLL.Logic
{
    public class EmailApprovalRequestsBLL : Repository<EmailApprovalRequest>
    {
        public EmailApprovalRequestsBLL(PhNetworkEntities Context) : base(Context)
        {

        }
        public  DateTime StartOfWeek(DateTime dt)
        {
            int diff = (7 + (dt.DayOfWeek - DayOfWeek.Saturday)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }
        public List<TopLeastUsersDTO> GetTopAssignees(string UserType, int NumberOfLeastToTake)
        {
            try
            {
                List<TopLeastUsersDTO> FinalList = new List<TopLeastUsersDTO>();
                DateTime StartOfWeek = this.StartOfWeek(DateTime.Now);
                if (UserType == "CallCenterUser")
                {

                    var Result = GetAll().Where(p => p.RequstStatusID != (int)RequestStatus.AssignedByOpeartor && p.CreationDate > StartOfWeek).Where(p => p.OperatorAssignee != null).GroupBy(p => p.OperatorAssignee).Select(group => new { Asignee = group.Key, TicketCount = group.Count() }).OrderByDescending(p => p.TicketCount).Take(NumberOfLeastToTake);

                    foreach (var item in Result)
                    {
                        TopLeastUsersDTO temp = new TopLeastUsersDTO
                        {
                            UserName = item.Asignee,
                            TicketsCount = item.TicketCount
                        };
                        FinalList.Add(temp);
                    }
                    return FinalList;

                }
                else if (UserType == "CallCenterDoctor")
                {

                    var Result = GetAll().Where(p => p.RequstStatusID != (int)RequestStatus.AssignedByDoctor && p.CreationDate > StartOfWeek).Where(p => p.DoctorAssignee != null).GroupBy(p => p.DoctorAssignee).Select(group => new { Asignee = group.Key, TicketCount = group.Count() }).OrderByDescending(p => p.TicketCount).Take(NumberOfLeastToTake).ToList();
                    foreach (var item in Result)
                    {
                        TopLeastUsersDTO temp = new TopLeastUsersDTO
                        {
                            UserName = item.Asignee,
                            TicketsCount = item.TicketCount
                        };
                        FinalList.Add(temp);
                    }
                    return FinalList;
                }
                else if (UserType == "CallCenterManager" || UserType == "CallCenterAuditDoctor")
                {
                    var Result = GetAll().Where(p => p.RequstStatusID != (int)RequestStatus.AssignedByAudit && p.CreationDate > StartOfWeek).Where(p => p.AuditAssignee != null).GroupBy(p => p.AuditAssignee).Select(group => new { Asignee = group.Key, TicketCount = group.Count() }).OrderByDescending(p => p.TicketCount).Take(NumberOfLeastToTake).ToList();
                    foreach (var item in Result)
                    {
                        TopLeastUsersDTO temp = new TopLeastUsersDTO
                        {
                            UserName = item.Asignee,
                            TicketsCount = item.TicketCount
                        };
                        FinalList.Add(temp);
                    }
                    return FinalList;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<TopLeastUsersDTO> GetLeastAssignees(string UserType, int NumberOfTopToTake)
        {
            try
            {
                List<TopLeastUsersDTO> FinalList = new List<TopLeastUsersDTO>();
                DateTime StartOfWeek = this.StartOfWeek(DateTime.Now);

                if (UserType == "CallCenterUser")
                {
                    var Result = GetAll().Where(p => p.RequstStatusID != (int)RequestStatus.AssignedByOpeartor && p.CreationDate > StartOfWeek).Where(p => p.OperatorAssignee != null).GroupBy(p => p.OperatorAssignee).Select(group => new { Asignee = group.Key, TicketCount = group.Count() }).OrderBy(p => p.TicketCount).Take(NumberOfTopToTake).OrderByDescending(p => p.TicketCount).ToList();
                    foreach (var item in Result)
                    {
                        TopLeastUsersDTO temp = new TopLeastUsersDTO
                        {
                            UserName = item.Asignee,
                            TicketsCount = item.TicketCount
                        };
                        FinalList.Add(temp);
                    }
                    return FinalList;

                }
                else if (UserType == "CallCenterDoctor")
                {
                    var Result = GetAll().Where(p => p.RequstStatusID != (int)RequestStatus.AssignedByDoctor && p.CreationDate > StartOfWeek).Where(p => p.DoctorAssignee != null).GroupBy(p => p.DoctorAssignee).Select(group => new { Asignee = group.Key, TicketCount = group.Count() }).OrderBy(p => p.TicketCount).Take(NumberOfTopToTake).OrderByDescending(p => p.TicketCount).ToList();
                    foreach (var item in Result)
                    {
                        TopLeastUsersDTO temp = new TopLeastUsersDTO
                        {
                            UserName = item.Asignee,
                            TicketsCount = item.TicketCount
                        };
                        FinalList.Add(temp);
                    }
                    return FinalList;
                }
                else if (UserType == "CallCenterManager" || UserType == "CallCenterAuditDoctor")
                {
                    var Result = GetAll().Where(p => p.RequstStatusID != (int)RequestStatus.AssignedByAudit && p.CreationDate > StartOfWeek).Where(p => p.AuditAssignee != null).GroupBy(p => p.AuditAssignee).Select(group => new { Asignee = group.Key, TicketCount = group.Count() }).OrderBy(p => p.TicketCount).Take(NumberOfTopToTake).OrderByDescending(p => p.TicketCount).ToList();
                    foreach (var item in Result)
                    {
                        TopLeastUsersDTO temp = new TopLeastUsersDTO
                        {
                            UserName = item.Asignee,
                            TicketsCount = item.TicketCount
                        };
                        FinalList.Add(temp);
                    }
                    return FinalList;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<WarningTypeAndCountDTO> GetTicketscountGroupedbyWarnningType()
        {


            try
            {
                List<WarningTypeAndCountDTO> ListOfTicketsCountByWarningType = new List<WarningTypeAndCountDTO>();
                WarningTypeAndCountDTO Temp = new WarningTypeAndCountDTO();


                var Normal = GetAll().Where(p => p.IsFirstNotifySent != true).Where(p => p.IsSecondNotifySent != true).Where(p => p.IsThirdNotifySent != true).Where(p=>p.RequstStatusID!=(int)RequestStatus.Closed && p.RequstStatusID!= (int)RequestStatus.Ignored && p.RequstStatusID != (int)RequestStatus.NewAutoGenerated && p.RequstStatusID != (int)RequestStatus.AssignedByOpeartor).Count();
                Temp.WarningType = "Normal";
                Temp.TicketsCount = Normal;
                ListOfTicketsCountByWarningType.Add(Temp);

                var LateFirstWarningRequests = GetAll().Where(p => p.IsFirstNotifySent == true).Where(p => p.IsSecondNotifySent != true).Where(p => p.IsThirdNotifySent != true).Where(p => p.RequstStatusID != (int)RequestStatus.Closed && p.RequstStatusID != (int)RequestStatus.Ignored).Count();
                Temp = new WarningTypeAndCountDTO();
                Temp.WarningType = "FirstWarning";
                Temp.TicketsCount = LateFirstWarningRequests;
                ListOfTicketsCountByWarningType.Add(Temp);

                var LateSecondWarningRequests = GetAll().Where(p => p.IsSecondNotifySent == true).Where(p => p.IsFirstNotifySent == true).Where(p => p.IsThirdNotifySent != true).Where(p => p.RequstStatusID != (int)RequestStatus.Closed && p.RequstStatusID != (int)RequestStatus.Ignored).Count();
                Temp = new WarningTypeAndCountDTO();
                Temp.WarningType = "SecondMorning";
                Temp.TicketsCount = LateSecondWarningRequests;
                ListOfTicketsCountByWarningType.Add(Temp);

                var LateThirdWarningRequests = GetAll().Where(p => p.IsThirdNotifySent == true).Where(p => p.IsFirstNotifySent == true).Where(p => p.IsSecondNotifySent == true).Where(p => p.RequstStatusID != (int)RequestStatus.Closed && p.RequstStatusID != (int)RequestStatus.Ignored).Count();


                Temp = new WarningTypeAndCountDTO();
                Temp.WarningType = "ThirdWarning";
                Temp.TicketsCount = LateThirdWarningRequests;
                ListOfTicketsCountByWarningType.Add(Temp);

                return ListOfTicketsCountByWarningType;
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }


        }

        public List<WarningTypeAndCountDTO> GetTicketscountGroupedbyWarnningTypeForOPerators()
        {


            try
            {
                List<WarningTypeAndCountDTO> ListOfTicketsCountForOperators = new List<WarningTypeAndCountDTO>();
                WarningTypeAndCountDTO Temp = new WarningTypeAndCountDTO();


                var Normal = GetAll().Where(p => p.IsAutoGeneratedNotify != true).Where(p => p.RequstStatusID != (int)RequestStatus.Closed && p.RequstStatusID != (int)RequestStatus.Ignored).Where(p=>p.RequstStatusID == (int)RequestStatus.NewAutoGenerated || p.RequstStatusID == (int)RequestStatus.AssignedByOpeartor).Count();
                Temp.WarningType = "Normal";
                Temp.TicketsCount = Normal;
                ListOfTicketsCountForOperators.Add(Temp);

                var LateAutoGenarated = GetAll().Where(p => p.IsAutoGeneratedNotify == true).Where(p => p.RequstStatusID != (int)RequestStatus.Closed && p.RequstStatusID != (int)RequestStatus.Ignored).Where(p => p.RequstStatusID == (int)RequestStatus.NewAutoGenerated || p.RequstStatusID == (int)RequestStatus.AssignedByOpeartor).Count();
                Temp = new WarningTypeAndCountDTO();
                Temp.WarningType = "LateAutoGenarated";
                Temp.TicketsCount = LateAutoGenarated;
                ListOfTicketsCountForOperators.Add(Temp);


                return ListOfTicketsCountForOperators;
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }


        }


        public List<RequestsCountOverDayDTO> GetRequestsCountOverDay()
        {
            var Result = GetAll().Where(p => p.CreationDate.Value.Day == DateTime.Now.Day).GroupBy(p => p.CreationDate.Value.Hour).Select(group => new { Hours = group.Key, RequestsCount = group.Count() }).OrderBy(p => p.Hours).ToList();
            List<RequestsCountOverDayDTO> FinalResult = new List<RequestsCountOverDayDTO>();
            foreach (var item in Result)
            {
                RequestsCountOverDayDTO temp = new RequestsCountOverDayDTO()
                {

                    Hours = item.Hours,
                    RequestCount = item.RequestsCount
                };
                FinalResult.Add(temp);

            }

            var lasthour = (FinalResult.Last().Hours);
            if (FinalResult.Count < 24)
            {
                for (int i = 0; i < lasthour; i++)
                {
                    var test = FinalResult.Any(p => p.Hours == i);
                    if (test == false)
                    {
                        RequestsCountOverDayDTO temp = new RequestsCountOverDayDTO();
                        temp.Hours = i;
                        temp.RequestCount = 0;
                        FinalResult.Add(temp);
                    }
                }

            }

            //if (FinalResult.Count < 24)
            //{
            //    for (int i = FinalResult.Count; i < 24; i++)
            //    {
            //        RequestsCountOverDayDTO temp = new RequestsCountOverDayDTO();
            //        FinalResult.Add(temp);
            //    }
            //}
            //List<RequestsCountOverDayDTO> ModifiedList = new List<RequestsCountOverDayDTO>(new RequestsCountOverDayDTO[24]);
            //int HourNow = DateTime.Now.Hour;

            //for (int i = 0; i < HourNow; i++)
            //{
            //    foreach (var item in FinalResult)
            //    {
            //        if (i != item.Hours)
            //        {
            //            ModifiedList[item.Hours] = item;
            //            RequestsCountOverDayDTO temp = new RequestsCountOverDayDTO()
            //            {
            //                Hours = i,
            //                RequestCount = 0
            //            };
            //            ModifiedList[i]=temp;

            //        }
            //    }
            //}
            var ModifiedList= FinalResult.OrderBy(p => p.Hours).ToList();
            return ModifiedList;
        }

        public List<RequestsCountOverDayDTO> GetRequestsCountOverDayByDate(DateTime Date)
        {
            var Result = GetAll().Where(p => p.CreationDate.Value.Day == Date.Day).GroupBy(p => p.CreationDate.Value.Hour).Select(group => new { Hours = group.Key, RequestsCount = group.Count() }).OrderBy(p => p.Hours).ToList();
            List<RequestsCountOverDayDTO> FinalResult = new List<RequestsCountOverDayDTO>();
            foreach (var item in Result)
            {
                RequestsCountOverDayDTO temp = new RequestsCountOverDayDTO()
                {

                    Hours = item.Hours,
                    RequestCount = item.RequestsCount
                };
                FinalResult.Add(temp);

            }
            var lasthour = (FinalResult.Last().Hours);
            if (FinalResult.Count < 24)
            {
                for (int i = 0; i < lasthour; i++)
                {
                    var test = FinalResult.Any(p => p.Hours == i);
                    if (test==false)
                    {
                        RequestsCountOverDayDTO temp = new RequestsCountOverDayDTO();
                        temp.Hours = i;
                        temp.RequestCount = 0;
                        FinalResult.Add(temp);
                    }
                }

            }

     


            //if (FinalResult.Count < 24)
            //{
            //    for (int i = FinalResult.Count; i < 24; i++)
            //    {
            //        RequestsCountOverDayDTO temp = new RequestsCountOverDayDTO();
            //        FinalResult.Add(temp);
            //    }
            //}
            //List<RequestsCountOverDayDTO> ModifiedList = new List<RequestsCountOverDayDTO>(new RequestsCountOverDayDTO[24]);
            //int HourNow = DateTime.Now.Hour;

            //for (int i = 0; i < HourNow; i++)
            //{
            //    foreach (var item in FinalResult)
            //    {
            //        if (i != item.Hours)
            //        {
            //            ModifiedList[item.Hours] = item;
            //            RequestsCountOverDayDTO temp = new RequestsCountOverDayDTO()
            //            {
            //                Hours = i,
            //                RequestCount = 0
            //            };
            //            ModifiedList[i] = temp;

            //        }
            //    }
            //}
            var ModifiedList= FinalResult.OrderBy(p => p.Hours).ToList();
            return ModifiedList;
        }


        public List<TikcetsAvaregeTimeDTO> GetTicketsAvrageTime()
        {
           

            var NormalTickets = GetAll().Where(p => p.TicketTypeID == (int)TicketTypes.General).Where(p => p.RequstStatusID == (int)RequestStatus.Closed).ToList();
            List<TikcetsAvaregeTimeDTO> TempGeneral = new List<TikcetsAvaregeTimeDTO>() ;

            foreach (var item in NormalTickets)
            {
                TikcetsAvaregeTimeDTO Temp = new TikcetsAvaregeTimeDTO();
                Temp.TicketType = TicketTypes.General.ToString();
                var temptime = (int)(item.ClosedTime - item.CreationDate).Value.TotalMinutes;
                Temp.AvrageTimeInMinutes = temptime;
                TempGeneral.Add(Temp);

            }
            var SpecialTickets = GetAll().Where(p => p.TicketTypeID == (int)TicketTypes.Special).Where(p => p.RequstStatusID == (int)RequestStatus.Closed).ToList();
            List<TikcetsAvaregeTimeDTO> TempSpecial = new List<TikcetsAvaregeTimeDTO>();

            foreach (var item in SpecialTickets)
            {
                TikcetsAvaregeTimeDTO Temp = new TikcetsAvaregeTimeDTO();
                Temp.TicketType = TicketTypes.General.ToString();
                var temptime = (int)(item.ClosedTime - item.CreationDate).Value.TotalMinutes;
                Temp.AvrageTimeInMinutes = temptime;
                TempSpecial.Add(Temp);

            }
            var InquiresTickets = GetAll().Where(p => p.TicketTypeID == (int)TicketTypes.Inquiries).Where(p => p.RequstStatusID == (int)RequestStatus.Closed).ToList();
            List<TikcetsAvaregeTimeDTO> TempInquires = new List<TikcetsAvaregeTimeDTO>();

            foreach (var item in InquiresTickets)
            {
                TikcetsAvaregeTimeDTO Temp = new TikcetsAvaregeTimeDTO();
                Temp.TicketType = TicketTypes.General.ToString();
                var temptime = (int)(item.ClosedTime - item.CreationDate).Value.TotalMinutes;
                Temp.AvrageTimeInMinutes = temptime;
                TempInquires.Add(Temp);

            }

            var FaxTickets = GetAll().Where(p => p.IsFaxMail==true).Where(p => p.RequstStatusID == (int)RequestStatus.Closed).ToList();
            List<TikcetsAvaregeTimeDTO> TempFax = new List<TikcetsAvaregeTimeDTO>();

            foreach (var item in FaxTickets)
            {
                TikcetsAvaregeTimeDTO Temp = new TikcetsAvaregeTimeDTO();
                Temp.TicketType = TicketTypes.General.ToString();
                var temptime = (int)(item.ClosedTime - item.CreationDate).Value.TotalMinutes;
                Temp.AvrageTimeInMinutes = temptime;
                TempFax.Add(Temp);
            }

            List<TikcetsAvaregeTimeDTO> AvarageTime = new List<TikcetsAvaregeTimeDTO>();
            int GeneralTime=0;
            int GeneralAvargeTime = 0;
            int SpecialTime = 0;
            int SpecialAvargeTime=0;
            int InquiresTime = 0;
            int InquiresAvargeTime=0;
            int FaxTime = 0;
            int FaxAvargeTime=0;
            foreach (var item in TempGeneral)
            {
                GeneralTime += item.AvrageTimeInMinutes;
            }
            foreach (var item in TempSpecial)
            {
                SpecialTime += item.AvrageTimeInMinutes;
            }
            foreach (var item in TempInquires)
            {
                InquiresTime += item.AvrageTimeInMinutes;
            }
            foreach (var item in TempFax)
            {
                FaxTime += item.AvrageTimeInMinutes;
            }

            GeneralAvargeTime = GeneralTime / TempGeneral.Count();
            SpecialAvargeTime = SpecialTime / TempSpecial.Count();
            InquiresAvargeTime = InquiresTime / TempInquires.Count();
            FaxAvargeTime = FaxTime / TempFax.Count();

            TikcetsAvaregeTimeDTO Temp1 = new TikcetsAvaregeTimeDTO();
            TikcetsAvaregeTimeDTO Temp2 = new TikcetsAvaregeTimeDTO();
            TikcetsAvaregeTimeDTO Temp3 = new TikcetsAvaregeTimeDTO();
            TikcetsAvaregeTimeDTO Temp4 = new TikcetsAvaregeTimeDTO();

            Temp1.TicketType = "Genral";
            Temp1.AvrageTimeInMinutes = GeneralAvargeTime;
            Temp2.TicketType = "Special";
            Temp2.AvrageTimeInMinutes = SpecialAvargeTime;
            Temp3.TicketType = "Inquires";
            Temp3.AvrageTimeInMinutes = InquiresAvargeTime;
            Temp4.TicketType = "Fax";
            Temp4.AvrageTimeInMinutes = FaxAvargeTime;

            AvarageTime.Add(Temp1);
            AvarageTime.Add(Temp2);
            AvarageTime.Add(Temp3);
            AvarageTime.Add(Temp4);


            return AvarageTime;
        }
    }
}
