using System;

namespace Modules.Game.Scripts.EntityMovement
{
    public interface IMovementLogic: IDisposable
    {
        //TODO: Add default and limit values for speed and acceleration

        public void MoveInDirection(EntityMovementComponent.MovementType moveType);

        public void MoveToTarget<T>();

        public void Stop();

        public void Pause();

        public void Resume();

        public void Init(EntityMovementComponent entityMovementComponent);
    }
}
