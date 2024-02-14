using System.Collections.Generic;
using Forms;
using UnityEngine;
using Zenject;

public class InventoryScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject _uiItemRefPrefab;

    [SerializeField]
    private Transform _container;

    private PlayerReference _playerReference;
    private Instantiator _instantiator;

    [Inject]
    public void Inject(PlayerReference playerReference, Instantiator instantiator)
    {
        _playerReference = playerReference;
        _instantiator = instantiator;
    }


    public void OpenInventory()
    {
        Inventory inventory = _playerReference.Player.ActorInventory;
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);

            foreach (KeyValuePair<ItemForm, int> item in inventory.Items)
            {
                UIItemRef uiItem = _instantiator.Instantiate(_uiItemRefPrefab, _container).GetComponent<UIItemRef>();
                uiItem.SetupItemRef(inventory, item.Key);
            }
        }
        else
        {
            gameObject.SetActive(false);
            foreach (Transform child in _container.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
