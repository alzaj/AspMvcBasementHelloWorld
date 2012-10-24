﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BasementHelloWorldCommonParts.UI;

namespace AspMvcBasementHelloWorld.ViewModels
{
    public class DialogueModel : I_UI_DialogWithUser
    {
#region I_UI_DialogWithUser
        int I_OpaView.viewID
        {
            get { return 1; }
            set { }
        }

        private string _selectedLanguage = "";
        string I_UI_DialogWithUser.selectedLanguage
        {
            get { return _selectedLanguage; }
            set { _selectedLanguage = value; }
        }

        private List<I_IdDescriptionPaar> _avaliableLanguages = new List<I_IdDescriptionPaar> 
        {
                new BasementHelloWorldCommonParts.HelloWorldStructures.IdDescriptionPaar{ shortID="de", description="Deutsch"},
                new BasementHelloWorldCommonParts.HelloWorldStructures.IdDescriptionPaar{ shortID="en", description="English"},
                new BasementHelloWorldCommonParts.HelloWorldStructures.IdDescriptionPaar{ shortID="fr", description="Français"},
                new BasementHelloWorldCommonParts.HelloWorldStructures.IdDescriptionPaar{ shortID="ru", description="Русский"}
        };

        List<I_IdDescriptionPaar> I_UI_DialogWithUser.avaliableLanguages
        {
            get { return _avaliableLanguages; }
            set { _avaliableLanguages = value; }
        }

        string I_UI_DialogWithUser.actionExplanation_SelectLanguage { get { return "Use selected language"; } set { } }

        bool I_UI_DialogWithUser.isActionPossible_SelectLanguage
        {
            get
            {
                return true;
            }
            set
            {

            }
        }

        string I_UI_DialogWithUser.greetingText
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
        bool I_UI_DialogWithUser.greetingVisible
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


        string I_UI_DialogWithUser.helloUserMessageText
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
        bool I_UI_DialogWithUser.helloUserMessageVisible
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
        string I_UI_DialogWithUser.userName
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

        string I_UI_DialogWithUser.actionExplanation_TellUserName { get { return "Report name"; } set { } }

        private bool _isActionPossible_TellUserName = false;
        bool I_UI_DialogWithUser.isActionPossible_TellUserName
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

        #endregion //I_UI_DialogWithUser

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
            if (language == _selectedLanguage)
            {
                ausgabe = "selected=\"selected\" ";
            }
            return ausgabe;
        }
    }
}