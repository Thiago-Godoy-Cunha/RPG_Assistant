using RPG_Assistant.Enums;
using RPG_Assistant.Loading
namespace RPG_Assistant.Models;

public class Weapon : Item {
    private byte[] _damage;
    private DamageType _damageType;
    private byte _marginOfThreat;
    private byte _criticalMultiplier;
    public Weapon(string name, byte space, string description, short price) : base (name, ItemType.Arma, space, description, price) {
        var raw = ItemDataLoader.GetRaw(ItemType.Arma, name);

        
    }
}