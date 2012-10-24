using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcProjectTests
{
    public class TestSettings
    {
        public static GuiToTest GUI = GuiToTest.MockUI;
        public static DbToTest DB = DbToTest.MockDataBase;        
    }

    public enum GuiToTest {
        AspMvcApplication,
        MockUI
    }

    public enum DbToTest
    {
        MockDataBase
    }
}
