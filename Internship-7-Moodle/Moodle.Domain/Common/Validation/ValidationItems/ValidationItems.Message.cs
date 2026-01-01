namespace Moodle.Domain.Common.Validation.ValidationItems
{
    public static partial class ValidationItems
    {
        public static class Message
        {
            public static string CodePrefix = nameof(Message);

            public static ValidationItem ContentRequired => new ValidationItem
            {
                Code = $"{CodePrefix}1",
                Message = "Poruka ne smije biti prazna.",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.BusinessRule
            };

            public static ValidationItem ContentTooLong => new ValidationItem
            {
                Code = $"{CodePrefix}2",
                Message = "Poruka je predugačka.",
                Severity = ValidationSeverity.Error,
                Type = ValidationType.BusinessRule
            };
        }
    }
}
