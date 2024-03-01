using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField]
    private AppTransitionHandler _appTransitionHandler;

    [SerializeField]
    private SaveManager _saveManager;


    public override void InstallBindings()
    {
        Container.BindInstance(_appTransitionHandler).AsSingle();
        Container.BindInstance(_saveManager).AsSingle();
    }
}
