using UnityEngine;
using Zenject;

public class PlayerFollower : MonoBehaviour
{
    private Transform _transform;

    private PlayerReference _playerReference;

    [Inject]
    public void Inject(PlayerReference playerReference)
    {
        _playerReference = playerReference;
    }


    private void Start()
    {
        _transform = transform;
    }


    void Update()
    {
        Vector3 playerPos = _playerReference.Player.transform.position;
        Vector3 newPos = playerPos;
        newPos.y = transform.position.y;
        _transform.position = newPos;
    }
}