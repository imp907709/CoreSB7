using CoreSBBL.Logging.Models;
using CoreSBShared.Universal.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreSBBL.Logging.Infrastructure.EF
{
    public class LogsContext : DbContext
    {
        public LogsContext(DbContextOptions<LogsContext> options) : base(options)
        {
        }

        public virtual DbSet<LogsDALEF> Logging { get; set; }
        public virtual DbSet<LogsLabelDALEF> LoggingLabel { get; set; }
        public virtual DbSet<LogsTagDALEF> LoggingTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            RegisterModel<LogsDALEF>(modelBuilder, "Logs");
            RegisterModel<LogsLabelDALEF>(modelBuilder, "LogsLabel");
            RegisterModel<LogsTagDALEF>(modelBuilder, "LogsTags");
            
            modelBuilder.Entity<LogsDALEF>()
                .HasMany(e => e.Tags)
                .WithMany(e => e.Loggings)
                .UsingEntity("LogsToTags",
                    l => l.HasOne(typeof(LogsTagDALEF)).WithMany().HasForeignKey("TagsId").HasPrincipalKey(nameof(LogsTagDALEF.EfId)),
                    r => r.HasOne(typeof(LogsDALEF)).WithMany().HasForeignKey("LogId").HasPrincipalKey(nameof(LogsDALEF.EfId)),
                    j => j.HasKey("LogId", "TagsId"));
        }
        
        private void RegisterModel<T>(ModelBuilder modelBuilder, string Name) 
            where T : EntityEF
        {
            modelBuilder.Entity<T>().ToTable(Name);
            modelBuilder.Entity<T>().Property(p=> p.EfId).ValueGeneratedOnAdd();
            modelBuilder.Entity<T>().HasKey(p => p.EfId).HasName($"{Name}_Id");
            modelBuilder.Entity<T>().Property(p => p.EfId).HasColumnName("Id");
        }
    }
}
