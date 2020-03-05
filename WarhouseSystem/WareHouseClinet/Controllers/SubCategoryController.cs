using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using WarhouseSystem.Application.SubCategoryApp;
using WarhouseSystem.Common.ViewModels;
using WarhouseSystem.Common.Utilites;
using WarhouseSystem.Common.Enums;
using WarhouseSystem.Validation;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;

namespace WarhouseSystem.Controllers
{
    public class SubCategoryController : ControllerBase
    {
        IManageSubCategory manageSubCategory;
        public SubCategoryController()
        {
            manageSubCategory = new ManageSubCategory();
        }

        [HttpPost]
        public IActionResult AddSubCategory(SubCategoryViewModel model)
        {
            try
            {
                Utilites.CheckResult = manageSubCategory.AddSubCategory(model);
                return Ok(ApiResultFactory.Success(Utilites.CheckResult));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.InternalServerError, "Added SubCategory Is Faild"));
            }
        }

        [HttpGet]
        public IActionResult GetAllSubCategories()
        {
            try
            {
                IEnumerable<SubCategoryViewModel> models = manageSubCategory.GetAllSubCategories();
                return Ok(ApiResultFactory.Success(models));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.Empty, "No Data"));
            }
        }

        [HttpGet]

        public IActionResult GetAllSubCategoriesByCategoryId(long categoryId)
        {
            try
            {
                IEnumerable<SubCategoryViewModel> models = manageSubCategory.GetSubCategoriesByCategoryId(categoryId);
                return Ok(ApiResultFactory.Success(models));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.Empty, "No Data"));
            }
        }

        [HttpGet]
        public IActionResult GetSubCategoryById(long Id)
        {
            try
            {
                SubCategoryViewModel model = manageSubCategory.GetSubCategory(Id);
                return Ok(ApiResultFactory.Success(model));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.Empty, "No Data"));
            }
        }

        [HttpPut]
        public IActionResult UpdateSubCategory(long Id, SubCategoryViewModel model)
        {
            try
            {
                Utilites.CheckResult = manageSubCategory.UpdateSubCategory(Id, model);
                return Ok(ApiResultFactory.Success(Utilites.CheckResult));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.InvalidOperation, "Cannot Modify SubCategories"));
            }
        }

    }
}
