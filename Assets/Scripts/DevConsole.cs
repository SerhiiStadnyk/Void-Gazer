using System.Collections.Generic;
using Forms;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Utility;
using Zenject;

public class DevConsole : MonoBehaviour
{
    [SerializeField]
    private GameObject _console;

    [SerializeField]
    private InputActionAsset _playerControls;

    [SerializeField]
    private List<ItemForm> _items;

    [SerializeField]
    private GameObject _uiPrefab;

    [SerializeField]
    private Transform _container;

    private ItemForm _item;
    private InputAction _openConsoleAction;
    private InputAction _spawnAtAction;
    private InputAction _cancelAction;

    private Instantiator _instantiator;


    [Inject]
    public void Inject(Instantiator instantiator)
    {
        _instantiator = instantiator;
    }


    private void Awake()
    {
        _openConsoleAction = _playerControls.FindActionMap(UtilityTermMap.Debug).FindAction(UtilityTermMap.DevConsole);
        _spawnAtAction = _playerControls.FindActionMap(UtilityTermMap.Debug).FindAction(UtilityTermMap.DebugSpawnAt);
        _cancelAction = _playerControls.FindActionMap(UtilityTermMap.Debug).FindAction(UtilityTermMap.DebugCancel);
        RegisterInputActions();
    }


    private void OnEnable()
    {
        _openConsoleAction.Enable();
    }

    private void OnDisable()
    {
        _openConsoleAction.Disable();
        _spawnAtAction.Disable();
        _cancelAction.Disable();
    }


    public void ChosePrefab(ItemForm item)
    {
        OpenConsole(new InputAction.CallbackContext());
        _item = item;

        _spawnAtAction.Enable();
        _cancelAction.Enable();
    }


    private void RegisterInputActions()
    {
        _openConsoleAction.performed += OpenConsole;
        _spawnAtAction.performed += SpawnObject;
        _cancelAction.performed += CancelActionDebug;
    }


    private void OpenConsole(InputAction.CallbackContext callbackContext)
    {
        if (!_console.activeSelf)
        {
            foreach (ItemForm item in _items)
            {
                GameObject obj = _instantiator.Instantiate(_uiPrefab, _container);
                obj.GetComponent<Button>().onClick.AddListener(() => ChosePrefab(item));
                obj.GetComponentInChildren<TMP_Text>().text = item.FormName;
            }
            _console.SetActive(true);
            _spawnAtAction.Disable();
            _cancelAction.Disable();
        }
        else
        {
            foreach (Transform child in _container.transform)
            {
                _instantiator.Dispose(child.gameObject);
            }
            _console.SetActive(false);
        }
    }


    private void SpawnObject(InputAction.CallbackContext callbackContext)
    {
        _instantiator.InstantiateAtPointer(_item);
    }


    private void CancelActionDebug(InputAction.CallbackContext callbackContext)
    {
        _spawnAtAction.Disable();
        _cancelAction.Disable();
    }
}
