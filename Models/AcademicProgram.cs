namespace GraduationApplication.Models
{
    public class AcademicProgram
    {
        public int ProgramId { get; set; }  // Primary Key
        public required string ProgramTitle { get; set; }

        // Navigation property
        public ICollection<StudentGraduationApplication>? 
            GraduationApplications { get; set; } 
    }
}
