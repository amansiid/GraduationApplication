using GraduationApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GraduationApplication.Pages.AcademicPrograms
{
    public class EditAcademicProgModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditAcademicProgModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AcademicProgram AcademicProgram { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            AcademicProgram = await _context.AcademicPrograms.FindAsync(id);

            if (AcademicProgram == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(AcademicProgram).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AcademicProgramExists(AcademicProgram.ProgramId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./AcademicProgIndex");
        }

        private bool AcademicProgramExists(int id)
        {
            return _context.AcademicPrograms.Any(e => e.ProgramId == id);
        }
    }
}
