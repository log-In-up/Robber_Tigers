using Assets.Scripts.Infrastructure;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrastructure.Services.Buildings;
using Assets.Scripts.Infrastructure.Services.Factory;
using Assets.Scripts.Infrastructure.Services.Input;
using Assets.Scripts.Infrastructure.Services.PlayerBank;
using Assets.Scripts.Infrastructure.Services.UserInterface;
using Assets.Scripts.Infrastructure.States;
using Assets.Scripts.UserInterface.Elements;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

namespace Assets.Scripts.UserInterface.Screens
{
    public class GameplayScreen : Screen
    {
        [SerializeField]
        private AddMenu _addMenu;

        [SerializeField]
        private Button _exit;

        [SerializeField]
        private GameObject _hint;

        [SerializeField]
        private TextMeshProUGUI _totalCoins;

        [SerializeField]
        private TextMeshProUGUI _totalMeat;

        private IGameStateMachine _gameStateMachine;
        private IGameUI _gameUI;
        private InputActions _inputActions;
        private IPlayerBank _playerBank;
        private ISceneLoader _sceneLoader;

        public override ScreenID ID => ScreenID.Gameplay;

        public override void Activate()
        {
            _exit.onClick.AddListener(OnClickExit);

            _playerBank.OnChangeCoins += OnChangeCoins;
            _playerBank.OnChangeMeat += OnChangeMeat;

            _inputActions.Enable();
            _inputActions.UI.Click.performed += OnClick;
            _inputActions.UI.Click.performed += HintPerformed;

            InitializeScreenObjects();
            _addMenu.UpdateMenu();

            base.Activate();
        }

        public override void Deactivate()
        {
            _exit.onClick.RemoveListener(OnClickExit);

            _playerBank.OnChangeCoins -= OnChangeCoins;
            _playerBank.OnChangeMeat -= OnChangeMeat;

            _inputActions.UI.Click.performed -= OnClick;
            _inputActions.UI.Click.performed -= HintPerformed;
            _inputActions.Disable();

            _exit.interactable = true;

            base.Deactivate();
        }

        public override void Setup(ServiceLocator serviceLocator)
        {
            _gameStateMachine = serviceLocator.GetService<IGameStateMachine>();
            _gameUI = serviceLocator.GetService<IGameUI>();
            _playerBank = serviceLocator.GetService<IPlayerBank>();
            _sceneLoader = serviceLocator.GetService<ISceneLoader>();

            _inputActions = new InputActions();
            _addMenu.Initialize(_playerBank,
                serviceLocator.GetService<IBuildingsService>(),
                serviceLocator.GetService<IGameFactory>());

            base.Setup(serviceLocator);
        }

        private void HintPerformed(CallbackContext context)
        {
            SwapHintToAddMenu(false);

            _inputActions.UI.Click.performed -= HintPerformed;
        }

        private void InitializeScreenObjects()
        {
            _exit.interactable = true;

            _totalMeat.text = _playerBank.GameData.CurrentAmountOfMeat.ToString();
            _totalCoins.text = _playerBank.GameData.CurrentNumberOfCoins.ToString();

            SwapHintToAddMenu(true);
        }

        private void OnChangeCoins(int value)
        {
            _totalCoins.text = value.ToString();
        }

        private void OnChangeMeat(int value)
        {
            _totalMeat.text = value.ToString();
        }

        private void OnClick(CallbackContext context)
        {
        }

        private void OnClickExit()
        {
            _exit.interactable = false;

            _gameUI.OpenScreen(ScreenID.Splash);
            _sceneLoader.LoadScreensaverScene(OnSceneLoad);
        }

        private void OnSceneLoad()
        {
            _gameUI.OpenScreen(ScreenID.Menu);
            _gameStateMachine.Enter<PreGameLoopState>();
        }

        private void SwapHintToAddMenu(bool value)
        {
            _hint.SetActive(value);
            _addMenu.gameObject.SetActive(!value);
        }
    }
}