using UnityEngine;

namespace Assets.Scripts.StaticData
{
    [CreateAssetMenu(fileName = "Bank Static Data", menuName = "Static Data/Bank")]
    public class BankStaticData : ScriptableObject
    {
        [SerializeField, Min(0)]
        private int _incomeFromBank = 5;

        [SerializeField, Min(0)]
        private int _incomeFromButcherShop = 7;

        [SerializeField, Min(0)]
        private int _startAmountOfMeat = 100;

        [SerializeField, Min(0)]
        private int _startNumberOfCoins = 100;

        public int IncomeFromBank => _incomeFromBank;
        public int IncomeFromButcherShop => _incomeFromButcherShop;
        public int StartAmountOfMeat => _startAmountOfMeat;
        public int StartNumberOfCoins => _startNumberOfCoins;
    }
}