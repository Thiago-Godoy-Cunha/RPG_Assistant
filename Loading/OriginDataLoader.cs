using RPG_Assistant.Config;
using RPG_Assistant.Enums;
using System.Text.Json;

namespace RPG_Assistant.Loading;

public static class OriginDataLoader {
    private static Dictionary<string, JsonElement> Data =>
        JsonDataLoader.Load(DataPaths.Origins, rootProperty: "origens");

    public static JsonElement GetRaw(OriginType type) => Data[type.ToString()];
}