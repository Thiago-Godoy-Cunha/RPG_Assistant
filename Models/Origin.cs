using RPG_Assistant.Enums;
using System.Text.Json;
using System;
using System.IO;

namespace RPG_Assistant.Models;

public class Origin {
    private OriginType _name;
    private string _description;
    private string[] _items;
    private string[] _benefits;

    public Origin(OriginType name) {
        _name = name;

        string jsonString = File.ReadAllText("C:\\Users\\thiag\\source\\repos\\Thiago-Godoy-Cunha\\RPG_Assistant\\origens.json");
        using JsonDocument doc = JsonDocument.Parse(jsonString);
        JsonElement listaOrigens = doc.RootElement.GetProperty("origens");

        foreach (JsonElement origem in listaOrigens.EnumerateArray()) {
            string nomeNoJson = origem.GetProperty("nome").GetString();
            string nomeFormatado = nomeNoJson?.Replace(" ", "");

            if (nomeFormatado != null && nomeFormatado.Equals(name.ToString(), StringComparison.OrdinalIgnoreCase)) {
                _description = origem.GetProperty("descricao").GetString();
                if (origem.GetProperty("nome").GetString() == "Amnesico") {
                    _items.Append(origem.GetProperty("itens").GetString());
                } else {
                    foreach (string item in origem.GetProperty("itens").GetString().Split(',')) {
                        _items.Append(item.Trim());
                    }
                }
                string beneficiosString = origem.GetProperty("beneficios").GetString().Replace(';',',');
                foreach (string beneficio in beneficiosString.Split(',')) {
                    _benefits.Append(beneficio.Trim());
                    break;
                }
            }
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

    public string Items {
        get => _items;
        set => _items = value;
    }

    public string Benefits {
        get => _benefits;
        set => _benefits = value;
    }
}