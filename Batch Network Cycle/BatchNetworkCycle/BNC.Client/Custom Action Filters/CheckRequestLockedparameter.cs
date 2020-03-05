using BNC.BLL.Logic.Tables;
using BNC.BLL.UnitOfWork;
using System;
using BNC.DAL.Factory;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BNC.DTOs.Business;
using System.Web.Routing;
using BNC.BLL.Logic.Shared_Logic;

namespace BNC.Client.Custom_Action_Filters
{
    public class CheckRequestLockedparameter : FilterAttribute, IActionFilter
    {
        private string _RequestParameterName { get; set; }
        private int _EntityTypeID { get; set; }
        private BaseController BaseController;
        public CheckRequestLockedparameter(string RequestID,int EntityTypeID)
        {
            BaseController = new BaseController();
            _RequestParameterName = RequestID;
            _EntityTypeID = EntityTypeID;
        }
        public void OnActionExecuting(ActionExecutingContext filterContext )
        {
            var RequestID= (int) filterContext.ActionParameters.SingleOrDefault(p => p.Key == _RequestParameterName).Value;
            EntityRequestDTO EntityRequestDTO = new EntityRequestDTO
            {
                EntityID = _EntityTypeID,
                ReqID = RequestID
            };
            CheckLockedRequestDTO CheckLockedRequest= BaseController.UnitOfWork.SharedBncBLL.CheckLockedRequest(EntityRequestDTO);
            if (CheckLockedRequest.IsLockedRequest)
            {
                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary(
                    new
                    {
                        controller = "User",
                        action = "UnAuthorized"
                    })
                );
            }

        }
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //throw new NotImplementedException();
        }        
    }
}