﻿using System;
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
    public class SampleIntegrationTests
    {
        public SampleIntegrationTests()
        {
            appHost = AppHost.Simulate(AspMvcBasementHelloWorld.C_StringConstants.C_ApplicationName);
            //If you MVC project is not in the root of your solution directory then include the path
            //e.g. AppHost.Simulate("Website\MyMvcApplication")
        }

        AppHost appHost;

        [Fact]
        public void Index_GET_Deleteme()
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
        public void Index_POST_Deleteme()
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

        [Fact, TestCategory("Unit")]
        public void FastTest()
        {
            Xunit.Assert.True(true);
        }

        [Fact, TestCategory("Unit")]
        public void SlowTest()
        {
            Xunit.Assert.True(false);
        }
    }
}