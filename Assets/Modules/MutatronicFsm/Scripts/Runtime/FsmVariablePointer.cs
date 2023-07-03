using UnityEngine;

namespace Modules.MutatronicFsm.Scripts.Runtime
{
    [CreateAssetMenu(fileName = "FsmVarPointer_.asset", menuName = "Mutatronic/Runtime/Fsm/Variable Pointer")]
    public class FsmVariablePointer : ScriptableObject
    {
        [SerializeField]
        private string _variableName;

        public string VariableName => _variableName;
    }
}