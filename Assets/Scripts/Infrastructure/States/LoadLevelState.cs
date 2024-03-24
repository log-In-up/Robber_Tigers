using Assets.Scripts.Infrastructure.Services.UserInterface;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly IGameUI _gameUI;

        private readonly SceneLoader _sceneLoader;
        private readonly GameStateMachine _stateMachine;

        public LoadLevelState(GameStateMachine stateMachine,
            SceneLoader sceneLoader,
            IGameUI gameUI)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _gameUI = gameUI;
        }

        public void Enter(string payload)
        {
            AsyncOperation load = _sceneLoader.LoadGameLevel(payload, OnLoaded);
        }

        public void Exit()
        {
        }

        private void OnLoaded()
        {
            _stateMachine.Enter<GameLoopState>();
        }
    }
}