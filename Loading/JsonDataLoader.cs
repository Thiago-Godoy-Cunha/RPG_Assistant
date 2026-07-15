using System.Text.Json;
using System.Text;

namespace RPG_Assistant.Loading;

public static class JsonDataLoader {
    private static readonly Dictionary<string, Dictionary<string, JsonElement>> _cache = new();

    public static Dictionary<string, JsonElement> Load(string filePath, string rootProperty) {
        if (_cache.TryGetValue(filePath, out var cached))
            return cached;

        string json = File.ReadAllText(filePath);
        using JsonDocument doc = JsonDocument.Parse(json);

        var result = doc.RootElement.GetProperty(rootProperty)
            .EnumerateArray()
            .ToDictionary(
                item => item.GetProperty("nome").GetString()!.Replace(" ",""),
                item => item.Clone());

        _cache[filePath] = result;
        return result;
    }
}