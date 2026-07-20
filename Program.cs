using RPG_Assistant.Cli;
using RPG_Assistant.Models;
Item item = Item.Create("Arco Curto");
Console.WriteLine(item.Name + "\n" + item.Description);
Item item2 = Item.Create("Flechas");
Console.WriteLine(item2.Name + "\n" + item2.Description);
CharacterCreationFlow.Run();