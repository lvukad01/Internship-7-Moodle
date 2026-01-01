namespace Moodle.Domain.Common.Validation.ValidationItems
{
    public static partial class ValidationItems
    {
        public static class Announcement
        {
            public static string CodePrefix = nameof(Announcement);

            public static readonly ValidationItem TitleRequired = new ValidationItem
            {
                Code = $"{CodePrefix}1",
                Message = "Naslov obavijesti je obavezan.",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.FormalValidation
            };

            public static readonly ValidationItem ContentRequired = new ValidationItem
            {
                Code = $"{CodePrefix}2",
                Message = "Tekst obavijesti je obavezan.",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.FormalValidation
            };

            public static readonly ValidationItem CourseNotFound = new ValidationItem
            {
                Code = $"{CodePrefix}3",
                Message = "Kolegij ne postoji.",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.BusinessRule
            };
        }
    }
}
