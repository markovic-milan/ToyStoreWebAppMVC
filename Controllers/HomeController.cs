using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(ApplicationDbContext context, SignInManager<ApplicationUser> signInManager, ILogger<HomeController> logger, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Payout(ShopModel shopModel)
        {

            if (shopModel == null)
            {
                shopModel.TransactionInValid = "true";
            }
            else if (shopModel.Cart == null || shopModel.Cart.Count() == 0)
            {
                ModelState.AddModelError("", "Add toy to cart first!");
            }

            UserOrder userOrder = new UserOrder();
            List<OrderItem> orderItems = new List<OrderItem>();

            Order order = new Order()
            {
                OrderTime = DateTime.Now,
                Total = 0m,
                OrderItem = new List<OrderItem>()
            };

            var total = 0m;
            foreach (Toy toy in shopModel.Cart)
            {
                var igracka = await GetToyById(toy.Id);

                if (igracka.Quantity > 0)
                {
                    total += igracka.Cost;
                    order.OrderItem.Add(new OrderItem()
                    {
                        Toy = igracka,
                        Quantity = 1
                    });
                }
            }
            order.Total = total;

            _context.Order.Add(order);
            await _context.SaveChangesAsync();
            var userId = _userManager.GetUserAsync(User).Result?.Id;
            userOrder.OrderId = order.Id;
            userOrder.UserId = userId;
            _context.UserOrder.Add(userOrder);
            var res = await _context.SaveChangesAsync();
            if(res > 0)
            {
                shopModel.TransactionInValid = "";
            }
            return RedirectToAction("Index", "Home");

        }
        public async Task<IActionResult> Index(ShopModel shopModel)
        {
            var toys = await GetToys();

            shopModel.Toys = toys;

            return View(shopModel);
        }
        private async Task<Toy> GetToyById(int id)
        {
            var toy = await (from t in _context.Toy
                             where t.Id == id
                             select t).FirstOrDefaultAsync();
            return toy;
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
