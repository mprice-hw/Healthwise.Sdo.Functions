using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthwise.Sdo.Functions.Validation
{
    // https://medium.com/@mariochiadev/model-validation-for-http-triggered-azure-functions-in-c-130676c2d490
    public class ValidationWrapper<T>
    {
        public bool IsValid { get; set; }
        public T Value { get; set; }

        public IEnumerable<ValidationResult> ValidationResults { get; set; }
    }
}
