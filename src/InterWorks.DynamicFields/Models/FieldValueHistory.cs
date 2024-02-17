namespace InterWorks.DynamicFields.Models;

public class FieldValueHistory
{
    public Guid Id { get; set; }
    public Guid CustomerFieldId { get; init; }
    public string? OldValue { get; init; }
    public required string NewValue { get; init; }
    public DateTimeOffset DateModified { get; set; }
    public CustomerField? CustomerField { get; init; }
}