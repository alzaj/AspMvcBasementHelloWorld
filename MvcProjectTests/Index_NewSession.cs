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

namespace MvcProjectTests
{
    [CustomTestClassCommand]
    public class Index_NewSession
    {
        public Index_NewSession()
        {
            appHost = AppHost.Simulate(AspMvcBasementHelloWorld.C_StringConstants.C_ApplicationName);
            //If you MVC project is not in the root of your solution directory then include the path
            //e.g. AppHost.Simulate("Website\MyMvcApplication")
        }

        AppHost appHost;

        [Fact, TestCategory("Integration")]
        public void Index_GET()
        {
            appHost.Start(browsingSession =>
            {

                //Arrange
                string url = ""; //relative to web site root

                WorkerRequestSettings rqstSettings = new WorkerRequestSettings(url, WorkerRequestSettings.httpRequestMethods.GET);

                //Act
                RequestResult result = browsingSession.ProcessRequest(rqstSettings);
                var viewResult = (ViewResult)result.ActionExecutedContext.Result;

                //Assert
                Xunit.Assert.Equal("Index", viewResult.ViewName);
            });
        }

        [Fact, TestCategory("Integration")]
        public void Index_ModelLanguagesCount()
        {
            appHost.Start(browsingSession =>
            {

                //Arrange
                string url = ""; //relative to web site root

                WorkerRequestSettings rqstSettings = new WorkerRequestSettings(url, WorkerRequestSettings.httpRequestMethods.GET);

                //Act
                RequestResult result = browsingSession.ProcessRequest(rqstSettings);
                var viewResult = (ViewResult)result.ActionExecutedContext.Result;

                //Assert
                Xunit.Assert.Equal(4, ((DialogueModel)viewResult.Model).languagesCount);
            });
        }

        [Fact]
        public void ThisMethodHasNoCategory()
        {
            Xunit.Assert.True(false);
        }
    }
}
