using System;
using System.Linq;
using System.Xml.Serialization;

namespace TokenizerGame
{
    [XmlInclude(typeof(Word))]
    [XmlInclude(typeof(Punctuation))]
    public abstract class Token
    {
        [XmlText]
        public string Value { get; set; }

        [XmlIgnore]
        public int LineNumber { get; set; }

        protected Token() { }
        protected Token(string value, int lineNumber = 1)
        {
            Value = value;
            LineNumber = lineNumber;
        }

        public override string ToString() => Value;
    }

    public class Word : Token
    {
        public Word() : base() { }
        public Word(string value, int lineNumber = 1) : base(value, lineNumber) { }

        [XmlIgnore]
        public int Length => Value.Length;

        public bool StartsWithConsonant()
        {
            if (string.IsNullOrEmpty(Value)) return false;
            char firstChar = char.ToLower(Value[0]);
            return Constants.Consonants.Contains(firstChar);
        }
    }

    public class Punctuation : Token
    {
        public Punctuation() : base() { }
        public Punctuation(string value, int lineNumber = 1) : base(value, lineNumber) { }
    }
}

