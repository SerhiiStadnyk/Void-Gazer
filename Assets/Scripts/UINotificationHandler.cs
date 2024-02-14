using System.Collections.Generic;
using System.Linq;
using Forms;
using UnityEngine;
using Zenject;

public class UINotificationHandler : MonoBehaviour
{
    [SerializeField]
    private Camera _uiCamera;

    [SerializeField]
    private GameObject _uiNotificationPrefab;

    [SerializeField]
    private int _notificationPoolSize;

    private const float _notificationRotationX = 90;

    private List<UINotification> _notificationPool;

    private Instantiator _instantiator;


    [Inject]
    public void Inject(Instantiator instantiator)
    {
        _instantiator = instantiator;
    }


    private void Awake()
    {
        _notificationPool = new List<UINotification>(_notificationPoolSize);
        for (int i = 0; i < _notificationPoolSize; i++)
        {
            UINotification notification = _instantiator.Instantiate(_uiNotificationPrefab, transform).GetComponent<UINotification>();
            _notificationPool.Add(notification);
            notification.transform.Rotate(_notificationRotationX, 0f, 0f);
            notification.GetComponentInChildren<Canvas>().worldCamera = _uiCamera;
            notification.gameObject.SetActive(false);
        }
    }


    public void ShowNotification(ItemFormInstance item, Vector3 position)
    {
        UINotification pooledNotification = _notificationPool.FirstOrDefault(notification => !notification.gameObject.activeInHierarchy);
        if (pooledNotification != null)
        {
            pooledNotification.transform.position = position;
            pooledNotification.StartNotification(item.Quantity, item.Form.ItemIcon);
        }
    }
}