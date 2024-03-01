using System;
using UnityEngine;
using UnityEngine.UI;

public class UISelectableElement : MonoBehaviour
{
    [SerializeField]
    private Image _backgroundImage;

    [SerializeField]
    private Button _button;

    [SerializeField]
    private Color _activeColor;

    [SerializeField]
    private Color _inactiveColor;


    public virtual void SetupElement(Action callback)
    {
        _button.onClick.AddListener(() => callback?.Invoke());
    }


    public virtual void SetActive(bool value)
    {
        _backgroundImage.color = value ? _activeColor : _inactiveColor;
    }
}
