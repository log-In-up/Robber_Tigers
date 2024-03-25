using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrastructure.Services.Buildings;
using Assets.Scripts.Infrastructure.Services.Factory;
using Assets.Scripts.Infrastructure.Services.PlayerBank;
using Assets.Scripts.Infrastructure.Services.UserInterface;
using Assets.Scripts.StaticData;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Infrastructure.States
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly SceneLoader _sceneLoader;
        private readonly ServiceLocator _serviceLocator;
        private readonly GameStaticData _gameStaticData;
        private readonly ICoroutineRunner _coroutineRunner;
        private IExitableState _activeState;
        private Dictionary<Type, IExitableState> _states;

        public GameStateMachine(SceneLoader sceneLoader,
            ServiceLocator serviceLocator,
            GameStaticData gameStaticData,
            ICoroutineRunner coroutineRunner)
        {
            _sceneLoader = sceneLoader;
            _serviceLocator = serviceLocator;
            _gameStaticData = gameStaticData;
            _coroutineRunner = coroutineRunner;
        }

        ~GameStateMachine()
        {
            _states.Clear();
            _states = null;

            _activeState = null;
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();

            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();

            state.Enter(payload);
        }

        public void InitializeStateMashine()
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(GameLoopState)] = new GameLoopState(this,
                    _gameStaticData,
                    _coroutineRunner,
                    _serviceLocator.GetService<IBuildingsService>(),
                    _serviceLocator.GetService<IGameFactory>(),
                    _serviceLocator.GetService<IGameUI>()),
                [typeof(LoadLevelState)] = new LoadLevelState(this,
                    _sceneLoader,
                    _serviceLocator.GetService<IPlayerBank>(),
                    _serviceLocator.GetService<IGameUI>()),
                [typeof(LoadGameState)] = new LoadGameState(this,
                    _sceneLoader,
                    _gameStaticData,
                    _serviceLocator.GetService<IGameUI>()),
                [typeof(PreGameLoopState)] = new PreGameLoopState(this)
            };
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}