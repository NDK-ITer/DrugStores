using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DrugStore.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddAuthentication().AddGoogle(googleOptions =>
{
    googleOptions.ClientId = configuration["Authentication:Google:ClientId"];
    googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"];
});

services.AddAuthentication().AddFacebook(facebookOptions =>
{
    facebookOptions.AppId = configuration["Authentication:Facebook:AppId"];
    facebookOptions.AppSecret = configuration["Authentication:Facebook:AppSecret"];
});


var connectionString = builder.Configuration.GetConnectionString("DrugStoreDbContextConnection") ?? throw new InvalidOperationException("Connection string 'DrugStoreDbContextConnection' not found.");

builder.Services.AddDbContext<ApplicationsDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<AppNetUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationsDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


var app = builder.Build();

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "MyArea",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();
