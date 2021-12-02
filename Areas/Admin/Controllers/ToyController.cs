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

namespace ToyStoreWebAppMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ToyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ToyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Toy
        public async Task<IActionResult> Index()
        {
            return View(await _context.Toy.ToListAsync());
        }

        // GET: Admin/Toy/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toy = await _context.Toy
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toy == null)
            {
                return NotFound();
            }

            return View(toy);
        }

        // GET: Admin/Toy/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Toy/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Color,Category,Name,Description,ThumbnailImagePath,Manufacturer")] Toy toy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(toy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(toy);
        }

        // GET: Admin/Toy/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toy = await _context.Toy.FindAsync(id);
            if (toy == null)
            {
                return NotFound();
            }
            return View(toy);
        }

        // POST: Admin/Toy/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Color,Category,Name,Description,ThumbnailImagePath,Manufacturer")] Toy toy)
        {
            if (id != toy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(toy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToyExists(toy.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(toy);
        }

        // GET: Admin/Toy/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toy = await _context.Toy
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toy == null)
            {
                return NotFound();
            }

            return View(toy);
        }

        // POST: Admin/Toy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var toy = await _context.Toy.FindAsync(id);
            _context.Toy.Remove(toy);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToyExists(int id)
        {
            return _context.Toy.Any(e => e.Id == id);
        }
    }
}
