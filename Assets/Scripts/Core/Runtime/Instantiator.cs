using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Core.Runtime
{
    public partial class Instantiator : MonoBehaviour
    {
        [SerializeField]
        private Transform _defaultContainer;

        private DiContainer _container;
        private SceneLifetimeHandler _sceneLifetimeHandler;


        [Inject]
        public void Inject(DiContainer container, SceneLifetimeHandler sceneLifetimeHandler)
        {
            _container = container;
            _sceneLifetimeHandler = sceneLifetimeHandler;
        }


        protected GameObject InstantiateAtPointerInternal(GameObject prefab, Action<GameObject> onBeforeInitObject = null)
        {
            Vector3 pointerPos = Mouse.current.position.ReadValue();
            pointerPos = Camera.main.ScreenToWorldPoint(pointerPos);
            pointerPos.y = 0;
            return InstantiateInternal(prefab, pointerPos, Quaternion.identity, _defaultContainer, onBeforeInitObject);
        }


        public GameObject Instantiate(GameObject prefab)
        {
            return InstantiateInternal(prefab, Vector3.zero, Quaternion.identity, _defaultContainer);
        }


        public GameObject Instantiate(GameObject prefab, Transform parent)
        {
            return InstantiateInternal(prefab, Vector3.zero, Quaternion.identity, parent);
        }


        public GameObject Instantiate(GameObject prefab, Vector3 pos)
        {
            return InstantiateInternal(prefab, pos, Quaternion.identity, _defaultContainer);
        }


        protected GameObject InstantiateInternal(GameObject prefab, Vector3 pos, Quaternion rotation, Transform parent, Action<GameObject> onBeforeInitObject = null)
        {
            GameObject obj = _container.InstantiatePrefab(prefab, parent);
            obj.transform.position = pos;
            obj.transform.rotation = rotation;

            onBeforeInitObject?.Invoke(obj);
            _sceneLifetimeHandler.InitObject(obj);

            return obj;
        }


        public void Dispose(GameObject obj)
        {
            _sceneLifetimeHandler.DisposeObject(obj);
            Destroy(obj);
        }
    }
}