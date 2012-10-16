using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using AspMvcBasementHelloWorld;
using AspMvcBasementHelloWorld.Controllers;
using System.Web.Mvc;
using MvcIntegrationTestFramework.Browsing;
using MvcIntegrationTestFramework.Hosting;
using MvcIntegrationTestFramework.Interception;

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

                string url = ""; //relative to web site root
                RequestResult result = browsingSession.Get(url);
                var viewResult = (ViewResult)result.ActionExecutedContext.Result;
                Assert.Equal("Index", viewResult.ViewName);
                Assert.Equal("192.168.1.121", viewResult.ViewData["result"]);
            });
        }

        [Fact]
        public void Index_POST()
        {
            appHost.Start(browsingSession =>
            {
                string url = ""; //relative to web site root
                RequestResult result = browsingSession.Post(url, new
                {
                    SubmitBtn = "Submit",
                    SomeTextBox = "hop-hop",
                });
                var viewResult = (ViewResult)result.ActionExecutedContext.Result;
                Assert.Equal("Index", viewResult.ViewName);
                Assert.Equal("Submit", viewResult.ViewData["result"]);
            });
        }
    }
}
