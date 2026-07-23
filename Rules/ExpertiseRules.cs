using RPG_Assistant.Enums;
using RPG_Assistant.Extensions;
using RPG_Assistant.Models;
using System.Xml.Linq;

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
    public static int GetExpertiseModifier(Character character, ExpertiseType expertise) {
        AttributeType associatedAttr = expertise.GetAssociatedAttribute();
        sbyte attributeValue = character.Attributes.TryGetValue(associatedAttr, out sbyte attrVal) ? attrVal : (sbyte)0;
        byte levelBonus = (byte)(character.TotalLevel / 2);
        byte trainingBonus = 0;
        if (character.TrainedExpertises.Contains(expertise)) {
            trainingBonus = character.TotalLevel switch {
                <= 6 => 2,
                <= 14 => 4,
                _ => 6
            };
        }
        return attributeValue + levelBonus + trainingBonus;
    }
    public static List<ExpertiseType> GetOptionalExpertiseOptions(List<string> optionalRaw) =>
        optionalRaw.Select(Enum.Parse<ExpertiseType>).ToList();
}