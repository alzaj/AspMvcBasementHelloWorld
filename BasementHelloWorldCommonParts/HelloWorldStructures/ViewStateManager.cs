using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasementHelloWorldCommonParts.UI;

namespace BasementHelloWorldCommonParts.HelloWorldStructures
{
    public static class ViewStateManager
    {
        private static Dictionary<int, ArchivedViewState> _views = new Dictionary<int, ArchivedViewState>();

        public static IEnumerable<ArchivedViewState> views
        {
            get { return (IEnumerable<ArchivedViewState>)_views; }
        }

        private static int _nextFreeIndex = 1;
        public static int currentNextFreeIndex()
        {
            return _nextFreeIndex;
        }
        public static int consumeLastFreeID()
        {
            _nextFreeIndex += 1;
            return _nextFreeIndex - 1;
        }

        private static int _returnCurrentFreeIdAndIncrementIt()
        {
            int ausgabe = _nextFreeIndex;
            _nextFreeIndex += 1;
            return ausgabe;
        }

        public static T_viewType getViewFromViewState<T_viewType>(int viewID) where T_viewType : I_OpaView, new()
        {
            T_viewType ausgabe;
            bool viewExists = _views.ContainsKey(viewID);

            ausgabe = new T_viewType();
            if (viewExists && (viewID > 0))
            {
                ((I_OpaView)ausgabe).assimilateWith(_views[viewID]);
            }
            else
            {
                ausgabe.viewID = consumeLastFreeID();
            }

            return ausgabe;
        }

        public static void saveViewToViewState(I_OpaView viewToSave)
        {
            if (_views.ContainsKey(viewToSave.viewID))
            {
                _views[viewToSave.viewID].assimilateWith(viewToSave);
            }
            else
            {
                _views.Add(viewToSave.viewID, new ArchivedViewState(viewToSave));
            }

        }


        public static void Delete(int viewID)
        {
            _views.Remove(viewID);
        }

    }

    public class ArchivedViewState : I_OpaView
    {
        public ArchivedViewState(I_OpaView itemToArchive)
        {
            if (!(itemToArchive is I_OpaView))
            {
                throw new ArgumentException("The item to be archived must implement the I_OpaView interface");
            }
            initFrom_I_OpaView(itemToArchive);
        }

        #region I_OpaView

        private System.Type _viewType;
        public System.Type viewType
        { get { return _viewType; } }

        private int _viewID = 0;
        public int viewID
        { 
            get { return _viewID; }
            set { _viewID = value; }
        }

        private int _parentViewID = 0;
        public int parentViewID
        { get { return _parentViewID; } }

        private Dictionary<string, bool> _boolProperties;
        public Dictionary<string, bool> boolProperties 
        {
            get
            { return _boolProperties; }
        }

        private Dictionary<string, int> _intProperties;
        public Dictionary<string, int> intProperties
        {
            get
            { return _intProperties; }
        }

        private Dictionary<string, string> _stringProperties;
        public Dictionary<string, string> stringProperties
        {
            get { return _stringProperties; }
        }

        private Dictionary<string, List<int>> _subViewsLists;
        public Dictionary<string, List<int>> subViewsLists
        {
            get
            { return _subViewsLists; }
        }
        
        public void assimilateWith(I_OpaView anotherView)
        {
            initFrom_I_OpaView(anotherView);
        }

        public void afterAssimilating() { }

        #endregion //I_OpaView

        private void initFrom_I_OpaView(I_OpaView anotherView)
        {
            this._viewID = anotherView.viewID;
            this._parentViewID = anotherView.parentViewID;
            this._viewType = anotherView.viewType;

            this._boolProperties = anotherView.boolProperties;
            this._intProperties = anotherView.intProperties;
            this._stringProperties = anotherView.stringProperties;
            this._subViewsLists = anotherView.subViewsLists;
        }

    }
}
