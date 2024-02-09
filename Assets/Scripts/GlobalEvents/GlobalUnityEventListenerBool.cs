using UnityEngine;
using UnityEngine.Events;

namespace GlobalEvents
{
    public class GlobalUnityEventListenerBool : MonoBehaviour, IInitable
    {
        [SerializeField]
        private GlobalEventBool _globalEvent;

        [SerializeField]
        private UnityEvent<bool> _unityEvent;


        void IInitable.Init()
        {
            _globalEvent.OnTrigger += OnEvent;
        }


        private void OnEvent(bool value)
        {
            _unityEvent.Invoke(value);
        }
    }
}