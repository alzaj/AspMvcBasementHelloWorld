using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasementHelloWorldCommonParts.UI
{
    public interface I_UI_MultipleDialogs : I_OpaView
    {
        List<int> subViews_DialogWithUser { get; set; }

        bool boolProp_isActionPossible_AddNewDialog { get; set; }

        bool boolProp_isActionPossible_RemoveLastDialog { get; set; }
    }
}
