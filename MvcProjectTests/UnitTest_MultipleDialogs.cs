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
#endregion //Tests

#region TestsSetup
        public MultipleDialogs_Processor Get_MultiDialog_Processor(int viewID)
        {

            return new MultipleDialogs_Processor(ViewStateManager.getViewFromViewState<Mock_UI_MultipleDialogs>(viewID));
        }
#endregion TestsSetup

    }
}
