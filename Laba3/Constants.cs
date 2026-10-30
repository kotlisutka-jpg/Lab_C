using System;

namespace TokenizerGame
{
    public static class Constants
    {
        public const string Consonants = "bcdfghjklmnpqrstvwxyzбсгджзйклмнпрстфхцчшщ";
 
        public const string SentencePattern = @"(?<=[\.\?!])\s+";

        public const string TokenPattern = @"([a-zA-Zа-яА-ЯёЁ0-9']+(?:-[a-zA-Zа-яА-ЯёЁ0-9']+)*)|([^\s\w])";

        public const string DefaultVocabularyPath = "vocabulary.txt";
        public const string DefaultInputPath = "input.txt";
        public const string DefaultXmlOutputPath = "result_output.xml";

        public const string DefaultReplacement = "[Глоорп]";
    }
}
