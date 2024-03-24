using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UserInterface.Screens
{
    public class ComicScreen : Screen
    {
        [SerializeField]
        private List<GameObject> _comicPages;

        public override ScreenID ID => ScreenID.Comic;

        public override void Activate()
        {
            _comicPages[0].SetActive(true);

            base.Activate();
        }

        public override void Deactivate()
        {
            foreach (GameObject page in _comicPages)
            {
                page.SetActive(false);
            }

            base.Deactivate();
        }
    }
}