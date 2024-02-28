using UnityEngine;

public class BaseScreen : MonoBehaviour
{
    //TODO: Add optional audio effects on screen opening and closing
    //TODO: Add optional animation support for screen

    public virtual void OpenScreen()
    {
        gameObject.SetActive(true);
    }


    public virtual void CloseScreen()
    {
        gameObject.SetActive(false);
    }
}