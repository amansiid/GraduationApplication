using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GraduationApplication.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GraduationApplication.Pages.GraduationApps
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public StudentGraduationApplication GraduationApplication { get; set; }
        
        [BindProperty]
        public string FirstName { get; set; }
        
        [BindProperty]
        public string LastName { get; set; }
        public List<SelectListItem> AcademicPrograms { get; set; }
        public string SuccessMessage { get; set; }

        public IActionResult OnGet()
        {
            PopulateAcademicPrograms();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string Quarter, int Year)
        {
            //thow an error if name has a number
            if (!string.IsNullOrEmpty(FirstName) && FirstName.Any(char.IsDigit) ||
               !string.IsNullOrEmpty(LastName) && LastName.Any(char.IsDigit))
            {
                ModelState.AddModelError(string.Empty, "Name cannot contain numbers.");
            }

            // Concatenate FirstName and LastName for StudentName
            GraduationApplication.StudentName = $"{FirstName} {LastName}";

            // Combine Quarter and Year for AppliedQuarter
            GraduationApplication.AppliedQuarter = $"{Quarter} {Year}";

            // Remove validation for specific properties
            ModelState.Remove("GraduationApplication.StudentName");
            ModelState.Remove("GraduationApplication.AcademicProgram");
            ModelState.Remove("GraduationApplication.AppliedQuarter");

            if (!ModelState.IsValid)
            {
                // Log errors to the console (or remove this after debugging)
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        Console.WriteLine($"Error: {error.ErrorMessage}");
                    }
                }

                // Repopulate dropdown for academic programs if validation fails
                PopulateAcademicPrograms();
                return Page();
            }

            try
            {
                // Add application to the context
                _context.GraduationApplications.Add(GraduationApplication);
                await _context.SaveChangesAsync();

                SuccessMessage = "Graduation application created successfully.";
                // Clear form data after successful submission
                ModelState.Clear();
                GraduationApplication = new StudentGraduationApplication();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving graduation application: {ex.Message}");
                ModelState.AddModelError(string.Empty, "An error occurred while saving your application.");
            }

            // Repopulate dropdown for academic programs
            PopulateAcademicPrograms();
            return Page();
        }

        private void PopulateAcademicPrograms()
        {
            AcademicPrograms = _context.AcademicPrograms
                .Select(p => new SelectListItem
                {
                    Value = p.ProgramId.ToString(),
                    Text = p.ProgramTitle
                }).ToList();
        }

        private void LogModelStateErrors()
        {
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
        }
    }
}
