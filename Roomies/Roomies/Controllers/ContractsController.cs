using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EFGetStarted.AspNetCore.NewDb.Models;

namespace Roomies.Controllers
{
    public class ContractsController : Controller
    {
        private readonly RoomiesContext _context;
        private long _OfficerID = 0;

        public ContractsController(RoomiesContext context)
        {
            _context = context;
        }

        // GET: Contracts
        public async Task<IActionResult> Index()
        {
            var roomiesContext = _context.Contracts.Include(c => c.ContractForm).Include(c => c.Officer).Include(c => c.Room).Include(c => c.Tenant);
            return View(await roomiesContext.ToListAsync());
        }

        // GET: Contracts/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _context.Contracts
                .Include(c => c.ContractForm)
                .Include(c => c.Officer)
                .Include(c => c.Room)
                .Include(c => c.Tenant)
                .FirstOrDefaultAsync(m => m.RoomID == id);
            if (contract == null)
            {
                return NotFound();
            }

            return View(contract);
        }

        // GET: Contracts/Create
        public IActionResult Create()
        {
            ViewData["ContractFormID"] = new SelectList(_context.ContractForms, "ContractFormID", "ContractFormID");
            ViewData["OfficerID"] = new SelectList(_context.Officers, "OfficerID", "OfficerID");
            ViewData["RoomID"] = new SelectList(_context.Rooms, "RoomID", "RoomID");
            ViewData["TenantID"] = new SelectList(_context.Tenants, "TenantID", "TenantID");
            return View();
        }

        // POST: Contracts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoomID,TenantID,ContractFormID,OfficerID,WaterStartUnit,ElectricityStartUnit,DepositAmount,DepositDateTime,ContractStartDate,ContractEndDate,MinimumMonth,IsActive,IsDeleted,CreatedDateTime,CreatedBy,UpdatedDateTime,UpdatedBy")] Contract contract)
        {
            var now = DateTime.Now;
            contract.IsActive = true;
            contract.IsDeleted = false;
            contract.CreatedDateTime = now;
            contract.CreatedBy = _OfficerID;
            contract.UpdatedDateTime = now;
            contract.UpdatedBy = _OfficerID;

            if (ModelState.IsValid)
            {
                _context.Add(contract);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContractFormID"] = new SelectList(_context.ContractForms, "ContractFormID", "ContractFormID", contract.ContractFormID);
            ViewData["OfficerID"] = new SelectList(_context.Officers, "OfficerID", "OfficerID", contract.OfficerID);
            ViewData["RoomID"] = new SelectList(_context.Rooms, "RoomID", "RoomID", contract.RoomID);
            ViewData["TenantID"] = new SelectList(_context.Tenants, "TenantID", "TenantID", contract.TenantID);
            return View(contract);
        }

        // GET: Contracts/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _context.Contracts.FindAsync(id);
            if (contract == null)
            {
                return NotFound();
            }
            ViewData["ContractFormID"] = new SelectList(_context.ContractForms, "ContractFormID", "ContractFormID", contract.ContractFormID);
            ViewData["OfficerID"] = new SelectList(_context.Officers, "OfficerID", "OfficerID", contract.OfficerID);
            ViewData["RoomID"] = new SelectList(_context.Rooms, "RoomID", "RoomID", contract.RoomID);
            ViewData["TenantID"] = new SelectList(_context.Tenants, "TenantID", "TenantID", contract.TenantID);
            return View(contract);
        }

        // POST: Contracts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("RoomID,TenantID,ContractFormID,OfficerID,WaterStartUnit,ElectricityStartUnit,DepositAmount,DepositDateTime,ContractStartDate,ContractEndDate,MinimumMonth,IsActive,IsDeleted,CreatedDateTime,CreatedBy,UpdatedDateTime,UpdatedBy")] Contract contract)
        {
            contract.UpdatedDateTime = DateTime.Now;
            contract.UpdatedBy = _OfficerID;

            if (id != contract.RoomID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contract);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContractExists(contract.RoomID))
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
            ViewData["ContractFormID"] = new SelectList(_context.ContractForms, "ContractFormID", "ContractFormID", contract.ContractFormID);
            ViewData["OfficerID"] = new SelectList(_context.Officers, "OfficerID", "OfficerID", contract.OfficerID);
            ViewData["RoomID"] = new SelectList(_context.Rooms, "RoomID", "RoomID", contract.RoomID);
            ViewData["TenantID"] = new SelectList(_context.Tenants, "TenantID", "TenantID", contract.TenantID);
            return View(contract);
        }

        // GET: Contracts/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _context.Contracts
                .Include(c => c.ContractForm)
                .Include(c => c.Officer)
                .Include(c => c.Room)
                .Include(c => c.Tenant)
                .FirstOrDefaultAsync(m => m.RoomID == id);
            if (contract == null)
            {
                return NotFound();
            }

            return View(contract);
        }

        // POST: Contracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var contract = await _context.Contracts.FindAsync(id);
            contract.IsActive = false;
            contract.IsDeleted = true;
            contract.UpdatedDateTime = DateTime.Now;
            contract.UpdatedBy = _OfficerID;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContractExists(long id)
        {
            return _context.Contracts.Any(e => e.RoomID == id);
        }
    }
}
