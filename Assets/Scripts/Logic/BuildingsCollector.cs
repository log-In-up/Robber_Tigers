using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrastructure.Services.Buildings;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    public class BuildingsCollector : MonoBehaviour
    {
        [SerializeField]
        private List<Bank> _banks;

        [SerializeField]
        private List<ButcheryShop> _butcheryShops;

        private void Awake()
        {
            ServiceLocator container = ServiceLocator.Container;

            IBuildingsService buildingsService = container.GetService<IBuildingsService>();

            buildingsService.SendBuildings(_banks, _butcheryShops);
            buildingsService.Enable();
        }
    }
}