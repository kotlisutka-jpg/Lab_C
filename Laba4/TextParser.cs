using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace TokenizerGame
{
    public static class TextParser
    {
        public static TextDocument Parse(string input)
        {
            var document = new TextDocument();
            if (string.IsNullOrWhiteSpace(input)) return document;

            string sentencePattern =  Constants.SentencePattern;  //Вынести логику констант в одельное, желательно файл и назвать его можно константы
            var rawSentences = Regex.Split(input, sentencePattern).Where(s => !string.IsNullOrWhiteSpace(s));

            foreach (var rawSentence in rawSentences)
            {
                var sentence = new Sentence();

                string tokenPattern = Constants.TokenPattern;   //Вынести логику констант в одельное, желательно файл и назвать его можно константы;
                var matches = Regex.Matches(rawSentence, tokenPattern);

                foreach (Match match in matches)
                {
                    if (char.IsLetterOrDigit(match.Value[0]))
                    {
                        sentence.Tokens.Add(new Word(match.Value));
                    }
                    else
                    {
                        sentence.Tokens.Add(new Punctuation(match.Value));
                    }
                }

                if (sentence.Tokens.Count > 0)
                {
                    document.Sentences.Add(sentence);
                }
            }

            return document;
        }
    }
}
