using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasementHelloWorldCommonParts.UI
{
    public interface I_IdDescriptionPaar : I_OpaView
    {
        string strProp_shortID { get; set; }
        string strProp_description { get; set; }
    }
}
