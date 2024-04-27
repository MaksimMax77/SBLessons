using System.Collections.Generic;
using UnityEngine;

namespace Code.ComponentActions
{
    public class ActionsManager : MonoBehaviour
    {
        [SerializeField] private ComponentAction[] _actions;
        public ComponentAction[] GetActions()
        {
            return _actions;
        }

        public List<T> GetActionsByType<T>() where T : ComponentAction
        {
            var collectedActions = new List<T>();

            for (int i = 0, len = _actions.Length; i < len; ++i)
            {
                if (_actions[i] is T castAction)
                {
                    collectedActions.Add(castAction);
                }
            }

            return collectedActions;
        }

        private void OnEnable()
        {
            for (int i = 0, len = _actions.Length; i < len; ++i)
            {
                _actions[i].ResetAction();
            }
        }
    }
}