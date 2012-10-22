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
    [TestCategory("Unit"), TestCategory("DialogView_InitialCall")]
    class DialogView_InitialCall
    {
        [Fact]
        public void Index_GET()
        { 
        }
    }
}
