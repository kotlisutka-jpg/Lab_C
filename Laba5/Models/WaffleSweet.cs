namespace NewYearGiftProject.Models
{
    public class WaffleSweet : Sweet
    {
        public bool IsGlazed { get; private set; }

        public WaffleSweet(string name, double weight, double sugar, bool isGlazed)
            : base(name, weight, sugar)
        {
            IsGlazed = isGlazed;
        }

        public override string GetDescription()
        {
            string glazeInfo = IsGlazed ? "в глазури" : "без глазури";
            return $"Вафля '{Name}' ({glazeInfo}, вес: {WeightGrams}г, сахар: {SugarContentGrams}г)";
        }
    }
}
