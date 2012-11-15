using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasementHelloWorldCommonParts.UI
{
    public interface I_OpaView
    {
        int viewID { get; set; }
        int parentViewID { get; }
        System.Type viewType { get; }

        Dictionary<string, bool> boolProperties { get; }
        Dictionary<string, int> intProperties { get; }
        Dictionary<string, string> stringProperties { get; }

        Dictionary<string, List<int>> subViewsLists { get; }

        void assimilateWith(I_OpaView anotherView);
        void afterAssimilating();
    }
}
