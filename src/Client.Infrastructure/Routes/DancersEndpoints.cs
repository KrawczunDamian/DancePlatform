namespace DancePlatform.Client.Infrastructure.Routes
{
    public static class DancersEndpoints
    {
        public static string ExportFiltered(string searchString)
        {
            return $"{Export}?searchString={searchString}";
        }

        public static string Export = "api/v1/dancers/export";
        public static string GetProfilePicture = "api/v1/dancers/getProfilePicture";
        public static string UpdateProfilePicture = "api/v1/dancers/updateProfilePicture";
        public static string AddDancerMember = "api/v1/dancers/addDancerMember";
        public static string GetDancerMembers = "api/v1/dancers/getDancerMembers";


        public static string GetAll = "api/v1/dancers";
        public static string GetById = "api/v1/dancers";
        public static string Delete = "api/v1/dancers";
        public static string Save = "api/v1/dancers";
        public static string GetCount = "api/v1/dancers/count";
    }
}
