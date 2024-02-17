using InterWorks.DynamicFields.Commands;
using InterWorks.DynamicFields.Models;
using InterWorks.DynamicFields.Services.Abstractions;
using MediatR;

namespace InterWorks.DynamicFields.Handlers;

public sealed class InsertFieldHistoryCommandHandler : IRequestHandler<InsertFieldHistoryCommand, FieldValueHistory>
{
    private readonly ICustomerFieldHistoryService _service;

    public InsertFieldHistoryCommandHandler(ICustomerFieldHistoryService service)
    {
        _service = service;
    }

    public async Task<FieldValueHistory> Handle(InsertFieldHistoryCommand request, CancellationToken cancellationToken)
    {
        var history = new FieldValueHistory
        {
            CustomerFieldId = request.CustomerFieldId,
            NewValue = request.NewValue,
            OldValue = request.OldValue
        };

        await _service.InsertAsync(history);

        return await Task.FromResult(history);
    }
}