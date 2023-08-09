using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Modules.MutatronicCore.Scripts.Runtime.InputActions
{
    public class InputActionEventInvokerVector3 : InputActionEventInvokerBase
    {
        [SerializeField]
        private List<InputActionEventPairVector3> _inputActionVector3Events;

        protected override List<InputActionEventPairBase> InputActionEvents => new List<InputActionEventPairBase>(_inputActionVector3Events);


        [Serializable]
        protected class InputActionEventPairVector3: InputActionEventPairBase
        {
            [SerializeField]
            private Vector3 _value;

            [SerializeField]
            private UnityEvent<Vector3> _unityEvent;

            public override Action PairAction => () => _unityEvent.Invoke(_value);
        }
    }
}
