using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using RPG_Assistant.Enums;
using RPG_Assistant.Extensions;
namespace RPG_Assistant.Models;
public class Character {
    private string _name;
    private short _currentHealth;
    private short _currentMana;
    private byte _def;
    private byte _desloc;
    private Dictionary<AttributeType, byte> _attributes;
    private List<ExpertiseType> _trainedExpertises;
    public Dictionary<Class, byte> _classLevels;
    public Class _initialClass;
    public List<Power> _chosenPowers;
    public List<Spell> _learnedSpells;
    public Character(string name, Dictionary<AttributeType, byte> attributes, Class firstClass) {
        foreach (AttributeType type in Enum.GetValues<AttributeType>()) {
            if (!attributes.ContainsKey(type)) {
                throw new ArgumentException($"Falta o atributo {type} na inicialização do personagem.");
            }
        }
        _name = name;
        _classLevels.Add(firstClass, 1);
        _attributes = attributes;
        _initialClass = firstClass; _currentHealth = MaxHealth;
        _currentMana = MaxMana;
    }
    public byte TotalLevel => (byte)_classLevels.Values.Sum(lvl => lvl);
    public short MaxHealth {
        get {
            int constitutionMod = _attributes[AttributeType.Constitution];
            int totalLevel = TotalLevel;
            int baseHp = _initialClass.InitialHp;

            foreach (var (classes, level) in _classLevels) {
                if (classes == _initialClass) {
                    baseHp += classes.HpPerLevel * (level - 1);
                }
                else {
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
    public string Name { get => _name; set => _name = value; }
    public byte Def { get => _def; set => _def = value; }
    public byte Desloc { get => _desloc; set => _desloc = value; }
    public short CurrentHealth { get => _currentHealth; set => _currentHealth = value; }
    public short CurrentMana { get => _currentMana; set => _currentMana = value; }

    public void TrainExpertise(ExpertiseType type) => _trainedExpertises.Add(type); 
    public void UntrainExpertise(ExpertiseType type) => _trainedExpertises.Remove(type);
    public bool IsTrained(ExpertiseType type) => _trainedExpertises.Contains(type);
    public int GetExpertiseModifier(ExpertiseType expertise) {
        AttributeType associatedAttr = expertise.GetAssociatedAttribute();
        byte attributeValue = _attributes[associatedAttr];
        byte levelBonus = (byte) (TotalLevel / 2);
        byte trainingBonus = 0;
        if (_trainedExpertises.Contains(expertise)) {
            trainingBonus = TotalLevel switch {
                <= 6 => 2,
                <= 14 => 4,
                _ => 6
            };
        }
        return attributeValue + levelBonus + trainingBonus;
    }
    public bool CanLearnPower(Power power) {
        if (TotalLevel < power.RequiredCharLevel) return false;
        foreach(var (requiredClass, requiredLevel) in power.RequiredClassLevel) {
            if (_classLevels.TryGetValue(requiredClass, out byte currentClassLevel)) {
                return (currentClassLevel >= requiredLevel);
            }
        }
        return true;
    }
    public bool CanLearnSpell(Class casterClass, Spell spell) {
        if (casterClass.SpellCircleProgression == null) return false;
        if (!_classLevels.TryGetValue(casterClass, out byte classLevel)) return false;
        foreach (var (classes, levels) in _classLevels) {
            if (classes.SpellCircleProgression == null) return false;
            return spell.Circle <= classes.SpellCircleProgression;
        }
        return false;
    }
    public void LearnPower(Power power) {
        if (!CanLearnPower(power))
            throw new InvalidOperationException($"O personagem não cumpre os requisitos para aprender o poder: {power.Name}.");
        _chosenPowers.Add(power);
    }
    public void LearnSpell(Class casterClass, Spell spell) {
        if (!CanLearnSpell(casterClass, spell))
            throw new InvalidOperationException($"O personagem não tem acesso ao {spell.Circle}° círculo para aprender: {spell.Name}.");
        _learnedSpells.Add(spell);
    }
    public List<Skill> GetAllActiveSkills() {
        var allSkills = new List<Skill>();
        foreach(var (classes, level) in _classLevels) {
            foreach (var (levelUnlocked, skills) in classes.AutomaticProgressionTable) {
                if (TotalLevel >= levelUnlocked) {
                    allSkills.AddRange(skills);
                }
            }
        }
        allSkills.AddRange(_chosenPowers);
        return allSkills;
    }
}