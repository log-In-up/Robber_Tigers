using Assets.Scripts.Infrastructure.Services.Buildings;
using Assets.Scripts.Infrastructure.Services.Factory;
using Assets.Scripts.Infrastructure.Services.Input;
using Assets.Scripts.Infrastructure.Services.PlayerBank;
using Assets.Scripts.Infrastructure.Services.Upgrades;
using Assets.Scripts.Infrastructure.States;
using Assets.Scripts.StaticData;

namespace Assets.Scripts.Infrastructure.Services
{
    public class ServiceInitializer
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly ServiceLocator _serviceLocator;
        private readonly GameStaticData _gameStaticData;
        private readonly BankStaticData _bankStaticData;
        private readonly ISceneLoader _sceneLoader;

        public ServiceInitializer(IGameStateMachine stateMachine,
            ServiceLocator serviceLocator,
            GameStaticData gameStaticData,
            BankStaticData bankStaticData,
            ISceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _serviceLocator = serviceLocator;
            _gameStaticData = gameStaticData;
            _bankStaticData = bankStaticData;
            _sceneLoader = sceneLoader;
        }

        public void RegisterServices()
        {
            _serviceLocator.RegisterService(_stateMachine);
            _serviceLocator.RegisterService(_sceneLoader);
            RegisterAndLoadPlayerBankService();
            _serviceLocator.RegisterService<IUpgradableService>(new UpgradableService());

            _serviceLocator.RegisterService<IBuildingsService>(new BuildingsService(_bankStaticData,
                _serviceLocator.GetService<IPlayerBank>()));

            _serviceLocator.RegisterService<IGameFactory>(new GameFactory(_gameStaticData));
        }

        private void RegisterAndLoadPlayerBankService()
        {
            PlayerBankService playerBankService = new PlayerBankService(_bankStaticData);
            playerBankService.LoadData();
            _serviceLocator.RegisterService<IPlayerBank>(playerBankService);
        }
    }
}