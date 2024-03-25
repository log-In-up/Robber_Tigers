using Assets.Scripts.Logic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Services.Buildings
{
    public interface IBuildingsService : IService
    {
        public void Disable();

        public void Enable();

        void InstantiateBank();

        void InstantiateButchery();

        void SendBuildings(List<Bank> banks, List<ButcheryShop> butcheryShops);
    }
}