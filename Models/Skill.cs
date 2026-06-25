namespace RPG_Assistant.Models;
public record Skill(
    string Name,
    string Effect,
    bool IsPassive,
    Cost Cost,
    Duration Duration,
    EffectScope EffectScope);