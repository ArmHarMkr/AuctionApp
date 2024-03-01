using AuctionApp.DAL.Data;
using AuctionApp.DAL.Repository;
using AuctionApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

    }
}
