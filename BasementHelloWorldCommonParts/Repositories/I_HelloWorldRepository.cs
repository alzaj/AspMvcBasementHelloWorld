using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasementHelloWorldCommonParts.HelloWorldStructures;

namespace BasementHelloWorldCommonParts.Repositories
{
/// <summary>
/// Because words are predefined, we create this enumeration to simplify access
/// </summary>
/// <remarks></remarks>
    public enum NeededWords
    {
        //unique text to id mapping
        actionExplanation_SelectLanguage = 1,
        actionExplanation_TellUserName = 2,
        initialGreetingText = 3,
        helloUserMessage = 4,
        yes = 5,
        no = 6,
        questionForChatingAgain = 7,
    }

    public interface I_HelloWorldRepository
    {
        Dictionary<string, string> GetAllLanguages();
        Dictionary<LanguageWord, string> GetAllTranslations();

        string GetWordTranslation(NeededWords word, string langID);

        void AddNewLanguage(string langID, string nativeText);
        void AddNewTranslation(NeededWords word, string langID, string translationText);
    }
}
