namespace RPG_Assistant.Models;
public record Power(
    string Name,
    string Effect,
    bool IsPassive,
    Cost Cost,
    Duration Duration,
    EffectScope EffectScope,
    byte RequiredCharLevel,
    Dictionary<Class, byte> RequiredClassLevel
) : Skill(Name, Effect, IsPassive, Cost, Duration, EffectScope);