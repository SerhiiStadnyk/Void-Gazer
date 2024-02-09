using System;
using Forms;
using GlobalEvents;
using UnityEngine;
using Zenject;

public class PlayerInteractionHandler : MonoBehaviour
{
    [SerializeField]
    private GlobalEventBool _interactableApproachedEvent;

    [SerializeField]
    private float _interactionRadius = 1f;

    [SerializeField]
    private int _interactableLayerIndex;

    private int _interactableLayer => 1 << _interactableLayerIndex;

    private ActorFormInstance _actor;
    private bool _isInteractableInArea;

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


    private void Update()
    {
        Collider[] colliders = new Collider[10];
        int count = Physics.OverlapSphereNonAlloc(transform.position, _interactionRadius, colliders, _interactableLayer);
        bool result = false;

        for (int i = 0; i < count; i++)
        {
            if (colliders[i] != null)
            {
                IInteractable interactable = colliders[i].GetComponent<IInteractable>();
                if (interactable != null && interactable.Interactable)
                {
                    result = true;
                    break;
                }
            }
        }

        Array.Clear(colliders, 0, colliders.Length);

        if (result != _isInteractableInArea)
        {
            _isInteractableInArea = result;
            _interactableApproachedEvent.TriggerEvent(result);
        }
    }


    protected void Awake()
    {
        _actor = GetComponent<ActorFormInstance>();
    }


    private void OnEnable()
    {
        _inputHandler.OnInteract += _actor.InteractWith;
        _actor.ActorInventory.OnItemAdded += ItemPickupEffects;
    }


    private void OnDisable()
    {
        _inputHandler.OnInteract -= _actor.InteractWith;
        _actor.ActorInventory.OnItemAdded -= ItemPickupEffects;
    }


    private void ItemPickupEffects(ItemFormInstance itemInstance)
    {
        _audioHandler.PlayAtPosition(itemInstance.transform.position, itemInstance.Form.PickUpSound);
        _uiNotificationHandler.ShowNotification(itemInstance, _actor.transform.position);
    }
}
