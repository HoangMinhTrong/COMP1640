namespace Domain.Interfaces
{
    public interface IAcademicYearRepository : IBaseRepository<AcademicYear>
    {
        Task<AcademicYear?> GetLatestAcademicYearAsync();
    }
}