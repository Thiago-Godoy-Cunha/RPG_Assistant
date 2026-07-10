using RPG_Assistant.Cli;

public static class EnumPrompt {
    public static T Choose<T>(string question, Func<string, string>? formatDisplay = null)
        where T : struct, Enum {
        T[] values = Enum.GetValues<T>();
        formatDisplay ??= name => name;
        return SelectionPrompt.Choose(question, values, v => formatDisplay(v.ToString()));
    }
}