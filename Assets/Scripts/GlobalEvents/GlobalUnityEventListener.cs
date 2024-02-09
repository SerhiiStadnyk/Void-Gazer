using UnityEngine;
using UnityEngine.Events;

namespace GlobalEvents
{
    public class GlobalUnityEventListener : MonoBehaviour, IInitable
    {
        [SerializeField]
        private GlobalEvent _globalEvent;

        [SerializeField]
        private UnityEvent _unityEvent;


        void IInitable.Init()
        {
            _globalEvent.OnTrigger += OnEvent;
        }


        private void OnEvent()
        {
            _unityEvent.Invoke();
        }
    }
}