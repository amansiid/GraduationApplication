using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GraduationApplication.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GraduationApplication.Pages.AcademicPrograms
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AcademicProgram AcademicProgram { get; set; }

        public string SuccessMessage { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Log detailed model state errors
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        Console.WriteLine($"Error: {error.ErrorMessage}");
                    }
                }
                return Page();
            }

            // Check if the program already exists 
            var existingProgram = await _context.AcademicPrograms
                .FirstOrDefaultAsync(p => p.ProgramTitle.ToLower() == AcademicProgram.ProgramTitle.ToLower());

            if (existingProgram != null)
            {
                ModelState.AddModelError(string.Empty, "An academic program with this title already exists.");
                return Page();
            }

            try
            {
                _context.AcademicPrograms.Add(AcademicProgram);
                await _context.SaveChangesAsync();

                SuccessMessage = "Academic Program created successfully!";

                // Clear the form by creating a new instance of AcademicProgram
                AcademicProgram = new AcademicProgram
                {
                    ProgramTitle = string.Empty // Set default value for required property
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving academic program: {ex.Message}");
                ModelState.AddModelError(string.Empty, "An error occurred while saving your input.");
            }

            ModelState.Clear();
            return Page();
        }
    }
}

