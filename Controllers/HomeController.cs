using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ToyStoreWebAppMVC.Data;
using ToyStoreWebAppMVC.Entities;
using ToyStoreWebAppMVC.Models;

namespace ToyStoreWebAppMVC.Controllers
{
    public class HomeController : Controller
    {
        public readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;
        private static List<Toy> cart = new List<Toy>();
        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            var toy = await _context.Toy
                 .FirstOrDefaultAsync(m => m.Id == id);
            if (toy == null)
            {
                //Error
            }
            else
            {
                cart.Add(toy);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Remove(int id)
        {
            cart.RemoveAll(i => i.Id == id);

            return PartialView("_PayoutPartial", new ShopModel { Cart = cart });
        }
        public async Task<IActionResult> Payout()
        {
            decimal cijena = 0m;
            foreach (Toy t in cart)
            {
                cijena += t.Cost;

            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Index(ShopModel shopModel)
        {
            var toys = await GetToys();

            shopModel.Toys = toys;
            shopModel.Cart = cart;

            return View(shopModel);
        }

        private async Task<List<Toy>> GetToys()
        {
            var toys = await (from toy in _context.Toy
                              select new Toy
                              {
                                  Id = toy.Id,
                                  Name = toy.Name,
                                  ThumbnailImagePath = toy.ThumbnailImagePath,
                                  Description = toy.Description,
                                  Cost = toy.Cost
                              }).ToListAsync();

            return toys;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
