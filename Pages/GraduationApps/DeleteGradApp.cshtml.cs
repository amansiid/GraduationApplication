using GraduationApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GraduationApplication.Pages.GraduationApps
{
    public class DeleteGradAppModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteGradAppModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public StudentGraduationApplication? GradApp { get; set; }

        // Retrieve the academic program for deletion confirmation
        public async Task<IActionResult> OnGetAsync(int id)
        {
            GradApp = await _context.GraduationApplications
                .Include(g => g.ProgramId) 
                .FirstOrDefaultAsync(m => m.ApplicationId == id);

            if (GradApp == null)
            {
                return NotFound();
            }
            return Page();
        }

        // Handle deletion of the academic program
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var applicationToDelete = await _context.GraduationApplications.FindAsync(id);
            if (applicationToDelete == null)
            {
                return NotFound();
            }

            try
            {
                _context.GraduationApplications.Remove(applicationToDelete);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(string.Empty, "Unable to delete the application. Please try again.");
                return Page();
            }

            return RedirectToPage("./GraduationAppIndex"); 
        }
    }
}
