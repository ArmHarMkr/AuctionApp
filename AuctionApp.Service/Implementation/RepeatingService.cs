using AuctionApp.DAL.Data;
using AuctionApp.DAL.Repository;
using AuctionApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AuctionApp.Service.Implementation
{
    public class RepeatingService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly Random _random = new Random();

        public RepeatingService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var timer = new PeriodicTimer(TimeSpan.FromMinutes(5));
            var deleteTimer = new PeriodicTimer(TimeSpan.FromHours(24));
            while (await timer.WaitForNextTickAsync(stoppingToken) && !stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    await GenerateAuctionProd(unitOfWork, db);
                }
            }
            while(await deleteTimer.WaitForNextTickAsync(stoppingToken) && !stoppingToken.IsCancellationRequested)
            {
                using(var scope = _scopeFactory.CreateScope())
                {
                    var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    await DeleteOldProds(unitOfWork, db);
                }
            }
        }

        public async Task GenerateAuctionProd(IUnitOfWork unitOfWork, AppDbContext db)
        {
            AuctionProd prod = new()
            {
                ProductName = "Mandarin",
                ProductDescription = "Fresh mandarin",
                ImagePath = "E:\\.Net projects\\AuctionApp\\AuctionApp\\wwwroot\\img\\Mandarin.jpg",
                InitialPrice = _random.Next(50, 100),
            };
            prod.LastPrice = prod.InitialPrice;

            await db.AuctionProducts.AddAsync(prod);
            await db.SaveChangesAsync();
        }

        public async Task DeleteOldProds(IUnitOfWork unitOfWork, AppDbContext db)
        {
            var thresholdDate = DateTime.Now.AddHours(-24);
            var oldProducts = await db.AuctionProducts.Where(p => p.ProdCreated < thresholdDate).ToListAsync();

            db.AuctionProducts.RemoveRange(oldProducts);
            await db.SaveChangesAsync();
        }

    }


}
