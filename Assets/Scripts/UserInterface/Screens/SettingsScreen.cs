using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UserInterface.Screens
{
    public class SettingsScreen : Screen
    {
        [SerializeField]
        private Button _close;

        public override ScreenID ID => ScreenID.Settings;

        public override void Activate()
        {
            _close.onClick.AddListener(OnClickClose);

            base.Activate();
        }

        public override void Deactivate()
        {
            _close.onClick.RemoveListener(OnClickClose);

            base.Deactivate();
        }

        private void OnClickClose()
        {
            GameUI.OpenScreen(ScreenID.Menu);
        }
    }
}