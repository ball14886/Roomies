using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EFGetStarted.AspNetCore.NewDb.Models;
using System;

namespace Roomies.Controllers
{
    public class RoomsController : Controller
    {
        private readonly RoomiesContext _context;
        private long _OfficerID = 0;

        public RoomsController(RoomiesContext context)
        {
            _context = context;
        }

        // GET: Rooms
        public async Task<IActionResult> Index()
        {
            var roomiesContext = _context.Rooms.Include(r => r.Apartment).Include(r => r.RoomType);
            return View(await roomiesContext.ToListAsync());
        }

        // GET: Rooms/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms
                .Include(r => r.Apartment)
                .Include(r => r.RoomType)
                .FirstOrDefaultAsync(m => m.RoomID == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // GET: Rooms/Create
        public IActionResult Create()
        {
            ViewData["ApartmentID"] = new SelectList(_context.Apartments, "ApartmentID", "ApartmentID");
            ViewData["RoomTypeID"] = new SelectList(_context.RoomTypes, "RoomTypeID", "RoomTypeID");
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoomID,RoomNumber,ApartmentID,RoomTypeID,IsRepairRequired,IsBeingRepaired,IsActive,IsDeleted,CreatedDateTime,CreatedBy,UpdatedDateTime,UpdatedBy")] Room room)
        {
            var now = DateTime.Now;
            room.IsActive = true;
            room.IsDeleted = false;
            room.CreatedDateTime = now;
            room.CreatedBy = _OfficerID;
            room.UpdatedDateTime = now;
            room.UpdatedBy = _OfficerID;

            if (ModelState.IsValid)
            {
                _context.Add(room);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApartmentID"] = new SelectList(_context.Apartments, "ApartmentID", "ApartmentID", room.ApartmentID);
            ViewData["RoomTypeID"] = new SelectList(_context.RoomTypes, "RoomTypeID", "RoomTypeID", room.RoomTypeID);
            return View(room);
        }

        // GET: Rooms/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            ViewData["ApartmentID"] = new SelectList(_context.Apartments, "ApartmentID", "ApartmentID", room.ApartmentID);
            ViewData["RoomTypeID"] = new SelectList(_context.RoomTypes, "RoomTypeID", "RoomTypeID", room.RoomTypeID);
            return View(room);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("RoomID,RoomNumber,ApartmentID,RoomTypeID,IsRepairRequired,IsBeingRepaired,IsActive,IsDeleted,CreatedDateTime,CreatedBy,UpdatedDateTime,UpdatedBy")] Room room)
        {
            room.UpdatedBy = _OfficerID;
            room.UpdatedDateTime = DateTime.Now;

            if (id != room.RoomID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(room);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomExists(room.RoomID))
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
            ViewData["ApartmentID"] = new SelectList(_context.Apartments, "ApartmentID", "ApartmentID", room.ApartmentID);
            ViewData["RoomTypeID"] = new SelectList(_context.RoomTypes, "RoomTypeID", "RoomTypeID", room.RoomTypeID);
            return View(room);
        }

        // GET: Rooms/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms
                .Include(r => r.Apartment)
                .Include(r => r.RoomType)
                .FirstOrDefaultAsync(m => m.RoomID == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var room = await _context.Rooms.FindAsync(id);
            room.IsActive = false;
            room.IsDeleted = true;
            room.UpdatedDateTime = DateTime.Now;
            room.UpdatedBy = _OfficerID;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomExists(long id)
        {
            return _context.Rooms.Any(e => e.RoomID == id);
        }
    }
}
