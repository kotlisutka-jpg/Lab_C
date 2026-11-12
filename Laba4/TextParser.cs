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

            string[] lines = input.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var currentSentence = new Sentence();

            for (int i = 0; i < lines.Length; i++)
            {
                int lineNumber = i + 1;
                var matches = Regex.Matches(lines[i], Constants.TokenPattern);

                foreach (Match match in matches)
                {
                    string tokenValue = match.Value;

                    if (char.IsLetterOrDigit(tokenValue[0]))
                    {
                        currentSentence.Tokens.Add(new Word(tokenValue, lineNumber));
                    }
                    else
                    {
                        currentSentence.Tokens.Add(new Punctuation(tokenValue, lineNumber));

                        if (Constants.SentenceEndings.Contains(tokenValue))
                        {
                            document.Sentences.Add(currentSentence);
                            currentSentence = new Sentence();
                        }
                    }
                }
            }

            if (currentSentence.Tokens.Count > 0)
            {
                document.Sentences.Add(currentSentence);
            }

            return document;
        }
    }
}

