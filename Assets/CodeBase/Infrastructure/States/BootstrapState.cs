using System;
using CodeBase.Infrastructure.Services;
using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class BootstrapState : IState
    {
        private const string Initial = "InitialScene";
        private readonly GameStateMachine _stateMachine;
        
        private SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }
        

        public void Enter()
        {
            RegisterServices();
            _sceneLoader.LoadScene(Initial, EnterLoadLevel);
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadLevelState,string>("Main");
        }
        private void RegisterServices()
        {
            Game.InputServiceInstance = new InputService();
            AllServices.Container.RegisterSingle<IGameFactory>(new GameFactory(AllServices.Container.Single<IAssetProvider>()));
        }

        public void Exit()
        {
            
        }
    }
}