namespace NewYearGiftProject.Models
{
    public class ChocolateCandy : Sweet
    {
        public int CocoaPercentage { get; private set; }
        public string Filling { get; private set; }

        public ChocolateCandy(string name, double weight, double sugar, int cocoaPercentage, string filling)
            : base(name, weight, sugar)
        {
            CocoaPercentage = cocoaPercentage;
            Filling = filling;
        }

        public override string GetDescription()
        {
            return $"Шоколадная конфета '{Name}' (начинка: {Filling}, какао: {CocoaPercentage}%, вес: {WeightGrams}г, сахар: {SugarContentGrams}г)";
        }
    }
}
