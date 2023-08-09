using System;
using System.Collections.Generic;
using Modules.MutatronicCore.Submodules.InputActions.Scripts;
using UnityEngine;
using Zenject;

namespace Modules.MutatronicCore.Scripts.Runtime.InputActions
{
    public abstract class InputActionEventInvokerBase : MutatronicBehaviour
    {
        protected abstract List<InputActionEventPairBase> InputActionEvents { get; }

        private InputActionsHandler _inputActionsHandler;


        [Inject]
        public void Inject(InputActionsHandler inputActionsHandler)
        {
            _inputActionsHandler = inputActionsHandler;
        }


        protected void OnEnable()
        {
            foreach (InputActionEventPairBase pair in InputActionEvents)
            {
                _inputActionsHandler.Subscribe(pair.PairAction, pair.InputAction);
            }
        }


        protected void OnDisable()
        {
            foreach (InputActionEventPairBase pair in InputActionEvents)
            {
                _inputActionsHandler.Unsubscribe(pair.PairAction, pair.InputAction);
            }
        }


        protected abstract class InputActionEventPairBase
        {
            [SerializeField]
            private InputAction _inputAction;

            public InputAction InputAction => _inputAction;

            public abstract Action PairAction { get; }
        }
    }
}
