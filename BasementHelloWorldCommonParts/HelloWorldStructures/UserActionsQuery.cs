using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasementHelloWorldCommonParts.HelloWorldStructures
{
    public class UserActionsQuery
    {
        private List<OpaUserAction> _actions = new List<OpaUserAction>();
        public IEnumerable<OpaUserAction> actions { get { return _actions; } }
        public void AddUserAction(int hostViewID, OpaUserAction actionToAdd)
        {
            actionToAdd.hostViewID = hostViewID;
            _actions.Add(actionToAdd);
        }

        public void Clear() { _actions.Clear(); }
    }
}

