using Modules.MutatronicCore.Scripts.Runtime;
using Modules.MutatronicCore.Submodules.InputActions.Scripts;
using UnityEngine;
using Zenject;

namespace Modules.Game.Scripts.EntityMovement
{
    [RequireComponent(typeof(EntityMovementComponent))]
    public class EntityMovementInputController : MutatronicBehaviour
    {
        private EntityMovementComponent _entityMovementComponent;

        private InputActionsHandler _inputActionsHandler;

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


        [Inject]
        private void Inject(InputActionsHandler inputActionsHandler)
        {
            _inputActionsHandler = inputActionsHandler;
        }


        protected void Awake()
        {
            _entityMovementComponent = GetComponent<EntityMovementComponent>();
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
            _entityMovementComponent.MoveInDirection(EntityMovementComponent.MovementType.MoveRight);
        }


        private void MoveLeft()
        {
            _entityMovementComponent.MoveInDirection(EntityMovementComponent.MovementType.MoveLeft);
        }


        private void MoveForward()
        {
            _entityMovementComponent.MoveInDirection(EntityMovementComponent.MovementType.MoveForward);
        }


        private void MoveBackwards()
        {
            _entityMovementComponent.MoveInDirection(EntityMovementComponent.MovementType.MoveBackwards);
        }


        private void MoveUp()
        {
            _entityMovementComponent.MoveInDirection(EntityMovementComponent.MovementType.MoveUp);
        }


        private void MoveDown()
        {
            _entityMovementComponent.MoveInDirection(EntityMovementComponent.MovementType.MoveDown);
        }


        private void RotateRight()
        {
            _entityMovementComponent.MoveInDirection(EntityMovementComponent.MovementType.RotateRight);
        }


        private void RotateLeft()
        {
            _entityMovementComponent.MoveInDirection(EntityMovementComponent.MovementType.RotateLeft);
        }


        private void RotateUp()
        {
            _entityMovementComponent.MoveInDirection(EntityMovementComponent.MovementType.RotateUp);
        }


        private void RotateDown()
        {
            _entityMovementComponent.MoveInDirection(EntityMovementComponent.MovementType.RotateDown);
        }
    }
}
