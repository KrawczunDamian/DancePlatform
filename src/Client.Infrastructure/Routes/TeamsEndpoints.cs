namespace DancePlatform.Client.Infrastructure.Routes
{
    public static class TeamsEndpoints
    {
        public static string ExportFiltered(string searchString)
        {
            return $"{Export}?searchString={searchString}";
        }

        public static string Export = "api/v1/teams/export";
        public static string GetProfilePicture = "api/v1/teams/GetProfilePicture";
        public static string UpdateProfilePicture = "api/v1/teams/updateProfilePicture";
        

        public static string GetAll = "api/v1/teams";
        public static string GetById = "api/v1/teams";
        public static string Delete = "api/v1/teams";
        public static string Save = "api/v1/teams";
        public static string GetCount = "api/v1/teams/count";
    }
}
