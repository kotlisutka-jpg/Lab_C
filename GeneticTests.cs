using System;
using System.Diagnostics;

namespace GeneticSearch
{
    public static class GeneticTests
    {
        public static void RunAllTests()
        {
            Console.WriteLine("\n____________ЗАПУСК ТЕСТОВ_____________");
            
            try
            {
                TestRleDecoding();
                TestRleEncoding();
                Console.WriteLine("ТЕСТЫ УСПЕШНО ПРОЙДЕНЫ");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ТЕСТ ПРОВАЛЕН: {ex.Message}");
            }
            Console.WriteLine("______________________________\n");
        }

        private static void TestRleDecoding()
        {
            //проверка, что "3A2B" правильно превращается в "AAABB"
            string result = RleCodec.Decode("3A2B");
            Debug.Assert(result == "AAABB", $"Ожидалось AAABB, но получено {result}");
            
            //проверка, на одиночную букву
            string result2 = RleCodec.Decode("X");
            Debug.Assert(result2 == "X", $"Ожидалось X, но получено {result2}");
        }

        private static void TestRleEncoding()
        {
            //проверка, что "AAABB" правильно сжимается в "3A2B" (или "3ABB" по вашей логике)
            string result = RleCodec.Encode("AAABB"); 
            Debug.Assert(result == "3ABB", $"Ожидалось 3ABB, но получено {result}"); 
        }
    }
}
