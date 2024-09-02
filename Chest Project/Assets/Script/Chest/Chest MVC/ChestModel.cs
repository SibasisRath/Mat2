using UnityEngine;

namespace ChestProject.Chest
{
    public class ChestModel
    {
        private int goldCoinsWillBeCollected;
        private int gemsCoinsWillBeCollected;

        // Public Variables
        public ChestTypes ChestTypes { get; private set; }
        private int minCoinCount;
        private int maxCoinCount;
        private int minGemCount;
        private int maxGemCount;
        private int gemsNeededToUnlock;
        public Sprite ClosedChestSprite { get; private set; }
        public Sprite OpenChestSprite { get; private set; }
        public int TimerSecs { get; private set; }
        public int GoldCoinsWillBeCollected { get => goldCoinsWillBeCollected; }
        public int GemsWillBeCollected { get => gemsCoinsWillBeCollected; }
        public int GemsNeededToUnlock 
        {
            get => gemsNeededToUnlock;
            set 
            {
                if (value > 0)
                {
                    gemsNeededToUnlock = value;
                }
                else
                {
                    gemsNeededToUnlock = 0;
                }

            }
        }
        public ChestModel() {}

        public void AssigningValues(ChestSO chestScriptableObject)
        {
            this.ChestTypes = chestScriptableObject.ChestTypes;
            this.minCoinCount = chestScriptableObject.MinCoinCount;
            this.maxCoinCount = chestScriptableObject.MaxCoinCount;
            this.minGemCount = chestScriptableObject.MinGemCount;
            this.maxGemCount = chestScriptableObject.MaxGemCount;
            this.ClosedChestSprite = chestScriptableObject.ClosedChestSprite;
            this.OpenChestSprite = chestScriptableObject.OpenChestSprite;
            this.TimerSecs = chestScriptableObject.TimerSecs;

            GenerateRandomCurrency();
            InitiallyGemsRequiredToUnlock();
        }

        private void GenerateRandomCurrency() 
        {
            System.Random random = new ();
            goldCoinsWillBeCollected = random.Next(minCoinCount, maxCoinCount + 1);
            gemsCoinsWillBeCollected = random.Next(minGemCount, maxGemCount + 1);
        }

        private void InitiallyGemsRequiredToUnlock()
        {
            gemsNeededToUnlock = Mathf.CeilToInt((float)TimerSecs / 600);
        }
        ~ChestModel() { }
    }
}

