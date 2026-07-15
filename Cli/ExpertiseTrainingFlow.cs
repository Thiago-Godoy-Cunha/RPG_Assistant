using RPG_Assistant.Enums;
using RPG_Assistant.Loading;
using RPG_Assistant.Models;
using RPG_Assistant.Rules;

namespace RPG_Assistant.Cli;

public static class ExpertiseTrainingFlow {
    public static void TrainObrigatoryExpertises(Character character) {
        List<string>[] expertisesList = ExpertiseDataLoader.GetClassExpertises(character.InitialClass.Name);
        List<string> obrigatoryRaw = expertisesList[0];

        Console.Clear();
        Console.WriteLine(character.ToString());

        if (ExpertiseRules.HasObrigatoryChoice(obrigatoryRaw)) {
            var options = ExpertiseRules.GetChoiceOptions(obrigatoryRaw);
            ExpertiseType chosen = SelectionPrompt.Choose(
                "\nEscolha uma das perícias obrigatórias:", options, allowRandom: false);
            character.TrainExpertise(chosen);
        }

        foreach (var expertise in ExpertiseRules.GetAutoTrainedObrigatoryExpertises(obrigatoryRaw))
            character.TrainExpertise(expertise);
    }

    public static void TrainOptionalExpertises(Character character) {
        List<string>[] expertisesList = ExpertiseDataLoader.GetClassExpertises(character.InitialClass.Name);
        List<string> optionalRaw = expertisesList[1];
        byte qtdOptExpertises = ExpertiseDataLoader.GetQtdOptExpertises(character.InitialClass.Name);

        var allOptions = ExpertiseRules.GetOptionalExpertiseOptions(optionalRaw);
        var availableOptions = SelectionGuard.ExcludeAlreadyChosen(allOptions, character.TrainedExpertises);

        Console.Clear();
        Console.WriteLine(character.ToString());

        List<ExpertiseType> chosen = MultiSelectPrompt.Choose(
            $"\nEscolha {qtdOptExpertises} perícias opcionais:", availableOptions, qtdOptExpertises);

        foreach (var expertise in chosen)
            character.TrainExpertise(expertise);
    }
}