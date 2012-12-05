using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasementHelloWorldCommonParts.UI
{
    public class Mock_UI_MultipleDialogs : OpaView, I_UI_MultipleDialogs
    {
        public List<int> _subViews_DialogWithUser = new List<int>();
        public List<int> subViews_DialogWithUser {
            get { return _subViews_DialogWithUser; }
            set { _subViews_DialogWithUser = value; }
        }
    }
}
