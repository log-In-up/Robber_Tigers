using Assets.Scripts.StaticData;

namespace Assets.Scripts.Data
{
    public class GameData
    {
        public int CurrentAmountOfMeat;
        public int CurrentNumberOfCoins;

        public GameData(BankStaticData bankStaticData)
        {
            CurrentAmountOfMeat = bankStaticData.StartAmountOfMeat;
            CurrentNumberOfCoins = bankStaticData.StartNumberOfCoins;
        }
    }
}