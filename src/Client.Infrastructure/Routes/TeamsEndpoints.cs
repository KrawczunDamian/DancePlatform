namespace DancePlatform.Client.Infrastructure.Routes
{
    public static class TeamsEndpoints
    {
        public static string ExportFiltered(string searchString)
        {
            return $"{Export}?searchString={searchString}";
        }

        public static string Export = "api/v1/teams/export";

        public static string GetAll = "api/v1/teams";
        public static string Delete = "api/v1/teams";
        public static string Save = "api/v1/teams";
        public static string GetCount = "api/v1/teams/count";
    }
}
