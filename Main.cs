using System;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using GType = Google.GenAI.Types;
using System.Text.Json;

public class Program
{
	static async Task Main(string[] args) {

        Console.WriteLine("--- RPG CHAT ---\nO que deseja?");
        string entrada = Console.ReadLine();

        RPGEngine rpgEngine = new RPGEngine();

        string resposta = await rpgEngine.SendMessage(entrada);
        Console.WriteLine("--- Resposta ---\n" + resposta);
    }
}