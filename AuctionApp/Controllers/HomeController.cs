using AuctionApp.DAL.Data;
using AuctionApp.DAL.Repository;
using AuctionApp.Domain.Entities;
using AuctionApp.Models;
using AuctionApp.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AuctionApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _db;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<AppUser> _userManager;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, UserManager<AppUser> userManager, AppDbContext db, IEmailSender emailSender)
        {
            _db = db;
            _emailSender = emailSender;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }


        public async Task<IActionResult> Index()
        {
            var prodsFromDb = _db.AuctionProducts.Include(d => d.BoughtUser).Include(d => d.LastBidUser);
            return View(prodsFromDb);
        }

        public async Task<IActionResult> BuyProd(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var prodFromDb = _unitOfWork.AuctionProd.Get(p => p.AuctionProdId == id);
            return View(prodFromDb);
        }

        [HttpPost]
        public async Task<IActionResult> PlaceBid(string id, AuctionProd auctionProd)
        {
            var prodFromDb = await _db.AuctionProducts.FirstOrDefaultAsync(p => p.AuctionProdId == id);
            var currentUser = await _userManager.GetUserAsync(User);
            if (prodFromDb != null)
            {
                prodFromDb.LastBidPrice = auctionProd.LastBidPrice;
                if(prodFromDb.LastBidUser != null)
                {
                    await _emailSender.SendEmailAsync(prodFromDb.LastBidUser.Email, "You bid was dirupted", "Your bid was dirupted, check it");
                }
                prodFromDb.LastBidUser = currentUser;
                prodFromDb.LastPrice = prodFromDb.LastPrice + auctionProd.LastBidPrice;
                if(prodFromDb.LastPrice >= 500)
                {
                    prodFromDb.BoughtUser = prodFromDb.LastBidUser;
                    prodFromDb.IsBought = true;
                    await _emailSender.SendEmailAsync(prodFromDb.BoughtUser.Email, "Buying product", "Congratulations! You bought " + prodFromDb.ProductName + " for " + prodFromDb.LastPrice);
                }
                await _unitOfWork.Save();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
