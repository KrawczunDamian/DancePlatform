using DancePlatform.Application.Interfaces.Services;
using System;

namespace DancePlatform.Infrastructure.Shared.Services
{
    public class SystemDateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}