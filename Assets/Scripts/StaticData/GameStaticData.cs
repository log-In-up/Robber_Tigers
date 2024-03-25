using UnityEngine;

namespace Assets.Scripts.StaticData
{
    [CreateAssetMenu(fileName = "Game Static Data", menuName = "Static Data/Game")]
    public class GameStaticData : ScriptableObject
    {
        [SerializeField, Min(0.01f)]
        private float _delayBeforeScreensaver = 2.5f;

        [SerializeField]
        private string _gameScene;

        [SerializeField]
        private string _gameScreensaver;

        [SerializeField, Min(0.01f)]
        private float _increasedTigerSpeed = 2.0f;

        [SerializeField]
        private string _initialScene;

        [SerializeField, Min(0.01f)]
        private float _normalTigerSpeed = 1.0f;

        [SerializeField, Min(0.01f)]
        private float _speedUpTime = 0.5f;

        public float DelayBeforeScreensaver => _delayBeforeScreensaver;
        public string GameScene => _gameScene;
        public string GameScreensaver => _gameScreensaver;
        public float IncreasedTigerSpeed => _increasedTigerSpeed;
        public string InitialScene => _initialScene;
        public float NormalTigerSpeed => _normalTigerSpeed;
        public float SpeedUpTime => _speedUpTime;
    }
}