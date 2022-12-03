using DACHUYENNGANH.Helpers.FileManager;
using DACHUYENNGANH.Interface;
using DACHUYENNGANH.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NuGet.Protocol.Core.Types;
using System;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DAChuyenNganhContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("QLNH")
    ?? throw new InvalidOperationException("Connection string 'QLNH' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.Configure<IdentityOptions>(opt=>
{
    opt.Password.RequiredLength = 5;
    opt.Password.RequireLowercase = true;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(10);
    opt.Lockout.MaxFailedAccessAttempts =5 ;
    opt.SignIn.RequireConfirmedAccount = true;
   
});
builder.Services.AddOptions();
builder.Services.AddTransient<IDatabaseRepo, HSVayDoanhNghiepRepository>();
builder.Services.Configure<AppDbConnection>(opt =>
{
    opt.GetConnectionString = builder.Configuration.GetConnectionString("QLNH");
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(30));
builder.Services.AddTransient<IStorageService, FileStorageService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


//builder.Services.AddTransient<IDatabaseRepo, HSVayDoanhNghiepRepository>();
IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
        webBuilder.UseWebRoot("wwwroot");

    });
builder.Services.AddScoped<IDatabaseRepo, HSVayDoanhNghiepRepository>();
var mvcBuilder = builder.Services.AddRazorPages();

if (builder.Environment.IsDevelopment())
{
    mvcBuilder.AddRazorRuntimeCompilation();
}
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


app.UseAuthorization();

app.UseSession();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapRazorPages();
});

app.MapControllerRoute(

    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=IndexAdmin}/{id?}");
app.MapControllerRoute(
//name: "default",
//pattern: "{controller=Report}/{action=Print}/{id?}");

name: "default",
pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

