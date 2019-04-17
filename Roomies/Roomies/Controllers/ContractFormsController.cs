using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EFGetStarted.AspNetCore.NewDb.Models;

namespace Roomies.Controllers
{
    public class ContractFormsController : Controller
    {
        private readonly RoomiesContext _context;
        private long _OfficerID = 0;

        public ContractFormsController(RoomiesContext context)
        {
            _context = context;
        }

        // GET: ContractForms
        public async Task<IActionResult> Index()
        {
            return View(await _context.ContractForms.ToListAsync());
        }

        // GET: ContractForms/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contractForm = await _context.ContractForms
                .FirstOrDefaultAsync(m => m.ContractFormID == id);
            if (contractForm == null)
            {
                return NotFound();
            }

            return View(contractForm);
        }

        // GET: ContractForms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContractForms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContractFormID,Name,Description,Picture,IsActive,IsDeleted,CreatedDateTime,CreatedBy,UpdatedDateTime,UpdatedBy")] ContractForm contractForm)
        {
            var now = DateTime.Now;
            contractForm.IsActive = true;
            contractForm.IsDeleted = false;
            contractForm.CreatedDateTime = now;
            contractForm.CreatedBy = _OfficerID;
            contractForm.UpdatedDateTime = now;
            contractForm.UpdatedBy = _OfficerID;

            if (ModelState.IsValid)
            {
                _context.Add(contractForm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contractForm);
        }

        // GET: ContractForms/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contractForm = await _context.ContractForms.FindAsync(id);
            if (contractForm == null)
            {
                return NotFound();
            }
            return View(contractForm);
        }

        // POST: ContractForms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("ContractFormID,Name,Description,Picture,IsActive,IsDeleted,CreatedDateTime,CreatedBy,UpdatedDateTime,UpdatedBy")] ContractForm contractForm)
        {
            contractForm.UpdatedDateTime = DateTime.Now;
            contractForm.UpdatedBy = _OfficerID;

            if (id != contractForm.ContractFormID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contractForm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContractFormExists(contractForm.ContractFormID))
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
            return View(contractForm);
        }

        // GET: ContractForms/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contractForm = await _context.ContractForms
                .FirstOrDefaultAsync(m => m.ContractFormID == id);
            if (contractForm == null)
            {
                return NotFound();
            }

            return View(contractForm);
        }

        // POST: ContractForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var contractForm = await _context.ContractForms.FindAsync(id);
            contractForm.IsActive = false;
            contractForm.IsDeleted = true;
            contractForm.UpdatedDateTime = DateTime.Now;
            contractForm.UpdatedBy = _OfficerID;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContractFormExists(long id)
        {
            return _context.ContractForms.Any(e => e.ContractFormID == id);
        }
    }
}
