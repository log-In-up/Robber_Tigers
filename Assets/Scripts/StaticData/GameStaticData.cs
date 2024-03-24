using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.StaticData
{
    [CreateAssetMenu(fileName = "Game Static Data", menuName = "Static Data/Game")]
    public class GameStaticData : ScriptableObject
    {
        [SerializeField, Min(0.01f)]
        private float _delayBeforeScreensaver = 2.5f;

        [SerializeField]
        private SceneAsset _gameScreensaver;

        [SerializeField]
        private SceneAsset _initialScene;

        public float DelayBeforeScreensaver => _delayBeforeScreensaver;
        public string GameScreensaver => _gameScreensaver.name;
        public string InitialScene => _initialScene.name;
    }
}