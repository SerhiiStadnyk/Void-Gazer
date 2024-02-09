using System;
using UnityEngine;

namespace GlobalEvents
{
    [CreateAssetMenu(fileName = "GlobalEvent_Action", menuName = "Game/Global Events/Event Action", order = 1)]
    public class GlobalEvent : ScriptableObject
    {
        public event Action OnTrigger;

        public void TriggerEvent()
        {
            OnTrigger?.Invoke();
        }
    }
}