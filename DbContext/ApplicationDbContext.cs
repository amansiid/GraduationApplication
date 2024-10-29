using GraduationApplication.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<AcademicProgram> AcademicPrograms { get; set; }
    public DbSet<StudentGraduationApplication> GraduationApplications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AcademicProgram>().HasKey(p => p.ProgramId);

        modelBuilder.Entity<StudentGraduationApplication>()
            .HasKey(p => p.ApplicationId);

        modelBuilder.Entity<StudentGraduationApplication>()
            .HasOne(g => g.AcademicProgram)
            .WithMany(a => a.GraduationApplications)
            .HasForeignKey(g => g.ProgramId);  // Defines the foreign key
    }
}
