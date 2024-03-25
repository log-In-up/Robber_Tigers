using Assets.Scripts.StaticData;
using Dreamteck.Splines;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly GameStaticData _gameStaticData;
        private List<SplineFollower> _followerList;
        private Transform _spawnParent;
        private SplineComputer _splineComputer;
        private GameObject _tiger;

        public GameFactory(GameStaticData gameStaticData)
        {
            _followerList = new List<SplineFollower>();
            _gameStaticData = gameStaticData;
        }

        public List<SplineFollower> Tigers => _followerList;

        public void CleanUp()
        {
            _followerList.Clear();
        }

        public void CreateTiger()
        {
            GameObject tiger = Object.Instantiate(_tiger, _spawnParent);

            SplineFollower splineFollower = tiger.GetComponent<SplineFollower>();
            splineFollower.followSpeed = _gameStaticData.NormalTigerSpeed;
            _followerList.Add(splineFollower);

            splineFollower.spline = _splineComputer;
        }

        public void SendInstantiateData(SplineComputer splineComputer, Transform transformParent, GameObject tiger)
        {
            _splineComputer = splineComputer;
            _spawnParent = transformParent;
            _tiger = tiger;
        }
    }
}