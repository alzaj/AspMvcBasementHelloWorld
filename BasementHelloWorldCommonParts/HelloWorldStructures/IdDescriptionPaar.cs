using BasementHelloWorldCommonParts.UI;

namespace BasementHelloWorldCommonParts.HelloWorldStructures
{
    public class IdDescriptionPaar : I_IdDescriptionPaar
    {
 
        #region I_IdDescriptionPaar

        public int viewID
        { get {return 1; } set{ } }

        private string _shortID = "";
        public string shortID
        {
            get { return _shortID; }
            set { _shortID = value; }
        }

        private string _description = "";
        public string description
        {
            get { return _description; }
            set { _description = value; }
        }

        #endregion //I_IdDescriptionPaar

    }
}
