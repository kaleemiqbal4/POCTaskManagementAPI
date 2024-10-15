using System.Text.Json.Serialization;

namespace POCProject.API.ModelValidation;

/// <summary>
/// Represents the details of a specific error related to model validation.
/// Contains the name of the property with the issue, the error message, and the row number if applicable.
/// </summary>
public class ErrorDetails
{
    /// <summary>
    /// Gets or sets the name of the property that has a validation issue.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the error message explaining the validation issue.
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// Gets or sets the row number associated with the validation issue, if applicable.
    /// Will be ignored in the JSON response if it is the default value (0).
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int Number { get; set; }
}
