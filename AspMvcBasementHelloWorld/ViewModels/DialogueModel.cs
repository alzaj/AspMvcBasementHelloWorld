using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BasementHelloWorldCommonParts.UI;
using BasementHelloWorldCommonParts.HelloWorldStructures;

namespace AspMvcBasementHelloWorld.ViewModels
{
    public class DialogueModel : Mock_UI_DialogWithUser, I_UI_DialogWithUser
    {

        private int _languagesCount = 4;
        public int languagesCount
        {
            get { return _languagesCount; }
            set { _languagesCount = value; }
        }

#region Html controls names

        public static string SetLanguageButtonName
        {
            get { return "SetLanguageButton"; }
        }

        public static string languageDropDownName
        {
            get { return "languagesDDL"; }
        }

        public static string reportNameTextBoxName
        {
            get { return "reportNameTB"; }
        }

        public static string reportNameButtonName
        {
            get { return "reportNameBtn"; }
        }
        
#endregion //Html controls names


        public string GetSelectedAttributeForLanguage(string language)
        {
            string ausgabe = "";
            if (language == strProp_selectedLanguage)
            {
                ausgabe = "selected=\"selected\" ";
            }
            return ausgabe;
        }
    }
}