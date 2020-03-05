using EmaTours.BLL;
using EmaTours.DTOs;
using EmaTours.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace EmaTours.API.Controllers
{
    public class HotelController : BaseController
    {
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        // GET: Hotel
        public ResultViewModel SearchHotel(int LanguageFK = 0, int OperatingLocation = 0,int OperatingCountry=0)
        {

            ResultViewModel Result = new ResultViewModel();
            Result.Success = true;
            try
            {
                Result = new ResultViewModel
                {
                    Data = UnitOfWork.HotelsBLL.SearchHotels(OperatingCountryId:OperatingCountry,LanguageID:LanguageFK,OperatingLocationId:OperatingLocation),
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