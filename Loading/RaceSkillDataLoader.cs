using RPG_Assistant.Config;
using RPG_Assistant.Enums;
using RPG_Assistant.Models;
using System.Text.Json;

namespace RPG_Assistant.Loading;

public static class RaceSkillDataLoader {
    private static Dictionary<string, JsonElement>? _cache;

    private static Dictionary<string, JsonElement> Data() {
        if (_cache != null) return _cache;

        string json = File.ReadAllText(DataPaths.Skills);
        using JsonDocument doc = JsonDocument.Parse(json);

        var result = new Dictionary<string, JsonElement>(StringComparer.OrdinalIgnoreCase);

        foreach (JsonElement wrapper in doc.RootElement.GetProperty("habilidades de raça").EnumerateArray()) {
            foreach (JsonProperty classEntry in wrapper.EnumerateObject()) {
                result[classEntry.Name] = classEntry.Value.Clone();
            }
        }

        _cache = result;
        return result;
    }

    public static JsonElement GetRaw(RaceType type) => Data()[type.ToString()];
    public static List<Skill> GetAbilities(RaceType type) {
        return GetRaw(type)
            .EnumerateArray()
            .Select(item => new Skill(
                Name: item.GetProperty("nome").GetString()!,
                Description: item.GetProperty("descricao").GetString()!,
                Requirement: item.GetProperty("requisito").GetString()!))
            .ToList();
    }
}