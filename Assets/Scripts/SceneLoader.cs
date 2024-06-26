﻿using Assets.Scripts.StaticData;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Infrastructure
{
    public class SceneLoader : ISceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly GameStaticData _gameStaticData;

        public SceneLoader(ICoroutineRunner coroutineRunner, GameStaticData gameStaticData)
        {
            _coroutineRunner = coroutineRunner;
            _gameStaticData = gameStaticData;
        }

        public void Load(string sceneName, Action onLoaded = null)
        {
            _coroutineRunner.StartCoroutine(LoadScene(sceneName, 0, onLoaded));
        }

        public void LoadGameLevel(string sceneName, Action onLoaded = null)
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

            _coroutineRunner.StartCoroutine(LoadScene(asyncOperation, onLoaded));
        }

        public void LoadScreensaverScene(float delay, Action onLoaded = null)
        {
            _coroutineRunner.StartCoroutine(LoadScene(_gameStaticData.GameScreensaver, delay, onLoaded));
        }

        public void LoadScreensaverScene(Action onLoaded = null)
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(_gameStaticData.GameScreensaver, LoadSceneMode.Single);

            _coroutineRunner.StartCoroutine(LoadScene(asyncOperation, onLoaded));
        }

        private IEnumerator LoadScene(string sceneName, float delay, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == sceneName)
            {
                onLoaded?.Invoke();
                yield break;
            }

            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            waitNextScene.allowSceneActivation = false;

            while (!waitNextScene.isDone)
            {
                yield return null;
            }

            yield return new WaitForSeconds(delay);
            waitNextScene.allowSceneActivation = true;

            onLoaded?.Invoke();
        }

        private IEnumerator LoadScene(AsyncOperation asyncOperation, Action onLoaded = null)
        {
            while (!asyncOperation.isDone)
            {
                yield return null;
            }

            onLoaded?.Invoke();
        }
    }
}