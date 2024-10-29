namespace GraduationApplication.Models
{
    public class StudentGraduationApplication
    {
        public int ApplicationId { get; set; }
        public string StudentName { get; set; }
        public int ProgramId { get; set; }
        public string AppliedQuarter { get; set; }

        // Navigation property to related AcademicProgram
        public virtual AcademicProgram AcademicProgram { get; set; }
    }
}
