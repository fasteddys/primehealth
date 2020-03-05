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
    public class ClientController : BaseController
    {

        public ResultViewModel SignUp(ClientDTO ClientDTO)
        {
 
            ResultViewModel Result = new ResultViewModel();
            Result.Success = true;
            try
            {
                Client Client = UnitOfWork.ClientsBLL.SignUp(ClientDTO);
                UnitOfWork.Submit();

                ClientDTO ReClientData = new ClientDTO{
                    ClientID = Client.ClientID,
                    ClientName = Client.ClientName,
                    NotificationMethodID = Client.ClientPreferredNotificationMethodFK,
                    ClientEmail = Client.ClientEmail,
                    ClientPassport = Client.ClientPassportNumber,
                    ClientPhone = Client.ClientPhone,
                    CountryID = Client.ClientCountryFK
                };

                Result =new ResultViewModel
                {
                    Data = ReClientData,
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

        public ResultViewModel CheckIfClientSignUp(ClientDTO ClientDTO)
        {

            ResultViewModel Result = new ResultViewModel();
            Result.Success = true;
            try
            {
                

                Result = new ResultViewModel
                {
                    Data = UnitOfWork.ClientsBLL.CheckIfClientSignUp(ClientDTO),
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
        public ResultViewModel GetAllClientData(int ClientID)
        {

            ResultViewModel Result = new ResultViewModel();
            Result.Success = true;
            try
            {


                Result = new ResultViewModel
                {
                    Data = UnitOfWork.ClientsBLL.GetAllClientData(ClientID),
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
        public ResultViewModel EditClientData(ClientDTO ClientDTO)
        {
            ResultViewModel Result = new ResultViewModel();

            UnitOfWork.ClientsBLL.EditClientData(ClientDTO);
            UnitOfWork.Submit();
            //Result.Success = true;
            try
            {


                Result = new ResultViewModel
                {
                    Data = ClientDTO,
                    Message = "Successfully Edit Your Data",
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
        public ResultViewModel AddFeedBack(List<FeedBackDTO> FeedBack)
        {

            ResultViewModel Result = new ResultViewModel();
            Result.Success = true;
            try
            {

                UnitOfWork.ClientServicesRatingBLL.AddFeedBack(FeedBack);
                UnitOfWork.Submit();
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




    }
}