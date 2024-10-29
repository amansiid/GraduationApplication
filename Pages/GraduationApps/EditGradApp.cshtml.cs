using GraduationApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GraduationApplication.Pages.GraduationApps
{
    public class EditGradAppModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditGradAppModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public StudentGraduationApplication GraduationApplication { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            GraduationApplication = await _context.GraduationApplications.FindAsync(id);

            if (GraduationApplication == null)
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

            _context.Attach(GraduationApplication).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GraduationApplicationExists(GraduationApplication.ApplicationId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./GraduationAppIndex");
        }

        private bool GraduationApplicationExists(int id)
        {
            return _context.GraduationApplications.Any(e => e.ApplicationId == id);
        }
    }
}
