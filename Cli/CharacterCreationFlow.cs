using RPG_Assistant.Enums;
using RPG_Assistant.Extensions;
using RPG_Assistant.Models;

namespace RPG_Assistant.Cli;

public static class CharacterCreationFlow {
    public static void Run() {
        string name = NamePrompt.GetName();

        ClassType selectedClass = EnumPrompt.Choose<ClassType>("Qual é a sua classe?");

        RaceType selectedRace = EnumPrompt.Choose<RaceType>("Qual sua raça?");

        OriginType selectedOrigin = EnumPrompt.Choose<OriginType>(
            "Qual é a sua origem?", StringExtensions.SplitPascalCase);

        var attributes = AttributePrompt.GetAttributes();

        Character character = new Character(name, selectedClass, selectedOrigin, selectedRace, attributes);

        ExpertiseTrainingFlow.TrainObrigatoryExpertises(character);
        ExpertiseTrainingFlow.TrainOptionalExpertises(character);

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