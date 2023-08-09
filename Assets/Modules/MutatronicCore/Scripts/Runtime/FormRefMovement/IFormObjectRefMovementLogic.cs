using System;

namespace Modules.MutatronicCore.Scripts.Runtime.FormRefMovement
{
    public interface IFormObjectRefMovementLogic<T>: IDisposable
    {
        //TODO: Add default and limit values for speed and acceleration

        public void MoveTo(T target);

        public void RotateTo(T target);

        public void Stop();

        public void Pause();

        public void Resume();

        public void Init(FormObjectRefMovementComponentBase<T> formObjectRefMovementComponentBase);
    }
}
