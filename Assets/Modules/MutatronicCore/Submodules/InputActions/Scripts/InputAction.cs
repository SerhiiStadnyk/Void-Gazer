using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Modules.MutatronicCore.Submodules.InputActions.Scripts
{
    [CreateAssetMenu(fileName = "InputAction", menuName = "MutatronicCore/Create Input action", order = 1)]
    public class InputAction : ScriptableObject
    {
        [SerializeField]
        private List<GameCondition.GameCondition> _conditions;


        public bool CanPerform()
        {
            bool result = true;
            if (_conditions is { Count: > 0 })
            {
                result = _conditions.All(condition => condition.IsTrue);
            }

            return result;
        }
    }
}
