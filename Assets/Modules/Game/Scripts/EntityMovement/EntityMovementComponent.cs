using System;
using System.Collections.Generic;
using Modules.MutatronicCore.Scripts.Runtime;
using Modules.MutatronicCore.Submodules.GameCondition;
using UnityEngine;

namespace Modules.Game.Scripts.EntityMovement
{
    public class EntityMovementComponent : MutatronicBehaviour
    {
        [SerializeField]
        private List<GameCondition> _moveBlockerGameCondition;

        [SerializeField]
        private MovementLogicFactory _movementLogicFactory;

        [SerializeField]
        private MovementLogicPointer _movementLogicPointer;

        private IMovementLogic _movementLogic;

        public IMovementLogic MovementLogic => _movementLogic;

        public enum MovementType
        {
            MoveRight,
            MoveLeft,
            MoveForward,
            MoveBackwards,
            MoveUp,
            MoveDown,
            RotateRight,
            RotateLeft,
            RotateUp,
            RotateDown
        }

        //TODO: Add movement modifiers


        protected void Awake()
        {
            _movementLogic = new EntityMovementLogicTopDownArcade();
            _movementLogic.Init(this);
        }


        private void OnEnable()
        {
            _movementLogic.Resume();
        }


        private void OnDisable()
        {
            _movementLogic.Pause();
        }


        private void OnDestroy()
        {
            _movementLogic.Stop();
            _movementLogic.Dispose();
        }


        public void MoveInDirection(MovementType movementType)
        {
            _movementLogic.MoveInDirection(movementType);
        }


        public void MoveToTarget<T>(MovementTarget<T> target)
        {
            _movementLogic.MoveToTarget<T>();
        }


        public void Stop()
        {
            _movementLogic.Stop();
        }


        public void Pause()
        {
            _movementLogic.Pause();
        }


        public void SetMovementLogic(MovementLogicPointer pointer)
        {
            if (_movementLogic != null)
            {
                _movementLogic.Stop();
                _movementLogic.Dispose();
            }
        }
    }
}