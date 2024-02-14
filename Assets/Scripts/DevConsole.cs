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
    private List<GameObject> _itemPrefabs;

    [SerializeField]
    private GameObject _uiPrefab;

    [SerializeField]
    private Transform _container;

    private GameObject _prefab;
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


    public void ChosePrefab(GameObject prefab)
    {
        OpenConsole(new InputAction.CallbackContext());
        _prefab = prefab;

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
            foreach (GameObject prefab in _itemPrefabs)
            {
                GameObject obj = _instantiator.Instantiate(_uiPrefab, _container);
                obj.GetComponent<Button>().onClick.AddListener(() => ChosePrefab(prefab));
                obj.GetComponentInChildren<TMP_Text>().text = prefab.GetComponent<ItemFormInstance>().Form.FormName;
            }
            _console.SetActive(true);
            _spawnAtAction.Disable();
            _cancelAction.Disable();
        }
        else
        {
            foreach (Transform child in _container.transform)
            {
                Destroy(child.gameObject);
            }
            _console.SetActive(false);
        }
    }


    private void SpawnObject(InputAction.CallbackContext callbackContext)
    {
        _instantiator.InstantiateAtPointer(_prefab);
    }


    private void CancelActionDebug(InputAction.CallbackContext callbackContext)
    {
        _spawnAtAction.Disable();
        _cancelAction.Disable();
    }
}
