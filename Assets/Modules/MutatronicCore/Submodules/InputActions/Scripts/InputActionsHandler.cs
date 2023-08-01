using System;
using System.Collections.Generic;

namespace Modules.MutatronicCore.Submodules.InputActions.Scripts
{
    public class InputActionsHandler
    {
        private Dictionary<InputAction, List<Action>> _inputActions = new Dictionary<InputAction, List<Action>>();


        public void Subscribe(Action subscriberAction, InputAction inputAction)
        {
            if ( _inputActions.ContainsKey(inputAction))
            {
                if (!_inputActions[inputAction].Contains(subscriberAction))
                {
                    _inputActions[inputAction].Add(subscriberAction);
                }
            }
            else
            {
                List<Action> subsList = new List<Action>(1)
                {
                    subscriberAction
                };
                _inputActions.Add(inputAction, subsList);
            }
        }


        public void Unsubscribe(Action subscriberAction, InputAction inputAction)
        {
            if (_inputActions.ContainsKey(inputAction) && _inputActions[inputAction].Contains(subscriberAction))
            {
                _inputActions[inputAction].Remove(subscriberAction);
            }
        }


        public void PerformInputAction(InputAction inputAction)
        {
            if (_inputActions.ContainsKey(inputAction) && inputAction.CanPerform())
            {
                foreach (Action listener in _inputActions[inputAction])
                {
                    listener?.Invoke();
                }
            }
        }
    }
}