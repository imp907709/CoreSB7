using CoreSBShared.Universal.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreSBShared.Universal.Infrastructure.EF
{
    public class EFcontext : DbContext
    {
        public EFcontext(DbContextOptions<EFcontext> options) : base(options)
        {
        }

        public DbSet<LoggingDAL> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoggingDAL>().ToTable("Logging");
            modelBuilder.Entity<LoggingDAL>().Property(p=>p.Id).ValueGeneratedOnAdd();
        }
    }
}