

namespace Moodle.Domain.Common.Validation.ValidationItems
{
    public static partial class ValidationItems
    {

        public static class User
        {
            public static string CodePrefix = nameof(User);

            public static readonly ValidationItem EmailRequired = new ValidationItem
            {
                Code = $"{CodePrefix}1",
                Message = "Potrebno unijeti email.",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.FormalValidation
            };


            //public static readonly ValidationItem InvalidWebsiteUrl = new ValidationItem
            //{
            //    Code = $"{CodePrefix}8",
            //    Message = "Pogrešan URL web stranice.",
            //    Severity = ValidationSeverity.Warning,
            //    Type = ValidationType.FormalValidation
            //};


            public static readonly ValidationItem EmailValid = new ValidationItem
            {
                Code = $"{CodePrefix}2",
                Message = "Email nije točan.",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.FormalValidation
            };

            public static readonly ValidationItem PasswordRequired = new ValidationItem
            {
                Code = $"{CodePrefix}3",
                Message = "Potrebno unijeti lozinku.",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.FormalValidation
            };

            public static readonly ValidationItem EmailAlreadyExists = new ValidationItem
            {
                Code = $"{CodePrefix}4",
                Message = "Email vec postoji.",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.FormalValidation
            };
            public static readonly ValidationItem UserNotFound = new ValidationItem
            {
                Code = $"{CodePrefix}5",
                Message = "Korisnik s tim ID-om ne postoji.",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.BusinessRule
            };

        }

    }
}
