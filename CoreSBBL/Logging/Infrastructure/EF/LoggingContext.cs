using CoreSBShared.Universal.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreSBShared.Universal.Infrastructure.EF
{
    public class LoggingContext : DbContext
    {
        public LoggingContext(DbContextOptions<LoggingContext> options) : base(options)
        {
        }

        public DbSet<LoggingEF> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoggingEF>().ToTable("Logging");
            modelBuilder.Entity<LoggingEF>().Property(p=> p.EfId).ValueGeneratedOnAdd();
            modelBuilder.Entity<LoggingEF>().HasKey(p => p.EfId).HasName("id");
        }
    }
}