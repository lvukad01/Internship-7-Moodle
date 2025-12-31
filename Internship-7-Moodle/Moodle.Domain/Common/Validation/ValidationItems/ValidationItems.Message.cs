namespace Moodle.Domain.Common.Validation.ValidationItems
{
    public static class MessageValidationItems
    {
        public static ValidationItem ContentRequired => new ValidationItem
        {
            Code = "MSG1",
            Message = "Poruka ne smije biti prazna.",
            Severity = ValidationSeverity.Error,
            Type = ValidationType.BusinessRule
        };

        public static ValidationItem ContentTooLong => new ValidationItem
        {
            Code = "MSG2",
            Message = "Poruka je predugačka.",
            Severity = ValidationSeverity.Error,
            Type = ValidationType.BusinessRule
        };
    }
}
