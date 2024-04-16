using Core.Runtime.Maps;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Core.Runtime
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField]
        private SceneTransitionHandler _sceneTransitionHandler;

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
            Container.BindInstance(_sceneTransitionHandler).AsSingle();
            Container.BindInstance(_saveManager).AsSingle();
            Container.BindInstance(_sceneLifetimeHandler).AsSingle();
            Container.BindInstance(_idManager).AsSingle();
            Container.BindInstance(_formsCollection).AsSingle();

            Container.BindInstance(new SceneLifetimeHandlersContainer()).AsSingle();
        }
    }
}
