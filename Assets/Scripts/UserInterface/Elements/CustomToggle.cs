using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UserInterface.Elements
{
    [RequireComponent(typeof(Toggle))]
    public class CustomToggle : MonoBehaviour
    {
        [SerializeField]
        private Image _graphic;

        [SerializeField]
        private Sprite _whenIsOffSprite;

        [SerializeField]
        private Sprite _whenIsOnSprite;

        private Toggle _toggle;

        private void Awake()
        {
            _toggle = GetComponent<Toggle>();

            UpdateInteraction(_toggle.isOn);
        }

        private void OnDisable()
        {
            _toggle.onValueChanged.RemoveListener(UpdateInteraction);
        }

        private void OnEnable()
        {
            _toggle.onValueChanged.AddListener(UpdateInteraction);
        }

        private void UpdateInteraction(bool value)
        {
            _graphic.sprite = value ? _whenIsOnSprite : _whenIsOffSprite;
        }
    }
}