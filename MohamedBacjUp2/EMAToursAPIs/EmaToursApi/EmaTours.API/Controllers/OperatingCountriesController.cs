﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using EmaTours.Entities;
using static EmaTours.BLL.StaticData.StaticEnums;
using EmaTours.DTOs;
using System.Reflection;
using EmaTours.Utilities;

namespace EmaTours.API.Controllers
{
    public class OperatingCountriesController : BaseController
    {
        public ResultViewModel GetAllCountries(int LanguageFK)
        {

            ResultViewModel Result = new ResultViewModel();
            Result.Success = true;
            try
            {
                Result = new ResultViewModel { Data = UnitOfWork.OperatingCountriesBLL.GetAllOperatingCountries( LanguageFK),
                     Message="Success",
                      Success=true
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
        public ResultViewModel GetCountryNameByID(int CountryID)
        {

            ResultViewModel Result = new ResultViewModel();
            Result.Success = true;
            try
            {
                Result = new ResultViewModel
                {
                    Data = UnitOfWork.OperatingCountriesBLL.Find(x=>x.CountryID== CountryID)?.FirstOrDefault()?.ConutryName,
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