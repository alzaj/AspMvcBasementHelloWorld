using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasementHelloWorldCommonParts.UI;
using BasementHelloWorldCommonParts.HelloWorldStructures;
using BasementHelloWorldCommonParts.Repositories;


namespace BasementHelloWorldCommonParts.UA_Processors
{
    public class MultipleDialogs_Processor
    {

        public MultipleDialogs_Processor(I_UI_MultipleDialogs view)
        {
            constructMe(view, new RAM_HelloWorldRepository());
        }

        public MultipleDialogs_Processor(I_UI_MultipleDialogs view, I_HelloWorldRepository repo)
        {
            constructMe(view, repo);
        }

        private void constructMe(I_UI_MultipleDialogs view, I_HelloWorldRepository repo)
        {
            _UserView = view;
            _repository = repo;

            initUserView();
        }

        private I_HelloWorldRepository _repository;

        private I_UI_MultipleDialogs _UserView;
        public I_UI_MultipleDialogs UserView
        {
            get { return _UserView; }
        }

        private Dictionary<int, Dialog_Processor> _SubDialogs = new Dictionary<int,Dialog_Processor>();


#region initializations

        private void initUserView()
        {
            if (UserView.subViews_DialogWithUser.Count > 4) { UserView.boolProp_isActionPossible_AddNewDialog = false; }
            else { UserView.boolProp_isActionPossible_AddNewDialog = true; }
            foreach (int dialogID in UserView.subViews_DialogWithUser)
            {
                _SubDialogs.Add(dialogID, new Dialog_Processor(ViewStateManager.getViewFromViewState<Mock_UI_DialogWithUser>(dialogID), _repository));
            }
        }

#endregion //initializations

#region UserActions

        public class Action_AddNewDialog : OpaUserAction
        {
        }

        private void AddNewDialog(Action_AddNewDialog act)
        {
            Mock_UI_DialogWithUser newDlg = ViewStateManager.getViewFromViewState<Mock_UI_DialogWithUser>(0);
            newDlg.boolProp_isActionPossible_SelectLanguage = true;

            UserView.subViews_DialogWithUser.Add(newDlg.viewID);
            ViewStateManager.saveViewToViewState(newDlg);

            _SubDialogs.Add(newDlg.viewID, new Dialog_Processor(newDlg, _repository));

            if (this.UserView.subViews_DialogWithUser.Count > 0)
            {
                this.UserView.boolProp_isActionPossible_RemoveLastDialog = true;
            }
            if (this.UserView.subViews_DialogWithUser.Count > 4)
            {
                this.UserView.boolProp_isActionPossible_AddNewDialog = false;
            }

        }

        public class Action_RemoveLastDialog : OpaUserAction
        {
        }

        private void RemoveLastDialog(Action_RemoveLastDialog act)
        {
            if (UserView.subViews_DialogWithUser.Count > 0)
            {
                ViewStateManager.Delete(UserView.subViews_DialogWithUser[UserView.subViews_DialogWithUser.Count - 1]);
                _SubDialogs.Remove(UserView.subViews_DialogWithUser[UserView.subViews_DialogWithUser.Count - 1]);
                UserView.subViews_DialogWithUser.RemoveAt(UserView.subViews_DialogWithUser.Count - 1);
                if (this.UserView.subViews_DialogWithUser.Count < 1)
                {
                    this.UserView.boolProp_isActionPossible_RemoveLastDialog = false;
                }
                if (this.UserView.subViews_DialogWithUser.Count < 5)
                {
                    this.UserView.boolProp_isActionPossible_AddNewDialog = true;
                }
            }
        }


        public class SubDialogAction_SetSelectedLanguage : OpaUserAction
        {
            public int subDialogID = 0;
            public string newLang = "";
        }

        public class SubDialogAction_ReportUserName : OpaUserAction
        {
            public int subDialogID = 0;
            public string userName = "";
        }

        public class SubDialogAction_AcceptChatAgain : OpaUserAction
        {
            public int subDialogID = 0;
            public bool needChatAgain = false;
        }

        private void SubDialog_SetSelectedLanguage(SubDialogAction_SetSelectedLanguage act)
        {
            if (_SubDialogs.ContainsKey(act.subDialogID))
            {
                _SubDialogs[act.subDialogID].AddUserAction(UserView.viewID, new BasementHelloWorldCommonParts.UA_Processors.Dialog_Processor.Action_SetSelectedLanguage { newLang = act.newLang, hostViewID = UserView.viewID });
                _SubDialogs[act.subDialogID].InvokeUserActions();
            }
        }

        private void SubDialog_ReportUserName(SubDialogAction_ReportUserName act)
        {
            if (_SubDialogs.ContainsKey(act.subDialogID))
            {
                _SubDialogs[act.subDialogID].AddUserAction(UserView.viewID, new BasementHelloWorldCommonParts.UA_Processors.Dialog_Processor.Action_ReportUserName { userName = act.userName, hostViewID = UserView.viewID });
                _SubDialogs[act.subDialogID].InvokeUserActions();
            }
        }

        private void SubDialog_AnswerChatAgainQuestion(SubDialogAction_AcceptChatAgain act)
        {
            if (_SubDialogs.ContainsKey(act.subDialogID))
            {
                _SubDialogs[act.subDialogID].AddUserAction(UserView.viewID, new BasementHelloWorldCommonParts.UA_Processors.Dialog_Processor.Action_AcceptChatAgain { needChatAgain = act.needChatAgain, hostViewID = UserView.viewID });
                _SubDialogs[act.subDialogID].InvokeUserActions();
            }
        }

#endregion //UserActions

#region UserActions help methods

        private UserActionsQuery _userActionsQuery = new UserActionsQuery();

        public void AddUserAction(int hostViewID, OpaUserAction action)
        {
            _userActionsQuery.AddUserAction(hostViewID, action);
        }

        public void InvokeUserAction(OpaUserAction action)
        {
            if (action is Action_AddNewDialog)
            {
                this.AddNewDialog((Action_AddNewDialog)action);
            }
            else if (action is Action_RemoveLastDialog)
            {
                this.RemoveLastDialog((Action_RemoveLastDialog)action);
            }
            else if (action is SubDialogAction_SetSelectedLanguage)
            {
                this.SubDialog_SetSelectedLanguage((SubDialogAction_SetSelectedLanguage)action);
            }
            else if (action is SubDialogAction_ReportUserName)
            {
                this.SubDialog_ReportUserName((SubDialogAction_ReportUserName)action);
            }
            else if (action is SubDialogAction_AcceptChatAgain)
            {
                this.SubDialog_AnswerChatAgainQuestion((SubDialogAction_AcceptChatAgain)action);
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
    }
}
