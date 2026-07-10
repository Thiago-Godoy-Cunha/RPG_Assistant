namespace RPG_Assistant.Rules;
public static class AttributeRules {
    public static sbyte AttributeCost(sbyte value) {
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
}
