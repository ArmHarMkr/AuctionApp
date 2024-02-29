using AuctionApp.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionApp.DAL.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {   
        }
        public DbSet<AuctionProd> AuctionProducts { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AuctionProd>()
                .HasOne(a => a.LastBidUser)
                .WithMany()
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<AuctionProd>()
                .HasOne(a => a.BoughtUser)
                .WithMany()
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientSetNull);
        }


    }
}
