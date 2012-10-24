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
            Dialog_UserActions processor = Get_Dialog_UserActions();
            processor.UserView.selectedLanguage = "";
            
            //Act
            processor.InvokeUserAction(new BasementHelloWorldCommonParts.UA_Processors.Dialog_UserActions.Action_SetSelectedLanguage { newLang = newLang });


            //Assert
            Assert.Equal(newLang, processor.UserView.selectedLanguage);
        }

        [Fact]
        public void SetInvalidOrEmptyLanguage()
        {
            //Arrange
            foreach (string s in new string[] {"xx", ""} ){
                string newLang = s;
                Dialog_UserActions processor = Get_Dialog_UserActions();
                processor.UserView.selectedLanguage = "";

                //Act
                processor.InvokeUserAction(new BasementHelloWorldCommonParts.UA_Processors.Dialog_UserActions.Action_SetSelectedLanguage { newLang = s });


                //Assert
                Assert.Equal("", processor.UserView.selectedLanguage);
                //Resetting all dialogue fields
                Assert.False(processor.UserView.isActionPossible_TellUserName);
                Assert.False(processor.UserView.greetingVisible);
            }
        }
        #endregion //Tests

        [Fact]
        public void SelectValidLanguage_UserNameUnknown()
        {
            //Arrange
            string newLang = "de";
            Dialog_UserActions processor = Get_Dialog_UserActions();
            processor.UserView.selectedLanguage = "";
            processor.UserView.userName = "";

            //Act
            processor.InvokeUserAction(new BasementHelloWorldCommonParts.UA_Processors.Dialog_UserActions.Action_SetSelectedLanguage { newLang = newLang });

            //Assert
            Assert.Equal(newLang, processor.UserView.selectedLanguage);
            Assert.True(processor.UserView.greetingVisible);
            Assert.True(processor.UserView.isActionPossible_TellUserName);
        }

        [Fact]
        public void SubmitUserName_languageAlreadySelected()
        {
            //Arrange
            string newUserName = "Fransois";
            Dialog_UserActions processor = Get_Dialog_UserActions();
            processor.UserView.selectedLanguage = "fr";

            //Act
            processor.InvokeUserAction(new BasementHelloWorldCommonParts.UA_Processors.Dialog_UserActions.Action_ReportUserName { userName = newUserName });

            //Assert
            Assert.Equal(newUserName, processor.UserView.userName);
            Assert.False(processor.UserView.greetingVisible);
            Assert.False(processor.UserView.isActionPossible_TellUserName);
            Assert.True(processor.UserView.helloUserMessageVisible);
        }

        [Fact]
        public void SelectValidLanguage_UserNameAlreadySubmitted()
        {
            //Arrange
            string newLang = "de";
            Dialog_UserActions processor = Get_Dialog_UserActions();
            processor.UserView.selectedLanguage = "fr";
            processor.UserView.userName = "Fransois";

            //Act
            processor.InvokeUserAction(new BasementHelloWorldCommonParts.UA_Processors.Dialog_UserActions.Action_SetSelectedLanguage { newLang = newLang });

            //Assert
            Assert.Equal(newLang, processor.UserView.selectedLanguage);
            Assert.False(processor.UserView.greetingVisible);
            Assert.False(processor.UserView.isActionPossible_TellUserName);
        }

        #region TestsSetup
        public I_UI_DialogWithUser GetI_UI_DialogWithUser()
        {
            if (TestSettings.GUI == GuiToTest.MockUI)
            {
                return new Mock_UI_DialogWithUser();
            }
            else if (TestSettings.GUI == GuiToTest.AspMvcApplication)
            {
                return new DialogueModel();
            }
            else { throw new NotImplementedException("Not implemented setup for " + Enum.GetName(typeof(GuiToTest), TestSettings.GUI)); }
        }
 
        public Dialog_UserActions Get_Dialog_UserActions()
        {
            if (TestSettings.GUI == GuiToTest.AspMvcApplication) 
            {
                return new Dialog_UserActions(new DialogueModel());
            }
            else if (TestSettings.GUI == GuiToTest.MockUI)
            {
                return new Dialog_UserActions(new Mock_UI_DialogWithUser());
            }
            else
            {
                throw new NotImplementedException();
            }
        }
        #endregion TestsSetup
    }
}
