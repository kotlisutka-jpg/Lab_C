using System;
using System.Text;

namespace GeneticSearch.Services
{
    public static class RleCodec
    { 
        public static string Decode(string aminoAcids)
        {
            if (string.IsNullOrEmpty(aminoAcids)) return string.Empty;

            StringBuilder decoded = new StringBuilder();
            for (int i = 0; i < aminoAcids.Length; i++)
            {
                char ch = aminoAcids[i];
                if (char.IsDigit(ch))
                {
                    int count = ch - '0';
                    char letter = aminoAcids[i + 1];

                    for (int j = 0; j < count; j++)
                    {
                        decoded.Append(letter);
                    }
                    i++;
                }
                else
                {
                    decoded.Append(ch);
                }
            }
            return decoded.ToString();
        }
 
        public static string Encode(string aminoAcids)
        {
            if (string.IsNullOrEmpty(aminoAcids)) return string.Empty;

            StringBuilder encoded = new StringBuilder();
            for (int i = 0; i < aminoAcids.Length; i++)
            {
                char ch = aminoAcids[i];
                int count = 1;
                while (i < aminoAcids.Length - 1 && aminoAcids[i + 1] == ch)
                {
                    count++;
                    i++;
                }
                if (count > 2) encoded.Append(count).Append(ch);
                if (count == 1) encoded.Append(ch);
                if (count == 2) encoded.Append(ch).Append(ch);
            }
            return encoded.ToString();
        }
    }
}
