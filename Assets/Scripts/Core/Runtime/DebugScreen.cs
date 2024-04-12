using System;
using System.Collections.Generic;
using Core.Runtime.Forms;
using Core.Runtime.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Zenject;

namespace Core.Runtime
{
    public class DebugScreen : BaseScreen, IInitable, IDisposable
    {
        [SerializeField]
        private InputActionAsset _playerControls;

        [SerializeField]
        private List<ItemForm> _items;

        [SerializeField]
        private List<SceneReference> _sceneRefs;

        [SerializeField]
        private GameObject _uiPrefab;

        [SerializeField]
        private Transform _itemsContainer;

        [SerializeField]
        private Transform _scenesContainer;

        private ItemForm _item;
        private InputAction _spawnAtAction;
        private InputAction _cancelAction;

        private Instantiator _instantiator;
        private AppTransitionHandler _appTransitionHandler;


        [Inject]
        public void Inject(Instantiator instantiator, AppTransitionHandler appTransitionHandler)
        {
            _instantiator = instantiator;
            _appTransitionHandler = appTransitionHandler;
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
            _spawnAtAction?.Disable();
            _cancelAction?.Disable();
            base.OpenScreen();
        }


        public void OpenSpawnItemScreen()
        {
            foreach (ItemForm item in _items)
            {
                GameObject obj = _instantiator.Instantiate(_uiPrefab, _itemsContainer);
                obj.GetComponent<Button>().onClick.AddListener(() => ChosePrefab(item));
                obj.GetComponentInChildren<TMP_Text>().text = item.FormId;
            }
        }


        public void OpenChangeSceneScreen()
        {
            foreach (SceneReference sceneReference in _sceneRefs)
            {
                GameObject obj = _instantiator.Instantiate(_uiPrefab, _scenesContainer);
                obj.GetComponent<Button>().onClick.AddListener(() => ChangeScene(sceneReference));
                obj.GetComponentInChildren<TMP_Text>().text = sceneReference.name;
            }
        }


        private void ChangeScene(SceneReference sceneReference)
        {
            _appTransitionHandler.SwitchScene(sceneReference);
        }


        public void CleanScreens()
        {
            foreach (Transform child in _itemsContainer)
            {
                _instantiator.Dispose(child.gameObject);
            }

            foreach (Transform child in _scenesContainer)
            {
                _instantiator.Dispose(child.gameObject);
            }
        }


        public override void CloseScreen()
        {
            base.CloseScreen();
            CleanScreens();
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
}
