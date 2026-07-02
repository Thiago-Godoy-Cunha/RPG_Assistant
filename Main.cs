using System;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using GType = Google.GenAI.Types;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;
using RPG_Assistant.Enums;
using RPG_Assistant.Models;

public class ClassesRoot {
    public List<Class> Classes { get; set; }
}

public class Program
{
    static void Main(string[] args) {
        ClassType _selectedClass = (ClassType) new Random().Next(1, 15);
        while (true) {
            Console.WriteLine("Qual é a sua classe?");
            foreach (ClassType classe in Enum.GetValues(typeof(ClassType))) {
                Console.WriteLine(((int)classe + 1) + " - " + classe.ToString());
            }
            Console.WriteLine("0 - Escolher aleatório");

            string _inputClass = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(_inputClass)) {
                Console.WriteLine("Escolha uma opção válida");
                continue;
            }

            if (_inputClass == "0") {
                break;
            }

            if (int.TryParse(_inputClass, out int parsed)) {
                var values = Enum.GetValues(typeof(ClassType));
                int count = values.Length;
                if (parsed >= 1 && parsed <= count) {
                    _selectedClass = (ClassType)(parsed - 1);
                    break;
                }
            }
            else {
                bool matched = false;
                foreach (ClassType classe in Enum.GetValues(typeof(ClassType))) {
                    if (string.Equals(_inputClass, classe.ToString(), StringComparison.OrdinalIgnoreCase)) {
                        _selectedClass = Enum.Parse<ClassType>(classe.ToString());
                        matched = true;
                        break;
                    }
                }
                if (matched) break;
            }

            Console.WriteLine("Escolha uma opção válida");
        }
        
        Console.WriteLine("Qual é o nome do seu personagem?");
        string _inputName = Console.ReadLine();
        Console.WriteLine(" ------ Tabela Custos Atributos -------");
        Console.WriteLine(" ------   Total de 10 pontos    -------");
        PrintAttributeCostTable();
        Console.WriteLine("Distribua:");
        var attributes = GetAttributesFromUser();

        Character character = new Character(_inputName, _selectedClass);
        character.SetAttributes(attributes);
        Console.WriteLine(character.toString());

        Console.WriteLine("Atributos definidos:");
        foreach (var kv in attributes)
        {
            Console.WriteLine($"{kv.Key}: {kv.Value}");
        }
    }

    private static void PrintAttributeCostTable()
    {
        byte nameWidth = 18;
        byte costWidth = 15;
        string sep = "+" + new string('-', nameWidth + 2) + "+" + new string('-', costWidth + 2) + "+";

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

    private static Dictionary<AttributeType, sbyte> GetAttributesFromUser()
    {
        var attributes = (AttributeType[])Enum.GetValues(typeof(AttributeType));

        while (true)
        {
            var results = new Dictionary<AttributeType, sbyte>();

            foreach (var attr in attributes)
            {
                sbyte value = GetAttributeValue(attr);
                results[attr] = value;
            }

            int totalCost = 0;
            foreach (var v in results.Values)
            {
                totalCost += AttributeCost(v);
            }

            if (totalCost == 10)
            {
                return results;
            }

            Console.WriteLine($"A soma dos custos deu {totalCost}. Precisa ser exatamente 10. Vamos digitar os atributos novamente.\n");
        }
    }

    private static sbyte GetAttributeValue(AttributeType attributeName)
    {
        while (true)
        {
            Console.Write($"{attributeName}: ");
            var input = Console.ReadLine();
            if (!int.TryParse(input, out int val))
            {
                Console.WriteLine("Digite somente números entre -1 e 4.");
                continue;
            }

            if (val < -1 || val > 4)
            {
                Console.WriteLine("Valor inválido. Use um número entre -1 e 4.");
                continue;
            }

            return (sbyte)val;
        }
    }

    private static int AttributeCost(int value)
    {
        // Values -1..2 cost their own value; 3 costs 4; 4 costs 7
        return value switch
        {
            -1 => -1,
            0 => 0,
            1 => 1,
            2 => 2,
            3 => 4,
            4 => 7,
            _ => throw new ArgumentOutOfRangeException(nameof(value), "Valor de atributo fora do intervalo esperado")
        };
    }
}
