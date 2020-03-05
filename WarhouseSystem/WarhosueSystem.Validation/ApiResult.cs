using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarhouseSystem.Validation
{
   public class ApiResult
    {
        public string ErrorCode { get; private set; }

        public string ErrorMessage { get; private set; }

        public object Data { get; private set; }

        // public List<ValidationError> ValidationErrors { get; private set; }
        private ApiResult()
        {
            ErrorCode = string.Empty;
            ErrorMessage = string.Empty;
           // ValidationErrors = new List<ValidationError>();
        }

        private ApiResult(ErrorCode errorCode, string errorMessage)
        {
            ErrorCode = errorCode.ToString();
            ErrorMessage = errorMessage;
        }

        private ApiResult(object data)
            : this()
        {
            Data = data;
        }
        public static ApiResult Success(object data)
        {
            return new ApiResult(data);
        }

        public static ApiResult Failure(ErrorCode errorCode, string errorMessage)
        {
            return new ApiResult(errorCode, errorMessage);
        }

        
     
    }
}
