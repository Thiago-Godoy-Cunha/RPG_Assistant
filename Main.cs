using RPG_Assistant.Enums;
using RPG_Assistant.Models;
using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using GType = Google.GenAI.Types;

public class ClassesRoot {
    public List<Class> Classes { get; set; }
}

public class Program
{
    static async Task Main(string[] args) {
        OriginType _selectedOrigin = GetOriginFromUser();

        ClassType _selectedClass = GetClassFromUser();
        string _inputName = GetNameFromUser();

        Character character = new Character(_inputName, _selectedClass);

        TrainObrigatoryExpertises(character);
        TrainOptionalExpertises(character);

        var _selectedAttributes = GetAttributesFromUser();

        character.SetAttributes(_selectedAttributes);

        Console.Clear();
        Console.WriteLine(character.ToString());

        Dictionary<AttributeType, sbyte> attributes = character.Attributes;
        Console.WriteLine("\nAtributos definidos:");
        foreach (var attr in attributes)
        {
            Console.WriteLine($"{attr.Key}: {attr.Value}");
        }
        Console.WriteLine("\nPerícias treinadas:");
        List<ExpertiseType> expertises = character.TrainedExpertises;
        foreach (var exp in expertises) {
            Console.WriteLine($"{exp}");
        }
    }
    public static string GetNameFromUser() {
        while (true) {
            Console.Clear();
            Console.WriteLine("Qual é o nome do seu personagem?");
            string _inputName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(_inputName)) {
                Console.WriteLine("Escolha uma opção válida");
                continue;
            }
            return _inputName;
        }
    }
    /// <summary>
    /// Pede ao usuário para escolher um valor de um enum, exibindo as opções numeradas.
    /// Aceita o número da opção, o nome do valor (case-insensitive) ou "0" para um valor aleatório.
    /// </summary>
    /// <param name="question">Pergunta exibida acima da lista de opções.</param>
    /// <param name="formatDisplay">Formatação opcional aplicada ao nome de cada valor ao exibir (ex.: SplitPascalCase).</param>
    public static T GetEnumFromUser<T>(string question, Func<string, string> formatDisplay = null) where T : struct, Enum {
        T[] _values = (T[])Enum.GetValues(typeof(T));
        formatDisplay ??= (name => name);

        T _selected = _values[new Random().Next(_values.Length)];

        while (true) {
            Console.Clear();
            Console.WriteLine(question);
            for (int i = 0; i < _values.Length; i++) {
                Console.WriteLine((i + 1) + " - " + formatDisplay(_values[i].ToString()));
            }
            Console.WriteLine("0 - Escolher aleatório");

            string _input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(_input)) {
                Console.WriteLine("Escolha uma opção válida");
                continue;
            }

            if (_input == "0") {
                break;
            }

            if (int.TryParse(_input, out int parsed)) {
                if (parsed >= 1 && parsed <= _values.Length) {
                    _selected = _values[parsed - 1];
                    break;
                }
            } else {
                bool matched = false;
                foreach (T value in _values) {
                    if (string.Equals(_input, value.ToString(), StringComparison.OrdinalIgnoreCase)) {
                        _selected = value;
                        matched = true;
                        break;
                    }
                }
                if (matched)
                    break;
            }

            Console.WriteLine("Escolha uma opção válida");
        }

        return _selected;
    }
    public static ClassType GetClassFromUser() => GetEnumFromUser<ClassType>("Qual é a sua classe?");
    public static OriginType GetOriginFromUser() => GetEnumFromUser<OriginType>("Qual é a sua origem?", SplitPascalCase);

    public static void TrainObrigatoryExpertises(Character character) {
        List<string>[] expertisesList = character.InitialClass.GetClassExpertises();
        Console.Clear();
        Console.WriteLine(character.ToString());
        if (expertisesList[0][0].Contains('/')) {
            Console.WriteLine("\nEscolha uma das perícias obrigatórias:");
            for (int i = 0; i < expertisesList[0][0].Split('/').Length; i++) {
                Console.WriteLine($"{i + 1} - {expertisesList[0][0].Split('/')[i]}");
            }
            while (true) {
                string _inputExpertise = Console.ReadLine();
                if (int.TryParse(_inputExpertise, out int parsed)) {
                    if (parsed >= 1 && parsed <= 2 && !(character.IsTrained(Enum.Parse<ExpertiseType>(expertisesList[0][0].Split('/')[parsed - 1])))) {
                        character.TrainExpertise(Enum.Parse<ExpertiseType>(expertisesList[0][0].Split('/')[parsed - 1]));
                        break;
                    }
                }
                Console.WriteLine("Escolha uma opção válida");
            }
            character.TrainExpertise(Enum.Parse<ExpertiseType>(expertisesList[0][1]));
        } else {
            for (int i = 0; i < expertisesList[0].Count; i++) {
                character.TrainExpertise(Enum.Parse<ExpertiseType>(expertisesList[0][i]));
            }
        }
    }

    public static void TrainOptionalExpertises(Character character) {
        List<string>[] expertisesList = character.InitialClass.GetClassExpertises();
        byte qtdOptExpertises = character.InitialClass.GetQtdOptExpertises();
        List<string> list = expertisesList[1];
        bool clicado = false;
        ConsoleKeyInfo tecla;
        Dictionary<byte, bool> marcado = new();
        byte index = 0;
        byte count = 0;

        int initialCursorX = 0;
        int initialCursorY = 0;

        Console.Clear();
        Console.WriteLine(character.ToString());
        Console.WriteLine($"\nEscolha {qtdOptExpertises} perícias opcionais:");
        initialCursorX = Console.CursorLeft;
        initialCursorY = Console.CursorTop;
        do {
            int cursorX = 0;
            int cursorY = 0;
            Console.SetCursorPosition(initialCursorX, initialCursorY);
            for (byte i = 0; i < list.Count; i++) {
                marcado.TryAdd(i, false);
                Console.Write($"{list[i]} [");
                if (index == i) {
                    cursorX = Console.CursorLeft;
                    cursorY = Console.CursorTop;
                }
                if (marcado[i]) {
                    Console.WriteLine("X]");
                } else {
                    Console.WriteLine(" ]");
                }
            }
            Console.SetCursorPosition(cursorX, cursorY);
            tecla = Console.ReadKey();
            if (tecla.Key == ConsoleKey.Spacebar) {
                if (!marcado[index] && count < qtdOptExpertises) {
                    count++;
                    marcado[index] = true;
                } else if (marcado[index]) {
                    count--;
                    marcado[index] = false;
                } else {
                    Console.SetCursorPosition(0, initialCursorY + list.Count);
                    Console.WriteLine($"Você não pode escolher mais de {qtdOptExpertises} opções");
                    Thread.Sleep(2000);
                    Console.SetCursorPosition(0, initialCursorY + list.Count);
                    Console.Write(new string(' ', 50));
                }
            } else if (tecla.Key == ConsoleKey.UpArrow || tecla.Key == ConsoleKey.LeftArrow) {
                index--;
                if (index == 255)
                    index = (byte)(list.Count - 1);
            } else if (tecla.Key == ConsoleKey.DownArrow || tecla.Key == ConsoleKey.RightArrow) {
                index++;
                if (index > (byte)(list.Count - 1))
                    index = 0;
            } else if (tecla.Key == ConsoleKey.Enter) {
                if (count < qtdOptExpertises) {
                    Console.SetCursorPosition(0, initialCursorY + list.Count);
                    Console.WriteLine($"Escolha {qtdOptExpertises} opções para confirmar");
                    Thread.Sleep(2000);
                    Console.SetCursorPosition(0, initialCursorY + list.Count);
                    Console.Write(new string(' ', 50));
                } else {
                    clicado = true;
                }
            }
        } while (!clicado);
        foreach (var m in marcado) {
            if(m.Value)
                character.TrainExpertise(Enum.Parse<ExpertiseType>(expertisesList[1][m.Key]));
        }
    }
    private static void PrintAttributeCostTable()
    {
        byte nameWidth = 18;
        byte costWidth = 15;
        string sep = "+" + new string('-', nameWidth + 2) + "+" + new string('-', costWidth + 2) + "+";

        Console.Clear();
        Console.WriteLine(" ------ Tabela Custos Atributos -------");
        Console.WriteLine(" ------   Total de 10 pontos    -------");

        Console.WriteLine(sep);
        Console.WriteLine($"| {"Atributo",-18} | {"Custo",-15} |");
        Console.WriteLine(sep);
        Console.WriteLine($"| {"-1",-18} | {"-1",-15} |");
        Console.WriteLine($"| {" 0",-18} | {" 0",-15} |");
        Console.WriteLine($"| {" 1",-18} | {" 1",-15} |");
        Console.WriteLine($"| {" 2",-18} | {" 2",-15} |");
        Console.WriteLine($"| {" 3",-18} | {" 4",-15} |");
        Console.WriteLine($"| {" 4",-18} | {" 7",-15} |");

        Console.WriteLine(sep);
    }

    private static Dictionary<AttributeType, sbyte> GetAttributesFromUser() {
        PrintAttributeCostTable();

        int initialCursorX = Console.CursorLeft;
        int initialCursorY = Console.CursorTop;
        var attributes = Enum.GetValues<AttributeType>();
        sbyte totalCost = 0;

        while (true) {
            var results = attributes.ToDictionary(key => key, value => (sbyte)0);

            foreach (var currentAttr in attributes) {
                while (true) {
                    Console.SetCursorPosition(initialCursorX, initialCursorY);
                    int cursorX = 0;
                    int cursorY = 0;
                    Console.Write($"\tRestam {(10-totalCost).ToString("D2")} pontos");

                    foreach (var attr in attributes) {
                        if (attr == currentAttr) {
                            Console.Write($"\n{attr}: ");
                            cursorX = Console.CursorLeft;
                            cursorY = Console.CursorTop;
                        } else {
                            Console.Write($"\n{attr}: {results[attr]}");
                        }
                    }

                    Console.SetCursorPosition(cursorX, cursorY);
                    Console.Write(new string(' ', 50));
                    Console.SetCursorPosition(cursorX, cursorY);
                    string input = Console.ReadLine();

                    if (!int.TryParse(input, out int val) || val < -1 || val > 4) {
                        Console.SetCursorPosition(cursorX+2, cursorY);
                        Console.Write(" Erro: Digite somente números entre -1 e 4.");

                        Thread.Sleep(2000);
                        continue;
                    }

                    results[currentAttr] = (sbyte)val;
                    totalCost += AttributeCost((sbyte)val);
                    break;
                }
            }

            if (totalCost == 10) {
                return results;
            }

            Console.Clear();
            Console.WriteLine($"A soma dos custos deu {totalCost}. Precisa ser exatamente 10.");
            Console.WriteLine("Vamos reiniciar o preenchimento dos atributos...\n");

            Thread.Sleep(3500);
            PrintAttributeCostTable();
        }
    }

    private static sbyte AttributeCost(int value) {
        return value switch {
            -1 => -1,
            0 => 0,
            1 => 1,
            2 => 2,
            3 => 4,
            4 => 7,
            _ => throw new ArgumentOutOfRangeException(nameof(value), "Valor fora do intervalo esperado")
        };
    }

    private static string SplitPascalCase(string input) {
        if (string.IsNullOrEmpty(input)) return input;
        var sb = new StringBuilder();
        sb.Append(input[0]);
        for (int i = 1; i < input.Length; i++) {
            char c = input[i];
            char prev = input[i - 1];
            if (char.IsUpper(c) && !char.IsWhiteSpace(prev)) {
                sb.Append(' ');
            }
            sb.Append(c);
        }
        return sb.ToString();
    }
}
