using Microsoft.AspNetCore.Identity;
using System.Diagnostics.CodeAnalysis;

namespace AuctionApp.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }    
        public DateTime UserCreatedTime { get; set; } = DateTime.Now;
        [AllowNull]
        public ICollection<AuctionProd> BoughtProducts { get; } = new List<AuctionProd>();
    }
}
