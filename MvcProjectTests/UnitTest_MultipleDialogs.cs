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
    [TestCategory("Unit"), TestCategory("UnitTest_MultipleDialogs")]
    public class UnitTest_MultipleDialogs
    {

#region Tests
        [Fact]
        public void AddNewDialog()
        {
            //Arrange
            MultipleDialogs_Processor processor = Get_MultiDialog_Processor(0);
            Assert.True(processor.UserView.subViews_DialogWithUser.Count == 0);

            //Act
            processor.InvokeUserAction(new BasementHelloWorldCommonParts.UA_Processors.MultipleDialogs_Processor.Action_AddNewDialog());

            //Assert
            Assert.Equal(1, processor.UserView.subViews_DialogWithUser.Count);
        }

        [Fact]
        public void RemoveLastDialog()
        {
            //Arrange
            MultipleDialogs_Processor processor = Get_MultiDialog_Processor(0);
            Assert.True(processor.UserView.subViews_DialogWithUser.Count == 0);
            processor.InvokeUserAction(new BasementHelloWorldCommonParts.UA_Processors.MultipleDialogs_Processor.Action_AddNewDialog());
            processor.InvokeUserAction(new BasementHelloWorldCommonParts.UA_Processors.MultipleDialogs_Processor.Action_AddNewDialog());
            processor.InvokeUserAction(new BasementHelloWorldCommonParts.UA_Processors.MultipleDialogs_Processor.Action_AddNewDialog());
            Assert.Equal(3, processor.UserView.subViews_DialogWithUser.Count);

            //Act
            processor.InvokeUserAction(new BasementHelloWorldCommonParts.UA_Processors.MultipleDialogs_Processor.Action_RemoveLastDialog());

            //Assert
            Assert.Equal(2, processor.UserView.subViews_DialogWithUser.Count);
        }

        [Fact]
        public void ChangeSubViews()
        {
            //Arange
            MultipleDialogs_Processor processor = Get_MultiDialog_Processor(0);
            processor.AddUserAction(processor.UserView.viewID,new BasementHelloWorldCommonParts.UA_Processors.MultipleDialogs_Processor.Action_AddNewDialog());
            processor.AddUserAction(processor.UserView.viewID, new BasementHelloWorldCommonParts.UA_Processors.MultipleDialogs_Processor.Action_AddNewDialog());
            processor.AddUserAction(processor.UserView.viewID, new BasementHelloWorldCommonParts.UA_Processors.MultipleDialogs_Processor.Action_AddNewDialog());
            processor.InvokeUserActions();
            Assert.Equal(3, processor.UserView.subViews_DialogWithUser.Count);
            int viewID = processor.UserView.viewID;
            int d1ID = processor.UserView.subViews_DialogWithUser[0];
            int d2ID = processor.UserView.subViews_DialogWithUser[1];
            int d3ID = processor.UserView.subViews_DialogWithUser[2];

            //Act
            processor.AddUserAction(processor.UserView.viewID, new BasementHelloWorldCommonParts.UA_Processors.MultipleDialogs_Processor.SubDialogAction_SetSelectedLanguage { newLang = "fr", subDialogID = d1ID });
            processor.AddUserAction(processor.UserView.viewID, new BasementHelloWorldCommonParts.UA_Processors.MultipleDialogs_Processor.SubDialogAction_SetSelectedLanguage { newLang = "en", subDialogID = d2ID });
            processor.AddUserAction(processor.UserView.viewID, new BasementHelloWorldCommonParts.UA_Processors.MultipleDialogs_Processor.SubDialogAction_SetSelectedLanguage { newLang = "ru", subDialogID = d3ID });
            processor.InvokeUserActions();
            
            ViewStateManager.saveViewToViewState(processor.UserView);

            //Assert
            Assert.Equal("fr", TestSettings.Get_UI_DialogWithUser(d1ID).strProp_selectedLanguage);
            Assert.Equal("en", TestSettings.Get_UI_DialogWithUser(d2ID).strProp_selectedLanguage);
            Assert.Equal("ru", TestSettings.Get_UI_DialogWithUser(d3ID).strProp_selectedLanguage);
        }

        [Fact]
        public void SubDialog_SetValidSelectedLanguage()
        {
            //Arange
            string newLang = "de";
            MultipleDialogs_Processor processor = Get_MultiDialog_Processor(0);
            processor.AddUserAction(processor.UserView.viewID, new BasementHelloWorldCommonParts.UA_Processors.MultipleDialogs_Processor.Action_AddNewDialog());
            processor.AddUserAction(processor.UserView.viewID, new BasementHelloWorldCommonParts.UA_Processors.MultipleDialogs_Processor.Action_AddNewDialog());
            processor.AddUserAction(processor.UserView.viewID, new BasementHelloWorldCommonParts.UA_Processors.MultipleDialogs_Processor.Action_AddNewDialog());
            processor.InvokeUserActions();
            Assert.Equal(3, processor.UserView.subViews_DialogWithUser.Count);
            int viewID = processor.UserView.viewID;
            int d2ID = processor.UserView.subViews_DialogWithUser[1];

            //Act
            processor.AddUserAction(processor.UserView.viewID, new BasementHelloWorldCommonParts.UA_Processors.MultipleDialogs_Processor.SubDialogAction_SetSelectedLanguage { newLang = newLang, subDialogID = d2ID });
            processor.InvokeUserActions();

            //Assert
            Assert.Equal(newLang, TestSettings.Get_UI_DialogWithUser(d2ID).strProp_selectedLanguage);
            Assert.True(TestSettings.Get_UI_DialogWithUser(d2ID).boolProp_isActionPossible_SelectLanguage);
            Assert.Equal("de_useSelectedLanguage", TestSettings.Get_UI_DialogWithUser(d2ID).strProp_actionExplanation_SelectLanguage);
        }

        [Fact]
        public void SubDialog_SubmitUserName_languageAlreadySelected()
        {
            //Arrange
            string newUserName = "Fransois";
            MultipleDialogs_Processor processor = Get_MultiDialog_Processor(0);
            processor.AddUserAction(processor.UserView.viewID, new BasementHelloWorldCommonParts.UA_Processors.MultipleDialogs_Processor.Action_AddNewDialog());
            processor.AddUserAction(processor.UserView.viewID, new BasementHelloWorldCommonParts.UA_Processors.MultipleDialogs_Processor.Action_AddNewDialog());
            processor.AddUserAction(processor.UserView.viewID, new BasementHelloWorldCommonParts.UA_Processors.MultipleDialogs_Processor.Action_AddNewDialog());
            processor.InvokeUserActions();
            Assert.Equal(3, processor.UserView.subViews_DialogWithUser.Count);
            int viewID = processor.UserView.viewID;
            int d2ID = processor.UserView.subViews_DialogWithUser[1];

            processor.AddUserAction(processor.UserView.viewID, new BasementHelloWorldCommonParts.UA_Processors.MultipleDialogs_Processor.SubDialogAction_SetSelectedLanguage { newLang = "fr", subDialogID = d2ID });
            processor.InvokeUserActions();

            ViewStateManager.saveViewToViewState(processor.UserView);

            //Act
            processor = Get_MultiDialog_Processor(viewID);
            processor.AddUserAction(viewID, new BasementHelloWorldCommonParts.UA_Processors.MultipleDialogs_Processor.SubDialogAction_ReportUserName { userName = newUserName, subDialogID = d2ID });
            processor.InvokeUserActions();


            I_UI_DialogWithUser editedSubDlg = TestSettings.Get_UI_DialogWithUser(d2ID);
            //Assert
            Assert.Equal(newUserName, editedSubDlg.strProp_userName);
            Assert.False(editedSubDlg.boolProp_greetingVisible);
            Assert.False(editedSubDlg.boolProp_isActionPossible_TellUserName);
            Assert.True(editedSubDlg.boolProp_helloUserMessageVisible);
            Assert.Equal("helloUserMessage_fr_" + newUserName, editedSubDlg.strProp_helloUserMessageText);
            Assert.True(editedSubDlg.boolProp_isActionPossible_AnswerChatAgainQuestion);
            Assert.Equal("questionForChatingAgain_fr", editedSubDlg.strProp_questionForChatingAgain);
            Assert.Equal("yes_fr", editedSubDlg.strProp_actionExplanation_DoChatAgain);
            Assert.Equal("no_fr", editedSubDlg.strProp_actionExplanation_DontChatAgain);
        }

        [Fact]
        public void AnswerChatAgainQuestion_YES()
        {
            //Arrange
            MultipleDialogs_Processor processor = Get_MultiDialog_Processor(0);
            processor.AddUserAction(processor.UserView.viewID, new BasementHelloWorldCommonParts.UA_Processors.MultipleDialogs_Processor.Action_AddNewDialog());
            processor.AddUserAction(processor.UserView.viewID, new BasementHelloWorldCommonParts.UA_Processors.MultipleDialogs_Processor.Action_AddNewDialog());
            processor.AddUserAction(processor.UserView.viewID, new BasementHelloWorldCommonParts.UA_Processors.MultipleDialogs_Processor.Action_AddNewDialog());
            processor.InvokeUserActions();
            Assert.Equal(3, processor.UserView.subViews_DialogWithUser.Count);
            int viewID = processor.UserView.viewID;
            int d2ID = processor.UserView.subViews_DialogWithUser[1];

            processor.AddUserAction(processor.UserView.viewID, new BasementHelloWorldCommonParts.UA_Processors.MultipleDialogs_Processor.SubDialogAction_SetSelectedLanguage { newLang = "de", subDialogID = d2ID });
            processor.InvokeUserActions();
            ViewStateManager.saveViewToViewState(processor.UserView);

            processor = Get_MultiDialog_Processor(viewID);

            processor.AddUserAction(processor.UserView.viewID, new BasementHelloWorldCommonParts.UA_Processors.MultipleDialogs_Processor.SubDialogAction_ReportUserName { userName = "Fransois", subDialogID = d2ID });
            processor.InvokeUserActions();

            ViewStateManager.saveViewToViewState(processor.UserView);

            processor = Get_MultiDialog_Processor(viewID);

            //Act
            processor.AddUserAction(processor.UserView.viewID, new BasementHelloWorldCommonParts.UA_Processors.MultipleDialogs_Processor.SubDialogAction_AcceptChatAgain { needChatAgain = true, subDialogID = d2ID });
            processor.InvokeUserActions();

            I_UI_DialogWithUser editedSubDlg = TestSettings.Get_UI_DialogWithUser(d2ID);
            //Assert
            Assert.Equal("de", editedSubDlg.strProp_selectedLanguage);
            Assert.True(editedSubDlg.boolProp_greetingVisible);
            Assert.Equal("de_greeting", editedSubDlg.strProp_greetingText);
            Assert.True(editedSubDlg.boolProp_isActionPossible_TellUserName);
            Assert.Equal("de_tellUserName", editedSubDlg.strProp_actionExplanation_TellUserName);
            Assert.False(editedSubDlg.boolProp_isActionPossible_AnswerChatAgainQuestion);
        }

        [Fact]
        public void AnswerChatAgainQuestion_NO()
        {
            //Arrange
            MultipleDialogs_Processor processor = Get_MultiDialog_Processor(0);
            processor.AddUserAction(processor.UserView.viewID, new BasementHelloWorldCommonParts.UA_Processors.MultipleDialogs_Processor.Action_AddNewDialog());
            processor.AddUserAction(processor.UserView.viewID, new BasementHelloWorldCommonParts.UA_Processors.MultipleDialogs_Processor.Action_AddNewDialog());
            processor.AddUserAction(processor.UserView.viewID, new BasementHelloWorldCommonParts.UA_Processors.MultipleDialogs_Processor.Action_AddNewDialog());
            processor.InvokeUserActions();
            Assert.Equal(3, processor.UserView.subViews_DialogWithUser.Count);
            int viewID = processor.UserView.viewID;
            int d2ID = processor.UserView.subViews_DialogWithUser[1];

            processor.AddUserAction(processor.UserView.viewID, new BasementHelloWorldCommonParts.UA_Processors.MultipleDialogs_Processor.SubDialogAction_SetSelectedLanguage { newLang = "de", subDialogID = d2ID });
            processor.InvokeUserActions();
            ViewStateManager.saveViewToViewState(processor.UserView);

            processor = Get_MultiDialog_Processor(viewID);

            processor.AddUserAction(processor.UserView.viewID, new BasementHelloWorldCommonParts.UA_Processors.MultipleDialogs_Processor.SubDialogAction_ReportUserName { userName = "Fransois", subDialogID = d2ID });
            processor.InvokeUserActions();

            ViewStateManager.saveViewToViewState(processor.UserView);

            processor = Get_MultiDialog_Processor(viewID);

            //Act
            processor.AddUserAction(processor.UserView.viewID, new BasementHelloWorldCommonParts.UA_Processors.MultipleDialogs_Processor.SubDialogAction_AcceptChatAgain { needChatAgain = false, subDialogID = d2ID });
            processor.InvokeUserActions();

            I_UI_DialogWithUser editedSubDlg = TestSettings.Get_UI_DialogWithUser(d2ID);
            //Assert
            Assert.Equal("", editedSubDlg.strProp_selectedLanguage);
            Assert.Equal("", editedSubDlg.strProp_userName);
            //Resetting all dialogue fields
            Assert.False(editedSubDlg.boolProp_isActionPossible_TellUserName);
            Assert.False(editedSubDlg.boolProp_greetingVisible);
            Assert.False(editedSubDlg.boolProp_isActionPossible_AnswerChatAgainQuestion);
        }
#endregion //Tests

#region TestsSetup
        public MultipleDialogs_Processor Get_MultiDialog_Processor(int viewID)
        {
            return new MultipleDialogs_Processor(ViewStateManager.getViewFromViewState<Mock_UI_MultipleDialogs>(viewID), new Mock_HelloWorldRepository());
        }
#endregion TestsSetup

    }
}
