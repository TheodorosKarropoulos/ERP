using InterWorks.Discounts.Strategies;

namespace InterWorks.Discounts.Tests.Strategies;

public class PromotionDiscountStrategyTests
{
    [Fact]
    public void CalculateDiscount_ReturnsZero_WhenAmountIsZero()
    {
        // Arrange
        var promotionDiscountStrategy = new PromotionDiscountStrategy(10);
        
        // Act
        var discount = promotionDiscountStrategy.CalculateDiscount(0);
        
        // Assert
        Assert.Equal(0, discount);
    }

    [Fact]
    public void CalculateDiscount_ReturnsCorrectDiscount_WhenAmountAndDiscountAreValid()
    {
        // Arrange
        var promotionDiscountStrategy = new PromotionDiscountStrategy(10);
        
        // Act
        var discount = promotionDiscountStrategy.CalculateDiscount(100);

        // Assert
        Assert.Equal(10, discount);
    }

    [Fact]
    public void CalculateDiscount_ReturnsCorrectDiscount_WhenDiscountIsZero()
    {
        // Arrange
        var promotionDiscountStrategy = new PromotionDiscountStrategy(0);
        
        // Act
        var discount = promotionDiscountStrategy.CalculateDiscount(100);
        
        // Assert
        Assert.Equal(0, discount);
    }

    [Fact]
    public void CalculateDiscount_ReturnsZero_WhenAmountIsNegative()
    {
        // Arrange
        var promotionDiscountStrategy = new PromotionDiscountStrategy(10);
        
        // Act
        var discount = promotionDiscountStrategy.CalculateDiscount(-100);
        
        // Assert
        Assert.Equal(0, discount);
    }

    [Fact]
    public void CalculateDiscount_ReturnsZero_WhenDiscountIsNegative()
    {
        // Arrange
        var promotionDiscountStrategy = new PromotionDiscountStrategy(-10);
        
        // Act
        var discount = promotionDiscountStrategy.CalculateDiscount(100);
        
        // Assert
        Assert.Equal(0, discount);
    }
}