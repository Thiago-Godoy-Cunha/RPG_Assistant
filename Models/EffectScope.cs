using RPG_Assistant.Enums;
namespace RPG_Assistant.Models;
public readonly record struct EffectScope(
    RangeType Range,
    AreaType? Area = null, 
    byte AreaSize = 0      
) {
    public byte RangeDistanceMeters => Range switch {
        RangeType.Short => 9,
        RangeType.Normal => 30,
        RangeType.Long => 90,
        _ => 0
    };
    public bool HasArea => Area.HasValue;
}