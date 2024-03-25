using Assets.Scripts.StaticData;
using Assets.Scripts.UserInterface;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    [DisallowMultipleComponent]
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField]
        private GameUI _hud;

        [SerializeField]
        private GameStaticData _gameStaticData;

        [SerializeField]
        private BankStaticData _bankStaticData;

        private Game _game;

        private void Awake()
        {
            _game = new Game(this, _hud, _gameStaticData, _bankStaticData);

            DontDestroyOnLoad(gameObject);

            _game.Launch();
        }

        private void OnApplicationQuit()
        {
            _game = null;
        }
    }
}