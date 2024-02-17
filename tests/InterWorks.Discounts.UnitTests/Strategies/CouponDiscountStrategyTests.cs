using InterWorks.Discounts.Strategies;

namespace InterWorks.Discounts.Tests.Strategies;

public class CouponDiscountStrategyTests
{
    [Fact]
    public void CalculateDiscount_ReturnsZero_WhenAmountIsZero()
    {
        // Arrange
        var couponDiscountStrategy = new CouponDiscountStrategy(10);
        
        // Act
        var discount = couponDiscountStrategy.CalculateDiscount(0);

        // Assert
        Assert.Equal(0, discount);
    }

    [Fact]
    public void CalculateDiscount_ReturnsZero_WhenDiscountIsZero()
    {
        // Arrange
        var couponDiscountStrategy = new CouponDiscountStrategy(0);
        
        // Act
        var discount = couponDiscountStrategy.CalculateDiscount(100);

        // Assert
        Assert.Equal(0, discount);
    }

    [Fact]
    public void CalculateDiscount_ReturnsCorrectDiscount_WhenAmountAndDiscountAreValid()
    {
        // Arrange
        var couponDiscountStrategy = new CouponDiscountStrategy(10);
        
        // Act
        var discount = couponDiscountStrategy.CalculateDiscount(100);

        // Assert
        Assert.Equal(10, discount);
    }

    [Fact]
    public void CalculateDiscount_ReturnsZero_WhenAmountIsNegative()
    {
        // Arrange
        var couponDiscountStrategy = new CouponDiscountStrategy(10);
        
        // Act
        var discount = couponDiscountStrategy.CalculateDiscount(-100);

        // Assert
        Assert.Equal(0, discount);
    }

    [Fact]
    public void CalculateDiscount_ReturnsZero_WhenDiscountIsNegative()
    {
        // Arrange
        var couponDiscountStrategy = new CouponDiscountStrategy(-10);
        
        // Act
        var discount = couponDiscountStrategy.CalculateDiscount(100);

        // Assert
        Assert.Equal(0, discount);
    }
}