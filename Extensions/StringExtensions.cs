using System.Text;
namespace RPG_Assistant.Extensions;

public static class StringExtensions {
    public static string SplitPascalCase(string input) {
        if (string.IsNullOrEmpty(input))
            return input;
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