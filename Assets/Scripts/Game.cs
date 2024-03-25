using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrastructure.Services.UserInterface;
using Assets.Scripts.Infrastructure.States;
using Assets.Scripts.StaticData;
using Assets.Scripts.UserInterface;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public class Game
    {
        private readonly GameStaticData _gameStaticData;
        private readonly GameUI _hud;
        private SceneLoader _sceneLoader;
        private ServiceInitializer _serviceInitializer;
        private ServiceLocator _serviceLocator;
        private GameStateMachine _stateMachine;

        public Game(ICoroutineRunner coroutineRunner, GameUI hud, GameStaticData gameStaticData, BankStaticData bankStaticData)
        {
            _gameStaticData = gameStaticData;
            _hud = hud;

            _sceneLoader = new SceneLoader(coroutineRunner, gameStaticData);
            _serviceLocator = new ServiceLocator();
            _stateMachine = new GameStateMachine(_sceneLoader, _serviceLocator, gameStaticData, coroutineRunner);
            _serviceInitializer = new ServiceInitializer(_stateMachine, _serviceLocator, gameStaticData, bankStaticData, _sceneLoader);
        }

        ~Game()
        {
            _sceneLoader = null;
            _serviceLocator = null;
            _serviceInitializer = null;
            _stateMachine = null;
        }

        public void Launch()
        {
            GameUI hud = CreateAndRegisterHUD();
            hud.OpenScreen(ScreenID.Splash);

            _serviceInitializer.RegisterServices();
            _stateMachine.InitializeStateMashine();

            hud.InitializeScreens(_serviceLocator);

            _sceneLoader.Load(_gameStaticData.InitialScene, EnterLoadLevel);
        }

        private GameUI CreateAndRegisterHUD()
        {
            GameUI hud = Object.Instantiate(_hud);
            Object.DontDestroyOnLoad(hud);

            _serviceLocator.RegisterService<IGameUI>(hud);

            return hud;
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadGameState>();
        }
    }
}