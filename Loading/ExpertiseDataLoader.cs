using RPG_Assistant.Enums;
using System.Text.Json;
namespace RPG_Assistant.Loading;

public static class ExpertiseDataLoader {
    public static List<string>[] GetClassExpertises(ClassType type) {
        JsonElement raw = ClassDataLoader.GetRaw(type);

        List<string> obrigatory = raw.GetProperty("ObrigatoryExpertises")
            .EnumerateArray()
            .Select(e => e.GetString()!)
            .ToList();

        List<string> optional = raw.GetProperty("OptionalExpertises")
            .EnumerateArray()
            .Select(e => e.GetString()!)
            .ToList();

        return new[] { obrigatory, optional };
    }

    public static byte GetQtdOptExpertises(ClassType type) {
        JsonElement raw = ClassDataLoader.GetRaw(type);
        return raw.GetProperty("QtdOptionalExpertises").GetByte();
    }
}