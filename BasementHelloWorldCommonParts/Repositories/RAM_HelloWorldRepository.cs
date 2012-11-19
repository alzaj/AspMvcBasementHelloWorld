using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasementHelloWorldCommonParts.HelloWorldStructures;

namespace BasementHelloWorldCommonParts.Repositories
{
    public class RAM_HelloWorldRepository : I_HelloWorldRepository
    {
        private Dictionary<LanguageWord,string> _translations = new Dictionary<LanguageWord,string>();

        public string GetWordTranslation(NeededWords word, string langID)
        {
            LanguageWord lw = new LanguageWord(word, langID);
            return _translations[lw];
        }
        public Dictionary<LanguageWord,string> GetAllTranslations()
        {
            return _translations;
        }
        public void AddNewTranslation(NeededWords word, string langID, string translationText)
        {
            LanguageWord lw = new LanguageWord(word,langID);
            _translations.Add(lw, translationText);
        }


        private Dictionary<string,string> _languages = new Dictionary<string,string>();
        public Dictionary<string, string> GetAllLanguages()
        {
            return _languages;
        }
        public void AddNewLanguage(string langID, string nativeText)
        {
            _languages.Add(langID, nativeText);
        }


        public void populateWithTestData()
        {
            foreach (KeyValuePair<string, string> lang in SampleData.defaultLanguages)
            {
                AddNewLanguage(lang.Key, lang.Value);
            }
            foreach (KeyValuePair<LanguageWord, string> transl in SampleData.defaultTranslations())
            {
                AddNewTranslation(transl.Key.word, transl.Key.languageID, transl.Value);
            }
        }
    }
}
