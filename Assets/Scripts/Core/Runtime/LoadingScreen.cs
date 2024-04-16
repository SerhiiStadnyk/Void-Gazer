using UnityEngine;
using Zenject;

namespace Core.Runtime
{
    public class LoadingScreen : BaseScreen
    {
        [SerializeField]
        private Transform _container;

        [SerializeField]
        private UISelectableElementDefault _saveFileUiPrefab;

        [SerializeField]
        private SceneReference _sceneReference;

        private SaveFile _selectedSaveFile;
        private UISelectableElementDefault _selectedElementDefault;

        private SaveManager _saveManager;
        private Core.Runtime.Instantiator _instantiator;
        private SceneTransitionHandler _transitionHandler;


        [Inject]
        public void Inject(SaveManager saveManager, Core.Runtime.Instantiator instantiator, SceneTransitionHandler transitionHandler)
        {
            _saveManager = saveManager;
            _instantiator = instantiator;
            _transitionHandler = transitionHandler;
        }


        public override void OpenScreen()
        {
            UpdateScreen();
            base.OpenScreen();
        }


        public override void CloseScreen()
        {
            base.CloseScreen();
            CleanScreen();
        }


        public void DeleteSave()
        {
            //TODO: Add confirmation screen
            if (_selectedSaveFile != null)
            {
                _saveManager.DeleteSaveFile(_selectedSaveFile);
            }

            UpdateScreen();
        }


        public void Load()
        {
            if (_selectedSaveFile != null)
            {
                _saveManager.InitLoading(_selectedSaveFile);
                _transitionHandler.LoadScene(_sceneReference);
            }
        }


        private void CleanScreen()
        {
            _selectedSaveFile = null;
            _selectedElementDefault = null;

            foreach (Transform child in _container)
            {
                _instantiator.Dispose(child.gameObject);
            }
        }


        private void UpdateScreen()
        {
            CleanScreen();
            foreach (SaveFile saveFile in _saveManager.SaveFiles)
            {
                UISelectableElementDefault elementDefault = _instantiator.Instantiate(_saveFileUiPrefab.gameObject, _container).GetComponent<UISelectableElementDefault>();
                elementDefault.InitElement(() => OnSaveSelected(saveFile, elementDefault));
            }
        }


        private void OnSaveSelected(SaveFile saveFile, UISelectableElementDefault elementDefault)
        {
            if (_selectedElementDefault != elementDefault)
            {
                if (_selectedSaveFile != null)
                {
                    _selectedElementDefault.SetActive(false);
                }

                _selectedElementDefault = elementDefault;
                _selectedElementDefault.SetActive(true);

                _selectedSaveFile = saveFile;
            }
        }
    }
}
