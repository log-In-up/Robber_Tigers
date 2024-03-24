using Assets.Scripts.Infrastructure.Services.UserInterface;
using Assets.Scripts.StaticData;
using Assets.Scripts.UserInterface;

namespace Assets.Scripts.Infrastructure.States
{
    public class LoadGameState : IState
    {
        private readonly IGameUI _gameUI;
        private readonly SceneLoader _sceneLoader;
        private readonly GameStaticData _gameStaticData;
        private readonly GameStateMachine _stateMachine;

        public LoadGameState(GameStateMachine stateMachine,
            SceneLoader sceneLoader,
            GameStaticData gameStaticData,
            IGameUI gameUI)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _gameStaticData = gameStaticData;
            _gameUI = gameUI;
        }

        public void Enter()
        {
            _sceneLoader.LoadScreensaverScene(_gameStaticData.DelayBeforeScreensaver, OnSceneLoad);
        }

        public void Exit()
        {
        }

        private void OnSceneLoad()
        {
            _gameUI.OpenScreen(ScreenID.Menu);
            _stateMachine.Enter<PreGameLoopState>();
        }
    }
}