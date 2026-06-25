using RPG_Assistant.Enums;
namespace RPG_Assistant.Models;
public record Spell(
    string Name,
    SchoolType School, 
    SpellCircle Circle,
    MagicType Type,
    Cost Cost,
    Duration Duration,
    EffectScope EffectScope,
    string EffectDescription
);