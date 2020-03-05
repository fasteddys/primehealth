using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using EmaTours.Entities;
using static EmaTours.BLL.StaticData.StaticEnums;
using EmaTours.DTOs;
using System.Reflection;
using EmaTours.Utilities;
using EmaTours.BLL.Logic.Tables;
using EmaTours.DTOs.Business;

namespace EmaTours.API.Controllers
{
    public class PickUpController : BaseController
    {

        public ResultViewModel AddPickUpRequest(ClientPickUpDTO ClientPickUp)
        {
 
            ResultViewModel Result = new ResultViewModel();
            Result.Success = true;
            try
            {
                ClientPickUpRequest ClientPickUpRequest= UnitOfWork.SharedClientPickUp.BookPickUp(ClientPickUp);
                UnitOfWork.Submit();
                ClientPickUp.PickUpRequestID = ClientPickUpRequest.ClientPickUpRequestID;
                Result =new ResultViewModel
                { Data= ClientPickUp,
                 Message="Success",
                  Success=true


                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateException(ex, MethodBase.GetCurrentMethod().Name);
                Result.Message = ex.Message;
                Result.Success = false;

            }
            return Result;

        }


        public ResultViewModel GetPickUpRequest(int ClientPickUpID)
        {

            ResultViewModel Result = new ResultViewModel();
            Result.Success = true;
            try
            {
                Result = new ResultViewModel
                {
                    Data = UnitOfWork.SharedClientPickUp.GetPickUp(ClientPickUpID),
                    Message = "Success",
                    Success = true


                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateException(ex, MethodBase.GetCurrentMethod().Name);
                Result.Message = ex.Message;
                Result.Success = false;

            }
            return Result;

        }

        [HttpPost]
        public ResultViewModel EditPickUpRequest(ClientPickUpDTO ClientPickUp)
        {

            ResultViewModel Result = new ResultViewModel();
            Result.Success = true;
            try
            {
                UnitOfWork.SharedClientPickUp.EditPickUp(ClientPickUp);
                UnitOfWork.Submit();

                Result = new ResultViewModel
                {
                    Data = ClientPickUp,
                    Message = "Success",
                    Success = true


                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateException(ex, MethodBase.GetCurrentMethod().Name);
                Result.Message = ex.Message;
                Result.Success = false;

            }
            return Result;

        }
    }
}