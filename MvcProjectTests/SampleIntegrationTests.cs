using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Xunit;
using Xunit.Extensions;
using Xunit.Sdk;
using AspMvcBasementHelloWorld;
using AspMvcBasementHelloWorld.Controllers;
using AspMvcBasementHelloWorld.ViewModels;
using MvcIntegrationTestFramework.Browsing;
using MvcIntegrationTestFramework.Hosting;
using MvcIntegrationTestFramework.Interception;

namespace MvcProjectTests
{
    [CustomTestClassCommand]
    public class SampleIntegrationTests
    {

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
