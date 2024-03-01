using System.Collections.Generic;
using Forms;
using UnityEngine;
using Zenject;

public class ObjectLifetimeHandler : MonoBehaviour
{
    private SaveManager _saveManager;


    [Inject]
    public void Inject(SaveManager saveManager)
    {
        _saveManager = saveManager;
    }


    private void Awake()
    {
        List<IInitable> initables = new List<IInitable>();

        Component[] components = FindObjectsByType<Component>(FindObjectsSortMode.None);
        foreach (Component component in components)
        {
            if (component is IInitable initable)
            {
                initables.Add(initable);
            }

            if (component is ISaveable saveable)
            {
                _saveManager.RegisterSaveable(saveable);
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

        ISaveable[] saveables = obj.GetComponentsInChildren<ISaveable>(true);
        foreach (ISaveable saveable in saveables)
        {
            _saveManager.RegisterSaveable(saveable);
        }
    }


    public void DisposeObject(BaseFormInstance formInstance)
    {
        ISaveable[] saveables = formInstance.GetComponentsInChildren<ISaveable>(true);
        foreach (ISaveable saveable in saveables)
        {
            _saveManager.UnregisterSaveable(saveable);
        }
    }
}