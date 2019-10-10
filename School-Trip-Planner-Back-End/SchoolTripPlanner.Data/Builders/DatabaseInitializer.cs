using System.Linq;
using SchoolTripPlanner.Data.Persistence;

namespace SchoolTripPlanner.Data.Builders
{
    public class DatabaseInitializer
    {
        public static void Initialize(ToddlerScanContext context)
        {
            context.Database.EnsureCreated();
//            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('ToddlerScanData.dbo.Classes', RESEED, 0)");

            if (!context.Teachers.Any())
            {
                context.Teachers.Add(new TeacherBuilder().Build());
            }

            context.SaveChanges();

            if (!context.Classes.Any())
            {
                context.Classes.Add(new ClassBuilder().Build());
            }
            else if (context.Classes.Any(t => t.Id == 1))
            {
//                context.Classes.Update(new ClassBuilder().WithId(1).Build());
            }

            context.SaveChanges();

            if (!context.Toddlers.Any())
            {
                context.Toddlers.Add(new ToddlerBuilder().WithClassId(1).Build());
            }
            else if (context.Toddlers.Any(t => t.Id == 1))
            {
//                context.Toddlers.Update(new ToddlerBuilder().WithId(1).WithClassId(1).Build());
            }

            context.SaveChanges();

            if (!context.SchoolTrips.Any())
            {
                context.SchoolTrips.Add(new SchoolTripBuilder().WithTeacherId(1).Build());
            }

            context.SaveChanges();
        }
    }
}