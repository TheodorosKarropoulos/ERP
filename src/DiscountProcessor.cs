using InterWorks.Discounts.Constants;
using InterWorks.Discounts.Models;

namespace InterWorks.Discounts;

public class DiscountProcessor : IDiscountProcessor
{
    private readonly List<string> _discountOrder = new()
    {
        DiscountName.PriceList,
        DiscountName.Promotion,
        DiscountName.Coupon
    };
    
    public DiscountApplicationResult ProcessDiscounts(Order order)
    {
        ArgumentNullException.ThrowIfNull(order);

        return GetDiscountApplicationResult(order);
    }

    private DiscountApplicationResult GetDiscountApplicationResult(Order order)
    {
        var result = new DiscountApplicationResult
        {
            TotalAmount = order.TotalAmount
        };

        foreach (var discountType in _discountOrder)
        {
            if (order.Discounts is null)
            {
                continue;
            }
            
            var discountStrategy = order.Discounts
                .FirstOrDefault(x => x.Name == discountType);
            
            if (discountStrategy is null)
            {
                continue;
            }

            var discountAmount =
                discountStrategy.CalculateDiscount(result.AmountAfterDiscount);
            
            result.Discounts.Add(new Discount(discountType, discountAmount));
        }

        return result;
    }
}