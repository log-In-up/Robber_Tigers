using Assets.Scripts.Data;
using Assets.Scripts.StaticData;

namespace Assets.Scripts.Infrastructure.Services.PlayerBank
{
    public class PlayerBankService : IPlayerBank
    {
        private readonly BankStaticData _bankStaticData;

        private GameData _gameData;

        public PlayerBankService(BankStaticData bankStaticData)
        {
            _bankStaticData = bankStaticData;
        }

        public event IPlayerBank.CurrencyEventHandler OnChangeCoins, OnChangeMeat;

        public GameData GameData => _gameData;

        public bool CanWithdrawCoins(int value)
        {
            return _gameData.CurrentNumberOfCoins - value >= 0;
        }

        public bool CanWithdrawMeat(int value)
        {
            return _gameData.CurrentAmountOfMeat - value >= 0;
        }

        public void DepositCoins(int value)
        {
            _gameData.CurrentNumberOfCoins += value;
            OnChangeCoins?.Invoke(_gameData.CurrentNumberOfCoins);
        }

        public void DepositMeat(int value)
        {
            _gameData.CurrentAmountOfMeat += value;
            OnChangeMeat?.Invoke(_gameData.CurrentAmountOfMeat);
        }

        public void LoadData()
        {
            _gameData = new GameData(_bankStaticData);
        }

        public void WithdrawCoins(int value)
        {
            _gameData.CurrentNumberOfCoins -= value;
            OnChangeCoins?.Invoke(_gameData.CurrentNumberOfCoins);
        }

        public void WithdrawMeat(int value)
        {
            _gameData.CurrentAmountOfMeat -= value;
            OnChangeMeat?.Invoke(_gameData.CurrentAmountOfMeat);
        }
    }
}