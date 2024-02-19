using InterWorks.Discounts.Constants;

namespace InterWorks.Api.Requests;

public record ProcessDiscount(decimal TotalAmount, IEnumerable<OrderDiscount> Discounts);
public record OrderDiscount(string Name, decimal Discount);