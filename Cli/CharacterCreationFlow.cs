using RPG_Assistant.Enums;
using RPG_Assistant.Extensions;
using RPG_Assistant.Models;

namespace RPG_Assistant.Cli;

public static class CharacterCreationFlow {
    public static void Run() {
        OriginType selectedOrigin = EnumPrompt.Choose<OriginType>(
            "Qual é a sua origem?", StringExtensions.SplitPascalCase);

        ClassType selectedClass = EnumPrompt.Choose<ClassType>("Qual é a sua classe?");

        string name = NamePrompt.GetName();

        Character character = new Character(name, selectedClass);

        ExpertiseTrainingFlow.TrainObrigatoryExpertises(character);
        ExpertiseTrainingFlow.TrainOptionalExpertises(character);

        var attributes = AttributePrompt.GetAttributes();
        character.SetAttributes(attributes);

        ShowSummary(character);
    }

    private static void ShowSummary(Character character) {
        Console.Clear();
        Console.WriteLine(character.ToString());

        Console.WriteLine("\nAtributos definidos:");
        foreach (var attr in character.Attributes)
            Console.WriteLine($"{attr.Key}: {attr.Value}");

        Console.WriteLine("\nPerícias treinadas:");
        foreach (var exp in character.TrainedExpertises)
            Console.WriteLine($"{exp}");
    }
}