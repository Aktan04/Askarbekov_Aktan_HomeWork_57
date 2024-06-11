using System.Globalization;
using System.IO.Compression;
using Hw57.Models;
using Hw57.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddControllersWithViews().AddViewLocalization();
builder.Services.AddControllersWithViews();
string connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<MyTaskContext>(options => options.UseNpgsql(connection))
    .AddIdentity<User, IdentityRole<int>>(options =>
    {
        options.Password.RequiredLength = 5;   // минимальная длина
        options.Password.RequireNonAlphanumeric = false;   // требуются ли не алфавитно-цифровые символы
        options.Password.RequireLowercase = false; // требуются ли символы в нижнем регистре
        options.Password.RequireUppercase = false; // требуются ли символы в верхнем регистре
        options.Password.RequireDigit = false; // требуются ли цифры
    })
    .AddEntityFrameworkStores<MyTaskContext>();
builder.Services.AddTransient<TaskService>();
builder.Services.AddMemoryCache();
builder.Services.AddResponseCompression(options => options.EnableForHttps = true);
builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Optimal;
});
var app = builder.Build();
app.UseResponseCompression();
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var userManager = services.GetRequiredService<UserManager<User>>();
    var rolesManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();
    await AdminInitializer.SeedAdminUser(rolesManager, userManager);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred while seeding the database.");
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
var supportedCultures = new[]
{
    new CultureInfo("en"),
    new CultureInfo("ru"),
};
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("ru"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=MyTask}/{action=Index}/{id?}");

app.Run();