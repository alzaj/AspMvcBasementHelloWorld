using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasementHelloWorldCommonParts.UI;
using BasementHelloWorldCommonParts.HelloWorldStructures;
using BasementHelloWorldCommonParts.Repositories;

namespace BasementHelloWorldCommonParts.UA_Processors
{
    public class Dialog_UserActions //<T_ViewType> where T_ViewType : OpaView, I_UI_DialogWithUser, new()
    {
        private void constructMe(I_UI_DialogWithUser view, I_HelloWorldRepository repo)
        {
            _UserView = view;
            _repository = repo;
            initUserView();
        }

        public Dialog_UserActions(I_UI_DialogWithUser view, I_HelloWorldRepository repo)
        {
            constructMe(view, repo);
        }

        public Dialog_UserActions(I_UI_DialogWithUser view)
        {
            constructMe(view, new Mock_HelloWorldRepository());
        }

        //public Dialo_UserActions

        private int _userViewID
        {
            get
            {
                if (_UserView == null) { return 0; }
                else { return _UserView.viewID; }
            }
            set
            {
                if (value == 0) { throw new ArgumentException("ViewID cannot be Null"); }
                else
                {
                    _UserView = ViewStateManager.getViewFromViewState<Mock_UI_DialogWithUser>(value);
                }
            }
        }

        private I_UI_DialogWithUser _UserView;

        public I_UI_DialogWithUser UserView
        {
            get { return _UserView; }
        }

        private I_HelloWorldRepository _repository;


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



        private Dictionary<string, string> _defaultAvaliableLanguages
        {
            get
            {
                Dictionary<string, string> ausgabe = new Dictionary<string, string>();

                ausgabe.Add("de", "Deutsch");
                ausgabe.Add("en", "English");
                ausgabe.Add("fr", "Français");
                ausgabe.Add("ru", "Русский");

                return ausgabe;
            }
        }

        public List<string> getAvailableLanguages()
        {
            //return new string[] { "de", "en", "fr", "ru" };

            List<string> ausgabe = new List<string>();
            foreach (int spracheID in UserView.subViews_availableLanguages)
            {
                IdDescriptionPaar tmpSprache = ViewStateManager.getViewFromViewState<IdDescriptionPaar>(spracheID);
                ausgabe.Add(tmpSprache.strProp_shortID);
            }

            return ausgabe;
        }

#region initializations

        private void initUserView()
        {
            UserView.boolProp_isActionPossible_SelectLanguage = true;
            if (String.IsNullOrEmpty(UserView.strProp_actionExplanation_SelectLanguage))
            {
                UserView.strProp_actionExplanation_SelectLanguage = _repository.GetWordTranslation(NeededWords.actionExplanation_SelectLanguage,"en");
            }

            initLanguages();
        }

        private void initLanguages()
        {
            foreach (KeyValuePair<string, string> lang in _defaultAvaliableLanguages)
            {
                bool isLangAdded = false;
                foreach (int aID in UserView.subViews_availableLanguages)
                {
                    if (ViewStateManager.getViewFromViewState<IdDescriptionPaar>(aID).strProp_shortID == lang.Key)
                    {
                        isLangAdded = true;
                        break;
                    }
                }
                if (!isLangAdded)
                {
                    IdDescriptionPaar newLang = ViewStateManager.getViewFromViewState<IdDescriptionPaar>(0);
                    newLang.strProp_shortID = lang.Key;
                    newLang.strProp_description = lang.Value;

                    UserView.subViews_availableLanguages.Add(newLang.viewID);
                    ViewStateManager.saveViewToViewState(newLang);
                }
            }
        }

#endregion //initializations

#region UserActions help methods

        private UserActionsQuery _userActionsQuery = new UserActionsQuery();

        public void AddUserAction(int hostViewID, OpaUserAction action)
        {
            _userActionsQuery.AddUserAction(hostViewID, action);
        }

        public void InvokeUserAction(OpaUserAction action)
        {
            if (action is Action_SetSelectedLanguage)
            {
                this.SetSelectedLanguage((Action_SetSelectedLanguage)action);
            }
            else if (action is Action_ReportUserName)
            {
                this.ReportUserName((Action_ReportUserName)action);
            }
            else if (action is Action_AcceptChatAgain)
            {
                this.AnswerChatAgainQuestion((Action_AcceptChatAgain)action);
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

        public void InvokeUserActions(UserActionsQuery actionsQuery)
        {
            foreach (OpaUserAction act in actionsQuery.actions)
            {
                InvokeUserAction(act);
            }

            ViewStateManager.saveViewToViewState(UserView);
        }

#endregion //UserActions help functions

#region UserActions

        public class Action_SetSelectedLanguage : OpaUserAction
        {
            public string newLang = "";
        }

        public class Action_ReportUserName : OpaUserAction
        {
            public string userName = "";
        }

        public class Action_AcceptChatAgain : OpaUserAction
        {
            public bool needChatAgain = false;
        }

        private void SetSelectedLanguage(Action_SetSelectedLanguage act)
        {
            if (!isValidLanguage(act.newLang))
            {
                act.newLang = "";
            }
            else
            {
                //debug: set last selected attachement
                foreach (int spracheID in UserView.subViews_availableLanguages)
                {
                    IdDescriptionPaar tmpSprache = ViewStateManager.getViewFromViewState<IdDescriptionPaar>(spracheID);
                    if (tmpSprache.strProp_shortID == act.newLang)
                    {
                        string newDescr = tmpSprache.strProp_description.Split(new char[] { '(' })[0];
                        tmpSprache.strProp_description = newDescr + "(last selected at " + DateTime.Now.ToString("HH:mm)");
                        ViewStateManager.saveViewToViewState(tmpSprache);
                    }
                }
            }

            bool isLanguageChanged = !(UserView.strProp_selectedLanguage == act.newLang);
            UserView.strProp_selectedLanguage = act.newLang;
    
            if (!String.IsNullOrEmpty(act.newLang) && isLanguageChanged)
            {
                if (String.IsNullOrEmpty(UserView.strProp_userName))
                {
                    //UserView.strProp_greetingText = GetGreetingForLanguage(act.newLang);
                    SetGuiForStep(2);
                }
                else
                {
                    SetGuiForStep(3);
                }

            }
            else if (String.IsNullOrEmpty(act.newLang))
            {
                UserView.strProp_greetingText = "";
                UserView.strProp_userName = "";
                SetGuiForStep(1);
            }
            else
            {
            }


        }

        private void ReportUserName(Action_ReportUserName act)
        {
            if (!(string.IsNullOrEmpty(UserView.strProp_selectedLanguage)) && !(string.IsNullOrEmpty(act.userName.Trim())))
            {
                UserView.strProp_userName = act.userName;
                //UserView.strProp_helloUserMessageText = GetHelloUserMessage();
                SetGuiForStep(3);
            }
            else if (string.IsNullOrEmpty(act.userName.Trim()))
            {
                SetGuiForStep(2);
            }
            else if (string.IsNullOrEmpty(UserView.strProp_selectedLanguage))
            {
                SetGuiForStep(1);
            }
        }

        private void AnswerChatAgainQuestion(Action_AcceptChatAgain act)
        {
            if (act.needChatAgain)
            {
                UserView.strProp_userName = "";
                SetGuiForStep(2);
            }
            else
            {
                UserView.strProp_selectedLanguage = "";
                UserView.strProp_userName = "";
                SetGuiForStep(1);
            }
        }

#endregion //UserActions

#region setting GUI for steps

        private void SetGuiForStep(int stepNr)
        {
            SetGuiElementsVisiblity(stepNr);
            SetGuiElementsText(stepNr);
        }

        private void SetGuiElementsVisiblity(int stateNr)
        {
            switch (stateNr)
            {
                case 1: //application start. no language selected. all elements invisible (except language ddl)
                    UserView.boolProp_isActionPossible_SelectLanguage = true;
                    UserView.boolProp_greetingVisible = false;
                    UserView.boolProp_isActionPossible_TellUserName = false;
                    UserView.boolProp_helloUserMessageVisible = false;
                    UserView.boolProp_isActionPossible_AnswerChatAgainQuestion = false;
                    break;
                case 2: //language selected. it's possible to submit the name
                    UserView.boolProp_isActionPossible_SelectLanguage = true;
                    UserView.boolProp_greetingVisible = true;
                    UserView.boolProp_isActionPossible_TellUserName = true;
                    UserView.boolProp_helloUserMessageVisible = false;
                    UserView.boolProp_isActionPossible_AnswerChatAgainQuestion = false;
                    break;
                case 3: //language selected, name submitted. appears hello greeting and user can restar/abbort dialogue
                    UserView.boolProp_isActionPossible_SelectLanguage = true;
                    UserView.boolProp_greetingVisible = false;
                    UserView.boolProp_isActionPossible_TellUserName = false;
                    UserView.boolProp_helloUserMessageVisible = true;
                    UserView.boolProp_isActionPossible_AnswerChatAgainQuestion = true;
                    break;
                default:
                    throw new ArgumentException("Invalid selection");
                    //break;
            }
        }

        private void SetGuiElementsText(int stateNr)
        {
            string selLang = UserView.strProp_selectedLanguage;
            switch (stateNr)
            {
                case 1: //application start. no language selected. all elements invisible (except language ddl with default english text
                    UserView.strProp_actionExplanation_SelectLanguage = _repository.GetWordTranslation(NeededWords.actionExplanation_SelectLanguage,"en");
                    break;
                case 2: //language selected. it's possible to submit the name
                    UserView.strProp_actionExplanation_SelectLanguage = _repository.GetWordTranslation(NeededWords.actionExplanation_SelectLanguage,selLang);
                    UserView.strProp_greetingText = _repository.GetWordTranslation(NeededWords.initialGreetingText,selLang);
                    UserView.strProp_actionExplanation_TellUserName = _repository.GetWordTranslation(NeededWords.actionExplanation_TellUserName,selLang);
                    break;
                case 3: //language selected, name submitted. appears hello greeting and user can restar/abbort dialogue
                    UserView.strProp_actionExplanation_SelectLanguage = _repository.GetWordTranslation(NeededWords.actionExplanation_SelectLanguage,selLang);
                    UserView.strProp_helloUserMessageText = placeholderReplaceUserName(_repository.GetWordTranslation(NeededWords.helloUserMessage,selLang), UserView.strProp_userName);

                    UserView.strProp_questionForChatingAgain = _repository.GetWordTranslation(NeededWords.questionForChatingAgain, selLang);
                    UserView.strProp_actionExplanation_DoChatAgain = _repository.GetWordTranslation(NeededWords.yes, selLang);
                    UserView.strProp_actionExplanation_DontChatAgain = _repository.GetWordTranslation(NeededWords.no, selLang);
                    break;
                default:
                    throw new ArgumentException("Invalid selection");
                //break;
            }
        }

#endregion //setting GUI for steps

#region placeholder replacements
        string placeholderReplaceUserName(string stringWithPlaceholder, string userName)
        {
            return stringWithPlaceholder.Replace(Const_String.userNamePlaceholder, userName);
        }
#endregion //placeholder replacements
    }
}
