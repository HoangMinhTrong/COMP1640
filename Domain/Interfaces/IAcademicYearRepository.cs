namespace Domain.Interfaces
{
    public interface IAcademicYearRepository : IBaseRepository<AcademicYear>
    {
        Task<AcademicYear> GetAsync(int id);
        Task<AcademicYear?> GetLatestAcademicYearAsync();
        Task<AcademicYear> GetCurrentAsync();
    }
}