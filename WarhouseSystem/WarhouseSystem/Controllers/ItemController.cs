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
using System.Web;
using System.IO;
using Newtonsoft.Json;

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
        public IHttpActionResult AddItems()
        {
            try
            {
                ItemViewModel model=new ItemViewModel();
                var file = HttpContext.Current.Request.Files.Count > 0 ?
        HttpContext.Current.Request.Files[0] : null;
                var someValueFromPost = HttpContext.Current.Request.Form["ItemViewModel"];
                model = JsonConvert.DeserializeObject<ItemViewModel>(someValueFromPost);
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);

                    var path = Path.Combine(
                        HttpContext.Current.Server.MapPath("~/uploads"),
                        fileName
                    );

                    file.SaveAs(path);

                }

                    Utilites.CheckResult = manageItem.AddItem(model);
                return Ok(ApiResultFactory.Success(Utilites.CheckResult));
            }
            catch(Exception ex )
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.InternalServerError, "Item Added Is Faild"));
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
        public IHttpActionResult UpdateItem( ItemViewModel model)
        {
            try
            {
                Utilites.CheckResult = manageItem.UpdateItem( model);
                return Ok(ApiResultFactory.Success(Utilites.CheckResult));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.InvalidOperation, "Cannot Modify Item"));
            }
        }
        [HttpDelete]
        public IHttpActionResult DeleteItem(int Id)
        {
            try
            {
                Utilites.CheckResult = manageItem.DeleteItem(Convert.ToInt64( Id));
                return Ok(ApiResultFactory.Success(Utilites.CheckResult));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.InvalidOperation, "Can not deleted"));
            }
        }


    }
}
