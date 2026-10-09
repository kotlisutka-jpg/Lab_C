namespace GeneticSearch

{
    public struct GeneticData
    {
        public string Protein { get; set; }
        public string Organism { get; set; }
        public string AminoAcids { get; set; }
    }

    public struct GeneticCommand
    {
        public string Name { get; set; }
        public string Parameter1 { get; set; }
        public string Parameter2 { get; set; }
    }
}
