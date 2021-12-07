using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ToyStoreWebAppMVC.Data;
using ToyStoreWebAppMVC.Entities;
using ToyStoreWebAppMVC.Models;

namespace ToyStoreWebAppMVC.Controllers
{
    public class HomeController : Controller
    {
        public readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Payout(ShopModel shopModel)
        {
            shopModel.NotInStock = "[]";
            if (shopModel == null)
            {
                return BadRequest();
            }
            else if (shopModel.Cart == null || shopModel.Cart.Count() == 0)
            {
                ModelState.AddModelError("", "Add toy to cart first!");
                return RedirectToAction("Index", shopModel);
            }
            else
            {
                List<Toy> notInStock = new List<Toy>();
                UserOrder userOrder = new UserOrder();

                Order order = new Order()
                {
                    OrderTime = DateTime.Now,
                    Total = 0m,
                    OrderItem = new List<OrderItem>()
                };

                var total = 0m;
                foreach (Toy toy in shopModel.Cart)
                {
                    var result = await BuyToy(toy.Id);
                    if (result > 0)
                    {
                        var igracka = await GetToyById(toy.Id);
                        total += igracka.Cost;
                        order.OrderItem.Add(new OrderItem()
                        {
                            ToyName = igracka.Name,
                            ToyColor = igracka.Color,
                            ToyCost = igracka.Cost,
                            ToyManufacturer = igracka.Manufacturer,
                            Quantity = 1
                        });
                    }
                    else
                    {
                        //Nema na stanju!
                        notInStock.Add(toy);
                    }

                }
                if (notInStock.Count > 0)
                {
                    shopModel.NotInStock = JsonConvert.SerializeObject(notInStock, Formatting.Indented);
                }
                if (notInStock.Count == shopModel.Cart.Count())
                {
                    return RedirectToAction("Index", shopModel);
                }
                else
                {
                    order.Total = total;
                    _context.Order.Add(order);
                    await _context.SaveChangesAsync();
                    var userId = _userManager.GetUserAsync(User).Result?.Id;
                    userOrder.OrderId = order.Id;
                    userOrder.UserId = userId;
                    _context.UserOrder.Add(userOrder);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", shopModel);
                }
            }
        }

        private async Task<int> BuyToy(int id)
        {
            return await _context.Database.ExecuteSqlRawAsync("UPDATE Toy SET Quantity = Quantity - 1 WHERE " +
                            "Id = " + id + " AND Quantity > 0");
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