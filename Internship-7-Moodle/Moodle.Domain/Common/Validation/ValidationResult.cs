
namespace Moodle.Domain.Common.Validation
{
    public class ValidationResult
    {
        private readonly List<ValidationItem> _validationItems = new List<ValidationItem>();
        public IReadOnlyList<ValidationItem> Items => _validationItems;
        public bool HasErrors => _validationItems.Any(validationResult => validationResult.Severity == ValidationSeverity.Error);
        public bool HasWarning => _validationItems.Any(validationResult => validationResult.Severity == ValidationSeverity.Warning);
        public bool HasInfo => _validationItems.Any(validationResult => validationResult.Severity == ValidationSeverity.Info);

        public void AddValidationItem(ValidationItem validationItem)
        {
            _validationItems.Add(validationItem);
        }
    }
}
