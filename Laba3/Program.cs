using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Xml.Serialization;

namespace TokenizerGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("=== Запуск Лабораторной работы №3: Токенизатор ===\n");

            Console.Write($"Введите путь к файлу словаря (нажмите Enter для значения по умолчанию '{Constants.DefaultVocabularyPath}'): ");
            string vocabPath = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(vocabPath))
            {
                vocabPath = Constants.DefaultVocabularyPath;
            }

            if (!File.Exists(vocabPath))
            {
                Console.WriteLine($"Ошибка: Файл словаря '{vocabPath}' не найден.");
                return;
            }

            List<string> vocabulary = File.ReadAllLines(vocabPath)
                                           .Select(w => w.Trim())
                                           .Where(w => !string.IsNullOrEmpty(w))
                                           .ToList();
            HashSet<string> stopWords = new HashSet<string>(vocabulary, StringComparer.OrdinalIgnoreCase);

            TokenizerTests.RunAllTests(vocabulary);

            Console.Write($"Введите путь к файлу с исходным текстом (нажмите Enter для значения по умолчанию '{Constants.DefaultInputPath}'): ");
            string inputPath = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(inputPath))
            {
                inputPath = Constants.DefaultInputPath;
            }
            if (!File.Exists(inputPath))
            {
                File.WriteAllText(inputPath, "Они были многочисленны, но меня уже не догнать. Кто это делал?", Encoding.UTF8);
                Console.WriteLine($"[Инфо]: Файл '{inputPath}' не найден, создан автоматически с тестовым текстом.");
            }

            string inputText = File.ReadAllText(inputPath, Encoding.UTF8);

            string xmlFileName = Constants.DefaultXmlOutputPath;
            string replacement = Constants.DefaultReplacement;

            TextDocument doc = TextParser.Parse(inputText);
            

            Console.WriteLine("Исходный текст:");
            doc.PrintText();

            Console.WriteLine("\n-Пункт 1: Предложения по возрастанию кол. слов ");
            var sortedByWords = doc.GetSentencesOrderedByWordCount();
            sortedByWords.ForEach(s => Console.WriteLine($"[{s.Words.Count} слов] {s}"));

            Console.WriteLine("\n-Пункт 2: Предложения по возрастанию длины ");
            var sortedByLength = doc.GetSentencesOrderedByLength();
            sortedByLength.ForEach(s => Console.WriteLine($"[{s.ToString().Length} симв.] : {s}"));

            int targetLengthSearch = 3;
            Console.WriteLine($"\n-Пункт 3: Уникальные слова длиной {targetLengthSearch} в вопросах ");
            var uniqueWords = doc.FindUniqueWordsInQuestions(targetLengthSearch);
            if (uniqueWords.Any()) uniqueWords.ForEach(w => Console.WriteLine($" - {w}"));
            else Console.WriteLine("Слов не найдено.");

            int targetLengthReplace = 4;
            Console.WriteLine($"\n-Пункт 5: Замена слов длиной {targetLengthReplace} на '{replacement}' в 1-ом предложении ");
            doc.ReplaceWordsInSentence(sentenceIndex: 0, targetLengthReplace, replacement);
            doc.PrintText();

            int lengthToRemove = 3;
            Console.WriteLine($"\n-Пункт 4: Удаление слов длиной {lengthToRemove} на согласную ");
            doc.RemoveWordsStartingWithConsonant(lengthToRemove);
            doc.PrintText();

            Console.WriteLine("\n-Пункт 6: Удаление стоп-слов ");
            doc.RemoveStopWords(stopWords);
            doc.PrintText();

            Console.WriteLine("\n-Пункт 7: Экспорт в XML ");
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(TextDocument));
                using (StreamWriter writer = new StreamWriter(xmlFileName, false, Encoding.UTF8))
                {
                    serializer.Serialize(writer, doc);
                }
                Console.WriteLine("==================================================");
                Console.WriteLine(File.ReadAllText(xmlFileName));
                Console.WriteLine("==================================================");
            }
            catch (Exception ex) { Console.WriteLine($"Ошибка XML: {ex.Message}"); }
        }
    }
}
