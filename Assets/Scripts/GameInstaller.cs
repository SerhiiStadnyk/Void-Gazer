using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField]
    private PlayerInputHandler _playerInputHandler;

    [SerializeField]
    private AudioHandler _audioHandler;

    [SerializeField]
    private UINotificationHandler _uiNotificationHandler;

    [SerializeField]
    private InventoryScreen _inventoryScreen;

    [SerializeField]
    private ObjectLifetimeHandler _objectLifetimeHandler;

    [SerializeField]
    private Instantiator _instantiator;


    public override void InstallBindings()
    {
        Container.BindInstance(_playerInputHandler).AsSingle();
        Container.BindInstance(_audioHandler).AsSingle();
        Container.BindInstance(_uiNotificationHandler).AsSingle();
        Container.BindInstance(_inventoryScreen).AsSingle();
        Container.BindInstance(_objectLifetimeHandler).AsSingle();
        Container.BindInstance(_instantiator).AsSingle();

        Container.BindInstance(new PlayerReference()).AsSingle();
    }
}
