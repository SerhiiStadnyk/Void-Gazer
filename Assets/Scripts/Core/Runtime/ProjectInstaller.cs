using Core.Runtime.Maps;
using UnityEngine;
using Zenject;

namespace Core.Runtime
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField]
        private AppTransitionHandler _appTransitionHandler;

        [SerializeField]
        private SaveManager _saveManager;

        [SerializeField]
        private SceneLifetimeHandler _sceneLifetimeHandler;

        [SerializeField]
        private IdManager _idManager;

        [SerializeField]
        private FormsCollection _formsCollection;


        public override void InstallBindings()
        {
            Container.BindInstance(_appTransitionHandler).AsSingle();
            Container.BindInstance(_saveManager).AsSingle();
            Container.BindInstance(_sceneLifetimeHandler).AsSingle();
            Container.BindInstance(_idManager).AsSingle();
            Container.BindInstance(_formsCollection).AsSingle();

            Container.BindInstance(new SceneLifetimeHandlersContainer()).AsSingle();
        }
    }
}
