using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasementHelloWorldCommonParts.UI
{
    public interface I_IdDescriptionPaar : I_OpaView
    {

        string shortID { get; set; }
        string description { get; set; }

    }
}
