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
        // Determine path relative to the running app so file is found when running from bin folder
        string path = Path.Combine(AppContext.BaseDirectory, "classes.json");
        if (!File.Exists(path)) {
            // fallback to current directory
            path = "classes.json";
        }

        //string json = File.ReadAllText("classes.json");
        //// Desserializa para um objeto tipado
        //var dados = JsonSerializer.Deserialize<Dados>(json);
        //Console.WriteLine(json);
        //Console.WriteLine("Qual é a sua classe?");
        //foreach(ClassType classe in Enum.GetValues(typeof(ClassType))) { 
        //    Console.WriteLine((int)(classe+1) + " - " + classe.ToString());
        //}
        //string _inputClass = Console.ReadLine();
        //Console.WriteLine("Qual é o nome do seu personagem?");
        //string _inputName = Console.ReadLine();
        //Character character = new Character(_inputName, _inputClass);

        // Read and print classes.json using the resolved path
        try {
            string json = File.ReadAllText("C:\\Users\\thiag\\source\\repos\\Thiago-Godoy-Cunha\\RPG_Assistant\\classes.json");
            var root = JsonSerializer.Deserialize<ClassesRoot>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (root?.Classes != null) {
                Console.WriteLine("Loaded classes from JSON:");
                foreach (var c in root.Classes) {
                    Console.WriteLine($"{c.Nome}: InitialHp={c.InitialHp}, HpPerLevel={c.HpPerLevel}, ManaPerLevel={c.ManaPerLevel}");
                }
            } else {
                Console.WriteLine("No classes found or JSON deserialization failed.");
            }
        } catch (Exception ex) {
            Console.WriteLine($"Failed to read or parse '{path}': {ex.Message}");
        }
    }
}
