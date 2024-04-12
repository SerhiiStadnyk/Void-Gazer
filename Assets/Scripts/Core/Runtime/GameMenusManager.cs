using System.Collections.Generic;
using System.Linq;
using Core.Runtime.GlobalEvents;
using Core.Runtime.Utility;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Runtime
{
    public class GameMenusManager : MonoBehaviour
    {
        [SerializeField]
        private InputActionAsset _playerControls;

        [SerializeField]
        private BaseScreen _gameMenuScreen;

        [SerializeField]
        private Transform _menuScreensContainer;

        [SerializeField]
        private Transform _gameplayScreensContainer;

        [SerializeField]
        private GlobalEvent _openInventoryInputEvent;

        [SerializeField]
        private GlobalEvent _openDebugConsoleInputEvent;

        private List<BaseScreen> _menuScreens = new List<BaseScreen>();
        private List<BaseScreen> _gameplayScreens = new List<BaseScreen>();
        private KeyValuePair<BaseScreen, ScreenGroup> _activeScreen;

        private InputAction _openInventoryAction;
        private InputAction _openGameMenuAction;
        private InputAction _openDebugScreenAction;

        private enum ScreenGroup
        {
            Default,
            Menu,
            Gameplay
        }


        private void Awake()
        {
            _menuScreens.AddRange(_menuScreensContainer.GetComponentsInChildren<BaseScreen>(true));
            _gameplayScreens.AddRange(_gameplayScreensContainer.GetComponentsInChildren<BaseScreen>(true));

            _openInventoryAction = _playerControls.FindActionMap(UtilityTermMap.Player).FindAction(UtilityTermMap.OpenInventory);
            _openGameMenuAction = _playerControls.FindActionMap(UtilityTermMap.Player).FindAction(UtilityTermMap.Escape);
            _openDebugScreenAction = _playerControls.FindActionMap(UtilityTermMap.Debug).FindAction(UtilityTermMap.DevConsole);
        }


        private void OnEnable()
        {
            _openInventoryAction.Enable();
            _openGameMenuAction.Enable();
            _openDebugScreenAction.Enable();

            RegisterInputActions();
        }


        private void OnDisable()
        {
            _openInventoryAction.Disable();
            _openGameMenuAction.Disable();
            _openDebugScreenAction.Disable();

            UnregisterInputActions();
        }


        private void RegisterInputActions()
        {
            _openInventoryAction.performed += OnOpenInventoryScreen;
            _openGameMenuAction.performed += OnEscape;
            _openDebugScreenAction.performed += OpenDebugScreen;
        }


        private void UnregisterInputActions()
        {
            _openInventoryAction.performed -= OnOpenInventoryScreen;
            _openGameMenuAction.performed -= OnEscape;
            _openDebugScreenAction.performed -= OpenDebugScreen;
        }


        private void UpdateActiveScreen()
        {
            List<BaseScreen> allScreens = _menuScreens.Concat(_gameplayScreens).ToList();
            BaseScreen activeScreen = allScreens.FirstOrDefault(screen => screen.gameObject.activeInHierarchy);
            ScreenGroup activeGroup = ScreenGroup.Default;
            if (_menuScreens.Contains(activeScreen))
            {
                activeGroup = ScreenGroup.Menu;
            }
            else if (_gameplayScreens.Contains(activeScreen))
            {
                activeGroup = ScreenGroup.Gameplay;
            }

            _activeScreen = new KeyValuePair<BaseScreen, ScreenGroup>(activeScreen, activeGroup);
        }


        private void OnEscape(InputAction.CallbackContext context)
        {
            UpdateActiveScreen();

            if (_activeScreen.Key == null)
            {
                _gameMenuScreen.OpenScreen();
            }
            else
            {
                _activeScreen.Key.CloseScreen();
            }
        }


        private void OnOpenInventoryScreen(InputAction.CallbackContext context)
        {
            UpdateActiveScreen();
            if (_activeScreen.Value != ScreenGroup.Menu)
            {
                _openInventoryInputEvent.TriggerEvent();
            }
        }


        private void OpenDebugScreen(InputAction.CallbackContext context)
        {
            UpdateActiveScreen();
            if (_activeScreen.Value != ScreenGroup.Menu)
            {
                _openDebugConsoleInputEvent.TriggerEvent();
            }
        }
    }
}