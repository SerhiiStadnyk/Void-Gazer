using System;
using UnityEngine;
using UnityEngine.UI;

public class UISelectableElementDefault : MonoBehaviour
{
    [SerializeField]
    protected Image _backgroundImage;

    [SerializeField]
    protected Button _button;

    [SerializeField]
    protected Color _activeColor;

    [SerializeField]
    protected Color _inactiveColor;


    public virtual void InitElement(Action callback)
    {
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() => callback?.Invoke());
    }


    public virtual void SetActive(bool value)
    {
        _backgroundImage.color = value ? _activeColor : _inactiveColor;
    }
}