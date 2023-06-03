using CoreSBBL.Logging.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreSBBL.Logging.Infrastructure.EF
{
    public class LoggingContext : DbContext
    {
        public LoggingContext(DbContextOptions<LoggingContext> options) : base(options)
        {
        }

        public DbSet<LoggingDAL> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoggingDAL>().ToTable("Logging");
            modelBuilder.Entity<LoggingDAL>().Property(p=> p.EfId).ValueGeneratedOnAdd();
            modelBuilder.Entity<LoggingDAL>().HasKey(p => p.EfId).HasName("id");
        }
    }
}
