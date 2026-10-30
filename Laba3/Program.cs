using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace TokenizerGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("=== Запуск Лабораторной работы №3: Токенизатор ===\n");

            string vocabPath = "vocabulary.txt";
            if (!File.Exists(vocabPath)) return;

            List<string> vocabulary = File.ReadAllLines(vocabPath).Select(w => w.Trim()).Where(w => !string.IsNullOrEmpty(w)).ToList();
            HashSet<string> stopWords = new HashSet<string>(vocabulary, StringComparer.OrdinalIgnoreCase);

            string inputPath = "input.txt";
            if (!File.Exists(inputPath))
            {
                File.WriteAllText(inputPath, "Они были многочисленны, но меня уже не догнать. Кто это делал?", Encoding.UTF8);
            }
            string inputText = File.ReadAllText(inputPath, Encoding.UTF8);

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
            string replacement = "[Глоорп]";
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
            string xmlFileName = "result_output.xml";
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
