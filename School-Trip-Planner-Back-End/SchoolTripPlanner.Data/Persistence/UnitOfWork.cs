using AutoMapper;
using SchoolTripPlanner.Data.Contracts;
using SchoolTripPlanner.Data.Repositories;

namespace SchoolTripPlanner.Data.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ToddlerScanContext _context;
        public ITeacherRepository TeacherRepository { get; private set; }
        public ISchoolTripRepository SchoolTripRepository { get; }
        public IClassRepository ClassRepository { get; }
        public IToddlerRepository ToddlerRepository { get; }
        public IScanToddlerRepository ScanToddlerRepository { get; set; }
        public IScanRepository ScanRepository { get; set; }

        public UnitOfWork(ToddlerScanContext context, IMapper mapper)
        {
            _context = context;
            TeacherRepository = new TeacherRepository(context, mapper);
            SchoolTripRepository = new SchoolTripRepository(context, mapper);
            ClassRepository = new ClassRepository(context);
            ToddlerRepository = new ToddlerRepository(context);
            ScanToddlerRepository = new ScanToddlerRepository(context);
            ScanRepository = new ScanRepository(context);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}