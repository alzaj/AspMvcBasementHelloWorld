using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AspMvcBasementHelloWorld.ViewModels;
using BasementHelloWorldCommonParts.UA_Processors;
using BasementHelloWorldCommonParts.UI;
using BasementHelloWorldCommonParts.HelloWorldStructures;

namespace AspMvcBasementHelloWorld.Controllers
{
    public class HomeController : Controller
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
                catch{}
                return ausgabe;
            }
            set 
            {
                System.Web.HttpContext.Current.Session["_dialogViewIndex"] = value;
            }
        }

        //[HttpGet]
        public ActionResult Index()
        {
            DialogueModel userView;
            try { 
                userView = ViewStateManager.getViewFromViewState<DialogueModel>(_dialogViewIndex); 
            }
            catch (KeyNotFoundException)
            {
                userView = new DialogueModel();
            }
            
            Dialog_UserActions viewProcessor = new Dialog_UserActions(userView);
            _dialogViewIndex = viewProcessor.UserView.viewID;

            //viewProcessor.AddUserAction(new Dialog_UserActions.Action_SetSelectedLanguage { newLang = "ru" });
            //viewProcessor.AddUserAction(new Dialog_UserActions.Action_ReportUserName { userName = "Alex" });
            //viewProcessor.InvokeUserActions();
            viewProcessor.InvokeUserActions(ExtractRequestActions_Index());
            //viewProcessor.InvokeUserAction(new Dialog_UserActions.Action_SetSelectedLanguage {newLang = "de"});

            return View(viewProcessor.UserView);

        }



        //[HttpPost]
        //[ActionName("Index")]
        //public ActionResult Index_Post()
        //{
        //    return View();
        //}

        #region user actions
        private UserActionsQuery ExtractRequestActions_Index()
        {
            UserActionsQuery ausgabe = new UserActionsQuery();

            string languageToSelect = "";
            bool needLanguageToSelect = false;
            string submittedUserName = "";
            bool isUserNameSubmitted = false;

            foreach (string k in Request.Form.Keys)
            {
                string val = Request.Form[k];

                if (k == DialogueModel.SetLanguageButtonName) { needLanguageToSelect = true; }
                else if (k == DialogueModel.languageDropDownName) { languageToSelect = val; }
                else if (k == DialogueModel.reportNameTextBoxName) { submittedUserName = val; }
                else if (k == DialogueModel.reportNameButtonName) { isUserNameSubmitted = true; }
            }

            if (needLanguageToSelect) { ausgabe.AddUserAction(_dialogViewIndex, new Dialog_UserActions.Action_SetSelectedLanguage { newLang = languageToSelect }); }
            if (isUserNameSubmitted) { ausgabe.AddUserAction(_dialogViewIndex, new Dialog_UserActions.Action_ReportUserName { userName = submittedUserName }); }

            return ausgabe;
        }

        #endregion //user actions

    }
}
