using AutoMapper;
using InterWorks.Api.Requests;
using InterWorks.DynamicFields.Models;

namespace InterWorks.Api.Mappers;

public class CreateCustomerProfile : Profile
{
    public CreateCustomerProfile()
    {
        CreateMap<CreateCustomer, Customer>()
            .ForMember(dest => dest.Name, opt
                => opt.MapFrom(src => src.Name));
    }
}