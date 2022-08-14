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
        private readonly AllServices _services;
        
        private SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices allServices)
        {
            
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = allServices;
            RegisterServices();
        }
        

        public void Enter()
        {
            
            _sceneLoader.LoadScene(Initial, EnterLoadLevel);
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadLevelState,string>("Main");
        }
        private void RegisterServices()
        {
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IGameFactory>(new GameFactory(AllServices.Container.Single<IAssetProvider>()));
            _services.RegisterSingle<IInputService>(new InputService());
            Debug.Log("Registration Finished");
        }

        public void Exit()
        {
            
        }
    }
}