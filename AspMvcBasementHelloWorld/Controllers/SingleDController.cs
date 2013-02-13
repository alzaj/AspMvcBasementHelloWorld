using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AspMvcBasementHelloWorld.ViewModels;
using BasementHelloWorldCommonParts.UA_Processors;
using BasementHelloWorldCommonParts.UI;
using BasementHelloWorldCommonParts.HelloWorldStructures;
using BasementHelloWorldCommonParts.Repositories;

namespace AspMvcBasementHelloWorld.Controllers
{
    public class SingleDController : Controller
    {

        private int _dialogViewIndex
        {
            get
            {
                int ausgabe = 0;
                try
                {
                    ausgabe = (int)System.Web.HttpContext.Current.Session["_dialogViewIndex"];
                }
                catch { }
                return ausgabe;
            }
            set
            {
                System.Web.HttpContext.Current.Session["_dialogViewIndex"] = value;
            }
        }

        //
        // GET: /SingleD/
        public ActionResult Index()
        {
            SingleDialogueModelHelper userView;
            try
            {
                userView = ViewStateManager.getViewFromViewState<SingleDialogueModelHelper>(_dialogViewIndex);
            }
            catch (KeyNotFoundException)
            {
                userView = new SingleDialogueModelHelper();
            }

            RAM_HelloWorldRepository rep = new RAM_HelloWorldRepository();
            rep.populateWithTestData();
            Dialog_Processor viewProcessor = new Dialog_Processor(userView, rep);
            _dialogViewIndex = viewProcessor.UserView.viewID;

            //viewProcessor.AddUserAction(new Dialog_UserActions.Action_SetSelectedLanguage { newLang = "ru" });
            //viewProcessor.AddUserAction(new Dialog_UserActions.Action_ReportUserName { userName = "Alex" });
            //viewProcessor.InvokeUserActions();
            viewProcessor.InvokeUserActions(ExtractRequestActions_Index());
            //viewProcessor.InvokeUserAction(new Dialog_UserActions.Action_SetSelectedLanguage {newLang = "de"});

            return View("Index", viewProcessor.UserView);
        }

        #region user actions
        private UserActionsQuery ExtractRequestActions_Index()
        {
            UserActionsQuery ausgabe = new UserActionsQuery();

            string languageToSelect = "";
            bool needLanguageToSelect = false;
            string submittedUserName = "";
            bool isUserNameSubmitted = false;
            bool isAnsewerChatAgainSubmitted = false;
            bool needChatAgain = false;

            foreach (string k in Request.Form.Keys)
            {
                string val = Request.Form[k];

                if (k == SingleDialogueModelHelper.SetLanguageButtonName) { needLanguageToSelect = true; }
                else if (k == SingleDialogueModelHelper.languageDropDownName) { languageToSelect = val; }
                else if (k == SingleDialogueModelHelper.reportNameTextBoxName) { submittedUserName = val; }
                else if (k == SingleDialogueModelHelper.reportNameButtonName) { isUserNameSubmitted = true; }
                else if (k == SingleDialogueModelHelper.chatAgainNOButtonName) { isAnsewerChatAgainSubmitted = true; }
                else if (k == SingleDialogueModelHelper.chatAgainYESButtonName) { isAnsewerChatAgainSubmitted = true; needChatAgain = true; }
            }

            if (needLanguageToSelect) { ausgabe.AddUserAction(_dialogViewIndex, new Dialog_Processor.Action_SetSelectedLanguage { newLang = languageToSelect }); }
            if (isUserNameSubmitted) { ausgabe.AddUserAction(_dialogViewIndex, new Dialog_Processor.Action_ReportUserName { userName = submittedUserName }); }
            if (isAnsewerChatAgainSubmitted) { ausgabe.AddUserAction(_dialogViewIndex, new Dialog_Processor.Action_AcceptChatAgain { needChatAgain = needChatAgain }); }

            return ausgabe;
        }

        #endregion //user actions

    }
}
