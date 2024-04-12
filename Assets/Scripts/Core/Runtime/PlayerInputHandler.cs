using System;
using Core.Runtime.Utility;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Runtime
{
    public class PlayerInputHandler : MonoBehaviour
    {
        [SerializeField]
        private InputActionAsset _playerControls;

        private InputAction _moveAction;
        private InputAction _interactAction;

        public event Action OnInteract;

        public Vector2 MoveInput { get; private set; }


        protected void Awake()
        {
            _moveAction = _playerControls.FindActionMap(UtilityTermMap.Player).FindAction(UtilityTermMap.Move);
            _interactAction = _playerControls.FindActionMap(UtilityTermMap.Player).FindAction(UtilityTermMap.Interact);
        }


        private void OnEnable()
        {
            _moveAction.Enable();
            _interactAction.Enable();

            RegisterInputActions();
        }


        private void OnDisable()
        {
            _moveAction.Disable();
            _interactAction.Disable();

            UnregisterInputActions();
        }


        private void RegisterInputActions()
        {
            _moveAction.performed += OnMoveActionPerformed;
            _moveAction.canceled += OnMoveActionCanceled;
            _interactAction.performed += OnInteractActionPerformed;
        }


        private void UnregisterInputActions()
        {
            _moveAction.performed -= OnMoveActionPerformed;
            _moveAction.canceled -= OnMoveActionCanceled;
            _interactAction.performed -= OnInteractActionPerformed;
        }


        private void OnInteractActionPerformed(InputAction.CallbackContext context)
        {
            OnInteract?.Invoke();
        }


        private void OnMoveActionCanceled(InputAction.CallbackContext context)
        {
            MoveInput = Vector2.zero;
        }


        private void OnMoveActionPerformed(InputAction.CallbackContext context)
        {
            MoveInput = context.ReadValue<Vector2>();
        }
    }
}
