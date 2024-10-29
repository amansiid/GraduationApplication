using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GraduationApplication.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationApplication.Pages.GraduationApps
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
            Applications = new List<StudentGraduationApplication>();
        }

        public IList<StudentGraduationApplication> Applications { get; set; }

        // Pagination properties
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }

        public async Task OnGetAsync(int pageNumber = 1)
        {
            int pageSize = 7; // Set your preferred page size
            PageNumber = pageNumber;

            var totalRecords = await _context.GraduationApplications.CountAsync();
            TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            Applications = await _context.GraduationApplications
                .Include(g => g.AcademicProgram)
                .Skip((PageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
