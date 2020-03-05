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
using EmaTours.DTOs.Business;

namespace EmaTours.API.Controllers
{
    public class ClientTripsController : BaseController
    {
        public ResultViewModel UserAssignTripRequest(ClientTripDTO ClientTrip)
        {

            ResultViewModel Result = new ResultViewModel();
            Result.Success = true;
            try
            {
                UnitOfWork.ClientTripRequestsBLL.UserAssignTripRequest(ClientTrip);

                Result = new ResultViewModel
                {
                    Data = null,
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

        public ResultViewModel UserCloseTripRequest(ClientTripDTO ClientTrip)
        {

            ResultViewModel Result = new ResultViewModel();
            Result.Success = true;
            try
            {
                UnitOfWork.ClientTripRequestsBLL.UserCloseTripRequest(ClientTrip);

                Result = new ResultViewModel
                {
                    Data = null,
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

        public ResultViewModel GetAllTripRequest(ClientTripDTO ClientTrip)
        {

            ResultViewModel Result = new ResultViewModel();
            Result.Success = true;
            try
            {
                Result = new ResultViewModel
                {
                    Data = UnitOfWork.ClientTripRequestsBLL.GetAllTripRequest(),
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

        public ResultViewModel StartNewTrip(ClientTripDTO ClientTrip)
        {

            ResultViewModel Result = new ResultViewModel();
            Result.Success = true;
            try
            {
              var ClientTripRequest=  UnitOfWork.SharedClientTripsBLL.StartNewTrip(ClientTrip);
                UnitOfWork.Submit();
                ClientTrip.ClientTripRequestID = ClientTripRequest.ClientTripRequestID;
                Result = new ResultViewModel
                {
                    Data = ClientTrip,
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