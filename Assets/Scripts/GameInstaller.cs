using UnityEngine;
using UnityEngine.Serialization;
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


    public override void InstallBindings()
    {
        Container.BindInstance(_playerInputHandler);
        Container.BindInstance(_audioHandler);
        Container.BindInstance(_uiNotificationHandler);
        Container.BindInstance(_inventoryScreen);

        Container.BindInstance(new PlayerReference());
    }
}
