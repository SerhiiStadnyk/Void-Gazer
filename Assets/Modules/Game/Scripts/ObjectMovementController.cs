using Modules.MutatronicCore.Scripts.Runtime;
using Modules.MutatronicCore.Submodules.InputActions.Scripts;
using UnityEngine;
using Zenject;

namespace Modules.Game.Scripts
{
    public class ObjectMovementController : MutatronicBehaviour
    {
        [SerializeField]
        private InputAction _moveForwardInputAction;

        private InputActionsHandler _inputActionsHandler;


        [Inject]
        private void Inject(InputActionsHandler inputActionsHandler)
        {
            _inputActionsHandler = inputActionsHandler;
        }


        private void OnEnable()
        {
            _inputActionsHandler.Subscribe(MoveForward, _moveForwardInputAction);
        }


        private void OnDisable()
        {
            _inputActionsHandler.Unsubscribe(MoveForward, _moveForwardInputAction);
        }


        private void MoveForward()
        {
        }
    }
}