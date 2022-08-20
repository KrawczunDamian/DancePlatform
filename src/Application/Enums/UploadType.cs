using System.ComponentModel;

namespace DancePlatform.Application.Enums
{
    public enum UploadType : byte
    {
        [Description(@"Images\ProfilePictures")]
        ProfilePicture,

        [Description(@"Images\TeamProfilePictures")]
        TeamProfilePicture,

        [Description(@"Images\FederationProfilePictures")]
        FederationProfilePicture,
    }
}