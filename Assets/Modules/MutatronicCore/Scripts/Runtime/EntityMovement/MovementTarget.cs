namespace Modules.MutatronicCore.Scripts.Runtime.EntityMovement
{
    public abstract class MovementTarget<T>
    {
        public T Target => _target;

        private T _target;


        protected MovementTarget(T target)
        {
            _target = target;
        }
    }
}
