using HRMS.Utilities;
using HRMS.Entities;
using System.Collections.Generic;
using System.Linq;
using System;
using static HRMS.BLL.StaticData.StaticEnums;
using HRMS.BLL.StaticData;
using Microsoft.Reporting.WebForms;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Data;
using System.IO;
using ClosedXML.Excel;
using System.Reflection;
using iTextSharp.text;
using iTextSharp.text.pdf;
using HRMS.API.Reports.HrReport;
using HRMS.Utilities.DTO;
using HRMS.DTOs;
using HRMS.DTOs.Business;
using System.Globalization;
using System.Configuration;

namespace HRMS.API.Controllers
{
    public class RequestController : BaseController
    {
        public ResultViewModel AddRequest(NewRequestDTO NewRequest)
        {

            ResultViewModel Result = new ResultViewModel();
            Result.Success = true;
            int RequestID = 0;
            bool IsValid = true;
            string Messages = "";
            MailingDTO  MailingDTO;
            Request request;
            bool IsAutoApprove = false;
           bool CheckMailEnabled = false;
           Exception exOutputSubmit;
            bool SaveRequestSuccess=false;
            try
            {

                UnitOfWork.RequestBLL.AddNewRequest(NewRequest, out IsValid, out Messages,out MailingDTO,
                    out  request,out CheckMailEnabled,out IsAutoApprove);
                if (IsValid)
                {
                    SaveRequestSuccess= UnitOfWork.Submit(out exOutputSubmit);
                }
                if (request.RequestID != 0 && IsAutoApprove)
                {
                    ManagerActionDTO ManagerActionDTO = new ManagerActionDTO();
                    string Message = "";
                    bool Success = false;
                    ManagerActionDTO.ModifiedByUserId = request.UserFK;
                    ManagerActionDTO.RequestID = request.RequestID;
                    ManagerActionDTO.UserID = (int)request.UserFK;
                    User Requester = new Entities.User();
                    Request Request = new Request();
                    User UserManager = new Entities.User();
                 bool   SendApproveMail = false;
                    UnitOfWork.RequestBLL.RequestApprove(ManagerActionDTO, out Message, out Success,out Requester, out Request, out UserManager,out SendApproveMail);
                    UnitOfWork.Submit(out exOutputSubmit);


                    if (CheckMailEnabled
                        &&( ConfigurationManager.AppSettings["CheckMailEnabled"] == "1" || ConfigurationManager.AppSettings["CheckMailEnabled"] == null)) {
                        MailingDTO.IsAutoApproveMail = IsAutoApprove;
                        MailingDTO.RequestID = request.RequestID;
                        User UserToSendEmail = UnitOfWork.UserBLL.Find(x => x.UserID == request.UserFK).FirstOrDefault();
                        MailingDTO.Actionto = UserToSendEmail.UserName;
                        UnitOfWork.RequestBLL.SendMailOnRequestingVication(MailingDTO);
                    }
                }
                else if (request.RequestID != 0 && CheckMailEnabled
                    && (ConfigurationManager.AppSettings["CheckMailEnabled"] == "1" || ConfigurationManager.AppSettings["CheckMailEnabled"] == null)
                    )
                {
                    MailingDTO.RequestID = request.RequestID;
                    List<User> UserToSendEmail = UnitOfWork.RequestBLL.GetUserShouldApproveTheRequest(request);
                    MailingDTO.ActionBy = UserToSendEmail.FirstOrDefault().UserName;
                    MailingDTO.CC.AddRange(UserToSendEmail.Select(x => x.UserEmail));
                    // MailingDTO.To.Add(User.UserEmail);
                    // MailingDTO.To.Add(User.UserEmail);
                    for (int i = 0; i < UserToSendEmail.Count(); i++)
                    {
                        if (i == 0)
                        {
                            MailingDTO.NextActionto = MailingDTO.NextActionto + UserToSendEmail[i].UserName;
                        }
                        else
                        {
                            MailingDTO.NextActionto = MailingDTO.NextActionto + "/" + UserToSendEmail[i].UserName;

                        }
                    }
                    UnitOfWork.RequestBLL.SendMailOnRequestingVication(MailingDTO);
                }
                Messages = Messages.Replace("##$$", "<br/>");
                return new ResultViewModel
                {
                    Data = request.RequestID,
                    Message = Messages,
                    Success = SaveRequestSuccess

                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = 
                    ex.Message,
                    Success = false

                };
            }

        }

        public ResultViewModel SaveFilesPath(RequestAttachmentDTO RequestAttachmentSaving)
        {

            try
            {
                UnitOfWork.RequestBLL.SaveFilesPath(RequestAttachmentSaving);
                Exception exOutputSubmit;
                if (UnitOfWork.Submit(out exOutputSubmit))
                {
                    return new ResultViewModel
                    {
                        Data = null,
                        Message = "Success",
                        Success = true

                    };
                }
                else
                {
                    return new ResultViewModel
                    {
                        Data = null,
                        Message = exOutputSubmit.Message,
                        Success = false

                    };
                }

            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false

                };
            }

        }




        //Requests View  Start
        [HttpPost]
        public ResultViewModel FiltterRequests(ViewRequestsFilterDTO ViewRequestsFilterDTO)
        {
            if(ViewRequestsFilterDTO.StatusID==0)
            {
                return GetALLUserRequests(ViewRequestsFilterDTO);

            }
           else if (ViewRequestsFilterDTO.StatusID == (int)RequestStatus.Pending)
            {
                return UserRequestsPending(ViewRequestsFilterDTO);
            }
           else
            {
                return UserRequestsApproved(ViewRequestsFilterDTO);
            }
        }
        [HttpPost]
        public ResultViewModel HRFiltterRequests(HRViewRequestsFilterDTO ViewRequestsFilterDTO)
        {


            try
            {

                if (ViewRequestsFilterDTO.ListUsers.Count == 0)
                {
                    if (ViewRequestsFilterDTO.ListSubDepartment.Count == 0)
                    {
                        if (ViewRequestsFilterDTO.ListDepartment.Count() == 0)
                        {
                            ViewRequestsFilterDTO.ListUsers = UnitOfWork.UserBLL.GetAll().Select(x => x.UserID).ToList();
                        }
                        else
                        {

                            var listOfSubdepartment = UnitOfWork.SubDepartmentBLL.GetSubDepartmentByDepartmrntIDs(ViewRequestsFilterDTO.ListDepartment).Select(x => x.SubDepartmentID);
                            ViewRequestsFilterDTO.ListUsers = UnitOfWork.UserBLL.GetSubDepartmentByDepartmrntIDs(listOfSubdepartment.ToList()).Select(x => x.UserID).ToList();
                        }
                    }
                    else
                    {
                        ViewRequestsFilterDTO.ListUsers = UnitOfWork.UserBLL.GetSubDepartmentByDepartmrntIDs(ViewRequestsFilterDTO.ListSubDepartment).Select(x => x.UserID).ToList();

                    }
                }
                List<Request> Result = new List<Entities.Request>();
                Result = UnitOfWork.RequestBLL.HRApprovedRequests(ViewRequestsFilterDTO);
                //if (ViewRequestsFilterDTO.StatusID == (int)RequestStatus.Approved)
                //{
                //    Result = UnitOfWork.RequestBLL.HRApprovedRequests(ViewRequestsFilterDTO);

                //}
                //else if (ViewRequestsFilterDTO.StatusID == (int)RequestStatus.Rejected)
                //{
                //    Result = UnitOfWork.RequestBLL.HRRejectedRequests(ViewRequestsFilterDTO);

                //}


                List<ViewRequestDTO> ViewRequestList = new List<ViewRequestDTO>();
              
                    foreach (var item in Result.ToList())
                {
                    try
                    {
                        string Unit = ((StaticEnums.LeaveDurationUnit)Enum.Parse(typeof(StaticEnums.LeaveDurationUnit), item.LeaveDurationUnitFK.ToString())).ToString();
                        string UserName = UnitOfWork.UserBLL.Find(x => x.UserID == item.UserFK).FirstOrDefault()?.UserName;
                        var LeaveType = UnitOfWork.LeaveTypeBLL.Find(x => x.LeaveTypeID == item.LeaveTypeFK).FirstOrDefault();

                        DailyAttendanceDTO Times = new DailyAttendanceDTO();
                        if (item.PartialDurationUnitFK != null || item.LeaveDurationUnitFK == (int)LeaveDurationUnit.Hours)
                        {
                            TimeAttendanceParametersDTO TimeAttendanceParameters = new TimeAttendanceParametersDTO
                            {
                                StartDate = item.DurationFrom.ToString("dd/MM/yyyy"),
                                EndDate = item.DurationTo.ToString("dd/MM/yyyy"),
                                UserID = (int)item.UserFK

                            };
                            Times = UnitOfWork.UserTimeAttendanceBLL.GetAttendance(TimeAttendanceParameters).FirstOrDefault();
                        }
                        else
                        {
                            Times = new DailyAttendanceDTO
                            {
                                FingerPrintIn = "",
                                FingerPrintOut = ""


                            };

                        }


                        ViewRequestList.Add(new ViewRequestDTO
                        {
                            RequestID = item.RequestID,
                            AccessControlID = UnitOfWork.UserBLL.Find(x => x.UserID == item.UserFK).FirstOrDefault()?.AccessControlUserFK,
                            BackToWork = item.BackToWork,
                            CreationDate = item.CreationDate,
                            DurationFrom = item.DurationFrom,
                            RequestDuration = item.RequestDuration,
                            UserName = UserName,
                            DurationTo = item.DurationTo,
                            LeaveType = UnitOfWork.LeaveTypeBLL.Find(x => x.LeaveTypeID == item.LeaveTypeFK).FirstOrDefault().LeaveTypeName,
                            RequestStatus = UnitOfWork.RequestStatusBLL.Find(x => x.RequestStatusID == item.RequesStatusFK).FirstOrDefault().RequestStatusName,
                            Unit = Unit,
                            PunchIn = Times?.FingerPrintIn,
                            PunchOut = Times?.FingerPrintOut,
                            PartialDurationUnit = UnitOfWork.LeaveTypePartialDurationUnitDIMBLL.Find(x => x.PartialDurationUnitID ==
                              item.PartialDurationUnitFK).FirstOrDefault()?.PartialDurationUnitName

                        });
                    }
                    catch (Exception ex)
                    {

                    }
                    }
        
                return new ResultViewModel
                {
                    Data = ViewRequestList
                    ,
                    Message = "Success",
                    Success = true

                };
            }

            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false

                };
            }
        }
        public ResultViewModel UserRequestsApproved(ViewRequestsFilterDTO ViewRequestsFilterDTO)
        {
            ResultViewModel Result = new ResultViewModel();
            Result.Success = true;
            try
            {
                List<ViewRequestDTO> ViewRequestList = new List<ViewRequestDTO>();
                foreach (var item in UnitOfWork.RequestBLL.UserRequestsApproved(ViewRequestsFilterDTO).ToList())
                {
                    string Unit = ((StaticEnums.LeaveDurationUnit)Enum.Parse(typeof(StaticEnums.LeaveDurationUnit), item.LeaveDurationUnitFK.ToString())).ToString();
                    string UserName = UnitOfWork.UserBLL.Find(x => x.UserID == item.UserFK).FirstOrDefault()?.UserName;
                    ViewRequestList.Add(new ViewRequestDTO
                    {
                        RequestID = item.RequestID,
                        BackToWork = item.BackToWork,
                        CreationDate = item.CreationDate,
                        DurationFrom = item.DurationFrom,
                        RequestDuration = item.RequestDuration,
                        UserName = UserName,
                        DurationTo = item.DurationTo,
                        LeaveType = UnitOfWork.LeaveTypeBLL.Find(x => x.LeaveTypeID == item.LeaveTypeFK).FirstOrDefault().LeaveTypeName,
                        RequestStatus = UnitOfWork.RequestStatusBLL.Find(x => x.RequestStatusID == item.RequesStatusFK).FirstOrDefault().RequestStatusName,
                        Unit = Unit
                    });
                }

                return new ResultViewModel
                {
                    Data = ViewRequestList,
                    Message = "Success",
                    Success = true

                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false

                };
            }
        }
        public ResultViewModel GetALLUserRequests(ViewRequestsFilterDTO ViewRequestsFilterDTO)
        {
            ResultViewModel Result = new ResultViewModel();
            Result.Success = true;

            try
            {

                List<ViewRequestDTO> ViewRequestList = new List<ViewRequestDTO>();
                foreach (var item in UnitOfWork.RequestBLL.GetALLUserRequests(ViewRequestsFilterDTO).ToList())
                {
                    string Unit = ((StaticEnums.LeaveDurationUnit)Enum.Parse(typeof(StaticEnums.LeaveDurationUnit), item.LeaveDurationUnitFK.ToString())).ToString();
                    string UserName = UnitOfWork.UserBLL.Find(x => x.UserID == item.UserFK).FirstOrDefault()?.UserName;
                    ViewRequestList.Add(new ViewRequestDTO
                    {
                        RequestID = item.RequestID,
                        BackToWork = item.BackToWork,
                        CreationDate = item.CreationDate,
                        DurationFrom = item.DurationFrom,
                        RequestDuration = item.RequestDuration,
                        UserName = UserName,
                        DurationTo = item.DurationTo,
                        LeaveType = UnitOfWork.LeaveTypeBLL.Find(x => x.LeaveTypeID == item.LeaveTypeFK).FirstOrDefault().LeaveTypeName,
                        RequestStatus = UnitOfWork.RequestStatusBLL.Find(x => x.RequestStatusID == item.RequesStatusFK).FirstOrDefault().RequestStatusName,
                        Unit = Unit
                    });
                }

                return new ResultViewModel
                {
                    Data = ViewRequestList,
                    Message = "Success",
                    Success = true

                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false

                };
            }
        }
        public ResultViewModel UserRequestsRejected(ViewRequestsFilterDTO ViewRequestsFilterDTO)
        {
            ResultViewModel Result = new ResultViewModel();
            Result.Success = true;
            try
            {
                List<ViewRequestDTO> ViewRequestList = new List<ViewRequestDTO>();
                foreach (var item in UnitOfWork.RequestBLL.UserRequestsRejected(ViewRequestsFilterDTO).ToList())
                {
                    string Unit = ((StaticEnums.LeaveDurationUnit)Enum.Parse(typeof(StaticEnums.LeaveDurationUnit), item.LeaveDurationUnitFK.ToString())).ToString();
                    string UserName = UnitOfWork.UserBLL.Find(x => x.UserID == item.UserFK).FirstOrDefault()?.UserName;
                    ViewRequestList.Add(new ViewRequestDTO
                    {
                        RequestID = item.RequestID,
                        BackToWork = item.BackToWork,
                        CreationDate = item.CreationDate,
                        DurationFrom = item.DurationFrom,
                        RequestDuration = item.RequestDuration,
                        UserName = UserName,
                        DurationTo = item.DurationTo,
                        LeaveType = UnitOfWork.LeaveTypeBLL.Find(x => x.LeaveTypeID == item.LeaveTypeFK).FirstOrDefault().LeaveTypeName,
                        RequestStatus = UnitOfWork.RequestStatusBLL.Find(x => x.RequestStatusID == item.RequesStatusFK).FirstOrDefault().RequestStatusName,
                        Unit = Unit
                    });
                }

                return new ResultViewModel
                {
                    Data = ViewRequestList,
                    Message = "Success",
                    Success = true

                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false

                };
            }
        }
        public ResultViewModel UserRequestsPending(ViewRequestsFilterDTO ViewRequestsFilterDTO)
        {
            ResultViewModel Result = new ResultViewModel();
            Result.Success = true;

            try
            {
                List<ViewRequestDTO> ViewRequestList = new List<ViewRequestDTO>();
                foreach (var item in UnitOfWork.RequestBLL.UserRequestsPending(ViewRequestsFilterDTO).ToList())
                {
                    string Unit = ((StaticEnums.LeaveDurationUnit)Enum.Parse(typeof(StaticEnums.LeaveDurationUnit), item.LeaveDurationUnitFK.ToString())).ToString();
                    string UserName = UnitOfWork.UserBLL.Find(x => x.UserID == item.UserFK).FirstOrDefault()?.UserName;
                    ViewRequestList.Add(new ViewRequestDTO
                    {
                        RequestID = item.RequestID,
                        BackToWork = item.BackToWork,
                        CreationDate = item.CreationDate,
                        DurationFrom = item.DurationFrom,
                        RequestDuration = item.RequestDuration,
                        UserName = UserName,
                        DurationTo = item.DurationTo,
                        LeaveType = UnitOfWork.LeaveTypeBLL.Find(x => x.LeaveTypeID == item.LeaveTypeFK).FirstOrDefault().LeaveTypeName,
                        RequestStatus = UnitOfWork.RequestStatusBLL.Find(x => x.RequestStatusID == item.RequesStatusFK).FirstOrDefault().RequestStatusName,
                        Unit = Unit
                    });
                }
                return new ResultViewModel
                {
                    Data = ViewRequestList,
                    Message = "Success",
                    Success = true

                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false

                };
            }
        }
        //Requests View  End

        //Approvals   View  Start
        [HttpPost]
        public ResultViewModel FiltterApprovals(ViewRequestsFilterDTO ViewRequestsFilterDTO)
        {
            if (ViewRequestsFilterDTO.StatusID == 0)
            {
                return GetALLRequestByManager(ViewRequestsFilterDTO);
                
            }
            else if (ViewRequestsFilterDTO.StatusID == (int)RequestStatus.Pending)
            {
                return RequestsPendingManagerApproval(ViewRequestsFilterDTO);
            }
            else
            {
                return GetApprovedRequestByManager(ViewRequestsFilterDTO);
            }
        }
        public ResultViewModel GetApprovedRequestByManager(ViewRequestsFilterDTO ViewRequestsFilterDTO)
        {
            ResultViewModel Result = new ResultViewModel();
            Result.Success = true;
            try
            {
                List<ViewRequestDTO> ViewRequestList = new List<ViewRequestDTO>();
                foreach (var item in UnitOfWork.RequestBLL.GetApprovedRequests(ViewRequestsFilterDTO).ToList())
                {
                    string Unit = ((StaticEnums.LeaveDurationUnit)Enum.Parse(typeof(StaticEnums.LeaveDurationUnit), item.LeaveDurationUnitFK.ToString())).ToString();
                    string UserName = UnitOfWork.UserBLL.Find(x => x.UserID == item.UserFK).FirstOrDefault()?.UserName;
                    ViewRequestList.Add(new ViewRequestDTO
                    {
                        RequestID = item.RequestID,
                        BackToWork = item.BackToWork,
                        CreationDate = item.CreationDate,
                        DurationFrom = item.DurationFrom,
                        RequestDuration = item.RequestDuration,
                        UserName = UserName,
                        DurationTo = item.DurationTo,
                        LeaveType = UnitOfWork.LeaveTypeBLL.Find(x => x.LeaveTypeID == item.LeaveTypeFK).FirstOrDefault().LeaveTypeName,
                        RequestStatus = UnitOfWork.RequestStatusBLL.Find(x => x.RequestStatusID == item.RequesStatusFK).FirstOrDefault().RequestStatusName,
                        Unit = Unit
                    });
                }

                return new ResultViewModel
                {
                    Data = ViewRequestList,
                    Message = "Success",
                    Success = true

                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false

                };
            }
        }
        public ResultViewModel GetALLRequestByManager(ViewRequestsFilterDTO ViewRequestsFilterDTO)
        {
            ResultViewModel Result = new ResultViewModel();
            Result.Success = true;
            try
            {
                List<ViewRequestDTO> ViewRequestList = new List<ViewRequestDTO>();
                foreach (var item in UnitOfWork.RequestBLL.RequestsOnTheFlowOfManager(ViewRequestsFilterDTO).ToList())
                {
                    string Unit = ((StaticEnums.LeaveDurationUnit)Enum.Parse(typeof(StaticEnums.LeaveDurationUnit), item.LeaveDurationUnitFK.ToString())).ToString();
                    string UserName = UnitOfWork.UserBLL.Find(x => x.UserID == item.UserFK).FirstOrDefault()?.UserName;
                    ViewRequestList.Add(new ViewRequestDTO
                    {
                        RequestID = item.RequestID,
                        BackToWork = item.BackToWork,
                        CreationDate = item.CreationDate,
                        DurationFrom = item.DurationFrom,
                        RequestDuration = item.RequestDuration,
                        UserName = UserName,
                        DurationTo = item.DurationTo,
                        LeaveType = UnitOfWork.LeaveTypeBLL.Find(x => x.LeaveTypeID == item.LeaveTypeFK).FirstOrDefault().LeaveTypeName,
                        RequestStatus = UnitOfWork.RequestStatusBLL.Find(x => x.RequestStatusID == item.RequesStatusFK).FirstOrDefault().RequestStatusName,
                        Unit = Unit
                    });
                }

                return new ResultViewModel
                {
                    Data = ViewRequestList,
                    Message = "Success",
                    Success = true

                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false

                };
            }
        }
        public ResultViewModel GetRejectedRequestByManager(ViewRequestsFilterDTO ViewRequestsFilterDTO)
        {
            ResultViewModel Result = new ResultViewModel();
            Result.Success = true;
            try
            {

                List<ViewRequestDTO> ViewRequestList = new List<ViewRequestDTO>();
                foreach (var item in UnitOfWork.RequestBLL.GetRejectedRequests(ViewRequestsFilterDTO).ToList())
                {
                    string Unit = ((StaticEnums.LeaveDurationUnit)Enum.Parse(typeof(StaticEnums.LeaveDurationUnit), item.LeaveDurationUnitFK.ToString())).ToString();
                    string UserName = UnitOfWork.UserBLL.Find(x => x.UserID == item.UserFK).FirstOrDefault()?.UserName;
                    ViewRequestList.Add(new ViewRequestDTO
                    {
                        RequestID = item.RequestID,
                        BackToWork = item.BackToWork,
                        CreationDate = item.CreationDate,
                        DurationFrom = item.DurationFrom,
                        RequestDuration = item.RequestDuration,
                        UserName = UserName,
                        DurationTo = item.DurationTo,
                        LeaveType = UnitOfWork.LeaveTypeBLL.Find(x => x.LeaveTypeID == item.LeaveTypeFK).FirstOrDefault().LeaveTypeName,
                        RequestStatus = UnitOfWork.RequestStatusBLL.Find(x => x.RequestStatusID == item.RequesStatusFK).FirstOrDefault().RequestStatusName,
                        Unit = Unit
                    });
                }
                return new ResultViewModel
                {
                    Data = ViewRequestList,
                    Message = "Success",
                    Success = true

                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false

                };
            }
        }
        public ResultViewModel RequestsPendingManagerApproval(ViewRequestsFilterDTO ViewRequestsFilterDTO)
        {
            ResultViewModel Result = new ResultViewModel();
            Result.Success = true;
            try
            {
                List<ViewRequestDTO> ViewRequestList = new List<ViewRequestDTO>();
                foreach (var item in UnitOfWork.RequestBLL.RequestsPendingManagerApproval(ViewRequestsFilterDTO).ToList())
                {
                    string Unit;
                    try
                    {
                        Unit = ((StaticEnums.LeaveDurationUnit)Enum.Parse(typeof(StaticEnums.LeaveDurationUnit), item.LeaveDurationUnitFK.ToString())).ToString();
                    }
                    catch
                    {
                        continue;
                    }
                    string UserName = UnitOfWork.UserBLL.Find(x => x.UserID == item.UserFK).FirstOrDefault()?.UserName;
                    ViewRequestList.Add(new ViewRequestDTO
                    {
                        RequestID = item.RequestID,
                        BackToWork = item.BackToWork,
                        CreationDate = item.CreationDate,
                        DurationFrom = item.DurationFrom,
                        RequestDuration = item.RequestDuration,
                        UserName = UserName,
                        DurationTo = item.DurationTo,
                        LeaveType = UnitOfWork.LeaveTypeBLL.Find(x => x.LeaveTypeID == item.LeaveTypeFK).FirstOrDefault().LeaveTypeName,
                        RequestStatus = UnitOfWork.RequestStatusBLL.Find(x => x.RequestStatusID == item.RequesStatusFK).FirstOrDefault().RequestStatusName,
                        Unit = Unit
                    });
                }
                return new ResultViewModel
                {
                    Data = ViewRequestList,
                    Message = "Success",
                    Success = true

                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false

                };
            }
        }
        //Approvals   View  End
        public ResultViewModel GetAllSubDepartments(int UserID)
        {
            ResultViewModel Result = new ResultViewModel();
            Result.Success = true;
            try
            {
                List<ViewRequestDTO> ViewRequestList = new List<ViewRequestDTO>();
                ViewRequestsFilterDTO ViewRequestsFilterDTO = new ViewRequestsFilterDTO();
                ViewRequestsFilterDTO.UserID = UserID;
                foreach (var item in UnitOfWork.RequestBLL.UserRequestsApproved(ViewRequestsFilterDTO).ToList())
                {
                    string Unit = ((StaticEnums.LeaveDurationUnit)Enum.Parse(typeof(StaticEnums.LeaveDurationUnit), item.LeaveDurationUnitFK.ToString())).ToString();
                    string UserName = UnitOfWork.UserBLL.Find(x => x.UserID == item.UserFK).FirstOrDefault()?.UserName;
                    ViewRequestList.Add(new ViewRequestDTO
                    {
                        RequestID = item.RequestID,
                        BackToWork = item.BackToWork,
                        CreationDate = item.CreationDate,
                        DurationFrom = item.DurationFrom,
                        RequestDuration = item.RequestDuration,
                        UserName = UserName,
                        DurationTo = item.DurationTo,
                        LeaveType = UnitOfWork.LeaveTypeBLL.Find(x => x.LeaveTypeID == item.LeaveTypeFK).FirstOrDefault().LeaveTypeName,
                        RequestStatus = UnitOfWork.RequestStatusBLL.Find(x => x.RequestStatusID == item.RequesStatusFK).FirstOrDefault().RequestStatusName,
                        Unit = Unit
                    });
                }
                return new ResultViewModel
                {
                    Data = ViewRequestList,
                    Message = "Success",
                    Success = true

                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false

                };
            }
        }
        public ResultViewModel GetUserOutStandingLeaves(int UserID, int LeaveTypeID)
        {
            ResultViewModel Result = new ResultViewModel();
            Result.Success = true;
            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.RequestBLL.GetUserOutStandingLeaves(UserID, LeaveTypeID),
                    Message = "Success",
                    Success = true

                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false

                };
            }
        }
        public ResultViewModel GetUserProfileRequests(int UserID, DateTime StartDate, DateTime EndDate, int LeaveTypeID, int StatusID)
        {
            ResultViewModel Result = new ResultViewModel();
            Result.Success = true;
            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.RequestBLL.GetUserProfileRequests(UserID, StartDate, EndDate, LeaveTypeID, StatusID).ToList(),
                    Message = "Success",
                    Success = true

                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false

                };
            }
        }
        [HttpPost]
        public ResultViewModel Approve(ManagerActionDTO ManagerActionDTO)
        {
           // Request Request = new Request();

            ResultViewModel Result = new ResultViewModel();
            Result.Success = true;

            try
            {
                string Message = "";
                bool Success = true;
                User Requester=new Entities.User();
                Request Request=new Request();
                User UserManager=new Entities.User();
                bool SendApproveMail = false;
                string RejectionReason="";
                UnitOfWork.RequestBLL.RequestApprove(ManagerActionDTO, out Message, out Success, out Requester, out Request,out UserManager,out SendApproveMail);
                Exception exOutputSubmit;
                if (UnitOfWork.Submit(out exOutputSubmit))
                {
                    if (SendApproveMail == true

                        && (ConfigurationManager.AppSettings["CheckMailEnabled"] == "1" || ConfigurationManager.AppSettings["CheckMailEnabled"] == null)
                        )
                    {
                        UnitOfWork.RequestBLL.SendApproveMail(Requester, Request, UserManager);
                    }

                    return new ResultViewModel
                    {
                        Data = null,
                        Message = "Success",
                        Success = true

                    };
                }
                else
                {
                    return new ResultViewModel
                    {
                        Data = null,
                        Message = exOutputSubmit.Message,
                        Success = false

                    };
                }

            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = false,
                    Message = ex.Message,
                    Success = false

                };
            }

        }
        [HttpPost]
        public ResultViewModel Reject(ManagerActionDTO ManagerActionDTO)
        {
            ResultViewModel Result = new ResultViewModel();
            Result.Success = true;

            try
            {
                User Requester = new Entities.User();
                Request Request = new Request();
                User UserManager = new Entities.User();
                string RejectionReason = "";
                UnitOfWork.RequestBLL.RequestReject(ManagerActionDTO, out Requester, out Request, out UserManager, out RejectionReason);
                Exception exOutputSubmit;
                if (UnitOfWork.Submit(out exOutputSubmit))
                {
                    if ((ConfigurationManager.AppSettings["CheckMailEnabled"] == "1" || ConfigurationManager.AppSettings["CheckMailEnabled"] == null))
                    {
                        UnitOfWork.RequestBLL.SendRejectMAil(Requester, Request, UserManager, RejectionReason);
                    }
                    return new ResultViewModel
                    {
                        Data = null,
                        Message = "Success",
                        Success = true

                    };
                }
                else
                {
                    return new ResultViewModel
                    {
                        Data = null,
                        Message = exOutputSubmit.Message,
                        Success = false

                    };
                }

            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false

                };
            }

        }


        [HttpPost]
        public ResultViewModel CheckIfRequestPendingManagerApproval(ManagerPendingRequests CheckRequestPendingManager)
        {
            Request Request = new Request();
            ResultViewModel Result = new ResultViewModel();
            Result.Success = true;

            try
            {
                   return new ResultViewModel
                   {
                       Data = UnitOfWork.RequestBLL.CheckIfRequestPendingManagerApproval(CheckRequestPendingManager.RequestID, CheckRequestPendingManager.UserID),
                       Message = "Success",
                       Success = true

                   };

                //Exception exOutputSubmit;
                //if ()
                //{
                //    return new ResultViewModel
                //    {
                //        Data = null,
                //        Message = "Success",
                //        Success = true

                //    };
                //}
                //else
                //{
                //    return new ResultViewModel
                //    {
                //        Data = null,
                //        Message = exOutputSubmit.Message,
                //        Success = false

                //    };
                //}

            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false

                };
            }

        }
        [HttpGet]
        public ResultViewModel ViewRequestsDetails(int RequestID)
        {
            try
            {

                return new ResultViewModel
                {
                    Data = UnitOfWork.RequestBLL.ViewRequestsDetails(RequestID),
                    Message = "Success",
                    Success = true

                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false

                };
            }

        }
        public ResultViewModel CheckIfDay(int LeaveTypeID)
        {
            ResultViewModel Result = new ResultViewModel();
            Result.Success = true;
            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.RequestBLL.CheckIfDay(LeaveTypeID),
                    Message = "Success",
                    Success = true

                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false

                };
            }
        }
        [HttpGet]
        public HttpResponseMessage Report(int RequestID)
        {
            var OriginalRequest = UnitOfWork.RequestBLL.Find(x => x.RequestID == RequestID).SingleOrDefault();
            if (OriginalRequest.RequesStatusFK == (int)RequestStatus.Approved)
            {
                LocalReport lr = new LocalReport();
                string path = System.Web.Hosting.HostingEnvironment.MapPath("/Reports/Vacation.rdlc");
                if (System.IO.File.Exists(path))
                {
                    lr.ReportPath = path;
                }
                else
                {
                    //  return null;
                }
                lr.ReportPath = path;
                //VacationRequest_Result vacationRequest_Result = new VacationRequest_Result();
                var vacation = UnitOfWork.StoredFunctions.GetVacationRequestReport(RequestID);
                //vacationRequest_Result.DepartmentArName = vacation.DepartmentArName;
                //vacationRequest_Result.LeaveTypeArName = vacation.LeaveTypeArName;
                //vacationRequest_Result.StatusArName = vacation.StatusArName;
                //vacationRequest_Result.UnitArName = vacation.UnitArName;
                //vacationRequest_Result.UserArName = vacation.UserArName;
                //vacationRequest_Result.DurationFrom = vacation.DurationFrom;
                //vacationRequest_Result.DurationTo = vacation.DurationTo;
                //vacationRequest_Result.RequestDuration = vacation.RequestDuration;
                //vacationRequest_Result.StatusArName = vacation.StatusArName;
                //vacationRequest_Result.UnitArName = vacation.UnitArName;


                ReportDataSource rd = new ReportDataSource("Vacation", vacation);
                lr.DataSources.Add(rd);
                string reportType = "PDF";
                string mimeType;
                string encoding;
                string fileNameExtension;
                string deviceInfo =

                "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>8.5in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>1in</MarginLeft>" +
                "  <MarginRight>1in</MarginRight>" +
                "  <MarginBottom>0.5in</MarginBottom>" +
                "</DeviceInfo>";

                Warning[] warnings;
                string[] streams;
                //byte[] renderedBytes;

                //renderedBytes = lr.Render(
                //    reportType,
                //    deviceInfo,
                //    out mimeType,
                //    out encoding,
                //    out fileNameExtension,
                //    out streams,
                //    out warnings);
                //return File(renderedBytes, mimeType);

                byte[] ResponseStream = lr.Render(
                        reportType,
                        deviceInfo,
                        out mimeType,
                        out encoding,
                        out fileNameExtension,
                        out streams,
                        out warnings);
                HttpResponseMessage apiResponse;
                apiResponse = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(ResponseStream)
                };
                apiResponse.Content.Headers.ContentLength = ResponseStream.Length;
                apiResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                apiResponse.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = String.Format("LeaveRequest_" + RequestID + "_" + DateTime.Now.ToShortDateString().ToString() + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + ".pdf")
                };

                return apiResponse;
            }
            else
            {
                return null;
            }
        }
        [AcceptVerbs("Post")]
        public HttpResponseMessage ExtractHRReportPDF(HRViewRequestsFilterDTO ViewRequestsFilterDTO)
        {
            if (ViewRequestsFilterDTO.ListUsers.Count == 0)
            {
                if (ViewRequestsFilterDTO.ListSubDepartment.Count == 0)
                {
                    if (ViewRequestsFilterDTO.ListDepartment.Count() == 0)
                    {
                        ViewRequestsFilterDTO.ListUsers = UnitOfWork.UserBLL.GetAll().Select(x => x.UserID).ToList();
                    }
                    else
                    {

                        var listOfSubdepartment = UnitOfWork.SubDepartmentBLL.GetSubDepartmentByDepartmrntIDs(ViewRequestsFilterDTO.ListDepartment).Select(x => x.SubDepartmentID);
                        ViewRequestsFilterDTO.ListUsers = UnitOfWork.UserBLL.GetSubDepartmentByDepartmrntIDs(listOfSubdepartment.ToList()).Select(x => x.UserID).ToList();
                    }
                }
                else
                {
                    ViewRequestsFilterDTO.ListUsers = UnitOfWork.UserBLL.GetSubDepartmentByDepartmrntIDs(ViewRequestsFilterDTO.ListSubDepartment).Select(x => x.UserID).ToList();

                }
            }

            List<Request> Result = new List<Entities.Request>();
            if (ViewRequestsFilterDTO.StatusID == (int)RequestStatus.Approved)
            {
                Result = UnitOfWork.RequestBLL.HRApprovedRequests(ViewRequestsFilterDTO);

            }
            else if (ViewRequestsFilterDTO.StatusID == (int)RequestStatus.Rejected)
            {
                Result = UnitOfWork.RequestBLL.HRRejectedRequests(ViewRequestsFilterDTO);

            }




            LocalReport lr = new LocalReport();
                string path = System.Web.Hosting.HostingEnvironment.MapPath("/Reports/HrReport/HrReport.rdlc");
                if (System.IO.File.Exists(path))
                {
                    lr.ReportPath = path;
                }
                else
                {
                    //  return null;
                }
                lr.ReportPath = path;
            //VacationRequest_Result vacationRequest_Result = new VacationRequest_Result();
            HRReportDataSet data = new HRReportDataSet();



            List<ViewRequestDTO> ViewRequestList = new List<ViewRequestDTO>();
            foreach (var item in Result.ToList())
            {
                string Unit = ((StaticEnums.LeaveDurationUnit)Enum.Parse(typeof(StaticEnums.LeaveDurationUnit), item.LeaveDurationUnitFK.ToString())).ToString();
                string UserName = UnitOfWork.UserBLL.Find(x => x.UserID == item.UserFK).FirstOrDefault()?.UserName;

                string Status = ((StaticEnums.RequestStatus)Enum.Parse(typeof(StaticEnums.RequestStatus), item.RequesStatusFK.ToString())).ToString();
                string LeaveType = UnitOfWork.LeaveTypeBLL.Find(x => x.LeaveTypeID == item.LeaveTypeFK).FirstOrDefault()?.LeaveTypeName;

                data.Requests.AddRequestsRow(
                     Status,
                    UserName,
                    item.RequestID.ToString(), LeaveType,
                    item.RequestDuration.ToString(), item.CreationDate.ToString(),
                   item.DurationFrom.ToString(), item.DurationTo.ToString(), item.BackToWork.ToString()
                );
            }








           // "das","asdas","asdas","sadasd","asd","sdas","sdas","asds","ads");
                DataTable RequestsTable = data.Tables["Requests"];
            ReportDataSource rd = new ReportDataSource("DataSet1", RequestsTable);

            lr.DataSources.Add(rd);
                string reportType = "PDF";
                string mimeType;
                string encoding;
                string fileNameExtension;
                string deviceInfo =

                "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>8.5in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>1in</MarginLeft>" +
                "  <MarginRight>1in</MarginRight>" +
                "  <MarginBottom>0.5in</MarginBottom>" +
                "</DeviceInfo>";

                Warning[] warnings;
                string[] streams;
            
                byte[] ResponseStream = lr.Render(
                        reportType,
                        deviceInfo,
                        out mimeType,
                        out encoding,
                        out fileNameExtension,
                        out streams,
                        out warnings);
                HttpResponseMessage apiResponse;
                apiResponse = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(ResponseStream)
                    
                };
                apiResponse.Content.Headers.ContentLength = ResponseStream.Length;
                apiResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                apiResponse.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = String.Format("LeaveRequest_" + "_" + DateTime.Now.ToShortDateString().ToString() + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + ".pdf")
                };

                return apiResponse;
         
        }

        //[AcceptVerbs("Post")]
        //public HttpResponseMessage ExtractHRReportPDF(HRViewRequestsFilterDTO ViewRequestsFilterDTO)
        //{
        //    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);

        //    if (ViewRequestsFilterDTO.ListUsers.Count == 0)
        //    {
        //        if (ViewRequestsFilterDTO.ListSubDepartment.Count == 0)
        //        {
        //            if (ViewRequestsFilterDTO.ListDepartment.Count() == 0)
        //            {
        //                ViewRequestsFilterDTO.ListUsers = UnitOfWork.UserBLL.GetAll().Select(x => x.UserID).ToList();
        //            }
        //            else
        //            {

        //                var listOfSubdepartment = UnitOfWork.SubDepartmentBLL.GetSubDepartmentByDepartmrntIDs(ViewRequestsFilterDTO.ListDepartment).Select(x => x.SubDepartmentID);
        //                ViewRequestsFilterDTO.ListUsers = UnitOfWork.UserBLL.GetSubDepartmentByDepartmrntIDs(listOfSubdepartment.ToList()).Select(x => x.UserID).ToList();
        //            }
        //        }
        //        else
        //        {
        //            ViewRequestsFilterDTO.ListUsers = UnitOfWork.UserBLL.GetSubDepartmentByDepartmrntIDs(ViewRequestsFilterDTO.ListSubDepartment).Select(x => x.UserID).ToList();

        //        }
        //    }

        //    List<Request> Result = new List<Entities.Request>();
        //    if (ViewRequestsFilterDTO.StatusID == (int)RequestStatus.Approved)
        //    {
        //        Result = UnitOfWork.RequestBLL.HRApprovedRequests(ViewRequestsFilterDTO);

        //    }
        //    else if (ViewRequestsFilterDTO.StatusID == (int)RequestStatus.Rejected)
        //    {
        //        Result = UnitOfWork.RequestBLL.HRRejectedRequests(ViewRequestsFilterDTO);

        //    }


        //    List<ViewRequestDTO> ViewRequestList = new List<ViewRequestDTO>();
        //    foreach (var item in Result.ToList())
        //    {
        //        string Unit = ((StaticEnums.LeaveDurationUnit)Enum.Parse(typeof(StaticEnums.LeaveDurationUnit), item.LeaveDurationUnitFK.ToString())).ToString();
        //        string UserName = UnitOfWork.UserBLL.Find(x => x.UserID == item.UserFK).FirstOrDefault()?.UserName;
        //        ViewRequestList.Add(new ViewRequestDTO
        //        {
        //            RequestID = item.RequestID,
        //            BackToWork = item.BackToWork,
        //            CreationDate = item.CreationDate,
        //            DurationFrom = item.DurationFrom,
        //            RequestDuration = item.RequestDuration,
        //            UserName = UserName,
        //            DurationTo = item.DurationTo,
        //            LeaveType = UnitOfWork.LeaveTypeBLL.Find(x => x.LeaveTypeID == item.LeaveTypeFK).FirstOrDefault().LeaveTypeName,
        //            RequestStatus = UnitOfWork.RequestStatusBLL.Find(x => x.RequestStatusID == item.RequesStatusFK).FirstOrDefault().RequestStatusName,
        //            Unit = Unit
        //        });
        //    }


        //    DataTable dtRequest = new DataTable();
        //    dtRequest.Columns.Add("User");
        //    dtRequest.Columns.Add("Request ID");
        //    dtRequest.Columns.Add("Leave Type");
        //    dtRequest.Columns.Add("Request Duration");
        //    dtRequest.Columns.Add("Creation Date");
        //    dtRequest.Columns.Add("Duration From");
        //    dtRequest.Columns.Add("Duration To");
        //    dtRequest.Columns.Add("Back To Work");
        //    dtRequest.Columns.Add("Request Status");
        //    foreach (var item in ViewRequestList)
        //    {
        //        dtRequest.Rows.Add(item.UserName, item.RequestID, item.LeaveType, item.RequestDuration, item.CreationDate, item.DurationFrom, item.DurationTo, item.BackToWork, item.RequestStatus);
        //    }

        //    byte[] fileBytes = exportpdf(dtRequest).ToArray();
        //    response.Content = new ByteArrayContent(fileBytes);
        //    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
        //    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

        //    return response;
        //}
        [AcceptVerbs("Post")]
        public HttpResponseMessage ExtractHRReportExecl(HRViewRequestsFilterDTO ViewRequestsFilterDTO)
        {
            try
            {

                if (ViewRequestsFilterDTO.ListUsers.Count == 0)
                {
                    if (ViewRequestsFilterDTO.ListSubDepartment.Count == 0)
                    {
                        if (ViewRequestsFilterDTO.ListDepartment.Count() == 0)
                        {
                            ViewRequestsFilterDTO.ListUsers = UnitOfWork.UserBLL.GetAll().Select(x => x.UserID).ToList();
                        }
                        else
                        {

                            var listOfSubdepartment = UnitOfWork.SubDepartmentBLL.GetSubDepartmentByDepartmrntIDs(ViewRequestsFilterDTO.ListDepartment).Select(x => x.SubDepartmentID);
                            ViewRequestsFilterDTO.ListUsers = UnitOfWork.UserBLL.GetSubDepartmentByDepartmrntIDs(listOfSubdepartment.ToList()).Select(x => x.UserID).ToList();
                        }
                    }
                    else
                    {
                        ViewRequestsFilterDTO.ListUsers = UnitOfWork.UserBLL.GetSubDepartmentByDepartmrntIDs(ViewRequestsFilterDTO.ListSubDepartment).Select(x => x.UserID).ToList();

                    }
                }

                List<Request> Result = new List<Entities.Request>();
                if (ViewRequestsFilterDTO.StatusID == (int)RequestStatus.Approved)
                {
                    Result = UnitOfWork.RequestBLL.HRApprovedRequests(ViewRequestsFilterDTO);

                }
                else if (ViewRequestsFilterDTO.StatusID == (int)RequestStatus.Rejected)
                {
                    Result = UnitOfWork.RequestBLL.HRRejectedRequests(ViewRequestsFilterDTO);

                }


                List<ViewRequestDTO> ViewRequestList = new List<ViewRequestDTO>();
                foreach (var item in Result.ToList())
                {
                    try {

                        var LeaveType = UnitOfWork.LeaveTypeBLL.Find(x => x.LeaveTypeID == item.LeaveTypeFK).FirstOrDefault();

                        DailyAttendanceDTO Times = new DailyAttendanceDTO();
                        if (item.PartialDurationUnitFK !=null|| item.LeaveDurationUnitFK== (int)LeaveDurationUnit.Hours)
                        {
                            TimeAttendanceParametersDTO TimeAttendanceParameters = new TimeAttendanceParametersDTO
                            {
                                StartDate = item.DurationFrom.ToString("dd/MM/yyyy"),
                                EndDate = item.DurationTo.ToString("dd/MM/yyyy"),
                                UserID = (int)item.UserFK

                            };
                            Times = UnitOfWork.UserTimeAttendanceBLL.GetAttendance(TimeAttendanceParameters).FirstOrDefault();
                        }
                        else
                        {
                            Times = new DailyAttendanceDTO
                            { FingerPrintIn="",
                             FingerPrintOut=""


                            };

                        }
                        
                        string Unit = ((StaticEnums.LeaveDurationUnit)Enum.Parse(typeof(StaticEnums.LeaveDurationUnit), item.LeaveDurationUnitFK.ToString())).ToString();
                        string UserName = UnitOfWork.UserBLL.Find(x => x.UserID == item.UserFK).FirstOrDefault()?.UserName;
                        ViewRequestList.Add(new ViewRequestDTO
                        {
                            RequestID = item.RequestID,
                            AccessControlID = UnitOfWork.UserBLL.Find(x => x.UserID == item.UserFK).FirstOrDefault()?.AccessControlUserFK,
                            BackToWork = item.BackToWork,
                            CreationDate = item.CreationDate,
                            DurationFrom = item.DurationFrom,
                            RequestDuration = item.RequestDuration,
                            UserName = UserName,
                            DurationTo = item.DurationTo,
                            LeaveType = LeaveType.LeaveTypeName,
                            RequestStatus = UnitOfWork.RequestStatusBLL.Find(x => x.RequestStatusID == item.RequesStatusFK).FirstOrDefault().RequestStatusName,
                            Unit = Unit,
                            PunchOut = Times.FingerPrintOut,
                            PunchIn = Times.FingerPrintIn,
                            PartialDurationUnit = UnitOfWork.LeaveTypePartialDurationUnitDIMBLL.Find(x => x.PartialDurationUnitID ==
                            item.PartialDurationUnitFK).FirstOrDefault()?.PartialDurationUnitName

                        });
                    }
                    catch { }
                    }

                DataTable dtRequest = new DataTable();
                dtRequest.Columns.Add("ID");
                dtRequest.Columns.Add("User");
                dtRequest.Columns.Add("AC ID");
                dtRequest.Columns.Add("Leave Type");
                dtRequest.Columns.Add("Requested Date");
                dtRequest.Columns.Add("Duration From");
                dtRequest.Columns.Add("Duration To");
                dtRequest.Columns.Add("Back To Work");
                dtRequest.Columns.Add("Request Duration");
                dtRequest.Columns.Add("Unit");
                dtRequest.Columns.Add("Partial Duration Unit");
                dtRequest.Columns.Add("PunchIn");
                dtRequest.Columns.Add("PunchOut");
                dtRequest.Columns.Add("Request Status");

                foreach (var item in ViewRequestList)
                {
                    dtRequest.Rows.Add(item.RequestID, item.UserName, item.AccessControlID, item.LeaveType,
                        item.CreationDate, item.DurationFrom, item.DurationTo, item.BackToWork,
                        item.RequestDuration, item.Unit, item.PartialDurationUnit, item.PunchIn, item.PunchOut, item.RequestStatus);
                }

                XLWorkbook wb = new XLWorkbook();

                //  byte[] excelData=new byte[10];

                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                var stream = new MemoryStream();
                wb.AddWorksheet(dtRequest, "Cypress Requests Report");
                wb.SaveAs(stream);
                result.Content = new ByteArrayContent(stream.ToArray());
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-excel");
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "HrReport" + DateTime.Now.ToString() + ".xls"
                };
                return result;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        public byte[] exportpdf(DataTable dtEmployee)
        {

            // creating document object  
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            iTextSharp.text.Rectangle rec = new iTextSharp.text.Rectangle(PageSize.A4);
            rec.BackgroundColor = new BaseColor(System.Drawing.Color.Olive);
            Document doc = new Document(rec);
            doc.SetPageSize(iTextSharp.text.PageSize.A4);
            PdfWriter writer = PdfWriter.GetInstance(doc, ms);
            doc.Open();
            //Adding paragraph for report generated by  
           // Paragraph prgGeneratedBY = new Paragraph();
            Paragraph prgHeading = new Paragraph();

            
         

        

            //Adding  Logo
            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance("https://res.cloudinary.com/dbma3ugxq/image/upload/v1552756269/cypress-logo.png");
            jpg.ScaleAbsolute(150f,60f);
            jpg.Alignment = Element.ALIGN_LEFT;
            prgHeading.Add(jpg);

            //Adding  Header Line

            Paragraph prgHeaderText = new Paragraph();
            BaseFont BaseHeaderTextStyle = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font AdvancedHeaderTextStyle = new iTextSharp.text.Font(BaseHeaderTextStyle, 15, 2, iTextSharp.text.BaseColor.RED);
            prgHeaderText.Alignment = Element.ALIGN_CENTER;
            prgHeaderText.Add(new Chunk("CYPRESS REQUESTS REPORT", AdvancedHeaderTextStyle));
            prgHeading.Add(prgHeaderText);

            //Adding  Date
            Paragraph prgDate = new Paragraph();
            BaseFont BaseDateStyle = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font AdvancedDateStyle = new iTextSharp.text.Font(BaseDateStyle, 8, 2, iTextSharp.text.BaseColor.BLUE);
            prgDate.Alignment = Element.ALIGN_RIGHT;
            prgDate.Add(new Chunk("Generattion Date : " + DateTime.Now.ToShortDateString()+"\n", AdvancedDateStyle));
            prgHeading.Add(prgDate);


            //Adding a line  
            Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, iTextSharp.text.BaseColor.BLACK, Element.ALIGN_LEFT, 1)));

            prgHeading.Add(p);
            prgHeading.SpacingAfter = 10;
            doc.Add(prgHeading);

        

            //Adding  PdfPTable  

            PdfPTable table = new PdfPTable(dtEmployee.Columns.Count);

            for (int i = 0; i < dtEmployee.Columns.Count; i++)
            {
                string cellText = HtmEncode(dtEmployee.Columns[i].ColumnName);
                PdfPCell cell = new PdfPCell();
                BaseFont CellbtnAuthor = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

                cell.Phrase = new Phrase(cellText, new iTextSharp.text.Font(CellbtnAuthor, 8, 2, new BaseColor(System.Drawing.ColorTranslator.FromHtml("#000000"))));
                cell.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#C8C8C8"));
               // cell.Phrase = new Phrase(cellText, new Font(Font.FontFamily.TIMES_ROMAN, 10, 1, new BaseColor(grdStudent.HeaderStyle.ForeColor)));  
                //cell.BackgroundColor = new BaseColor(grdStudent.HeaderStyle.BackColor);  
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.PaddingBottom = 1;
                table.AddCell(cell);
            }

            //writing table Data  
            for (int i = 0; i < dtEmployee.Rows.Count; i++)
            {
                for (int j = 0; j < dtEmployee.Columns.Count; j++)
                {
                    PdfPCell cell = new PdfPCell();
                    cell.Phrase = new Phrase(dtEmployee.Rows[i][j].ToString(), new iTextSharp.text.Font(BaseDateStyle,8, 2, new BaseColor(System.Drawing.ColorTranslator.FromHtml("#000000"))));

                    table.AddCell(cell);
                }
            }

            doc.Add(table);
            doc.Close();

            byte[] result = ms.ToArray();
            return result;

        }
        public string HtmEncode( string htmlDecodedString)
        {
            if (htmlDecodedString.Length > 0)
            {
                return System.Net.WebUtility.HtmlEncode(htmlDecodedString);
            }
            else
            {
                return htmlDecodedString;
            }
        }
            // GET api/values/
        public HttpResponseMessage GetAll()
        {

            //DataTable Dt= GetData();  Getdata from database in thr form d
            //Generate  html table sturcture
            string str = "<table><tr> <th>colume1</th></tr><tr> <td>abc</td> </tr></table>";
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StringContent(str);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-excel");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment"); //attachment will force download
            result.Content.Headers.ContentDisposition.FileName = "data.xlsx";
            return result;

        }

        public ResultViewModel AddNewRequestDetails(RequestDetailsDTO RequestDetailsDTO)
        {
            try
            {
                UnitOfWork.RequestDetailsBLL.AddNewRequestDetails(RequestDetailsDTO);
                return new ResultViewModel
                {
                    Data = null,
                    Message = "Success",
                    Success = true

                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false

                };
            }
        }
        [HttpGet]
        public ResultViewModel GetAllRequestDetails(int RequestID)
        {
            try
            {

                return new ResultViewModel
                {
                    Data = UnitOfWork.RequestDetailsBLL.GetAllRequestDetails(RequestID),
                    Message = "Success",
                    Success = true

                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false

                };
            }
        }


        public ResultViewModel GetAllRequestStatusTypes()
        {


            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.RequesStatusBLL.GetAllRequesStatus(),
                    Message = "Success",
                    Success = true

                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false

                };
            }


        }

        public ResultViewModel GetAllRequestStatusTypes(int LoggedUserID)
        {


            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.RequesStatusBLL.GetAllRequesStatus(LoggedUserID),
                    Message = "Success",
                    Success = true

                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false

                };
            }


        }

        public ResultViewModel GetALLRequestAttachment(int RequestID)
        {


            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.RequestBLL.GetALLRequestAttachment(RequestID),
                    Message = "Success",
                    Success = true

                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false

                };
            }


        }
        public  string HtmDecode( string htmlEncodedString)
        {
            if (htmlEncodedString.Length > 0)
            {
                return System.Net.WebUtility.HtmlDecode(htmlEncodedString);
            }
            else
            {
                return htmlEncodedString;
            }
        }

        [HttpPost]
        //rejection Reason report
        public ResultViewModel GetAllRejectionReason(RejectionReasonInputDTO rejectionReasonInput)
        {

            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.RequestBLL.GetAllRejectionReason(rejectionReasonInput),
                    Message = "Success",
                    Success = true

                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false

                };
            }

        }
        [AcceptVerbs("Post")]
        public HttpResponseMessage GetAllRejectionReasonExcel(RejectionReasonInputDTO rejectionReasonInput)
        {
            List<RejectionReasonOutputDTO> rejectionReasonOutputDTOList = UnitOfWork.RequestBLL.GetAllRejectionReason(rejectionReasonInput);
            DataTable dtRequest = new DataTable();
            dtRequest.Columns.Add("Request ID");
            dtRequest.Columns.Add("User Name");
            dtRequest.Columns.Add("AC ID");
            dtRequest.Columns.Add("Request Create Date");
            dtRequest.Columns.Add("Leave Type Name");
            dtRequest.Columns.Add("Request Start Date");
            dtRequest.Columns.Add("Request End Date");
            dtRequest.Columns.Add("Request Duration");
            dtRequest.Columns.Add("Request Duration Unit ");
            dtRequest.Columns.Add("Manager Name");
            dtRequest.Columns.Add("Rejection Reason");
            foreach (var item in rejectionReasonOutputDTOList)
            {
                dtRequest.Rows.Add(item.RequestID, item.UserName, item.AccessControlID, item.RequestCreateDate, item.LeaveTypeName, item.RequestStartDate,item.RequestEndDate,item.RequestDuration,item.RequestDurationUnitName , item.ManagerName, item.RejectionReasonComment);
            }

            XLWorkbook wb = new XLWorkbook();
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new MemoryStream();
            wb.AddWorksheet(dtRequest, "Cypress RejectionReasons");
            wb.SaveAs(stream);
            result.Content = new ByteArrayContent(stream.ToArray());
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-excel");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "RejectionReasons" + DateTime.Now.ToString() + ".xls"
            };
            
            return result;
        }
        //------------------------------------------------------------------------------------------------
         [HttpPost]
        //deleted Reason report
        public ResultViewModel GetAllDeletedReason(DeletedReasonInputDTO deletedReasonInput)
        {

            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.RequestBLL.GetAllDeletedReason(deletedReasonInput),
                    Message = "Success",
                    Success = true

                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false

                };
            }

        }
        [AcceptVerbs("Post")]
        public HttpResponseMessage GetAllDeletedReasonExcel(DeletedReasonInputDTO deletedReasonInput)
        {
            List<DeletedReasonOutputDTO> rejectionReasonOutputDTOList = UnitOfWork.RequestBLL.GetAllDeletedReason(deletedReasonInput);
            DataTable dtRequest = new DataTable();
            dtRequest.Columns.Add("Request ID");
            dtRequest.Columns.Add("User Name");
            dtRequest.Columns.Add("AC ID");
            dtRequest.Columns.Add("Request Create Date");
            dtRequest.Columns.Add("Leave Type Name");
            dtRequest.Columns.Add("Request Start Date");
            dtRequest.Columns.Add("Request End Date");
            dtRequest.Columns.Add("Request Duration");
            dtRequest.Columns.Add("Request Duration Unit ");
            dtRequest.Columns.Add("Deleted By");
            dtRequest.Columns.Add("Deleted Reason");
            foreach (var item in rejectionReasonOutputDTOList)
            {
                dtRequest.Rows.Add(item.RequestID, item.UserName, item.AccessControlID, item.RequestCreateDate, item.LeaveTypeName, item.RequestStartDate, item.RequestEndDate, item.RequestDuration, item.RequestDurationUnitName, item.ManagerName, item.DeletedReasonComment);
            }

            XLWorkbook wb = new XLWorkbook();
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new MemoryStream();
            wb.AddWorksheet(dtRequest, "Cypress DeletedReasons");
            wb.SaveAs(stream);
            result.Content = new ByteArrayContent(stream.ToArray());
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-excel");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "DeletedReasons" + DateTime.Now.ToString() + ".xls"
            };

            return result;
        }
        public ResultViewModel getAllDeletedReasonsForSpecificRequestById(int reqId)
        {
            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.RequestBLL.getAllDeletedReasonsForSpecificRequestById(reqId),
                    Message = "sucess",
                    Success = true,

                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false

                };
            }
        }
        //------------------------------------------------------------------------------------------------
        [HttpPost]
        //Partial deleted Reason report
        public ResultViewModel GetAllPartialDeletedReason(DeletedReasonInputDTO PartialDeletedReasonInput)
        {

            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.RequestBLL.GetAllPartialDeletedReason(PartialDeletedReasonInput),
                    Message = "Success",
                    Success = true

                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false

                };
            }

        }
        [AcceptVerbs("Post")]
        public HttpResponseMessage GetAllPartialDeletedReasonExcel(DeletedReasonInputDTO PartialDeletedReasonInput)
        {
            List<DeletedReasonOutputDTO> rejectionReasonOutputDTOList = UnitOfWork.RequestBLL.GetAllPartialDeletedReason(PartialDeletedReasonInput);
            DataTable dtRequest = new DataTable();
            dtRequest.Columns.Add("Request ID");
            dtRequest.Columns.Add("User Name");
            dtRequest.Columns.Add("AC ID");
            dtRequest.Columns.Add("Request Create Date");
            dtRequest.Columns.Add("Leave Type Name");
            dtRequest.Columns.Add("Request Start Date");
            dtRequest.Columns.Add("Request End Date");
            dtRequest.Columns.Add("Request Duration");
            dtRequest.Columns.Add("Request Duration Unit ");
            dtRequest.Columns.Add("Deleted By");
            dtRequest.Columns.Add("Deleted Reason");
            foreach (var item in rejectionReasonOutputDTOList)
            {
               dtRequest.Rows.Add(item.RequestID, item.UserName, item.AccessControlID, item.RequestCreateDate, item.LeaveTypeName, item.RequestStartDate, item.RequestEndDate, item.RequestDuration, item.RequestDurationUnitName, item.ManagerName, item.DeletedReasonComment);
            }

            XLWorkbook wb = new XLWorkbook();
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new MemoryStream();
            wb.AddWorksheet(dtRequest, "Cypress DeletedReasons");
            wb.SaveAs(stream);
            result.Content = new ByteArrayContent(stream.ToArray());
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-excel");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "PartialDeletedReasons" + DateTime.Now.ToString() + ".xls"
            };

            return result;
        }
        public ResultViewModel getAllPartialDeletedReasonsForSpecificRequestById(int reqId)
        {
            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.RequestBLL.getAllPartialDeletedReasonsForSpecificRequestById(reqId),
                    Message = "sucess",
                    Success = true,

                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false

                };
            }
        }
        //------------------------------------------------------------------------------------------------
        //pending Request report
        [HttpPost]
        public ResultViewModel GetAllPendingRequest(PendingRequestInputDTO pendingInput)
        {

            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.RequestBLL.GetAllLateRequestMail(pendingInput),
                    Message = "Success",
                    Success = true

                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false

                };
            }

        }
        [AcceptVerbs("Post")]
        public HttpResponseMessage GetAllPendingRequestExcel(PendingRequestInputDTO pendingInput)
        {
            List<PendingRequestOutputDTO> rejectionReasonOutputDTOList = UnitOfWork.RequestBLL.GetAllLateRequestMail(pendingInput);
            DataTable dtRequest = new DataTable();
            dtRequest.Columns.Add("Request ID");
            dtRequest.Columns.Add("User Name");
            dtRequest.Columns.Add("AC ID");
            dtRequest.Columns.Add("Request Create Date");
            dtRequest.Columns.Add("Leave Type Name");
            dtRequest.Columns.Add("Request Start Date");
            dtRequest.Columns.Add("Request End Date");
            dtRequest.Columns.Add("Request Duration");
            dtRequest.Columns.Add("Request Duration Unit ");
            dtRequest.Columns.Add("Manager Name");
            dtRequest.Columns.Add("Pending From");
            foreach (var item in rejectionReasonOutputDTOList)
            {
                dtRequest.Rows.Add(item.RequestID, item.UserName,item.AccessControlID ,item.RequestCreateDate, item.LeaveTypeName, item.RequestStartDate, item.RequestEndDate, item.RequestDuration, item.RequestDurationUnitName, item.ManagerName, item.PendingFrom);
            }

            XLWorkbook wb = new XLWorkbook();
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new MemoryStream();
            wb.AddWorksheet(dtRequest, "Cypress PendingMail");
            wb.SaveAs(stream);
            result.Content = new ByteArrayContent(stream.ToArray());
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-excel");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "PendingMail" + DateTime.Now.ToString() + ".xls"
            };

            return result;
        }
        public ResultViewModel GetRequestHandlerRecords(int RequestID)
        {

            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.RequestBLL.GetRequestHandlerRecords(RequestID),
                    Message = "Success",
                    Success = true

                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = "Can not Get Request Handler Data",
                    Success = false

                };
            }

        }
        [HttpPost]
        public ResultViewModel DeletePartialPeriodFromRequest(RequestDeletionDTO RequestDeletion)
        {

            try
            {
                if (RequestDeletion.DeletedDays != null)
                {
                    UnitOfWork.RequestBLL.DeletePartialPeriodFromRequestDays(RequestDeletion);
                }
                else
                {
                    UnitOfWork.RequestBLL.DeletePartialPeriodFromRequestHours(RequestDeletion);


                }
                Exception exOutputSubmit;
                if (UnitOfWork.Submit(out exOutputSubmit))
                {
                    return new ResultViewModel
                    {
                        Data = null,
                        Message = "Success",
                        Success = true

                    };
                }
                else
                {
                    return new ResultViewModel
                    {
                        Data = null,
                        Message = exOutputSubmit.Message,
                        Success = false

                    };
                }


            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.ToString(),
                    Success = false

                };
            }

        }

        [HttpPost]
        public ResultViewModel CancelRequest(RequestCancelDTO RequestCancel)
        {

            try
            {
                UnitOfWork.RequestBLL.CancelRequest(RequestCancel);
                Exception exOutputSubmit;
                if (UnitOfWork.Submit(out exOutputSubmit))
                {
                    return new ResultViewModel
                    {
                        Data = null,
                        Message = "Success",
                        Success = true

                    };
                }
                else
                {
                    return new ResultViewModel
                    {
                        Data = null,
                        Message = exOutputSubmit.Message,
                        Success = false

                    };
                }


            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.ToString(),
                    Success = false

                };
            }

        }
        [HttpGet]
        public ResultViewModel CheckCancelEnable(int RequestID)
        {
            try
            {
                    return new ResultViewModel
                    {
                        Data = UnitOfWork.RequestBLL.CheckCancelEnable(RequestID),
                        Message = "Success",
                        Success = true

                    };
                
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.ToString(),
                    Success = false

                };
            }
        }

        [HttpGet]
        public ResultViewModel GetStartAndEndWorkingTime(int UserID, string RequestDate)
        {
            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.RequestBLL.GetStartAndEndWorkingTime(UserID, RequestDate),
                    Message = "Success",
                    Success = true

                };

            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.ToString(),
                    Success = false

                };
            }
        }

        
        //------------------------------------------------------------------------------------------------
        //check if request is approved 
        [HttpGet]
        public ResultViewModel CheckIfRequestIsApproved(int id)
        {

            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.RequestBLL.CheckIfRequestIsApproved(id),
                    Message = "Success",
                    Success = true

                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = "Can not Get This Request ",
                    Success = false

                };
            }

        }
        //-----------------------------------------------------------------



    }

} 


