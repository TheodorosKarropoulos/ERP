using InterWorks.Discounts.Models;

namespace InterWorks.Discounts;

public interface IDiscountProcessor
{
    DiscountApplicationResult ProcessDiscounts(Order order);
}