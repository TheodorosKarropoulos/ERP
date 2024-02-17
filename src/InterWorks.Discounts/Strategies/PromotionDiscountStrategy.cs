using InterWorks.Discounts.Constants;

namespace InterWorks.Discounts.Strategies
{
    public class PromotionDiscountStrategy : IDiscountStrategy
    {
        public PromotionDiscountStrategy(decimal discount)
        {
            Discount = discount;
        }

        public string Name => DiscountName.Promotion;
        public DiscountType Type => DiscountType.Percentage;
        public decimal Discount { get; }

        public decimal CalculateDiscount(decimal amount)
        {
            if (amount <= 0 || Discount <= 0)
            {
                return default;
            }
            
            return amount * (Discount / 100);
        }
    }
}
