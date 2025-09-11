using CapeTownMunicipalityApp.Models;
using CapeTownMunicipalityApp.Services;
using SQLitePCL;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;

Batteries.Init();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();
// Multi-Language Support

var supportedCultures = new[]
{
    new CultureInfo("en"),// English
    new CultureInfo("af"),// Afrikaans
    new CultureInfo("xh"),// isiXhosa
    new CultureInfo("zu"),// isiZulu

    // To be implemented later
    //new CultureInfo("ns"),// isiNdebele
    //new CultureInfo("st"),// Sesotho
    //new CultureInfo("nso"),// Sepedi
    //new CultureInfo("tn"),// Setswana
    //new CultureInfo("ss"),// siSwati
    //new CultureInfo("ve"),// Tshivenda
    //new CultureInfo("ts")// Xitsonga
};
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("en");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    options.RequestCultureProviders.Insert(0, new CookieRequestCultureProvider());
});


builder.Services.AddDbContext<LocalDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IReportService, ReportService>();
var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LocalDbContext>();
    db.Database.Migrate();
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();



app.UseRouting();
app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
app.Use(async (context, next) =>
{
    var rqf = context.Features.Get<IRequestCultureFeature>();
    var culture = rqf?.RequestCulture.Culture.Name ?? "null";
    Console.WriteLine($" Current Culture: {culture}");
    await next.Invoke();
});

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


    app.Run();
