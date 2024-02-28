using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField]
    private AppTransitionHandler _appTransitionHandler;


    public override void InstallBindings()
    {
        Container.BindInstance(_appTransitionHandler).AsSingle();
    }
}
