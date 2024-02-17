using InterWorks.DynamicFields.Constants;

namespace InterWorks.DynamicFields.Models;

public sealed class CustomerField
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public required string FieldName { get; set; }
    public required string Value { get; set; }
    public FieldType Type { get; set; }
    public Customer Customer { get; set; }
    public IEnumerable<FieldValueHistory>? FieldValueHistories { get; set; }
    public DateTimeOffset DateModified { get; set; }
}