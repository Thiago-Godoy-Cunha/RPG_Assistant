using RPG_Assistant.Enums;
using System.Text.Json;
namespace RPG_Assistant.Models;
public class Class {
    private ClassType _name;
    private byte _initialHp;
    private byte _hpPerLevel;
    private byte _manaPerLevel;
    //private List<ClassPower> AvailablePowers;
    //private Dictionary<byte, SpellCircle>? SpellCircleProgression;

    public Class(ClassType name) {
        _name = name;

        string jsonString = File.ReadAllText("C:\\Users\\thiag\\source\\repos\\Thiago-Godoy-Cunha\\RPG_Assistant\\classes.json");
        using JsonDocument doc = JsonDocument.Parse(jsonString);
        JsonElement listaClasses = doc.RootElement.GetProperty("classes");

        foreach (JsonElement classe in listaClasses.EnumerateArray()) {
            string nomeNoJson = classe.GetProperty("Nome").GetString();

            if (nomeNoJson != null && nomeNoJson.Equals(name.ToString(), StringComparison.OrdinalIgnoreCase)) {
                _initialHp = classe.GetProperty("InitialHp").GetByte();
                _hpPerLevel = classe.GetProperty("HpPerLevel").GetByte();
                _manaPerLevel = classe.GetProperty("ManaPerLevel").GetByte();

                break;
            }
        }
    }

    public List<string>[] GetClassExpertises() {
        var obrExpertisesList = new List<string>();
        var optExpertisesList = new List<string>();
        string jsonString = File.ReadAllText("C:\\Users\\thiag\\source\\repos\\Thiago-Godoy-Cunha\\RPG_Assistant\\classes.json");
        using JsonDocument doc = JsonDocument.Parse(jsonString);
        JsonElement listaClasses = doc.RootElement.GetProperty("classes");
        foreach (JsonElement classes in listaClasses.EnumerateArray()) {
            string nomeNoJson = classes.GetProperty("Nome").GetString();

            if (nomeNoJson != null && nomeNoJson.Equals(_name.ToString(), StringComparison.OrdinalIgnoreCase)) {
                int i = 0;
                foreach (JsonElement obrExpertise in classes.GetProperty("ObrigatoryExpertises").EnumerateArray()) {
                    string value = obrExpertise.GetString();
                    obrExpertisesList.Add(value);
                    i++;
                }
                foreach (JsonElement optExpertise in classes.GetProperty("OptionalExpertises").EnumerateArray()) {
                    string value = optExpertise.GetString();
                    optExpertisesList.Add(value);
                    i++;
                }

                break;
            }
        }
        return new List<string>[] { obrExpertisesList, optExpertisesList };
    }

    public byte GetQtdOptExpertises() {
        byte qtdexp = 0;
        string jsonString = File.ReadAllText("C:\\Users\\thiag\\source\\repos\\Thiago-Godoy-Cunha\\RPG_Assistant\\classes.json");
        using JsonDocument doc = JsonDocument.Parse(jsonString);
        JsonElement listaClasses = doc.RootElement.GetProperty("classes");
        foreach (JsonElement classes in listaClasses.EnumerateArray()) {
            string nomeNoJson = classes.GetProperty("Nome").GetString();

            if (nomeNoJson != null && nomeNoJson.Equals(_name.ToString(), StringComparison.OrdinalIgnoreCase)) {
                qtdexp = classes.GetProperty("QtdOptionalExpertises").GetByte();
            } 
        }
        return qtdexp;
    }


    public ClassType Name {
        get => _name;
        set => _name = value;
    }
    public byte InitialHp {
        get => _initialHp;
        set => _initialHp = value;
    }
    public byte HpPerLevel {
        get => _hpPerLevel;
        set => _hpPerLevel = value;
    }
    public byte ManaPerLevel {
        get => _manaPerLevel;
        set => _manaPerLevel = value;
    }
}