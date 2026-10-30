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

        protected Token() { }
        protected Token(string value) { Value = value; }

        public override string ToString() => Value;
    }

    public class Word : Token
    {
        public Word() : base() { }
        public Word(string value) : base(value) { }

        [XmlIgnore]
        public int Length => Value.Length;

        public bool StartsWithConsonant()
        {
            if (string.IsNullOrEmpty(Value)) return false;
            char firstChar = char.ToLower(Value[0]);
            
            string consonants = "bcdfghjklmnpqrstvwxyzбсгджзйклмнпрстфхцчшщ";      // Вынести логику констант в одельное, желательно файл и назвать его можно константы
            return consonants.Contains(firstChar);                                 // чтобы можно было их вызывать , а не прописывать
        }
    }

    public class Punctuation : Token
    {
        public Punctuation() : base() { }
        public Punctuation(string value) : base(value) { }
    }
}
