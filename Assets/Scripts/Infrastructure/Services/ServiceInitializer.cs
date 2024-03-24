using Assets.Scripts.Infrastructure.States;
using Assets.Scripts.StaticData;

namespace Assets.Scripts.Infrastructure.Services
{
    public class ServiceInitializer
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly ServiceLocator _serviceLocator;
        private readonly GameStaticData _gameStaticData;
        private readonly ISceneLoader _sceneLoader;

        public ServiceInitializer(IGameStateMachine stateMachine,
            ServiceLocator serviceLocator,
            GameStaticData gameStaticData,
            ISceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _serviceLocator = serviceLocator;
            _gameStaticData = gameStaticData;
            _sceneLoader = sceneLoader;
        }

        public void RegisterServices()
        {
            _serviceLocator.RegisterService(_stateMachine);
            _serviceLocator.RegisterService(_sceneLoader);
        }
    }
}