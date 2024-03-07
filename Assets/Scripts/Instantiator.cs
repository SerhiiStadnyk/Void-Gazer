using Forms;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

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


    protected GameObject InstantiateAtPointerInternal(GameObject prefab)
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
        GameObject obj = _container.InstantiatePrefab(prefab, parent);
        obj.transform.position = pos;
        obj.transform.rotation = rotation;

        _sceneLifetimeHandler.InitObject(obj);

        return obj;
    }


    public void Dispose(BaseFormInstance formInstance)
    {
        _sceneLifetimeHandler.DisposeObject(formInstance.gameObject);
        Destroy(formInstance.gameObject);
    }


    public void Dispose(GameObject obj)
    {
        _sceneLifetimeHandler.DisposeObject(gameObject);
        Destroy(obj);
    }
}