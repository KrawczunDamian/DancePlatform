﻿namespace DancePlatform.Client.Infrastructure.Routes
{
    public static class TeamsEndpoints
    {
        public static string ExportFiltered(string searchString)
        {
            return $"{Export}?searchString={searchString}";
        }

        public static string Export = "api/v1/teams/export";
        public static string GetProfilePicture = "api/v1/teams/getProfilePicture";
        public static string UpdateProfilePicture = "api/v1/teams/updateProfilePicture";
        public static string AddTeamMember = "api/v1/teams/addTeamMember";
        public static string GetTeamMembers = "api/v1/teams/getTeamMembers";
        public static string RemoveMember = "api/v1/teams/removeMember";
        public static string AddMember = "api/v1/teams/addMember";
        public static string UploadTeamPicture = "api/v1/teams/uploadTeamPicture";
        public static string GetTeamGallery = "api/v1/teams/getTeamGallery";
        public static string RemoveTeamPicture = "api/v1/teams/removeTeamPicture";


        public static string GetAll = "api/v1/teams";
        public static string GetById = "api/v1/teams";
        public static string Delete = "api/v1/teams";
        public static string Save = "api/v1/teams";
        public static string GetCount = "api/v1/teams/count";
    }
}
