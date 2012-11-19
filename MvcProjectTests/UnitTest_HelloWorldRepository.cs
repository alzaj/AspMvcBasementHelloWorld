using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Xunit;
using Xunit.Extensions;
using BasementHelloWorldCommonParts.Repositories;
using BasementHelloWorldCommonParts.HelloWorldStructures;

namespace MvcProjectTests
{
    [CustomTestClassCommand]
    [TestCategory("Unit"), TestCategory("UnitTest_HelloWorldRepository")]
    public class UnitTest_HelloWorldRepository
    {

#region Tests
        [Fact]
        public void InitDebugRepository()
        {
            //Arrange
            I_HelloWorldRepository repEmpty = TestSettings.Get_HelloWorldRepository();
            I_HelloWorldRepository repLangs_NoTranslations = TestSettings.Get_HelloWorldRepository(true);
            I_HelloWorldRepository repLangsAndTranslations = TestSettings.Get_HelloWorldRepository(true, true);

            //Act

            //Assert
            Assert.Equal(0,repEmpty.GetAllLanguages().Count);
            Assert.Equal(0,repEmpty.GetAllTranslations().Count);

            Assert.Equal(4,repLangs_NoTranslations.GetAllLanguages().Count);
            Assert.Equal(0,repLangs_NoTranslations.GetAllTranslations().Count);

            Assert.Equal(4,repLangsAndTranslations.GetAllLanguages().Count);
            Assert.Equal(28, repLangsAndTranslations.GetAllTranslations().Count);
        }

#endregion //Tests

#region TestsSetup
#endregion TestsSetup
    }
}
