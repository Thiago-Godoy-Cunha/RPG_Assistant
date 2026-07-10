using RPG_Assistant.Config;
using RPG_Assistant.Enums;
using RPG_Assistant.Loading;
using System;
using System.IO;
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
        _description = raw.GetProperty("descricao").GetString();

        if (raw.GetProperty("nome").GetString() == "Amnesico") {
            _items.Append(raw.GetProperty("itens").GetString());
        } else {
            foreach (string item in raw.GetProperty("itens").GetString().Split(',')) {
                _items.Append(item.Trim());
            }
        }

        string beneficiosString = raw.GetProperty("beneficios").GetString().Replace(';',',');
        foreach (string beneficio in beneficiosString.Split(',')) {
            _benefits.Append(beneficio.Trim());
            break;
        }
    }

    public OriginType Name {
        get => _name;
        set => _name = value;
    }

    public string Description {
        get => _description;
        set => _description = value;
    }

    public string[] Items {
        get => _items;
    }

    public string[] Benefits {
        get => _benefits;
    }
}