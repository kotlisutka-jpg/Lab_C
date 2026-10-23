using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TokenizerGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Запуск Лабораторной работы №3: Токенизатор...");

            string vocabPath = "vocabulary.txt";
            if (!File.Exists(vocabPath))
            {
                Console.WriteLine($"Ошибка: Файл словаря {vocabPath} не найден.");
                return;
            }

            // Загружаем словарь и сортируем токена от самых длинных к коротким
            List<string> vocabulary = File.ReadAllLines(vocabPath)
                                           .Select(w => w.Trim())
                                           .Where(w => !string.IsNullOrEmpty(w))
                                           .OrderByDescending(w => w.Length)
                                           .ToList();

             TokenizerTests.RunAllTests(vocabulary);


            string inputText = "они были многочисленные но меня уже не догнать";
            Console.WriteLine($"\nИсходный текст: \"{inputText}\"");

            List<string> tokens = Tokenize(inputText, vocabulary);

            Console.WriteLine("\nРезультат токенизации:");
            foreach (var token in tokens)
            {
                Console.WriteLine($"- {token}");
            }
        }
        public static List<string> Tokenize(string text, List<string> vocab)
        {
            List<string> result = new List<string>();
            string[] words = text.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var word in words)
            {
                string remaining = word.ToLower();
                while (remaining.Length > 0)
                {
                    string match = vocab.FirstOrDefault(t => remaining.StartsWith(t));

                    if (match != null)
                    {
                        result.Add(match);
                        remaining = remaining.Substring(match.Length);
                    }
                    else
                    {
                        // Если символ не найден в словаре, выделяем его как неизвестный токен
                        result.Add(remaining[0].ToString());
                        remaining = remaining.Substring(1);
                    }
                }
            }
            return result;
        }
    }
}
