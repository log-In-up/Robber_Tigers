using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UserInterface.Screens
{
    public class MenuScreen : Screen
    {
        [SerializeField]
        private Button _leaderboard;

        [SerializeField]
        private Button _offer;

        [SerializeField]
        private Button _play;

        [SerializeField]
        private Button _settings;

        public override ScreenID ID => ScreenID.Menu;

        public override void Activate()
        {
            _leaderboard.onClick.AddListener(OnClickLeaderboard);
            _offer.onClick.AddListener(OnClickOffer);
            _play.onClick.AddListener(OnClickPlay);
            _settings.onClick.AddListener(OnClickSettings);

            _play.interactable = true;

            base.Activate();
        }

        public override void Deactivate()
        {
            _leaderboard.onClick.RemoveListener(OnClickLeaderboard);
            _offer.onClick.RemoveListener(OnClickOffer);
            _play.onClick.RemoveListener(OnClickPlay);
            _settings.onClick.RemoveListener(OnClickSettings);

            base.Deactivate();

            _play.interactable = true;
        }

        private void OnClickLeaderboard()
        {
        }

        private void OnClickOffer()
        {
        }

        private void OnClickPlay()
        {
            _play.interactable = false;

            GameUI.OpenScreen(ScreenID.Comic);
        }

        private void OnClickSettings()
        {
            GameUI.OpenScreen(ScreenID.Settings);
        }
    }
}