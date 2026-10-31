using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace TokenizerGame
{
    public class Sentence
    {
        [XmlArray("Tokens")]
        [XmlArrayItem(typeof(Token))]
        public List<Token> Tokens { get; set; } = new List<Token>();

        [XmlIgnore]
        public List<Word> Words => Tokens.OfType<Word>().ToList();

        [XmlIgnore]
        public bool IsQuestion => Tokens.LastOrDefault() is Punctuation p && p.Value.Contains("?");

        public void ReplaceWordsByLength(int length, string replacement)
        {
            for (int i = 0; i < Tokens.Count; i++)
            {
                if (Tokens[i] is Word w && w.Length == length)
                {
                    Tokens[i] = new Word(replacement);
                }
            }
        }

        public void RemoveWordsStartingWithConsonant(int length)
        {
            Tokens.RemoveAll(t => t is Word w && w.Length == length && w.StartsWithConsonant());
        }

        public void RemoveStopWords(HashSet<string> stopWords)
        {
            Tokens.RemoveAll(t => t is Word w && stopWords.Contains(w.Value));
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Tokens.Count; i++)
            {
                if (i > 0 && Tokens[i] is Word && !(Tokens[i - 1] is Punctuation p && p.Value == "-"))
                {
                    sb.Append(" ");
                }
                sb.Append(Tokens[i].Value);
            }
            return sb.ToString();
        }
    }

    [XmlRoot("text")]
    public class TextDocument
    {
        [XmlElement("sentence")]
        public List<Sentence> Sentences { get; set; } = new List<Sentence>();

        public List<Sentence> GetSentencesOrderedByWordCount()
        {
            return Sentences.OrderBy(s => s.Words.Count).ToList();
        }

        public List<Sentence> GetSentencesOrderedByLength()
        {
            return Sentences.OrderBy(s => s.ToString().Length).ToList();
        }

        public List<string> FindUniqueWordsInQuestions(int length)
        {
            return Sentences
                .Where(s => s.IsQuestion)
                .SelectMany(s => s.Words)
                .Where(w => w.Length == length)
                .Select(w => w.Value.ToLower())
                .Distinct()
                .ToList();
        }

        public void RemoveWordsStartingWithConsonant(int length)
        {
            foreach (var sentence in Sentences)
            {
                sentence.RemoveWordsStartingWithConsonant(length);
            }
        }

        public void ReplaceWordsInSentence(int sentenceIndex, int length, string replacement)
        {
            if (sentenceIndex >= 0 && sentenceIndex < Sentences.Count)
            {
                Sentences[sentenceIndex].ReplaceWordsByLength(length, replacement);
            }
        }

        public void RemoveStopWords(HashSet<string> stopWords)
        {
            foreach (var sentence in Sentences)
            {
                sentence.RemoveStopWords(stopWords);
            }
        }

        public void PrintText()
        {
            foreach (var sentence in Sentences)
            {
                Console.WriteLine(sentence.ToString());
            }
        }
    }
}
