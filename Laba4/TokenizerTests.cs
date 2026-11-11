 using System;
using System.Collections.Generic;
using System.Linq;

namespace TokenizerGame
{
    public static class TokenizerTests
    {
        public static void RunAllTests(List<string> vocabulary)
        {
            Console.WriteLine("\n==================================================");
            Console.WriteLine("        АВТОМАТИЧЕСКОЕ ТЕСТИРОВАНИЕ КЛАССОВ        ");
            Console.WriteLine("==================================================");

            HashSet<string> stopWords = new HashSet<string>(vocabulary, StringComparer.OrdinalIgnoreCase);

            int passedTests = 0;
            int totalTests = 6;

            if (Test_SentenceSortingByWordCount()) passedTests++;
            if (Test_SentenceSortingByLength()) passedTests++;
            if (Test_QuestionSentenceSearch()) passedTests++;
            if (Test_RemoveWordsStartingWithConsonant()) passedTests++;
            if (Test_ReplaceWordsInSentence()) passedTests++;
            if (Test_StopWordsRemoval(stopWords)) passedTests++;

            Console.WriteLine("==================================================");
            Console.WriteLine($"ИТОГ ТЕСТИРОВАНИЯ: Успешно пройдено {passedTests} из {totalTests} тестов.");
            Console.WriteLine("==================================================\n");
        }

        private static bool AssertResult(string testName, string expected, string actual)
        {
            if (expected.Trim() == actual.Trim())
            {
                Console.WriteLine($"[УСПЕХ] {testName}");
                return true;
            }
            else
            {
                Console.WriteLine($"[ПРОВАЛ] {testName}");
                Console.WriteLine($"  -> Ожидалось: '{expected}'");
                Console.WriteLine($"  -> Получено:  '{actual}'");
                return false;
            }
        }

        private static bool Test_SentenceSortingByWordCount()
        {
            string sourceText = "Много слов тут. Мало. Среднее число слов.";
            TextDocument doc = TextParser.Parse(sourceText);

            string expectedOrder = "Мало. Много слов тут. Среднее число слов.";

            var sortedSentences = doc.GetSentencesOrderedByWordCount();
            string actualOrder = string.Join(" ", sortedSentences.Select(s => s.ToString()));

            return AssertResult("Пункт 1: Сортировка по количеству слов", expectedOrder, actualOrder);
        }

        private static bool Test_SentenceSortingByLength()
        {
            string sourceText = "Длинное предложение. Ого. Среднее слово.";
            TextDocument doc = TextParser.Parse(sourceText);

            string expectedOrder = "Ого. Среднее слово. Длинное предложение.";

            var sortedSentences = doc.GetSentencesOrderedByLength();
            string actualOrder = string.Join(" ", sortedSentences.Select(s => s.ToString()));

            return AssertResult("Пункт 2: Сортировка по длине предложения", expectedOrder, actualOrder);
        }

        private static bool Test_QuestionSentenceSearch()
        {
            string sourceText = "Это обычный текст. Кто там пришел? Что там?";
            TextDocument doc = TextParser.Parse(sourceText);

            string expectedWords = "кто, там, что";

            var actualList = doc.FindUniqueWordsInQuestions(length: 3);
            string actualWords = string.Join(", ", actualList);

            return AssertResult("Пункт 3: Поиск уникальных слов в вопросах", expectedWords, actualWords);
        }

        private static bool Test_RemoveWordsStartingWithConsonant()
        {
            string sourceText = "Кот ел рыбу и пил."; 
            TextDocument doc = TextParser.Parse(sourceText);

            string expectedText = "ел рыбу и.";

            doc.RemoveWordsStartingWithConsonant(length: 3);
            string actualText = string.Join(" ", doc.Sentences.Select(s => s.ToString()));

            return AssertResult("Пункт 4: Удаление слов на согласную", expectedText, actualText);
        }

        private static bool Test_ReplaceWordsInSentence()
        {   
            string sourceText = "Этот тест наша проверка.";
            TextDocument doc = TextParser.Parse(sourceText);

            string expectedText = "[Глоорп] [Глоорп] [Глоорп] проверка.";

            doc.ReplaceWordsInSentence(sentenceIndex: 0, length: 4, replacement: "[Глоорп]");
            string actualText = doc.Sentences[0].ToString();

            return AssertResult("Пункт 5: Замена слов заданной длины на подстроку", expectedText, actualText);
        }

        private static bool Test_StopWordsRemoval(HashSet<string> stopWords)
        {
            string sourceText = "они были многочисленны но меня уже не догнать.";
            TextDocument doc = TextParser.Parse(sourceText);

            string expectedText = "многочисленны догнать.";

            doc.RemoveStopWords(stopWords);
            string actualText = string.Join(" ", doc.Sentences.Select(s => s.ToString()));

            return AssertResult("Пункт 6: Удаление стоп-слов по словарю", expectedText, actualText);
        }
    }
}
