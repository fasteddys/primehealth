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
    public class CategoryController : ApiController
    {
        IManageCategory manageCategory;
        public CategoryController()
        {
            manageCategory = new ManageCategory();
        }

        [HttpPost]
        public IHttpActionResult AddCategory(CategoryViewModel model)
        {
            try
            {
                Utilites.CheckResult = manageCategory.AddCategory(model);
                return Ok(ApiResultFactory.Success(Utilites.CheckResult));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.InternalServerError, "Added Category Is Faild"));
            }
        }

        [HttpGet]
        public IHttpActionResult GetAllCategories()
        {
            try
            {
                IEnumerable<CategoryViewModel> models = manageCategory.GetAllCategorys();
                return Ok(ApiResultFactory.Success(models));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.Empty, "No Data"));
            }
        }

        [HttpGet]
        public IHttpActionResult GetCategoryById(long Id)
        {
            try
            {
                CategoryViewModel model = manageCategory.GetCategory(Id);
                return Ok(ApiResultFactory.Success(model));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.Empty, "No Data"));
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteCategory(long Id)
        {
            try
            {
                Utilites.CheckResult = manageCategory.DeleteCategory(Id);
                return Ok(ApiResultFactory.Success(Utilites.CheckResult));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.InvalidOperation, "Cannot Delete Category"));
            }
        }

        [HttpPut]
        public IHttpActionResult UpdateCategory(long Id, CategoryViewModel model)
        {
            try
            {
                Utilites.CheckResult = manageCategory.UpdateCategory(Id, model);
                return Ok(ApiResultFactory.Success(Utilites.CheckResult));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.InvalidOperation, "Cannot Modify Category"));
            }
        }

    }
}
