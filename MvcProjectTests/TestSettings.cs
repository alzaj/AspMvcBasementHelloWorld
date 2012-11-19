using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasementHelloWorldCommonParts.Repositories;
using BasementHelloWorldCommonParts.UI;
using BasementHelloWorldCommonParts.HelloWorldStructures;
using AspMvcBasementHelloWorld.ViewModels;

namespace MvcProjectTests
{
    public class TestSettings
    {
        //>>>>>>>>>>>>>>>>>
        //>>>>>>>>>>>>>>>>> CHANGE ONLY FOLLOWING CONSTANTS. rest changes automatically
        //>>>>>>>>>>>>>>>>>
        public static GuiToTest GUI = GuiToTest.MockUI;
        public static DbToTest DB = DbToTest.RAMDataBase;

        public static I_UI_DialogWithUser Get_UI_DialogWithUser(int viewID)
        {
            I_UI_DialogWithUser ausgabe;

            switch (GUI)
            {
                case GuiToTest.MockUI:
                    ausgabe = ViewStateManager.getViewFromViewState<Mock_UI_DialogWithUser>(viewID);
                    break;
                case GuiToTest.AspMvcApplication:
                    ausgabe = ViewStateManager.getViewFromViewState<DialogueModel>(viewID);
                    break;
                default:
                    throw new NotImplementedException();
            }

            return ausgabe;
        }
        
        public static I_HelloWorldRepository Get_HelloWorldRepository(bool needDefaultLanguagesForTesting = false, bool needDefaultTranslationsForTesting = false )
        {
            I_HelloWorldRepository ausgabe;
            switch (DB)
            {
                case DbToTest.RAMDataBase:
                    ausgabe = new RAM_HelloWorldRepository();
                    break;
                default:
                    throw new NotImplementedException();
            }

            if (needDefaultLanguagesForTesting)
            {
                foreach (KeyValuePair<string, string> lang in SampleData.defaultLanguages)
                {
                    ausgabe.AddNewLanguage(lang.Key, lang.Value);
                }

            }

            if (needDefaultTranslationsForTesting)
            {
                foreach (KeyValuePair<LanguageWord, string> transl in SampleData.defaultTranslations())
                {
                    ausgabe.AddNewTranslation(transl.Key.word, transl.Key.languageID, transl.Value);
                }
            }

            return ausgabe;
        }
    }

    public enum GuiToTest {
        AspMvcApplication,
        MockUI
    }

    public enum DbToTest
    {
        RAMDataBase,
        SQLDataBase
    }
}
