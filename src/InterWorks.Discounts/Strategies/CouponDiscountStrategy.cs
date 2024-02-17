using InterWorks.Discounts.Constants;

namespace InterWorks.Discounts.Strategies
{
    public class CouponDiscountStrategy : IDiscountStrategy
    {
        public CouponDiscountStrategy(decimal discount)
        {
            Discount = discount;
        }

        public string Name => DiscountName.Coupon;
        public DiscountType Type => DiscountType.FlatAmount;
        public decimal Discount { get; }

        public decimal CalculateDiscount(decimal amount)
        {
            if (amount <= 0 || Discount <= 0)
            {
                return default;
            }
            
            return Discount;
        }
    }
}
