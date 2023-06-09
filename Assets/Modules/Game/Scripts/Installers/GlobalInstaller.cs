using Modules.MutatronicCore.Scripts.Runtime.Scene;
using UnityEngine;
using Zenject;

namespace Modules.Game.Scripts.Installers
{
    public class GlobalInstaller : MonoInstaller
    {
        [SerializeField]
        private SceneHandler _sceneHandler;


        public override void InstallBindings()
        {
            Container.Bind<SceneHandler>().FromInstance(_sceneHandler).AsSingle();
        }
    }
}
