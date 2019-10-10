using Microsoft.EntityFrameworkCore;
using SchoolTripPlanner.Data.Contracts;
using SchoolTripPlanner.Data.Persistence.EntityConfigurations;
using SchoolTripPlanner.Domain.Models;
using SchoolTrip = SchoolTripPlanner.Domain.Models.SchoolTrip;
using Toddler = SchoolTripPlanner.Domain.Models.Toddler;

namespace SchoolTripPlanner.Data.Persistence
{
    public class ToddlerScanContext : DbContext, IToddlerScanContext
    {
        public ToddlerScanContext(DbContextOptions<ToddlerScanContext> options)
            : base(options)
        {
        }

        public DbSet<Toddler> Toddlers { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<SchoolTrip> SchoolTrips { get; set; }
        public DbSet<SchoolTripToddler> SchoolTripToddlers { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<ScanToddler> ScanToddlers { get; set; }
        public DbSet<Scan> Scans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClassConfiguration());
            modelBuilder.ApplyConfiguration(new ScanConfiguration());
            modelBuilder.ApplyConfiguration(new ScanToddlerConfiguration());
            modelBuilder.ApplyConfiguration(new SchoolTripConfiguration());
            modelBuilder.ApplyConfiguration(new SchoolTripToddlerConfiguration());
            modelBuilder.ApplyConfiguration(new TeacherConfiguration());
            modelBuilder.ApplyConfiguration(new ToddlerConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server = (localdb)\\mssqllocaldb; Database = SchoolTripPlannerData; Trusted_Connection = True; ");
            //            optionsBuilder.UseSqlServer(
            //                "Server = tcp:schooltripplannerdbserver.database.windows.net,1433; Initial Catalog = SchoolTripPlanner_db; Persist Security Info = True; User ID = schooltriplanneradmin; Password =123456Azeaze; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = True;");
        }
    }
}