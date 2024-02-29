using AuctionApp.DAL.Data;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using AuctionApp.DAL.Repository;
using AuctionApp.DAL.Implementation;
using AuctionApp.Domain.Entities;
using System.Threading;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddRazorPages();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuctionProdRepository, AuctionRepository>();

var app = builder.Build();

// Create a timer that calls GenerateAuctionProd every 5 seconds
var timer = new Timer(async _ =>
{
    using (var scope = app.Services.CreateScope())
    {
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        await unitOfWork.AuctionProd.GenerateAuctionProd();
    }
}, null, TimeSpan.Zero, TimeSpan.FromSeconds(600));


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

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
