using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField]
    private PlayerInputHandler _playerInputHandler;


    public override void InstallBindings()
    {
        Container.BindInstance(_playerInputHandler);
    }
}
