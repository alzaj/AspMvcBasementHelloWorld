using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasementHelloWorldCommonParts.HelloWorldStructures;

namespace BasementHelloWorldCommonParts.Repositories
{
    public class RAM_HelloWorldRepository : I_HelloWorldRepository
    {
        public string GetWordTranslation(NeededWords word, string langID)
        {
            return "ToDo RAM_HelloWorldRepository.GetWordTranslation";
        }

        public Dictionary<string, string> GetAllLanguages()
        {
            return new Dictionary<string,string>();
        }

        public Dictionary<LanguageWord,string> GetAllTranslations()
        {
            return new Dictionary<LanguageWord, string>();
        }

    }
}
