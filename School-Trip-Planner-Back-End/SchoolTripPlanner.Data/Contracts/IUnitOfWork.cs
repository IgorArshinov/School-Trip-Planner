namespace SchoolTripPlanner.Data.Contracts
{
    public interface IUnitOfWork
    {
        ITeacherRepository TeacherRepository { get; }
        ISchoolTripRepository SchoolTripRepository { get; }
        IClassRepository ClassRepository { get; }
        IToddlerRepository ToddlerRepository { get; }
        IScanToddlerRepository ScanToddlerRepository { get; set; }
        IScanRepository ScanRepository { get; set; }
        void SaveChanges();
    }
}