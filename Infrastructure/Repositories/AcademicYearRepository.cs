﻿using Domain;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AcademicYearRepository : BaseRepository<AcademicYear>, IAcademicYearRepository
    {
        public AcademicYearRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<AcademicYear> GetAsync(int id)
        {
            return await GetAsync(_ => _.Id == id);
        }

        public Task<AcademicYear?> GetCurrentAsync()
        {
            DateTime utcTime = DateTime.UtcNow;
            TimeZoneInfo localTimeZone = TimeZoneInfo.Local;
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, localTimeZone);

            return GetAsync(_ => localTime >= _.OpenDate && localTime < _.ClosureDate);
        }

        public async Task<AcademicYear?> GetLatestAcademicYearAsync()
        {
            return await GetAllQuery()
                .OrderByDescending(y => y.Id)
                .FirstOrDefaultAsync();
        }
        
        public async Task<bool> IsEnableSubmitIdea()
        {
            var currentAcademicYear = await GetCurrentAsync();
            if (currentAcademicYear is null) return false;
            return DateTime.UtcNow < currentAcademicYear.ClosureDate.ToUniversalTime();
        }
    
        public async Task<bool> IsEnableSubmitComment()
        {
            var currentAcademicYear = await GetCurrentAsync();
            if (currentAcademicYear is null) return false;
            return DateTime.UtcNow < currentAcademicYear.FinalClosureDate.ToUniversalTime();
        }
    }
}
