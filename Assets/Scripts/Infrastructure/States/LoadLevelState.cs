using Assets.Scripts.Infrastructure.Services.PlayerBank;
using Assets.Scripts.Infrastructure.Services.UserInterface;
using Assets.Scripts.UserInterface;

namespace Assets.Scripts.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly IGameUI _gameUI;
        private readonly IPlayerBank _playerBank;
        private readonly SceneLoader _sceneLoader;
        private readonly GameStateMachine _stateMachine;

        public LoadLevelState(GameStateMachine stateMachine,
            SceneLoader sceneLoader,
            IPlayerBank playerBank,
            IGameUI gameUI)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _playerBank = playerBank;
            _gameUI = gameUI;
        }

        public void Enter(string payload)
        {
            _gameUI.OpenScreen(ScreenID.Splash);
            _sceneLoader.LoadGameLevel(payload, OnLoaded);
        }

        public void Exit()
        {
        }

        private void OnLoaded()
        {
            _gameUI.OpenScreen(ScreenID.Gameplay);
            _stateMachine.Enter<GameLoopState>();
        }
    }
}