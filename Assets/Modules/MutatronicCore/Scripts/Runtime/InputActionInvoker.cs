using System;
using System.Collections.Generic;
using Modules.MutatronicCore.Submodules.InputActions.Scripts;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Modules.MutatronicCore.Scripts.Runtime
{
    public class InputActionInvoker : MutatronicBehaviour
    {
        [SerializeField]
        private List<InputActionPair> _inputActionPairs;

        private InputActionsHandler _inputActionsHandler;


        [Inject]
        public void Inject(InputActionsHandler inputActionsHandler)
        {
            _inputActionsHandler = inputActionsHandler;
        }


        protected void OnEnable()
        {
            foreach (InputActionPair pair in _inputActionPairs)
            {
                _inputActionsHandler.Subscribe(pair.PairAction, pair.InputAction);
            }
        }


        protected void OnDisable()
        {
            foreach (InputActionPair pair in _inputActionPairs)
            {
                _inputActionsHandler.Unsubscribe(pair.PairAction, pair.InputAction);
            }
        }


        [Serializable]
        private class InputActionPair
        {
            [SerializeField]
            private InputAction _inputAction;

            [SerializeField]
            private UnityEvent _unityEvent;

            public InputAction InputAction => _inputAction;

            public Action PairAction => _unityEvent.Invoke;
        }
    }
}
