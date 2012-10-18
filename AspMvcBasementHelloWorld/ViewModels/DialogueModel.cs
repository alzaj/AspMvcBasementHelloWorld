using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspMvcBasementHelloWorld.ViewModels
{
    public class DialogueModel
    {
        private int _languagesCount = 4;
        public int languagesCount
        {
            get{ return _languagesCount;}
            set{ _languagesCount = value;}
        }
    }
}