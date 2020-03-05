using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CypressPendingRequestsReminder.DTOs;
using CypressPendingRequestsReminder.Entities;
using CypressPendingRequestsReminder.StaticData;
using static CypressPendingRequestsReminder.StaticData.StaticEnums;

namespace CypressPendingRequestsReminder.BLL
{
    public class RequestBLL
    {

        public async Task ScheduleOfExecutionAsync()
        {
            List<TimeSpan> ListOfTimes = new List<TimeSpan>();
            using (HRMSEntities HRMS = new HRMSEntities())
            {
                foreach (var item in HRMS.Configurations.Where(x => x.ConfigurationKey.Contains("EmailReminderServiceRuntime")).Select(x => x.ConfigurationValue).ToList())
                {
                    ListOfTimes.Add(TimeSpan.Parse(item));

                }
               
            }
            ListOfTimes= ListOfTimes.OrderBy(x => x).ToList();
            foreach (var item in ListOfTimes)
            {
                RequestBLL Request = new RequestBLL();
                TimeSpan alertTime = item;
                DateTime current = DateTime.Now;
                TimeSpan timeToGo = alertTime - current.TimeOfDay;
                if (timeToGo >= TimeSpan.Zero)
                {
                    await Task.Delay(timeToGo);
                    GetAllLateRequestMailAsync();
                    
                }
            }
            TimeSpan EndDayTime =new TimeSpan(24,00,00);
            DateTime CurrentDayTime = DateTime.Now;
            TimeSpan TimeToNextDay = EndDayTime - CurrentDayTime.TimeOfDay;
            await Task.Delay(TimeToNextDay);
            await ScheduleOfExecutionAsync();



        }

        public void GetAllLateRequestMailAsync()
        {
        
            List<RequestDTO> ListLateRequests = new List<RequestDTO>();
            int Time = 0;
            using (HRMSEntities HRMS = new HRMSEntities())
            {
                List<Request> ListRequest = HRMS.Requests.Where(x => x.RequesStatusFK == (int)RequestStatus.Pending).ToList();
                foreach (var item in ListRequest)
                {
                    var ItemRequestDTO = RequestMaping(item);
                    ItemRequestDTO.IncluedHR = false;
                    if (HRMS.ServicesLogs.Where(x => x.RequestFK == ItemRequestDTO.RequestID && x.Value == ItemRequestDTO.Order.ToString()).Count() <
                        Convert.ToInt32(HRMS.Configurations.Where(x => x.ConfigurationKey == "NumberOfReminders").FirstOrDefault().ConfigurationValue))
                    {
                        var ratingValue = 1 - (Convert.ToDouble(HRMS.Configurations.Where(x => x.ConfigurationKey == "PendingReminderRate").FirstOrDefault().ConfigurationValue) / 100);
                        if (ItemRequestDTO.Order == 1)
                        {
                            if (HRMS.ServicesLogs.Where(x => x.RequestFK == ItemRequestDTO.RequestID && x.Value == ItemRequestDTO.Order.ToString()).Count() 
                                >0)
                            {
                                ItemRequestDTO.IncluedHR = true;
                            }

                            //(ItemRequestDTO.CreationDate - ItemRequestDTO.DurationFrom).Value.Days * ratingValue;
                            if (ItemRequestDTO.DurationFrom > ItemRequestDTO.CreationDate)
                            {


                                var RequirdApprovalBeforStartRequest = (ItemRequestDTO.DurationFrom - ItemRequestDTO.CreationDate).Value.TotalMinutes * ratingValue;
                                if (RequirdApprovalBeforStartRequest > (ItemRequestDTO.DurationFrom - DateTime.Now).TotalMinutes)
                                {
                                    ListLateRequests.Add(ItemRequestDTO);

                                }
                            }
                            else
                            {


                                var RequirdApprovalBeforStartRequest =
                                    (ItemRequestDTO.CreationDate - ItemRequestDTO.DurationFrom).Value.TotalMinutes * ratingValue;
                                if (RequirdApprovalBeforStartRequest < (DateTime.Now - ItemRequestDTO.CreationDate).Value.TotalMinutes)
                                {
                                    ListLateRequests.Add(ItemRequestDTO);

                                }
                            }


                        }
                        else
                        {
                            ApprovalFlowRequestDetail RequestDetails = HRMS.ApprovalFlowRequestDetails.Where
                              (x => x.RequestFK == ItemRequestDTO.RequestID && x.Order == ItemRequestDTO.Order - 1
                               && x.IsDeleted == false && x.ActionDate != null).FirstOrDefault();
                            if (RequestDetails?.ActionDate != null)
                            {
                                if (HRMS.ServicesLogs.Where(x => x.RequestFK == ItemRequestDTO.RequestID && x.Value == ItemRequestDTO.Order.ToString()).Count()> 0)
                                {
                                    ItemRequestDTO.IncluedHR = true;
                                }


                                if (ItemRequestDTO.DurationFrom > RequestDetails?.ActionDate.Value)
                                {
                                    var RequirdApprovalBeforStartRequest = (ItemRequestDTO.DurationFrom - RequestDetails?.ActionDate.Value).Value.TotalMinutes * ratingValue;
                                    if (RequirdApprovalBeforStartRequest > (ItemRequestDTO.DurationFrom - DateTime.Now).TotalMinutes)
                                    {
                                        ListLateRequests.Add(ItemRequestDTO);

                                    }
                                }

                                else
                                {
                                    var RequirdApprovalBeforStartRequest = (RequestDetails?.ActionDate.Value - ItemRequestDTO.DurationFrom).Value.TotalMinutes * ratingValue;


                                    if (RequirdApprovalBeforStartRequest < (DateTime.Now - RequestDetails?.ActionDate.Value).Value.TotalMinutes)
                                    {
                                        ListLateRequests.Add(ItemRequestDTO);

                                    }
                                }

                            }


                        }

                    }


                }


                foreach (var item in ListLateRequests)
                {

                    SendReminderMail(item);
                    HRMS.ServicesLogs.Add(new ServicesLog
                    {
                        IsActive = true,
                        IsDeleted = false,
                        RequestFK = item.RequestID,

                        CreationDate = DateTime.Now,
                        ServiceLogTypeFK = (int)ServiceLogType.SendLateMailToManager,
                        ServicesFK = (int)ServicesType.SendReminderMailService,
                        Value = item.Order.ToString(),
                    });
                }
                HRMS.SaveChanges();


            }
   
        }
        public void SendReminderMail(RequestDTO Request)
        {
            using (HRMSEntities HRMS = new HRMSEntities())
            {
                LeaveType LeaveType = new LeaveType();
                User User = HRMS.Users.Where(x => x.UserID == Request.UserFK).FirstOrDefault();
                string HREmail = HRMS.Configurations.Where(x => x.ConfigurationKey == "HREmail").FirstOrDefault().ConfigurationValue;
                LeaveType.LeaveTypeName= HRMS.LeaveTypes.Where(x => x.LeaveTypeID == Request.LeaveTypeFK).FirstOrDefault().LeaveTypeName;
                try
                {
                    MailingDTO MailingDTO = new MailingDTO
                    {
                        RequestID = Request.RequestID,
                        Action = "submitted",

                        LeaveTypeName = LeaveType.LeaveTypeName,
                        Duration = Request.RequestDuration,
                        StartDate = Request.DurationFrom,
                        EndtDate = Request.DurationTo,
                        ComeBackDate = Request.BackToWork,
                         DurationUnitID= Request.LeaveDurationUnitFK,


                        Unit = ((StaticEnums.LeaveDurationUnit)Enum.Parse(typeof(StaticEnums.LeaveDurationUnit), Request.LeaveDurationUnitFK.ToString())).ToString()
                    };
                    MailingDTO.To = new List<string>();
                    MailingDTO.CC = new List<string>();
                    List<User> UserToSendEmail = GetUserShouldApproveTheRequest(Request);
                    MailingDTO.DurationUnitID = Request.LeaveDurationUnitFK;
                    MailingDTO.Actionto = User.UserName;
                    MailingDTO.CC.Add(User.UserEmail);
                    MailingDTO.IncludeHr = Request.IncluedHR;
                    if (Request.IncluedHR == true)
                    {
                        MailingDTO.CC.Add(HREmail);
                    }
                    MailingDTO.To.AddRange(UserToSendEmail.Select(x => x.UserEmail));
                    for (int i = 0; i < UserToSendEmail.Count(); i++)
                    {
                        if (i == 0)
                        {
                            MailingDTO.ActionBy = MailingDTO.ActionBy + UserToSendEmail[i].UserName;
                        }
                        else
                        {
                            MailingDTO.ActionBy = MailingDTO.ActionBy + "/" + UserToSendEmail[i].UserName;

                        }
                    }
                    if (HRMS.Configurations.Where(x => x.ConfigurationKey == "CheckMailEnabled").FirstOrDefault().ConfigurationValue == "1")
                    {
                        Mailing Mailing = new Mailing();
                        Mailing.PrepareMailToSent(MailingDTO);
                    }
                }
                catch (Exception ex)
                {

                    // ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-BLL");
                }


            }

        }
        public List<User> GetUserShouldApproveTheRequest(RequestDTO Request)
        {
            using (HRMSEntities HRMS = new HRMSEntities())
            {
                List<ApprovalFlowRequestDetail> ApprovalFlowDetail = new List<ApprovalFlowRequestDetail>();
                User User = HRMS.Users.Where(x => x.UserID == Request.UserFK).FirstOrDefault();
                List<User> ListManagers = new List<User>();
                int? TeamManagerID = HRMS.SubDepartments.Where(x => x.SubDepartmentID == User.SubDepartmentFK)?.FirstOrDefault()?.TeamManagerFK;
                int? UserTeamManagerID = HRMS.Managers.Where(x => x.ManagerID == TeamManagerID).FirstOrDefault()?.ManagerUserFK;

                int? ManagerID = User?.DirectManagerFK;
                int? DirectManagerID = HRMS.Managers.Where(x => x.ManagerID == ManagerID).FirstOrDefault()?.ManagerUserFK;
                int NextOrder = 0;

                NextOrder = Request.Order;
                User Manager = new User();
                ApprovalFlowDetail = HRMS.ApprovalFlowRequestDetails.Where(x => x.RequestFK == Request.RequestID && x.Order == NextOrder && x.IsDeleted == false).ToList();

                foreach (var item in ApprovalFlowDetail)
                {
                    if (item.ApprovalPersonFK != null)
                    {
                        ListManagers.AddRange(HRMS.Users.Where(x => x.UserID == item.ApprovalPersonFK).ToList());

                    }

                    else if (item.IsDirectManager == true)
                    {
                        ListManagers.AddRange(HRMS.Users.Where(x => x.UserID == DirectManagerID).ToList());

                    }

                    else if (item.IsTeamManager == true)
                    {
                        ListManagers.AddRange(HRMS.Users.Where(x => x.UserID == UserTeamManagerID).ToList());

                    }

                }
                return ListManagers;
            }
        }

        public RequestDTO RequestMaping(Request item)
        {
            return new RequestDTO
            {
                BackToWork = item.BackToWork,
                Comment = item.Comment,
                CreationDate = item.CreationDate,
                DeletionDate = item.DeletionDate,
                DurationFrom = item.DurationFrom,
                DurationTo = item.DurationTo,
                IsActive = item.IsActive,
                IsDeleted = item.IsDeleted,
                LeaveDurationUnitFK = item.LeaveDurationUnitFK,
                LeaveTypeFK = item.LeaveTypeFK,
                Level = item.Level,
                ModificationDate = item.ModificationDate,
                OnBehalfOfRequesterID = item.OnBehalfOfRequesterID,
                Order = item.Order,
                PartialDurationUnitFK = item.PartialDurationUnitFK,
                Reason = item.Reason,
                RequesStatusFK = item.RequesStatusFK,
                RequestDuration = item.RequestDuration,
                RequestID = item.RequestID,
                Substitute = item.Substitute,
                UserFK = item.UserFK,
                
            };

        }
    }
}