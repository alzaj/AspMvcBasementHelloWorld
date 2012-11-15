using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using Xunit;
using Xunit.Extensions;
using AspMvcBasementHelloWorld;
using AspMvcBasementHelloWorld.Controllers;
using AspMvcBasementHelloWorld.ViewModels;
using System.Web.Mvc;
using MvcIntegrationTestFramework.Browsing;
using MvcIntegrationTestFramework.Hosting;
using MvcIntegrationTestFramework.Interception;
using BasementHelloWorldCommonParts;
using BasementHelloWorldCommonParts.UI;
using BasementHelloWorldCommonParts.UA_Processors;
using BasementHelloWorldCommonParts.HelloWorldStructures;
using BasementHelloWorldCommonParts.Repositories;

namespace MvcProjectTests
{
    [CustomTestClassCommand]
    [TestCategory("Unit"), TestCategory("UnitTest_DialogView")]
    public class UnitTest_DialogView
    {
#region Tests

        [Fact]
        public void SetValidSelectedLanguage()
        { 
            //Arrange
            string newLang = "de";
            Dialog_UserActions processor = Get_Dialog_UserActions(0);
            Assert.True(string.IsNullOrEmpty(processor.UserView.strProp_selectedLanguage));
            Assert.True(processor.UserView.boolProp_isActionPossible_SelectLanguage);
            
            //Act
            processor.InvokeUserAction(new BasementHelloWorldCommonParts.UA_Processors.Dialog_UserActions.Action_SetSelectedLanguage { newLang = newLang });


            //Assert
            Assert.Equal(newLang, processor.UserView.strProp_selectedLanguage);
            Assert.True(processor.UserView.boolProp_isActionPossible_SelectLanguage);
            Assert.Equal("de_useSelectedLanguage", processor.UserView.strProp_actionExplanation_SelectLanguage);
        }

        [Fact]
        public void SetInvalidOrEmptyLanguage()
        {
            //Arrange
            foreach (string s in new string[] {"xx", ""} ){
                string newLang = s;
                Dialog_UserActions processor = Get_Dialog_UserActions(0);
                Assert.True(string.IsNullOrEmpty( processor.UserView.strProp_selectedLanguage));

                //Act
                processor.InvokeUserAction(new BasementHelloWorldCommonParts.UA_Processors.Dialog_UserActions.Action_SetSelectedLanguage { newLang = s });


                //Assert
                Assert.True(string.IsNullOrEmpty(processor.UserView.strProp_selectedLanguage));
                Assert.Equal("en_useSelectedLanguage", processor.UserView.strProp_actionExplanation_SelectLanguage);
                Assert.True(string.IsNullOrEmpty(processor.UserView.strProp_userName));
                //Resetting all dialogue fields
                Assert.True(processor.UserView.boolProp_isActionPossible_SelectLanguage);
                Assert.False(processor.UserView.boolProp_isActionPossible_TellUserName);
                Assert.False(processor.UserView.boolProp_greetingVisible);
            }
        }

        [Fact]
        public void SelectValidLanguage_UserNameUnknown()
        {
            //Arrange
            string newLang = "de";
            Dialog_UserActions processor = Get_Dialog_UserActions(0);
            Assert.True(string.IsNullOrEmpty(processor.UserView.strProp_selectedLanguage));
            Assert.True(string.IsNullOrEmpty(processor.UserView.strProp_userName));

            //Act
            processor.InvokeUserAction(new BasementHelloWorldCommonParts.UA_Processors.Dialog_UserActions.Action_SetSelectedLanguage { newLang = newLang, hostViewID = processor.UserView.viewID });

            //Assert
            Assert.Equal(newLang, processor.UserView.strProp_selectedLanguage);
            Assert.True(processor.UserView.boolProp_greetingVisible);
            Assert.Equal("de_greeting", processor.UserView.strProp_greetingText);
            Assert.True(processor.UserView.boolProp_isActionPossible_TellUserName);
            Assert.Equal("de_tellUserName", processor.UserView.strProp_actionExplanation_TellUserName);
        }

        [Fact]
        public void SubmitUserName_languageAlreadySelected()
        {
            //Arrange
            string newUserName = "Fransois";
            Dialog_UserActions processor = Get_Dialog_UserActions(0);
            Assert.True(string.IsNullOrEmpty(processor.UserView.strProp_selectedLanguage));
            processor.InvokeUserAction(new BasementHelloWorldCommonParts.UA_Processors.Dialog_UserActions.Action_SetSelectedLanguage { newLang = "fr" });
            ViewStateManager.saveViewToViewState(processor.UserView);
            int viewID = processor.UserView.viewID;

            //Act
            processor = Get_Dialog_UserActions(viewID);
            processor.InvokeUserAction(new BasementHelloWorldCommonParts.UA_Processors.Dialog_UserActions.Action_ReportUserName { userName = newUserName });

            //Assert
            Assert.Equal(newUserName, processor.UserView.strProp_userName);
            Assert.False(processor.UserView.boolProp_greetingVisible);
            Assert.False(processor.UserView.boolProp_isActionPossible_TellUserName);
            Assert.True(processor.UserView.boolProp_helloUserMessageVisible);
            Assert.Equal("helloUserMessage_fr_" + newUserName, processor.UserView.strProp_helloUserMessageText);
        }

        [Fact]
        public void SelectValidLanguage_UserNameAlreadySubmitted()
        {
            //Arrange
            string newLang = "de";
            Dialog_UserActions processor = Get_Dialog_UserActions(0);
            processor.InvokeUserAction(new BasementHelloWorldCommonParts.UA_Processors.Dialog_UserActions.Action_SetSelectedLanguage { newLang = "fr" });
            processor.InvokeUserAction(new BasementHelloWorldCommonParts.UA_Processors.Dialog_UserActions.Action_ReportUserName { userName = "Fransois" });
            Assert.Equal("helloUserMessage_fr_Fransois", processor.UserView.strProp_helloUserMessageText);
            ViewStateManager.saveViewToViewState(processor.UserView);
            int viewID = processor.UserView.viewID;

            //Act
            processor = Get_Dialog_UserActions(viewID);
            processor.InvokeUserAction(new BasementHelloWorldCommonParts.UA_Processors.Dialog_UserActions.Action_SetSelectedLanguage { newLang = newLang });

            //Assert
            Assert.Equal(newLang, processor.UserView.strProp_selectedLanguage);
            Assert.Equal("helloUserMessage_de_Fransois", processor.UserView.strProp_helloUserMessageText);
            Assert.False(processor.UserView.boolProp_greetingVisible);
            Assert.False(processor.UserView.boolProp_isActionPossible_TellUserName);
        }

        [Fact]
        public void SelectEmptyLanguage_UserNameAlreadySubmitted()
        {
            //Arrange
            string newLang = "";
            Dialog_UserActions processor = Get_Dialog_UserActions(0);
            processor.InvokeUserAction(new BasementHelloWorldCommonParts.UA_Processors.Dialog_UserActions.Action_SetSelectedLanguage { newLang = "fr" });
            processor.InvokeUserAction(new BasementHelloWorldCommonParts.UA_Processors.Dialog_UserActions.Action_ReportUserName { userName = "Fransois" });
            ViewStateManager.saveViewToViewState(processor.UserView);
            int viewID = processor.UserView.viewID;

            //Act
            processor = Get_Dialog_UserActions(viewID);
            processor.InvokeUserAction(new BasementHelloWorldCommonParts.UA_Processors.Dialog_UserActions.Action_SetSelectedLanguage { newLang = newLang });

            //Assert
            Assert.Equal("", processor.UserView.strProp_selectedLanguage);
            Assert.Equal("", processor.UserView.strProp_userName);
            //Resetting all dialogue fields
            Assert.False(processor.UserView.boolProp_isActionPossible_TellUserName);
            Assert.False(processor.UserView.boolProp_greetingVisible);
        }
        
#endregion //Tests

        #region TestsSetup
        public I_UI_DialogWithUser GetI_UI_DialogWithUser()
        {
           return new Mock_UI_DialogWithUser();
        }
 
        public Dialog_UserActions Get_Dialog_UserActions(int viewID)
        {
           return new Dialog_UserActions(ViewStateManager.getViewFromViewState<DialogueModel>(viewID), new Mock_HelloWorldRepository());
        }
        #endregion TestsSetup
    }
}
