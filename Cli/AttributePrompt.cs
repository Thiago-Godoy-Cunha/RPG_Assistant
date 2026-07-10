using RPG_Assistant.Enums;
using RPG_Assistant.Rules;

namespace RPG_Assistant.Cli;

public static class AttributePrompt {
    public static void PrintAttributeCostTable() {
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

    public static Dictionary<AttributeType, sbyte> GetAttributes() {
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
                    Console.Write($"\tRestam {(10 - totalCost).ToString("D2")} pontos");

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
                        Console.SetCursorPosition(cursorX + 2, cursorY);
                        Console.Write(" Erro: Digite somente números entre -1 e 4.");

                        Thread.Sleep(2000);
                        continue;
                    }

                    results[currentAttr] = (sbyte)val;
                    totalCost += AttributeRules.AttributeCost((sbyte)val);
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
}
