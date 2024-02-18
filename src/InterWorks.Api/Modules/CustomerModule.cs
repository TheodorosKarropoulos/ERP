using AutoMapper;
using InterWorks.Api.Requests;
using InterWorks.DynamicFields.Models;
using InterWorks.DynamicFields.Services.Abstractions;

namespace InterWorks.Api.Modules;

public static class CustomerModule
{
    public static void AddCustomerEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/customer/{id}",
            async (Guid id, ICustomerService service) => await service.GetByIdAsync(id));

        app.MapPost("/customer",
            async (CreateCustomer request, ICustomerService service, IMapper mapper) =>
            {
                var customer = mapper.Map<CreateCustomer, Customer>(request);
                await service.CreateAsync(customer);

                return Results.Ok(customer.Id);
            });

        app.MapPut("/customer",
            async (Customer customer, ICustomerService service) => { await service.UpdateAsync(customer); });
    }
}