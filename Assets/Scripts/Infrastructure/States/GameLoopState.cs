using Assets.Scripts.Infrastructure.Services.Buildings;
using Assets.Scripts.Infrastructure.Services.Factory;
using Assets.Scripts.Infrastructure.Services.Input;
using Assets.Scripts.Infrastructure.Services.UserInterface;
using Assets.Scripts.StaticData;
using Assets.Scripts.UserInterface;
using Dreamteck.Splines;
using System.Collections;
using UnityEngine;
using CallbackContext = UnityEngine.InputSystem.InputAction.CallbackContext;

namespace Assets.Scripts.Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly IBuildingsService _buildingsService;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IGameFactory _gameFactory;
        private readonly GameStaticData _gameStaticData;
        private readonly IGameUI _gameUI;
        private readonly GameStateMachine _stateMachine;
        private Coroutine _coroutine;
        private InputActions _inputActions;

        public GameLoopState(GameStateMachine stateMachine,
            GameStaticData gameStaticData,
            ICoroutineRunner coroutineRunner,
            IBuildingsService buildingsService,
            IGameFactory gameFactory,
            IGameUI gameUI)
        {
            _stateMachine = stateMachine;
            _gameStaticData = gameStaticData;
            _coroutineRunner = coroutineRunner;
            _buildingsService = buildingsService;
            _gameFactory = gameFactory;
            _gameUI = gameUI;

            _inputActions = new InputActions();
        }

        ~GameLoopState()
        {
            _inputActions = null;
        }

        public void Enter()
        {
            _gameUI.OpenScreen(ScreenID.Gameplay);

            _inputActions.Enable();

            _inputActions.UI.Click.performed += ClickPerformed;
        }

        public void Exit()
        {
            _inputActions.UI.Click.performed -= ClickPerformed;
            _inputActions.Disable();

            if (_coroutine != null)
            {
                _coroutineRunner.StopCoroutine(_coroutine);
            }

            _buildingsService.Disable();
            _gameFactory.CleanUp();
        }

        private void ClickPerformed(CallbackContext callbackContext)
        {
            if (_coroutine != null)
            {
                _coroutineRunner.StopCoroutine(_coroutine);
            }
            _coroutineRunner.StartCoroutine(SpeedUp());
        }

        private IEnumerator SpeedUp()
        {
            foreach (SplineFollower follower in _gameFactory.Tigers)
            {
                follower.followSpeed = _gameStaticData.IncreasedTigerSpeed;
            }

            yield return new WaitForSeconds(_gameStaticData.SpeedUpTime);

            foreach (SplineFollower follower in _gameFactory.Tigers)
            {
                follower.followSpeed = _gameStaticData.NormalTigerSpeed;
            }
        }
    }
}