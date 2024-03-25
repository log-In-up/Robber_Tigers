using Assets.Scripts.Infrastructure.Services.Buildings;
using Assets.Scripts.Infrastructure.Services.Factory;
using Assets.Scripts.Infrastructure.Services.PlayerBank;
using Assets.Scripts.StaticData;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UserInterface.Elements
{
    public class AddMenu : MonoBehaviour
    {
        [SerializeField]
        private Button _addBank;

        [SerializeField]
        private TextMeshProUGUI _addBankText;

        [SerializeField]
        private Button _addButchery;

        [SerializeField]
        private TextMeshProUGUI _addButcheryText;

        [SerializeField]
        private Button _addTiger;

        [SerializeField]
        private TextMeshProUGUI _addTigerText;

        private int _bankIndex, _butcheryIndex;

        private IBuildingsService _buildingsService;

        private IGameFactory _gameFactory;

        private IPlayerBank _playerBank;

        [SerializeField]
        private AddMenuStaticData _staticData;

        public void Initialize(IPlayerBank playerBank, IBuildingsService buildingsService, IGameFactory gameFactory)
        {
            _playerBank = playerBank;
            _buildingsService = buildingsService;
            _gameFactory = gameFactory;
        }

        public void UpdateMenu()
        {
            _bankIndex = 0;
            _butcheryIndex = 0;

            _addBankText.text = _staticData.BankPriceList[_bankIndex].ToString();
            _addButcheryText.text = _staticData.ButcherShopPriceList[_butcheryIndex].ToString();
            _addTigerText.text = _staticData.TigerPrice.ToString();

            UpdateInteractionMenu();
        }

        private void OnChangeCoins(int value)
        {
            UpdateInteractionMenu();
        }

        private void OnChangeMeat(int value)
        {
            UpdateInteractionMenu();
        }

        private void OnClickAddBank()
        {
            if (_bankIndex + 1 <= _staticData.BankPriceList.Count - 1)
            {
                _playerBank.WithdrawMeat(_staticData.BankPriceList[_bankIndex]);

                _bankIndex++;
                _addBankText.text = _staticData.BankPriceList[_bankIndex].ToString();
            }
            else
            {
                _addBankText.text = "MAX";
                _addBank.interactable = false;
            }

            _buildingsService.InstantiateBank();
        }

        private void OnClickAddButchery()
        {
            if (_butcheryIndex + 1 <= _staticData.ButcherShopPriceList.Count - 1)
            {
                _playerBank.WithdrawCoins(_staticData.ButcherShopPriceList[_butcheryIndex]);

                _butcheryIndex++;
                _addButcheryText.text = _staticData.ButcherShopPriceList[_butcheryIndex].ToString();
            }
            else
            {
                _addButcheryText.text = "MAX";
                _addButchery.interactable = false;
            }

            _buildingsService.InstantiateButchery();
        }

        private void OnClickAddTiger()
        {
            _playerBank.WithdrawCoins(_staticData.TigerPrice);

            _gameFactory.CreateTiger();
        }

        private void OnDisable()
        {
            _addBank.onClick.RemoveListener(OnClickAddBank);
            _addButchery.onClick.RemoveListener(OnClickAddButchery);
            _addTiger.onClick.RemoveListener(OnClickAddTiger);

            _playerBank.OnChangeCoins -= OnChangeCoins;
            _playerBank.OnChangeMeat -= OnChangeMeat;
        }

        private void OnEnable()
        {
            _addBank.onClick.AddListener(OnClickAddBank);
            _addButchery.onClick.AddListener(OnClickAddButchery);
            _addTiger.onClick.AddListener(OnClickAddTiger);

            _playerBank.OnChangeCoins += OnChangeCoins;
            _playerBank.OnChangeMeat += OnChangeMeat;
        }

        private void UpdateInteractionMenu()
        {
            if (_bankIndex + 1 < _staticData.BankPriceList.ToArray().Length)
            {
                _addBank.interactable = _playerBank.CanWithdrawMeat(_staticData.BankPriceList[_bankIndex]);
            }
            else
            {
                _addBank.interactable = false;
            }

            if (_butcheryIndex + 1 < _staticData.ButcherShopPriceList.ToArray().Length)
            {
                _addButchery.interactable = _playerBank.CanWithdrawCoins(_staticData.ButcherShopPriceList[_butcheryIndex]);
            }
            else
            {
                _addButchery.interactable = false;
            }

            _addTiger.interactable = _playerBank.CanWithdrawCoins(_staticData.TigerPrice);
        }
    }
}