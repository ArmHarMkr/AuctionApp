using AuctionApp.DAL.Data;
using AuctionApp.DAL.Repository;
using AuctionApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionApp.DAL.Implementation
{
    public class AuctionRepository : Repository<AuctionProd>, IAuctionProdRepository
    {
        private AppDbContext _db;
        private readonly UserManager<AppUser> _userManager;
        public AuctionRepository(AppDbContext db, UserManager<AppUser> userManager) : base(db)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task GenerateAuctionProd()
        {
            Random rnd = new Random();
            AuctionProd prod = new()
            {
                ProductName = "Mandarin",
                ProductDescription = "Fresh mandarin",
                ImagePath = "E:\\.Net projects\\AuctionApp\\AuctionApp\\wwwroot\\img\\Mandarin.jpg",
                InitialPrice = rnd.Next(50, 100)
            };
            await _db.AuctionProducts.AddAsync(prod);
            await _db.SaveChangesAsync();
        }

    }
}
