using AuctionApp.DAL.Data;
using AuctionApp.DAL.Repository;
using AuctionApp.Domain.Entities;
using AuctionApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AuctionApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _db;
        private readonly UserManager<AppUser> _userManager;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, UserManager<AppUser> userManager, AppDbContext db)
        {
            _db = db;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }


        public async Task<IActionResult> Index()
        {
            IEnumerable<AuctionProd> prodsFromDb = _unitOfWork.AuctionProd.GetAll().ToList();
            return View(prodsFromDb);

            return View();
        }
    }
}
