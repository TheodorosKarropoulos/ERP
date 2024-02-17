using InterWorks.DynamicFields.Models;
using MediatR;

namespace InterWorks.DynamicFields.Commands;

public sealed class InsertFieldHistoryCommand : IRequest<FieldValueHistory>
{
    public Guid CustomerFieldId { get; init; }
    public string? OldValue { get; init; }
    public required string NewValue { get; init; }
}