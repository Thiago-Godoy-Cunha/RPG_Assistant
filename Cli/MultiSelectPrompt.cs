namespace RPG_Assistant.Cli;

public static class MultiSelectPrompt {
    public static List<T> Choose<T>(
        string question,
        IReadOnlyList<T> options,
        int maxSelections,
        Func<T, string>? display = null) {
        display ??= item => item?.ToString() ?? "";
        var selected = new HashSet<int>();
        int cursor = 0;

        Console.Clear();
        Console.WriteLine(question);
        int originY = Console.CursorTop;

        while (true) {
            RenderCheckboxes(options, display, selected, cursor, originY);
            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Spacebar)
                ToggleSelection(selected, cursor, maxSelections, options.Count, originY);
            else if (key.Key is ConsoleKey.UpArrow or ConsoleKey.LeftArrow)
                cursor = (cursor - 1 + options.Count) % options.Count;
            else if (key.Key is ConsoleKey.DownArrow or ConsoleKey.RightArrow)
                cursor = (cursor + 1) % options.Count;
            else if (key.Key == ConsoleKey.Enter && selected.Count == maxSelections)
                break;
            else if (key.Key == ConsoleKey.Enter)
                ShowTemporaryMessage($"Escolha {maxSelections} opções para confirmar", originY + options.Count);
        }

        return selected.OrderBy(i => i).Select(i => options[i]).ToList();
    }

    private static void RenderCheckboxes<T>(
        IReadOnlyList<T> options, Func<T, string> display,
        HashSet<int> selected, int cursor, int originY) {
        Console.SetCursorPosition(0, originY);
        for (int i = 0; i < options.Count; i++) {
            string mark = selected.Contains(i) ? "X" : " ";
            string prefix = i == cursor ? "> " : "  ";
            Console.WriteLine($"{prefix}{display(options[i])} [{mark}]");
        }
    }

    private static void ToggleSelection(
        HashSet<int> selected, int cursor, int maxSelections, int optionCount, int originY) {
        if (selected.Contains(cursor)) {
            selected.Remove(cursor);
            return;
        }
        if (selected.Count >= maxSelections) {
            ShowTemporaryMessage($"Você não pode escolher mais de {maxSelections} opções", originY + optionCount);
            return;
        }
        selected.Add(cursor);
    }

    private static void ShowTemporaryMessage(string message, int line) {
        Console.SetCursorPosition(0, line);
        Console.Write(message);
        Thread.Sleep(1500);
        Console.SetCursorPosition(0, line);
        Console.Write(new string(' ', message.Length));
    }
}