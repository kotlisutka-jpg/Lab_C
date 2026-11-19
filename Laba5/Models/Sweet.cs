using System;
using NewYearGiftProject.Interfaces;

namespace NewYearGiftProject.Models
{
    public abstract class Sweet : ISweetComponent
    {
        public string Name { get; protected set; }
        public double WeightGrams { get; protected set; }
        public double SugarContentGrams { get; protected set; }

        protected Sweet(string name, double weightGrams, double sugarContentGrams)
        {
            Name = name;
            WeightGrams = weightGrams;
            SugarContentGrams = sugarContentGrams;
        }

        public abstract string GetDescription();

        public int CompareTo(ISweetComponent? other)
        {
            if (other == null) return 1;
            return WeightGrams.CompareTo(other.WeightGrams);
        }

        public override string ToString() => GetDescription();
    }
}
