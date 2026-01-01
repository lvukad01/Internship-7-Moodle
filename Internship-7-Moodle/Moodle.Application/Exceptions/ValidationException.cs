using Moodle.Domain.Common.Validation;

namespace Moodle.Application.Exceptions
{
    public class ValidationException : Exception //povezuje domain i application layer
    {
        public IReadOnlyCollection<ValidationItem> Errors { get; }

        public ValidationException(ValidationItem error)
            : this(new List<ValidationItem> { error })
        {
        }

        public ValidationException(IEnumerable<ValidationItem> errors)
        {
            Errors = errors.ToList().AsReadOnly();
        }
    }
}

