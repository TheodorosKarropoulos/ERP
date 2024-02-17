using InterWorks.Discounts.Constants;

namespace InterWorks.Discounts.Strategies
{
    public class PriceListDiscountStrategy : IDiscountStrategy
    {
        public PriceListDiscountStrategy(decimal discount)
        {
            Discount = discount;
        }

        public string Name => DiscountName.PriceList;
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
