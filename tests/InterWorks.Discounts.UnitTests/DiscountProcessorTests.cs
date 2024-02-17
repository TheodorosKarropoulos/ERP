using InterWorks.Discounts.Constants;
using InterWorks.Discounts.Models;
using InterWorks.Discounts.Strategies;

namespace InterWorks.Discounts.Tests;

public class DiscountProcessorTests
{
    [Fact]
    public void Should_Calculate_All_Discounts_With_Correct_Order()
    {
        // Arrange
        var order = GetOrderWithAllDiscountTypesUnOrdered();
        var discountProcessor = new DiscountProcessor();

        // Act
        var result = discountProcessor.ProcessDiscounts(order);

        var priceListDiscount = result.Discounts.FirstOrDefault(x => x.DiscountName == DiscountName.PriceList);
        var promotionDiscount = result.Discounts.FirstOrDefault(x => x.DiscountName == DiscountName.Promotion);
        var couponDiscount = result.Discounts.FirstOrDefault(x => x.DiscountName == DiscountName.Coupon);

        // Assert
        Assert.NotNull(priceListDiscount);
        Assert.NotNull(promotionDiscount);
        Assert.NotNull(couponDiscount);
        Assert.Equal(17, priceListDiscount.DiscountAmount);
        Assert.Equal(32.3m, promotionDiscount.DiscountAmount);
        Assert.Equal(10m, couponDiscount.DiscountAmount);
        Assert.Equal(280.7m, result.AmountAfterDiscount);
        Assert.Equal(59.3m, result.DiscountAmount);

        Assert.True(result.Discounts[0].DiscountName == DiscountName.PriceList);
        Assert.True(result.Discounts[1].DiscountName == DiscountName.Promotion);
        Assert.True(result.Discounts[2].DiscountName == DiscountName.Coupon);
    }

    [Fact]
    public void Should_Calculate_All_Discounts()
    {
        // Arrange
        var order = GetOrderWithAllDiscountTypes();
        var discountProcessor = new DiscountProcessor();

        // Act
        var result = discountProcessor.ProcessDiscounts(order);

        var priceListDiscount = result.Discounts.FirstOrDefault(x => x.DiscountName == DiscountName.PriceList);
        var promotionDiscount = result.Discounts.FirstOrDefault(x => x.DiscountName == DiscountName.Promotion);
        var couponDiscount = result.Discounts.FirstOrDefault(x => x.DiscountName == DiscountName.Coupon);

        // Assert
        Assert.NotNull(priceListDiscount);
        Assert.NotNull(promotionDiscount);
        Assert.NotNull(couponDiscount);
        Assert.Equal(17, priceListDiscount.DiscountAmount);
        Assert.Equal(32.3m, promotionDiscount.DiscountAmount);
        Assert.Equal(10m, couponDiscount.DiscountAmount);
        Assert.Equal(280.7m, result.AmountAfterDiscount);
        Assert.Equal(59.3m, result.DiscountAmount);
    }

    [Fact]
    public void Should_Calculate_PriceList_And_Coupon_Discounts()
    {
        // Arrange
        var order = GetOrderWithPriceListAndCouponDiscount();
        var discountProcessor = new DiscountProcessor();

        // Act
        var result = discountProcessor.ProcessDiscounts(order);

        var priceListDiscount = result.Discounts.FirstOrDefault(x => x.DiscountName == DiscountName.PriceList);
        var promotionDiscount = result.Discounts.FirstOrDefault(x => x.DiscountName == DiscountName.Promotion);
        var couponDiscount = result.Discounts.FirstOrDefault(x => x.DiscountName == DiscountName.Coupon);

        // Assert
        Assert.NotNull(priceListDiscount);
        Assert.NotNull(couponDiscount);
        Assert.Null(promotionDiscount);
        Assert.Equal(17, priceListDiscount.DiscountAmount);
        Assert.Equal(27m, result.DiscountAmount);
        Assert.Equal(313m, result.AmountAfterDiscount);
    }

    [Fact]
    public void Should_Calculate_PriceList_And_Promotion_Discounts()
    {
        // Arrange
        var order = GetOrderWithPriceListAndPromotionDiscount();
        var discountProcessor = new DiscountProcessor();

        // Act
        var result = discountProcessor.ProcessDiscounts(order);

        var priceListDiscount = result.Discounts.FirstOrDefault(x => x.DiscountName == DiscountName.PriceList);
        var promotionDiscount = result.Discounts.FirstOrDefault(x => x.DiscountName == DiscountName.Promotion);
        var couponDiscount = result.Discounts.FirstOrDefault(x => x.DiscountName == DiscountName.Coupon);

        // Assert
        Assert.NotNull(priceListDiscount);
        Assert.Null(couponDiscount);
        Assert.NotNull(promotionDiscount);
        Assert.Equal(17, priceListDiscount.DiscountAmount);
        Assert.Equal(32.3m, promotionDiscount.DiscountAmount);
        Assert.Equal(290.7m, result.AmountAfterDiscount);
        Assert.Equal(49.3m, result.DiscountAmount);
    }

    [Fact]
    public void Should_Calculate_Coupon_And_Promotion_Discounts()
    {
        // Arrange
        var order = GetOrderWithCouponAndPromotionDiscountTypes();
        var discountProcessor = new DiscountProcessor();

        // Act
        var result = discountProcessor.ProcessDiscounts(order);

        var priceListDiscount = result.Discounts.FirstOrDefault(x => x.DiscountName == DiscountName.PriceList);
        var promotionDiscount = result.Discounts.FirstOrDefault(x => x.DiscountName == DiscountName.Promotion);
        var couponDiscount = result.Discounts.FirstOrDefault(x => x.DiscountName == DiscountName.Coupon);

        // Assert
        Assert.Null(priceListDiscount);
        Assert.NotNull(promotionDiscount);
        Assert.NotNull(couponDiscount);
        Assert.Equal(34m, promotionDiscount.DiscountAmount);
        Assert.Equal(10m, couponDiscount.DiscountAmount);
        Assert.Equal(296m, result.AmountAfterDiscount);
        Assert.Equal(44m, result.DiscountAmount);
    }

    private static Order GetOrderWithAllDiscountTypes()
    {
        return new Order
        {
            Discounts = new List<IDiscountStrategy>
            {
                new PriceListDiscountStrategy(5m),
                new PromotionDiscountStrategy(10m),
                new CouponDiscountStrategy(10m)
            },
            TotalAmount = 340m
        };
    }

    private static Order GetOrderWithAllDiscountTypesUnOrdered()
    {
        return new Order
        {
            Discounts = new List<IDiscountStrategy>
            {
                new PromotionDiscountStrategy(10m),
                new CouponDiscountStrategy(10m),
                new PriceListDiscountStrategy(5m)
            },
            TotalAmount = 340m
        };
    }

    private static Order GetOrderWithPriceListAndCouponDiscount()
    {
        return new Order
        {
            Discounts = new List<IDiscountStrategy>
            {
                new PriceListDiscountStrategy(5m),
                new CouponDiscountStrategy(10m)
            },
            TotalAmount = 340m
        };
    }

    private static Order GetOrderWithPriceListAndPromotionDiscount()
    {
        return new Order
        {
            Discounts = new List<IDiscountStrategy>
            {
                new PromotionDiscountStrategy(10m),
                new PriceListDiscountStrategy(5m)
            },
            TotalAmount = 340m
        };
    }

    private static Order GetOrderWithCouponAndPromotionDiscountTypes()
    {
        return new Order
        {
            Discounts = new List<IDiscountStrategy>
            {
                new CouponDiscountStrategy(10m),
                new PromotionDiscountStrategy(10m)
            },
            TotalAmount = 340m
        };
    }
}