 using System;
using System.Collections.Generic;

namespace GeneticSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            //тесты
            GeneticTests.RunAllTests();

            Console.WriteLine("Запуск процесса генетического поиска...");

            string inputSequences = "sequences.txt";
            string inputCommands = "commands.txt";
            string outputFile = "genedata.txt";
 
            GeneticRepository repository = new GeneticRepository();
            Commands commandsProcessor = new Commands();
 
            List<GeneticData> database = repository.ReadData(inputSequences);
            Console.WriteLine($"Успешно загружено и расшифровано белков: {database.Count}");
 
            commandsProcessor.Process(inputCommands, outputFile, database);

            Console.WriteLine($"Обработка завершена. Результаты сохранены в файл: {outputFile}");
        }
    }
}
