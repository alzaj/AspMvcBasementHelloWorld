using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasementHelloWorldCommonParts.UI
{
    public interface I_UI_DialogWithUser : I_OpaView
    {
        string strProp_selectedLanguage { get; set; }
        List<int> subViews_avaliableLanguages { get; set; }

        string strProp_actionExplanation_SelectLanguage { get; set; }
        bool boolProp_isActionPossible_SelectLanguage { get; set; }

        string strProp_greetingText { get; set; }
        bool boolProp_greetingVisible { get; set; }

        string strProp_userName { get; set; }

        string strProp_actionExplanation_TellUserName { get; set; }
        bool boolProp_isActionPossible_TellUserName { get; set; }

        string strProp_helloUserMessageText { get; set; }
        bool boolProp_helloUserMessageVisible { get; set; }

    }
}
