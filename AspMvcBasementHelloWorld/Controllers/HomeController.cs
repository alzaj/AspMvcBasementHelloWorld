using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AspMvcBasementHelloWorld.ViewModels;
using BasementHelloWorldCommonParts.UA_Processors;
using BasementHelloWorldCommonParts.UI;

namespace AspMvcBasementHelloWorld.Controllers
{
    public class HomeController : Controller
    {
        //[HttpGet]
        public ActionResult Index()
        {
            Dialog_UserActions viewProcessor = new Dialog_UserActions(new DialogueModel());

            //viewProcessor.AddUserAction(new Dialog_UserActions.Action_SetSelectedLanguage { newLang = "ru" });
            //viewProcessor.AddUserAction(new Dialog_UserActions.Action_ReportUserName { userName = "Alex" });

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
        private List<Dialog_UserActions.LocalUserAction> ExtractRequestActions_Index()
        {
            List<Dialog_UserActions.LocalUserAction> ausgabe = new List<Dialog_UserActions.LocalUserAction>();

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
                else if (k == DialogueModel.SetLanguageButtonName) { isUserNameSubmitted = true; }
            }

            if (needLanguageToSelect){ ausgabe.Add(new Dialog_UserActions.Action_SetSelectedLanguage { newLang = languageToSelect }); }
            if (isUserNameSubmitted){ ausgabe.Add(new Dialog_UserActions.Action_ReportUserName { userName = submittedUserName }); }

            return ausgabe;
        }

        #endregion //user actions

    }
}
