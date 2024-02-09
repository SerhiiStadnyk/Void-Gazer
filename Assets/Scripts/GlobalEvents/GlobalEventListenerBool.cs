using UnityEngine;

namespace GlobalEvents
{
    public class GlobalEventListenerBool : MonoBehaviour, IInitable
    {
        [SerializeField]
        private GlobalEventBool _globalEvent;


        void IInitable.Init()
        {
            _globalEvent.OnTrigger += OnEvent;
        }


        private void OnEvent(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}