using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using HRMS.Entities;
using HRMS.DTOs.Business;
using static HRMS.BLL.StaticData.StaticEnums;
using HRMS.DTOs;
using System.Reflection;
using HRMS.Utilities;

namespace HRMS.API.Controllers
{
    public class ApprovalFlowController : BaseController
    { 

        public ResultViewModel GetStageOfVication()
        {
            Request Leave = new Request();
            User User = new User();
            ResultViewModel Result = new ResultViewModel();
            Result.Success = true;
            try
            {
                Result.Data = UnitOfWork.ApprovalFlowBLL.GetManagerApprovalFlow(Leave, User);
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");
                Result.Message = ex.Message;
                Result.Success = false;

            }
            return Result;

        }


        [System.Web.Http.HttpPost]
        public ResultViewModel AddNewApprovalFlow(ApprovalFlowContainerDTO ApprovalFlowContanerDTO)
        {
            ResultViewModel Result = new ResultViewModel();
            Exception exOutputSubmit;

            try
            {



                ApprovalFlow NewApprovalFlow = new ApprovalFlow();
                NewApprovalFlow.ApprovalFlowName = ApprovalFlowContanerDTO.ApprovalFlowName;
                    UnitOfWork.ApprovalFlowBLL.Add(NewApprovalFlow);                 
                for (int i = 0; i < ApprovalFlowContanerDTO.ApprovalFlow.Count(); i++)
                {
                    if (i == 0)
                    {
                        UnitOfWork.ApprovalFlowBLL.AddNewApprovalFlow(NewApprovalFlow, ApprovalFlowContanerDTO.ApprovalFlow[i]);
                    }
                    else
                    {

                        UnitOfWork.ApprovalFlowBLL.AddNewAlternativeFlow(NewApprovalFlow, ApprovalFlowContanerDTO.ApprovalFlow[i]);

                    }

                }



                if(UnitOfWork.Submit(out exOutputSubmit))
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
        public ResultViewModel EditNewApprovalFlow(string ApprovalFlowName, List<ApprovalFlowDTO> ApprovalFlow)
        {
            Exception exOutputSubmit;
            try
            {

                ApprovalFlow NewApprovalFlow = new ApprovalFlow();
                NewApprovalFlow.ApprovalFlowName = ApprovalFlowName;
                UnitOfWork.ApprovalFlowBLL.Add(NewApprovalFlow);
                for (int i = 0; i < ApprovalFlow.Count(); i++)
                {
                    if (i == 0)
                    {
                        UnitOfWork.ApprovalFlowBLL.EditApprovalFlow(NewApprovalFlow, ApprovalFlow[i]);

                    }
                    else
                    {

                        UnitOfWork.ApprovalFlowBLL.AddNewAlternativeFlow(NewApprovalFlow, ApprovalFlow[i]);

                    }

                }



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
        [HttpPost]
        public ResultViewModel AddNewApprovalFlowS(string ApprovalFlowName, List<ApprovalFlowDTO> ApprovalFlowDataDTO)
        {

            return new ResultViewModel
            {
                Data = null,
                Message = "",
                Success = true
            };
        }


        public ResultViewModel GetApprovalFlowByID(int FlowID)
        {
            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.ApprovalFlowBLL.GetApprovalFlowDetails(FlowID)
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
        public ResultViewModel GetUserApprovedByManager(int UserIDOfManager)
        {
            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.ApprovalFlowBLL.GetUserApprovedByManager(UserIDOfManager)
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
        [HttpGet]
        public ResultViewModel CheckUserApprovedByManager(int UserIDOfManager,int UserID)
        {
            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.ApprovalFlowBLL.CheckUserApprovedByManager(UserIDOfManager, UserID),
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

        
        public ResultViewModel GetApprovalFlowDetails(int ApprovalFlowID)
        {
            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.ApprovalFlowBLL.GetApprovalFlowDetails(ApprovalFlowID),
                    Message = "Success",
                    Success = true
                };
            }
            catch(Exception ex)
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

        public ResultViewModel GetAllApprovalFlow()

        {
            try
            {
                return new ResultViewModel
                {
                    Data = UnitOfWork.ApprovalFlowBLL.GetApprovalFlowView().ToList(),
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
        public ResultViewModel CheckIfApprovalFlowVaildForUser(AppovalFlowAddUserValidationDTO appovalFlowAddUserValidationDTO)
        {
            ResultViewModel Result = new ResultViewModel();

            try
            {
                UnitOfWork.ApprovalFlowBLL.CheckIfApprovalFlowVaildForUser(appovalFlowAddUserValidationDTO.UserID, appovalFlowAddUserValidationDTO.ApprovalFlowID);

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
                    Data = false,
                    Message = ex.Message,
                    Success = false

                };
            }
        }
    }
}