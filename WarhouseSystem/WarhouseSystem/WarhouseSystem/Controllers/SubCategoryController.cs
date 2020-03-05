using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using WarhouseSystem.Application.SubCategoryApp;
using WarhouseSystem.Application.CategoryApp;
using WarhouseSystem.Application.ProcessTransactionApp;
using WarhouseSystem.Common.ViewModels;
using WarhouseSystem.Common.Utilites;
using WarhouseSystem.Common.Enums;
using WarhouseSystem.Validation;
using System.Web.Http;


namespace WarhouseSystem.Controllers
{
    public class SubCategoryController : ApiController
    {
        IManageSubCategory manageSubCategory;
        IManageCategory manageCategory;
        IManageProcessTransaction manageProcessTransaction;
        public SubCategoryController()
        {
            manageSubCategory = new ManageSubCategory();
            manageCategory = new ManageCategory();
            manageProcessTransaction = new ManageProcessTransaction();

        }

        [HttpPost]
        public IHttpActionResult AddSubCategory(SubCategoryViewModel model)
        {
            try
            {
                long? categoryCount = manageCategory.GetSubCategoryCountById(model.CategoryId);
                long subcategoryCount = manageSubCategory.GetCount(model.CategoryId);
                long subCategoryId = 0;
                try
                {
                    if(categoryCount>0)
                    {
                        if(categoryCount < subcategoryCount+1)
                        {
                            return Ok(ApiResultFactory.Failure(ErrorCode.InvalidOperation, "Count of Items is completed"));
                        }
                        else
                        {
                           
                            Utilites.CheckResult = manageSubCategory.AddSubCategory(model,out subCategoryId);
                            Tuple<bool,long> customData =new Tuple<bool, long>(Utilites.CheckResult, subCategoryId);
                            return Ok(ApiResultFactory.Success(customData));
                        }
                    }
                    else
                    {
                        return Ok(ApiResultFactory.Failure(ErrorCode.NotFound, "Count of Category Still zero"));
                    }
                      
                }
            
                catch
                {
                    return Ok(ApiResultFactory.Failure(ErrorCode.InternalServerError, "Added SubCategory Is Faild"));
                }
            
              
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.InternalServerError, "Added SubCategory Is Faild"));
            }
        }

        [HttpGet]
        public IHttpActionResult GetAllSubCategories()
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

        public IHttpActionResult GetAllSubCategoriesByCategoryId(long Id)
        {
            try
            {
                IEnumerable<SubCategoryViewModel> models = manageSubCategory.GetSubCategoriesByCategoryId(Id);
                return Ok(ApiResultFactory.Success(models));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.Empty, "No Data"));
            }
        }

        [HttpGet]
        public IHttpActionResult GetSubCategoryById(long Id)
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
        public IHttpActionResult UpdateSubCategory(long Id, SubCategoryViewModel model)
        {
            try
            {
                Utilites.CheckResult = manageSubCategory.UpdateSubCategory(Id, model);
                //Utilites.CheckResult = manageProcessTransaction.AddProcessTransaction(model2);
                return Ok(ApiResultFactory.Success(Utilites.CheckResult));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.InvalidOperation, "Cannot Modify SubCategories"));
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteSubCategory(long Id)
        {
            try
            {
                Utilites.CheckResult = manageSubCategory.DeleteSubCategory(Id);
                //Utilites.CheckResult = manageProcessTransaction.AddProcessTransaction(model2);
                return Ok(ApiResultFactory.Success(Utilites.CheckResult));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.InvalidOperation, "Cannot Delete SubCategory"));
            }
        }

    }
}
