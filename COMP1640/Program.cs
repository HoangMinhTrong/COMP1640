using COMP1640.Extentions;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using Utilities;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

var builder = WebApplication.CreateBuilder(args);

// For running in Railway
var portVar = Environment.GetEnvironmentVariable("PORT");
if (portVar is { Length: > 0 } && int.TryParse(portVar, out int port))
{
    builder.WebHost.ConfigureKestrel(options =>
    {
        options.ListenAnyIP(port);
    });
}

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

// Load configuration
var configuration = builder.Configuration
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables()
    .Build();

// Add services to the container.
builder.Services
    .AddControllersWithViews()
    .AddRazorRuntimeCompilation();

var services = builder.Services;
services.AddHttpContextAccessor();
services.AddIdentity();
services.AddScoped<IClaimsTransformation, MyClaimsTransformation>();

services
    .AddDatabase(configuration)
    .AddServices()
    .AddRepositoriesBase()
    .AddUnitOfWork();

services
    .AddMailgun(configuration)
    .AddMailkit(configuration);

services.AddMediatR(Assembly.GetExecutingAssembly());

services
    .AddCurrentUserInfo()
    .AddEmailSender();

services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

/*using (var scope = app.Services.CreateScope())
{
    var servicesMigration = scope.ServiceProvider;

    var context = servicesMigration.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
}*/

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();