using Modules.MutatronicCore.Submodules.InputActions.Scripts;
using UnityEngine;
using Zenject;

namespace Modules.MutatronicCore.Scripts.Runtime.EntityMovement
{
    [RequireComponent(typeof(EntityMovementComponent))]
    public class EntityMovementInputController : MutatronicBehaviour
    {
        [SerializeField]
        private EntityMovementComponent _entityMovementComponent;

        [SerializeField]
        private InputAction _moveForwardInputAction;

        [SerializeField]
        private InputAction _moveBackwardInputAction;

        [SerializeField]
        private InputAction _moveLeftInputAction;

        [SerializeField]
        private InputAction _moveRightInputAction;

        [SerializeField]
        private InputAction _moveUpInputAction;

        [SerializeField]
        private InputAction _moveDownInputAction;

        [SerializeField]
        private InputAction _rotateRightInputAction;

        [SerializeField]
        private InputAction _rotateLeftInputAction;

        [SerializeField]
        private InputAction _rotateUpInputAction;

        [SerializeField]
        private InputAction _rotateDownInputAction;

        private InputActionsHandler _inputActionsHandler;
        private MovementTarget _movementTarget;


        [Inject]
        private void Inject(InputActionsHandler inputActionsHandler)
        {
            _inputActionsHandler = inputActionsHandler;
        }


        protected void Awake()
        {
            _movementTarget = new MovementTarget();
        }


        private void OnEnable()
        {
            _inputActionsHandler.Subscribe(MoveForward, _moveForwardInputAction);
            _inputActionsHandler.Subscribe(MoveBackwards, _moveBackwardInputAction);
            _inputActionsHandler.Subscribe(MoveRight, _moveRightInputAction);
            _inputActionsHandler.Subscribe(MoveLeft, _moveLeftInputAction);
            _inputActionsHandler.Subscribe(MoveUp, _moveUpInputAction);
            _inputActionsHandler.Subscribe(MoveDown, _moveDownInputAction);
            _inputActionsHandler.Subscribe(RotateRight, _rotateRightInputAction);
            _inputActionsHandler.Subscribe(RotateLeft, _rotateLeftInputAction);
            _inputActionsHandler.Subscribe(RotateUp, _rotateUpInputAction);
            _inputActionsHandler.Subscribe(RotateDown, _rotateDownInputAction);
        }


        private void OnDisable()
        {
            _inputActionsHandler.Unsubscribe(MoveForward, _moveForwardInputAction);
            _inputActionsHandler.Unsubscribe(MoveBackwards, _moveBackwardInputAction);
            _inputActionsHandler.Unsubscribe(MoveRight, _moveRightInputAction);
            _inputActionsHandler.Unsubscribe(MoveLeft, _moveLeftInputAction);
            _inputActionsHandler.Unsubscribe(MoveUp, _moveUpInputAction);
            _inputActionsHandler.Unsubscribe(MoveDown, _moveDownInputAction);
            _inputActionsHandler.Unsubscribe(RotateRight, _rotateRightInputAction);
            _inputActionsHandler.Unsubscribe(RotateLeft, _rotateLeftInputAction);
            _inputActionsHandler.Unsubscribe(RotateUp, _rotateUpInputAction);
            _inputActionsHandler.Unsubscribe(RotateDown, _rotateDownInputAction);
        }


        private void MoveRight()
        {
            _movementTarget.TargetDirection = Vector3.right;
            _entityMovementComponent.MoveTo(_movementTarget);
        }


        private void MoveLeft()
        {
            _movementTarget.TargetDirection = Vector3.left;
            _entityMovementComponent.MoveTo(_movementTarget);
        }


        private void MoveForward()
        {
            _movementTarget.TargetDirection = Vector3.forward;
            _entityMovementComponent.MoveTo(_movementTarget);
        }


        private void MoveBackwards()
        {
            _movementTarget.TargetDirection = Vector3.back;
            _entityMovementComponent.MoveTo(_movementTarget);
        }


        private void MoveUp()
        {
            _movementTarget.TargetDirection = Vector3.up;
            _entityMovementComponent.MoveTo(_movementTarget);
        }


        private void MoveDown()
        {
            _movementTarget.TargetDirection = Vector3.down;
            _entityMovementComponent.MoveTo(_movementTarget);
        }


        private void RotateRight()
        {
            _movementTarget.TargetDirection = Vector3.right;
            _entityMovementComponent.RotateTo(_movementTarget);
        }


        private void RotateLeft()
        {
            _movementTarget.TargetDirection = Vector3.left;
            _entityMovementComponent.RotateTo(_movementTarget);
        }


        private void RotateUp()
        {
            _movementTarget.TargetDirection = Vector3.up;
            _entityMovementComponent.RotateTo(_movementTarget);
        }


        private void RotateDown()
        {
            _movementTarget.TargetDirection = Vector3.down;
            _entityMovementComponent.RotateTo(_movementTarget);
        }
    }
}
