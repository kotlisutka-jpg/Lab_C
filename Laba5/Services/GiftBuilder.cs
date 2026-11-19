using NewYearGiftProject.Interfaces;
using NewYearGiftProject.Models;

namespace NewYearGiftProject.Services
{
    public class GiftBuilder : IGiftBuilder
    {
        private NewYearGift _gift;

        public GiftBuilder(string giftName)
        {
            _gift = new NewYearGift(giftName);
        }

        public IGiftBuilder AddChocolate(string name, double weight, double sugar, int cocoa, string filling)
        {
            _gift.AddSweet(new ChocolateCandy(name, weight, sugar, cocoa, filling));
            return this;
        }

        public IGiftBuilder AddCaramel(string name, double weight, double sugar, string flavor)
        {
            _gift.AddSweet(new CaramelCandy(name, weight, sugar, flavor));
            return this;
        }

        public IGiftBuilder AddWaffle(string name, double weight, double sugar, bool isGlazed)
        {
            _gift.AddSweet(new WaffleSweet(name, weight, sugar, isGlazed));
            return this;
        }

        public NewYearGift Build()
        {
            return _gift;
        }
    }
}
