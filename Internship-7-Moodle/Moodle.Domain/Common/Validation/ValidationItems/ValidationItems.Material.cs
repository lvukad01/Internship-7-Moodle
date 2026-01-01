namespace Moodle.Domain.Common.Validation.ValidationItems
{
    public static partial class ValidationItems
    {
        public static class Material
        {
            public static string CodePrefix = nameof(Material);

            public static readonly ValidationItem TitleRequired = new ValidationItem
            {
                Code = $"{CodePrefix}1",
                Message = "Naziv materijala je obavezan.",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.FormalValidation
            };

            public static readonly ValidationItem UrlRequired = new ValidationItem
            {
                Code = $"{CodePrefix}2",
                Message = "URL materijala je obavezan.",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.FormalValidation
            };

            public static readonly ValidationItem UrlInvalid = new ValidationItem
            {
                Code = $"{CodePrefix}3",
                Message = "URL materijala nije ispravnog formata.",
                Severity = ValidationSeverity.Warning,
                Type = ValidationType.FormalValidation
            };

            public static readonly ValidationItem CourseNotFound = new ValidationItem
            {
                Code = $"{CodePrefix}4",
                Message = "Kolegij ne postoji.",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.BusinessRule
            };
        }
    }
}
