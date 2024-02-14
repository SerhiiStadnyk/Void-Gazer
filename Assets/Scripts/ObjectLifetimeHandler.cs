using System.Collections.Generic;
using UnityEngine;

public class ObjectLifetimeHandler : MonoBehaviour
{
    private void Awake()
    {
        List<IInitable> initables = new List<IInitable>();

        foreach (GameObject obj in Resources.FindObjectsOfTypeAll<GameObject>())
        {
            foreach (Component component in obj.GetComponents<Component>())
            {
                if (component is IInitable initable)
                {
                    initables.Add(initable);
                }
            }
        }

        foreach (IInitable initable in initables)
        {
            initable.Init();
        }
    }


    public void InitObject(GameObject obj)
    {
        IInitable[] initables = obj.GetComponentsInChildren<IInitable>(true);
        foreach (IInitable initable in initables)
        {
            initable.Init();
        }
    }
}