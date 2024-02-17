using InterWorks.Discounts.Models;

namespace InterWorks.Discounts.Applications;

public interface IDiscountProcessor
{
    DiscountApplicationResult ProcessDiscounts(Order order);
}