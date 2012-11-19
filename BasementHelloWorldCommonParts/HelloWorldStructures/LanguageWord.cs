using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasementHelloWorldCommonParts.Repositories;

namespace BasementHelloWorldCommonParts.HelloWorldStructures
{
    public class LanguageWord : IEquatable<LanguageWord>
    {
        private NeededWords _word;
        public NeededWords word { get { return _word; } }

        private string _languageID;
        public string languageID { get { return _languageID; } }

        public LanguageWord(NeededWords w, string langID)
        {
            if ((int)w > 2000) { throw new ArgumentException("The class supports max 2000 words."); }
            _word = w;

            byte[] asciiBytes = Encoding.ASCII.GetBytes(langID);
            if (asciiBytes.Length != 2) { throw new ArgumentException("LanguageID must be exact two ASCII characters long"); }
            _languageID = langID;
        }

        public bool Equals(LanguageWord other)
        {
            return (_word == other.word) && (_languageID == other.languageID);
        }


        public override int GetHashCode()
        {
            byte[] asciiBytes = Encoding.ASCII.GetBytes(_languageID);
            return asciiBytes[0] + asciiBytes[1] * 1000 + (int)_word * 1000000;
        }

    }

}
