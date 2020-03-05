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
using Microsoft.AspNetCore.Mvc;

namespace WarhouseSystem.Controllers
{

    public class CategoryController : ControllerBase
    {
        IManageCategory manageCategory;
        public CategoryController()
        {
            manageCategory = new ManageCategory();
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult AddCategory(CategoryViewModel model)
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
        public IActionResult GetAllCategories()
        {
            try
            {
                IEnumerable<CategoryViewModel> models = manageCategory.GetAllCategorys();
                return Ok(ApiResultFactory.Success(models));
            }
            catch(Exception ex)
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.Empty, "No Data"));
            }
        }

        [HttpGet]
        public IActionResult GetCategoryById(long Id)
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
        public IActionResult DeleteCategory(long Id)
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
        public IActionResult UpdateCategory(long Id, CategoryViewModel model)
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
