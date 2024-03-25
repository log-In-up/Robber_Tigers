using Assets.Scripts.Data;

namespace Assets.Scripts.Infrastructure.Services.PlayerBank
{
    public interface IPlayerBank : IService
    {
        GameData GameData { get; }

        delegate void CurrencyEventHandler(int value);

        event CurrencyEventHandler OnChangeCoins, OnChangeMeat;

        bool CanWithdrawCoins(int value);

        bool CanWithdrawMeat(int value);

        void DepositCoins(int value);

        void DepositMeat(int value);

        void LoadData();

        void WithdrawCoins(int value);

        void WithdrawMeat(int value);
    }
}