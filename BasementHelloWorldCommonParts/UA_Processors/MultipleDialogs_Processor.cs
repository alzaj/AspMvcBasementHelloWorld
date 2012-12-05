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
            constructMe(view);
        }
        private void constructMe(I_UI_MultipleDialogs view)
        {
            _UserView = view;
            initUserView();
        }

        private I_UI_MultipleDialogs _UserView;
        public I_UI_MultipleDialogs UserView
        {
            get { return _UserView; }
        }

#region initializations

        private void initUserView()
        {

        }

#endregion //initializations

#region UserActions

        public class Action_AddNewDialog : OpaUserAction
        {
        }

        private void AddNewDialog(Action_AddNewDialog act)
        {
            
        }

        public class Action_RemoveLastDialog : OpaUserAction
        {
        }

        private void RemoveLastDialog(Action_RemoveLastDialog act)
        {
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
