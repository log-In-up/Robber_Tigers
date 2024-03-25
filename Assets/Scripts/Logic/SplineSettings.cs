using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrastructure.Services.Factory;
using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    public class SplineSettings : MonoBehaviour
    {
        [SerializeField]
        private SplineComputer _splineComputer;

        [SerializeField]
        private Transform _transformParent;

        [SerializeField]
        private GameObject _tiger;

        private void Awake()
        {
            ServiceLocator serviceLocator = ServiceLocator.Container;

            IGameFactory gameFactory = serviceLocator.GetService<IGameFactory>();
            gameFactory.SendInstantiateData(_splineComputer, _transformParent, _tiger);
            gameFactory.CreateTiger();
        }
    }
}