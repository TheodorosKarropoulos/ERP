using InterWorks.DynamicFields.Commands;
using InterWorks.DynamicFields.Constants;
using InterWorks.DynamicFields.Models;
using InterWorks.DynamicFields.Repositories;
using InterWorks.DynamicFields.Repositories.Abstractions;
using InterWorks.DynamicFields.Services.Abstractions;
using MediatR;

namespace InterWorks.DynamicFields.Services;

public sealed class CustomerFieldService : ICustomerFieldService
{
    private readonly ICustomerFieldRepository _repository;
    private readonly IMediator _mediator;

    public CustomerFieldService(ICustomerFieldRepository repository, IMediator mediator)
    {
        _repository = repository;
        _mediator = mediator;
    }
    
    public async Task CreateFieldAsync(CustomerField customerField)
    {
        await _repository.CreateAsync(customerField);
        await _mediator.Send(new InsertFieldHistoryCommand
        {
            CustomerFieldId = customerField.Id,
            NewValue = customerField.Value
        });
    }

    public async Task UpdateAsync(Guid id, string newValue)
    {
        var field = await _repository.GetByIdAsync(id);
        if (field is null)
        {
            return;
        }

        var oldValue = field.Value;
        field.Value = newValue;

        await _repository.UpdateAsync(field);

        await _mediator.Send(new InsertFieldHistoryCommand
        {
            CustomerFieldId = id,
            NewValue = newValue,
            OldValue = oldValue
        });
    }

    public async Task<IEnumerable<CustomerField>> GetByCustomerIdAsync(Guid customerId)
    {
        return await _repository.GetByCustomerIdAsync(customerId);
    }
}