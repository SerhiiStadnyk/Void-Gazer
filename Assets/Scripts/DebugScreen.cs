using System;
using System.Collections.Generic;
using Forms;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Utility;
using Zenject;

public class DebugScreen : BaseScreen, IInitable, IDisposable
{
    [SerializeField]
    private InputActionAsset _playerControls;

    [SerializeField]
    private List<ItemForm> _items;

    [SerializeField]
    private GameObject _uiPrefab;

    [SerializeField]
    private Transform _container;

    private ItemForm _item;
    private InputAction _spawnAtAction;
    private InputAction _cancelAction;

    private Instantiator _instantiator;


    [Inject]
    public void Inject(Instantiator instantiator)
    {
        _instantiator = instantiator;
    }


    void IInitable.Init()
    {
        _spawnAtAction = _playerControls.FindActionMap(UtilityTermMap.Debug).FindAction(UtilityTermMap.DebugSpawnAt);
        _cancelAction = _playerControls.FindActionMap(UtilityTermMap.Debug).FindAction(UtilityTermMap.DebugCancel);
    }


    void IDisposable.Dispose()
    {
        UnregisterInputActions();
    }


    public void ChosePrefab(ItemForm item)
    {
        CloseScreen();
        _item = item;

        RegisterInputActions();
        _spawnAtAction.Enable();
        _cancelAction.Enable();
    }


    private void RegisterInputActions()
    {
        _spawnAtAction.performed += SpawnObject;
        _cancelAction.performed += CancelActionDebug;
    }


    private void UnregisterInputActions()
    {
        _spawnAtAction.performed -= SpawnObject;
        _cancelAction.performed -= CancelActionDebug;
    }


    public override void OpenScreen()
    {
        foreach (ItemForm item in _items)
        {
            GameObject obj = _instantiator.Instantiate(_uiPrefab, _container);
            obj.GetComponent<Button>().onClick.AddListener(() => ChosePrefab(item));
            obj.GetComponentInChildren<TMP_Text>().text = item.FormName;
        }
        _spawnAtAction?.Disable();
        _cancelAction?.Disable();
        base.OpenScreen();
    }


    public override void CloseScreen()
    {
        base.CloseScreen();
        foreach (Transform child in _container.transform)
        {
            _instantiator.Dispose(child.gameObject);
        }
    }


    private void SpawnObject(InputAction.CallbackContext callbackContext)
    {
        _instantiator.InstantiateAtPointer(_item);
    }


    private void CancelActionDebug(InputAction.CallbackContext callbackContext)
    {
        UnregisterInputActions();
        _spawnAtAction.Disable();
        _cancelAction.Disable();
    }
}
