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

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var toys = await GetToys();

            ToyModel toyModel = new ToyModel();
            toyModel.Toys = toys;


            return View(toyModel);
        }

        private async Task<List<Toy>> GetToys()
        {
            var toys = await (from toy in _context.Toy
                              select new Toy
                              {
                                  Id = toy.Id,
                                  Name = toy.Name,
                                  ThumbnailImagePath = toy.ThumbnailImagePath,
                                  Description = toy.Description
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
