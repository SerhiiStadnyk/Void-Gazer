using System.Collections.Generic;
using Forms;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InventoryScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject _uiItemRefPrefab;

    [SerializeField]
    private Transform _container;

    [SerializeField]
    private Button _dropButton;

    private PlayerReference _playerReference;
    private Instantiator _instantiator;

    private UIItemRef _chosenItemRef;
    private List<UIItemRef> _uiItemRefs = new List<UIItemRef>();
    private Inventory _inventory;

    [Inject]
    public void Inject(PlayerReference playerReference, Instantiator instantiator)
    {
        _playerReference = playerReference;
        _instantiator = instantiator;
    }


    public void OpenInventory()
    {
        _inventory = _playerReference.Player.ActorInventory;
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
            _dropButton.gameObject.SetActive(false);

            foreach (KeyValuePair<ItemForm, int> item in _inventory.Items)
            {
                UIItemRef uiItem = _instantiator.Instantiate(_uiItemRefPrefab, _container).GetComponent<UIItemRef>();
                uiItem.SetupItemRef(_inventory, item.Key, OnItemClicked);
                _uiItemRefs.Add(uiItem);
            }
        }
        else
        {
            gameObject.SetActive(false);
            _uiItemRefs.Clear();
            foreach (Transform child in _container.transform)
            {
                _instantiator.Dispose(child.gameObject);
            }
        }
    }


    public void DropItem()
    {
        _inventory.DropItem(_chosenItemRef.Item, 1);
        UpdateRef(_chosenItemRef);
        if (_chosenItemRef == null)
        {
            _dropButton.gameObject.SetActive(false);
        }
    }


    private void UpdateRef(UIItemRef itemRef)
    {
        itemRef.UpdateData();
        if (_inventory.GetItemCount(itemRef.Item) <= 0)
        {
            _instantiator.Dispose(_chosenItemRef.gameObject);
            _chosenItemRef = null;
        }
    }


    private void OnItemClicked(UIItemRef itemRef)
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
            _dropButton.gameObject.SetActive(true);
        }
    }
}
