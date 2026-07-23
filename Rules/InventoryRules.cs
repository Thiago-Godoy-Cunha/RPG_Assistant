using RPG_Assistant.Models;

namespace RPG_Assistant.Rules;

public static class InventoryRules {
    public static bool WearArmor(Character character) => character.Armor != null;
    public static bool HoldShields(Character character) => character.Shield != null;

}
