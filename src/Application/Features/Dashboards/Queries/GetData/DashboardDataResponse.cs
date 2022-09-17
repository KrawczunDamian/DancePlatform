using System.Collections.Generic;

namespace DancePlatform.Application.Features.Dashboards.Queries.GetData
{
    public class DashboardDataResponse
    {
        public int TeamCount { get; set; }
        public int DancerCount { get; set; }
        public int UserCount { get; set; }
        public int RoleCount { get; set; }
    }
}