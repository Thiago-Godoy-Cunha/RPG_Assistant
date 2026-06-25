using RPG_Assistant.Enums;
namespace RPG_Assistant.Models;
public record Class(
    string Name,
    byte InitialHp,
    byte HpPerLevel,
    byte ManaPerLevel,
    Dictionary<byte, List<Skill>> AutomaticProgressionTable,
    List<Power> AvailablePowers,
    SpellCircle? SpellCircleProgression
);