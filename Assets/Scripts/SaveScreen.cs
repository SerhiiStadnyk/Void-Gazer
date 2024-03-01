using UnityEngine;
using Zenject;

public class SaveScreen : BaseScreen
{
    [SerializeField]
    private Transform _container;

    [SerializeField]
    private UISelectableElement _saveFileUiPrefab;
    //TODO: Replace with universal choosable ui element

    private SaveFile _selectedSaveFile;
    private UISelectableElement _selectedElement;

    private SaveManager _saveManager;
    private Instantiator _instantiator;


    [Inject]
    public void Inject(SaveManager saveManager, Instantiator instantiator)
    {
        _saveManager = saveManager;
        _instantiator = instantiator;
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


    public void Save()
    {
        if (_selectedSaveFile == null)
        {
            //TODO: Add naming screen
            _saveManager.CreateSaveFile("123");
        }
        else
        {
            _saveManager.Save(_selectedSaveFile);
            //TODO: Add confirmation screen
        }

        UpdateScreen();
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
