using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BasementHelloWorldCommonParts.UI;
using BasementHelloWorldCommonParts.HelloWorldStructures;

namespace AspMvcBasementHelloWorld.ViewModels
{
    public class SingleDialogueModelHelper : Mock_UI_DialogWithUser, I_UI_DialogWithUser
    {

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

        public static string chatAgainYESButtonName
        {
            get { return "chatAgainYESBtn"; }
        }

        public static string chatAgainNOButtonName
        {
            get { return "chatAgainNOBtn"; }
        }
        
#endregion //Html controls names


        public static string GetSelectedAttributeForLanguage(string language, string currentlySelectedLanguage)
        {
            string ausgabe = "";
            if (language == currentlySelectedLanguage)
            {
                ausgabe = "selected=\"selected\" ";
            }
            return ausgabe;
        }

        public static string AppendHtmlPrefix(string controlName, int currentSubViewID)
        {
            string ausgabe = controlName;
            if (currentSubViewID > 0) 
            {
                ausgabe = ControlNamePrefix + currentSubViewID.ToString().PadLeft(10, '0') + "_" + controlName;
            }
            return ausgabe;
        }

        public static string ControlNamePrefix
        {
            get { return "SubDialogue"; }
        }

        public static string extractRawControlName(string controlNameWithPrefix)
        {
            string ausgabe = "";
            if (controlNameWithPrefix.Length > 22)
            { 
                if (controlNameWithPrefix.StartsWith(ControlNamePrefix))
                {
                    ausgabe = controlNameWithPrefix.Substring(22,controlNameWithPrefix.Length - 22);
                }
            }

            return ausgabe;
        }

        public static int extractSubViewIdFromControlNameWithPrefix(string controlNameWithPrefix)
        {
            int ausgabe = 0;
            if (controlNameWithPrefix.Length > 22)
            {
                if (controlNameWithPrefix.StartsWith("SubDialogue"))
                {
                    try
                    {
                        ausgabe = Convert.ToInt32(controlNameWithPrefix.Substring(11, 10));
                    }
                    catch { }
                }
            }
            return ausgabe;
        }
    }
}