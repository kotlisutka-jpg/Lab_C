using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TokenizerGame
{
    public static class TokenizerTests
    {
        public static void RunAllTests(List<string> vocabulary)
        {
            Console.WriteLine("\n____________ЗАПУСК ТЕСТОВ (ЛАБА 3)_____________");
            try
            {
                TestExactWordTokenization(vocabulary);
                TestSubwordTokenization(vocabulary);
                Console.WriteLine("ТЕСТЫ ТРЕТЬЕЙ ЛАБЫ УСПЕШНО ПРОЙДЕНЫ");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ТЕСТ ПРОВАЛЕН: {ex.Message}");
            }
            Console.WriteLine("_______________________________________________\n");
        }

        private static void TestExactWordTokenization(List<string> vocabulary)
        {
            // Проверяем токенизацию слова, которое целиком есть в словаре
            List<string> result = Program.Tokenize("они", vocabulary);
            
            Debug.Assert(result.Count == 1, $"Ожидался 1 токен, получено {result.Count}"); 
            Debug.Assert(result[0] == "они", $"Ожидался токен 'они', получено '{result[0]}'");

        }

        private static void TestSubwordTokenization(List<string> vocabulary)
        {
            
            List<string> result = Program.Tokenize("многочисленные", vocabulary);
            
            Debug.Assert(result.Contains("многочисленные"), "Токен 'многочисленные' должен быть успешно выделен");
        }
    }
}
