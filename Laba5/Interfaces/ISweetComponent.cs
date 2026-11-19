using System;

namespace NewYearGiftProject.Interfaces
{
    public interface ISweetComponent : IComparable<ISweetComponent>
    {
        string Name { get; }
        double WeightGrams { get; }
        double SugarContentGrams { get; }
        string GetDescription();
    }
}
