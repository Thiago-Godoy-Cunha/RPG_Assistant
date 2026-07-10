using RPG_Assistant.Config;
using RPG_Assistant.Enums;
using System.Text.Json;

namespace RPG_Assistant.Loading;

public static class ClassDataLoader {
    private static Dictionary<string, JsonElement> Data =>
        JsonDataLoader.Load(DataPaths.Classes, rootProperty: "classes");

    public static JsonElement GetRaw(ClassType type) => Data[type.ToString()];
}