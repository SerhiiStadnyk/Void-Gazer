using UnityEngine;
using Zenject;

public class LoadingScreen : BaseScreen
{
    [SerializeField]
    private Transform _container;

    [SerializeField]
    private UISelectableElement _saveFileUiPrefab;

    [SerializeField]
    private SceneReference _sceneReference;

    private SaveFile _selectedSaveFile;
    private UISelectableElement _selectedElement;

    private SaveManager _saveManager;
    private Instantiator _instantiator;
    private AppTransitionHandler _transitionHandler;


    [Inject]
    public void Inject(SaveManager saveManager, Instantiator instantiator, AppTransitionHandler transitionHandler)
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
        _selectedElement = null;

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
            UISelectableElement element = _instantiator.Instantiate(_saveFileUiPrefab.gameObject, _container).GetComponent<UISelectableElement>();
            element.SetupElement(() => OnSaveSelected(saveFile, element));
        }
    }


    private void OnSaveSelected(SaveFile saveFile, UISelectableElement element)
    {
        if (_selectedElement != element)
        {
            if (_selectedSaveFile != null)
            {
                _selectedElement.SetActive(false);
            }

            _selectedElement = element;
            _selectedElement.SetActive(true);

            _selectedSaveFile = saveFile;
        }
    }
}
