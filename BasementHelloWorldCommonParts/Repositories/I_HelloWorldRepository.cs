using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasementHelloWorldCommonParts.Repositories
{
    public interface I_HelloWorldRepository
    {
        string getactionExplanation_SelectLanguage(string langID);
        string getactionExplanation_TellUserName(string langID);

        string getInitialGreetingText(string langID);
        string getHelloUserMessage(string langID, string userName);
    }
}
