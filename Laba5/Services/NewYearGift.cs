using System;
using System.Collections.Generic;
using System.Linq;
using NewYearGiftProject.Interfaces;

namespace NewYearGiftProject.Services
{
    public class NewYearGift
    {
        public string GiftName { get; set; }
        private readonly List<ISweetComponent> _sweets = new List<ISweetComponent>();

        public NewYearGift(string giftName)
        {
            GiftName = giftName;
        }

        public void AddSweet(ISweetComponent sweet)
        {
            if (sweet != null) _sweets.Add(sweet);
        }

        public double CalculateTotalWeight()
        {
            return _sweets.Sum(s => s.WeightGrams);
        }

        public void SortByWeight()
        {
            _sweets.Sort();
        }

        public List<ISweetComponent> FindSweetsBySugarRange(double minSugar, double maxSugar)
        {
            return _sweets
                .Where(s => s.SugarContentGrams >= minSugar && s.SugarContentGrams <= maxSugar)
                .ToList();
        }

        public void PrintContents()
        {
            Console.WriteLine($"\n=== Состав подарка: '{GiftName}' ===");
            foreach (var sweet in _sweets)
            {
                Console.WriteLine($" - {sweet.GetDescription()}");
            }
            Console.WriteLine($"Общий вес подарка: {CalculateTotalWeight()} г.");
        }
    }
}
