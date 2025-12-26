using System.Text.Json.Serialization;

namespace Moodle.Domain.Common.Validation
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ValidationSeverity
    {
        Info,
        Warning,
        Error
    }
}