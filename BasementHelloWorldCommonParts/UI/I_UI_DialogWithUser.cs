using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasementHelloWorldCommonParts.UI
{
    public interface I_UI_DialogWithUser : I_OpaView
    {
        string selectedLanguage { get; set; }

        List<I_IdDescriptionPaar> avaliableLanguages { get; set; }

        string actionExplanation_SelectLanguage { get; set; }
        bool isActionPossible_SelectLanguage { get; set; }

        string greetingText { get; set; }
        bool greetingVisible { get; set; }

        string userName { get; set; }

        string actionExplanation_TellUserName { get; set; }
        bool isActionPossible_TellUserName { get; set; }

        string helloUserMessageText { get; set; }
        bool helloUserMessageVisible { get; set; }

    }
}
