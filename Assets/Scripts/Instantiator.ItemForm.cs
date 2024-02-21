using Forms;
using UnityEngine;

public partial class Instantiator
{
    [SerializeField]
    private ItemMap _itemMap;


    public GameObject Instantiate(ItemForm item, int quantity)
    {
        GameObject prefab = _itemMap.GetPrefab(item);
        return Instantiate(prefab, Vector3.zero, Quaternion.identity, _defaultContainer);
    }


    public GameObject Instantiate(ItemForm item, int quantity, Transform parent)
    {
        GameObject prefab = _itemMap.GetPrefab(item);
        return Instantiate(prefab, Vector3.zero, Quaternion.identity, parent);
    }


    public GameObject Instantiate(ItemForm item, int quantity, Vector3 pos)
    {
        GameObject prefab = _itemMap.GetPrefab(item);
        return Instantiate(prefab, pos, Quaternion.identity, _defaultContainer);
    }


    public GameObject InstantiateAtPointer(ItemForm item)
    {
        GameObject prefab = _itemMap.GetPrefab(item);
        return InstantiateAtPointerInternal(prefab);
    }
}
