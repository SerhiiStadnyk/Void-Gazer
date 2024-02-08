using Forms;
using UnityEngine;
using Utility;
using Zenject;

public class PlayerInteractionHandler : MonoBehaviour
{
    private ActorFormInstance _actor;

    private PlayerInputHandler _inputHandler;
    private AudioHandler _audioHandler;
    private UINotificationHandler _uiNotificationHandler;

    [Inject]
    public void Inject(PlayerInputHandler inputHandler, AudioHandler audioHandler, UINotificationHandler uiNotificationHandler)
    {
        _inputHandler = inputHandler;
        _audioHandler = audioHandler;
        _uiNotificationHandler = uiNotificationHandler;
    }


    protected void Awake()
    {
        _actor = GetComponent<ActorFormInstance>();
    }


    protected void Start()
    {
        _inputHandler.OnInteract += _actor.InteractWith;
        _actor.ActorInventory.OnItemAdded += ItemPickupEffects;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(UtilityTermMap.Interactable))
        {
            Debug.LogWarning("Item in range");
        }
    }


    private void ItemPickupEffects(ItemFormInstance itemInstance)
    {
        _audioHandler.PlayAtPosition(itemInstance.transform.position, itemInstance.Form.PickUpSound);
        _uiNotificationHandler.ShowNotification(itemInstance, _actor.transform.position);
    }
}
