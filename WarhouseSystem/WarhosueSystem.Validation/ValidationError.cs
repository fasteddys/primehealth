using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarhouseSystem.Validation
{
   public class ValidationError
    {
        public ValidationError()
        {
        }

        public ValidationError(string field, string error)
        {
            Field = field;
            Error = error;
        }

        public string Field { get; set; }

        public string Error { get; set; }
    }
}

