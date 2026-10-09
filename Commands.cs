using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
 
namespace GeneticSearch

{
    public class Commands
    {
        public void Process(string commandsFilename, string outputFilename, List<GeneticData> proteins)
        {
            if (!File.Exists(commandsFilename))
            {
                Console.WriteLine($"Ошибка: Файл команд {commandsFilename} не найден.");
                return;
            }

            using (StreamReader reader = new StreamReader(commandsFilename))
            using (StreamWriter writer = new StreamWriter(outputFilename, false, Encoding.UTF8))
            {
                writer.WriteLine("Ivan Ivanov");
                writer.WriteLine("Genetic Searching");

                int commandCounter = 1;

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    string[] parts = line.Split('\t');
                    GeneticCommand cmd = new GeneticCommand
                    {
                        Name = parts[0].Trim(),
                        Parameter1 = parts.Length > 1 ? parts[1].Trim() : string.Empty,
                        Parameter2 = parts.Length > 2 ? parts[2].Trim() : string.Empty
                    };

                    string idStr = commandCounter.ToString("D3");
                    commandCounter++;

                    writer.WriteLine("--------------------------------------------------------------------------");

                    switch (cmd.Name.ToLower())
                    {
                        case "search":
                            ExecuteSearch(writer, idStr, cmd.Parameter1, proteins);
                            break;
                        case "diff":
                            ExecuteDiff(writer, idStr, cmd.Parameter1, cmd.Parameter2, proteins);
                            break;
                        case "mode":
                            ExecuteMode(writer, idStr, cmd.Parameter1, proteins);
                            break;
                    }
                }

                writer.WriteLine("--------------------------------------------------------------------------");
            }
        }

        private void ExecuteSearch(StreamWriter writer, string id, string querySequence, List<GeneticData> proteins)
        {
            writer.WriteLine($"{id}   search   {querySequence} ");
            writer.WriteLine("organism\t\t\t\tprotein ");

            bool foundAny = false;
            foreach (var item in proteins)
            {
                if (item.AminoAcids.Contains(querySequence))
                {
                    writer.WriteLine($"{item.Organism}\t\t{item.Protein}");
                    foundAny = true;
                }
            }

            if (!foundAny) writer.WriteLine("NOT FOUND");
        }

        private void ExecuteDiff(StreamWriter writer, string id, string protein1Name, string protein2Name, List<GeneticData> proteins)
        {
            writer.WriteLine($"{id}   diff   {protein1Name}   {protein2Name} ");
            writer.WriteLine("amino-acids difference:");

            var p1 = proteins.FirstOrDefault(p => p.Protein.Equals(protein1Name, StringComparison.OrdinalIgnoreCase));
            var p2 = proteins.FirstOrDefault(p => p.Protein.Equals(protein2Name, StringComparison.OrdinalIgnoreCase));

            bool missing1 = string.IsNullOrEmpty(p1.Protein);
            bool boolMissing2 = string.IsNullOrEmpty(p2.Protein);

            if (missing1 || boolMissing2)
            {
                writer.Write("MISSING: ");
                if (missing1 && boolMissing2) writer.WriteLine($"{protein1Name}, {protein2Name}");
                else if (missing1) writer.WriteLine(protein1Name);
                else writer.WriteLine(protein2Name);
                return;
            }

            string s1 = p1.AminoAcids;
            string s2 = p2.AminoAcids;
            int minLength = Math.Min(s1.Length, s2.Length);
            int differenceCount = 0;

            for (int i = 0; i < minLength; i++)
            {
                if (s1[i] != s2[i]) differenceCount++;
            }

            differenceCount += Math.Abs(s1.Length - s2.Length);
            writer.WriteLine(differenceCount);
        }

        private void ExecuteMode(StreamWriter writer, string id, string proteinName, List<GeneticData> proteins)
        {
            writer.WriteLine($"{id}   mode   {proteinName} ");
            writer.WriteLine("amino-acid occurs:");

            var targetProtein = proteins.FirstOrDefault(p => p.Protein.Equals(proteinName, StringComparison.OrdinalIgnoreCase));

            if (string.IsNullOrEmpty(targetProtein.Protein))
            {
                writer.WriteLine($"MISSING: {proteinName}");
                return;
            }

            string sequence = targetProtein.AminoAcids;
            if (string.IsNullOrEmpty(sequence))
            {
                writer.WriteLine("Ошибка: последовательность пуста.");
                return;
            }

            var frequencies = new Dictionary<char, int>();
            foreach (char ch in sequence)
            {
                if (!char.IsLetter(ch)) continue;

                if (frequencies.ContainsKey(ch)) frequencies[ch]++;
                else frequencies[ch] = 1;
            }

            var bestAminoAcid = frequencies.OrderByDescending(kv => kv.Value).ThenBy(kv => kv.Key).First();
            writer.WriteLine($"{bestAminoAcid.Key}          {bestAminoAcid.Value}");
        }
    }
}
