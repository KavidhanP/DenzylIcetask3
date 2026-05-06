using LogiTech.Models;
using LogiTech.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LogiTech.Controllers
{
    /// <summary>
    /// Functional Requirement: Centralized Inventory & Warehouse Audit Trail
    /// Student: Adrian Chetty (ST10442488)
    /// </summary>
    public class AuditController : Controller
    {
        private readonly LogiTechDbContext _context;

        public AuditController(LogiTechDbContext context)
        {
            _context = context;
        }

        // GET: Audit
        // Screen 6: Searchable filters above chronological log
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var logs = _context.WarehouseAuditLogs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                logs = logs.Where(l => l.TrackingNumber.Contains(searchString));
            }

            // Chronological order for audit integrity
            return View(await logs.OrderByDescending(l => l.Timestamp).ToListAsync());
        }

        // GET: Audit/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var auditLog = await _context.WarehouseAuditLogs
                .FirstOrDefaultAsync(m => m.LogId == id);

            if (auditLog == null)
                return NotFound();

            return View(auditLog);
        }

        // GET: Audit/LogMovement
        public IActionResult LogMovement()
        {
            return View();
        }

        // POST: Audit/LogMovement
        // Implements Chain of Custody and theft prevention logging
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogMovement(
            [Bind("TrackingNumber,Status,Location,OperatorName,Notes")] WarehouseAudit warehouseAudit)
        {
            // Set timestamp server-side before ModelState runs its final checks
            warehouseAudit.Timestamp = DateTime.Now;
            ModelState.Remove(nameof(warehouseAudit.Timestamp));

            if (!ModelState.IsValid)
                return View(warehouseAudit);

            try
            {
                _context.Add(warehouseAudit);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Movement logged for {warehouseAudit.TrackingNumber}.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Failed to save: {ex.Message}");
                return View(warehouseAudit);
            }
        }

        // GET: Audit/Delete/5
        // Note: In strict audit trails, deletions should be restricted to Admins
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var auditLog = await _context.WarehouseAuditLogs
                .FirstOrDefaultAsync(m => m.LogId == id);

            if (auditLog == null)
                return NotFound();

            return View(auditLog);
        }

        // POST: Audit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var auditLog = await _context.WarehouseAuditLogs.FindAsync(id);

            if (auditLog == null)
                return NotFound();

            try
            {
                _context.WarehouseAuditLogs.Remove(auditLog);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Audit log entry deleted.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Failed to delete: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}