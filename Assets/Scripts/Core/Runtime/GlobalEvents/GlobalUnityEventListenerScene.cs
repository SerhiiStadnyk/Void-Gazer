using System;
using UnityEngine;
using UnityEngine.Events;

namespace Core.Runtime.GlobalEvents
{
    public class GlobalUnityEventListenerScene : MonoBehaviour, IInitable, IDisposable
    {
        [SerializeField]
        private GlobalEventScene _globalEvent;

        [SerializeField]
        private UnityEvent<SceneReference> _unityEvent;


        void IInitable.Init()
        {
            _globalEvent.OnTrigger += OnEvent;
        }


        void IDisposable.Dispose()
        {
            _globalEvent.OnTrigger -= OnEvent;
        }


        private void OnEvent(SceneReference sceneRef)
        {
            _unityEvent.Invoke(sceneRef);
        }
    }
}