using InterWorks.DynamicFields.Constants;

namespace InterWorks.Api.Requests;

public record CreateCustomerField(Guid CustomerId, string FieldName, string FieldValue, FieldType FieldType);