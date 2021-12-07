using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToyStoreWebAppMVC.Data;
using ToyStoreWebAppMVC.Entities;
using ToyStoreWebAppMVC.Models;

namespace ToyStoreWebAppMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserOrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserOrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/UserOrder
        public async Task<IActionResult> Index()
        {
            TransactionModel transactionModel = new TransactionModel();
            var result = await (from userO in _context.UserOrder
                                join order in _context.Order on userO.OrderId equals order.Id
                                join userInfo in _context.Users on userO.UserId equals userInfo.Id
                                select new UserTransaction
                                {
                                    Username = userInfo.UserName,
                                    FirstName = userInfo.FirstName,
                                    LastName = userInfo.LastName,
                                    TransactionDate = order.OrderTime,
                                    Total = order.Total,
                                    OrderId = order.Id
                                }).ToListAsync();
            transactionModel.UserTransactions = result;
            return View(transactionModel);
        }
        public async Task<IActionResult> Details(int id)
        {
            var orderList = await (from orderItem in _context.OrderItem
                                   where orderItem.OrderId == id
                                   select new Toy
                                   {
                                       Name = orderItem.ToyName,
                                       Color = orderItem.ToyColor,
                                       Cost = orderItem.ToyCost,
                                       Quantity = orderItem.Quantity,
                                       Manufacturer = orderItem.ToyManufacturer
                                   }).ToListAsync();
            return View(orderList);
        }
        private bool UserOrderExists(int id)
        {
            return _context.UserOrder.Any(e => e.Id == id);
        }
    }
}
