using RPG_Assistant.Enums;

namespace RPG_Assistant.Rules;

public static class ExpertiseRules {
    public static bool HasObrigatoryChoice(List<string> obrigatoryRaw) =>
        obrigatoryRaw[0].Contains('/');

    public static List<ExpertiseType> GetChoiceOptions(List<string> obrigatoryRaw) =>
        obrigatoryRaw[0].Split('/').Select(Enum.Parse<ExpertiseType>).ToList();

    public static List<ExpertiseType> GetAutoTrainedObrigatoryExpertises(List<string> obrigatoryRaw) {
        if (HasObrigatoryChoice(obrigatoryRaw))
            return new List<ExpertiseType> { Enum.Parse<ExpertiseType>(obrigatoryRaw[1]) };

        return obrigatoryRaw.Select(Enum.Parse<ExpertiseType>).ToList();
    }

    public static List<ExpertiseType> GetOptionalExpertiseOptions(List<string> optionalRaw) =>
        optionalRaw.Select(Enum.Parse<ExpertiseType>).ToList();
}