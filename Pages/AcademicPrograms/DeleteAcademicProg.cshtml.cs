using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GraduationApplication.Models;
using System.Threading.Tasks;

namespace GraduationApplication.Pages.AcademicPrograms
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public AcademicProgram? AcademicProgram { get; set; } // Declared as nullable

        // Retrieve the academic program for deletion confirmation
        public async Task<IActionResult> OnGetAsync(int id)
        {
            AcademicProgram = await _context.AcademicPrograms.FindAsync(id);

            if (AcademicProgram == null)
            {
                return NotFound();
            }
            return Page();
        }

        // Handle deletion of the academic program
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var programToDelete = await _context.AcademicPrograms.FindAsync(id);
            if (programToDelete == null)
            {
                return NotFound();
            }

            try
            {
                _context.AcademicPrograms.Remove(programToDelete);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(string.Empty, "Unable to delete the program. Please try again.");
                return Page();
            }

            return RedirectToPage("./AcademicProgIndex"); 
        }
    }
}
