using System.Linq;
using DancePlatform.Shared.Constants.Localization;
using DancePlatform.Shared.Settings;

namespace DancePlatform.Server.Settings
{
    public record ServerPreference : IPreference
    {
        public string LanguageCode { get; set; } = LocalizationConstants.SupportedLanguages.FirstOrDefault()?.Code ?? "en-US";
    }
}