using System;
using UnityEngine;

namespace GlobalEvents
{
    [CreateAssetMenu(fileName = "GlobalEvent_Bool", menuName = "Game/Global Events/Event Bool", order = 1)]
    public class GlobalEventBool : ScriptableObject
    {
        public event Action<bool> OnTrigger;

        public void TriggerEvent(bool value)
        {
            OnTrigger?.Invoke(value);
        }
    }
}