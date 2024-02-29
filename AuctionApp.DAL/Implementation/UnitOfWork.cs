using AuctionApp.DAL.Data;
using AuctionApp.DAL.Repository;
using AuctionApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace AuctionApp.DAL.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;
        private readonly UserManager<AppUser> _userManager;
        //Addint interfaces
        public IAuctionProdRepository AuctionProd { get; }

        //

        public UnitOfWork(AppDbContext db, UserManager<AppUser> userManager)
        {
            _db = db;
            _userManager = userManager;
            AuctionProd = new AuctionRepository(_db, _userManager);
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
