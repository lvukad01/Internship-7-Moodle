namespace Moodle.Domain.Common.Validation.ValidationItems
{
    public static partial class ValidationItems
    {
        public static class Course
        {
            public static string CodePrefix = nameof(Course);

            public static readonly ValidationItem CourseNotFound = new ValidationItem
            {
                Code = $"{CodePrefix}1",
                Message = "Kolegij ne postoji.",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.BusinessRule
            };

            public static readonly ValidationItem NameRequired = new ValidationItem
            {
                Code = $"{CodePrefix}2",
                Message = "Naziv kolegija je obavezan.",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.FormalValidation
            };

            public static readonly ValidationItem NameTooShort = new ValidationItem
            {
                Code = $"{CodePrefix}3",
                Message = "Naziv kolegija mora imati barem 3 znaka.",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.FormalValidation
            };

            public static readonly ValidationItem ProfessorRequired = new ValidationItem
            {
                Code = $"{CodePrefix}4",
                Message = "Kolegij mora imati dodijeljenog profesora.",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.BusinessRule
            };

            public static readonly ValidationItem StudentAlreadyEnrolled = new ValidationItem
            {
                Code = $"{CodePrefix}5",
                Message = "Student je već upisan na kolegij.",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.BusinessRule
            };
        }
    }
}

