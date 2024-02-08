using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Utility;

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
        RegisterInputActions();
    }


    private void RegisterInputActions()
    {
        _moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        _moveAction.canceled += _ => MoveInput = Vector2.zero;

        _interactAction.performed += _ => OnInteract?.Invoke();
    }


    private void OnEnable()
    {
        _moveAction.Enable();
        _interactAction.Enable();
    }

    private void OnDisable()
    {
        _moveAction.Disable();
        _interactAction.Disable();
    }
}
