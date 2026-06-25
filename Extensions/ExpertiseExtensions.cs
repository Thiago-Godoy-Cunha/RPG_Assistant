using RPG_Assistant.Enums;
namespace RPG_Assistant.Extensions;
public static class ExpertiseExtensions {
    public static AttributeType GetAssociatedAttribute(this ExpertiseType type) => type switch {
        ExpertiseType.Athletics or
        ExpertiseType.Fighting => AttributeType.Strength,

        ExpertiseType.Riding or
        ExpertiseType.Acrobatics or
        ExpertiseType.Stealth or
        ExpertiseType.Initiative or
        ExpertiseType.Thievery or
        ExpertiseType.Piloting or
        ExpertiseType.Reflexes => AttributeType.Dexterity,

        ExpertiseType.Fortitude => AttributeType.Constitution,

        ExpertiseType.Knowledge or
        ExpertiseType.War or
        ExpertiseType.Investigation or
        ExpertiseType.Mysticism or
        ExpertiseType.Nobility or
        ExpertiseType.Craft or
        ExpertiseType.Aim => AttributeType.Intelligence,

        ExpertiseType.Medicine or
        ExpertiseType.Insight or
        ExpertiseType.Perception or
        ExpertiseType.Religion or
        ExpertiseType.Survival or
        ExpertiseType.Will => AttributeType.Wisdom,

        ExpertiseType.AnimalHandling or
        ExpertiseType.Performance or
        ExpertiseType.Diplomacy or
        ExpertiseType.Deception or
        ExpertiseType.Intimidation or
        ExpertiseType.Gambling => AttributeType.Charisma,

        _ => AttributeType.Intelligence
    };
}