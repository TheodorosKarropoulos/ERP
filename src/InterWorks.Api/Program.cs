using InterWorks.Api.Extensions;
using InterWorks.Api.Modules;
using InterWorks.Discounts.Applications;
using InterWorks.DynamicFields.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApiDependencies();
builder.Services.AddDynamicFieldsDependencies();
builder.Services.AddScoped<IDiscountProcessor, DiscountProcessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.AddCustomerEndpoints();
app.AddCustomerFieldEndpoints();
app.AddCustomerFieldHistoryEndpoints();
app.AddDiscountEndpoints();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int) (TemperatureC / 0.5556);
}