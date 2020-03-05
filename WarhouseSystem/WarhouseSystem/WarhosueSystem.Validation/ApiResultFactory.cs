using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarhouseSystem.Validation
{
    public static class ApiResultFactory
    {
         public static ApiResult Success(object data)
        {
            return ApiResult.Success(data);
        }

        public static ApiResult Failure(ErrorCode errorCode, string errorMessage)
        {
            return ApiResult.Failure(errorCode, errorMessage);
        }

        
    }
}
