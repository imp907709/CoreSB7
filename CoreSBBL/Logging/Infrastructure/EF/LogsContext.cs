using CoreSBBL.Logging.Models.DAL.GN;
using CoreSBBL.Logging.Models.DAL.TC;
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

namespace CoreSBBL.Logging.Infrastructure.GN
{
    public class LogsContextGN : DbContext
    {
        public LogsContextGN(DbContextOptions<LogsContextGN> options) : base(options)
        {
        }
        
        public virtual DbSet<LogsDALEfGn> LoggingGN { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LogsDALEfGn>().HasKey(s => s.Id);
            modelBuilder.Entity<LogsDALEfGn>().ToTable("LogGN");
            modelBuilder.Entity<LogsDALEfGn>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<LogsDALEfGn>().Property(p => p.Id).HasColumnName("Id");

        }

        private void RegisterModel<T>(ModelBuilder modelBuilder, string Name)
            where T :  CoreDalIntg
        {
            modelBuilder.Entity<T>().ToTable(Name);
            modelBuilder.Entity<T>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<T>().HasKey(p => p.Id).HasName($"{Name}_Id");
            modelBuilder.Entity<T>().Property(p => p.Id).HasColumnName("Id");
        }
    }
}

