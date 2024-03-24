using Assets.Scripts.Infrastructure.Services;
using System;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public interface ISceneLoader : IService
    {
        void Load(string sceneName, Action onLoaded = null);

        AsyncOperation LoadGameLevel(string sceneName, Action onLoaded = null);

        void LoadScreensaverScene(float delay = 0, Action onLoaded = null);
    }
}