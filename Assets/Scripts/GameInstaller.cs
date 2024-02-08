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


    public override void InstallBindings()
    {
        Container.BindInstance(_playerInputHandler);
        Container.BindInstance(_audioHandler);
        Container.BindInstance(_uiNotificationHandler);

        Container.BindInstance(new PlayerReference());
    }
}
