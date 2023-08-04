using System;
using UnityEngine;

namespace Modules.MutatronicCore.Scripts.Runtime.EntityMovement
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


        void IMovementLogic.MoveTo(MovementTarget target)
        {
            Vector3 foo = _entityTransform.forward.normalized * target.TargetDirection.z;
            _entityTransform.position += foo * 10 * Time.deltaTime;
        }


        void IMovementLogic.RotateTo(MovementTarget target)
        {
            Vector3 foo = new Vector3(0, target.TargetDirection.x, 0);
            _entityTransform.Rotate(foo, 100 * Time.deltaTime);
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