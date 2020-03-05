using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarhouseSystem.Validation
{
    public enum ErrorCode
    {
        NotFound,
        InvalidID,
        Empty,
        InvalidOperation,
        InvalidNationalId,
        InternalServerError,
        ValidationError,
        MissingInput,
        MissingConfiguration,
        RuntimeError,
        FieldExist,
        MeterError,
        DependencyError,
        NotAllowed
    }
}
