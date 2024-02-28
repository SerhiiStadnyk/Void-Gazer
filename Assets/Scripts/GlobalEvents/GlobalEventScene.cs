using System;
using UnityEditor;
using UnityEngine;

namespace GlobalEvents
{
    [CreateAssetMenu(fileName = "GlobalEvent_Scene", menuName = "Game/Global Events/Event Scene", order = 1)]
    public class GlobalEventScene : ScriptableObject
    {
        public event Action<SceneReference> OnTrigger;

        public void TriggerEvent(SceneReference sceneRef)
        {
            OnTrigger?.Invoke(sceneRef);
        }
    }
}