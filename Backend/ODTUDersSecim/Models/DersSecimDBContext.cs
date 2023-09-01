using System;
using Microsoft.EntityFrameworkCore;

namespace ODTUDersSecim.Models
{
	public class ODTUDersSecimDBContext:DbContext
	{

        private IConfiguration configuration;

        public ODTUDersSecimDBContext(DbContextOptions options, IConfiguration configuration) : base(options) 
        {
            this.configuration = configuration;
        }

        public DbSet<CengSubjects> CengSubjects { get; set; }
        public DbSet<AEESubjects> AEESubjects { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<Subjects> Subjects { get; set; }
        public DbSet<SubjectSections> SubjectSections { get; set; }
    }
}

