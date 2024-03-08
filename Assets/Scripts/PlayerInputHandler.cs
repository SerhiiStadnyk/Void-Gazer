using System;
using GlobalEvents;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Utility;

public class PlayerInputHandler : MonoBehaviour, IDisposable
{
    [SerializeField]
    private InputActionAsset _playerControls;

    [SerializeField]
    private GlobalEvent _openInventoryInputEvent;

    [SerializeField]
    private UnityEvent _escapeInputEvent;

    private InputAction _moveAction;
    private InputAction _interactAction;
    private InputAction _openInventoryAction;
    private InputAction _openGameMenuAction;

    public event Action OnInteract;

    public Vector2 MoveInput { get; private set; }


    protected void Awake()
    {
        _moveAction = _playerControls.FindActionMap(UtilityTermMap.Player).FindAction(UtilityTermMap.Move);
        _interactAction = _playerControls.FindActionMap(UtilityTermMap.Player).FindAction(UtilityTermMap.Interact);
        _openInventoryAction = _playerControls.FindActionMap(UtilityTermMap.Player).FindAction(UtilityTermMap.OpenInventory);
        _openGameMenuAction = _playerControls.FindActionMap(UtilityTermMap.Player).FindAction(UtilityTermMap.Escape);
    }


    private void OnEnable()
    {
        _moveAction.Enable();
        _interactAction.Enable();
        _openInventoryAction.Enable();
        _openGameMenuAction.Enable();

        RegisterInputActions();
    }


    private void OnDisable()
    {
        _moveAction.Disable();
        _interactAction.Disable();
        _openInventoryAction.Disable();
        _openGameMenuAction.Disable();

        UnregisterInputActions();
    }


    private void RegisterInputActions()
    {
        _moveAction.performed += OnMoveActionPerformed;
        _moveAction.canceled += OnMoveActionCanceled;

        _interactAction.performed += OnInteractActionPerformed;
        _openInventoryAction.performed += OnOpenInventory;
        _openGameMenuAction.performed += OnOpenGameMenuActionPerformed;
    }


    private void UnregisterInputActions()
    {
        _moveAction.performed -= OnMoveActionPerformed;
        _moveAction.canceled -= OnMoveActionCanceled;

        _interactAction.performed -= OnInteractActionPerformed;
        _openInventoryAction.performed -= OnOpenInventory;
        _openGameMenuAction.performed -= OnOpenGameMenuActionPerformed;
    }


    private void OnOpenGameMenuActionPerformed(InputAction.CallbackContext context)
    {
        _escapeInputEvent?.Invoke();
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


    private void OnOpenInventory(InputAction.CallbackContext callbackContext)
    {
        _openInventoryInputEvent.TriggerEvent();
    }


    void IDisposable.Dispose()
    {
        _moveAction?.Dispose();
        _interactAction?.Dispose();
        _openInventoryAction?.Dispose();
        _openGameMenuAction?.Dispose();
    }
}
