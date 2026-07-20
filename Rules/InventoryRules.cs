using RPG_Assistant.Models;

namespace RPG_Assistant.Rules;

public static class InventoryRules {
    public static bool WearsArmor(Character character) => character.Armor != null;

}
