using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasementHelloWorldCommonParts.HelloWorldStructures;

namespace BasementHelloWorldCommonParts.Repositories
{
    public class Mock_HelloWorldRepository : I_HelloWorldRepository
    {
        public string GetWordTranslation(NeededWords word, string langID)
        {
            string ausgabe = "";

            switch (word)
            {
                case NeededWords.actionExplanation_SelectLanguage:
                    ausgabe = langID + "_useSelectedLanguage";
                    break;
                case NeededWords.actionExplanation_TellUserName:
                    ausgabe = langID + "_tellUserName";
                    break;
                case NeededWords.initialGreetingText:
                    ausgabe = langID + "_greeting";
                    break;
                case NeededWords.helloUserMessage:
                    ausgabe = "helloUserMessage_" + langID + "_" + Const_String.userNamePlaceholder;
                    break;
                case NeededWords.questionForChatingAgain:
                    ausgabe = "questionForChatingAgain_" + langID;
                    break;
                case NeededWords.yes:
                    ausgabe = "yes_" + langID;
                    break;
                case NeededWords.no:
                    ausgabe = "no_" + langID;
                    break;
                default:
                    throw new NotImplementedException();
            }

            return ausgabe;
        }

        public Dictionary<string, string> GetAllLanguages()
        {
            return SampleData.defaultLanguages;
        }

        public Dictionary<LanguageWord, string> GetAllTranslations()
        {
            return new Dictionary<LanguageWord, string>();
        }

        public void AddNewLanguage(string langID, string nativeText)
        {
            throw new NotImplementedException();
        }

        public void AddNewTranslation(NeededWords word, string langID, string translationText)
        {
            throw new NotImplementedException();
        }
    }
}
