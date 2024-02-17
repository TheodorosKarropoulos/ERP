using InterWorks.Discounts.Constants;

namespace InterWorks.Discounts.Strategies
{
    public interface IDiscountStrategy
    {
        string Name { get; }
        DiscountType Type { get; }
        decimal Discount { get; }
        decimal CalculateDiscount(decimal amount);
    }
}
