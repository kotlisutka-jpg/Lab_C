using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace TokenizerGame
{ // переделать логику тестов: у нас должно быть уже нужный ответ и исходное значени, мы
  // мы пропускаем наше исходное значение по функции теста и сравниваем с тем, что должно
  // получиться в итиоге, и их сравнение и показывает работает ли код
    public static class TokenizerTests
    {
        public static void RunAllTests(List<string> vocabulary)
        {
            Console.WriteLine("\n==================================================");
            Console.WriteLine("        ЗАПУСК ТЭСТОВ ЛАБАРАТОРНАЙ РАБОТЫ №3       "); 

            HashSet<string> stopWords = new HashSet<string>(vocabulary, StringComparer.OrdinalIgnoreCase);

            Test_SentenceParsingAndSorting();
            Test_QuestionSentenceSearch();
            Test_RemoveWordsStartingWithConsonant();
            Test_ReplaceWordsInSentence();
            Test_StopWordsRemoval(stopWords);
            Test_XmlSerialization();
 
            Console.WriteLine("\n              ТЭСТЫ ЗАВЕРШАНЫ                  ");
            Console.WriteLine("==================================================\n");
        }

        private static void Test_SentenceParsingAndSorting()
        {
            Console.WriteLine("\n[ТЕСТ 1 и 2]: Сортировка предложений");
            string testText = "Короткое слово. Это очень длинное предложение для теста. Среднее предложение тут.";
            TextDocument doc = TextParser.Parse(testText);

            Console.WriteLine("  > По возрастанию количества слов:");
            var sortedByWords = doc.Sentences.OrderBy(s => s.Words.Count).ToList();
            foreach (var s in sortedByWords)
            {
                Console.WriteLine($"    - [{s.Words.Count} слов]: {s}");
            }

            Console.WriteLine("  > По возрастанию общей длины символов:");
            var sortedByLength = doc.Sentences.OrderBy(s => s.ToString().Length).ToList();
            foreach (var s in sortedByLength)
            {
                Console.WriteLine($"    - [{s.ToString().Length} симв.]: {s}");
            }
        }

        private static void Test_QuestionSentenceSearch()
        {
            Console.WriteLine("\n[ТЕСТ 3]: Поиск в вопросительных предложениях");
            string testText = "Это обычное предложение. Кто пришел? Что там произошло?";
            TextDocument doc = TextParser.Parse(testText);
            int targetLength = 3;

            var words = doc.Sentences
                .Where(s => s.IsQuestion)
                .SelectMany(s => s.Words)
                .Where(w => w.Length == targetLength)
                .Select(w => w.Value.ToLower())
                .Distinct()
                .ToList();

            Console.WriteLine($"  > Найденные слова длиной {targetLength} в вопросах (без повторов):");
            if (words.Count > 0)
            {
                words.ForEach(w => Console.WriteLine($"    - {w}"));
            }
            else
            {
                Console.WriteLine("    - Слов не найдено.");
            }
        }

        private static void Test_RemoveWordsStartingWithConsonant()
        {
            Console.WriteLine("\n[ТЕСТ 4]: Удаление слов на согласную букву");
            string testText = "Кот ел рыбу и пил воду.";
            TextDocument doc = TextParser.Parse(testText);
            int lengthToRemove = 3;

            Console.WriteLine("  > До удаления:");
            doc.PrintText();
            
            foreach (var sentence in doc.Sentences)
            {
                sentence.Tokens.RemoveAll(t => t is Word w && w.Length == lengthToRemove && w.StartsWithConsonant());
            }

            Console.WriteLine($"  > После удаления слов длиной {lengthToRemove} на согласную:");
            doc.PrintText();
        }


        private static void Test_ReplaceWordsInSentence()
        {
            Console.WriteLine("\n[ТЕСТ 5]: Замена слов в предложении на подстроку");
            string testText = "Они купили новый дом.";
            TextDocument doc = TextParser.Parse(testText);
            int targetLength = 4;
            string replacement = "[Глоорп]";

            if (doc.Sentences.Count > 0)
            {
                Console.WriteLine($"  > До замены: {doc.Sentences[0]}");

                var sentence = doc.Sentences[0];
                for (int i = 0; i < sentence.Tokens.Count; i++)
                {
                    if (sentence.Tokens[i] is Word w && w.Length == targetLength)
                    {
                        sentence.Tokens[i] = new Word(replacement);
                    }
                }

                Console.WriteLine($"  > После замены слов длиной {targetLength}:");
            }
        }

        private static void Test_StopWordsRemoval(HashSet<string> stopWords)
        {
            Console.WriteLine("\n[ТЕСТ 6]: Удаление стоп-слов на основе словаря");
            string testText = "они были многочисленные но меня уже не догнать.";
            TextDocument doc = TextParser.Parse(testText);

            Console.WriteLine("  > Текст до очистки:");
            doc.PrintText();

            foreach (var sentence in doc.Sentences)
            {
                sentence.Tokens.RemoveAll(t => t is Word w && stopWords.Contains(w.Value));
            }

            Console.WriteLine("  > Текст после удаления стоп-слов:");
            doc.PrintText();
        }

        private static void Test_XmlSerialization()
        {
            Console.WriteLine("\n[ТЕСТ 7]: Экспорт объекта Текст в XML");
            string testText = "Mary had a lamb.";
            TextDocument doc = TextParser.Parse(testText);
            string testXmlPath = "test_output.xml";

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(TextDocument));
                using (StreamWriter writer = new StreamWriter(testXmlPath, false, Encoding.UTF8))
                {
                    serializer.Serialize(writer, doc);
                }
                
                if (File.Exists(testXmlPath))
                {
                    Console.WriteLine($"  > Тест пройден. Файл XML успешно создан. Размер: {new FileInfo(testXmlPath).Length} байт.");
                    File.Delete(testXmlPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  > [ОШИБКА ТЕСТА XML]: {ex.Message}");
            }
        }
    }
}
