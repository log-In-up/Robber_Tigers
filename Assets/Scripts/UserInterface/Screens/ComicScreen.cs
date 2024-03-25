using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrastructure.Services.Input;
using Assets.Scripts.Infrastructure.States;
using Assets.Scripts.StaticData;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace Assets.Scripts.UserInterface.Screens
{
    public class ComicScreen : Screen
    {
        [SerializeField]
        private List<GameObject> _comicPages;

        [SerializeField]
        private GameStaticData _gameStaticData;

        private int _currentPage;
        private IGameStateMachine _gameStateMachine;
        private InputActions _inputActions;
        public override ScreenID ID => ScreenID.Comic;

        public override void Activate()
        {
            _inputActions.Enable();

            _currentPage = 0;
            foreach (GameObject page in _comicPages)
            {
                page.SetActive(false);
            }

            _comicPages[_currentPage].SetActive(true);

            base.Activate();

            _inputActions.UI.Click.performed += OnClick;
        }

        public override void Deactivate()
        {
            _inputActions.UI.Click.performed -= OnClick;

            base.Deactivate();

            _currentPage = 0;
            foreach (GameObject page in _comicPages)
            {
                page.SetActive(false);
            }

            _inputActions.Disable();
        }

        public override void Setup(ServiceLocator serviceLocator)
        {
            _gameStateMachine = serviceLocator.GetService<IGameStateMachine>();

            base.Setup(serviceLocator);

            _inputActions = new InputActions();
        }

        private void OnClick(CallbackContext context)
        {
            _comicPages[_currentPage].SetActive(false);

            if (_currentPage + 1 != _comicPages.Count)
            {
                _currentPage = (_currentPage + 1) % _comicPages.Count;

                _comicPages[_currentPage].SetActive(true);
            }
            else
            {
                _gameStateMachine.Enter<LoadLevelState, string>(_gameStaticData.GameScene);
            }
        }
    }
}