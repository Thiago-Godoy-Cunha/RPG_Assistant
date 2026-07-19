using RPG_Assistant.Config;
using RPG_Assistant.Enums;
using System.Text.Json;

namespace RPG_Assistant.Loading;

public static class ItemDataLoader {
    private static Dictionary<string, JsonElement> Data =>
        JsonDataLoader.Load(DataPaths.Items, rootProperty: "itens");

    public static JsonElement GetType(string name) {
        foreach (var data in Data) {
            data.Value.EnumerateArray()
        }
    }
}