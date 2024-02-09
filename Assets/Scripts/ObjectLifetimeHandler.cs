using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectLifetimeHandler : MonoBehaviour
{
    private void Awake()
    {
        List<IInitable> initables = new List<IInitable>();

        // Iterate through all GameObjects in the scene
        foreach (var gameObject in Resources.FindObjectsOfTypeAll<GameObject>())
        {
            // Check each component of the GameObject
            foreach (var component in gameObject.GetComponents<Component>())
            {
                // Check if the component implements IInitable interface
                if (component is IInitable initable)
                {
                    initables.Add(initable);
                }
            }
        }

        // Initialize all components found
        foreach (var initable in initables)
        {
            initable.Init();
        }
    }
}