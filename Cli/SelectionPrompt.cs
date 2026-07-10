namespace RPG_Assistant.Cli;

public static class SelectionPrompt {
    public static T Choose<T>(
        string question, // Textinho pra contextualizar usuário
        IReadOnlyList<T> options, // As opções de escolha
        Func<T, string>? display = null, // Entra qualquer coisa, sai string
        bool allowRandom = true) {
        display ??= item => item?.ToString() ?? ""; // Demorei uma hora pra entender essa linha, mas é só uma verificação se display foi passado e uma verificação se o item tem algo que possa retornar uma string

        RenderMenu(question, options, display, allowRandom);

        while (true) {
            string? input = Console.ReadLine();

            if (TryGetRandom(input, options, allowRandom, out T randomPick))
                return randomPick;

            if (TryGetByIndex(input, options, out T byIndex))
                return byIndex;

            if (TryGetByName(input, options, display, out T byName))
                return byName;

            Console.WriteLine("Escolha uma opção válida");
        }
    }

    // Renderizador das opções
    private static void RenderMenu<T>(
        string question, IReadOnlyList<T> options, Func<T, string> display, bool allowRandom) {
        Console.Clear();
        Console.WriteLine(question);
        for (int i = 0; i < options.Count; i++)
            Console.WriteLine($"{i + 1} - {display(options[i])}");
        if (allowRandom)
            Console.WriteLine("0 - Escolher aleatório");
    }

    // Facilitadores de entendimento de input //
    // Verifica se a opção escolhida foi aleatório caso permitido
    private static bool TryGetRandom<T>(
        string? input, IReadOnlyList<T> options, bool allowRandom, out T result) {
        result = default!;
        if (!allowRandom || input != "0")
            return false;
        result = options[Random.Shared.Next(options.Count)];
        return true;
    }

    // Verifica se é o index da opção
    private static bool TryGetByIndex<T>(string? input, IReadOnlyList<T> options, out T result) {
        result = default!;
        if (!int.TryParse(input, out int parsed))
            return false;
        if (parsed < 1 || parsed > options.Count)
            return false;
        result = options[parsed - 1];
        return true;
    }

    // Verifica se é o nome da opção
    private static bool TryGetByName<T>(
        string? input, IReadOnlyList<T> options, Func<T, string> display, out T result) {
        result = default!;
        if (string.IsNullOrWhiteSpace(input))
            return false;

        foreach (T option in options) {
            if (string.Equals(display(option), input, StringComparison.OrdinalIgnoreCase)) {
                result = option;
                return true;
            }
        }
        return false;
    }
}