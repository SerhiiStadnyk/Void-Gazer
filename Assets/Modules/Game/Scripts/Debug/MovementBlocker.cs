using System;
using Modules.MutatronicCore.Submodules.GameCondition;
using UnityEngine;

namespace Modules.Game.Scripts.Debug
{
    public class MovementBlocker : MonoBehaviour
    {
        [SerializeField]
        private GameCondition _movementBlockedCondition;

        private IDisposable _activator;


        void Start()
        {
            _activator = _movementBlockedCondition.ActivateCondition();
        }


        public void OnDestroy()
        {
            _activator?.Dispose();
        }
    }
}