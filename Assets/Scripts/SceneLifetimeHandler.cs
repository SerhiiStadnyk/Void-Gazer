using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class SceneLifetimeHandler : MonoBehaviour, IDisposable
{
    private List<ISaveable> _saveables;
    private List<IDisposable> _disposables;

    private SaveManager _saveManager;
    private SceneLifetimeHandlersContainer _sceneLifetimeHandlersContainer;

    public List<ISaveable> Saveables => _saveables;


    [Inject]
    public void Inject(SaveManager saveManager, SceneLifetimeHandlersContainer sceneLifetimeHandlersContainer)
    {
        _saveManager = saveManager;
        _sceneLifetimeHandlersContainer = sceneLifetimeHandlersContainer;
    }


    void IDisposable.Dispose()
    {
        Debug.LogWarning("Dispose scene");
        if (Saveables != null && Saveables.Count > 0)
        {
            Saveables.Clear();
            _saveManager.UnregisterLifetimeHandler(this);
        }
    }


    private void Awake()
    {
        _sceneLifetimeHandlersContainer.RegisterLifetimeHandler(this);

        _saveables = new List<ISaveable>();
        _disposables = new List<IDisposable>();
        List<IInitable> initables = new List<IInitable>();

        Component[] components = FindObjectsByType<Component>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (Component component in components)
        {
            if (component is IInitable initable)
            {
                initables.Add(initable);
            }

            if (component is IDisposable disposable)
            {
                _disposables.Add(disposable);
            }

            if (component is ISaveable saveable)
            {
                Saveables.Add(saveable);
            }
        }

        foreach (IInitable initable in initables)
        {
            initable.Init();
        }

        if (Saveables != null && Saveables.Count > 0)
        {
            _saveManager.RegisterLifetimeHandler(this);
            _saveManager.LoadSceneData(_saveables);
        }
    }


    public void InitObject(GameObject obj)
    {
        IInitable[] initables = obj.GetComponentsInChildren<IInitable>(true);
        foreach (IInitable initable in initables)
        {
            initable.Init();
        }

        IDisposable[] disposables = obj.GetComponentsInChildren<IDisposable>(true);
        foreach (IDisposable disposable in disposables)
        {
            _disposables.Add(disposable);
        }

        ISaveable[] saveables = obj.GetComponentsInChildren<ISaveable>(true);
        foreach (ISaveable saveable in saveables)
        {
            Saveables.Add(saveable);
        }
    }


    public void DisposeScene(Scene scene)
    {
        if (scene == gameObject.scene)
        {
            foreach (IDisposable disposable in _disposables)
            {
                disposable.Dispose();
            }
        }
    }


    public void DisposeObject(GameObject obj)
    {
        IDisposable[] disposables = obj.GetComponentsInChildren<IDisposable>(true);
        foreach (IDisposable disposable in disposables)
        {
            disposable.Dispose();
            _disposables.Remove(disposable);
        }

        ISaveable[] saveables = obj.GetComponentsInChildren<ISaveable>(true);
        foreach (ISaveable saveable in saveables)
        {
            Saveables.Remove(saveable);
        }
    }
}