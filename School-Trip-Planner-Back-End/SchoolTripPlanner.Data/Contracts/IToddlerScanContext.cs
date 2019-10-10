using Microsoft.EntityFrameworkCore;
using SchoolTripPlanner.Domain.Models;

namespace SchoolTripPlanner.Data.Contracts
{
    public interface IToddlerScanContext
    {
        DbSet<Toddler> Toddlers { get; set; }
        DbSet<Teacher> Teachers { get; set; }
        DbSet<SchoolTrip> SchoolTrips { get; set; }
        DbSet<Class> Classes { get; set; }
        DbSet<ScanToddler> ScanToddlers { get; set; }
        DbSet<Scan> Scans { get; set; }
    }
}