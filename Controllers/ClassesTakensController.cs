using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolManagementApp.MVC.Data;

namespace SchoolManagementApp.MVC.Controllers
{
    public class ClassesTakensController : Controller
    {
        private readonly SchoolManagementDbContext _context;

        public ClassesTakensController(SchoolManagementDbContext context)
        {
            _context = context;
        }

        // GET: ClassesTakens
        public async Task<IActionResult> Index()
        {
            var schoolManagementDbContext = _context.ClassesTakens.Include(c => c.Course).Include(c => c.Lecturer);
            return View(await schoolManagementDbContext.ToListAsync());
        }

        // GET: ClassesTakens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ClassesTakens == null)
            {
                return NotFound();
            }

            var classesTaken = await _context.ClassesTakens
                .Include(c => c.Course)
                .Include(c => c.Lecturer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (classesTaken == null)
            {
                return NotFound();
            }

            return View(classesTaken);
        }

        // GET: ClassesTakens/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Lecturers, "Id", "Id");
            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "Id", "Id");
            return View();
        }

        // POST: ClassesTakens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LecturerId,CourseId,Time")] ClassesTaken classesTaken)
        {
            if (ModelState.IsValid)
            {
                _context.Add(classesTaken);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Lecturers, "Id", "Id", classesTaken.CourseId);
            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "Id", "Id", classesTaken.LecturerId);
            return View(classesTaken);
        }

        // GET: ClassesTakens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ClassesTakens == null)
            {
                return NotFound();
            }

            var classesTaken = await _context.ClassesTakens.FindAsync(id);
            if (classesTaken == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Lecturers, "Id", "Id", classesTaken.CourseId);
            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "Id", "Id", classesTaken.LecturerId);
            return View(classesTaken);
        }

        // POST: ClassesTakens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LecturerId,CourseId,Time")] ClassesTaken classesTaken)
        {
            if (id != classesTaken.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classesTaken);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassesTakenExists(classesTaken.Id))
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
            ViewData["CourseId"] = new SelectList(_context.Lecturers, "Id", "Id", classesTaken.CourseId);
            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "Id", "Id", classesTaken.LecturerId);
            return View(classesTaken);
        }

        // GET: ClassesTakens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ClassesTakens == null)
            {
                return NotFound();
            }

            var classesTaken = await _context.ClassesTakens
                .Include(c => c.Course)
                .Include(c => c.Lecturer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (classesTaken == null)
            {
                return NotFound();
            }

            return View(classesTaken);
        }

        // POST: ClassesTakens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ClassesTakens == null)
            {
                return Problem("Entity set 'SchoolManagementDbContext.ClassesTakens'  is null.");
            }
            var classesTaken = await _context.ClassesTakens.FindAsync(id);
            if (classesTaken != null)
            {
                _context.ClassesTakens.Remove(classesTaken);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassesTakenExists(int id)
        {
          return (_context.ClassesTakens?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
