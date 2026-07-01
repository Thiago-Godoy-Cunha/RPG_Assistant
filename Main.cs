using System;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using GType = Google.GenAI.Types;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;
using RPG_Assistant.Enums;
using RPG_Assistant.Models;

public class ClassesRoot {
    public List<Class> Classes { get; set; }
}

public class Program
{
    static void Main(string[] args) {
        Console.WriteLine("Qual é a sua classe?");
        foreach (ClassType classe in Enum.GetValues(typeof(ClassType))) {
            Console.WriteLine((int)(classe + 1) + " - " + classe.ToString());
        }
        string _inputClass = Console.ReadLine();
        Console.WriteLine("Qual é o nome do seu personagem?");
        string _inputName = Console.ReadLine();
        Character character = new Character(_inputName, _inputClass);
        Console.WriteLine(character);
    }
}
