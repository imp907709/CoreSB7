using CoreSBBL.Logging.Models.DAL.GN;
using CoreSBBL.Logging.Models.DAL.TS;
using CoreSBShared.Universal.Infrastructure.EF;
using CoreSBShared.Universal.Infrastructure.Interfaces;
using CoreSBShared.Universal.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreSBBL.Logging.Infrastructure.TS
{
    public class LogsContextTC : DbContext
    {
        public LogsContextTC(DbContextOptions<LogsContextTC> options) : base(options)
        {
        }

        public virtual DbSet<LogsDALEf> Logging { get; set; }
        public virtual DbSet<LabelDalIntTc> LoggingLabel { get; set; }
        public virtual DbSet<LogsTagDALEfTc> LoggingTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            RegisterModel<LogsDALEf>(modelBuilder, "Logs");
            RegisterModel<LabelDalIntTc>(modelBuilder, "LogsLabel");
            RegisterModel<LogsTagDALEfTc>(modelBuilder, "LogsTags");

            modelBuilder.Entity<LogsDALEf>()
                .HasMany(e => e.Tags)
                .WithMany(e => e.Loggings)
                .UsingEntity("LogsToTags",
                    l => l.HasOne(typeof(LogsTagDALEfTc)).WithMany().HasForeignKey("TagsId")
                        .HasPrincipalKey(nameof(LogsTagDALEfTc.Id)),
                    r => r.HasOne(typeof(LogsDALEf)).WithMany().HasForeignKey("LogId")
                        .HasPrincipalKey(nameof(LogsDALEf.Id)),
                    j => j.HasKey("LogId", "TagsId"));
        }

        private void RegisterModel<T>(ModelBuilder modelBuilder, string Name)
            where T : CoreDalint
        {
            modelBuilder.Entity<T>().ToTable(Name);
            modelBuilder.Entity<T>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<T>().HasKey(p => p.Id).HasName($"{Name}_Id");
            modelBuilder.Entity<T>().Property(p => p.Id).HasColumnName("Id");
        }
    }
}

//Generic ids
namespace CoreSBBL.Logging.Infrastructure.GN
{
    public class LogsContextGN : DbContext
    {
        public LogsContextGN(DbContextOptions<LogsContextGN> options) : base(options)
        {
        }
        
        public virtual DbSet<LoggingDalEfInt> LoggingGN { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoggingDalEfInt>().HasKey(s => s.Id);
            modelBuilder.Entity<LoggingDalEfInt>().ToTable("LogGN");
            modelBuilder.Entity<LoggingDalEfInt>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<LoggingDalEfInt>().Property(p => p.Id).HasColumnName("Id");

        }

        private void RegisterModel<T>(ModelBuilder modelBuilder, string Name)
            where T :  CoreDalGnInt
        {
            modelBuilder.Entity<T>().ToTable(Name);
            modelBuilder.Entity<T>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<T>().HasKey(p => p.Id).HasName($"{Name}_Id");
            modelBuilder.Entity<T>().Property(p => p.Id).HasColumnName("Id");
        }
    }
}

