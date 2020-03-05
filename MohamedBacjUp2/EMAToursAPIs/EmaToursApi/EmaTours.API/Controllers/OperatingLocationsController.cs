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
using EmaTours.API.Models;

namespace EmaTours.API.Controllers
{
    [JsonRequestBehavior]
    public class OperatingLocationsController : BaseController
    {
        [HttpPost]
        [JsonRequestBehavior]
        public ResultViewModel GetAllOperatingLocations(OperatingCountryDTO OperatingCountry)
        {

            ResultViewModel Result = new ResultViewModel();
            Result.Success = true;
            try
            {
                Result = new ResultViewModel
                {
                    Data = UnitOfWork.OperatingLocationsBLL.GetAllOperatingLocations( OperatingCountry),
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
        public ResultViewModel GetLocationNameByID(int LocationID)
        {

            ResultViewModel Result = new ResultViewModel();
            Result.Success = true;
            try
            {
                Result = new ResultViewModel
                {
                    Data = UnitOfWork.OperatingLocationsBLL.Find(x => x.LocationID == LocationID)?.FirstOrDefault()?.LocationName,
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

        public void AddOperatingLocation()
        {

        }
        public void UpdateOperatingLocation()
        {

        }

        public void DeActivateOperatingLocation()
        {

        }





    }
}