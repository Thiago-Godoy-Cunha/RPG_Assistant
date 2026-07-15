namespace RPG_Assistant.Config;

public static class DataPaths {
    private static readonly string Root = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Data");
    public static string Classes => Path.Combine(Root, "classes.json");
    public static string Origins => Path.Combine(Root, "origens.json");
    public static string Powers => Path.Combine(Root, "poderes.json");
    public static string Skills => Path.Combine(Root, "habilidades.json");
    public static string Racas => Path.Combine(Root, "racas.json");
}