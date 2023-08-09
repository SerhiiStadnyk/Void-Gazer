using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Modules.MutatronicCore.Scripts.Runtime.InputActions
{
    public class InputActionEventInvoker : InputActionEventInvokerBase
    {
        [SerializeField]
        private List<InputActionEventPair> _inputActionEvents;

        protected override List<InputActionEventPairBase> InputActionEvents => new List<InputActionEventPairBase>(_inputActionEvents);


        [Serializable]
        protected class InputActionEventPair: InputActionEventPairBase
        {
            [SerializeField]
            private UnityEvent _unityEvent;

            public override Action PairAction => _unityEvent.Invoke;
        }
    }
}
