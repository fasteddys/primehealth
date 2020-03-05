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
    //         [CheckRequestLockedobj("FilterationRequestID", (int)StaticEnums.Entity.FilterationRequest, "FilterationRequest")]

    public class CheckRequestLockedobj : FilterAttribute, IActionFilter
    {
        private string _RequestParameterName { get; set; }
        private int _EntityTypeID { get; set; }
        private string _ObjectName { get; set; }

        
        private BaseController BaseController;

        public CheckRequestLockedobj(string RequestIdProprtyName,int EntityTypeID,string ObjectName)
        {
            BaseController = new BaseController();
            _RequestParameterName = RequestIdProprtyName;
            _EntityTypeID = EntityTypeID;
            _ObjectName = ObjectName;
        }
        public void OnActionExecuting(ActionExecutingContext filterContext )
        {
            dynamic Requestobject=  filterContext.ActionParameters.SingleOrDefault(p => p.Key == _ObjectName).Value;
            var RequestID= Requestobject.GetType().GetProperty(_RequestParameterName).GetValue(Requestobject, null);
            EntityRequestDTO EntityRequestDTO = new EntityRequestDTO
            {
                EntityID = _EntityTypeID,
                ReqID =(int) RequestID
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