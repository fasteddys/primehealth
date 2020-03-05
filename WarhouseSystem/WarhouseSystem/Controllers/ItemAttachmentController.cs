using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WarhouseSystem.Application.CategoryApp;
using WarhouseSystem.Common.ViewModels;
using WarhouseSystem.Common.Utilites;
using WarhouseSystem.Common.Enums;
using WarhouseSystem.Validation;

namespace WarhouseSystem.Controllers
{
    public class ItemAttachmentController : ApiController
    {
        IManageItemAttachment manageItemAttachment;
        public ItemAttachmentController()
        {
            manageItemAttachment = new ManageItemAttachment();
        }

        [HttpPost]
        public IHttpActionResult AddItemAttachment(ItemAttachmentModel model)
        {
            try
            {
                Utilites.CheckResult = manageItemAttachment.AddItemAttachment(model);
                return Ok(ApiResultFactory.Success(Utilites.CheckResult));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.InternalServerError, "Added Attachment Is Faild"));
            }
        }

        [HttpGet]
        public IHttpActionResult GetAllItemAttachment(int ItemId)
        {
            try
            {
                IEnumerable<ItemAttachmentModel> models = manageItemAttachment.GetAllItemAttachment(ItemId);
                return Ok(ApiResultFactory.Success(models));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.Empty, "No Data"));
            }
        }   

    }
}
