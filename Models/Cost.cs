namespace RPG_Assistant.Models;
public readonly record struct Cost(byte Value, bool IsSpecial = false, string SpecialDescription = "");