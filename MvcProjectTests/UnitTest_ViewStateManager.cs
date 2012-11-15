using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Xunit.Extensions;
using BasementHelloWorldCommonParts.HelloWorldStructures;
using BasementHelloWorldCommonParts.UI;

namespace MvcProjectTests
{
    [CustomTestClassCommand]
    [TestCategory("Unit"), TestCategory("UnitTest_ViewStateManager")]
    public class UnitTest_ViewStateManager
    {
        #region Tests
        [Fact]
        public void SaveAndRestoreSampleUserView()
        {
            //Arrange
            SampleUserView beforeView = ViewStateManager.getViewFromViewState<SampleUserView>(0);
            
            Assert.True(String.IsNullOrEmpty(beforeView.strProp_SampleStringProperty));
            beforeView.strProp_SampleStringProperty = "sampleStr";

            Assert.False(beforeView.boolProp_SampleBooleanProperty);
            beforeView.boolProp_SampleBooleanProperty = true;

            Assert.Equal(0, beforeView.intProp_SampleBooleanProperty);
            beforeView.intProp_SampleBooleanProperty = 10;

            Assert.Equal(0, beforeView.subViews_SampleSubViews.Count);
            for (int i = 1; i < 4; i++)
            {
                beforeView.subViews_SampleSubViews.Add(i);
            }

            //Act
            ViewStateManager.saveViewToViewState(beforeView);
            SampleUserView restoredView = ViewStateManager.getViewFromViewState<SampleUserView>(beforeView.viewID);

            //Assert
            Assert.Equal(beforeView.strProp_SampleStringProperty, restoredView.strProp_SampleStringProperty);
            Assert.True(restoredView.boolProp_SampleBooleanProperty);
            Assert.Equal(beforeView.intProp_SampleBooleanProperty, restoredView.intProp_SampleBooleanProperty);
            Assert.Equal(3, beforeView.subViews_SampleSubViews.Count);
            for (int i = 1; i < 4; i++)
            {
                Assert.Equal(i, beforeView.subViews_SampleSubViews[i-1]);
            }


        }
        #endregion //Tests

        #region HelpMethods

        private class SampleUserView : OpaView
        {
            public string strProp_SampleStringProperty { get; set; }
            public bool boolProp_SampleBooleanProperty { get; set; }
            public int intProp_SampleBooleanProperty { get; set; }

            private List<int> _subViews_SampleSubViews = new List<int>();
            public List<int> subViews_SampleSubViews 
            {
                get { return _subViews_SampleSubViews; }
                set { _subViews_SampleSubViews = value; }
            }
        }

        #endregion //HelpMethods
    }

   
}
