using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasementHelloWorldCommonParts.HelloWorldStructures;

namespace BasementHelloWorldCommonParts.UI
{
    public class OpaView : I_OpaView 
    {
        public OpaView()
        {
            _viewID = ViewStateManager.consumeLastFreeID();
        }

        public System.Type viewType
        { get { return this.GetType(); } }

        private int _viewID = 0;
        public int viewID
        { get { return _viewID; } }

        private int _parentViewID = 0;
        public int parentViewID
        { get { return _parentViewID; } }

        public Dictionary<string, bool> boolProperties 
        {
            get
            {
                Dictionary<string,bool> ausgabe = new Dictionary<string,bool>();

                PropertyInfo[] props = this.GetType().GetProperties();//(BindingFlags.Static | BindingFlags.Public);
                foreach (PropertyInfo pi in props)
                {
                    if (pi.PropertyType == typeof(bool) && pi.Name.StartsWith("boolProp_")) {
                        bool val = (bool)(pi.GetValue(this,null));
                        ausgabe.Add(pi.Name, val);
                    }

                }
                return ausgabe;
            }
        }

        public Dictionary<string, int> intProperties
        {
            get
            {
                Dictionary<string, int> ausgabe = new Dictionary<string, int>();

                PropertyInfo[] props = this.GetType().GetProperties();//(BindingFlags.Static | BindingFlags.Public);
                foreach (PropertyInfo pi in props)
                {
                    if (pi.PropertyType == typeof(int) && pi.Name.StartsWith("intProp_"))
                    {
                        int val = (int)(pi.GetValue(this, null));
                        ausgabe.Add(pi.Name, val);
                    }

                }
                return ausgabe;
            }
        }

        public Dictionary<string, string> stringProperties
        {
            get
            {
                Dictionary<string, string> ausgabe = new Dictionary<string, string>();

                PropertyInfo[] props = this.GetType().GetProperties();//(BindingFlags.Static | BindingFlags.Public);
                foreach (PropertyInfo pi in props)
                {
                    if (pi.PropertyType == typeof(string) && pi.Name.StartsWith("strProp_"))
                    {
                        string val = (string)(pi.GetValue(this, null));
                        ausgabe.Add(pi.Name, val);
                    }

                }
                return ausgabe;
            }
        }

        public Dictionary<string, List<int>> subViewsLists
        {
            get
            {
                Dictionary<string, List<int>> ausgabe = new Dictionary<string, List<int>>();

                PropertyInfo[] props = this.GetType().GetProperties();//(BindingFlags.Static | BindingFlags.Public);
                foreach (PropertyInfo pi in props)
                {
                    if (pi.PropertyType == typeof(List<int>) && pi.Name.StartsWith("subViews_"))
                    {
                        ausgabe.Add(pi.Name, (List<int>)(pi.GetValue(this, null)));
                    }

                }
                return ausgabe;
            }
        }

        public void assimilateWith(I_OpaView anotherView)
        {
            if (!(anotherView.viewType == viewType )){
                throw new ArgumentException("This instance is of type " + viewType.ToString() + " and cannot be assimilated with type " + anotherView.viewType.ToString() );
            }

            PropertyInfo[] props = this.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo pi in props)
            {
                string[] propNameParts = pi.Name.Split('.');
                string propName = "";
                if (propNameParts.Length > 0) { propName = propNameParts[propNameParts.Length - 1]; }

                if (propName.StartsWith("boolProp_"))
                {
                    pi.SetValue(this, anotherView.boolProperties[pi.Name], null);
                }
                else if (propName.StartsWith("intProp_"))
                {
                    pi.SetValue(this, anotherView.intProperties[pi.Name], null);
                }
                else if (propName.StartsWith("strProp_"))
                {
                    pi.SetValue(this, anotherView.stringProperties[pi.Name], null);
                }
                else if (propName.StartsWith("subViews_"))
                {
                    pi.SetValue(this, anotherView.subViewsLists[pi.Name], null);
                }
            }

            this._viewID = anotherView.viewID;
            this._parentViewID = anotherView.parentViewID;
        }
    }
}
