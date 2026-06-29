using RPG_Assistant.Enums;
namespace RPG_Assistant.Models;
public record Class(
    ClassType Name,
    byte InitialHp,
    byte HpPerLevel,
    byte ManaPerLevel,
    List<ClassPower> AvailablePowers,
    Dictionary<byte, SpellCircle>? SpellCircleProgression
);