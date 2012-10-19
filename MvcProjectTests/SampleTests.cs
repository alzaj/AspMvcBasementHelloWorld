using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using Xunit;
using AspMvcBasementHelloWorld;
using AspMvcBasementHelloWorld.Controllers;
using System.Web.Mvc;
using MvcIntegrationTestFramework.Browsing;
using MvcIntegrationTestFramework.Hosting;
using MvcIntegrationTestFramework.Interception;

//This line is unusable. Delete it please
namespace MvcProjectTests
{
    public class SampleTests
    {
        public SampleTests()
        {
            appHost = AppHost.Simulate("AspMvcBasementHelloWorld");
            //If you MVC project is not in the root of your solution directory then include the path
            //e.g. AppHost.Simulate("Website\MyMvcApplication")
        }

        AppHost appHost;

        [Fact]
        public void Index_GET()
        {
            appHost.Start(browsingSession =>
            {

                //Arrange
                string url = ""; //relative to web site root
                string clientIP = "192.168.2.32";

                WorkerRequestSettings rqstSettings = new WorkerRequestSettings(url, WorkerRequestSettings.httpRequestMethods.GET);
                rqstSettings.clientIpAddress = clientIP;

                //Act
                RequestResult result = browsingSession.ProcessRequest(rqstSettings);
                var viewResult = (ViewResult)result.ActionExecutedContext.Result;

                //Assert
                Assert.Equal("Index", viewResult.ViewName);
                Assert.Equal(clientIP, viewResult.ViewData["result"]);
            });
        }

        [Fact]
        public void Index_POST()
        {
            appHost.Start(browsingSession =>
            {
                //Arrange
                string url = ""; //relative to web site root

                WorkerRequestSettings rqstSettings = new WorkerRequestSettings(url, WorkerRequestSettings.httpRequestMethods.POST);
                rqstSettings.formValues = new NameValueCollection { 
                                                                    { "SubmitBtn", "Submit" }, 
                                                                    { "SomeTextBox", "hop-hop" } 
                                                                  };

                //Act
                RequestResult result = browsingSession.ProcessRequest(rqstSettings);
                var viewResult = (ViewResult)result.ActionExecutedContext.Result;

                //Assert
                Assert.Equal("Index", viewResult.ViewName);
                Assert.Equal("Submit", viewResult.ViewData["result"]);
            });
        }
    }
}
