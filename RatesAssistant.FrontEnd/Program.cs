using RatesAssistant.Application.Queries;
using RatesAssistant.Infrastructure.Persistence;
using RatesAssistant.Infrastructure.Persistence.Sql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddTransient<GetAvailableCurrenciesQuery>();
builder.Services.AddTransient<GetExchangeRateQuery>();
builder.Services.AddTransient<IExchangeRateRepository, ExchangeRateRepository>();

builder.Services.AddDbContext<RatesDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
