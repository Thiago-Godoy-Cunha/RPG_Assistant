using RPG_Assistant.Config;
using RPG_Assistant.Enums;
using RPG_Assistant.Loading;
using System.Text.Json;
namespace RPG_Assistant.Models;

public class Race {
    private RaceType _name;
    private byte _desloc;
    private string _atributos;
    private string _habilidades;

    public Race(RaceType name) {
        var raw = RaceDataLoader.GetRaw(name);
        _name = name;
        _desloc = byte.TryParse(raw.GetProperty("deslocamento").GetString().Split(' ')[0], out var d) ? d : (byte)0;
        _atributos = raw.GetProperty("atributos").GetString() ?? string.Empty;
        _habilidades = raw.GetProperty("habilidades").GetString() ?? string.Empty;
    }

    public RaceType Name {
        get => _name;
    }
    public string Atributos {
        get => _atributos;
    }
    public string Habilidades {
        get => _habilidades;
    }
    public byte Desloc {
        get => _desloc;
    }
}