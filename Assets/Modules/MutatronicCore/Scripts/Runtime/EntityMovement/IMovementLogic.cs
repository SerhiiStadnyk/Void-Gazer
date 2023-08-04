using System;

namespace Modules.MutatronicCore.Scripts.Runtime.EntityMovement
{
    public interface IMovementLogic: IDisposable
    {
        //TODO: Add default and limit values for speed and acceleration

        public void MoveTo(MovementTarget target);

        public void RotateTo(MovementTarget target);

        public void Stop();

        public void Pause();

        public void Resume();

        public void Init(EntityMovementComponent entityMovementComponent);
    }
}
