using InterWorks.Discounts.Strategies;

namespace InterWorks.Discounts.Tests.Strategies;

public class PriceListDiscountStrategyTests
{
    [Fact]
    public void CalculateDiscount_ReturnsZero_WhenAmountIsZero()
    {
        // Arrange
        var priceListStrategy = new PriceListDiscountStrategy(10);
        
        // Act
        var discount = priceListStrategy.CalculateDiscount(0);
        
        // Assert
        Assert.Equal(0, discount);
    }

    [Fact]
    public void CalculateDiscount_ReturnsCorrectDiscount_WhenAmountAndDiscountAreValid()
    {
        // Arrange
        var priceListStrategy = new PriceListDiscountStrategy(10);
        
        // Act
        var discount = priceListStrategy.CalculateDiscount(100);

        // Assert
        Assert.Equal(10, discount);
    }

    [Fact]
    public void CalculateDiscount_ReturnsCorrectDiscount_WhenDiscountIsZero()
    {
        // Arrange
        var priceListStrategy = new PriceListDiscountStrategy(0);
        
        // Act
        var discount = priceListStrategy.CalculateDiscount(100);
        
        // Assert
        Assert.Equal(0, discount);
    }

    [Fact]
    public void CalculateDiscount_ReturnsZero_WhenAmountIsNegative()
    {
        // Arrange
        var priceListStrategy = new PriceListDiscountStrategy(10);
        
        // Act
        var discount = priceListStrategy.CalculateDiscount(-100);
        
        // Assert
        Assert.Equal(0, discount);
    }

    [Fact]
    public void CalculateDiscount_ReturnsZero_WhenDiscountIsNegative()
    {
        // Arrange
        var priceListStrategy = new PriceListDiscountStrategy(-10);
        
        // Act
        var discount = priceListStrategy.CalculateDiscount(100);
        
        // Assert
        Assert.Equal(0, discount);
    }
}