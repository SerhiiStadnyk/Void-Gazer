using UnityEngine;
using Zenject;

public class SaveScreen : BaseScreen
{
    [SerializeField]
    private Transform _container;

    [SerializeField]
    private UISelectableElementDefault _saveFileUiPrefab;
    //TODO: Replace with universal choosable ui element

    private SaveFile _selectedSaveFile;
    private UISelectableElementDefault _selectedElementDefault;

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
