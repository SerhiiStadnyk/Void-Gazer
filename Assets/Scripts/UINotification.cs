using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UINotification : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _text;

    [SerializeField]
    private Image _image;

    [SerializeField]
    private string _textFormat;


    public void StartNotification(int quantity, Sprite iconSprite)
    {
        _text.text = String.Format(_textFormat, quantity);
        _image.sprite = iconSprite;

        gameObject.SetActive(true);
    }
}