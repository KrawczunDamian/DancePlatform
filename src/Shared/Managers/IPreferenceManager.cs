using DancePlatform.Shared.Settings;
using System.Threading.Tasks;
using DancePlatform.Shared.Wrapper;

namespace DancePlatform.Shared.Managers
{
    public interface IPreferenceManager
    {
        Task SetPreference(IPreference preference);

        Task<IPreference> GetPreference();

        Task<IResult> ChangeLanguageAsync(string languageCode);
    }
}