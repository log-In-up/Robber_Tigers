using Assets.Scripts.Infrastructure.Services.PlayerBank;
using Assets.Scripts.Logic;
using Assets.Scripts.StaticData;
using System.Collections.Generic;

namespace Assets.Scripts.Infrastructure.Services.Buildings
{
    public class BuildingsService : IBuildingsService
    {
        private readonly BankStaticData _bankStaticData;
        private readonly IPlayerBank _playerBank;

        private int _bankIndex, _butcheryShopIndex;

        private List<Bank> _banks;
        private List<ButcheryShop> _butcheryShops;

        public BuildingsService(BankStaticData bankStaticData, IPlayerBank playerBank)
        {
            _bankStaticData = bankStaticData;
            _playerBank = playerBank;
        }

        public void Disable()
        {
            foreach (Bank bank in _banks)
            {
                bank.OnVisit -= OnVisitBank;
            }

            foreach (ButcheryShop butcheryShop in _butcheryShops)
            {
                butcheryShop.OnVisit += OnVisitButcheryShop;
            }
        }

        public void Enable()
        {
            foreach (Bank bank in _banks)
            {
                bank.OnVisit += OnVisitBank;
            }

            foreach (ButcheryShop butcheryShop in _butcheryShops)
            {
                butcheryShop.OnVisit += OnVisitButcheryShop;
            }
        }

        public void InstantiateBank()
        {
            _bankIndex++;
            _banks[_bankIndex].gameObject.SetActive(true);
        }

        public void InstantiateButchery()
        {
            _butcheryShopIndex++;
            _butcheryShops[_butcheryShopIndex].gameObject.SetActive(true);
        }

        public void SendBuildings(List<Bank> banks, List<ButcheryShop> butcheryShops)
        {
            _banks = banks;
            _butcheryShops = butcheryShops;

            _bankIndex = 0;
            _butcheryShopIndex = 0;

            for (int index = 0; index < _banks.Count; index++)
            {
                if (index <= _bankIndex)
                {
                    _banks[index].gameObject.SetActive(true);
                }
                else
                {
                    _banks[index].gameObject.SetActive(false);
                }
            }

            for (int index = 0; index < _butcheryShops.Count; index++)
            {
                if (index <= _butcheryShopIndex)
                {
                    _butcheryShops[index].gameObject.SetActive(true);
                }
                else
                {
                    _butcheryShops[index].gameObject.SetActive(false);
                }
            }
        }

        private void OnVisitBank()
        {
            _playerBank.DepositCoins(_bankStaticData.IncomeFromBank);
        }

        private void OnVisitButcheryShop()
        {
            _playerBank.DepositMeat(_bankStaticData.IncomeFromButcherShop);
        }
    }
}