﻿using InterWorks.DynamicFields.Models;

namespace InterWorks.DynamicFields.Services.Abstractions;

public interface ICustomerFieldHistoryService
{
    Task InsertAsync(FieldValueHistory fieldValueHistory);
}