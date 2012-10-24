using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasementHelloWorldCommonParts.UI; 

namespace BasementHelloWorldCommonParts.UA_Processors
{
    public class Dialog_UserActions
    {
        public Dialog_UserActions(I_UI_DialogWithUser userView)
        {
            UserView = userView;
        }

        public I_UI_DialogWithUser UserView;

        public bool isValidLanguage(string language)
        {
            bool ausgabe = false;
            foreach (string lang in getAvailableLanguages())
            {
                if (lang == language) 
                {
                    ausgabe = true;
                    break;
                }
            }
            return ausgabe;
        }

        public List<string> getAvailableLanguages()
        {
            //return new string[] { "de", "en", "fr", "ru" };

            List<string> ausgabe = new List<string>();
            foreach (I_IdDescriptionPaar sprache in UserView.avaliableLanguages)
            {
                ausgabe.Add(sprache.shortID);
            }
            return ausgabe;
        }

        public string GetGreetingForLanguage(string language)
        {
            return "ToDo GetGreetingForLanguage";
        }

        public string GetHelloUserMessage()
        {
            return "ToDo GetHelloUserMessage(" + UserView.userName + ")";
        }

        #region UserActions help methods

        public class LocalUserAction
        {
        }

        private List<LocalUserAction> _userActionsQuery = new List<LocalUserAction>();

        public void AddUserAction(LocalUserAction action)
        {
            _userActionsQuery.Add(action);
        }

        public void InvokeUserAction(LocalUserAction action)
        {
            if (action is Action_SetSelectedLanguage)
            {
                this.SetSelectedLanguage((Action_SetSelectedLanguage)action);
            }
            else if (action is Action_ReportUserName)
            {
                this.ReportUserName((Action_ReportUserName)action);
            }
            else
            {
                throw new NotImplementedException("Dialog_UserActions: action \"" + action.GetType().Name + "\" not implemented!");
            }

        }

        public void InvokeUserActions()
        {
            InvokeUserActions(_userActionsQuery);
            _userActionsQuery.Clear();
        }

        public void InvokeUserActions(List<LocalUserAction> actions)
        {
            foreach (LocalUserAction act in actions)
            {
                InvokeUserAction(act);
            }
        }

        #endregion //UserActions help functions

        #region UserActions

        public class Action_SetSelectedLanguage : LocalUserAction
        {
            public string newLang = "";
        }

        public class Action_ReportUserName : LocalUserAction
        {
            public string userName = "";
        }

        private void SetSelectedLanguage(Action_SetSelectedLanguage act)
        {
            if (!isValidLanguage(act.newLang)) act.newLang = "";
            if (!String.IsNullOrEmpty(act.newLang) && !(UserView.selectedLanguage == act.newLang))
            {
                if (String.IsNullOrEmpty(UserView.userName))
                {
                    UserView.greetingText = GetGreetingForLanguage(act.newLang);
                    SetGuiElementsVisiblity(2);
                }
                else
                {
                    SetGuiElementsVisiblity(3);
                }

            }
            else if (String.IsNullOrEmpty(act.newLang))
            {
                UserView.greetingText = "";
                SetGuiElementsVisiblity(1);
            }
            else
            {
            }

            UserView.selectedLanguage = act.newLang;
        }

        private void ReportUserName(Action_ReportUserName act)
        {
            if (!(string.IsNullOrEmpty(UserView.selectedLanguage)) && !(string.IsNullOrEmpty(act.userName.Trim())))
            {
                UserView.userName = act.userName;
                UserView.helloUserMessageText = GetHelloUserMessage();
                SetGuiElementsVisiblity(3);
            }
            else if (string.IsNullOrEmpty(act.userName.Trim()))
            {
                SetGuiElementsVisiblity(2);
            }
            else if (string.IsNullOrEmpty(UserView.selectedLanguage))
            {
                SetGuiElementsVisiblity(1);
            }
        }
        #endregion //UserActions

        private void SetGuiElementsVisiblity(int stateNr)
        {
            switch (stateNr)
            {
                case 1: //application start. no language selected. all elements invisible (except language ddl)
                    UserView.greetingVisible = false;
                    UserView.isActionPossible_TellUserName = false;
                    UserView.helloUserMessageVisible = false;
                    break;
                case 2: //language selected. it's possible to submit the name
                    UserView.greetingVisible = true;
                    UserView.isActionPossible_TellUserName = true;
                    UserView.helloUserMessageVisible = false;
                    break;
                case 3: //language selected, name submitted. appears hello greeting and user can restar/abbort dialogue
                    UserView.greetingVisible = false;
                    UserView.isActionPossible_TellUserName = false;
                    UserView.helloUserMessageVisible = true;
                    break;
                default:
                    throw new ArgumentException("Invalid selection");
                    //break;
            }
        }

    }
}
