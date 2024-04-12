using System;
using UnityEngine;
using UnityEngine.Events;

namespace Core.Runtime.GlobalEvents
{
    public class GlobalUnityEventListener : MonoBehaviour, IInitable, IDisposable
    {
        [SerializeField]
        private GlobalEvent _globalEvent;

        [SerializeField]
        private UnityEvent _unityEvent;


        void IInitable.Init()
        {
            _globalEvent.OnTrigger += OnEvent;
        }


        void IDisposable.Dispose()
        {
            _globalEvent.OnTrigger -= OnEvent;
        }


        private void OnEvent()
        {
            _unityEvent.Invoke();
        }
    }
}