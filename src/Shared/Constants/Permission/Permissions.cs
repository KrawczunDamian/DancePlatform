using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DancePlatform.Shared.Constants.Permission
{
    public static class Permissions
    {
        public static class Teams
        {
            public const string View = "Permissions.Teams.View";
            public const string Create = "Permissions.Teams.Create";
            public const string Edit = "Permissions.Teams.Edit";
            public const string Delete = "Permissions.Teams.Delete";
            public const string Export = "Permissions.Teams.Export";
            public const string Search = "Permissions.Teams.Search";
        }

        public static class Dancers
        {
            public const string View = "Permissions.Dancers.View";
            public const string Create = "Permissions.Dancers.Create";
            public const string Edit = "Permissions.Dancers.Edit";
            public const string Delete = "Permissions.Dancers.Delete";
            public const string Export = "Permissions.Dancers.Export";
            public const string Search = "Permissions.Dancers.Search";
        }
        public static class Users
        {
            public const string View = "Permissions.Users.View";
            public const string Create = "Permissions.Users.Create";
            public const string Edit = "Permissions.Users.Edit";
            public const string Delete = "Permissions.Users.Delete";
            public const string Export = "Permissions.Users.Export";
            public const string Search = "Permissions.Users.Search";
        }

        public static class Roles
        {
            public const string View = "Permissions.Roles.View";
            public const string Create = "Permissions.Roles.Create";
            public const string Edit = "Permissions.Roles.Edit";
            public const string Delete = "Permissions.Roles.Delete";
            public const string Search = "Permissions.Roles.Search";
        }

        public static class RoleClaims
        {
            public const string View = "Permissions.RoleClaims.View";
            public const string Create = "Permissions.RoleClaims.Create";
            public const string Edit = "Permissions.RoleClaims.Edit";
            public const string Delete = "Permissions.RoleClaims.Delete";
            public const string Search = "Permissions.RoleClaims.Search";
        }

        public static class Communication
        {
            public const string Chat = "Permissions.Communication.Chat";
        }

        public static class Preferences
        {
            public const string ChangeLanguage = "Permissions.Preferences.ChangeLanguage";
        }

        public static class Dashboards
        {
            public const string View = "Permissions.Dashboards.View";
        }
        public static class AuditTrails
        {
            public const string View = "Permissions.AuditTrails.View";
            public const string Export = "Permissions.AuditTrails.Export";
            public const string Search = "Permissions.AuditTrails.Search";
        }
       /// <summary>
       /// Returns a list of Permissions.
       /// </summary>
       /// <returns></returns>
        public static List<string> GetRegisteredPermissions()
        {
            var permssions = new List<string>();
            foreach (var prop in typeof(Permissions).GetNestedTypes().SelectMany(c => c.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)))
            {
                var propertyValue = prop.GetValue(null);
                if (propertyValue is not null)
                    permssions.Add(propertyValue.ToString());
            }
            return permssions;
        }
    }
}