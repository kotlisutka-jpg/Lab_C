using System.Collections.Generic;

namespace TokenizerGame
{
    public class ConcordanceEntry
    {
        public int Frequency { get; set; }
        public SortedSet<int> LineNumbers { get; } = new SortedSet<int>();

        public void AddOccurrence(int lineNumber)
        {
            Frequency++;
            LineNumbers.Add(lineNumber);
        }
    }
}
