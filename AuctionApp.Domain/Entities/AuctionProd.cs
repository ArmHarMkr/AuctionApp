using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionApp.Domain.Entities
{
    public class AuctionProd
    {
        [Key]
        public string ProductId { get; set; } = Guid.NewGuid().ToString();
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string? ImagePath { get; set; }
        public double InitialPrice { get; set; }
        [AllowNull]
        public double LastPrice { get; set; }
        public bool IsBought { get; set; } = false;
        public DateTime? ProdCreated { get; set; } = DateTime.Now;
        
        [AllowNull]
        public AppUser? BoughtUser { get; set; }
        [AllowNull]
        public AppUser? LastBidUser { get; set; }
        [AllowNull]
        public double? LastBidPrice { get; set; }
    }

}
