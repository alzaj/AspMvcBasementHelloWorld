using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasementHelloWorldCommonParts.HelloWorldStructures;

namespace BasementHelloWorldCommonParts.UI
{
    public class Mock_UI_DialogWithUser : OpaView, I_UI_DialogWithUser
    {

        public Mock_UI_DialogWithUser()
        {
            //init available languages
            List<int> tmp = subViews_avaliableLanguages;
        }

        private string _selectedLanguage = "";
        public string strProp_selectedLanguage
        {
            get{return _selectedLanguage;}
            set
            { 
                _selectedLanguage = value;
            }
        }

        private Dictionary<string,string> _avaliableLanguages 
        {get 
        {
            Dictionary<string,string> ausgabe = new Dictionary<string,string>();

            ausgabe.Add("de", "Deutsch");
            ausgabe.Add("en", "English");
            ausgabe.Add("fr", "Français");
            ausgabe.Add("ru", "Русский");

            return ausgabe;
        }
        }

        private List<int> _subViews_available = new List<int>();
        public List<int> subViews_avaliableLanguages
        {
            get 
            {
                List<int> ausgabe = new List<int>();
                ausgabe.AddRange(_subViews_available);

                foreach (KeyValuePair<string,string> lang in _avaliableLanguages)
                {
                    bool isLangAdded = false;
                    foreach (int aID in _subViews_available)
                    {
                        if (ViewStateManager.getViewFromViewState<IdDescriptionPaar>(aID).strProp_shortID == lang.Key)
                        {
                            isLangAdded = true;
                            break;
                        }
                    }
                    if (!isLangAdded)
                    {
                        IdDescriptionPaar newLang = new IdDescriptionPaar { strProp_shortID = lang.Key, strProp_description = lang.Value };
                        _subViews_available.Add(newLang.viewID);
                        ViewStateManager.saveViewToViewState(newLang);
                        ausgabe.Add(newLang.viewID);
                    }
                }
                return ausgabe; 
            }
            set { _subViews_available = value; }
        }

        public string strProp_actionExplanation_SelectLanguage { get { return "Use selected language"; } set { } }

       public bool boolProp_isActionPossible_SelectLanguage
        {
            get
            {
                return true;
            }
            set
            {

            }
        }

        public string strProp_greetingText
        {
            get
            {
                return "ToDo greetingLabelText";
            }
            set
            {

            }
        }

        private bool _greetingVisible = false;
       public bool boolProp_greetingVisible
        {
            get
            {
                return _greetingVisible;
            }
            set
            {
                _greetingVisible = value;
            }
        }


        public string strProp_helloUserMessageText
        {
            get
            {
                return "ToDo helloUserMessageText";
            }
            set
            {

            }
        }

        private bool _helloUserMessageVisible = false;
       public bool boolProp_helloUserMessageVisible
        {
            get
            {
                return _helloUserMessageVisible;
            }
            set
            {
                _helloUserMessageVisible = value;
            }
        }


        private string _userName = "";
        public string strProp_userName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
            }
        }

        public string strProp_actionExplanation_TellUserName { get { return "Report name"; } set { } }

        private bool _isActionPossible_TellUserName = false;
       public bool boolProp_isActionPossible_TellUserName
        {
            get
            {
                return _isActionPossible_TellUserName;
            }
            set
            {
                _isActionPossible_TellUserName = value;
            }
        }

}
}
