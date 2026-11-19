using System;
using System.Collections.Generic;
using System.Linq;
using NewYearGiftProject.Interfaces;
using NewYearGiftProject.Services;

namespace NewYearGiftProject
{
    public static class GiftTests
    {
        public static void RunAllTests()
        {
            Console.WriteLine("\n==================================================");
            Console.WriteLine("        АВТОМАТИЧЕСКОЕ ТЕСТИРОВАНИЕ КЛАССОВ        ");
            Console.WriteLine("==================================================");

            int passedTests = 0;
            int totalTests = 3;

            if (Test_TotalWeightCalculation()) passedTests++;
            if (Test_SortingByWeight()) passedTests++;
            if (Test_FindBySugarRange()) passedTests++;

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

        private static bool Test_TotalWeightCalculation()
        {
            NewYearGift gift = new GiftBuilder("Тестовый подарок")
                .AddChocolate("Знічка", 15.5, 8.2, 55, "ореховая")
                .AddCaramel("Дюшес", 6.5, 5.4, "груша")
                .AddWaffle("Черноморские", 28.0, 12.0, true)
                .Build();

            string expected = "50";
            string actual = gift.CalculateTotalWeight().ToString();

            return AssertResult("Тест 1: Подсчет общего веса подарка", expected, actual);
        }

        private static bool Test_SortingByWeight()
        {
            NewYearGift gift = new GiftBuilder("Тестовый подарок")
                .AddWaffle("Вафля кузнечик", 30.0, 12.0, true)
                .AddCaramel("Легкая карамель", 5.0, 4.0, "яблоко")
                .AddChocolate("Средняя конфета", 15.0, 8.0, 50, "крем")
                .Build();

            gift.SortByWeight();

            string expectedOrder = "Легкая карамель, Средняя конфета, Вафля кузнечик";

            var sortedSweets = gift.FindSweetsBySugarRange(0, 100);
            string actualOrder = string.Join(", ", sortedSweets.Select(s => s.Name));

            return AssertResult("Тест 2: Сортировка сладостей по весу (IComparable)", expectedOrder, actualOrder);
        }

        private static bool Test_FindBySugarRange()
        {
            NewYearGift gift = new GiftBuilder("Тестовый подарок")
                .AddCaramel("Мало сахара", 10.0, 3.0, "мята")
                .AddChocolate("Знічка", 15.5, 8.2, 55, "ореховая")
                .AddWaffle("Много сахара", 25.0, 15.0, true)
                .Build();

            var found = gift.FindSweetsBySugarRange(5.0, 10.0);

            string expected = "Знічка";
            string actual = string.Join(", ", found.Select(s => s.Name));

            return AssertResult("Тест 3: Поиск конфет по диапазону содержания сахара", expected, actual);
        }
    }
}
