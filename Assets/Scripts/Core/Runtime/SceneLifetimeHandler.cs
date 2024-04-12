using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Core.Runtime
{
    public class SceneLifetimeHandler : MonoBehaviour, IDisposable
    {
        private List<IDisposable> _disposables;
        private List<ISceneObjectsInit> _sceneObjectsInits;

        private SceneLifetimeHandlersContainer _sceneLifetimeHandlersContainer;

        public enum SceneClosingType
        {
            Discard,
            Unload
        }

        public event Action<SceneClosingType> SceneClosing;


        [Inject]
        public void Inject(SceneLifetimeHandlersContainer sceneLifetimeHandlersContainer)
        {
            _sceneLifetimeHandlersContainer = sceneLifetimeHandlersContainer;
        }


        void IDisposable.Dispose()
        {
            _sceneLifetimeHandlersContainer.UnregisterLifetimeHandler(this);
        }


        protected void Awake()
        {
            _sceneObjectsInits = GetComponents<ISceneObjectsInit>().ToList();
            _sceneLifetimeHandlersContainer.RegisterLifetimeHandler(this);

            _disposables = new List<IDisposable>();
            var initables = new List<IInitable>();

            Component[] components = GetComponentsInChildren<Component>(true);
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

            IDisposable[] disposables = obj.GetComponentsInChildren<IDisposable>(true);
            foreach (IDisposable disposable in disposables)
            {
                _disposables.Add(disposable);
            }

            foreach (ISceneObjectsInit sceneObjectsInit in _sceneObjectsInits)
            {
                sceneObjectsInit.InitObject(obj);
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

            foreach (ISceneObjectsInit sceneObjectsInit in _sceneObjectsInits)
            {
                sceneObjectsInit.DisposeObject(obj);
            }
        }


        public void CloseScene(SceneClosingType closingType)
        {
            SceneClosing?.Invoke(closingType);

            gameObject.SetActive(false);
            foreach (IDisposable disposable in _disposables)
            {
                disposable.Dispose();
            }
        }
    }
}