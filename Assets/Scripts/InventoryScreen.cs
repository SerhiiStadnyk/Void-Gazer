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
    private UIItemRef _chosenItemRef;

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
                uiItem.SetupItemRef(inventory, item.Key, OnItemClocked);
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


    private void OnItemClocked(UIItemRef itemRef)
    {
        SetActiveItemRef(itemRef);
        _chosenItemRef = itemRef;
    }


    private void SetActiveItemRef(UIItemRef newItemRef)
    {
        if (newItemRef != _chosenItemRef)
        {
            if (_chosenItemRef != null)
            {
                _chosenItemRef.SetActive(false);
            }
            newItemRef.SetActive(true);
        }
    }
}
