using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EFGetStarted.AspNetCore.NewDb.Models;
using System;

namespace Roomies.Controllers
{
    public class OfficersController : Controller
    {
        private readonly RoomiesContext _context;
        private long _OfficerID = 0;

        public OfficersController(RoomiesContext context)
        {
            _context = context;
        }

        // GET: Officers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Officers.ToListAsync());
        }

        // GET: Officers/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var officer = await _context.Officers
                .FirstOrDefaultAsync(m => m.OfficerID == id);
            if (officer == null)
            {
                return NotFound();
            }

            return View(officer);
        }

        // GET: Officers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Officers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OfficerID,FirstName,MiddleName,LastName,TaxIdentification,PassPortNumber,DateOfBirth,Address,ContactNumber,ContactNumber2,Note,IsActive,IsDeleted,CreatedDateTime,CreatedBy,UpdatedDateTime,UpdatedBy")] Officer officer)
        {
            var now = DateTime.Now;
            officer.IsActive = true;
            officer.IsDeleted = false;
            officer.CreatedDateTime = now;
            officer.CreatedBy = _OfficerID;
            officer.UpdatedDateTime = now;
            officer.UpdatedBy = _OfficerID;

            if (ModelState.IsValid)
            {
                _context.Add(officer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(officer);
        }

        // GET: Officers/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var officer = await _context.Officers.FindAsync(id);
            if (officer == null)
            {
                return NotFound();
            }
            return View(officer);
        }

        // POST: Officers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("OfficerID,FirstName,MiddleName,LastName,TaxIdentification,PassPortNumber,DateOfBirth,Address,ContactNumber,ContactNumber2,Note,IsActive,IsDeleted,CreatedDateTime,CreatedBy,UpdatedDateTime,UpdatedBy")] Officer officer)
        {
            officer.UpdatedDateTime = DateTime.Now;
            officer.UpdatedBy = _OfficerID;

            if (id != officer.OfficerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(officer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfficerExists(officer.OfficerID))
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
            return View(officer);
        }

        // GET: Officers/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var officer = await _context.Officers
                .FirstOrDefaultAsync(m => m.OfficerID == id);
            if (officer == null)
            {
                return NotFound();
            }

            return View(officer);
        }

        // POST: Officers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var officer = await _context.Officers.FindAsync(id);

            officer.IsActive = false;
            officer.IsDeleted = true;
            officer.UpdatedDateTime = DateTime.Now;
            officer.UpdatedBy = _OfficerID;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OfficerExists(long id)
        {
            return _context.Officers.Any(e => e.OfficerID == id);
        }
    }
}
