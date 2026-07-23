using RPG_Assistant.Enums;
using RPG_Assistant.Extensions;
namespace RPG_Assistant.Models;
public class Character {
    private readonly string _name;
    private short _currentHealth;
    private short _currentMana;
    private byte _baseDef;
    private byte _desloc;
    private readonly List<Item> _inventory = new();
    private Armor? _armor;
    private Shield? _shield;
    private Dictionary<AttributeType, sbyte> _attributes = new();
    private List<ExpertiseType> _trainedExpertises = new();
    private readonly Dictionary<Class, byte> _classLevels = new();
    private readonly Class _initialClass;
    private readonly Origin _origin;
    private readonly Race _race;
    private readonly List<Power> _chosenPowers = new();
    private readonly List<Spell> _learnedSpells = new(); 
    public string Name => _name;
    public byte TotalLevel => (byte)_classLevels.Values.Sum(lvl => (int)lvl);

    public Character(string name, ClassType firstClass, OriginType origin, RaceType race, Dictionary<AttributeType, sbyte> attributes) {
        Class classe = new Class(firstClass);
        Origin origem = new Origin(origin);
        Race raca = new Race(race);
        _name = name;
        _classLevels.Add(classe, 1);
        _initialClass = classe;
        _origin = origem;
        _race = raca;
        _attributes = attributes;
        _desloc = raca.Desloc;
        _currentHealth = MaxHealth;
        _currentMana = MaxMana;
        _baseDef = attributes.TryGetValue(AttributeType.Destreza, out sbyte des) ? (byte)(10 + des) : (byte)10;
    }

    public override string ToString() {
        return $"{_name} é um {_race.Name} {_initialClass.Name} de nível {TotalLevel}\nVida: {CurrentHealth}\nMana: {CurrentMana}\nVida máxima: {MaxHealth}\nMana máxima: {MaxMana}\nDeslocamento: {Desloc}";
    }

    public short MaxHealth {
        get {
            int constitutionMod = _attributes[AttributeType.Constituicao];
            int totalLevel = TotalLevel;
            int baseHp = _initialClass.InitialHp;

            foreach (var (classes, level) in _classLevels) {
                if (classes == _initialClass) {
                    baseHp += classes.HpPerLevel * (level - 1);
                } else {
                    baseHp += classes.HpPerLevel * level;
                }
            }

            int constitutionBonus = constitutionMod * totalLevel;
            return (short)(baseHp + constitutionBonus);
        }
    }

    public short MaxMana {
        get {
            int totalMana = 0;
            foreach (var (classes, level) in _classLevels) {
                totalMana += classes.ManaPerLevel * level;
            }
            return (short)totalMana;
        }
    }

    private short MaxLoad {
        get {
            byte carga = 10;
            if (_attributes.TryGetValue(AttributeType.Forca, out sbyte forca) && forca >= 0) {
                carga += 2;
            } else {
                carga -= 1;
            }
            return (short)carga;
        }
    }
    public byte Def => (byte)(_baseDef + (_armor?.DefenseBonus ?? 0) + (_shield?.DefenseBonus ?? 0));
    public byte Desloc { get => _desloc; set => _desloc = value; }
    public short CurrentHealth { get => _currentHealth; set => _currentHealth = value; }
    public short CurrentMana { get => _currentMana; set => _currentMana = value; }

    public Dictionary<Class, byte> ClassLevels => _classLevels;

    public Class InitialClass => _initialClass;

    public List<ExpertiseType> TrainedExpertises => _trainedExpertises;
    public Dictionary<AttributeType, sbyte> Attributes  => _attributes;

    public Origin Origin => _origin;

    public Shield? Shield { get => _shield; }
    public Armor? Armor { get => _armor; }
    public List<Item> Inventory { get => _inventory; }

    public void TrainExpertise(ExpertiseType type) {
        if (_trainedExpertises.Contains(type))
            throw new InvalidOperationException($"Perícia '{type}' já está treinada.");
        _trainedExpertises.Add(type);
    }
    public void UntrainExpertise(ExpertiseType type) => _trainedExpertises.Remove(type);
    public bool IsTrained(ExpertiseType type) => _trainedExpertises.Contains(type);
    
    public bool AddItem(Item item) {
        if (item.Space <= MaxLoad) { 
            _inventory.Add(item);
            return true;
        } else {
            return false;
        }
    }
    public void RemoveItemByIndex(byte index) {
        _inventory.RemoveAt(index);
    }
    public bool EquipItem(byte index) {
        if(Inventory[index].Type == ItemType.Armadura && Armor == null) {
            _armor = Armor.EquipArmor((Armor)Inventory[index]);
            return true;
        }
        if(Inventory[index].Type == ItemType.Escudo && Shield == null) {
            _shield = Shield.EquipShield((Shield)Inventory[index]);
            return true;
        }
        return false;
    }
    public bool CanLearnPower(Power power) {
        if (TotalLevel < power.RequiredCharLevel) return false;

        foreach (var (requiredClass, requiredLevel) in power.RequiredClassLevel) {
            if (_classLevels.TryGetValue(requiredClass, out byte currentClassLevel)) {
                if (currentClassLevel < requiredLevel) return false;
            }
            else {
                if (requiredLevel > 0) return false;
            }
        }
        return true;
    }
    public void LearnPower(Power power) {
        if (_chosenPowers.Contains(power))
            return;

        if (!CanLearnPower(power))
            throw new InvalidOperationException($"O personagem não cumpre os requisitos para aprender o poder: {power.Name}.");

        _chosenPowers.Add(power);
    }

    public void SetAttributes(Dictionary<AttributeType, sbyte> attributes) => _attributes = attributes;
}
