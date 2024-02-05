using Movement;
using UnityEngine;
using Zenject;

public class PlayerMovementController : MonoBehaviour
{
    private ActorMovementController _actorMovementController;

    private PlayerInputHandler _inputHandler;


    [Inject]
    public void Inject(PlayerInputHandler inputHandler)
    {
        _inputHandler = inputHandler;
    }


    protected void Start()
    {
        _actorMovementController = GetComponent<ActorMovementController>();
    }


    private void Update()
    {
        if (_inputHandler.MoveInput.y > 0)
        {
            _actorMovementController.MoveForward();
        }
    }
}