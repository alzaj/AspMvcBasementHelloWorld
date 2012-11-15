using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasementHelloWorldCommonParts.Repositories
{
    public class Mock_HelloWorldRepository : I_HelloWorldRepository
    {
        public string getactionExplanation_SelectLanguage(string langID) { return langID + "_useSelectedLanguage"; }
        public string getactionExplanation_TellUserName(string langID) { return langID + "_tellUserName"; }

        public string getInitialGreetingText(string langID) { return langID + "_greeting";; }
        public string getHelloUserMessage(string langID, string userName) { return "helloUserMessage_" + langID + "_" + userName; }
    }
}
