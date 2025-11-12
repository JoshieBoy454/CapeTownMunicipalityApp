// Main program file - this is where everything starts up
using CapeTownMunicipalityApp.Models;
using CapeTownMunicipalityApp.Services;
using SQLitePCL;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;

// Initialize SQLite (needed for the database)
Batteries.Init();

// Build the web application
var builder = WebApplication.CreateBuilder(args);

// Set up localization (multi-language support)
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// Add MVC services with localization support
builder.Services.AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();

// Set up supported languages
var supportedCultures = new[]
{
    new CultureInfo("en"), // English
    new CultureInfo("af"), // Afrikaans
    new CultureInfo("xh"), // isiXhosa
    new CultureInfo("zu"), // isiZulu
};
// Configure localization options
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("en"); // Default to English
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    options.RequestCultureProviders.Insert(0, new CookieRequestCultureProvider()); // Save language choice in cookie
});

// Set up database connection
builder.Services.AddDbContext<LocalDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register our custom services
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IServiceRequestService, ServiceRequestService>();

// Build the app
var app = builder.Build();


// Make sure database is up to date (run migrations)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LocalDbContext>();
    db.Database.Migrate(); // This creates/updates the database tables
}

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    // In production, show a nice error page instead of detailed errors
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Tell browsers to use HTTPS
}

// Enable HTTPS redirection and static files
app.UseHttpsRedirection();
app.UseStaticFiles(); // Serve CSS, JS, images, etc.

// Set up routing
app.UseRouting();

// Enable localization
app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

// Debug middleware to see what language is being used
app.Use(async (context, next) =>
{
    var rqf = context.Features.Get<IRequestCultureFeature>();
    var culture = rqf?.RequestCulture.Culture.Name ?? "null";
    Console.WriteLine($" Current Culture: {culture}"); // Print current language to console
    await next.Invoke();
});

// Enable authorization
app.UseAuthorization();

// Set up default routing (Home/Index is the default page)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Start the application
app.Run();
