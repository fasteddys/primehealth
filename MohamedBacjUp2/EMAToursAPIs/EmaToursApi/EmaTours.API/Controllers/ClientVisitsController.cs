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
    public class ClientVisitsController : BaseController
    {

        public ResultViewModel GetAllClientVisits(int ClientID)
        {

            ResultViewModel Result = new ResultViewModel();
            Result.Success = true;
            try
            {
                Result = new ResultViewModel
                {
                    Data = UnitOfWork.ClientVisitsBLL.GetAllClientVisits(ClientID),
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
        public ResultViewModel GetClientVisitDetails(int ClientVisitID)
        {

            ResultViewModel Result = new ResultViewModel();
            Result.Success = true;
            try
            {
                Result = new ResultViewModel
                {
                    Data = UnitOfWork.SharedClientVisitsBLL.GetClientVisitDetails(ClientVisitID),
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



        public ResultViewModel StartNewVisits(VisitsDTO ClientVisitDTO)
        {

            ResultViewModel Result = new ResultViewModel();
            Result.Success = true;
            try
            {
                ClientVisit ClientVisit= UnitOfWork.SharedClientVisitsBLL.StartNewVisits(ClientVisitDTO);
                UnitOfWork.Submit();

                Result = new ResultViewModel
                {
                    Data = ClientVisit.ClientVisitID,
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
        public ResultViewModel EditVisit(VisitsDTO ClientVisitDTO)
        {

            ResultViewModel Result = new ResultViewModel();
            Result.Success = true;
            try
            {
                UnitOfWork.SharedClientVisitsBLL.EditVisit(ClientVisitDTO);
                UnitOfWork.Submit();

                Result = new ResultViewModel
                {
                    Data = ClientVisitDTO,
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