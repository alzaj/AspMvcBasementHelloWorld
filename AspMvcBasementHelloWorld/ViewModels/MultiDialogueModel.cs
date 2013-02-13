using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BasementHelloWorldCommonParts.UI;
using BasementHelloWorldCommonParts.HelloWorldStructures;

namespace AspMvcBasementHelloWorld.ViewModels
{
    public class MultiDialogueModel : Mock_UI_MultipleDialogs, I_UI_MultipleDialogs
    {

#region Html controls names

        public static string AddNewLanguageButtonName
        {
            get { return "AddNewLanguageButtonName"; }
        }

        public static string RemoveOldLanguageButtonName
        {
            get { return "RemoveOldLanguageButtonName"; }
        }

#endregion //Html controls names
    }
}