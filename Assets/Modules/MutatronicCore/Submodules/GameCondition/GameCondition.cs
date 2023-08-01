using System.Collections.Generic;
using UnityEngine;

namespace Modules.MutatronicCore.Submodules.GameCondition
{
    [CreateAssetMenu(fileName = "GameCondition", menuName = "MutatronicCore/Create game condition", order = 1)]
    public class GameCondition: ScriptableObject
    {
        [SerializeField]
        private bool _invert;

        public bool IsTrue => _invert ? !_isTrue : _isTrue;

        private bool _isTrue;

        private List<GameConditionActivator> _activators = new List<GameConditionActivator>();
        private List<IConditionListener> _listeners = new List<IConditionListener>();


        public GameConditionActivator ActivateCondition()
        {
            GameConditionActivator activator = new GameConditionActivator(this);
            _activators.Add(activator);

            if (_activators.Count == 1)
            {
                _isTrue = true;
                ConditionUpdated();
            }

            return activator;
        }


        public void DeactivateCondition(GameConditionActivator activator)
        {
            if (_activators.Contains(activator))
            {
                _activators.Remove(activator);

                if (_activators.Count == 0)
                {
                    _isTrue = false;
                    ConditionUpdated();
                }
            }
        }


        private void ConditionUpdated()
        {
            foreach (IConditionListener listener in _listeners)
            {
                listener.OnConditionUpdated(this);
            }
        }


        public void Subscribe(IConditionListener listener)
        {
            if (!_listeners.Contains(listener))
            {
                _listeners.Add(listener);
            }
        }


        public void Unsubscribe(IConditionListener listener)
        {
            if (_listeners.Contains(listener))
            {
                _listeners.Remove(listener);
            }
        }
    }
}
