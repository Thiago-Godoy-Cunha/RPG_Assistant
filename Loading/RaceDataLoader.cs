using RPG_Assistant.Config;
using RPG_Assistant.Enums;
using System.Text.Json;

namespace RPG_Assistant.Loading;

public static class RaceDataLoader {
    private static Dictionary<string, JsonElement> Data =>
        JsonDataLoader.Load(DataPaths.Races, rootProperty: "racas");

    public static JsonElement GetRaw(RaceType type) => Data[type.ToString()];
}