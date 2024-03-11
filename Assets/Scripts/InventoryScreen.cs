using System.Collections.Generic;
using Forms;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InventoryScreen : BaseScreen
{
    [SerializeField]
    private GameObject _uiItemRefPrefab;

    [SerializeField]
    private Transform _container;

    [SerializeField]
    private Button _dropButton;

    private PlayerReference _playerReference;
    private Instantiator _instantiator;

    private UISelectableItem _chosenSelectableItem;
    private Dictionary<UISelectableItem, ItemForm> _uiItemElements = new Dictionary<UISelectableItem, ItemForm>();
    private Inventory _inventory;


    [Inject]
    public void Inject(PlayerReference playerReference, Instantiator instantiator)
    {
        _playerReference = playerReference;
        _instantiator = instantiator;
    }


    public override void OpenScreen()
    {
        _inventory = _playerReference.Player.ActorInventory;
        _dropButton.gameObject.SetActive(false);

        InstantiateElements();
        UpdateElements();

        base.OpenScreen();
    }


    public override void CloseScreen()
    {
        base.CloseScreen();
        _uiItemElements.Clear();
        foreach (Transform child in _container.transform)
        {
            _instantiator.Dispose(child.gameObject);
        }
    }


    public void DropItem()
    {
        _inventory.DropItem(_uiItemElements[_chosenSelectableItem], 1);
        UpdateElements();
        if (_chosenSelectableItem == null)
        {
            _dropButton.gameObject.SetActive(false);
        }
    }


    private void InstantiateElements()
    {
        _uiItemElements.Clear();
        foreach (KeyValuePair<ItemForm, int> item in _inventory.Items)
        {
            UISelectableItem uiSelectableItem = _instantiator.Instantiate(_uiItemRefPrefab, _container).GetComponent<UISelectableItem>();
            _uiItemElements.Add(uiSelectableItem, item.Key);
        }
    }


    private void UpdateElements()
    {
        List<UISelectableItem> elementsToRemove = new List<UISelectableItem>();
        foreach (KeyValuePair<UISelectableItem, ItemForm> pair in _uiItemElements)
        {
            int itemCount = _inventory.GetItemCount(pair.Value);
            if (itemCount > 0)
            {
                pair.Key.InitElement(() => OnItemClicked(pair.Key), pair.Value.name, itemCount);
            }
            else
            {
                elementsToRemove.Add(pair.Key);
            }
        }

        foreach (UISelectableItem element in elementsToRemove)
        {
            if (_chosenSelectableItem == element)
            {
                _chosenSelectableItem = null;
            }

            _uiItemElements.Remove(element);
            _instantiator.Dispose(element.gameObject);
        }
    }


    private void OnItemClicked(UISelectableItem selectableItem)
    {
        SetActiveItemRef(selectableItem);
        _chosenSelectableItem = selectableItem;
    }


    private void SetActiveItemRef(UISelectableItem newSelectableItem)
    {
        if (newSelectableItem != _chosenSelectableItem)
        {
            if (_chosenSelectableItem != null)
            {
                _chosenSelectableItem.SetActive(false);
            }
            newSelectableItem.SetActive(true);
            _dropButton.gameObject.SetActive(true);
        }
    }
}