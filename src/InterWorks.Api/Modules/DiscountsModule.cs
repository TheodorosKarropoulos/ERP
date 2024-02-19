using InterWorks.Api.Requests;
using InterWorks.Discounts.Applications;
using InterWorks.Discounts.Constants;
using InterWorks.Discounts.Models;
using InterWorks.Discounts.Strategies;

namespace InterWorks.Api.Modules;

public static class DiscountsModule
{
    public static void AddDiscountEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/discounts",
            async (ProcessDiscount request, IDiscountProcessor processor) => processor.ProcessDiscounts(MapToOrder(request)));
    }

    private static Order MapToOrder(ProcessDiscount request)
    {
        return new Order
        {
            TotalAmount = request.TotalAmount,
            Discounts = request.Discounts.Select(MapToDiscount).ToList()
        };
    }

    private static IDiscountStrategy? MapToDiscount(OrderDiscount discount) =>
        discount.Name switch
        {
            DiscountName.PriceList => new PriceListDiscountStrategy(discount.Discount),
            DiscountName.Coupon => new CouponDiscountStrategy(discount.Discount),
            DiscountName.Promotion => new PromotionDiscountStrategy(discount.Discount),
            _ => default
        };
}