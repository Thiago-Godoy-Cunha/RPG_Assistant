using RPG_Assistant.Config;
using RPG_Assistant.Enums;
using RPG_Assistant.Loading;
using System.Text.Json;
namespace RPG_Assistant.Models;

public class Origin {
    private OriginType _name;
    private string _description;
    private string[] _items;
    private string[] _benefits;

    public Origin(OriginType name) {
        var raw = OriginDataLoader.GetRaw(name);
        _name = name;
        _description = raw.GetProperty("descricao").GetString() ?? string.Empty;

        var itemsList = new List<string>();
        if (raw.GetProperty("nome").GetString() == "Amnesico") {
            itemsList.Add(raw.GetProperty("itens").GetString() ?? string.Empty);
        }
        else {
            foreach (string item in (raw.GetProperty("itens").GetString() ?? string.Empty).Split(',')) {
                itemsList.Add(item.Trim());
            }
        }
        _items = itemsList.ToArray();

        var benefitsList = new List<string>();
        string beneficiosString = (raw.GetProperty("beneficios").GetString() ?? string.Empty).Replace(';', ',');
        foreach (string beneficio in beneficiosString.Split(',')) {
            benefitsList.Add(beneficio.Trim());
        }
        _benefits = benefitsList.ToArray();
    }

    public OriginType Name {
        get => _name;
    }
    public string Description {
        get => _description;
    }
    public string[] Items {
        get => _items;
    }
    public string[] Benefits {
        get => _benefits;
    }
}