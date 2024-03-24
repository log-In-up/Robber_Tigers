using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrastructure.Services.UserInterface;
using System.Collections.Generic;
using UnityEngine;
using Screen = Assets.Scripts.UserInterface.Screens.Screen;

namespace Assets.Scripts.UserInterface
{
    [DisallowMultipleComponent]
    public class GameUI : MonoBehaviour, IGameUI
    {
        [SerializeField]
        private List<Screen> _screens;

        public void InitializeScreens(ServiceLocator serviceLocator)
        {
            foreach (Screen screen in _screens)
            {
                screen.SetScreenData(this);

                screen.Setup(serviceLocator);
            }
        }

        public void OpenScreen(ScreenID screenID)
        {
            foreach (Screen screen in _screens)
            {
                if (screen.ID.Equals(screenID))
                {
                    screen.Activate();
                }
                else if (screen.IsOpen)
                {
                    screen.Deactivate();
                }
            }
        }

        public void OpenScreen<TPayload>(ScreenID screenID, TPayload payload) where TPayload : class
        {
            foreach (Screen screen in _screens)
            {
                if (screen.ID.Equals(screenID))
                {
                    screen.SendPayload(payload);
                    screen.Activate();
                }
                else if (screen.IsOpen)
                {
                    screen.Deactivate();
                }
            }
        }
    }
}