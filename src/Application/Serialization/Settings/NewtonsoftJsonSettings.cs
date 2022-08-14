
using DancePlatform.Application.Interfaces.Serialization.Settings;
using Newtonsoft.Json;

namespace DancePlatform.Application.Serialization.Settings
{
    public class NewtonsoftJsonSettings : IJsonSerializerSettings
    {
        public JsonSerializerSettings JsonSerializerSettings { get; } = new();
    }
}