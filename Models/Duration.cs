using RPG_Assistant.Enums;
namespace RPG_Assistant.Models;
public readonly record struct Duration(DurationType Type, short Value = 0, UnitType Unit = UnitType.None);