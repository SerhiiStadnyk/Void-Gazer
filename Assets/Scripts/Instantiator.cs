using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class Instantiator : MonoBehaviour
{
    [SerializeField]
    private Transform _defaultContainer;

    private DiContainer _container;
    private ObjectLifetimeHandler _objectLifetimeHandler;


    [Inject]
    public void Inject(DiContainer container, ObjectLifetimeHandler objectLifetimeHandler)
    {
        _container = container;
        _objectLifetimeHandler = objectLifetimeHandler;
    }


    public GameObject InstantiateAtPointer(GameObject prefab)
    {
        Vector3 pointerPos = Mouse.current.position.ReadValue();
        pointerPos = Camera.main.ScreenToWorldPoint(pointerPos);
        pointerPos.y = 0;
        return Instantiate(prefab, pointerPos, Quaternion.identity, _defaultContainer);
    }


    public GameObject Instantiate(GameObject prefab)
    {
        return Instantiate(prefab, Vector3.zero, Quaternion.identity, _defaultContainer);
    }


    public GameObject Instantiate(GameObject prefab, Transform parent)
    {
        return Instantiate(prefab, Vector3.zero, Quaternion.identity, parent);
    }


    public GameObject Instantiate(GameObject prefab, Vector3 pos)
    {
        return Instantiate(prefab, pos, Quaternion.identity, _defaultContainer);
    }


    public GameObject Instantiate(GameObject prefab, Vector3 pos, Quaternion rotation, Transform parent)
    {
        GameObject obj = GameObject.Instantiate(prefab, pos, rotation, parent);
        _container.Inject(obj);

        _objectLifetimeHandler.InitObject(obj);

        return obj;
    }
}