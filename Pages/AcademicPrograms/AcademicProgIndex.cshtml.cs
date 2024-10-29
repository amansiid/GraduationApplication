using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GraduationApplication.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraduationApplication.Pages.AcademicPrograms
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
            AcademicPrograms = new List<AcademicProgram>(); // Initialize the list
        }

        public IList<AcademicProgram> AcademicPrograms { get; set; }

        // Pagination properties
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }

        public async Task OnGetAsync(int pageNumber = 1)
        {
            int pageSize = 7;
            PageNumber = pageNumber;

            var totalRecords = await _context.AcademicPrograms.CountAsync();
            TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            AcademicPrograms = await _context.AcademicPrograms
                .Skip((PageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
