using System.Text.Json;
using RPG_Assistant.Config;
using RPG_Assistant.Enums;

namespace RPG_Assistant.Loading;

public static class ItemDataLoader {
    private static Dictionary<string, (ItemType Type, JsonElement Raw)>? _cache;

    private static Dictionary<string, (ItemType Type, JsonElement Raw)> Data() {
        if (_cache != null) return _cache;

        string json = File.ReadAllText(DataPaths.Items);
        using JsonDocument doc = JsonDocument.Parse(json);

        var result = new Dictionary<string, (ItemType, JsonElement)>(StringComparer.OrdinalIgnoreCase);

        foreach (JsonElement categoryWrapper in doc.RootElement.GetProperty("itens").EnumerateArray()) {
            foreach (JsonProperty category in categoryWrapper.EnumerateObject()) {
                foreach (JsonElement item in category.Value.EnumerateArray()) {
                    string name = item.GetProperty("nome").GetString()
                        ?? throw new InvalidDataException("Item sem 'nome' no JSON.");

                    string typeRaw = item.GetProperty("tipo").GetString()
                        ?? throw new InvalidDataException($"Item '{name}' sem 'tipo' no JSON.");

                    if (!Enum.TryParse(typeRaw, ignoreCase: true, out ItemType type))
                        throw new InvalidDataException($"Tipo de item desconhecido: '{typeRaw}' (item '{name}').");

                    result[name] = (type, item.Clone());
                }
            }
        }

        _cache = result;
        return result;
    }

    public static (ItemType Type, JsonElement Raw) GetRaw(string name) {
        if (!Data().TryGetValue(name, out var entry))
            throw new KeyNotFoundException($"Item '{name}' não encontrado.");
        return entry;
    }
}