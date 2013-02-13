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


    public class MultiDController : Controller
    {

        private int _multiDialogViewIndex
        {
            get
            {
                int ausgabe = 0;
                try
                {
                    ausgabe = (int)System.Web.HttpContext.Current.Session["_multiDialogViewIndex"];
                }
                catch { }
                return ausgabe;
            }
            set
            {
                System.Web.HttpContext.Current.Session["_multiDialogViewIndex"] = value;
            }
        }

        //
        // GET: /MultiD/
        public ActionResult Index()
        {
            MultiDialogueModel userView;
            try
            {
                userView = ViewStateManager.getViewFromViewState<MultiDialogueModel>(_multiDialogViewIndex);
            }
            catch (KeyNotFoundException)
            {
                userView = new MultiDialogueModel();
            }

            RAM_HelloWorldRepository rep = new RAM_HelloWorldRepository();
            rep.populateWithTestData();
            MultipleDialogs_Processor viewProcessor = new MultipleDialogs_Processor(userView, rep);
            _multiDialogViewIndex = viewProcessor.UserView.viewID;

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

            int subViewID = 0;
            bool needAddNewLanguage = false;
            bool needRemoveOldLanguage = false;

            List<int> subDlg_index = new List<int>();
            Dictionary<int, string> subDlg_languageToSelect = new Dictionary<int, string>(); ;
            Dictionary<int, bool> subDlg_needLanguageToSelect = new Dictionary<int, bool>();
            Dictionary<int, string> subDlg_submittedUserName = new Dictionary<int, string>();
            Dictionary<int, bool> subDlg_isUserNameSubmitted = new Dictionary<int, bool>();
            Dictionary<int, bool> subDlg_isAnsewerChatAgainSubmitted = new Dictionary<int, bool>();
            Dictionary<int, bool> subDlg_needChatAgain = new Dictionary<int, bool>();

            foreach (string k in Request.Form.Keys)
            {
                string val = Request.Form[k];

                if (k == MultiDialogueModel.AddNewLanguageButtonName) { needAddNewLanguage = true; }
                else if (k == MultiDialogueModel.RemoveOldLanguageButtonName) { needRemoveOldLanguage = true; }
                else if (k.StartsWith(SingleDialogueModelHelper.ControlNamePrefix)) 
                {
                    subViewID = SingleDialogueModelHelper.extractSubViewIdFromControlNameWithPrefix(k);
                    string subViewControlName = SingleDialogueModelHelper.extractRawControlName(k);

                    subDlg_index.Add(subViewID);
                    if (subViewControlName == SingleDialogueModelHelper.SetLanguageButtonName) { subDlg_needLanguageToSelect.Add(subViewID, true); }
                    else if (subViewControlName == SingleDialogueModelHelper.languageDropDownName) { subDlg_languageToSelect.Add(subViewID, val); }
                    else if (subViewControlName == SingleDialogueModelHelper.reportNameTextBoxName) { subDlg_submittedUserName.Add(subViewID, val); }
                    else if (subViewControlName == SingleDialogueModelHelper.reportNameButtonName) { subDlg_isUserNameSubmitted.Add(subViewID, true); }
                    else if (subViewControlName == SingleDialogueModelHelper.chatAgainNOButtonName) { subDlg_isAnsewerChatAgainSubmitted.Add(subViewID, true); subDlg_needChatAgain.Add(subViewID, false); }
                    else if (subViewControlName == SingleDialogueModelHelper.chatAgainYESButtonName) { subDlg_isAnsewerChatAgainSubmitted.Add(subViewID, true); subDlg_needChatAgain.Add(subViewID, true); }
                }
            }

            if (needAddNewLanguage) { ausgabe.AddUserAction(_multiDialogViewIndex, new MultipleDialogs_Processor.Action_AddNewDialog()); }
            if (needRemoveOldLanguage) { ausgabe.AddUserAction(_multiDialogViewIndex, new MultipleDialogs_Processor.Action_RemoveLastDialog()); }

            for (int i = 0; i < subDlg_index.Count; i++)
            {
                if (subDlg_needLanguageToSelect.ContainsKey(subDlg_index[i]))
                { ausgabe.AddUserAction(_multiDialogViewIndex, new MultipleDialogs_Processor.SubDialogAction_SetSelectedLanguage { newLang = subDlg_languageToSelect[subDlg_index[i]], subDialogID = subDlg_index[i] }); }
                if (subDlg_isUserNameSubmitted.ContainsKey(subDlg_index[i]))
                { ausgabe.AddUserAction(_multiDialogViewIndex, new MultipleDialogs_Processor.SubDialogAction_ReportUserName { userName = subDlg_submittedUserName[subDlg_index[i]], subDialogID = subDlg_index[i] }); }
                if (subDlg_isAnsewerChatAgainSubmitted.ContainsKey(subDlg_index[i]))
                { ausgabe.AddUserAction(_multiDialogViewIndex, new MultipleDialogs_Processor.SubDialogAction_AcceptChatAgain { needChatAgain = subDlg_needChatAgain[subDlg_index[i]], subDialogID = subDlg_index[i] }); }
            }
            return ausgabe;
        }

        #endregion //user actions

    }
}
