using System;
using System.Text;
using NewYearGiftProject.Services;

namespace NewYearGiftProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            NewYearGift kidsGift = new GiftBuilder("Чароўная казка")
                .AddChocolate("Знiчка", 15.5, 8.2, 55, "шоколадно-ореховая")
                .AddCaramel("Дюшес", 6.0, 5.4, "груша")
                .AddWaffle("Черноморские", 25.0, 12.0, true)
               .AddChocolate("Красная шапочка", 20.0, 10.5, 40, "пралине с вафлей")
                .Build();

            kidsGift.PrintContents();

            kidsGift.SortByWeight();
            Console.WriteLine("\n--- После сортировки по весу (возрастание) ---");
            kidsGift.PrintContents();

            double minSugar = 5.0;
            double maxSugar = 9.0;
            Console.WriteLine($"\n--- Поиск сладостей с содержанием сахара от {minSugar}г до {maxSugar}г ---");
            var foundSweets = kidsGift.FindSweetsBySugarRange(minSugar, maxSugar);
            
            foreach (var sweet in foundSweets)
            {
                Console.WriteLine($" * {sweet.GetDescription()}");
            }
        }
    }
}
