using System.Text.Json;
using DancePlatform.Application.Interfaces.Serialization.Options;

namespace DancePlatform.Application.Serialization.Options
{
    public class SystemTextJsonOptions : IJsonSerializerOptions
    {
        public JsonSerializerOptions JsonSerializerOptions { get; } = new();
    }
}