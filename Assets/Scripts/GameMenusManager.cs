using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameMenusManager : MonoBehaviour
{
    [SerializeField]
    private BaseScreen _gameMenuScreen;

    [SerializeField]
    private List<BaseScreen> _menusScreens;


    public void OnEscape()
    {
        BaseScreen activeScreen = _menusScreens.FirstOrDefault(menu => menu.gameObject.activeInHierarchy);
        if (activeScreen == null)
        {
            _gameMenuScreen.OpenScreen();
        }
        else
        {
            activeScreen.CloseScreen();
        }
    }
}