using Dreamteck.Splines;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Services.Factory
{
    public interface IGameFactory : IService
    {
        List<SplineFollower> Tigers { get; }

        void CleanUp();

        void CreateTiger();

        void SendInstantiateData(SplineComputer splineComputer, Transform transformParent, GameObject tiger);
    }
}