using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WarhouseSystem.Application.ItemApp;
using WarhouseSystem.Common.ViewModels;
using WarhouseSystem.Common.Utilites;
using WarhouseSystem.Common.Enums;
using WarhouseSystem.Validation;

namespace WarhouseSystem.Controllers
{
    public class ItemController : ApiController
    {
        IManageItem manageItem;

        public ItemController()
        {
            manageItem = new ManageItem();
        }

        [HttpPost]
        public IHttpActionResult AddItems(ItemViewModel model)
        {
            try
            {
                Utilites.CheckResult = manageItem.AddItem(model);
                return Ok(ApiResultFactory.Success(Utilites.CheckResult));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.InternalServerError, "Item Added Is Faild"));
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteItem(long Id)
        {
            try
            {
                Utilites.CheckResult = manageItem.DeleteItem(Id);
                return Ok(ApiResultFactory.Success(Utilites.CheckResult));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.InvalidOperation, "Cannot Delete Item"));
            }
        }

        [HttpGet]
        public IHttpActionResult GetAllItems()
        {
            try
            {
                IEnumerable<ItemViewModel> models = manageItem.GetAllItems();
                return Ok(ApiResultFactory.Success(models));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.Empty, "No Data"));
            }
        }

        [HttpGet]
        public IHttpActionResult GetItemById(long Id)
        {
            try
            {
                ItemViewModel model = manageItem.GetItem(Id);
                return Ok(ApiResultFactory.Success(model));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.Empty, "No Data"));
            }
        }

        [HttpGet]
        public IHttpActionResult GetItemBySubCategoryId(long subCategoryId)
        {
            try
            {
                IEnumerable<ItemViewModel> model = manageItem.GetAllItemsBySubCategoryId(subCategoryId);
                return Ok(ApiResultFactory.Success(model));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.Empty, "No Data"));
            }
        }

        [HttpPut]
        public IHttpActionResult UpdateItem(long Id, ItemViewModel model)
        {
            try
            {
                Utilites.CheckResult = manageItem.UpdateItem(Id, model);
                return Ok(ApiResultFactory.Success(Utilites.CheckResult));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.InvalidOperation, "Cannot Modify Item"));
            }
        }

    }
}
