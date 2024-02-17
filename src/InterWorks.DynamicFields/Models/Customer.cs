namespace InterWorks.DynamicFields.Models;

public sealed class Customer
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public IEnumerable<CustomerField>? CustomerFields { get; set; }
}