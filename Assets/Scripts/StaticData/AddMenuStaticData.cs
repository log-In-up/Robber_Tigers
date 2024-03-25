using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.StaticData
{
    [CreateAssetMenu(fileName = "Add Menu Static Data", menuName = "Static Data/Add Menu")]
    public class AddMenuStaticData : ScriptableObject
    {
        [SerializeField, Min(0)]
        private List<int> _bankPriceList;

        [SerializeField, Min(0)]
        private List<int> _butcherShopPriceList;

        [SerializeField, Min(0)]
        private int _tigerPrice = 500;

        public List<int> BankPriceList => _bankPriceList;
        public List<int> ButcherShopPriceList => _butcherShopPriceList;
        public int TigerPrice => _tigerPrice;
    }
}