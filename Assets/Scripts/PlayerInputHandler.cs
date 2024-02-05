using UnityEngine;
using UnityEngine.InputSystem;
using Utility;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset _playerControls;

    private InputAction _moveAction;

    public Vector2 MoveInput { get; private set; }


    protected void Awake()
    {
        _moveAction = _playerControls.FindActionMap(UtilityTermMap.Player).FindAction(UtilityTermMap.Move);
        RegisterInputActions();
    }


    private void RegisterInputActions()
    {
        _moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        _moveAction.canceled += context => MoveInput = Vector2.zero;
    }


    private void OnEnable()
    {
        _moveAction.Enable();
    }

    private void OnDisable()
    {
        _moveAction.Disable();
    }
}
