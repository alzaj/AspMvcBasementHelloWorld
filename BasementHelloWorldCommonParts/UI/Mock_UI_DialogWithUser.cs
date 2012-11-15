using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasementHelloWorldCommonParts.HelloWorldStructures;

namespace BasementHelloWorldCommonParts.UI
{
    public class Mock_UI_DialogWithUser : OpaView, I_UI_DialogWithUser
    {

        #region I_UI_DialogWithUser
        public string strProp_selectedLanguage
        { get; set; }

        private List<int> _subViews_available = new List<int>();
        public List<int> subViews_availableLanguages
        {
            get { return _subViews_available; }
            set { _subViews_available = value; }
        }

        public string strProp_actionExplanation_SelectLanguage
        { get; set; }

        public bool boolProp_isActionPossible_SelectLanguage
        { get; set; }

        public string strProp_greetingText
        { get; set; }

        public bool boolProp_greetingVisible
        { get; set; }

        public string strProp_helloUserMessageText
        { get; set; }

        public bool boolProp_helloUserMessageVisible
        { get; set; }

        public string strProp_userName
        { get; set; }

        public string strProp_actionExplanation_TellUserName
        { get; set; }

        public bool boolProp_isActionPossible_TellUserName
        { get; set; }
        #endregion //I_UI_DialogWithUser

        #region overriden functions

        #endregion //overriden functions
    }
}
