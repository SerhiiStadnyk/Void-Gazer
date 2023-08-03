using System;
using UnityEngine;

namespace Modules.Game.Scripts.EntityMovement
{
    public class EntityMovementLogicTopDownArcade: IMovementLogic
    {
        private EntityMovementComponent _entityMovementComponent;
        private Transform _entityTransform;


        void IMovementLogic.Init(EntityMovementComponent entityMovementComponent)
        {
            _entityMovementComponent = entityMovementComponent;
            _entityTransform = _entityMovementComponent.transform;
        }


        void IMovementLogic.MoveInDirection(EntityMovementComponent.MovementType moveType)
        {
            switch (moveType)
            {
                case EntityMovementComponent.MovementType.RotateRight:
                    _entityTransform.Rotate(Vector3.forward, -100 * Time.deltaTime);
                    break;
                case EntityMovementComponent.MovementType.RotateLeft:
                    _entityTransform.Rotate(Vector3.forward, 100 * Time.deltaTime);
                    break;
                case EntityMovementComponent.MovementType.MoveForward:
                    _entityTransform.position += _entityTransform.up.normalized * 10 * Time.deltaTime;
                    break;
                case EntityMovementComponent.MovementType.MoveBackwards:
                    _entityTransform.position -= _entityTransform.up.normalized * 10 * Time.deltaTime;
                    break;
            }
        }


        void IMovementLogic.MoveToTarget<T>()
        {
        }


        void IMovementLogic.Stop()
        {
        }


        void IMovementLogic.Pause()
        {
        }


        void IMovementLogic.Resume()
        {
        }


        void IDisposable.Dispose()
        {
        }
    }
}