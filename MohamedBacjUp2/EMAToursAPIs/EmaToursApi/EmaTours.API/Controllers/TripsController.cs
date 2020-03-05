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
    public class TripsController : BaseController
    {



        public ResultViewModel GetAllTripsData(int LanagugeID)
        {

            ResultViewModel Result = new ResultViewModel();
            Result.Success = true;
            try
            {
                Result = new ResultViewModel
                {
                    Data = UnitOfWork.TripsBLL.GetAllTripsData(LanagugeID),
                    Message = "Success",
                    Success = true
                };

            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateEmaToursException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "EmaToursApplication-API");
                Result.Message = ex.Message;
                Result.Success = false;

            }
            return Result;

        }
        public ResultViewModel GetAllTripsByLocationData(int LanagugeID,int LocationID)
        {

            ResultViewModel Result = new ResultViewModel();
            Result.Success = true;
            try
            {
                Result = new ResultViewModel
                {
                    Data = UnitOfWork.TripsBLL.GetAllTripsDataByLocation(LanagugeID, LocationID),
                    Message = "Success",
                    Success = true
                };

            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateEmaToursException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "EmaToursApplication-API");
                Result.Message = ex.Message;
                Result.Success = false;

            }
            return Result;

        }



        [HttpGet]
        public ResultViewModel GetTripData(int TripID, int languageID)
        {

            ResultViewModel Result = new ResultViewModel();
            Result.Success = true;
            try
            {
                Result = new ResultViewModel
                {
                    Data = UnitOfWork.TripsBLL.GetTripData(TripID, languageID),
                    Message = "Success",
                    Success = true
                };

            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateEmaToursException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "EmaToursApplication-API");
                Result.Message = ex.Message;
                Result.Success = false;

            }
            return Result;

        }

        

        //public ResultViewModel AddNewTrip(TripDTO TripDTO)
        //{

        //    ResultViewModel Result = new ResultViewModel();
        //    Result.Success = true;
        //    try
        //    {
        //        UnitOfWork.TripsBLL.AddNewTrip(TripDTO);
        //        Result = new ResultViewModel
        //        {
        //            Data =null,
        //            Message = "Success",
        //            Success = true
        //        };

        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionHandlerConstants.CreateEmaToursException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "EmaToursApplication-API");
        //        Result.Message = ex.Message;
        //        Result.Success = false;

        //    }
        //    return Result;

        //}


        public ResultViewModel DeactivateTrip(int TripID)
        {

            ResultViewModel Result = new ResultViewModel();
            Result.Success = true;
            try
            {
                UnitOfWork.TripsBLL.DeactivateTrip(TripID);
                Result = new ResultViewModel
                {
                    Data = null,
                    Message = "Success",
                    Success = true
                };

            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateEmaToursException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "EmaToursApplication-API");
                Result.Message = ex.Message;
                Result.Success = false;

            }
            return Result;

        }


        //public ResultViewModel EditTrip(TripDTO TripDTO)
        //{

        //    ResultViewModel Result = new ResultViewModel();
        //    Result.Success = true;
        //    try
        //    {
        //        UnitOfWork.TripsBLL.EditTrip(TripDTO);
        //        Result = new ResultViewModel
        //        {
        //            Data = null,
        //            Message = "Success",
        //            Success = true
        //        };

        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionHandlerConstants.CreateEmaToursException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "EmaToursApplication-API");
        //        Result.Message = ex.Message;
        //        Result.Success = false;

        //    }
        //    return Result;

        //}



        public ResultViewModel AddNewPhotoToTrip(PhotoDTO Photo)
        {

            ResultViewModel Result = new ResultViewModel();
            Result.Success = true;
            try
            {
                UnitOfWork.TripsBLL.AddNewPhotoToTrip(Photo);
                Result = new ResultViewModel
                {
                    Data = null,
                    Message = "Success",
                    Success = true
                };

            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateEmaToursException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "EmaToursApplication-API");
                Result.Message = ex.Message;
                Result.Success = false;

            }
            return Result;

        }



















    }
}