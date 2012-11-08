using BasementHelloWorldCommonParts.UI;

namespace BasementHelloWorldCommonParts.HelloWorldStructures
{
    public class IdDescriptionPaar : OpaView, I_IdDescriptionPaar
    {
 
        #region I_IdDescriptionPaar

        private string _shortID = "";
        public string strProp_shortID
        {
            get { return _shortID; }
            set { _shortID = value; }
        }

        private string _description = "";
        public string strProp_description
        {
            get { return _description; }
            set { _description = value; }
        }

        #endregion //I_IdDescriptionPaar

    }
}
