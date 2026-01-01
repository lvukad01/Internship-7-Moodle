namespace Moodle.Domain.Common.Validation.ValidationItems
{
    public static partial class ValidationItems
    {
        public static class Enrollment
        {
            public static string CodePrefix = nameof(Enrollment);

            public static readonly ValidationItem StudentAlreadyEnrolled = new ValidationItem
            {
                Code = $"{CodePrefix}1",
                Message = "Student je već upisan na kolegij.",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.BusinessRule
            };

            public static readonly ValidationItem StudentNotEnrolled = new ValidationItem
            {
                Code = $"{CodePrefix}2",
                Message = "Student nije upisan na kolegij.",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.BusinessRule
            };
        }
    }
}
