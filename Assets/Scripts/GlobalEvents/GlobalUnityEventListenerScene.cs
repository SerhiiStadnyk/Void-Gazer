using UnityEngine;
using UnityEngine.Events;

namespace GlobalEvents
{
    public class GlobalUnityEventListenerScene : MonoBehaviour, IInitable
    {
        [SerializeField]
        private GlobalEventScene _globalEvent;

        [SerializeField]
        private UnityEvent<SceneReference> _unityEvent;


        void IInitable.Init()
        {
            _globalEvent.OnTrigger += OnEvent;
        }


        private void OnEvent(SceneReference sceneRef)
        {
            _unityEvent.Invoke(sceneRef);
        }
    }
}