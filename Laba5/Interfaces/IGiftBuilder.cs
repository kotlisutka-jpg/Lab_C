using NewYearGiftProject.Services;

namespace NewYearGiftProject.Interfaces
{
    public interface IGiftBuilder
    {
        IGiftBuilder AddChocolate(string name, double weight, double sugar, int cocoa, string filling);
        IGiftBuilder AddCaramel(string name, double weight, double sugar, string flavor);
        IGiftBuilder AddWaffle(string name, double weight, double sugar, bool isGlazed);
        NewYearGift Build();
    }
}
