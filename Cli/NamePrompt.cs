namespace RPG_Assistant.Cli;

public static class NamePrompt {
    public static string GetName() {
        while (true) {
            Console.Clear();
            Console.WriteLine("Qual é o nome do seu personagem?");
            string _inputName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(_inputName)) {
                Console.WriteLine("Escolha uma opção válida");
                continue;
            }
            return _inputName;
        }
    }
}
