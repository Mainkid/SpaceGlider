using System;
using CodeBase.Camera;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Infrastructure
{
    public class LoadLevelState : IPayloadedState<string>
    {
        
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter(string sceneName)
        {
            _sceneLoader.LoadScene(sceneName,OnLoaded);
        }

        public void Exit()
        {
            throw new NotImplementedException();
        }

        private void OnLoaded()
        {
            GameObject hero = _gameFactory.CreateHero();
            CameraFollow(hero);
        }

        private void CameraFollow(GameObject gameObject) =>
            UnityEngine.Camera.main.GetComponent<CameraFollow>().Follow(gameObject);
    }
}