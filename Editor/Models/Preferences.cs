using System.Text.Json.Serialization;

namespace Editor.Models;

public sealed class Preferences
{
    private const int CurrentVersion = 1;

    [JsonPropertyName("version")]
    public int Version { get; set; } = CurrentVersion;

    [JsonPropertyName("defaultLicense")]
    public string? DefaultLicense { get; set; }

    [JsonPropertyName("defaultCopyright")]
    public string? DefaultCopyright { get; set; }

    [JsonPropertyName("githubToken")]
    public string? GitHubToken { get; set; }

    [JsonPropertyName("minifyJson")]
    public bool MinifyJson { get; set; }

    [JsonPropertyName("easterEggs")]
    public bool EasterEggs { get; set; }

    [JsonPropertyName("indentation")]
    public int Indentation { get; set; } = 4;

    [JsonPropertyName("defaultImportRepository")]
    public string? DefaultImportRepository { get; set; }
}

[JsonSourceGenerationOptions]
[JsonSerializable(typeof(Preferences))]
internal sealed partial class PreferencesJsonContext : JsonSerializerContext;
