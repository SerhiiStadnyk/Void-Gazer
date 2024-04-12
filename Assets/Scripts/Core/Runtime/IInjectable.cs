using Zenject;

namespace Core.Runtime
{
    public interface IInjectable
    {
        public void Inject(DiContainer container);
    }
}