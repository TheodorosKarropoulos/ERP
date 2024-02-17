using AutoMapper;
using InterWorks.Api.Requests;
using InterWorks.DynamicFields.Models;

namespace InterWorks.Api.Mappers;

public sealed class CreateCustomerFieldProfile : Profile
{
    public CreateCustomerFieldProfile()
    {
        CreateMap<CreateCustomerField, CustomerField>()
            .ForMember(dest => dest.Type, opt
                => opt.MapFrom(src => src.FieldType))
            .ForMember(dest => dest.FieldName, opt
                => opt.MapFrom(src => src.FieldName))
            .ForMember(dest => dest.Value, opt
                => opt.MapFrom(src => src.FieldValue));
    }
}