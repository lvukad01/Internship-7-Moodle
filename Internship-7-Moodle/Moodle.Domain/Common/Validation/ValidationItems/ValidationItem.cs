

namespace Moodle.Domain.Common.Validation
{
    public class ValidationItem
    {
        public string Code { get; init; }
        public string Message { get; init; }
        public ValidationSeverity Severity { get; set; }
        public ValidationType Type { get; init; }


    }
}
