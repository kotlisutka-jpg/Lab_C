using System;
using System.Collections.Generic;
using System.IO;
 
namespace GeneticSearch

{
    public class GeneticRepository
    {
        public List<GeneticData> ReadData(string filename)
        {
            List<GeneticData> data = new List<GeneticData>();

            if (!File.Exists(filename))
            {
                Console.WriteLine($"Ошибка: Файл данных {filename} не найден.");
                return data;
            }

            using (StreamReader reader = new StreamReader(filename))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    string[] parts = line.Split('\t');
                    if (parts.Length >= 3)
                    {
                        GeneticData item = new GeneticData
                        {
                            Protein = parts[0].Trim(),
                            Organism = parts[1].Trim(), 
                            
                            AminoAcids = RleCodec.Decode(parts[2].Trim())
                        };

                        data.Add(item);
                    }
                }
            }
            return data;
        }
    }
}
