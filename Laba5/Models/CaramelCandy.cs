namespace NewYearGiftProject.Models
{
    public class CaramelCandy : Sweet
    {
        public string FruitFlavor { get; private set; }

        public CaramelCandy(string name, double weight, double sugar, string fruitFlavor)
            : base(name, weight, sugar)
        {
            FruitFlavor = fruitFlavor;
        }

        public override string GetDescription()
        {
            return $"Карамель '{Name}' (вкус: {FruitFlavor}, вес: {WeightGrams}г, сахар: {SugarContentGrams}г)";
        }
    }
}
