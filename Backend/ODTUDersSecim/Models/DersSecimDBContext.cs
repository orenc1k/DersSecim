using System;
using Microsoft.EntityFrameworkCore;

namespace ODTUDersSecim.Models
{
	public class ODTUDersSecimDBContext:DbContext
	{

        private IConfiguration configuration;

        public ODTUDersSecimDBContext(DbContextOptions<ODTUDersSecimDBContext> options, IConfiguration configuration) : base(options) 
        {
            this.configuration = configuration;
        }

        public DbSet<Departments> Departments { get; set; }
        public DbSet<Subjects> Subjects { get; set; }
        public DbSet<SubjectSections> SubjectSections { get; set; }
        public DbSet<SectionDays> SectionDays { get; set; }
        public DbSet<MustCourses> MustCourses { get; set; }
        public DbSet<AvailableCourses> AvailableCourses { get; set; }
        public DbSet<ElectiveCourses> ElectiveCourses { get; set; }
    }
}

